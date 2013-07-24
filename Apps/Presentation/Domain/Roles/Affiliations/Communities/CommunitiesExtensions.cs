using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Apps.Presentation.Domain.Roles.Affiliations.Communities
{
    public static class CommunitiesExtensions
    {
        public static string GetShortDisplayText(this Community community)
        {
            return community.ShortName ?? community.Name;
        }
    }
}
