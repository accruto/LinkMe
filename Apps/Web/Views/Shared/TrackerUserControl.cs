using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Utility.Configuration;

namespace LinkMe.Web.Views.Shared
{
    public abstract class TrackerUserControl
        : ViewUserControl
    {
        protected static bool TrackersEnabled
        {
            get { return ApplicationContext.Instance.GetBoolProperty(ApplicationContext.TRACKERS_ENABLED); }
        }
    }
}