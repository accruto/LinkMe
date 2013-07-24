using LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Members.Html
{
    public static class JobAlertsExtensions
    {
        // Split at first ','.

        public static string FirstPartDescription(this JobAdSearch item)
        {
            var description = item.Criteria.GetDisplayText();
            var pos = description.IndexOf(',');
            return pos == -1 ? description : description.Substring(0, pos);
        }

        public static string SecondPartDescription(this JobAdSearch item)
        {
            var description = item.Criteria.GetDisplayText();
            var pos = description.IndexOf(',');
            return pos == -1 ? string.Empty : description.Substring(pos + 2);
        }
    }
}