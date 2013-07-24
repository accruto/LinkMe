using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Commands
{
    public interface IContenderListsCommand
    {
        void CreateList(ContenderList contenderList);
        void UpdateList(ContenderList contenderList);
        void DeleteList(Guid id);

        void CreateEntry(Guid listId, ContenderListEntry contenderListEntry);
        void CreateEntries(Guid listId, IEnumerable<Guid> contenderIds);
        void CreateEntries(Guid listId, IEnumerable<Guid> contenderIds, ApplicantStatus status);

        void DeleteEntry(Guid listId, Guid contenderId);
        void DeleteEntries(Guid listId, IEnumerable<Guid> contenderIds);
        void DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> contenderIds);
        void DeleteAllEntries(Guid listId);
        void DeleteAllEntries(Guid ownerId, IEnumerable<int> listTypes);

        void ChangeStatus(Guid listId, Guid applicantId, ApplicantStatus status);
        void ChangeStatus(Guid listId, IEnumerable<Guid> applicantIds, ApplicantStatus status);
        void UpdateApplication(Guid listId , Guid applicantId, Guid applicationId);
    }
}