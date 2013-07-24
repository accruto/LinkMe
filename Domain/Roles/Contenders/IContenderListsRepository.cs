using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders
{
    public interface IContenderListsRepository
    {
        void CreateList(ContenderList list);
        void UpdateList(ContenderList list);
        void DeleteList(Guid id);

        T GetList<T>(Guid id) where T : ContenderList, new();
        T GetList<T>(Guid ownerId, string name, IEnumerable<int> listTypes) where T : ContenderList, new();
        T GetSharedList<T>(Guid sharedWithId, string name, IEnumerable<int> listTypes) where T : ContenderList, new();

        IList<T> GetLists<T>(Guid ownerId, int listType) where T : ContenderList, new();
        IList<T> GetLists<T>(Guid ownerId, IEnumerable<int> listTypes) where T : ContenderList, new();
        IList<T> GetLists<T>(Guid ownerId, Guid sharedWithId, IEnumerable<int> listTypes) where T : ContenderList, new();
        IList<T> GetSharedLists<T>(Guid sharedWithId, int listType) where T : ContenderList, new();

        void CreateEntry(Guid listId, ContenderListEntry entry);
        void CreateEntries(Guid listId, DateTime time, IEnumerable<Guid> contenderIds);
        void CreateEntries(Guid listId, DateTime time, IEnumerable<Guid> contenderIds, ApplicantStatus status);
        void DeleteEntry(Guid listId, Guid contenderId);
        void DeleteEntries(Guid listId, IEnumerable<Guid> contenderIds);
        void DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> contenderIds);
        void DeleteAllEntries(Guid listId);
        void DeleteAllEntries(Guid ownerId, IEnumerable<int> listTypes);

        IList<T> GetEntries<T>(Guid listId, IEnumerable<int> notIfInListTypes) where T : ContenderListEntry, new();
        IList<T> GetEntries<T>(Guid listId, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds) where T : ContenderListEntry, new();
        IDictionary<Guid, IList<T>> GetEntries<T>(IEnumerable<Guid> listIds, IEnumerable<int> notIfInListTypes) where T : ContenderListEntry, new();

        bool IsListed(Guid listId, IEnumerable<int> notIfInListTypes, Guid contenderId);
        bool IsListed(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid contenderId);

        IList<Guid> GetListedContenderIds(Guid listId, IEnumerable<int> notIfInListTypes);
        IList<Guid> GetListedContenderIds(Guid listId, IEnumerable<int> notIfInListTypes, ApplicantStatus status);
        
        IList<Guid> GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes);
        IList<Guid> GetListedContenderIds(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds);

        int GetListedCount(Guid listId, IEnumerable<int> notIfInListTypes);
        IDictionary<Guid, int> GetListedCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes);

        IDictionary<Guid, DateTime?> GetLastUsedTimes(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes);

        int GetListCount(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, Guid contenderId);
        IDictionary<Guid, int> GetListCounts(Guid ownerId, Guid? sharedWithId, IEnumerable<int> listTypes, IEnumerable<int> notIfInListTypes, IEnumerable<Guid> contenderIds);

        bool IsApplicant(Guid listId, Guid contenderId);
        bool IsOwnerApplicant(Guid ownerId, Guid contenderId);
        IList<Guid> GetOwnerApplicants(Guid ownerId);
        IList<Guid> GetOwnerApplicants(Guid ownerId, IEnumerable<Guid> contenderIds);
        ApplicantStatus GetApplicantStatus(Guid applicationId);
        void ChangeStatus(Guid listId, Guid applicantId, ApplicantStatus status);
        void ChangeStatus(Guid listId, IEnumerable<Guid> applicantIds, ApplicantStatus status);
        void UpdateApplication(Guid listId, Guid applicantId, Guid applicationId);

        IList<Guid> GetApplicantListIds(IEnumerable<int> listTypes, Guid contenderId);
        IList<Guid> GetApplicantListIds(IEnumerable<Guid> listIds, IEnumerable<int> listTypes, Guid contenderId);
    }
}