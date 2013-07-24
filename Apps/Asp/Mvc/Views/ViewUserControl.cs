using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public class ViewUserControl
        : System.Web.Mvc.ViewUserControl
    {
        protected static ReadOnlyUrl GetUrlForPage<T>(params string[] queryString)
        {
            return NavigationManager.GetUrlForPage<T>(queryString);
        }

        protected ReadOnlyUrl ClientUrl
        {
            get { return ViewData.GetClientUrl(); }
        }

        protected ActivityContext ActivityContext
        {
            get { return ViewData.GetActivityContext(); }
        }

        protected RegisteredUser CurrentRegisteredUser
        {
            get { return ViewData.GetCurrentRegisteredUser(); }
        }

        protected Member CurrentMember
        {
            get { return CurrentRegisteredUser as Member; }
        }

        protected Employer CurrentEmployer
        {
            get { return CurrentRegisteredUser as Employer; }
        }

        protected Administrator CurrentAdministrator
        {
            get { return CurrentRegisteredUser as Administrator; }
        }
    }

    public class ViewUserControl<TModel>
        : System.Web.Mvc.ViewUserControl<TModel>
        where TModel : class
    {
        protected static ReadOnlyUrl GetUrlForPage<T>(params string[] queryString)
        {
            return NavigationManager.GetUrlForPage<T>(queryString);
        }

        protected ReadOnlyUrl ClientUrl
        {
            get { return ViewData.GetClientUrl(); }
        }

        protected RegisteredUser CurrentRegisteredUser
        {
            get { return ViewData.GetCurrentRegisteredUser(); }
        }

        protected Member CurrentMember
        {
            get { return CurrentRegisteredUser as Member; }
        }

        protected Employer CurrentEmployer
        {
            get { return CurrentRegisteredUser as Employer; }
        }

        protected Administrator CurrentAdministrator
        {
            get { return CurrentRegisteredUser as Administrator; }
        }
    }
}
