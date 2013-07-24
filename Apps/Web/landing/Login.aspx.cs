using System;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Content;
using LinkMe.Web.UI;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Landing
{
    public partial class Login : LinkMePage
    {
        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UseStandardStyleSheetReferences = false;
            AddStyleSheetReference(StyleSheets.Landing);
            AddStyleSheetReference(StyleSheets.FrontPage);
            AddStyleSheetReference(StyleSheets.Forms);

            // This form will act as the action for a number of other pages, both
            // within and without the LinkMe domain, so use an absolute url.
            // Make sure it is secure so that all posts conceal the password etc.

            var actionUrl = GetClientUrl().AsNonReadOnly();
            actionUrl.Scheme = Url.SecureScheme;
            LinkMeLoginForm.ActionUrl = actionUrl.AbsoluteUri;

            // Now that everything is set up check to see whether this is a POST.
            // This POST may or may not have originated from a btnJoin click so don't process
            // that event.  Simply look for a POST and try to join.

            if (Request.HttpMethod == "POST")
                OnPost();
        }

        private void OnPost()
        {
            // Need to determine whether this has been generated from someone clicking the
            // btnJoin button or whether this is from a post from another page.
            // If it results from the btnJoin being clicked then let it go as the event will
            // be generated later.

            string[] values = Request.Form.GetValues(ucLogin.LoginButtonId);
            if (values == null || values.Length == 0)
            {
                // Extract the values from the posted form.

                string email = Request.Form["LinkMeUsername"];
                string password = Request.Form["LinkMePassword"];
                var remember = false;
                if (!string.IsNullOrEmpty(Request.Form["LinkMeLoginRememberMe"]))
                    bool.TryParse(Request.Form["LinkMeLoginRememberMe"], out remember);

                ucLogin.Populate(email, password, remember);

                ucLogin.ProcessLogin(false);
            }
        }
    }
}
