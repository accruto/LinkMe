using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Query.Search.Engine.JobAds.Sort.ContentHandlers;
using com.browseengine.bobo.api;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.JobAds;
using org.apache.lucene.analysis;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Sort
{
    public class Indexer
    {
        private readonly Analyzer _contentAnalyzer;

        private readonly ContentHandler _contentHandler;
        private readonly IContentHandler _salaryHandler;
        private readonly IContentHandler _createdTimeHandler;
        private readonly IContentHandler _integratorHandler;
        private readonly IContentHandler _jobTypesHandler;

        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;

        public Indexer(IBooster booster, IJobAdFlagListsQuery jobAdFlagListsQuery)
        {
            _contentAnalyzer = new SimpleAnalyzer();

            _contentHandler = new ContentHandler();
            _salaryHandler = new SalaryContentHandler(booster);
            _createdTimeHandler = new CreatedTimeContentHandler(booster);
            _integratorHandler = new IntegratorContentHandler(booster);
            _jobTypesHandler = new JobTypesContentHandler(booster);

            _jobAdFlagListsQuery = jobAdFlagListsQuery;
        }

        public void IndexContent(IndexWriter indexWriter, JobAdSortContent content, bool isNew)
        {
            var document = new Document();

            // ID

            var id = content.JobAd.Id.ToFieldValue();
            var idField = new Field(SearchFieldName.Id, id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS);
            idField.setOmitTermFreqAndPositions(true);
            document.add(idField);

            // Handlers.

            _contentHandler.AddContent(document, content);
            _salaryHandler.AddContent(document, content);
            _createdTimeHandler.AddContent(document, content);
            _integratorHandler.AddContent(document, content);
            _jobTypesHandler.AddContent(document, content);

            // Save the document.

            if (isNew)
                indexWriter.addDocument(document, _contentAnalyzer);
            else
                indexWriter.updateDocument(new Term(SearchFieldName.Id, id), document, _contentAnalyzer);
        }

        public void DeleteContent(IndexWriter indexWriter, Guid id)
        {
            indexWriter.deleteDocuments(new Term(SearchFieldName.Id, id.ToFieldValue()));
        }

        public Filter GetFilter(IEnumerable<Guid> includeJobAdIds, IEnumerable<Guid> excludeJobAdIds)
        {
            var filters = new List<Filter>();

            if (includeJobAdIds != null)
                filters.Add(new SpecialsFilter(SearchFieldName.Id, false, includeJobAdIds.Select(id => id.ToFieldValue())));

            if (excludeJobAdIds != null)
                filters.Add(new SpecialsFilter(SearchFieldName.Id, true, excludeJobAdIds.Select(id => id.ToFieldValue())));

            return filters.Count == 0
                ? null
                : new ChainedFilter(filters.ToArray(), ChainedFilter.AND);
        }

        public LuceneSort GetSort(IMember member, JobAdSortQuery query)
        {
            switch (query.SortOrder)
            {
                case JobAdSortOrder.CreatedTime:
                    return _createdTimeHandler.GetSort(query);

                case JobAdSortOrder.JobType:
                    return _jobTypesHandler.GetSort(query);

                case JobAdSortOrder.Salary:
                    return _salaryHandler.GetSort(query);

                case JobAdSortOrder.Flagged:
                    return new LuceneSort(
                        new[] 
                        {
                            new BoboCustomSortField(SearchFieldName.Id, !query.ReverseSortOrder, new SpecialsComparatorSource(false, _jobAdFlagListsQuery.GetFlaggedJobAdIds(member).Select(id => id.ToFieldValue()), SearchFieldName.Id)),
                            SortField.FIELD_SCORE
                        });

                default:
                    return null;
            }
        }
    }
}
