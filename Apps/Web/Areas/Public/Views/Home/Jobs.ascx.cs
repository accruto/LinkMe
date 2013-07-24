using System;
using System.Text;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Api.Routes;
using LinkMe.Web.Areas.Public.Models.Home;
using SearchRoutes=LinkMe.Web.Areas.Members.Routes.SearchRoutes;

namespace LinkMe.Web.Areas.Public.Views.Home
{
    public class Jobs
        : ViewUserControl<ReferenceModel>
    {
        protected static ReadOnlyUrl PartialMatchesUrl { get { return LocationRoutes.PartialMatches.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchUrl { get { return SearchRoutes.Search.GenerateUrl(); } }

        protected string GetJobTypes()
        {
            var names = Enum.GetNames(typeof(JobTypes));
            var values = Enum.GetValues(typeof(JobTypes));

            var jobTypes = new StringBuilder();
            for (var index = 0; index < names.Length; ++index)
            {
                if ((JobTypes)values.GetValue(index) != JobTypes.All && (JobTypes)values.GetValue(index) != JobTypes.None)
                    jobTypes.Append(jobTypes.Length == 0 ? "" : ",")
                        .Append("{ name: \"").Append(names[index])
                        .Append("\", value: ").Append((int) values.GetValue(index)).Append("}");
            }
            return jobTypes.ToString();
        }
    }
}
