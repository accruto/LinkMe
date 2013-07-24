using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Files.Data
{
    public class FilesRepository
        : Repository, IFilesRepository
    {
        private struct FileCriteria
        {
            public FileType FileType;
            public string FileName;
            public string MimeType;
            public int ContentLength;
            public Binary ContentHash;
        }

        private static readonly DataLoadOptions FileReferenceLoadOptions = DataOptions.CreateLoadOptions<FileReferenceEntity>(f => f.FileDataEntity);

        private static readonly Func<FilesDataContext, Guid, FileReference> GetFileReferenceQuery
            = CompiledQuery.Compile((FilesDataContext dc, Guid id)
                => (from f in dc.FileReferenceEntities
                    where f.id == id
                    select f.Map()).SingleOrDefault());

        private static readonly Func<FilesDataContext, string, Range, IQueryable<FileReference>> GetFileReferencesSkipTakeQuery
            = CompiledQuery.Compile((FilesDataContext dc, string ids, Range range)
                => (from f in dc.FileReferenceEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on f.id equals i.value
                    orderby f.createdTime descending, f.name
                    select f).Skip(range.Skip).Take(range.Take.Value).Select(f => f.Map()));

        private static readonly Func<FilesDataContext, string, Range, IQueryable<FileReference>> GetFileReferencesTakeQuery
            = CompiledQuery.Compile((FilesDataContext dc, string ids, Range range)
                => (from f in dc.FileReferenceEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on f.id equals i.value
                    orderby f.createdTime descending, f.name
                    select f).Take(range.Take.Value).Select(f => f.Map()));

        private static readonly Func<FilesDataContext, string, Range, IQueryable<FileReference>> GetFileReferencesSkipQuery
            = CompiledQuery.Compile((FilesDataContext dc, string ids, Range range)
                => (from f in dc.FileReferenceEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on f.id equals i.value
                    orderby f.createdTime descending, f.name
                    select f).Skip(range.Skip).Select(f => f.Map()));

        private static readonly Func<FilesDataContext, string, IQueryable<FileReference>> GetFileReferencesQuery
            = CompiledQuery.Compile((FilesDataContext dc, string ids)
                => (from f in dc.FileReferenceEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on f.id equals i.value
                    orderby f.createdTime descending, f.name
                    select f).Select(f => f.Map()));

        private static readonly Func<FilesDataContext, FileCriteria, FileReference> GetFileReferenceByCriteriaQuery
            = CompiledQuery.Compile((FilesDataContext dc, FileCriteria criteria)
                => (from f in dc.FileReferenceEntities
                    where f.name == criteria.FileName
                    && f.mimeType == criteria.MimeType
                    && f.FileDataEntity.context == (byte)criteria.FileType
                    && f.FileDataEntity.sizeBytes == criteria.ContentLength
                    && f.FileDataEntity.hash == criteria.ContentHash
                    select f.Map()).SingleOrDefault());

        private static readonly Func<FilesDataContext, FileCriteria, FileData> GetFileData
            = CompiledQuery.Compile((FilesDataContext dc, FileCriteria criteria)
                => (from f in dc.FileDataEntities
                    where f.context == (byte)criteria.FileType
                    && f.sizeBytes == criteria.ContentLength
                    && f.hash == criteria.ContentHash
                    select f.Map()).SingleOrDefault());

        public FilesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        FileReference IFilesRepository.GetFileReference(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFileReference(dc, id);
            }
        }

        IList<FileReference> IFilesRepository.GetFileReferences(IEnumerable<Guid> ids, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFileReferences(dc, ids, range).ToList();
            }
        }

        FileReference IFilesRepository.GetFileReference(FileType fileType, string fileName, string mediaType, int contentLength, byte[] contentHash)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new FileCriteria
                {
                    FileType = fileType,
                    FileName = fileName,
                    MimeType = mediaType,
                    ContentLength = contentLength,
                    ContentHash = contentHash
                };
                return GetFileReferenceByCriteria(dc, criteria);
            }
        }

        FileData IFilesRepository.GetFileData(FileType fileType, int contentLength, byte[] contentHash)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new FileCriteria
                {
                    FileType = fileType,
                    ContentLength = contentLength,
                    ContentHash = contentHash
                };
                return GetFileData(dc, criteria);
            }
        }

        void IFilesRepository.CreateFileReference(FileReference fileReference)
        {
            using (var dc = CreateContext())
            {
                dc.FileReferenceEntities.InsertOnSubmit(fileReference.Map());
                dc.SubmitChanges();
            }
        }

        void IFilesRepository.CreateFileData(FileData fileData)
        {
            using (var dc = CreateContext())
            {
                dc.FileDataEntities.InsertOnSubmit(fileData.Map());
                dc.SubmitChanges();
            }
        }

        private static FileReference GetFileReference(FilesDataContext dc, Guid id)
        {
            dc.LoadOptions = FileReferenceLoadOptions;
            return GetFileReferenceQuery(dc, id);
        }

        private static FileReference GetFileReferenceByCriteria(FilesDataContext dc, FileCriteria criteria)
        {
            dc.LoadOptions = FileReferenceLoadOptions;
            return GetFileReferenceByCriteriaQuery(dc, criteria);
        }

        private static IEnumerable<FileReference> GetFileReferences(FilesDataContext dc, IEnumerable<Guid> ids, Range range)
        {
            dc.LoadOptions = FileReferenceLoadOptions;

            ids = ids.Distinct();
            if (range.Skip == 0)
                return range.Take != null
                    ? GetFileReferencesTakeQuery(dc, new SplitList<Guid>(ids).ToString(), range)
                    : GetFileReferencesQuery(dc, new SplitList<Guid>(ids).ToString());
            return range.Take != null
                ? GetFileReferencesSkipTakeQuery(dc, new SplitList<Guid>(ids).ToString(), range)
                : GetFileReferencesSkipQuery(dc, new SplitList<Guid>(ids).ToString(), range);
        }

        private FilesDataContext CreateContext()
        {
            return CreateContext(c => new FilesDataContext(c));
        }
    }
}
