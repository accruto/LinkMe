using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public class JobAdListsQuery
        : IJobAdListsQuery
    {
        private readonly IJobAdListsRepository _repository;

        public JobAdListsQuery(IJobAdListsRepository repository)
        {
            _repository = repository;
        }

        T IJobAdListsQuery.GetList<T>(Guid id)
        {
            return _repository.GetList<T>(id);
        }

        T IJobAdListsQuery.GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes)
        {
            return _repository.GetList<T>(ownerId, name, listTypes);
        }

        IList<T> IJobAdListsQuery.GetLists<T>(Guid ownerId, int listType)
        {
            return _repository.GetLists<T>(ownerId, listType);
        }

        IList<T> IJobAdListsQuery.GetLists<T>(Guid ownerId, int[] listTypes)
        {
            return _repository.GetLists<T>(ownerId, listTypes);
        }

        bool IJobAdListsQuery.IsListed(Guid listId, IEnumerable<int> notIfInListTypes, Guid jobAdId)
        {
            return _repository.IsListed(listId, notIfInListTypes, jobAdId);
        }

        bool IJobAdListsQuery.IsListed(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid jobAdId)
        {
            return _repository.IsListed(ownerId, listTypes, notIfInListTypes, jobAdId);
        }

        IList<Guid> IJobAdListsQuery.GetListedJobAdIds(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            return _repository.GetListedJobAdIds(listId, notIfInListTypes);
        }

        IList<Guid> IJobAdListsQuery.GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            return _repository.GetListedJobAdIds(ownerId, listTypes, notIfInListTypes);
        }

        IList<Guid> IJobAdListsQuery.GetListedJobAdIds(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> jobAdIds)
        {
            return _repository.GetListedJobAdIds(ownerId, listTypes, notIfInListTypes, jobAdIds);
        }

        int IJobAdListsQuery.GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes)
        {
            return _repository.GetListedCount(listId, notIfInListTypes);
        }

        IDictionary<Guid, int> IJobAdListsQuery.GetListedCounts(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            return _repository.GetListedCounts(ownerId, listTypes, notIfInListTypes);
        }

        IDictionary<Guid, DateTime?> IJobAdListsQuery.GetLastUsedTimes(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes)
        {
            return _repository.GetLastUsedTimes(ownerId, listTypes, notIfInListTypes);
        }
    }
}
