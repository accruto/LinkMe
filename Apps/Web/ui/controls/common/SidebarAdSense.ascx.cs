using LinkMe.Apps.Agents.Context;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class SidebarAdSense : LinkMeUserControl, ISectionControl
    {
        public bool ShowSection
        {
            get { return !Request.IsSecureConnection && !ActivityContext.Current.Community.IsSet; }
        }

        public string SectionTitle
        {
            get { return null; }
        }
    }
}