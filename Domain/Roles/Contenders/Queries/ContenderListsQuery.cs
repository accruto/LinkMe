using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public class ContenderListsQuery
        : IContenderListsQuery
    {
        private readonly IContenderListsRepository _repository;

        public ContenderListsQuery(IContenderListsRepository repository)
        {
            _repository = repository;
        }

        T IContenderListsQuery.GetList<T>(Guid id)
        {
            return _repository.GetList<T>(id);
        }

        T IContenderListsQuery.GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes)
        {
            return _repository.GetList<T>(ownerId, name, listTypes);
        }

        T IContenderListsQuery.GetSharedList<T>(Guid sharedWithId, string name, IEnumerable<int> listTypes)
        {
            return _repository.GetSharedList<T>(sharedWithId, name, listTypes);
        }

        IList<T> IContenderListsQuery.GetLists<T>(Guid ownerId, int listType)
        {
            return _repository.GetLists<T>(ownerId, listType);
        }

        IList<T> IContenderListsQuery.GetLists<T>(Guid ownerId, IEnumerable<int> listTypes)
        {
            return _repository.GetLists<T>(ownerId, listTypes);
        }

        IList<T> IContenderListsQuery.GetLists<T>(Guid ownerId, Guid sharedWithId, IEnumerable<int> listTypes) 
        {
            return _repository.GetLists<T>(ownerId, sharedWithId, listTypes);
        }

        IList<T> IContenderListsQuery.GetSharedLists<T>(Guid sharedWithId, int listType)
        {
            return _repository.GetSharedLists<T>(sharedWithId, listType);
        }

        IList<T> IContenderListsQuery.GetEntries<T>(Guid listId, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetEntries<T>(listId, notIfAlsoInListTypes);
        }

        IList<T> IContenderListsQuery.GetEntries<T>(Guid listId, IEnumerable<int> notIfAlsoInListTypes, IEnumerable<Guid> contenderIds)
        {
            return _repository.GetEntries<T>(listId, notIfAlsoInListTypes, contenderIds);
        }

        IDictionary<Guid, IList<T>> IContenderListsQuery.GetEntries<T>(IEnumerable<Guid> listIds, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetEntries<T>(listIds, notIfAlsoInListTypes);
        }

        bool IContenderListsQuery.IsListed(Guid listId, IEnumerable<int> notIfAlsoInListTypes, Guid contenderId)
        {
            return _repository.IsListed(listId, notIfAlsoInListTypes, contenderId);
        }

        bool IContenderListsQuery.IsListed(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, Guid contenderId)
        {
            return _repository.IsListed(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes, contenderId);
        }

        IList<Guid> IContenderListsQuery.GetListedContenderIds(Guid listId, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetListedContenderIds(listId, notIfAlsoInListTypes);
        }

        IList<Guid> IContenderListsQuery.GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetListedContenderIds(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes);
        }

        IList<Guid> IContenderListsQuery.GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, IEnumerable<Guid> contenderIds)
        {
            return _repository.GetListedContenderIds(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes, contenderIds);
        }

        IList<Guid> IContenderListsQuery.GetListedContenderIds(Guid listId, IEnumerable<int> notIfAlsoInListTypes, ApplicantStatus status)
        {
            return _repository.GetListedContenderIds(listId, notIfAlsoInListTypes, status);
        }

        int IContenderListsQuery.GetListedCount(Guid listId, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetListedCount(listId, notIfAlsoInListTypes);
        }

        IDictionary<Guid, int> IContenderListsQuery.GetListedCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetListedCounts(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes);
        }

        IDictionary<Guid, DateTime?> IContenderListsQuery.GetLastUsedTimes(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes)
        {
            return _repository.GetLastUsedTimes(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes);
        }

        int IContenderListsQuery.GetListCount(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, Guid candidateId)
        {
            return _repository.GetListCount(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes, candidateId);
        }

        IDictionary<Guid, int> IContenderListsQuery.GetListCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, IEnumerable<Guid> candidateIds)
        {
            return _repository.GetListCounts(ownerId, sharedWithId, listTypes, notIfAlsoInListTypes, candidateIds);
        }

        bool IContenderListsQuery.IsApplicant(Guid listId, Guid contenderId)
        {
            return _repository.IsApplicant(listId, contenderId);
        }

        IList<Guid> IContenderListsQuery.GetApplicantListIds(IEnumerable<int> listTypes, Guid contenderId)
        {
            return _repository.GetApplicantListIds(listTypes, contenderId);
        }

        IList<Guid> IContenderListsQuery.GetApplicantListIds(IEnumerable<Guid> listIds, IEnumerable<int> listTypes, Guid contenderId)
        {
            return _repository.GetApplicantListIds(listIds, listTypes, contenderId);
        }

        ApplicantStatus IContenderListsQuery.GetApplicantStatus(Guid applicationId)
        {
            return _repository.GetApplicantStatus(applicationId);
        }
    }
}