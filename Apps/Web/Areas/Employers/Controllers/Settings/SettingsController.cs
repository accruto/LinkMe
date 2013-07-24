using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.PhoneNumbers.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Validation;
using LinkMe.Web.Areas.Employers.Models.Settings;
using LinkMe.Web.Controllers;
using LinkMe.Apps.Agents.Context;

namespace LinkMe.Web.Areas.Employers.Controllers.Settings
{
    [EnsureHttps, EnsureAuthorized(UserType.Employer)]
    public class SettingsController
        : EmployersController
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand;
        private readonly IOrganisationsCommand _organisationsCommand;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IPhoneNumbersQuery _phoneNumbersQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IJobPostersCommand _jobPostersCommand;
        private readonly IJobPostersQuery _jobPostersQuery;
        private readonly INonMemberSettingsCommand _nonMemberSettingsCommand;
        private readonly INonMemberSettingsQuery _nonMemberSettingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ILinkedInCommand _linkedInCommand;
        private readonly ILinkedInQuery _linkedInQuery;

        public SettingsController(IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, IOrganisationsQuery organisationsQuery, IPhoneNumbersQuery phoneNumbersQuery, IIndustriesQuery industriesQuery, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, IAuthenticationManager authenticationManager, ICreditsQuery creditsQuery, IEmployerCreditsQuery employerCreditsQuery, IJobPostersCommand jobPostersCommand, IJobPostersQuery jobPostersQuery, INonMemberSettingsCommand nonMemberSettingsCommand, INonMemberSettingsQuery nonMemberSettingsQuery, ISettingsCommand settingsCommand, ISettingsQuery settingsQuery, ILinkedInCommand linkedInCommand, ILinkedInQuery linkedInQuery)
        {
            _employerAccountsCommand = employerAccountsCommand;
            _authenticationManager = authenticationManager;
            _creditsQuery = creditsQuery;
            _employerCreditsQuery = employerCreditsQuery;
            _jobPostersQuery = jobPostersQuery;
            _nonMemberSettingsQuery = nonMemberSettingsQuery;
            _settingsQuery = settingsQuery;
            _linkedInQuery = linkedInQuery;
            _linkedInCommand = linkedInCommand;
            _nonMemberSettingsCommand = nonMemberSettingsCommand;
            _jobPostersCommand = jobPostersCommand;
            _settingsCommand = settingsCommand;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _industriesQuery = industriesQuery;
            _phoneNumbersQuery = phoneNumbersQuery;
            _organisationsCommand = organisationsCommand;
            _organisationsQuery = organisationsQuery;
        }

        public ActionResult Settings()
        {
            var employer = CurrentEmployer;

            var credentials = _loginCredentialsQuery.GetCredentials(employer.Id);

            var jobPoster = _jobPostersQuery.GetJobPoster(employer.Id) ?? new JobPoster { Id = employer.Id };
            var nonMemberSettings = _nonMemberSettingsQuery.GetNonMemberSettings(employer.EmailAddress.Address);

            var settings = _settingsQuery.GetSettings(employer.Id);
            var employerUpdateCategory = (from c in _settingsQuery.GetCategories(UserType.Employer) where c.Name == "EmployerUpdate" select c).Single();
            var campaignCategory = (from c in _settingsQuery.GetCategories(UserType.Employer) where c.Name == "Campaign" select c).Single();

            var model = new SettingsModel
            {
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                OrganisationName = employer.Organisation.Name,
                CanEditOrganisationName = !employer.Organisation.IsVerified,
                Role = employer.SubRole,
                JobTitle = employer.JobTitle,
                EmailAddress = employer.EmailAddress.Address,
                PhoneNumber = employer.PhoneNumber == null ? null : employer.PhoneNumber.Number,
                IndustryIds = employer.Industries == null ? new List<Guid>() : employer.Industries.Select(i => i.Id).ToList(),
                Industries = _industriesQuery.GetIndustries(),
                HasLoginCredentials = credentials != null,
                LoginId = credentials == null ? null : credentials.LoginId,
                UseLinkedInProfile = _linkedInQuery.GetProfile(employer.Id) != null,
                Allocations = GetAllocations(employer),
                ShowSuggestedCandidates = jobPoster.ShowSuggestedCandidates,
                SendSuggestedCandidates = jobPoster.SendSuggestedCandidates,
                ReceiveSuggestedCandidates = nonMemberSettings == null || !nonMemberSettings.SuppressSuggestedCandidatesEmails,
                EmailEmployerUpdate = EmailCategory(employerUpdateCategory, settings),
                EmailCampaign = EmailCategory(campaignCategory, settings),
            };

            return View(model);
        }

