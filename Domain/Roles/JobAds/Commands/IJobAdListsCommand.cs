using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public interface IJobAdListsCommand
    {
        void CreateList(JobAdList jobAdList);
        void UpdateList(JobAdList jobAdList);
        void DeleteList(Guid id);

        void CreateEntries(Guid listId, IEnumerable<Guid> jobAdIds);

        void DeleteEntries(Guid listId);
        void DeleteEntries(Guid ownerId, IEnumerable<int> listTypes);
        void DeleteEntries(Guid listId, IEnumerable<Guid> jobAdIds);
        void DeleteEntries(Guid ownerId, IEnumerable<int> listTypes, IEnumerable<Guid> jobAdIds);
    }
}