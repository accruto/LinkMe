using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Web.Areas.Administrators.Models;
using LinkMe.Web.Areas.Administrators.Models.Members;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Members
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class MaintainMembersController
        : AdministratorsController
    {
        private const int DefaultCount = 200;

        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly IAdministrativeMemberSearchCommand _administrativeMemberSearchCommand;
        private readonly IEmailsCommand _emailsCommand;

        public MaintainMembersController(IUserAccountsCommand userAccountsCommand, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, IMembersQuery membersQuery, IAdministrativeMemberSearchCommand administrativeMemberSearchCommand, IEmailsCommand emailsCommand)
        {
            _userAccountsCommand = userAccountsCommand;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _membersQuery = membersQuery;
            _administrativeMemberSearchCommand = administrativeMemberSearchCommand;
            _emailsCommand = emailsCommand;
        }

        public ActionResult Search()
        {
            return View(new MemberSearchModel { Criteria = new AdministrativeMemberSearchCriteria { Count = DefaultCount } });
        }

        [HttpPost, ButtonClicked("Search")]
        public ActionResult Search(AdministrativeMemberSearchCriteria criteria)
        {
            // Search.

            var memberIds = _administrativeMemberSearchCommand.Search(criteria);
            var members = _membersQuery.GetMembers(memberIds).ToDictionary(m => m.Id, m => m);

            return View(new MemberSearchModel
            {
                Criteria = criteria,
                Members = (from i in memberIds
                           select members[i]).ToList()
            });
        }

        public ActionResult Edit(Guid id)
        {
            var member = _membersQuery.GetMember(id);
            if (member == null)
                return NotFound("member", "id", id);

            // Check for credentials.

            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);

            return View(new UserModel<IMember, MemberLoginModel>
            {
                User = member,
                UserLogin = new MemberLoginModel { LoginId = credentials != null ? credentials.LoginId : null },
            });
        }

        [HttpPost, ButtonClicked("Enable")]
        public ActionResult Enable(Guid id)
        {
            var member = _membersQuery.GetMember(id);
            if (member == null)
                return NotFound("member", "id", id);

            _userAccountsCommand.EnableUserAccount(member, User.Id().Value);
            return RedirectToRoute(MembersRoutes.Edit, new { id });
        }

        [HttpPost, ActionName("Enable"), ButtonClicked("Disable")]
        public ActionResult Disable(Guid id)
        {
            var member = _membersQuery.GetMember(id);
            if (member == null)
                return NotFound("member", "id", id);

            _userAccountsCommand.DisableUserAccount(member, User.Id().Value);
            return RedirectToRoute(MembersRoutes.Edit, new { id });
        }

        [HttpPost, ActionName("Enable"), ButtonClicked("Activate")]
        public ActionResult Activate(Guid id)
        {
            var member = _membersQuery.GetMember(id);
            if (member == null)
                return NotFound("member", "id", id);

            _userAccountsCommand.ActivateUserAccount(member, User.Id().Value);
            return RedirectToRoute(MembersRoutes.Edit, new { id });
        }

        [HttpPost, ActionName("Enable"), ButtonClicked("Deactivate")]
        public ActionResult Deactivate(Guid id)
        {
            var member = _membersQuery.GetMember(id);
            if (member == null)
                return NotFound("member", "id", id);

            _userAccountsCommand.DeactivateUserAccount(member, User.Id().Value);
            return RedirectToRoute(MembersRoutes.Edit, new { id });
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, MemberLoginModel memberLogin, [Bind(Include = "SendPasswordEmail")] CheckBoxValue sendPasswordEmail)
        {
            var member = _membersQuery.GetMember(id);
            if (member == null)
                return NotFound("member", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);
            if (credentials == null)
                return NotFound("member", "id", id);

            try
            {
                // Validate.

                memberLogin.SendPasswordEmail = sendPasswordEmail.IsChecked;
                memberLogin.Validate();

                // Update.

                credentials.PasswordHash = LoginCredentials.HashToString(memberLogin.Password);
                credentials.MustChangePassword = true;
                _loginCredentialsCommand.UpdateCredentials(member.Id, credentials, User.Id().Value);

                string message;
                if (memberLogin.SendPasswordEmail)
                {
                    var reminderEmail = new PasswordReminderEmail(member, credentials.LoginId, memberLogin.Password);
                    _emailsCommand.TrySend(reminderEmail);
                    message = "The password has been reset and an email has been sent.";
                }
                else
                {
                    message = "The password has been reset.";
                }

                return RedirectToRouteWithConfirmation(MembersRoutes.Edit, new { id }, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            memberLogin.LoginId = credentials.LoginId;
            return View("Edit", new UserModel<IMember, MemberLoginModel>
            {
                User = _membersQuery.GetMember(id),
                UserLogin = memberLogin
            });
        }
    }
}