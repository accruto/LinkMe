using System;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common.Navs
{
    public class NavUserControl
        : LinkMeUserControl
    {
        private static readonly IVerticalsQuery VerticalsQuery = Container.Current.Resolve<IVerticalsQuery>();
        protected string ActiveVerticalHeader { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var vertical = GetVertical();
            if (vertical != null)
            {
                switch (vertical.Url.ToLower())
                {
                    case "autopeople":
                    case "otd":
                    case "vecci":
                        ActiveVerticalHeader = vertical.Url.ToLower();
                        break;

                    default:
                        ActiveVerticalHeader = "shared";
                        break;
                }
            }
        }

        private static Vertical GetVertical()
        {
            var activityContext = ActivityContext.Current;
            if (activityContext == null)
                return null;

            var verticalId = activityContext.Vertical.Id;
            return verticalId == null ? null : GetVertical(verticalId.Value);
        }

        private static Vertical GetVertical(Guid verticalId)
        {
            // The vertical must have a url.

            var vertical = VerticalsQuery.GetVertical(verticalId);
            return vertical == null || string.IsNullOrEmpty(vertical.Url)
                ? null
                : vertical;
        }
    }
}