        [HttpPost, ButtonClicked("Save")]
        public ActionResult Settings(SettingsModel settings)
        {
            var employer = CurrentEmployer;
            settings.CanEditOrganisationName = !employer.Organisation.IsVerified;
            settings.Industries = _industriesQuery.GetIndustries();
            settings.Allocations = GetAllocations(employer);
            var credentials = _loginCredentialsQuery.GetCredentials(employer.Id);
            settings.HasLoginCredentials = credentials != null;

            try
            {
                // Validate before changing.

                settings.Prepare();
                settings.Validate();

                // Update the credentials.

                UpdateCredentials(employer.Id, credentials, settings.LoginId, settings.Password, settings.ConfirmPassword, settings.UseLinkedInProfile);
                
                // Update.

                employer.FirstName = settings.FirstName;
                employer.LastName = settings.LastName;
                employer.PhoneNumber = _phoneNumbersQuery.GetPhoneNumber(settings.PhoneNumber, ActivityContext.Current.Location.Country);
                employer.Industries = settings.IndustryIds == null
                    ? null
                    : (from i in settings.IndustryIds select _industriesQuery.GetIndustry(i)).ToList();
                employer.SubRole = settings.Role;
                employer.JobTitle = settings.JobTitle;

                _employerAccountsCommand.UpdateEmployer(employer);

                // Update the organisation.

                if (settings.CanEditOrganisationName)
                    UpdateOrganisation(employer, settings.OrganisationName);

                // Suggested candidates.

                UpdateSuggestedCandidates(employer, settings.ShowSuggestedCandidates, settings.SendSuggestedCandidates, settings.ReceiveSuggestedCandidates);

                // Communications.

                UpdateCommunicationSettings(employer.Id, settings.EmailEmployerUpdate, settings.EmailCampaign);

                // Reset the display name cached in the authentication details, in case the user updated any details that affect it.

                _authenticationManager.UpdateUser(HttpContext, employer, false);

                return RedirectToReturnUrlWithConfirmation("Your changes have been saved.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(settings);
        }

        [HttpPost, ActionName("Settings"), ButtonClicked("Cancel")]
        public ActionResult CancelSettings()
        {
            return RedirectToReturnUrl();
        }

        private IDictionary<Credit, Allocation> GetAllocations(IEmployer employer)
        {
            return new Dictionary<Credit, Allocation>
            {
                { _creditsQuery.GetCredit<ContactCredit>(), _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer) },
                { _creditsQuery.GetCredit<ApplicantCredit>(), _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer) },
                { _creditsQuery.GetCredit<JobAdCredit>(), _employerCreditsQuery.GetEffectiveActiveAllocation<JobAdCredit>(employer) },
            };
        }

        private static bool EmailCategory(Category category, RecipientSettings settings)
        {
            var frequency = (settings == null
                ? null
                : (from s in settings.CategorySettings where s.CategoryId == category.Id select s.Frequency).SingleOrDefault()) ?? category.DefaultFrequency;
            return frequency != Frequency.Never;
        }

