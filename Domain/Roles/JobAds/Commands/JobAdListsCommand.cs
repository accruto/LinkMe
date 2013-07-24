using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public class JobAdListsCommand
        : IJobAdListsCommand
    {
        private readonly IJobAdListsRepository _repository;

        public JobAdListsCommand(IJobAdListsRepository repository)
        {
            _repository = repository;
        }

        void IJobAdListsCommand.CreateList(JobAdList jobAdList)
        {
            jobAdList.Prepare();
            jobAdList.Validate();
            _repository.CreateList(jobAdList);
        }

        void IJobAdListsCommand.UpdateList(JobAdList jobAdList)
        {
            jobAdList.Validate();
            _repository.UpdateList(jobAdList);
        }

        void IJobAdListsCommand.DeleteList(Guid id)
        {
            _repository.DeleteList(id);
        }

        void IJobAdListsCommand.CreateEntries(Guid listId, IEnumerable<Guid> jobAdIds)
        {
            _repository.CreateEntries(listId, DateTime.Now, jobAdIds);
        }

        void IJobAdListsCommand.DeleteEntries(Guid listId, IEnumerable<Guid> jobAdIds)
        {
            _repository.DeleteEntries(listId, jobAdIds);
        }

        void IJobAdListsCommand.DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> jobAdIds)
        {
            _repository.DeleteEntries(ownerId, listTypes, jobAdIds);
        }

        void IJobAdListsCommand.DeleteEntries(Guid listId)
        {
            _repository.DeleteEntries(listId);
        }

        void IJobAdListsCommand.DeleteEntries(Guid ownerId, IEnumerable<int> listTypes)
        {
            _repository.DeleteEntries(ownerId, listTypes);
        }
    }
}