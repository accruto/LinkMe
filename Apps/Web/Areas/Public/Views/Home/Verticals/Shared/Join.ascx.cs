using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Public.Models.Home;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared
{
    public class Join
        : ViewUserControl<HomeModel>
    {
        protected static readonly ReadOnlyUrl HomeUrl = HomeRoutes.Home.GenerateUrl();
        protected static readonly ReadOnlyUrl TermsUrl = SupportRoutes.Terms.GenerateUrl();

        protected bool IsJoinError(string name)
        {
            return Model.Join != null
                && !ViewData.ModelState.IsValid
                && ViewData.ModelState[name] != null
                && ViewData.ModelState[name].Errors.Count > 0;
        }

        protected string GetJoinError(string name)
        {
            return ViewData.ModelState[name].Errors[0].ErrorMessage;
        }
    }
}
