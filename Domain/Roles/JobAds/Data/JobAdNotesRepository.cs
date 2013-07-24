using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.JobAds.Data
{
    public class JobAdNotesRepository
        : Repository, IJobAdNotesRepository
    {
        #region Compiled queries

        private static readonly Func<JobAdsDataContext, Guid, JobAdNoteEntity> GetJobAdNoteEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from n in dc.JobAdNoteEntities
                    where n.id == id
                    select n).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, Guid, IOrderedQueryable<JobAdNoteEntity>> GetJobAdNoteEntities
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Guid jobAdId)
                => from n in dc.JobAdNoteEntities
                   where n.jobAdId == jobAdId
                   && n.ownerId == ownerId
                   orderby n.lastUpdatedTime descending
                   select n);

        private static readonly Func<JobAdsDataContext, Guid, Guid, bool> HasNotes
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Guid jobAdId)
                => (from n in dc.JobAdNoteEntities
                    where n.jobAdId == jobAdId
                    && n.ownerId == ownerId
                    select n).Any());

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<Guid>> GetJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId)
                => (from n in dc.JobAdNoteEntities
                    select n.jobAdId).Distinct());

        #endregion

        public JobAdNotesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IJobAdNotesRepository.CreateNote(JobAdNote note)
        {
            using (var dc = CreateContext())
            {
                dc.JobAdNoteEntities.InsertOnSubmit(note.Map());
                dc.SubmitChanges();
            }
        }

        void IJobAdNotesRepository.UpdateNote(JobAdNote note)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdNoteEntity(dc, note.Id);
                if (entity != null)
                {
                    note.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IJobAdNotesRepository.DeleteNote(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdNoteEntity(dc, id);
                if (entity != null)
                {
                    dc.JobAdNoteEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        T IJobAdNotesRepository.GetNote<T>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdNoteEntity(dc, id).MapTo<T>();
            }
        }

        IList<T> IJobAdNotesRepository.GetNotes<T>(Guid ownerId, Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetJobAdNoteEntities(dc, ownerId, jobAdId) select e.MapTo<T>()).ToList();
            }
        }

        bool IJobAdNotesRepository.HasNotes(Guid ownerId, Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return HasNotes(dc, ownerId, jobAdId);
            }
        }

        IList<Guid> IJobAdNotesRepository.GetHasNotesJobAdIds(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIds(dc, ownerId).ToList();
            }
        }

        private JobAdsDataContext CreateContext()
        {
            return CreateContext(c => new JobAdsDataContext(c));
        }
    }
}
