using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public abstract class ViewPage
        : System.Web.Mvc.ViewPage
    {
        private RegisteredUser CurrentRegisteredUser
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

    public abstract class ViewPage<T>
        : System.Web.Mvc.ViewPage<T>
        where T : class
    {
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
