using System;
using System.Web;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;
using LinkMe.Web.UI;
using Constants = LinkMe.Apps.Asp.Constants;

namespace LinkMe.Web.Employers.Guests
{
    public partial class LoginRequired : LinkMePage
    {
        public const string ActionTextParam = "actionText";
        public const string ReturnUrlParam = Constants.ReturnUrlParameter;
        public const string LogLoginForParam = "logLoginFor";
        public const string LogLoginForSuggestedCandidates = "SC";

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
            return UserType.Employer;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Employer);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack && LoggedInUserId.HasValue)
            {
                // Already logged in (they probably browsed back or something). No point in displaying
                // this page, just redirect to where they want to go (or home if not specified).

                NavigationManager.Redirect(Context.GetReturnUrl());
            }
            else
            {
                ucContent.ImageUrl = new ApplicationUrl("~/ui/images/employer/saved-candidates-not-logged-in.png");

                var actionText = HttpUtility.HtmlEncode(Request.QueryString[ActionTextParam]);
                if (string.IsNullOrEmpty(actionText))
                {
                    actionText = "perform this action";
                }

                ucContent.Heading = "To " + actionText + " you need to log in";
            }
        }
    }
}
