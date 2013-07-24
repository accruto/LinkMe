using System.Collections.Generic;
using LinkMe.Domain.Users.Employers.Candidates;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    internal class BlockListComparer
        : IComparer<CandidateBlockList>
    {
        public int Compare(CandidateBlockList x, CandidateBlockList y)
        {
            // Temporary before Permanent.

            if (x.BlockListType != y.BlockListType)
            {
                if (x.BlockListType == BlockListType.Temporary)
                    return -1;
                if (y.BlockListType == BlockListType.Temporary)
                    return 1;

                return x.BlockListType == BlockListType.Permanent ? -1 : 1;
            }

            // If nothing else then use the name.

            return 0;
        }
    }
}
