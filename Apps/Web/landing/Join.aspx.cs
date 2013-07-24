using System;
using System.Web;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Referrals;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Validation;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Content;
using LinkMe.Web.Domain.Roles.Affiliations.Communities;
using LinkMe.Web.UI;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Landing
{
    public partial class Join
        : LinkMePage
    {
        private readonly IReferralsManager _referralsManager = Container.Current.Resolve<IReferralsManager>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            btnJoin.Click += btnJoin_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UseStandardStyleSheetReferences = false;
            AddStyleSheetReference(StyleSheets.Landing);

            // This form will act as the action for a number of other pages, both
            // within and without the LinkMe domain, so use an absolute url.
            // Make sure it is secure so that all posts conceal the password etc.

            var actionUrl = GetClientUrl().AsNonReadOnly();
            actionUrl.Scheme = Url.SecureScheme;
            LinkMeJoinForm.ActionUrl = actionUrl.AbsoluteUri;

            // Set up the validator.

            valDuplicateEmail.ErrorMessage = ValidationErrorMessages.DUPLICATE_USER_PROFILE + "<br />";
            valDuplicateEmail.ServerValidate += delegate(object sender, ServerValidateEventArgs args) { args.IsValid = true; };

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

            var values = Request.Form.GetValues(btnJoin.UniqueID);
            if (values == null || values.Length == 0)
            {
                // Extract the values from the posted form.

                var email = Request.Form["txtUsername"];
                var password = Request.Form["txtPassword"];
                var firstName = Request.Form["txtFirstName"];
                var lastName = Request.Form["txtLastName"];
                var acceptTermsAndConditions = false;
                if (!string.IsNullOrEmpty(Request.Form["chkAcceptTermsAndConditions"]))
                    bool.TryParse(Request.Form["chkAcceptTermsAndConditions"], out acceptTermsAndConditions);

                ucJoin.Populate(email, password, firstName, lastName, acceptTermsAndConditions);

                OnJoin();
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            OnJoin();
        }

        private void OnJoin()
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            // Try to create the member account.

            var member = ucJoin.GetMember();
            var credentials = ucJoin.GetUserCredentials();

            Guid? communityId = null;
            var community = _communitiesQuery.GetCurrentCommunity();
            if (community != null && community.HasMembers)
                communityId = community.Id;

            try
            {
                _memberAccountsCommand.CreateMember(member, credentials, communityId);
            }
            catch (DuplicateUserException)
            {
                valDuplicateEmail.IsValid = false;
                return;
            }

            // Do all the steps needed.

            _referralsManager.CreateReferral(HttpContext.Current.Request, member.Id);
            _authenticationManager.LogIn(new HttpContextWrapper(HttpContext.Current), member, AuthenticationStatus.Authenticated);

            // Now that everything is OK redirect to the next page in the process.

            NavigationManager.Redirect(JoinRoutes.Join.GenerateUrl());
        }
    }
}
