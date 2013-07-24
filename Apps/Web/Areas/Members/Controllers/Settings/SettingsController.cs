using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Users;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Members.Models.Settings;
using LinkMe.Web.Controllers;
using LinkMe.Web.Mvc;
using Constants=LinkMe.Domain.Accounts.Constants;

namespace LinkMe.Web.Areas.Members.Controllers.Settings
{
    [EnsureHttps, EnsureAuthorized(UserType.Member, RequiresActivation = true)]
    public class SettingsController
        : MembersController
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IAccountVerificationsCommand _accountVerificationsCommand;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly IVerticalsQuery _verticalsQuery;

        public SettingsController(IMemberAccountsCommand memberAccountsCommand, ILoginCredentialsQuery loginCredentialsQuery, IUserAccountsCommand userAccountsCommand, IAuthenticationManager authenticationManager, IAccountVerificationsCommand accountVerificationsCommand, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand, IVerticalsQuery verticalsQuery)
        {
            _memberAccountsCommand = memberAccountsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _userAccountsCommand = userAccountsCommand;
            _authenticationManager = authenticationManager;
            _accountVerificationsCommand = accountVerificationsCommand;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult Settings()
        {
            var member = CurrentMember;
            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();

            var settingsModel = new SettingsModel
            {
                CanEditContactDetails = CanEditContactDetails(),
                FirstName = member.FirstName,
                LastName = member.LastName,
                EmailAddress = primaryEmailAddress == null ? null : primaryEmailAddress.Address,
                SecondaryEmailAddress = secondaryEmailAddress == null ? null : secondaryEmailAddress.Address,
            };

            UpdateCategories(settingsModel, _settingsQuery.GetSettings(User.Id().Value));

            return View(settingsModel);
        }

        [NoExternalLogin, HttpPost, ButtonClicked("Save")]
        public ActionResult Settings(SettingsModel settings)
        {
            try
            {
                // Validate before changing.

                settings.Prepare();
                settings.Validate();

                // Check for an existing login.

                var member = CurrentMember;
                if (member.GetPrimaryEmailAddress().Address != settings.EmailAddress && _loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = settings.EmailAddress }))
                    throw new DuplicateUserException();

                // Update.

                member.FirstName = settings.FirstName;
                member.LastName = settings.LastName;
                member.EmailAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address = settings.EmailAddress},
                };
                if (!string.IsNullOrEmpty(settings.SecondaryEmailAddress))
                    member.EmailAddresses.Add(new EmailAddress { Address = settings.SecondaryEmailAddress });

                _memberAccountsCommand.UpdateMember(member);
                SendVerifications(member);

                // Reset the display name cached in the authentication details, in case the user updated any details that affect it.

                _authenticationManager.UpdateUser(HttpContext, member, false);
                return RedirectToReturnUrlWithConfirmation("Your changes have been saved.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            UpdateCategories(settings, _settingsQuery.GetSettings(User.Id().Value));

            settings.CanEditContactDetails = CanEditContactDetails();
            return View(settings);
        }

        [HttpPost, ActionName("Settings"), ButtonClicked("Cancel")]
        public ActionResult CancelSettings()
        {
            return RedirectToReturnUrl();
        }

        [HttpPost, ButtonClicked("CommunicationsSave")]
        public ActionResult Communications()
        {
            var member = CurrentMember;
            var settings = _settingsQuery.GetSettings(User.Id().Value);

            try
            {
                var categories = _settingsQuery.GetCategories(UserType.Member);

                // Dynamically determine what has been changed.

                foreach (var category in (from c in categories where c.Timing == Timing.Periodic select c))
                {
                    var frequency = GetValue<Frequency>(category.Name);

                    // If the value has changed then update the database.

                    var currentFrequency = GetFrequency(category.Id, settings);
                    if (currentFrequency != frequency)
                        _settingsCommand.SetFrequency(member.Id, category.Id, frequency);
                }

                foreach (var category in (from c in categories where c.Timing == Timing.Notification select c))
                {
                    var frequency = IsChecked(category.Name) ? Frequency.Immediately : Frequency.Never;

                    // If the value has changed then update the database.

                    var currentFrequency = GetFrequency(category.Id, settings);
                    if (currentFrequency != frequency)
                        _settingsCommand.SetFrequency(member.Id, category.Id, frequency);
                }

                return RedirectToReturnUrlWithConfirmation("Your changes have been saved.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();

            var settingsModel = new SettingsModel
            {
                CanEditContactDetails = CanEditContactDetails(),
                FirstName = member.FirstName,
                LastName = member.LastName,
                EmailAddress = primaryEmailAddress == null ? null : primaryEmailAddress.Address,
                SecondaryEmailAddress = secondaryEmailAddress == null ? null : secondaryEmailAddress.Address,
            };

            UpdateCategories(settingsModel, settings);

            return View("Settings", settingsModel);
        }

        [HttpPost, ActionName("Communications"), ButtonClicked("CommunicationsCancel")]
        public ActionResult CancelCommunications()
        {
            return RedirectToReturnUrl();
        }

        [NoExternalLogin]
        public ActionResult Deactivate()
        {
            return View();
        }

        [NoExternalLogin, HttpPost, ButtonClicked("Deactivate")]
        public ActionResult Deactivate([Bind(Include="Reasons")] DeactivationReason? reasons, [Bind(Include="Comments")] string comments)
        {
            // Set defaults and truncate the comments if needed.  Don't report any errors etc to the user while they are trying to deactivate.

            if (reasons == null)
            {
                ModelState.AddModelError(new[] { new RequiredValidationError("reason") }, new StandardErrorHandler());
                return View();
            }

            comments = TextUtil.Truncate(comments, Constants.MaxDeactivationCommentsLength);

            _userAccountsCommand.DeactivateUserAccount(CurrentRegisteredUser, reasons.Value, comments);

            // Log out.

            _authenticationManager.LogOut(HttpContext);

            // Redirect straight to the default page using HTTP, otherwise HTTPS will be used if the current page is secure.

            return RedirectToUrl(new ReadOnlyApplicationUrl(false, NavigationManager.GetHomeUrl().PathAndQuery));
        }

        [NoExternalLogin, HttpPost, ActionName("Deactivate"), ButtonClicked("Cancel")]
        public ActionResult CancelDeactivate()
        {
            return RedirectToReturnUrl();
        }

        private static Frequency? GetFrequency(Guid categoryId, RecipientSettings settings)
        {
            return settings == null
                ? null
                : (from s in settings.CategorySettings
                   where s.CategoryId == categoryId
                   select s.Frequency).SingleOrDefault();
        }

        private void UpdateCategories(SettingsModel settingsModel, RecipientSettings settings)
        {
            var categories = _settingsQuery.GetCategories(UserType.Member);

            settingsModel.PeriodicCategories = (from c in categories
                                                where c.Timing == Timing.Periodic
                                                select new Tuple<Category, Frequency?>(c, GetFrequency(c.Id, settings))).ToList();

            settingsModel.NotificationCategories = (from c in categories
                                                    where c.Timing == Timing.Notification
                                                    select new Tuple<Category, Frequency?>(c, GetFrequency(c.Id, settings))).ToList();
        }

        private bool CanEditContactDetails()
        {
            var activityContext = ActivityContext;
            return activityContext == null || _verticalsQuery.CanEditContactDetails(activityContext.Vertical.Id);
        }

        private void SendVerifications(Member member)
        {
            // If any email addresses are no longer verified after an update then send verifications.

            foreach (var emailAddress in member.EmailAddresses)
            {
                if (!emailAddress.IsVerified)
                    _accountVerificationsCommand.SendVerification(member, emailAddress.Address);
            }
        }
    }
}