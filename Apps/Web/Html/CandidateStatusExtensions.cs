using LinkMe.Domain;

namespace LinkMe.Web.Html
{
    public static class CandidateStatusExtensions
    {
        public static string GetCssClassPrefixForStatus(this CandidateStatus candidateStatus)
        {
            switch (candidateStatus)
            {
                case CandidateStatus.AvailableNow:
                    return "immediately-available";
                case CandidateStatus.ActivelyLooking:
                    return "actively-looking";
                case CandidateStatus.OpenToOffers:
                    return "happy-to-talk";
                case CandidateStatus.NotLooking:
                    return "not-looking";
                case CandidateStatus.Unspecified:
                    return "not-specified";
            }
            // Default to unspecified for any new CandidateStatus.
            return "not-specified";
        }
    }
}
