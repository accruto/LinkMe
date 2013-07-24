using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds
{
    public interface IJobAdListsRepository
    {
        void CreateList(JobAdList list);
        void UpdateList(JobAdList list);
        void DeleteList(Guid id);

        T GetList<T>(Guid id) where T : JobAdList, new();
        T GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes) where T : JobAdList, new();
        IList<T> GetLists<T>(Guid ownerId, int listType) where T : JobAdList, new();
        IList<T> GetLists<T>(Guid ownerId, int[] listTypes) where T : JobAdList, new();

        void CreateEntries(Guid listId, DateTime time, IEnumerable<Guid> jobAdIds);
        void DeleteEntries(Guid listId);
        void DeleteEntries(Guid ownerId, IEnumerable<int> listTypes);
        void DeleteEntries(Guid listId, IEnumerable<Guid> jobAdIds);
        void DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> jobAdIds);

        bool IsListed(Guid listId, IEnumerable<int> notIfInListTypes, Guid jobAdId);
        bool IsListed(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid jobAdId);

        int GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes);
        IDictionary<Guid, int> GetListedCounts(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes);
        IDictionary<Guid, DateTime?> GetLastUsedTimes(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes);

        IList<Guid> GetListedJobAdIds(Guid listId, IEnumerable<int> notIfInListTypes);
        IList<Guid> GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes);
        IList<Guid> GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> jobAdIds);
    }
}
