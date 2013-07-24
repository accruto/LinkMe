using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.Employers;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Administrators.Models.Campaigns;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Campaigns
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class CampaignsController
        : AdministratorsController
    {
        private const int MaxPerPage = 20;

        private readonly ICampaignsCommand _campaignsCommand;
        private readonly ICampaignsQuery _campaignsQuery;
        private readonly ICampaignEmailsCommand _campaignEmailsCommand;
        private readonly ICampaignCriteriaCommand _campaignCriteriaCommand;
        private readonly ISettingsQuery _settingsQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IAdministratorsQuery _administratorsQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly ILocationQuery _locationQuery;

        public CampaignsController(ICampaignsCommand campaignsCommand, ICampaignsQuery campaignsQuery, ICampaignEmailsCommand campaignEmailsCommand, ICampaignCriteriaCommand campaignCriteriaCommand, ISettingsQuery settingsQuery, IIndustriesQuery industriesQuery, ICommunitiesQuery communitiesQuery, IAdministratorsQuery administratorsQuery, ILoginCredentialsQuery loginCredentialsQuery, IEmployersQuery employersQuery, IMembersQuery membersQuery, ILocationQuery locationQuery)
        {
            _campaignsCommand = campaignsCommand;
            _campaignsQuery = campaignsQuery;
            _campaignEmailsCommand = campaignEmailsCommand;
            _campaignCriteriaCommand = campaignCriteriaCommand;
            _settingsQuery = settingsQuery;
            _industriesQuery = industriesQuery;
            _communitiesQuery = communitiesQuery;
            _administratorsQuery = administratorsQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
            _employersQuery = employersQuery;
            _membersQuery = membersQuery;
            _locationQuery = locationQuery;
        }

        public ActionResult Index()
        {
            return Index(null, null);
        }

        public ActionResult PagedIndex(int page)
        {
            return Index(null, page);
        }

        public ActionResult CategoryIndex(CampaignCategory category)
        {
            return Index(category, null);
        }

        public ActionResult PagedCategoryIndex(CampaignCategory category, int page)
        {
            return Index(category, page);
        }

        public ActionResult New()
        {
            // Show a new campaign with the default communication category set.

            var campaign = new Campaign
            {
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
            };

            return View(new CampaignSummaryModel { Campaign = campaign, CreatedBy = CurrentRegisteredUser, IsReadOnly = false, CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult New([Bind(Include = "Name,Category,CommunicationDefinitionId,CommunicationCategoryId,Query")] Campaign campaign)
        {
            try
            {
                // Use the current user.

                campaign.CreatedBy = User.Id().Value;

                // Create a new campaign.

                _campaignsCommand.CreateCampaign(campaign);

                // Move to the next page.

                return RedirectToRoute(CampaignsRoutes.EditCriteria, new { campaign.Id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(new CampaignSummaryModel { Campaign = campaign, CreatedBy = CurrentRegisteredUser, IsReadOnly = false, CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() });
        }

        public ActionResult Edit(Guid id)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);
            
            var createdBy = _administratorsQuery.GetAdministrator(campaign.CreatedBy);
            return View(new CampaignSummaryModel { Campaign = campaign, CreatedBy = createdBy, IsReadOnly = IsReadOnly(campaign), CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() });
        }

        [HttpPost, ActionName("Edit"), ValidateInput(false)]
        public ActionResult PostEdit(Guid id, string name, CampaignCategory category, Guid? communicationDefinitionId, Guid? communicationCategoryId, string query)
        {
            // Get the campaign to update.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);
            var createdBy = _administratorsQuery.GetAdministrator(campaign.CreatedBy);

            try
            {
                // Update it with the new values.

                campaign.Name = name;
                campaign.Category = category;
                campaign.CommunicationDefinitionId = communicationDefinitionId;
                campaign.CommunicationCategoryId = communicationCategoryId;
                campaign.Query = query.NullIfEmpty();
                _campaignsCommand.UpdateCampaign(campaign);

                return RedirectToRoute(CampaignsRoutes.Edit, new { id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(new CampaignSummaryModel { Campaign = campaign, CreatedBy = createdBy, IsReadOnly = false, CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() });
        }

        public ActionResult Activate(Guid id)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            // Update its status.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            return View("Status", campaign);
        }

        public ActionResult Stop(Guid id)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            // Update its status.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Stopped);
            return View("Status", campaign);
        }

        public ActionResult Delete(Guid id, int? page)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var createdBy = _administratorsQuery.GetAdministrator(campaign.CreatedBy);
            return View(new CampaignDeleteSummaryModel(new Pagination { Page = page }, new CampaignSummaryModel { Campaign = campaign, CreatedBy = createdBy, IsReadOnly = false, CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() }));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult PostDelete(Guid id, int? page)
        {
            // Get the campaign to update.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            try
            {
                // Update its status.

                _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Deleted);

                return page == null ? RedirectToRoute(CampaignsRoutes.Index) : RedirectToRoute(CampaignsRoutes.PagedIndex, new { page });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(campaign);
        }

        public ActionResult EditCriteria(Guid id)
        {
            // Get the campaign and criteria.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var criteria = _campaignsQuery.GetCriteria(id);
            if (criteria == null)
                return NotFound("criteria", "id", id);

            return View(CreateCampaignCriteria(campaign, criteria));
        }

        [HttpPost, ActionName("EditCriteria")]
        public ActionResult PostEditCriteria(Guid id, OrganisationEmployerSearchCriteria employerCriteria, MemberSearchCriteria memberCriteria)
        {
            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var criteria = campaign.Category == CampaignCategory.Employer ? (Criteria) employerCriteria : memberCriteria;
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);

            return RedirectToRoute(CampaignsRoutes.EditCriteria, new { id });
        }

        public ActionResult EditTemplate(Guid id)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var template = _campaignsQuery.GetTemplate(id) ?? new Template();

            var createdBy = _administratorsQuery.GetAdministrator(campaign.CreatedBy);
            return View(new CampaignSummaryModel { Campaign = campaign, Template = template, CreatedBy = createdBy, IsReadOnly = IsReadOnly(campaign), CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() });
        }

        [HttpPost, ActionName("EditTemplate"), ValidateInput(false)]
        public ActionResult PostEditTemplate(Guid id, string subject, string body)
        {
            // Check that the campaign exists.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            Template template = null;
            try
            {
                template = _campaignsQuery.GetTemplate(id);
                if (template == null)
                {
                    template = new Template
                    {
                        Subject = subject,
                        Body = body,
                    };
                    _campaignsCommand.CreateTemplate(id, template);
                }
                else
                {
                    // Update it with the new values.

                    template.Subject = subject;
                    template.Body = body;
                    _campaignsCommand.UpdateTemplate(id, template);
                }

                return RedirectToRoute(CampaignsRoutes.EditTemplate, new { id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(template);
        }

        public ActionResult Search(Guid id)
        {
            // Get the campaign and criteria.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var criteria = _campaignsQuery.GetCriteria(id);
            if (criteria == null)
                return NotFound("criteria", "id", id);

            var results = _campaignCriteriaCommand.Match(campaign.Category, criteria);
            return View(CreateCampaignCriteriaResults(campaign, criteria, results));
        }

        public ActionResult Preview(Guid id)
        {
            // Get the campaign to preview.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var email = _campaignEmailsCommand.CreateEmail(campaign, CreateUser(campaign.Category, "test@test.linkme.net.au"));
            var item = _campaignEmailsCommand.GeneratePreview(email, id);

            return View("Raw", item.Views[0].Body);
        }

        [HttpPost]
        public ActionResult Send(Guid id, string emailAddresses, string loginIds)
        {
            // Get the campaign to send.

            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            var template = _campaignsQuery.GetTemplate(id) ?? new Template();

            try
            {
                if (string.IsNullOrEmpty(emailAddresses) && string.IsNullOrEmpty(loginIds))
                    throw new ValidationErrorsException(new RequiredValidationError("email address list or login id list"));

                if (!string.IsNullOrEmpty(emailAddresses))
                    SendToEmailAddresses(id, campaign, emailAddresses);
                if (!string.IsNullOrEmpty(loginIds))
                    SendToLoginIds(id, campaign, loginIds);

                return RedirectToRoute(CampaignsRoutes.EditTemplate, new { id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            var createdBy = _administratorsQuery.GetAdministrator(campaign.CreatedBy);
            return View("EditTemplate", new CampaignSummaryModel { Campaign = campaign, Template = template, CreatedBy = createdBy, IsReadOnly = IsReadOnly(campaign), CommunicationDefinitions = _settingsQuery.GetDefinitions(), CommunicationCategories = _settingsQuery.GetCategories() });
        }

        public ActionResult Report(Guid id)
        {
            var campaign = _campaignsQuery.GetCampaign(id);
            if (campaign == null)
                return NotFound("campaign", "id", id);

            return View(campaign);
        }

        private void SendToLoginIds(Guid id, Campaign campaign, string loginIds)
        {
            var emails = campaign.Category == CampaignCategory.Employer ? GetEmployerEmails(campaign, loginIds) : GetMemberEmails(campaign, loginIds);
            _campaignEmailsCommand.Send(emails, id, true);
        }

        private IList<CampaignEmail> GetMemberEmails(Campaign campaign, string loginIds)
        {
            var emails = new List<CampaignEmail>();
            foreach (var loginId in loginIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var id = _loginCredentialsQuery.GetUserId(loginId);
                if (id == null)
                    throw new NotFoundException("loginIds");
                var member = _membersQuery.GetMember(id.Value);
                if (member == null)
                    throw new NotFoundException("loginIds");

                emails.Add(_campaignEmailsCommand.CreateEmail(campaign, member));
            }

            return emails;
        }

        private IList<CampaignEmail> GetEmployerEmails(Campaign campaign, string loginIds)
        {
            var emails = new List<CampaignEmail>();
            foreach (var loginId in loginIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var id = _loginCredentialsQuery.GetUserId(loginId);
                if (id == null)
                    throw new NotFoundException("loginIds");
                var employer = _employersQuery.GetEmployer(id.Value);
                if (employer == null)
                    throw new NotFoundException("loginIds");

                emails.Add(_campaignEmailsCommand.CreateEmail(campaign, employer));
            }

            return emails;
        }

        private void SendToEmailAddresses(Guid id, Campaign campaign, string emailAddresses)
        {
            IValidator validator = EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.MultipleEmails, false);
            var errors = validator.IsValid(emailAddresses)
                ? null
                : validator.GetValidationErrors("EmailAddresses");

            if (errors != null && errors.Length > 0)
                throw new ValidationErrorsException(errors);

            var emails = from a in emailAddresses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                         select _campaignEmailsCommand.CreateEmail(campaign, CreateUser(campaign.Category, a));
            _campaignEmailsCommand.Send(emails, id, true);
        }

        private ICommunicationUser CreateUser(CampaignCategory category, string emailAddress)
        {
            if (category == CampaignCategory.Member)
                return _membersQuery.GetMember(emailAddress);
            var employers = _employersQuery.GetEmployers(emailAddress);
            return employers.Count == 0 ? null : employers[0];
        }

        private ActionResult Index(CampaignCategory? category, int? page)
        {
            // Ask for the appropriate range of campaigns for this page.

            var currentPage = page ?? 1;
            var start = (currentPage - 1) * MaxPerPage;
            var result = _campaignsQuery.GetCampaigns(category, new Range(start, MaxPerPage));

            // Convert into records for viewing.

            var records = from i in result.RangeItems
                          select new CampaignRecord
                          {
                              Id = i.Id,
                              Name = i.Name,
                              Subject = (_campaignsQuery.GetTemplate(i.Id) ?? new Template()).Subject,
                              Status = i.Status,
                          };

            // If there are no records for this page, then redirect to the previous page.

            if (!records.Any())
            {
                if (page != null)
                {
                    if (page > 1)
                        return category == null ? RedirectToRoute(CampaignsRoutes.PagedIndex, new { page = page - 1 }) : RedirectToRoute(CampaignsRoutes.PagedCategoryIndex, new { page = page - 1, category });
                    return category == null ? RedirectToRoute(CampaignsRoutes.Index) : RedirectToRoute(CampaignsRoutes.CategoryIndex, new { category });
                }
            }

            foreach (var record in records)
                record.Page = currentPage;
            return View("Index", new PaginatedList<CampaignRecord>(MaxPerPage, result.TotalItems, currentPage, records));
        }

        private static bool IsReadOnly(Campaign campaign)
        {
            return !(campaign.Status == CampaignStatus.Draft || campaign.Status == CampaignStatus.Stopped);
        }

        private CampaignCriteriaModel CreateCampaignCriteria(Campaign campaign, Criteria criteria)
        {
            // Depends on criteria type.

            if (criteria is EmployerSearchCriteria)
                return new CampaignEmployerCriteriaModel(campaign, (EmployerSearchCriteria)criteria, IsReadOnly(campaign), _industriesQuery.GetIndustries());
            return new CampaignMemberCriteriaModel(campaign, (MemberSearchCriteria)criteria, IsReadOnly(campaign), _industriesQuery.GetIndustries(), _communitiesQuery.GetCommunities(), _locationQuery.GetCountries());
        }

        private CampaignCriteriaResultsModel CreateCampaignCriteriaResults(Campaign campaign, Criteria criteria, IList<RegisteredUser> users)
        {
            return new CampaignCriteriaResultsModel(CreateCampaignCriteria(campaign, criteria), users);
        }
    }
}