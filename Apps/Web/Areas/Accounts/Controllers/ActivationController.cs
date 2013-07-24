using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Users;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Accounts.Models;
using LinkMe.Web.Areas.Accounts.Routes;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Controllers;
using LinkMe.Web.Members.Friends;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    public class ActivationController
        : AccountsLoginJoinController
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand;
        private readonly IAccountVerificationsCommand _accountVerificationsCommand;
        private readonly IAuthenticationManager _authenticationManager;

        public ActivationController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery, IMemberAccountsCommand memberAccountsCommand, IAccountVerificationsCommand accountVerificationsCommand, IAuthenticationManager authenticationManager)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
            _memberAccountsCommand = memberAccountsCommand;
            _accountVerificationsCommand = accountVerificationsCommand;
            _authenticationManager = authenticationManager;
        }

        public ActionResult Activation(string activationCode)
        {
            string loginId = null;

            try
            {
                if (string.IsNullOrEmpty(activationCode))
                {
                    ModelState.AddModelError(new RequiredValidationError("activationCode"), new ActivationErrorHandler());
                    return View(new ActivationModel { Login = new Login() });
                }

                // Activate.

                var memberId = _accountVerificationsCommand.Activate(activationCode);
                if (memberId == null)
                {
                    ModelState.AddModelError(new NotFoundValidationError("activationCode", activationCode), new ActivationErrorHandler());
                    return View(new ActivationModel { Login = new Login() });
                }

                // Checked whether logged in.

                var currentUser = CurrentRegisteredUser;
                if (currentUser == null)
                {
                    loginId = GetLoginId(memberId.Value);
                }
                else
                {
                    if (currentUser.Id == memberId)
                    {
                        // Update the user.

                        _authenticationManager.UpdateUser(HttpContext, currentUser, true);
                        return RedirectToUrl(GetLoggedInMemberUrl(currentUser));
                    }
                }

                // Check for redirect.

                var returnUrl = HttpContext.Request.GetReturnUrl();
                if (returnUrl != null)
                    return RedirectToUrl(returnUrl);

                ModelState.AddModelConfirmation("Your account is now activated, please log in.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new ActivationErrorHandler());
            }

            return View(new ActivationModel { Login = new Login {LoginId = loginId} });
        }

        [HttpPost]
        public ActionResult Activation(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var result = TryLogIn(loginModel, rememberMe, r => null, () => new ActivationErrorHandler());
            return result ?? View(new ActivationModel { Login = loginModel });
        }

        public ActionResult Verification(string verificationCode)
        {
            string loginId = null;

            try
            {
                if (string.IsNullOrEmpty(verificationCode))
                {
                    ModelState.AddModelError(new RequiredValidationError("verificationCode"), new ActivationErrorHandler());
                    return View(new ActivationModel { Login = new Login() });
                }

                // Activate.

                var memberId = _accountVerificationsCommand.Verify(verificationCode);
                if (memberId == null)
                {
                    ModelState.AddModelError(new NotFoundValidationError("verificationCode", verificationCode), new ActivationErrorHandler());
                    return View(new ActivationModel { Login = new Login() });
                }

                // Checked whether logged in.

                var currentUser = CurrentRegisteredUser;
                if (currentUser == null)
                {
                    loginId = GetLoginId(memberId.Value);
                }
                else
                {
                    if (currentUser.Id == memberId)
                    {
                        // Update the user.

                        _authenticationManager.UpdateUser(HttpContext, currentUser, true);
                        return RedirectToUrl(GetLoggedInMemberUrl(currentUser));
                    }
                }

                ModelState.AddModelConfirmation("Your email address is now verified, please log in.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new ActivationErrorHandler());
            }

            return View(new ActivationModel { Login = new Login { LoginId = loginId } });
        }

        [HttpPost]
        public ActionResult Verification(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var result = TryLogIn(loginModel, rememberMe, r => null, () => new ActivationErrorHandler());
            return result ?? View(new ActivationModel { Login = loginModel });
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult NotActivated()
        {
            // A user may activate their account after being redirected to this form.
            // Because the activation status is kept in the session it needs to be refreshed
            // from the database.  Set the flag here that indicates that the next time a page
            // is accessed an explicit check of the database is needed.

            _authenticationManager.UpdateUser(HttpContext, CurrentRegisteredUser, true);

            // If they've already been activated then just redirect to the homepage.

            return CurrentMember.IsActivated ? RedirectToUrl(HttpContext.GetHomeUrl()) : View(CurrentMember);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member), HttpPost, ActionName("NotActivated")]
        public ActionResult ResendActivationEmail()
        {
            _accountVerificationsCommand.ResendActivation(CurrentRegisteredUser);

            // Redirect to the sent page.

            return RedirectToRoute(AccountsRoutes.ActivationSent);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult NotVerified()
        {
            // A user may activate their account after being redirected to this form.
            // Because the activation status is kept in the session it needs to be refreshed
            // from the database.  Set the flag here that indicates that the next time a page
            // is accessed an explicit check of the database is needed.

            _authenticationManager.UpdateUser(HttpContext, CurrentRegisteredUser, true);

            // If they've already been activated then just redirect to the homepage.

            var member = CurrentMember;
            if (member.EmailAddresses == null || member.EmailAddresses.All(e => e.IsVerified))
                return RedirectToUrl(HttpContext.GetHomeUrl());

            return View(CurrentMember);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member), HttpPost, ActionName("NotVerified")]
        public ActionResult ResendVerificationEmails()
        {
            var member = CurrentMember;
            foreach (var emailAddress in member.EmailAddresses.Where(a => !a.IsVerified))
                _accountVerificationsCommand.ResendVerification(CurrentRegisteredUser, emailAddress.Address);

            // Redirect to the sent page.

            return RedirectToRoute(AccountsRoutes.VerificationSent, new { returnUrl = HttpContext.GetReturnUrl() });
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult ActivationSent()
        {
            return View(CurrentMember);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult VerificationSent()
        {
            return View(CurrentMember);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult ChangeEmail()
        {
            var member = CurrentMember;
            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();

            return View(new ChangeEmailModel
            {
                EmailAddress = primaryEmailAddress == null ? null : primaryEmailAddress.Address,
                SecondaryEmailAddress = secondaryEmailAddress == null ? null : secondaryEmailAddress.Address,
            });
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member), HttpPost, ButtonClicked("Send")]
        public ActionResult ChangeEmail(ChangeEmailModel changeEmail)
        {
            try
            {
                // Validate.

                changeEmail.EmailAddress = changeEmail.EmailAddress == null ? null : changeEmail.EmailAddress.Trim();
                changeEmail.SecondaryEmailAddress = changeEmail.SecondaryEmailAddress == null ? null : changeEmail.SecondaryEmailAddress.Trim();
                changeEmail.Validate();

                // If changing the email address make sure it is not already being used.

                var member = CurrentMember;
                if (changeEmail.EmailAddress != member.EmailAddresses[0].Address)
                {
                    if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = changeEmail.EmailAddress }))
                        throw new DuplicateUserException();
                }

                // Change the email addresses and login id for the user.

                member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = changeEmail.EmailAddress, IsVerified = false } };
                if (!string.IsNullOrEmpty(changeEmail.SecondaryEmailAddress))
                    member.EmailAddresses.Add(new EmailAddress { Address = changeEmail.SecondaryEmailAddress, IsVerified = false });
                _memberAccountsCommand.UpdateMember(member);

                // Send activation.

                _accountVerificationsCommand.SendReactivation(member);
                _accountVerificationsCommand.StartActivationWorkflow(member);

                if (member.EmailAddresses.Count > 1 && !member.EmailAddresses[1].IsVerified)
                    _accountVerificationsCommand.SendVerification(member, member.EmailAddresses[1].Address);

                // Redirect.

                return RedirectToRoute(AccountsRoutes.ActivationSent);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new ActivationErrorHandler());
            }

            return View(changeEmail);
        }

        [EnsureHttps, EnsureAuthorized, HttpPost, ActionName("ChangeEmail"), ButtonClicked("Cancel")]
        public ActionResult CancelChangeEmail()
        {
            return RedirectToReturnUrl();
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult Welcome()
        {
            return View(CurrentMember);
        }

        protected override ReadOnlyUrl GetLoggedInMemberUrl(IUser user)
        {
            if (IsInvite)
                return NavigationManager.GetUrlForPage<Invitations>();

            var member = user as IMember;
            return member == null
                ? base.GetLoggedInMemberUrl(user)
                : ProfilesRoutes.Profile.GenerateUrl();
        }

        private bool IsInvite
        {
            get
            {
                // Just check for the "flag" in the query string to handle a special case.

                var isInvite = Request.QueryString["isInvite"];
                if (isInvite == null)
                    return false;

                bool value;
                return bool.TryParse(isInvite, out value) && value;
            }
        }

        private string GetLoginId(Guid userId)
        {
            var credentials = _loginCredentialsQuery.GetCredentials(userId);
            return credentials == null ? null : credentials.LoginId;
        }
    }
}