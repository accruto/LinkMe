using System;
using System.Collections.Generic;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    internal class FolderComparer
        : IComparer<JobAdFolder>
    {
        private readonly IDictionary<Guid, DateTime?> _lastUsedTimes;

        public FolderComparer(IDictionary<Guid, DateTime?> lastUsedTimes)
        {
            _lastUsedTimes = lastUsedTimes;
        }

        public int Compare(JobAdFolder x, JobAdFolder y)
        {
            // Order by either the last time a jobad was added, or if none then by when the folder was created.

            var xDateTime = GetLastUsedTime(x.Id) ?? x.CreatedTime;
            var yDateTime = GetLastUsedTime(y.Id) ?? y.CreatedTime;

            // Use descending order for the dates.

            if (xDateTime < yDateTime)
                return 1;
            if (xDateTime > yDateTime)
                return -1;

            // If nothing else then use the name.

            return string.Compare(x.Name, y.Name);
        }

        private DateTime? GetLastUsedTime(Guid folderId)
        {
            DateTime? lastUsedTime;
            return _lastUsedTimes.TryGetValue(folderId, out lastUsedTime) ? lastUsedTime : null;
        }
    }
}
