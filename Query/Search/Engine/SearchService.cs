using System;
using System.Threading;
using System.Threading.Tasks;
using LinkMe.Framework.Host;
using LinkMe.Framework.Instrumentation;
using org.apache.lucene.analysis;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.store;

namespace LinkMe.Query.Search.Engine
{
    public abstract class SearchService<TContent, TCache>
        : IChannelAware
        where TContent : Content
        where TCache : class
    {
        private readonly EventSource _eventSource;
        protected readonly ISearchEngineQuery _searchEngineQuery;
        private static readonly Term MetaDocIdTerm = new Term(SearchFieldName.Id, Guid.Empty.ToFieldValue());

        private bool _initialiseIndex = true;
        private bool _rebuildIndex;
        private int _initialiseThreads = Environment.ProcessorCount * 10;
        private bool _monitorForChanges;
        private string _indexFolder;
        private int _monitorIntervalMilliseconds = 60 * 1000;
        private DateTime _lastMonitorTime;
        private volatile bool _stopped = true;
        private IndexWriter _indexWriter;

        protected SearchService(EventSource eventSource, ISearchEngineQuery searchEngineQuery, string indexFolder)
        {
            _eventSource = eventSource;
            _searchEngineQuery = searchEngineQuery;
            _indexFolder = indexFolder;
        }

        public bool InitialiseIndex
        {
            set { _initialiseIndex = value; }
        }

        public bool RebuildIndex
        {
            set { _rebuildIndex = value; }
        }

        public string IndexFolder
        {
            set { _indexFolder = value; }
        }

        public int InitialiseThreads
        {
            set { _initialiseThreads = value; }
        }

        public bool MonitorForChanges
        {
            set { _monitorForChanges = value; }
        }

        public TimeSpan MonitorInterval
        {
            set { _monitorIntervalMilliseconds = (int)value.TotalMilliseconds; }
        }

        void IChannelAware.OnOpen()
        {
            const string method = "OnOpen";

            try
            {
                var directory = new RAMDirectory();
                _indexWriter = CreateIndexWriter(directory);

                // Load and sync data.

                var newTimestamp = DateTime.Now;
                if (_initialiseIndex)
                    Initialise(directory, newTimestamp);

                OnIndexInitialised(directory);

                // Start polling.

                if (_monitorForChanges)
                {
                    _lastMonitorTime = newTimestamp;
                    _stopped = false;
                    var timer = new Timer(MonitorCallback);
                    timer.Change(_monitorIntervalMilliseconds, Timeout.Infinite);
                }
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.Error, method, "Unexpected exception.", ex);
                throw;
            }

            _eventSource.Raise(Event.Information, method, "Initialization complete.");
        }

        void IChannelAware.OnClose()
        {
        }

        void IChannelAware.OnStart()
        {
        }

        void IChannelAware.OnStop()
        {
            _stopped = true;
        }

        void IChannelAware.OnPause()
        {
        }

        void IChannelAware.OnContinue()
        {
        }

        void IChannelAware.OnShutdown()
        {
        }

        protected abstract IndexWriter CreateIndexWriter(Directory directory);
        protected abstract void OnIndexInitialised(Directory directory);
        protected abstract TContent GetContent(Guid id, TCache cache);
        protected abstract void IndexContent(IndexWriter indexWriter, TContent content, bool isNew);
        protected abstract void UnindexContent(IndexWriter indexWriter, Guid id);

        protected void ClearIndex()
        {
            if (_indexWriter == null)
            {
                var directory = new RAMDirectory();
                _indexWriter = CreateIndexWriter(directory);
            }
            else
            {
                _indexWriter.deleteAll();
                _indexWriter.commit();
            }
        }

        private void AddContent(DateTime? modifiedSince, bool isNew, TCache cache)
        {
            const string method = "AddContent";

            var ids = _searchEngineQuery.GetModified(modifiedSince);

            _eventSource.Raise(Event.Information, method, string.Format("Indexing {0} articles...", ids.Count));

            if (ids.Count == 0)
                return;

            var totalCount = 0;
            var options = new ParallelOptions { MaxDegreeOfParallelism = _initialiseThreads };
            Parallel.For(0, ids.Count, options, i =>
            {
                UpdateContent(ids[i], isNew, cache);

                var count = Interlocked.Increment(ref totalCount);
                if (count % 10000 == 0)
                    _eventSource.Raise(Event.Information, method, string.Format("Indexed {0} articles...", count));
            });
        }

        protected void UpdateContent(Guid id)
        {
            UpdateContent(id, false, null);
        }

