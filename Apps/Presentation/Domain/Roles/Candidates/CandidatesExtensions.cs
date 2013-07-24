using System.Linq;
using System.Text;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public static class CandidatesExtensions
    {
        public static string GetIndustriesDisplayText(this ICandidate candidate, string separator)
        {
            var sb = new StringBuilder();

            if (!candidate.Industries.IsNullOrEmpty())
            {
                // Sort them alphabetically.

                foreach (var industry in candidate.Industries.OrderBy(i => i.Name))
                {
                    if (sb.Length > 0)
                        sb.Append(separator);
                    sb.Append(industry.Name);
                }
            }

            return sb.ToString();
        }

        public static string GetRelocationsDisplayText(this ICandidate candidate)
        {
            var sb = new StringBuilder();
            if (candidate.RelocationLocations != null)
            {
                foreach (var location in candidate.RelocationLocations)
                {
                    if (sb.Length != 0)
                        sb.Append(", ");

                    sb.Append(location.IsCountry ? location.Country.Name : location.ToString());
                }
            }

            return sb.Length == 0
                ? "no localities selected"
                : sb.ToString();
        }
    }
}
