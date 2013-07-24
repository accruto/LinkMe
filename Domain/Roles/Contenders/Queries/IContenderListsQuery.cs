using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public interface IContenderListsQuery
    {
        T GetList<T>(Guid id) where T : ContenderList, new();
        T GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes) where T : ContenderList, new();
        T GetSharedList<T>(Guid sharedWithId, string name, IEnumerable<int> listTypes) where T : ContenderList, new();

        IList<T> GetLists<T>(Guid ownerId, int listType) where T : ContenderList, new();
        IList<T> GetLists<T>(Guid ownerId, IEnumerable<int> listTypes) where T : ContenderList, new();
        IList<T> GetLists<T>(Guid ownerId, Guid sharedWithId, IEnumerable<int> listTypes) where T : ContenderList, new();
        IList<T> GetSharedLists<T>(Guid sharedWithId, int listType) where T : ContenderList, new();

        IList<T> GetEntries<T>(Guid listId, IEnumerable<int> notIfAlsoInListTypes) where T : ContenderListEntry, new();
        IList<T> GetEntries<T>(Guid listId, IEnumerable<int> notIfAlsoInListTypes, IEnumerable<Guid> contenderIds) where T : ContenderListEntry, new();
        IDictionary<Guid, IList<T>> GetEntries<T>(IEnumerable<Guid> listIds, IEnumerable<int> notIfAlsoInListTypes) where T : ContenderListEntry, new();

        bool IsListed(Guid listId, IEnumerable<int> notIfAlsoInListTypes, Guid contenderId);
        bool IsListed(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, Guid contenderId);

        IList<Guid> GetListedContenderIds(Guid listId, IEnumerable<int> notIfAlsoInListTypes);
        IList<Guid> GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes);
        IList<Guid> GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, IEnumerable<Guid> contenderIds);
        IList<Guid> GetListedContenderIds(Guid listId, IEnumerable<int> notIfAlsoInListTypes, ApplicantStatus status);

        int GetListedCount(Guid listId, IEnumerable<int> notIfAlsoInListTypes);
        IDictionary<Guid, int> GetListedCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes);

        IDictionary<Guid, DateTime?> GetLastUsedTimes(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes);

        int GetListCount(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, Guid candidateId);
        IDictionary<Guid, int> GetListCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfAlsoInListTypes, IEnumerable<Guid> candidateIds);

        bool IsApplicant(Guid listId, Guid contenderId);
        IList<Guid> GetApplicantListIds(IEnumerable<int> listTypes, Guid contenderId);
        IList<Guid> GetApplicantListIds(IEnumerable<Guid> listIds, IEnumerable<int> listTypes, Guid contenderId);
        ApplicantStatus GetApplicantStatus(Guid applicationId);
    }
}