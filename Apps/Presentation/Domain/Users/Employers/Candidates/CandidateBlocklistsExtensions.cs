using LinkMe.Domain.Users.Employers.Candidates;

namespace LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates
{
    public static class CandidateBlockListsExtensions
    {
        public static string GetNameDisplayText(this CandidateBlockList blockList)
        {
            return blockList.BlockListType.GetNameDisplayText();
        }

        public static string GetNameDisplayText(this BlockListType blockListType)
        {
            switch (blockListType)
            {
                case BlockListType.Temporary:
                    return "Current search";

                default:
                    return "All searches";
            }
        }
    }
}
