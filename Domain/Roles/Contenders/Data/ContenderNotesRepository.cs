using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Contenders.Data
{
    public class ContenderNotesRepository
        : Repository, IContenderNotesRepository
    {
        private static readonly Func<ContendersDataContext, Guid, CandidateNoteEntity> GetCandidateNoteEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid id)
                => (from n in dc.CandidateNoteEntities
                    where n.id == id
                    select n).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, Guid, Guid, IOrderedQueryable<CandidateNoteEntity>> GetCandidateNoteEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, Guid sharedWithId, Guid contenderId)
                => from n in dc.CandidateNoteEntities
                   where n.candidateId == contenderId
                   &&
                   (
                        (n.searcherId == ownerId && Equals(n.sharedWithId, null))
                        ||
                        (n.sharedWithId == sharedWithId)
                   )
                   orderby n.lastUpdatedTime descending
                   select n);

        private static readonly Func<ContendersDataContext, Guid, Guid, Guid, int> GetNoteCount
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, Guid sharedWithId, Guid contenderId)
                => (from n in dc.CandidateNoteEntities
                    where n.candidateId == contenderId
                    &&
                    (
                        (n.searcherId == ownerId && Equals(n.sharedWithId, null))
                        ||
                        (n.sharedWithId == sharedWithId)
                    )
                    select n).Count());

        private static readonly Func<ContendersDataContext, Guid, Guid, Guid, bool> HasNotes
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, Guid sharedWithId, Guid contenderId)
                => (from n in dc.CandidateNoteEntities
                    where n.candidateId == contenderId
                    &&
                    (
                        (n.searcherId == ownerId && Equals(n.sharedWithId, null))
                        ||
                        (n.sharedWithId == sharedWithId)
                    )
                    select n).Any());

        private static readonly Func<ContendersDataContext, Guid, Guid, IQueryable<Guid>> GetHasNotesContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, Guid sharedWithId)
                => (from n in dc.CandidateNoteEntities
                    where 
                    (
                        (n.searcherId == ownerId && Equals(n.sharedWithId, null))
                        ||
                        (n.sharedWithId == sharedWithId)
                    )
                    select n.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Guid, Guid, string, IQueryable<Tuple<Guid, int>>> GetNoteCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, Guid sharedWithId, string contenderIds)
                => from n in dc.CandidateNoteEntities
                   join c in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on n.candidateId equals c.value
                   where
                   (
                        (n.searcherId == ownerId && Equals(n.sharedWithId, null))
                        ||
                        (n.sharedWithId == sharedWithId)
                   )
                   group n by n.candidateId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        public ContenderNotesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IContenderNotesRepository.CreateNote(ContenderNote note)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateNoteEntities.InsertOnSubmit(note.Map());
                dc.SubmitChanges();
            }
        }

        void IContenderNotesRepository.UpdateNote(ContenderNote note)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCandidateNoteEntity(dc, note.Id);
                if (entity != null)
                {
                    note.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IContenderNotesRepository.DeleteNote(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCandidateNoteEntity(dc, id);
                if (entity != null)
                {
                    dc.CandidateNoteEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        T IContenderNotesRepository.GetNote<T>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCandidateNoteEntity(dc, id).MapTo<T>();
            }
        }

        IList<T> IContenderNotesRepository.GetNotes<T>(Guid ownerId, Guid sharedWithId, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetCandidateNoteEntities(dc, ownerId, sharedWithId, contenderId) select e.MapTo<T>()).ToList();
            }
        }

        bool IContenderNotesRepository.HasNotes(Guid ownerId, Guid sharedWithId, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return HasNotes(dc, ownerId, sharedWithId, contenderId);
            }
        }

        IList<Guid> IContenderNotesRepository.GetHasNotesContenderIds(Guid ownerId, Guid sharedWithId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetHasNotesContenderIds(dc, ownerId, sharedWithId).ToList();
            }
        }

        int IContenderNotesRepository.GetNoteCount(Guid ownerId, Guid sharedWithId, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetNoteCount(dc, ownerId, sharedWithId, contenderId);
            }
        }

        IDictionary<Guid, int> IContenderNotesRepository.GetNoteCounts(Guid ownerId, Guid sharedWithId, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetNoteCounts(dc, ownerId, sharedWithId, new SplitList<Guid>(contenderIds).ToString()).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        private ContendersDataContext CreateContext()
        {
            return CreateContext(c => new ContendersDataContext(c));
        }
    }
}