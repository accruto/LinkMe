using System;
using System.Collections.Generic;
using LinkMe.Domain.Users.Employers.Candidates;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    internal class FolderComparer
        : IComparer<CandidateFolder>
    {
        private readonly IDictionary<Guid, DateTime?> _counts;

        public FolderComparer(IDictionary<Guid, DateTime?> counts)
        {
            _counts = counts;
        }

        public int Compare(CandidateFolder x, CandidateFolder y)
        {
            // Flagged before Shortlist before Mobile before Private before Shared.

            if (x.FolderType != y.FolderType)
            {
                if (x.FolderType == FolderType.Shortlist)
                    return -1;
                if (y.FolderType == FolderType.Shortlist)
                    return 1;

                if (x.FolderType == FolderType.Mobile)
                    return -1;
                if (y.FolderType == FolderType.Mobile)
                    return 1;

                if (x.FolderType == FolderType.Private)
                    return -1;
                if (y.FolderType == FolderType.Private)
                    return 1;

                return x.FolderType == FolderType.Shared ? -1 : 1;
            }

            // Same type, so order by either the last time a candidate was added, or if none then by when the folder was created.

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
            _counts.TryGetValue(folderId, out lastUsedTime);
            return lastUsedTime;
        }
    }
}
