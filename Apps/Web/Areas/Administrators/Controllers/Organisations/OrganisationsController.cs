using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.Employers.Files;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.PhoneNumbers.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Search.Recruiters;
using LinkMe.Web.Areas.Administrators.Models.Organisations;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Organisations
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class OrganisationsController
        : AdministratorsController
    {
        private readonly IOrganisationsCommand _organisationsCommand;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IAdministratorsQuery _administratorsQuery;
        private readonly IEmployerAccountsCommand _employerAccountsCommand;
        private readonly IEmployersQuery _employersQuery;
        private readonly IExecuteOrganisationSearchCommand _executeOrganisationSearchCommand;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IPhoneNumbersQuery _phoneNumbersQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly IEmailsCommand _emailsCommand;
        private readonly IAccountReportsQuery _accountReportsQuery;

        private static readonly string[] CommunicationCategories = new[] { "EmployerUpdate", "Campaign" };

        public OrganisationsController(IOrganisationsCommand organisationsCommand, IOrganisationsQuery organisationsQuery, ILoginCredentialsQuery loginCredentialsQuery, IAdministratorsQuery administratorsQuery, IEmployerAccountsCommand employerAccountsCommand, IEmployersQuery employersQuery, IExecuteOrganisationSearchCommand executeOrganisationSearchCommand, ICommunitiesQuery communitiesQuery, IIndustriesQuery industriesQuery, IPhoneNumbersQuery phoneNumbersQuery, ILocationQuery locationQuery, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand, IEmailsCommand emailsCommand, IAccountReportsQuery accountReportsQuery)
        {
            _organisationsCommand = organisationsCommand;
            _organisationsQuery = organisationsQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
            _administratorsQuery = administratorsQuery;
            _employerAccountsCommand = employerAccountsCommand;
            _employersQuery = employersQuery;
            _executeOrganisationSearchCommand = executeOrganisationSearchCommand;
            _communitiesQuery = communitiesQuery;
            _industriesQuery = industriesQuery;
            _phoneNumbersQuery = phoneNumbersQuery;
            _locationQuery = locationQuery;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
            _emailsCommand = emailsCommand;
            _accountReportsQuery = accountReportsQuery;
        }

        public ActionResult Search()
        {
            var accountManagers = _administratorsQuery.GetAdministrators();
            return View(new OrganisationSearchModel
            {
                Criteria = new OrganisationSearchCriteria {VerifiedOrganisations = true},
                AccountManagers = accountManagers,
                Organisations = null
            });
        }

        [HttpPost]
        public ActionResult Search(OrganisationSearchCriteria criteria, [Bind(Include = "VerifiedOrganisations")] CheckBoxValue verifiedOrganisations, [Bind(Include = "UnverifiedOrganisations")] CheckBoxValue unverifiedOrganisations)
        {
            var accountManagers = _administratorsQuery.GetAdministrators();

            criteria.VerifiedOrganisations = verifiedOrganisations.IsChecked;
            criteria.UnverifiedOrganisations = unverifiedOrganisations.IsChecked;
            var organisations = _executeOrganisationSearchCommand.Search(criteria);

            return View(new OrganisationSearchModel
                            {
                                Criteria = criteria,
                                AccountManagers = accountManagers,
                                Organisations = organisations,
                            });
        }

        public ActionResult New()
        {
            var accountManagers = GetAccountManagers(null);

            return View(new OrganisationModel
                            {
                                Organisation = new VerifiedOrganisation { AccountManagerId = CurrentAdministrator.Id, VerifiedById = CurrentAdministrator.Id },
                                AccountManagers = accountManagers,
                                VerifiedByAccountManager = CurrentAdministrator,
                                Communities = _communitiesQuery.GetCommunities(),
                            });
        }

        [HttpPost, ActionName("New"), ButtonClicked("Cancel")]
        public ActionResult CancelNew()
        {
            return RedirectToRoute(HomeRoutes.Home);
        }

        [HttpPost, ButtonClicked("Create")]
        public ActionResult New([Bind(Include = "ParentFullName")] string parentFullName, [Bind(Include = "Name")] string name, [Bind(Include = "Location")] string location, [Bind(Include = "AccountManagerId")] Guid accountManagerId, [Bind(Include = "FirstName,LastName,EmailAddress")] ContactDetails contactDetails, [Bind(Include = "CommunityId")] Guid? communityId)
        {
            // Can only create a verified organisation.

            contactDetails.ParseEmailAddresses(contactDetails.EmailAddress);
            var organisation = new VerifiedOrganisation
            {
                Name = name,
                Address = GetAddress(location),
                AccountManagerId = accountManagerId,
                VerifiedById = CurrentAdministrator.Id,
                ContactDetails = contactDetails,
                AffiliateId = communityId,
            };

            try
            {
                organisation.SetParent(GetParentOrganisation(parentFullName));

                // Create the organisation itself.

                _organisationsCommand.CreateOrganisation(organisation);

                return RedirectToRouteWithConfirmation(OrganisationsRoutes.Edit, new { organisation.Id }, HttpUtility.HtmlEncode("The '" + organisation.FullName + "' organisation has been created."));
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the errors.

            var accountManagers = GetAccountManagers(organisation);

            return View(new OrganisationModel
                            {
                                Organisation = organisation,
                                AccountManagers = accountManagers,
                                VerifiedByAccountManager = GetVerifiedByAccountManager(organisation, accountManagers),
                                Communities = _communitiesQuery.GetCommunities(),
                            });
        }

        public ActionResult Edit(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var accountManagers = GetAccountManagers(organisation);

            return View(new OrganisationModel
                            {
                                Organisation = organisation,
                                OrganisationHierarchy = _organisationsQuery.GetOrganisationHierarchy(id),
                                AccountManagers = accountManagers,
                                VerifiedByAccountManager = GetVerifiedByAccountManager(organisation, accountManagers),
                                Communities = _communitiesQuery.GetCommunities(),
                            });
        }

        [HttpPost, ActionName("Edit"), ButtonClicked("Verify")]
        public ActionResult Verify(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            try
            {
                // Use the current user as the account manager and verifier.

                var userId = User.Id().Value;
                _organisationsCommand.VerifyOrganisation(organisation, userId, userId);

                return RedirectToRoute(OrganisationsRoutes.Edit, new { id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the errors.

            var accountManagers = GetAccountManagers(organisation);

            return View("Edit", new OrganisationModel
                                    {
                                        Organisation = organisation,
                                        OrganisationHierarchy = _organisationsQuery.GetOrganisationHierarchy(id),
                                        AccountManagers = accountManagers,
                                        VerifiedByAccountManager = GetVerifiedByAccountManager(organisation, accountManagers),
                                        Communities = _communitiesQuery.GetCommunities(),
                                    });
        }

        [HttpPost, ButtonClicked("Save")]
        public ActionResult Edit(Guid id, [Bind(Include="ParentFullName")] string parentFullName, [Bind(Include="Name")] string name, [Bind(Include="Location")] string location, [Bind(Include="AccountManagerId")] Guid? accountManagerId, [Bind(Include="FirstName,LastName,EmailAddress")] ContactDetails contactDetails, [Bind(Include = "CommunityId")] Guid? communityId)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            try
            {
                organisation.Name = name;
                organisation.Address = GetAddress(location);
                organisation.AffiliateId = communityId;

                contactDetails.ParseEmailAddresses(contactDetails.EmailAddress);
                if (organisation.IsVerified)
                {
                    var verifiedOrganisation = (VerifiedOrganisation) organisation;
                    verifiedOrganisation.AccountManagerId = accountManagerId.Value;
                    verifiedOrganisation.ContactDetails = contactDetails;
                    verifiedOrganisation.SetParent(GetParentOrganisation(parentFullName));
                }

                // Update the organisation.

                _organisationsCommand.UpdateOrganisation(organisation);

                return RedirectToRoute(OrganisationsRoutes.Edit, new { id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the errors.

            var accountManagers = GetAccountManagers(organisation);

            return View(new OrganisationModel
                            {
                                Organisation = organisation,
                                OrganisationHierarchy = _organisationsQuery.GetOrganisationHierarchy(id),
                                AccountManagers = accountManagers,
                                VerifiedByAccountManager = GetVerifiedByAccountManager(organisation, accountManagers),
                                Communities = _communitiesQuery.GetCommunities(),
                            });
        }

        public ActionResult Employers(Guid id, [Bind(Include = "IncludeChildOrganisations")] CheckBoxValue includeChildOrganisations)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var employers = GetEmployers(id, includeChildOrganisations);

            return View(new EmployersModel
                            {
                                Organisation = organisation,
                                IncludeChildOrganisations = includeChildOrganisations != null && includeChildOrganisations.IsChecked,
                                Employers = employers
                            });
        }

        [HttpPost, ActionName("Employers"), ButtonClicked("download")]
        public ActionResult Download(Guid id, [Bind(Include = "IncludeChildOrganisations")] CheckBoxValue includeChildOrganisations)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            // Get the employers.

            var employers = GetEmployers(id, includeChildOrganisations);

            // Login ids.

            var loginIds = _loginCredentialsQuery.GetLoginIds(from e in employers select e.Id);

            // Write them out.

            var fileStream = (from e in employers
            select new Tuple<IEmployer, string>(
                e,
                loginIds.ContainsKey(e.Id) ? loginIds[e.Id] : null)).ToFileStream();

            var fileName = FileSystem.GetValidFileName("LinkMeSearchResults.csv");
            return new FileStreamResult(fileStream, MediaType.Csv) { FileDownloadName = fileName };
        }

        public ActionResult NewEmployer(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            // Can only create logins for verified organisations.

            if (!organisation.IsVerified)
            {
                ModelState.AddModelError(string.Empty, "Cannot create logins for unverified organisations.");
                return View(new NewEmployerModel { Organisation = organisation });
            }

            return View(new NewEmployerModel
            {
                Organisation = organisation,
                Employers = _employersQuery.GetOrganisationEmployers(organisation.Id),
                Employer = new CreateEmployerModel { Industries = _industriesQuery.GetIndustries(), SubRole = Defaults.SubRole },
            });
        }

        [HttpPost, ActionName("NewEmployer"), ButtonClicked("Create")]
        public ActionResult NewEmployer(Guid id, [Bind(Include = "LoginId,Password,EmailAddress,FirstName,LastName,PhoneNumber,JobTitle,SubRole,IndustryId")] CreateEmployerModel login)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            // Can only create logins for verified organisations.

            if (!organisation.IsVerified)
            {
                ModelState.AddModelError(string.Empty, "Cannot create logins for unverified organisations.");
                return View(new NewEmployerModel { Organisation = organisation });
            }

            if (login == null)
                login = new CreateEmployerModel();

            try
            {
                // Look for errors.

                login.Validate();

                // Create the login.

                CreateEmployer(organisation, login);

                // Get ready to create another.

                return RedirectToRouteWithConfirmation(OrganisationsRoutes.NewEmployer, new { id }, HttpUtility.HtmlEncode("The account for " + login.FirstName + " " + login.LastName + " has been created."));
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            login.Industries = _industriesQuery.GetIndustries();

            return View(new NewEmployerModel
                            {
                                Organisation = organisation,
                                Employers = _employersQuery.GetOrganisationEmployers(organisation.Id),
                                Employer = login
                            });
        }

        [HttpPost, ActionName("NewEmployer"), ButtonClicked("Cancel")]
        public ActionResult CancelNewEmployer(Guid id)
        {
            return RedirectToRoute(OrganisationsRoutes.Employers, new { id });
        }

        public ActionResult Communications(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var settings = _settingsQuery.GetSettings(organisation.Id);
            var categories = _settingsQuery.GetCategories(UserType.Employer);
            return View(GetCommunicationsModel(organisation, settings, categories));
        }

        [HttpPost, ActionName("Communications")]
        public ActionResult PostCommunications(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var settings = _settingsQuery.GetSettings(id);
            var categories = _settingsQuery.GetCategories(UserType.Employer);

            try
            {
                // Dynamically determine what has been changed.

                foreach (var category in (from c in categories where c.Timing == Timing.Periodic select c))
                {
                    var frequency = GetValue<Frequency>(category.Name);

                    // If the value has changed then update the database.

                    var currentFrequency = GetFrequency(category.Id, settings);
                    if (currentFrequency != frequency)
                        _settingsCommand.SetFrequency(id, category.Id, frequency);
                }

                foreach (var category in (from c in categories where c.Timing == Timing.Notification select c))
                {
                    var frequency = IsChecked(category.Name) ? Frequency.Immediately : Frequency.Never;

                    // If the value has changed then update the database.

                    var currentFrequency = GetFrequency(category.Id, settings);
                    if (currentFrequency != frequency)
                        _settingsCommand.SetFrequency(id, category.Id, frequency);
                }

                return RedirectToRouteWithConfirmation(OrganisationsRoutes.Communications, new { id }, "Your changes have been saved.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(GetCommunicationsModel(organisation, settings, categories));
        }

        private static CommunicationsModel GetCommunicationsModel(Organisation organisation, RecipientSettings settings, IEnumerable<Category> categories)
        {
            return new CommunicationsModel
            {
                Organisation = organisation,
                Categories = (from c in categories
                              where CommunicationCategories.Contains(c.Name)
                              select new Tuple<Category, Frequency?>(c, GetFrequency(c.Id, settings))).ToList(),
            };
        }

        private Address GetAddress(string location)
        {
            return string.IsNullOrEmpty(location)
                ? null
                : new Address { Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, location) };
        }

        private static Frequency? GetFrequency(Guid categoryId, RecipientSettings settings)
        {
            return settings == null
                ? null
                : (from s in settings.CategorySettings
                   where s.CategoryId == categoryId
                   select s.Frequency).SingleOrDefault();
        }

        private Organisation GetParentOrganisation(string parentFullName)
        {
            if (string.IsNullOrEmpty(parentFullName))
                return null;

            var parent = _organisationsQuery.GetVerifiedOrganisation(parentFullName);
            if (parent == null)
                throw new NotFoundException("ParentOrganisation");
            return parent;
        }

        private Administrator GetVerifiedByAccountManager(Organisation organisation, IEnumerable<Administrator> accountManagers)
        {
            Administrator accountManager = null;

            if (organisation.IsVerified)
            {
                var verifiedOrganisation = (VerifiedOrganisation)organisation;

                // Look for it in the account manager list.

                accountManager = (from a in accountManagers where a.Id == verifiedOrganisation.VerifiedById select a).SingleOrDefault()
                    ?? _administratorsQuery.GetAdministrator(verifiedOrganisation.VerifiedById);
            }

            return accountManager;
        }

        private IList<Administrator> GetAccountManagers(Organisation organisation)
        {
            var accountManagers = _administratorsQuery.GetAdministrators(true).ToList();

            // Make sure that the account managers for this particular organisation are included even if they are disabled.

            if (organisation != null && organisation.IsVerified)
            {
                var verifiedOrganisation = (VerifiedOrganisation) organisation;
                if (!(from a in accountManagers where a.Id == verifiedOrganisation.AccountManagerId select a).Any())
                {
                    var accountManager = _administratorsQuery.GetAdministrator(verifiedOrganisation.AccountManagerId);
                    if (accountManager != null)
                        accountManagers.Insert(0, accountManager);
                }
            }

            return accountManagers;
        }

        private void CreateEmployer(IOrganisation organisation, CreateEmployerModel model)
        {
            var employer = new Employer
            {
                Organisation = organisation,
                SubRole = model.SubRole,
                EmailAddress = new EmailAddress { Address = model.EmailAddress, IsVerified = true },
                FirstName = model.FirstName,
                LastName = model.LastName,
                JobTitle = model.JobTitle,
                PhoneNumber = _phoneNumbersQuery.GetPhoneNumber(model.PhoneNumber, ActivityContext.Location.Country),
            };

            if (model.IndustryId != null)
                employer.Industries = new List<Industry> {_industriesQuery.GetIndustry(model.IndustryId.Value)};

            // Create the account, where the password must be changed at next login.

            var credentials = new LoginCredentials
            {
                LoginId = model.LoginId,
                Password = model.Password,
                PasswordHash = LoginCredentials.HashToString(model.Password),
                MustChangePassword = true,
            };

            _employerAccountsCommand.CreateEmployer(employer, credentials);

            var members = _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now);
            _emailsCommand.TrySend(new NewEmployerWelcomeEmail(employer, model.LoginId, model.Password, members));
        }

        private IList<Employer> GetEmployers(Guid organisationId, CheckBoxValue includeChildOrganisations)
        {
            IList<Employer> employers;
            if (includeChildOrganisations != null && includeChildOrganisations.IsChecked)
            {
                var organisationHierarchy = _organisationsQuery.GetOrganisationHierarchy(organisationId);
                var organisationsIds = GetOrganisationsIds(organisationId, organisationHierarchy);
                employers = _employersQuery.GetOrganisationEmployers(organisationsIds);
            }
            else
            {
                employers = _employersQuery.GetOrganisationEmployers(organisationId);
            }

            return (from e in employers
                    orderby e.Organisation.FullName , e.FullName
                    select e).ToList();
        }

        private static IEnumerable<Guid> GetOrganisationsIds(Guid organisationId, OrganisationHierarchy organisationHierarchy)
        {
            var organisationIds = new List<Guid>();
            GetOrganisationsIds(organisationIds, GetChildOrganisationHierarchy(organisationId, organisationHierarchy));
            return organisationIds;
        }

        private static OrganisationHierarchy GetChildOrganisationHierarchy(Guid organisationId, OrganisationHierarchy organisationHierarchy)
        {
            if (organisationHierarchy.Organisation.Id == organisationId)
                return organisationHierarchy;

            foreach (var child in organisationHierarchy.ChildOrganisationHierarchies)
            {
                var childOrganisationHierarchy = GetChildOrganisationHierarchy(organisationId, child);
                if (childOrganisationHierarchy != null)
                    return childOrganisationHierarchy;
            }

            return null;
        }

        private static void GetOrganisationsIds(ICollection<Guid> organisationIds, OrganisationHierarchy organisationHierarchy)
        {
            organisationIds.Add(organisationHierarchy.Organisation.Id);
            foreach (var child in organisationHierarchy.ChildOrganisationHierarchies)
                GetOrganisationsIds(organisationIds, child);
        }
    }
}