        private void UpdateCredentials(Guid employerId, LoginCredentials credentials, string loginId, string password, string confirmPassword, bool useLinkedInProfile)
        {
            if (credentials == null)
            {
                if (!string.IsNullOrEmpty(loginId) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(confirmPassword))
                {
                    // No existing credentials but trying to create some.

                    var credentialsModel = new LoginCredentialsModel { LoginId = loginId, Password = password, ConfirmPassword = confirmPassword };
                    credentialsModel.Validate();

                    _loginCredentialsCommand.CreateCredentials(employerId, new LoginCredentials { LoginId = loginId, PasswordHash = LoginCredentials.HashToString(password) });
                }
            }
            else
            {
                if (loginId != credentials.LoginId)
                {
                    // Cannot remove the login id.

                    if (string.IsNullOrEmpty(loginId))
                        throw new ValidationErrorsException(new RequiredValidationError("LoginId"));

                    // Check not trying to someone else's login id.

                    if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = loginId }))
                        throw new DuplicateUserException();

                    // Update the credentials.

                    credentials.LoginId = loginId;
                    _loginCredentialsCommand.UpdateCredentials(employerId, credentials, employerId);
                }

                // If not wanting to use LinkedIn any more then remove the profile.

                if (!useLinkedInProfile)
                    _linkedInCommand.DeleteProfile(employerId);
            }
        }

        private void UpdateOrganisation(IEmployer employer, string organisationName)
        {
            if (organisationName != employer.Organisation.Name)
            {
                var organisation = _organisationsQuery.GetOrganisation(employer.Organisation.Id);
                organisation.Name = organisationName;
                _organisationsCommand.UpdateOrganisation(organisation);
            }
        }

        private void UpdateSuggestedCandidates(IEmployer employer, bool showSuggestedCandidates, bool sendSuggestedCandidates, bool receiveSuggestedCandidates)
        {
            // Update the job poster.

            var jobPoster = _jobPostersQuery.GetJobPoster(employer.Id) ?? new JobPoster { Id = employer.Id };

            if (showSuggestedCandidates != jobPoster.ShowSuggestedCandidates || sendSuggestedCandidates != jobPoster.SendSuggestedCandidates)
            {
                jobPoster.ShowSuggestedCandidates = showSuggestedCandidates;
                jobPoster.SendSuggestedCandidates = sendSuggestedCandidates;
                _jobPostersCommand.UpdateJobPoster(jobPoster);
            }

            // Update the non-member settings.

            var nonMemberSettings = _nonMemberSettingsQuery.GetNonMemberSettings(employer.EmailAddress.Address);
            if (nonMemberSettings != null)
            {
                if (receiveSuggestedCandidates == nonMemberSettings.SuppressSuggestedCandidatesEmails)
                {
                    nonMemberSettings.SuppressSuggestedCandidatesEmails = !receiveSuggestedCandidates;
                    _nonMemberSettingsCommand.UpdateNonMemberSettings(nonMemberSettings);
                }
            }
            else if (!receiveSuggestedCandidates)
            {
                // !Suppress is the default state, so if there are no settings that's fine.

                nonMemberSettings = new NonMemberSettings
                {
                    EmailAddress = employer.EmailAddress.Address,
                    SuppressSuggestedCandidatesEmails = true
                };
                _nonMemberSettingsCommand.CreateNonMemberSettings(nonMemberSettings);
            }
        }

        private void UpdateCommunicationSettings(Guid employerId, bool emailEmployerUpdate, bool emailCampaign)
        {
            var settings = _settingsQuery.GetSettings(employerId);

            var category = (from c in _settingsQuery.GetCategories(UserType.Employer) where c.Name == "EmployerUpdate" select c).Single();
            if (emailEmployerUpdate != EmailCategory(category, settings))
                _settingsCommand.SetFrequency(employerId, category.Id, emailEmployerUpdate ? Frequency.Immediately : Frequency.Never);

            category = (from c in _settingsQuery.GetCategories(UserType.Employer) where c.Name == "Campaign" select c).Single();
            if (emailCampaign != EmailCategory(category, settings))
                _settingsCommand.SetFrequency(employerId, category.Id, emailCampaign ? Frequency.Immediately : Frequency.Never);
        }
    }
}