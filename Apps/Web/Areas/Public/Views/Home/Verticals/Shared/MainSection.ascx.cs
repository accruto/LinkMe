using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared
{
    public class MainSection
        : ViewUserControl
    {
        protected static readonly ReadOnlyUrl MascotImageUrl = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/mascots/default.png");
        protected static readonly ReadOnlyUrl UploadImageUrl = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/taglines/upload_your_resume_jobs_come_to_you.png");
    }
}
