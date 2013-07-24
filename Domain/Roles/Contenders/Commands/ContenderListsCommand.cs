using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders.Commands
{
    public class ContenderListsCommand
        : IContenderListsCommand
    {
        private readonly IContenderListsRepository _repository;

        public ContenderListsCommand(IContenderListsRepository repository)
        {
            _repository = repository;
        }

        void IContenderListsCommand.CreateList(ContenderList contenderList)
        {
            contenderList.Prepare();
            contenderList.Validate();
            _repository.CreateList(contenderList);
        }

        void IContenderListsCommand.UpdateList(ContenderList contenderList)
        {
            contenderList.Validate();
            _repository.UpdateList(contenderList);
        }

        void IContenderListsCommand.DeleteList(Guid id)
        {
            _repository.DeleteList(id);
        }

        void IContenderListsCommand.CreateEntry(Guid listId, ContenderListEntry contenderListEntry)
        {
            contenderListEntry.Prepare();
            contenderListEntry.Validate();
            _repository.CreateEntry(listId, contenderListEntry);
        }

        void IContenderListsCommand.CreateEntries(Guid listId, IEnumerable<Guid> contenderIds)
        {
            _repository.CreateEntries(listId, DateTime.Now, contenderIds);
        }

        void IContenderListsCommand.CreateEntries(Guid listId, IEnumerable<Guid> contenderIds, ApplicantStatus status)
        {
            _repository.CreateEntries(listId, DateTime.Now, contenderIds, status);
        }

        void IContenderListsCommand.DeleteEntry(Guid listId, Guid contenderId)
        {
            _repository.DeleteEntry(listId, contenderId);
        }

        void IContenderListsCommand.DeleteEntries(Guid listId, IEnumerable<Guid> contenderIds)
        {
            _repository.DeleteEntries(listId, contenderIds);
        }

        void IContenderListsCommand.DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> contenderIds)
        {
            _repository.DeleteEntries(ownerId, listTypes, contenderIds);
        }

        void IContenderListsCommand.DeleteAllEntries(Guid listId)
        {
            _repository.DeleteAllEntries(listId);
        }

        void IContenderListsCommand.DeleteAllEntries(Guid ownerId, IEnumerable<int> listTypes)
        {
            _repository.DeleteAllEntries(ownerId, listTypes);
        }

        void IContenderListsCommand.ChangeStatus(Guid listId, Guid applicantId, ApplicantStatus status)
        {
            _repository.ChangeStatus(listId, applicantId, status);
        }

        void IContenderListsCommand.ChangeStatus(Guid listId, IEnumerable<Guid> applicantIds, ApplicantStatus status)
        {
            _repository.ChangeStatus(listId, applicantIds, status);
        }

        void IContenderListsCommand.UpdateApplication(Guid listId, Guid applicantId, Guid applicationId)
        {
            _repository.UpdateApplication(listId, applicantId, applicationId);
        }
    }
}