        protected void IndexContent(Guid id)
        {
            var content = GetContent(id, null);
            if (content == null)
                return;
            IndexContent(_indexWriter, content, false);
        }

        protected void UnindexContent(Guid id)
        {
            UnindexContent(_indexWriter, id);
        }

        private void UpdateContent(Guid id, bool isNew, TCache cache)
        {
            var content = GetContent(id, cache);
            if (content == null)
                return;
            if (content.IsSearchable)
                IndexContent(_indexWriter, content, isNew);
            else
                UnindexContent(_indexWriter, id);
        }

        private void Initialise(Directory directory, DateTime initialPollingTime)
        {
            const string method = "InitialiseIndex";

            // Load search directory.

            if (!_rebuildIndex && !string.IsNullOrEmpty(_indexFolder) && System.IO.Directory.Exists(_indexFolder))
            {
                #region Log
                _eventSource.Raise(Event.Information, method, string.Format("Reading the index from '{0}'...", _indexFolder));
                #endregion

                _indexWriter.close();
                _indexWriter = null;
                ReadFileIndex(directory, _indexFolder);
                _indexWriter = CreateIndexWriter(directory);
            }

            // Index all content modified since last save.

            var oldTimestamp = GetMetaTimestamp();
            var isNew = !oldTimestamp.HasValue;

            AddContent(oldTimestamp, isNew, null);

            SetMetaTimestamp(initialPollingTime);
            _indexWriter.commit();

            #region Log
            _eventSource.Raise(Event.Information, method, "Indexing complete.");
            #endregion

            // Save search directory.

            if (!string.IsNullOrEmpty(_indexFolder))
            {
                #region Log
                _eventSource.Raise(Event.Information, method, "Saving the index...");
                #endregion

                if (System.IO.Directory.Exists(_indexFolder))
                    System.IO.Directory.Delete(_indexFolder, true);

                System.IO.Directory.CreateDirectory(_indexFolder);
                _indexWriter.close();
                _indexWriter = null;
                WriteFileIndex(directory, _indexFolder);
                _indexWriter = CreateIndexWriter(directory);
            }
        }

        private void MonitorCallback(object state)
        {
            const string method = "MonitorCallback";

            var timer = (Timer)state;
            timer.Dispose();
            var now = DateTime.Now;

            if (_stopped)
                return;

            try
            {
                var ids = _searchEngineQuery.GetModified(_lastMonitorTime);
                foreach (var id in ids)
                {
                    _eventSource.Raise(Event.Trace, method, "Updating content '" + id + "'.", Event.Arg("id", id));
                    UpdateContent(id, false, null);
                }
            }
            catch (Exception e)
            {
                _eventSource.Raise(Event.Error, method, "Unexpected exception while polling for changes", e);
            }

            _lastMonitorTime = now;
            timer = new Timer(MonitorCallback);
            timer.Change(_monitorIntervalMilliseconds, Timeout.Infinite);
        }

        protected IndexReader GetReader()
        {
            return _indexWriter.getReader();
        }

        private DateTime? GetMetaTimestamp()
        {
            var indexSearcher = new IndexSearcher(_indexWriter.getReader());

            // Find "meta" document.

            var query = new TermQuery(MetaDocIdTerm);
            var hits = indexSearcher.search(query, 1);

            if (hits.totalHits == 0)
                return null;

            // Get "meta" document timestamp.

            var metaDoc = indexSearcher.doc(hits.scoreDocs[0].doc);
            var timestamp = metaDoc.get(SearchFieldName.Timestamp);
            if (string.IsNullOrEmpty(timestamp))
                return null;

            return new DateTime(long.Parse(timestamp));
        }

        private void SetMetaTimestamp(DateTime timestamp)
        {
            var metaDoc = new Document();
            metaDoc.add(new Field(SearchFieldName.Id, MetaDocIdTerm.text(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            metaDoc.add(new NumericField(SearchFieldName.Timestamp, Field.Store.YES, false).setLongValue(timestamp.Ticks));
            _indexWriter.updateDocument(MetaDocIdTerm, metaDoc, new KeywordAnalyzer());
        }

        private static void ReadFileIndex(Directory directory, string indexFolder)
        {
            var fileDirectory = FSDirectory.open(new java.io.File(indexFolder));
            Directory.copy(fileDirectory, directory, false);
            fileDirectory.close();
        }

        private static void WriteFileIndex(Directory directory, string indexFolder)
        {
            var fileDirectory = FSDirectory.open(new java.io.File(indexFolder));
            Directory.copy(directory, fileDirectory, false);
            fileDirectory.close();
        }
    }
}
