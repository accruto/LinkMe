using LinkMe.Domain.Users.Employers.Candidates;

namespace LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates
{
    public static class CandidateFlagListsExtensions
    {
        public static string GetNameDisplayText(this CandidateFlagList flagList)
        {
            return "Flagged candidates";
        }
    }
}
