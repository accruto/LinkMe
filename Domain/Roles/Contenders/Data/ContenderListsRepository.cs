using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Contenders.Data
{
    public class ContenderListsRepository
        : Repository, IContenderListsRepository
    {
        private class Criteria
        {
            private readonly Guid _ownerId;
            private readonly Guid _sharedWithId;
            private readonly string _listTypes;
            private readonly string _notIfInListTypes;

            public Criteria(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
            {
                _ownerId = ownerId;
                _sharedWithId = sharedWithId ?? Guid.Empty;
                _listTypes = listTypes == null ? null : new SplitList<int>(listTypes).ToString();
                _notIfInListTypes = notIfInListTypes == null ? null : new SplitList<int>(notIfInListTypes).ToString();
            }

            public Guid OwnerId
            {
                get { return _ownerId; }
            }

            public Guid SharedWithId
            {
                get { return _sharedWithId; }
            }

            public string ListTypes
            {
                get { return _listTypes; }
            }

            public string NotIfInListTypes
            {
                get { return _notIfInListTypes; }
            }
        }

        // GetList<T>(Guid id)

        private static readonly Func<ContendersDataContext, Guid, CandidateListEntity> GetListEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid id)
                => (from l in dc.CandidateListEntities
                    where l.id == id
                    select l).SingleOrDefault());

        // GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes)

        private static readonly Func<ContendersDataContext, Guid, string, string, CandidateListEntity> GetOwnerListEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, string name, string listTypes)
                => (from l in dc.CandidateListEntities
                    join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == ownerId && Equals(l.sharedWithId, null))
                    && l.name == name
                    select l).SingleOrDefault());

        // GetSharedList<T>(Guid sharedWithId, string name, IEnumerable<int> listTypes)

        private static readonly Func<ContendersDataContext, Guid, string, string, CandidateListEntity> GetSharedListEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid sharedWithId, string name, string listTypes)
                => (from l in dc.CandidateListEntities
                    join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.sharedWithId == sharedWithId
                    && l.name == name
                    select l).SingleOrDefault());

        // GetLists<T>(Guid ownerId, int listType)

        private static readonly Func<ContendersDataContext, Guid, int, IQueryable<CandidateListEntity>> GetListEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, int listType)
                => from l in dc.CandidateListEntities
                   where !l.isDeleted
                   && l.ownerId == ownerId
                   && l.listType == listType
                   select l);

        // GetLists<T>(Guid ownerId, IEnumerable<int> listTypes)

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<CandidateListEntity>> GetTypesListEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, string listTypes)
                => from l in dc.CandidateListEntities
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && l.ownerId == ownerId
                   orderby l.name
                   select l);

        // GetLists<T>(Guid ownerId, Guid sharedWithId, IEnumerable<int> listTypes)

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<CandidateListEntity>> GetAllListEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from l in dc.CandidateListEntities
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   select l);

        // GetSharedLists<T>(Guid sharedWithId, int listType)

        private static readonly Func<ContendersDataContext, Guid, int, IQueryable<CandidateListEntity>> GetSharedListEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid sharedWithId, int listType)
                => from l in dc.CandidateListEntities
                   where !l.isDeleted
                   && l.sharedWithId == sharedWithId
                   && l.listType == listType
                   select l);

        // GetEntries<T>(Guid listId, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, Guid, IQueryable<CandidateListEntryEntity>> GetEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   where !l.isDeleted
                   && l.id == listId
                   select e);

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<CandidateListEntryEntity>> GetNotTypeEntryEntries
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string notIfInListTypes)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   where !l.isDeleted
                   && l.id == listId
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   select e);

        // GetEntries<T>(Guid listId, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds)

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<CandidateListEntryEntity>> GetFilteredEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                   where !l.isDeleted
                   && l.id == listId
                   select e);

        private static readonly Func<ContendersDataContext, Guid, string, string, IQueryable<CandidateListEntryEntity>> GetNotTypeFilteredEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string notIfInListTypes, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                   where !l.isDeleted
                   && l.id == listId
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   select e);

        // GetEntries<T>(IEnumerable<Guid> listIds, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, string, IQueryable<CandidateListEntryEntity>> GetAllEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, string listIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, listIds) on e.candidateListId equals i.value
                   where !l.isDeleted
                   select e);

        private static readonly Func<ContendersDataContext, string, string, IQueryable<CandidateListEntryEntity>> GetAllNotTypeEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, string listIds, string notIfInListTypes)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, listIds) on e.candidateListId equals i.value
                   where !l.isDeleted
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   select e);

        // IsListed(Guid listId, IEnumerable<int> notIfInListTypes, Guid contenderId)

        private static readonly Func<ContendersDataContext, Guid, Guid, bool> HasEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && e.candidateId == contenderId
                    select e).Any());

        private static readonly Func<ContendersDataContext, Guid, string, Guid, bool> HasNotTypeEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string notIfInListTypes, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    && e.candidateId == contenderId
                    select e).Any());

        // IsListed(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid contenderId)

        private static readonly Func<ContendersDataContext, Criteria, Guid, bool> HasOwnerEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    && e.candidateId == contenderId
                    select e).Any());

        private static readonly Func<ContendersDataContext, Criteria, Guid, bool> HasOwnerNotTypeEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    && e.candidateId == contenderId
                    select e).Any());

        private static readonly Func<ContendersDataContext, Criteria, Guid, bool> HasSharedEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    && e.candidateId == contenderId
                    select e).Any());

        private static readonly Func<ContendersDataContext, Criteria, Guid, bool> HasSharedNotTypeEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    && e.candidateId == contenderId
                    select e).Any());

        // GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds)

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Guid>> GetFilteredOwnerContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Guid>> GetFilteredOwnerNotTypeContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Guid>> GetFilteredSharedContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Guid>> GetFilteredSharedNotTypeContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    select e.candidateId).Distinct());

        // GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, Guid, int> GetCount
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && e.candidateListId == listId
                    select e).Count());

        private static readonly Func<ContendersDataContext, Guid, string, int> GetNotTypeCount
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string notIfInListTypes)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && e.candidateListId == listId
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    select e).Count());

        // GetListedCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, int>>> GetCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                   group e by e.candidateListId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, int>>> GetNotTypeCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   group e by e.candidateListId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, int>>> GetSharedCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   group e by e.candidateListId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, int>>> GetSharedNotTypeCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   group e by e.candidateListId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        // GetLastUsedTimes(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, DateTime?>>> GetLastUsedTimes
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                   group e by e.candidateListId into g
                   select new Tuple<Guid, DateTime?>(g.Key, g.Max(e => e.createdTime)));

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, DateTime?>>> GetNotTypeLastUsedTimes
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   group e by e.candidateListId into g
                   select new Tuple<Guid, DateTime?>(g.Key, g.Max(e => e.createdTime)));

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, DateTime?>>> GetSharedLastUsedTimes
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   group e by e.candidateListId into g
                   select new Tuple<Guid, DateTime?>(g.Key, g.Max(e => e.createdTime)));

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Tuple<Guid, DateTime?>>> GetSharedNotTypeLastUsedTimes
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   group e by e.candidateListId into g
                   select new Tuple<Guid, DateTime?>(g.Key, g.Max(e => e.createdTime)));

        // GetListedContenderIds(Guid listId, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, Guid, IQueryable<Guid>> GetContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<Guid>> GetNotTypeContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string notIfInListTypes)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    select e.candidateId).Distinct());

        // GetListedContenderIds(Guid listId, IEnumerable<int> notIfInListTypes, ApplicantStatus status)

        private static readonly Func<ContendersDataContext, Guid, ApplicantStatus, IQueryable<Guid>> GetStatusContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, ApplicantStatus status)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && Equals(e.jobApplicationStatus, status)
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Guid, string, ApplicantStatus, IQueryable<Guid>> GetNotTypeStatusContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, string notIfInListTypes, ApplicantStatus status)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    && Equals(e.jobApplicationStatus, status)
                    select e.candidateId).Distinct());

        // GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Guid>> GetOwnedContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Guid>> GetOwnedNotTypeContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                   select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Guid>> GetSharedContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Criteria, IQueryable<Guid>> GetSharedNotTypeContenderIds
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    select e.candidateId).Distinct());

        // GetListCount(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid contenderId)

        private static readonly Func<ContendersDataContext, Criteria, Guid, int> GetOwnerEntryCount
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid candidateId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    && e.candidateId == candidateId
                    select e).Count());

        private static readonly Func<ContendersDataContext, Criteria, Guid, int> GetOwnerNotTypeEntryCount
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid candidateId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    && e.candidateId == candidateId
                    select e).Count());

        private static readonly Func<ContendersDataContext, Criteria, Guid, int> GetSharedEntryCount
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid candidateId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on
                    l.listType equals t.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    && e.candidateId == candidateId
                    select e).Count());

        private static readonly Func<ContendersDataContext, Criteria, Guid, int> GetSharedNotTypeEntryCount
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, Guid candidateId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on
                    l.listType equals t.value
                    where !l.isDeleted
                    && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                    && !(from oe in dc.CandidateListEntryEntities
                         join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.candidateId).Contains(e.candidateId)
                    && e.candidateId == candidateId
                    select e).Count());

        // GetListCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds)

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Tuple<Guid, int>>> GetFilteredOwnerEntryCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   join c in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals c.value
                   where !l.isDeleted
                   && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                   group e by e.candidateId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Tuple<Guid, int>>> GetFilteredOwnerNotTypeEntryCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   join c in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals c.value
                   where !l.isDeleted
                   && (l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null))
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   group e by e.candidateId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Tuple<Guid, int>>> GetFilteredSharedEntryCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   join c in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals c.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   group e by e.candidateId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Criteria, string, IQueryable<Tuple<Guid, int>>> GetFilteredSharedNotTypeEntryCounts
            = CompiledQuery.Compile((ContendersDataContext dc, Criteria criteria, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   join c in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals c.value
                   where !l.isDeleted
                   && ((l.ownerId == criteria.OwnerId && Equals(l.sharedWithId, null)) || l.sharedWithId == criteria.SharedWithId)
                   && !(from oe in dc.CandidateListEntryEntities
                        join ol in dc.CandidateListEntities on oe.candidateListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.candidateId).Contains(e.candidateId)
                   group e by e.candidateId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<ContendersDataContext, Guid, Guid, bool> IsOwnerApplicant
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where l.ownerId == ownerId
                    && e.candidateId == contenderId
                    && !Equals(e.jobApplicationId, null)
                    select e).Any());

        private static readonly Func<ContendersDataContext, Guid, Guid, bool> IsApplicant
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where l.id == listId
                    && e.candidateId == contenderId
                    && !Equals(e.jobApplicationId, null)
                    select e).Any());

        private static readonly Func<ContendersDataContext, string, Guid, IQueryable<Guid>> GetApplicantListIds
            = CompiledQuery.Compile((ContendersDataContext dc, string listTypes, Guid contenderId)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   where e.candidateId == contenderId
                   && !Equals(e.jobApplicationId, null)
                   select l.id);

        private static readonly Func<ContendersDataContext, string, string, Guid, IQueryable<Guid>> GetFilteredApplicantListIds
            = CompiledQuery.Compile((ContendersDataContext dc, string listIds, string listTypes, Guid contenderId)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, listIds) on l.id equals i.value
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   where e.candidateId == contenderId
                   && !Equals(e.jobApplicationId, null)
                   select l.id);

        private static readonly Func<ContendersDataContext, Guid, IQueryable<Guid>> GetOwnerApplicants
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where l.ownerId == ownerId
                    && !Equals(e.jobApplicationId, null)
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<Guid>> GetFilteredOwnerApplicants
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, string ids)
                => (from e in dc.CandidateListEntryEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on e.candidateId equals i.value
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where l.ownerId == ownerId
                    && !Equals(e.jobApplicationId, null)
                    select e.candidateId).Distinct());

        private static readonly Func<ContendersDataContext, Guid, string, string, IQueryable<CandidateListEntryEntity>> GetFilteredTypeEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, string listTypes, string contenderIds)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, contenderIds) on e.candidateId equals i.value
                   where (l.ownerId == ownerId && Equals(l.sharedWithId, null))
                   select e);

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<CandidateListEntryEntity>> GetAllTypeEntryEntities
            = CompiledQuery.Compile((ContendersDataContext dc, Guid ownerId, string listTypes)
                => from e in dc.CandidateListEntryEntities
                   join l in dc.CandidateListEntities on e.candidateListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   where (l.ownerId == ownerId && Equals(l.sharedWithId, null))
                   select e);

        private static readonly Func<ContendersDataContext, Guid, Guid, CandidateListEntryEntity> GetEntryEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid listId, Guid contenderId)
                => (from e in dc.CandidateListEntryEntities
                    join l in dc.CandidateListEntities on e.candidateListId equals l.id
                    where l.id == listId
                    && e.candidateId == contenderId
                    select e).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, ApplicantStatus?> GetApplicantStatus
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicationId)
                => (from e in dc.CandidateListEntryEntities
                    where e.jobApplicationId == applicationId
                    select (ApplicantStatus?)e.jobApplicationStatus).SingleOrDefault());

        public ContenderListsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IContenderListsRepository.CreateList(ContenderList list)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateListEntities.InsertOnSubmit(list.Map());
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.UpdateList(ContenderList list)
        {
            using (var dc = CreateContext())
            {
                var entity = GetListEntity(dc, list.Id);
                if (entity != null)
                {
                    list.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IContenderListsRepository.DeleteList(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetListEntity(dc, id);
                if (entity != null)
                {
                    entity.isDeleted = true;
                    dc.SubmitChanges();
                }
            }
        }

        T IContenderListsRepository.GetList<T>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetListEntity(dc, id).MapTo<T>();
            }
        }

        T IContenderListsRepository.GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOwnerListEntity(dc, ownerId, name, new SplitList<int>(listTypes).ToString()).MapTo<T>();
            }
        }

        T IContenderListsRepository.GetSharedList<T>(Guid sharedWithId, string name, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetSharedListEntity(dc, sharedWithId, name, new SplitList<int>(listTypes).ToString()).MapTo<T>();
            }
        }

        IList<T> IContenderListsRepository.GetLists<T>(Guid ownerId, int listType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetListEntities(dc, ownerId, listType) select e.MapTo<T>()).ToList();
            }
        }


        IList<T> IContenderListsRepository.GetLists<T>(Guid ownerId, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetTypesListEntities(dc, ownerId, new SplitList<int>(listTypes).ToString()) select e.MapTo<T>()).ToList();
            }
        }

        IList<T> IContenderListsRepository.GetLists<T>(Guid ownerId, Guid sharedWithId, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetAllListEntities(dc, new Criteria(ownerId, sharedWithId, listTypes, null)) select e.MapTo<T>()).ToList();
            }
        }

        IList<T> IContenderListsRepository.GetSharedLists<T>(Guid sharedWithId, int listType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetSharedListEntities(dc, sharedWithId, listType) select e.MapTo<T>()).ToList();
            }
        }

        void IContenderListsRepository.CreateEntry(Guid listId, ContenderListEntry entry)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateListEntryEntities.InsertOnSubmit(entry.Map(listId));
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.CreateEntries(Guid listId, DateTime time, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext())
            {
                // Do not add for ones already there.

                var entities = GetFilteredEntryEntities(dc, listId, new SplitList<Guid>(contenderIds).ToString());
                dc.CandidateListEntryEntities.InsertAllOnSubmit(
                    from i in contenderIds.Except(from e in entities select e.candidateId)
                    select new CandidateListEntryEntity { candidateId = i, candidateListId = listId, createdTime = time });
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.CreateEntries(Guid listId, DateTime time, IEnumerable<Guid> contenderIds, ApplicantStatus status)
        {
            using (var dc = CreateContext())
            {
                // Do not add for ones already there.

                var entities = GetFilteredEntryEntities(dc, listId, new SplitList<Guid>(contenderIds).ToString());
                dc.CandidateListEntryEntities.InsertAllOnSubmit(
                    from i in contenderIds.Except(from e in entities select e.candidateId)
                    select new CandidateListEntryEntity { candidateId = i, candidateListId = listId, createdTime = time, jobApplicationStatus = (byte)status });
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.DeleteEntry(Guid listId, Guid contenderId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEntryEntity(dc, listId, contenderId);
                if (entity != null)
                {
                    dc.CandidateListEntryEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IContenderListsRepository.DeleteEntries(Guid listId, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext())
            {
                var entities = GetFilteredEntryEntities(dc, listId, new SplitList<Guid>(contenderIds).ToString());
                dc.CandidateListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext())
            {
                var entities = GetFilteredTypeEntryEntities(dc, ownerId, new SplitList<int>(listTypes).ToString(), new SplitList<Guid>(contenderIds).ToString());
                dc.CandidateListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.DeleteAllEntries(Guid listId)
        {
            using (var dc = CreateContext())
            {
                var entities = GetEntryEntities(dc, listId);
                dc.CandidateListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.DeleteAllEntries(Guid ownerId, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext())
            {
                var entities = GetAllTypeEntryEntities(dc, ownerId, new SplitList<int>(listTypes).ToString());
                dc.CandidateListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        IList<T> IContenderListsRepository.GetEntries<T>(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return notIfInListTypes.IsNullOrEmpty()
                    ? (from e in GetEntryEntities(dc, listId) select e.MapTo<T>()).ToList()
                    : (from e in GetNotTypeEntryEntries(dc, listId, new SplitList<int>(notIfInListTypes).ToString()) select e.MapTo<T>()).ToList();

            }
        }

        IList<T> IContenderListsRepository.GetEntries<T>(Guid listId, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return notIfInListTypes.IsNullOrEmpty()
                    ? (from e in GetFilteredEntryEntities(dc, listId, new SplitList<Guid>(contenderIds).ToString()) select e.MapTo<T>()).ToList()
                    : (from e in GetNotTypeFilteredEntryEntities(dc, listId, new SplitList<int>(notIfInListTypes).ToString(), new SplitList<Guid>(contenderIds).ToString()) select e.MapTo<T>()).ToList();
            }
        }

        IDictionary<Guid, IList<T>> IContenderListsRepository.GetEntries<T>(IEnumerable<Guid> listIds, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var entries = notIfInListTypes.IsNullOrEmpty()
                    ? (from e in GetAllEntryEntities(dc, new SplitList<Guid>(listIds).ToString())
                       group e by e.candidateListId into lists
                       select new
                       {
                           ListId = lists.Key,
                           Entries = (from l in lists select l.MapTo<T>()).ToList()
                       }).ToDictionary(l => l.ListId, l => l.Entries)
                    : (from e in GetAllNotTypeEntryEntities(dc, new SplitList<Guid>(listIds).ToString(), new SplitList<int>(notIfInListTypes).ToString())
                       group e by e.candidateListId into lists
                       select new
                       {
                           ListId = lists.Key,
                           Entries = (from l in lists select l.MapTo<T>()).ToList()
                       }).ToDictionary(l => l.ListId, l => l.Entries);

                // Make sure there is a list for each listId.

                return listIds.ToDictionary(
                    i => i,
                    i => entries.ContainsKey(i) ? (IList<T>)entries[i] : new List<T>());
            }
        }

        bool IContenderListsRepository.IsListed(Guid listId, IEnumerable<int> notIfInListTypes, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return notIfInListTypes.IsNullOrEmpty()
                    ? HasEntryEntity(dc, listId, contenderId)
                    : HasNotTypeEntryEntity(dc, listId, new SplitList<int>(notIfInListTypes).ToString(), contenderId);
            }
        }

        bool IContenderListsRepository.IsListed(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? HasOwnerEntryEntity(dc, new Criteria(ownerId, null, listTypes, null), contenderId)
                        : HasOwnerNotTypeEntryEntity(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes), contenderId);
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? HasSharedEntryEntity(dc, new Criteria(ownerId, sharedWithId, listTypes, null), contenderId)
                    : HasSharedNotTypeEntryEntity(dc, new Criteria(ownerId, sharedWithId, listTypes, notIfInListTypes), contenderId);
            }
        }

        IList<Guid> IContenderListsRepository.GetListedContenderIds(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return notIfInListTypes.IsNullOrEmpty()
                    ? GetContenderIds(dc, listId).ToList()
                    : GetNotTypeContenderIds(dc, listId, new SplitList<int>(notIfInListTypes).ToString()).ToList();
            }
        }

        IList<Guid> IContenderListsRepository.GetListedContenderIds(Guid listId, IEnumerable<int> notIfInListTypes, ApplicantStatus status)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return notIfInListTypes.IsNullOrEmpty()
                    ? GetStatusContenderIds(dc, listId, status).ToList()
                    : GetNotTypeStatusContenderIds(dc, listId, new SplitList<int>(notIfInListTypes).ToString(), status).ToList();
            }
        }

        IList<Guid> IContenderListsRepository.GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? GetOwnedContenderIds(dc, new Criteria(ownerId, null, listTypes, null)).ToList()
                        : GetOwnedNotTypeContenderIds(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes)).ToList();
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? GetSharedContenderIds(dc, new Criteria(ownerId, sharedWithId, listTypes, null)).ToList()
                    : GetSharedNotTypeContenderIds(dc, new Criteria(ownerId, sharedWithId, listTypes, notIfInListTypes)).ToList();
            }
        }

        IList<Guid> IContenderListsRepository.GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? GetFilteredOwnerContenderIds(dc, new Criteria(ownerId, null, listTypes, null), new SplitList<Guid>(contenderIds).ToString()).ToList()
                        : GetFilteredOwnerNotTypeContenderIds(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes), new SplitList<Guid>(contenderIds).ToString()).ToList();
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? GetFilteredSharedContenderIds(dc, new Criteria(ownerId, sharedWithId, listTypes, null), new SplitList<Guid>(contenderIds).ToString()).ToList()
                    : GetFilteredSharedNotTypeContenderIds(dc, new Criteria(ownerId, sharedWithId, listTypes, null), new SplitList<Guid>(contenderIds).ToString()).ToList();
            }
        }

        int IContenderListsRepository.GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return notIfInListTypes.IsNullOrEmpty()
                    ? GetCount(dc, listId)
                    : GetNotTypeCount(dc, listId, new SplitList<int>(notIfInListTypes).ToString());
            }
        }

        IDictionary<Guid, int> IContenderListsRepository.GetListedCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? GetCounts(dc, new Criteria(ownerId, null, listTypes, null)).ToDictionary(t => t.Item1, t => t.Item2)
                        : GetNotTypeCounts(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes)).ToDictionary(t => t.Item1, t => t.Item2);
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? GetSharedCounts(dc, new Criteria(ownerId, sharedWithId, listTypes, null)).ToDictionary(t => t.Item1, t => t.Item2)
                    : GetSharedNotTypeCounts(dc, new Criteria(ownerId, sharedWithId, listTypes, notIfInListTypes)).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        IDictionary<Guid, DateTime?> IContenderListsRepository.GetLastUsedTimes(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? GetLastUsedTimes(dc, new Criteria(ownerId, null, listTypes, null)).ToDictionary(t => t.Item1, t => t.Item2)
                        : GetNotTypeLastUsedTimes(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes)).ToDictionary(t => t.Item1, t => t.Item2);
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? GetSharedLastUsedTimes(dc, new Criteria(ownerId, sharedWithId, listTypes, null)).ToDictionary(t => t.Item1, t => t.Item2)
                    : GetSharedNotTypeLastUsedTimes(dc, new Criteria(ownerId, sharedWithId, listTypes, notIfInListTypes)).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        int IContenderListsRepository.GetListCount(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? GetOwnerEntryCount(dc, new Criteria(ownerId, null, listTypes, null), contenderId)
                        : GetOwnerNotTypeEntryCount(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes), contenderId);
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? GetSharedEntryCount(dc, new Criteria(ownerId, sharedWithId, listTypes, null), contenderId)
                    : GetSharedNotTypeEntryCount(dc, new Criteria(ownerId, sharedWithId, listTypes, notIfInListTypes), contenderId);
            }
        }

        IDictionary<Guid, int> IContenderListsRepository.GetListCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (sharedWithId == null)
                {
                    return notIfInListTypes.IsNullOrEmpty()
                        ? GetFilteredOwnerEntryCounts(dc, new Criteria(ownerId, null, listTypes, null), new SplitList<Guid>(contenderIds).ToString()).ToDictionary(t => t.Item1, t => t.Item2)
                        : GetFilteredOwnerNotTypeEntryCounts(dc, new Criteria(ownerId, null, listTypes, notIfInListTypes), new SplitList<Guid>(contenderIds).ToString()).ToDictionary(t => t.Item1, t => t.Item2);
                }

                return notIfInListTypes.IsNullOrEmpty()
                    ? GetFilteredSharedEntryCounts(dc, new Criteria(ownerId, sharedWithId, listTypes, null), new SplitList<Guid>(contenderIds).ToString()).ToDictionary(t => t.Item1, t => t.Item2)
                    : GetFilteredSharedNotTypeEntryCounts(dc, new Criteria(ownerId, sharedWithId, listTypes, notIfInListTypes), new SplitList<Guid>(contenderIds).ToString()).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        IList<Guid> IContenderListsRepository.GetOwnerApplicants(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOwnerApplicants(dc, ownerId).ToList();
            }
        }

        IList<Guid> IContenderListsRepository.GetOwnerApplicants(Guid ownerId, IEnumerable<Guid> contenderIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredOwnerApplicants(dc, ownerId, new SplitList<Guid>(contenderIds).ToString()).ToList();
            }
        }

        bool IContenderListsRepository.IsOwnerApplicant(Guid ownerId, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return IsOwnerApplicant(dc, ownerId, contenderId);
            }
        }

        bool IContenderListsRepository.IsApplicant(Guid listId, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return IsApplicant(dc, listId, contenderId);
            }
        }

        IList<Guid> IContenderListsRepository.GetApplicantListIds(IEnumerable<int> listTypes, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetApplicantListIds(dc, new SplitList<int>(listTypes).ToString(), contenderId).ToList();
            }
        }

        IList<Guid> IContenderListsRepository.GetApplicantListIds(IEnumerable<Guid> listIds, IEnumerable<int> listTypes, Guid contenderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredApplicantListIds(dc, new SplitList<Guid>(listIds).ToString(), new SplitList<int>(listTypes).ToString(), contenderId).ToList();
            }
        }

        void IContenderListsRepository.ChangeStatus(Guid listId, Guid applicantId, ApplicantStatus status)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEntryEntity(dc, listId, applicantId);
                if (entity != null)
                {
                    entity.jobApplicationStatus = (byte)status;
                    dc.SubmitChanges();
                }
            }
        }

        void IContenderListsRepository.ChangeStatus(Guid listId, IEnumerable<Guid> applicantIds, ApplicantStatus status)
        {
            using (var dc = CreateContext())
            {
                var entities = GetFilteredEntryEntities(dc, listId, new SplitList<Guid>(applicantIds).ToString());
                foreach (var entity in entities)
                    entity.jobApplicationStatus = (byte)status;
                dc.SubmitChanges();
            }
        }

        void IContenderListsRepository.UpdateApplication(Guid listId, Guid applicantId, Guid applicationId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEntryEntity(dc, listId, applicantId);
                if (entity != null)
                {
                    entity.jobApplicationId = applicationId;
                    dc.SubmitChanges();
                }
            }
        }

        ApplicantStatus IContenderListsRepository.GetApplicantStatus(Guid applicationId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetApplicantStatus(dc, applicationId) ?? ApplicantStatus.NotSubmitted;
            }
        }

        private ContendersDataContext CreateContext()
        {
            return CreateContext(c => new ContendersDataContext(c));
        }
    }
}