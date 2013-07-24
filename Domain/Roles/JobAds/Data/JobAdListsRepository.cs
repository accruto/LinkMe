using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.JobAds.Data
{
    public class JobAdListsRepository
        : Repository, IJobAdListsRepository
    {
        private struct Criteria
        {
            private readonly string _listTypes;
            private readonly string _notIfInListTypes;

            public Criteria(IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
            {
                _listTypes = new SplitList<int>(listTypes).ToString();
                _notIfInListTypes = notIfInListTypes == null ? null : new SplitList<int>(notIfInListTypes).ToString();
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

        private static readonly Func<JobAdsDataContext, Guid, Guid, bool> HasEntry
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId, Guid jobAdId)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    where e.jobAdListId == listId
                    && e.jobAdId == jobAdId
                    select e).Any());

        private static readonly Func<JobAdsDataContext, Guid, string, Guid, bool> HasNotTypeEntry
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId, string notIfInListTypes, Guid jobAdId)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    where e.jobAdListId == listId
                    && e.jobAdId == jobAdId
                    && !(from oe in dc.JobAdListEntryEntities
                         join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.jobAdId).Contains(e.jobAdId)
                    select e).Any());

        // IsListed(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid jobAdId)

        private static readonly Func<JobAdsDataContext, Guid, Criteria, Guid, bool> HasOwnerEntryEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria, Guid jobAdId)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.ownerId == ownerId
                    && e.jobAdId == jobAdId
                    select e).Any());

        private static readonly Func<JobAdsDataContext, Guid, Criteria, Guid, bool> HasOwnerNotTypeEntryEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria, Guid jobAdId)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.ownerId == ownerId
                    && !(from oe in dc.JobAdListEntryEntities
                         join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.jobAdId).Contains(e.jobAdId)
                    && e.jobAdId == jobAdId
                    select e).Any());

        // GetListedJobAdIds(Guid listId, IEnumerable<int> notIfInListTypes)

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<Guid>> GetJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    select e.jobAdId).Distinct());

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<Guid>> GetNotTypeJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId, string notIfInListTypes)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && !(from oe in dc.JobAdListEntryEntities
                         join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.jobAdId).Contains(e.jobAdId)
                    select e.jobAdId).Distinct());

        // GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<Guid>> GetOwnedJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, string listTypes)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.ownerId == ownerId
                    select e.jobAdId).Distinct());

        private static readonly Func<JobAdsDataContext, Guid, Criteria, IQueryable<Guid>> GetOwnedNotTypeJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.ownerId == ownerId
                    && !(from oe in dc.JobAdListEntryEntities
                         join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.jobAdId).Contains(e.jobAdId)
                    select e.jobAdId).Distinct());

        // GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes)

        private static readonly Func<JobAdsDataContext, Guid, int> GetCount
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    select e).Count());

        private static readonly Func<JobAdsDataContext, Guid, string, int> GetNotTypeCount
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId, string notIfInListTypes)
                => (from e in dc.JobAdListEntryEntities
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    where !l.isDeleted
                    && l.id == listId
                    && !(from oe in dc.JobAdListEntryEntities
                         join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                         join nt in dc.SplitInts(SplitList<int>.Delimiter, notIfInListTypes) on ol.listType equals nt.value
                         where ol.ownerId == l.ownerId
                         select oe.jobAdId).Contains(e.jobAdId)
                    select e).Count());

        // GetListedCounts(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)

        private static readonly Func<JobAdsDataContext, Guid, Criteria, IQueryable<Tuple<Guid, int>>> GetCounts
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   group e by e.jobAdListId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        private static readonly Func<JobAdsDataContext, Guid, Criteria, IQueryable<Tuple<Guid, int>>> GetNotTypeCounts
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && l.ownerId == ownerId
                   && !(from oe in dc.JobAdListEntryEntities
                        join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.jobAdId).Contains(e.jobAdId)
                   group e by e.jobAdListId into g
                   select new Tuple<Guid, int>(g.Key, g.Count()));

        // GetLastUsedTimes(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)

        private static readonly Func<JobAdsDataContext, Guid, Criteria, IQueryable<Tuple<Guid, DateTime?>>> GetLastUsedTimes
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   group e by e.jobAdListId into g
                   select new Tuple<Guid, DateTime?>(g.Key, g.Max(e => e.createdTime)));

        private static readonly Func<JobAdsDataContext, Guid, Criteria, IQueryable<Tuple<Guid, DateTime?>>> GetNotTypeLastUsedTimes
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                   where !l.isDeleted
                   && l.ownerId == ownerId
                   && !(from oe in dc.JobAdListEntryEntities
                        join ol in dc.JobAdListEntities on oe.jobAdListId equals ol.id
                        join nt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on ol.listType equals nt.value
                        where ol.ownerId == l.ownerId
                        select oe.jobAdId).Contains(e.jobAdId)
                   group e by e.jobAdListId into g
                   select new Tuple<Guid, DateTime?>(g.Key, g.Max(e => e.createdTime)));

        private static readonly Func<JobAdsDataContext, Guid, string, string, IQueryable<Guid>> GetFilteredJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, string listTypes, string jobAdIds)
                => (from e in dc.JobAdListEntryEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, jobAdIds) on e.jobAdId equals i.value
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.ownerId == ownerId
                    select e.jobAdId).Distinct());

        private static readonly Func<JobAdsDataContext, Guid, JobAdListEntity> GetListEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from l in dc.JobAdListEntities
                    where l.id == id
                    select l).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, string, string, JobAdListEntity> GetListEntityByName
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, string name, string listTypes)
                => (from l in dc.JobAdListEntities
                    join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                    where l.isDeleted == false
                          && l.ownerId == ownerId
                          && l.name == name
                    select l).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, int, IQueryable<JobAdListEntity>> GetListEntitiesByType
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, int listType)
                => from l in dc.JobAdListEntities
                   where l.isDeleted == false
                         && l.ownerId == ownerId
                         && l.listType == listType
                   select l);

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<JobAdListEntity>> GetListEntitiesByTypes
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, string listTypes)
                => from l in dc.JobAdListEntities
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   where l.isDeleted == false
                   && l.ownerId == ownerId
                   select l);

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<JobAdListEntryEntity>> GetJobAdListEntryEntities
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   where e.jobAdListId == listId
                   select e);

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<JobAdListEntryEntity>> GetAllJobAdListEntryEntitiesByType
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, string listTypes)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   where l.ownerId == ownerId
                   select e);

        private static readonly Func<JobAdsDataContext, Guid, Criteria, string, IQueryable<Guid>> GetFilteredNotTypeJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, Criteria criteria, string jobAdIds)
                => (from e in dc.JobAdListEntryEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, jobAdIds) on e.jobAdId equals i.value
                    join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                    join t in dc.SplitInts(SplitList<int>.Delimiter, criteria.ListTypes) on l.listType equals t.value
                    where !l.isDeleted
                    && l.ownerId == ownerId
                    && !(from b in dc.JobAdListEntryEntities
                         join bl in dc.JobAdListEntities on b.jobAdListId equals bl.id
                         join bt in dc.SplitInts(SplitList<int>.Delimiter, criteria.NotIfInListTypes) on bl.listType equals bt.value
                         where bl.ownerId == l.ownerId
                         select b.jobAdId).Contains(e.jobAdId)
                    select e.jobAdId).Distinct());

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<JobAdListEntryEntity>> GetFilteredJobAdListEntryEntities
             = CompiledQuery.Compile((JobAdsDataContext dc, Guid listId, string jobAdIds)
                => from e in dc.JobAdListEntryEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, jobAdIds) on e.jobAdId equals i.value
                   where e.jobAdListId == listId
                   select e);

        private static readonly Func<JobAdsDataContext, Guid, string, string, IQueryable<JobAdListEntryEntity>> GetFilteredTypedJobAdListEntryEntities
             = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, string listTypes, string jobAdIds)
                => from e in dc.JobAdListEntryEntities
                   join l in dc.JobAdListEntities on e.jobAdListId equals l.id
                   join t in dc.SplitInts(SplitList<int>.Delimiter, listTypes) on l.listType equals t.value
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, jobAdIds) on e.jobAdId equals i.value
                   where l.ownerId == ownerId
                   select e);

        public JobAdListsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        T IJobAdListsRepository.GetList<T>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetListEntity(dc, id).MapTo<T>();
            }
        }

        T IJobAdListsRepository.GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetListEntityByName(dc, ownerId, name, new SplitList<int>(listTypes).ToString()).MapTo<T>();
            }
        }

        IList<T> IJobAdListsRepository.GetLists<T>(Guid ownerId, int listType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetListEntitiesByType(dc, ownerId, listType) select e.MapTo<T>()).ToList();
            }
        }

        IList<T> IJobAdListsRepository.GetLists<T>(Guid ownerId, int[] listTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from e in GetListEntitiesByTypes(dc, ownerId, new SplitList<int>(listTypes).ToString()) select e.MapTo<T>()).ToList();
            }
        }

        bool IJobAdListsRepository.IsListed(Guid listId, IEnumerable<int> notIfInListTypes, Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? HasEntry(dc, listId, jobAdId)
                    : HasNotTypeEntry(dc, listId, new SplitList<int>(notIfInListTypesList).ToString(), jobAdId);
            }
        }

        bool IJobAdListsRepository.IsListed(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? HasOwnerEntryEntity(dc, ownerId, new Criteria(listTypes, null), jobAdId)
                    : HasOwnerNotTypeEntryEntity(dc, ownerId, new Criteria(listTypes, notIfInListTypesList), jobAdId);
            }
        }

        int IJobAdListsRepository.GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? GetCount(dc, listId)
                    : GetNotTypeCount(dc, listId, new SplitList<int>(notIfInListTypesList).ToString());
            }
        }

        IDictionary<Guid, int> IJobAdListsRepository.GetListedCounts(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? GetCounts(dc, ownerId, new Criteria(listTypes, null)).ToDictionary(t => t.Item1, t => t.Item2)
                    : GetNotTypeCounts(dc, ownerId, new Criteria(listTypes, notIfInListTypesList)).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        IDictionary<Guid, DateTime?> IJobAdListsRepository.GetLastUsedTimes(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? GetLastUsedTimes(dc, ownerId, new Criteria(listTypes, null)).ToDictionary(t => t.Item1, t => t.Item2)
                    : GetNotTypeLastUsedTimes(dc, ownerId, new Criteria(listTypes, notIfInListTypesList)).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        void IJobAdListsRepository.CreateEntries(Guid listId, DateTime time, IEnumerable<Guid> jobAdIds)
        {
            using (var dc = CreateContext())
            {
                if (jobAdIds != null)
                {
                    // Do not add for ones already there.

                    var jobAdIdsList = jobAdIds.ToList();
                    var entities = GetFilteredJobAdListEntryEntities(dc, listId, new SplitList<Guid>(jobAdIdsList).ToString());
                    dc.JobAdListEntryEntities.InsertAllOnSubmit(from i in jobAdIdsList.Except(from e in entities select e.jobAdId) select new JobAdListEntryEntity { jobAdId = i, jobAdListId = listId, createdTime = time });
                    dc.SubmitChanges();
                }
            }
        }

        IList<Guid> IJobAdListsRepository.GetListedJobAdIds(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? GetJobAdIds(dc, listId).ToList()
                    : GetNotTypeJobAdIds(dc, listId, new SplitList<int>(notIfInListTypesList).ToString()).ToList();
            }
        }

        IList<Guid> IJobAdListsRepository.GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? GetOwnedJobAdIds(dc, ownerId, new SplitList<int>(listTypes).ToString()).ToList()
                    : GetOwnedNotTypeJobAdIds(dc, ownerId, new Criteria(listTypes, notIfInListTypesList)).ToList();
            }
        }

        IList<Guid> IJobAdListsRepository.GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> jobAdIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var notIfInListTypesList = notIfInListTypes == null ? null : notIfInListTypes.ToList();
                return notIfInListTypesList.IsNullOrEmpty()
                    ? GetFilteredJobAdIds(dc, ownerId, new SplitList<int>(listTypes).ToString(), new SplitList<Guid>(jobAdIds).ToString()).ToList()
                    : GetFilteredNotTypeJobAdIds(dc, ownerId, new Criteria(listTypes, notIfInListTypesList), new SplitList<Guid>(jobAdIds).ToString()).ToList();
            }
        }

        void IJobAdListsRepository.DeleteEntries(Guid listId)
        {
            using (var dc = CreateContext())
            {
                var entities = GetJobAdListEntryEntities(dc, listId);
                dc.JobAdListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IJobAdListsRepository.DeleteEntries(Guid listId, IEnumerable<Guid> jobAdIds)
        {
            using (var dc = CreateContext())
            {
                var entities = GetFilteredJobAdListEntryEntities(dc, listId, new SplitList<Guid>(jobAdIds).ToString());
                dc.JobAdListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IJobAdListsRepository.DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> jobAdIds)
        {
            using (var dc = CreateContext())
            {
                var entities = GetFilteredTypedJobAdListEntryEntities(dc, ownerId, new SplitList<int>(listTypes).ToString(), new SplitList<Guid>(jobAdIds).ToString());
                dc.JobAdListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IJobAdListsRepository.DeleteEntries(Guid ownerId, IEnumerable<int> listTypes)
        {
            using (var dc = CreateContext())
            {
                var entities = GetAllJobAdListEntryEntitiesByType(dc, ownerId, new SplitList<int>(listTypes).ToString());
                dc.JobAdListEntryEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();
            }
        }

        void IJobAdListsRepository.CreateList(JobAdList list)
        {
            using (var dc = CreateContext())
            {
                dc.JobAdListEntities.InsertOnSubmit(list.Map());
                dc.SubmitChanges();
            }
        }

        void IJobAdListsRepository.UpdateList(JobAdList list)
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

        void IJobAdListsRepository.DeleteList(Guid id)
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

        private JobAdsDataContext CreateContext()
        {
            return CreateContext(c => new JobAdsDataContext(c));
        }
    }
}
