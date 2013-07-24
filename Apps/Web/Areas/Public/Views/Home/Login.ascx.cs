using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Models.Home;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Areas.Public.Views.Home
{
    public class Login
        : ViewUserControl<HomeModel>
    {
        protected static ReadOnlyUrl EmployerJoinUrl { get { return AccountsRoutes.Join.GenerateUrl(); } }
        protected static ReadOnlyUrl TermsUrl { get { return SupportRoutes.Terms.GenerateUrl(); } }

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
