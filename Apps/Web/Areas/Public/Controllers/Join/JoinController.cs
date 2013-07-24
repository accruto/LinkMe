using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Pageflows;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Domain.Users.Members.Affiliations.Queries;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Areas.Public.Models.Join;
using LinkMe.Web.Areas.Public.Models.Logins;
using LinkMe.Web.Areas.Public.Routes;
using ValueProviderDictionary = LinkMe.Apps.Asp.Mvc.ValueProviderDictionary;

namespace LinkMe.Web.Areas.Public.Controllers.Join
{
    public class JoinController
        : PublicPageflowController<JoinPageflow>
    {
        private readonly IAccountsManager _accountsManager;
        private readonly IMemberAccountsCommand _memberAccountsCommand;
        private readonly IAccountVerificationsCommand _accountVerificationsCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly ICandidateResumesCommand _candidateResumesCommand;
        private readonly IResumesQuery _resumesQuery;
        private readonly IAffiliationItemsFactory _affiliationItemsFactory;
        private readonly IMemberAffiliationsCommand _memberAffiliationsCommand;
        private readonly IMemberAffiliationsQuery _memberAffiliationsQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly IFilesQuery _filesQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IReferralsCommand _referralsCommand;
        private readonly IReferralsQuery _referralsQuery;
        private readonly IMemberFriendsCommand _memberFriendsCommand;
        private readonly IMemberFriendsQuery _memberFriendsQuery;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;

        private static readonly SalaryRate[] SalaryRates = new[] { SalaryRate.Year, SalaryRate.Hour };

        private static readonly IList<int?> Months = new int?[] {null}.Concat(from i in Enumerable.Range(1, 12) select (int?)i).ToList();
        private static readonly IList<int?> Years = new int?[] { null }.Concat((from i in Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1) select (int?)i).Reverse()).ToList();

        private static readonly PageflowRoutes Routes = new PageflowRoutes
        {
            new PageflowRoute("Join", JoinRoutes.Join),
            new PageflowRoute("PersonalDetails", JoinRoutes.PersonalDetails),
            new PageflowRoute("JobDetails", JoinRoutes.JobDetails),
            new PageflowRoute("Activate", JoinRoutes.Activate)
        };

        public JoinController(IPageflowEngine pageflowEngine, IAccountsManager accountsManager, IMemberAccountsCommand memberAccountsCommand, IAccountVerificationsCommand accountVerificationsCommand, IMembersQuery membersQuery, ICandidatesCommand candidatesCommand, ICandidatesQuery candidatesQuery, ICandidateResumesCommand candidateResumesCommand, IResumesQuery resumesQuery, IAffiliationItemsFactory affiliationItemsFactory, IMemberAffiliationsCommand memberAffiliationsCommand, IMemberAffiliationsQuery memberAffiliationsQuery, IMemberStatusQuery memberStatusQuery, IFilesQuery filesQuery, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IReferralsCommand referralsCommand, IReferralsQuery referralsQuery, IMemberFriendsCommand memberFriendsCommand, IMemberFriendsQuery memberFriendsQuery, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand)
            : base(Routes, pageflowEngine)
        {
            _membersQuery = membersQuery;
            _memberAccountsCommand = memberAccountsCommand;
            _accountVerificationsCommand = accountVerificationsCommand;
            _accountsManager = accountsManager;
            _candidatesQuery = candidatesQuery;
            _candidatesCommand = candidatesCommand;
            _candidateResumesCommand = candidateResumesCommand;
            _resumesQuery = resumesQuery;
            _filesQuery = filesQuery;
            _affiliationItemsFactory = affiliationItemsFactory;
            _memberAffiliationsCommand = memberAffiliationsCommand;
            _memberAffiliationsQuery = memberAffiliationsQuery;
            _memberStatusQuery = memberStatusQuery;
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _referralsCommand = referralsCommand;
            _referralsQuery = referralsQuery;
            _memberFriendsCommand = memberFriendsCommand;
            _memberFriendsQuery = memberFriendsQuery;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
        }

        [EnsureHttps]
        public ActionResult Join(Guid? inviteId, LoginReason? reason)
        {
            // Save the invitation id.

            Pageflow.HasInitialMember = User.IsAuthenticated();
            Pageflow.FriendInvitationId = inviteId;

            var model = GetModel<JoinModel>(User.Id());
            model.Reason = reason;

            return View(model);
        }

        [EnsureHttps, HttpPost, ActionName("Join")]
        public ActionResult PostJoin(Guid? fileReferenceId, Guid? parsedResumeId)
        {
            // Store the ids.

            Pageflow.FileReferenceId = fileReferenceId;
            Pageflow.ParsedResumeId = parsedResumeId;

            if (parsedResumeId != null)
            {
                var parsedResume = _resumesQuery.GetParsedResume(parsedResumeId.Value);
                if (parsedResume != null && !parsedResume.IsEmpty)
                    Pageflow.IsResumeValid = true;
            }

            Pageflow.IsUploadingResume = Pageflow.IsResumeValid;

            // Move onto the next step.

            return Next();
        }

        public ActionResult PersonalDetails()
        {
            var memberId = User.Id();
            var hasExecutedStep = CurrentStep.HasExecuted;

            // Use the currently logged in member or the default one.

            var memberModel = memberId != null
                ? GetPersonalDetailsMemberModel(memberId.Value, hasExecutedStep)
                : GetPersonalDetailsMemberModel();

            // Update it with anything in the parsed resume.

            UpdateMemberModel(memberModel, Pageflow.ParsedResumeId);

            return View(GetPersonalDetailsModel(memberId, memberModel, new PersonalDetailsPasswordsModel()));
        }

        [HttpPost]
        public ActionResult PersonalDetails(PersonalDetailsMemberModel memberModel, PersonalDetailsPasswordsModel passwordsModel)
        {
            var memberId = User.Id();

            try
            {
                // Make sure everything has been supplied appropriately.

                Validate(memberId, memberModel, passwordsModel);

                // Check whether the member is logged in.

                Member member = null;
                if (memberId != null)
                    member = _membersQuery.GetMember(memberId.Value);

                // If there is no member then try to join now.

                if (member == null)
                    member = TryJoin(memberModel, passwordsModel, Pageflow.FriendInvitationId);

                // Update the member.

                if (member != null)
                {
                    // Update the member.

                    UpdateMember(member, memberModel);
                    UpdateAffiliationItems(member.Id);

                    // Send out an activation email if needed.

                    if (!member.IsActivated)
                        _accountVerificationsCommand.SendActivation(member, member.GetPrimaryEmailAddress().Address);

                    // Member exists and has been updated so move on.

                    return Next();
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new JoinErrorHandler());
            }

            return View(GetPersonalDetailsModel(memberId, memberModel, passwordsModel));
        }

        public ActionResult JobDetails()
        {
            var memberId = User.Id().Value;

            // Get the member and update it with anything in the parsed resume.

            var memberModel = GetJobDetailsMemberModel(memberId);
            UpdateMemberModel(memberModel, Pageflow.ParsedResumeId);

            return View(GetJobDetailsModel(memberId, memberModel, GetReferralSourceId(memberId)));
        }

        [HttpPost]
        public ActionResult JobDetails(JobDetailsMemberModel memberModel, bool sendSuggestedJobs, int? externalReferralSourceId)
        {
            var memberId = User.Id().Value;

            try
            {
                // Make sure everything has been supplied appropriately.

                Validate(memberModel);

                // Update the member.

                var member = _membersQuery.GetMember(memberId);
                var candidate = _candidatesQuery.GetCandidate(member.Id);

                UpdateMember(member, candidate, memberModel);
                UpdateSuggestedJobs(member.Id, sendSuggestedJobs);
                UpdateReferral(memberId, externalReferralSourceId);

                // Send out a verification email if needed.

                if (member.EmailAddresses.Count > 1)
                    _accountVerificationsCommand.SendVerification(member, member.GetSecondaryEmailAddress().Address);

                // Member exists and has been updated so move on.

                return Next();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new JoinErrorHandler());
            }

            return View(GetJobDetailsModel(memberId, memberModel, externalReferralSourceId));
        }

        public ActionResult Activate()
        {
            var member = CurrentMember;

            // The member may be running through the join process in response to an invitation etc and so may be already activated.

            if (member.IsActivated)
                return RedirectToReturnUrl(ProfilesRoutes.Profile.GenerateUrl());

            return View(GetActivateModel(member));
        }

        protected override JoinPageflow CreatePageflow()
        {
            return new JoinPageflow
            {
                IsUploadingResume = true,
            };
        }

        private PersonalDetailsModel GetPersonalDetailsModel(Guid? memberId, PersonalDetailsMemberModel member, PersonalDetailsPasswordsModel passwords)
        {
            var model = GetModel<PersonalDetailsModel>(memberId);

            model.Member = member;
            model.Passwords = passwords;

            var communityId = ActivityContext.Community.Id;
            model.AffiliationItems = communityId != null && memberId != null
                ? _memberAffiliationsQuery.GetItems(memberId.Value, communityId.Value)
                : null;

            model.Reference = new PersonalDetailsReferenceModel
            {
                Countries = _locationQuery.GetCountries(),
                DefaultCountry = ActivityContext.Location.Country,
                SalaryRates = SalaryRates,
            };

            return model;
        }

        private PersonalDetailsMemberModel GetPersonalDetailsMemberModel(Guid memberId, bool hasExecutedStep)
        {
            var member = _membersQuery.GetMember(memberId);
            var candidate = _candidatesQuery.GetCandidate(memberId);

            var emailAddress = member.EmailAddresses == null || member.EmailAddresses.Count < 1 ? null : member.EmailAddresses[0];
            var phoneNumber = member.PhoneNumbers == null || member.PhoneNumbers.Count < 1 ? null : member.PhoneNumbers[0];

            return new PersonalDetailsMemberModel
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                EmailAddress = emailAddress == null ? null : emailAddress.Address,
                CountryId = member.Address.Location.Country.Id,
                Location = member.Address.Location.ToString(),
                PhoneNumber = phoneNumber == null ? null : phoneNumber.Number,
                PhoneNumberType = phoneNumber == null ? Defaults.PrimaryPhoneNumberType : phoneNumber.Type,
                Status = hasExecutedStep ? candidate.Status : (CandidateStatus?) null,
                SalaryLowerBound = candidate.DesiredSalary == null ? null : candidate.DesiredSalary.LowerBound,
                SalaryRate = candidate.DesiredSalary == null
                    ? Defaults.DesiredSalaryRate
                    : SalaryRates.Contains(candidate.DesiredSalary.Rate)
                        ? candidate.DesiredSalary.Rate
                        : Defaults.DesiredSalaryRate,
                Visibility = member.VisibilitySettings.Professional.EmploymentVisibility
            };
        }

        private PersonalDetailsMemberModel GetPersonalDetailsMemberModel()
        {
            var location = GetDefaultLocation();

            return new PersonalDetailsMemberModel
            {
                CountryId = location.Country.Id,
                PhoneNumberType = Defaults.PrimaryPhoneNumberType,
                Status = null,
                SalaryRate = Defaults.DesiredSalaryRate,
                Visibility = new VisibilitySettings().Professional.EmploymentVisibility,
            };
        }

        private void UpdateMemberModel(PersonalDetailsMemberModel memberModel, Guid? parsedResumeId)
        {
            if (parsedResumeId == null)
                return;
            var parsedResume = _resumesQuery.GetParsedResume(parsedResumeId.Value);
            if (parsedResume == null)
                return;

            // Update those bits that are not already set.

            if (string.IsNullOrEmpty(memberModel.FirstName))
                memberModel.FirstName = parsedResume.FirstName;
            if (string.IsNullOrEmpty(memberModel.LastName))
                memberModel.LastName = parsedResume.LastName;

            var country = _locationQuery.GetCountry(memberModel.CountryId);
            if (string.IsNullOrEmpty(memberModel.Location))
                memberModel.Location = _locationQuery.ResolveLocation(country, parsedResume.Address == null ? null : parsedResume.Address.Location).ToString().NullIfEmpty();

            if (string.IsNullOrEmpty(memberModel.EmailAddress))
            {
                if (parsedResume.EmailAddresses != null && parsedResume.EmailAddresses.Count != 0)
                    memberModel.EmailAddress = parsedResume.EmailAddresses[0].Address;
            }

            if (string.IsNullOrEmpty(memberModel.PhoneNumber))
            {
                var phoneNumber = GetPhoneNumber(parsedResume);
                if (phoneNumber != null)
                {
                    memberModel.PhoneNumber = phoneNumber.Number;
                    memberModel.PhoneNumberType = phoneNumber.Type;
                }
            }
        }

        private JobDetailsModel GetJobDetailsModel(Guid? memberId, JobDetailsMemberModel memberModel, int? externalReferralSourceId)
        {
            var model = GetModel<JobDetailsModel>(memberId);
            model.Member = memberModel;

            // Referral.

            model.ExternalReferralSourceId = externalReferralSourceId;
            model.SendSuggestedJobs = Pageflow.SendSuggestedJobs == null ? Defaults.SendSuggestedJobs : Pageflow.SendSuggestedJobs.Value;

            // Referance.

            var country = ActivityContext.Location.Country;
            model.Reference = new JobDetailsReferenceModel
            {
                Countries = _locationQuery.GetCountries(),
                CurrentCountry = country,
                CountrySubdivisions = (from s in _locationQuery.GetCountrySubdivisions(country) where !s.IsCountry select s).ToList(),
                Regions = _locationQuery.GetRegions(country),
                Industries = _industriesQuery.GetIndustries(),
                Months = Months,
                Years = Years,
                ExternalReferralSources = _referralsQuery.GetExternalReferralSources(),
            };

            return model;
        }

        private JobDetailsMemberModel GetJobDetailsMemberModel(Guid memberId)
        {
            var member = _membersQuery.GetMember(memberId);
            var candidate = _candidatesQuery.GetCandidate(memberId);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            var job = resume == null || resume.Jobs == null || resume.Jobs.Count == 0 ? null : resume.Jobs[0];

            var secondaryEmailAddress = member.GetSecondaryEmailAddress();
            var secondaryPhoneNumber = member.GetSecondaryPhoneNumber();

            var country = ActivityContext.Location.Country;

            return new JobDetailsMemberModel
            {
                JobTitle = job == null ? null : job.Title,
                JobCompany = job == null ? null : job.Company,
                IndustryIds = candidate.Industries == null ? null : candidate.Industries.Select(i => i.Id).ToList(),
                RecentProfession = candidate.RecentProfession,
                RecentSeniority = candidate.RecentSeniority,
                HighestEducationLevel = candidate.HighestEducationLevel,
                DesiredJobTitle = candidate.DesiredJobTitle,
                DesiredJobTypes = candidate.DesiredJobTypes,
                EthnicStatus = member.EthnicStatus,
                Gender = member.Gender,
                DateOfBirth = member.DateOfBirth,
                Citizenship = resume == null ? null : resume.Citizenship,
                VisaStatus = candidate.VisaStatus,
                RelocationPreference = candidate.RelocationPreference,
                RelocationCountryIds = candidate.RelocationLocations.GetRelocationCountryIds(),
                RelocationCountryLocationIds = candidate.RelocationLocations.GetRelocationCountryLocationIds(country),
                SecondaryEmailAddress = secondaryEmailAddress == null ? null : secondaryEmailAddress.Address,
                SecondaryPhoneNumber = secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Number,
                SecondaryPhoneNumberType = secondaryPhoneNumber == null ? Defaults.SecondaryPhoneNumberType : secondaryPhoneNumber.Type,
            };
        }

        private ActivateModel GetActivateModel(Member member)
        {
            var model = GetModel<ActivateModel>(member.Id);

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            model.MemberId = member.Id;
            model.EmailAddress = member.EmailAddresses == null || member.EmailAddresses.Count < 1 ? null : member.EmailAddresses[0].Address;
            model.MemberStatus = _memberStatusQuery.GetMemberStatus(member, candidate, resume);

            return model;
        }

        private int? GetReferralSourceId(Guid memberId)
        {
            var referral = _referralsQuery.GetExternalReferral(memberId);
            return referral == null ? (int?)null : referral.SourceId;
        }

        private void UpdateMemberModel(JobDetailsMemberModel memberModel, Guid? parsedResumeId)
        {
            if (parsedResumeId == null)
                return;
            var parsedResume = _resumesQuery.GetParsedResume(parsedResumeId.Value);
            if (parsedResume == null)
                return;

            // Update those bits that are not already set.

            if (memberModel.DateOfBirth == null)
                memberModel.DateOfBirth = parsedResume.DateOfBirth;
        }

        private TModel GetModel<TModel>(Guid? memberId)
            where TModel : JoinModel, new()
        {
            return new TModel
            {
                HasMember = memberId != null,
                HasInitialMember = Pageflow.HasInitialMember,
                IsUploadingResume = Pageflow.IsUploadingResume,
            };
        }

        private void Validate(Guid? memberId, PersonalDetailsMemberModel memberModel, PersonalDetailsPasswordsModel passwordsModel)
        {
            memberModel.Prepare();

            // The passwords are only used if the user has not already joined.

            if (memberId == null)
                passwordsModel.Prepare();

            if (memberModel.Status == CandidateStatus.Unspecified)
                memberModel.Status = null;

            // Gather all errors.

            var errors = memberModel.GetValidationErrors().ToList();
            if (memberId == null)
            {
                if (!passwordsModel.AcceptTerms)
                    errors = errors.Concat(new[] { new TermsValidationError("AcceptTerms") }).ToList();
                errors = errors.Concat(passwordsModel.GetValidationErrors()).ToList();
            }

            // Validate the location separately.

            if (!string.IsNullOrEmpty(memberModel.Location))
            {
                var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(memberModel.CountryId), memberModel.Location);
                IValidator validator = new PostalSuburbValidator();
                if (!validator.IsValid(location))
                    errors = errors.Concat(validator.GetValidationErrors("Location")).ToList();
                else
                    memberModel.Location = location.ToString();
            }

            if (errors.Any())
                throw new ValidationErrorsException(errors);
        }

        private static void Validate(JobDetailsMemberModel memberModel)
        {
            memberModel.Prepare();
            var errors = memberModel.GetValidationErrors().ToList();

            // Gender and date of birth are required.

            if (memberModel.Gender == Gender.Unspecified)
                errors.Add(new RequiredValidationError("Gender"));
            if (memberModel.DateOfBirth == null)
                errors.Add(new RequiredValidationError("DateOfBirth"));

            if (errors.Any())
                throw new ValidationErrorsException(errors);
        }

        private Member TryJoin(PersonalDetailsMemberModel memberModel, IHavePasswords passwordsModel, Guid? friendInvitationId)
        {
            var account = new MemberAccount
            {
                FirstName = memberModel.FirstName,
                LastName = memberModel.LastName,
                EmailAddress = memberModel.EmailAddress
            };

            var credentials = new AccountLoginCredentials
            {
                LoginId = memberModel.EmailAddress,
                Password = passwordsModel.Password,
                ConfirmPassword = passwordsModel.ConfirmPassword,
            };

            // If the user is responding to an invitation then don't need to activate, unless they have changed their email.

            var friendInvitation = GetFriendInvitation(friendInvitationId);
            var acceptingFriendInvitation = friendInvitation != null && string.Equals(account.EmailAddress, friendInvitation.InviteeEmailAddress, StringComparison.OrdinalIgnoreCase);
            var requiresActivation = !acceptingFriendInvitation;

            // Join.

            var member = _accountsManager.Join(HttpContext, account, credentials, requiresActivation);

            // Deal with the invitations.

            if (acceptingFriendInvitation)
                AcceptFriendInvitation(member.Id, friendInvitation);

            return member;
        }

        private FriendInvitation GetFriendInvitation(Guid? friendInvitationId)
        {
            return friendInvitationId == null ? null : _memberFriendsQuery.GetFriendInvitation(friendInvitationId.Value);
        }

        private void AcceptFriendInvitation(Guid memberId, FriendInvitation friendInvitation)
        {
            _memberFriendsCommand.AcceptInvitation(memberId, friendInvitation);
        }

        private void UpdateMember(Member member, PersonalDetailsMemberModel memberModel)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            member.FirstName = memberModel.FirstName;
            member.LastName = memberModel.LastName;
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = memberModel.EmailAddress } };

            // Address.

            member.Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(memberModel.CountryId), memberModel.Location) };

            // Phone number.

            if (member.PhoneNumbers == null || member.PhoneNumbers.Count == 0)
            {
                member.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber { Number = memberModel.PhoneNumber, Type = memberModel.PhoneNumberType }
                };
            }
            else
            {
                member.PhoneNumbers[0].Number = memberModel.PhoneNumber;
                member.PhoneNumbers[0].Type = memberModel.PhoneNumberType;
            }

            // Visibility. Start with the current and then ensure each is set as appropriate.

            var visibility = member.VisibilitySettings.Professional.EmploymentVisibility;
            visibility = visibility.SetFlag(ProfessionalVisibility.Resume, memberModel.Visibility.IsFlagSet(ProfessionalVisibility.Resume));
            if (visibility.IsFlagSet(ProfessionalVisibility.Resume))
            {
                visibility = visibility.SetFlag(ProfessionalVisibility.Name, memberModel.Visibility.IsFlagSet(ProfessionalVisibility.Name));
                visibility = visibility.SetFlag(ProfessionalVisibility.PhoneNumbers, memberModel.Visibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers));
                visibility = visibility.SetFlag(ProfessionalVisibility.RecentEmployers, memberModel.Visibility.IsFlagSet(ProfessionalVisibility.RecentEmployers));
            }
            else
            {
                visibility = visibility.ResetFlag(ProfessionalVisibility.Name);
                visibility = visibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
                visibility = visibility.ResetFlag(ProfessionalVisibility.RecentEmployers);
            }

            visibility = visibility.SetFlag(ProfessionalVisibility.Salary, memberModel.Visibility.IsFlagSet(ProfessionalVisibility.Salary));

            member.VisibilitySettings.Professional.EmploymentVisibility = visibility;

            // Update the member.

            _memberAccountsCommand.UpdateMember(member);

            // Status.

            candidate.Status = memberModel.Status ?? Defaults.CandidateStatus;
            candidate.DesiredSalary = memberModel.SalaryLowerBound == null
                ? null
                : new Salary { LowerBound = memberModel.SalaryLowerBound, Currency = Currency.AUD, Rate = memberModel.SalaryRate };

            // Update the candidate.

            _candidatesCommand.UpdateCandidate(candidate);

            // Save the uploaded resume for the member.

            if (Pageflow.FileReferenceId != null)
            {
                var fileReference = _filesQuery.GetFileReference(Pageflow.FileReferenceId.Value);
                if (fileReference != null)
                {
                    if (Pageflow.ParsedResumeId != null && Pageflow.IsResumeValid)
                    {
                        var parsedResume = _resumesQuery.GetParsedResume(Pageflow.ParsedResumeId.Value);
                        _candidateResumesCommand.CreateResume(candidate, parsedResume.Resume.Clone(), fileReference);
                    }
                    else
                    {
                        _candidateResumesCommand.CreateResumeFile(candidate, fileReference);
                    }
                }
            }
        }

        private void UpdateMember(Member member, Candidate candidate, JobDetailsMemberModel memberModel)
        {
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            // Update the member.

            member.EthnicStatus = memberModel.EthnicStatus;
            member.Gender = memberModel.Gender;
            member.DateOfBirth = memberModel.DateOfBirth;

            if (!string.IsNullOrEmpty(memberModel.SecondaryEmailAddress))
                member.UpdateSecondaryEmailAddress(new EmailAddress { Address = memberModel.SecondaryEmailAddress });
            if (!string.IsNullOrEmpty(memberModel.SecondaryPhoneNumber))
                member.UpdateSecondaryPhoneNumber(new PhoneNumber { Number = memberModel.SecondaryPhoneNumber, Type = memberModel.SecondaryPhoneNumberType });

            _memberAccountsCommand.UpdateMember(member);

            // Update the candidate.

            candidate.Industries = memberModel.IndustryIds == null
                ? null
                : _industriesQuery.GetIndustries(memberModel.IndustryIds);
            candidate.RecentProfession = memberModel.RecentProfession;
            candidate.RecentSeniority = memberModel.RecentSeniority;
            candidate.HighestEducationLevel = memberModel.HighestEducationLevel;
            candidate.DesiredJobTitle = memberModel.DesiredJobTitle;
            candidate.DesiredJobTypes = memberModel.DesiredJobTypes;
            candidate.VisaStatus = memberModel.VisaStatus;

            candidate.RelocationPreference = memberModel.RelocationPreference;
            candidate.RelocationLocations = GetRelocationLocations(memberModel.RelocationCountryIds, memberModel.RelocationCountryLocationIds);

            _candidatesCommand.UpdateCandidate(candidate);

            // Update the resume.

            if (resume != null)
            {
                // Set it.

                resume.Citizenship = memberModel.Citizenship;
                if (Pageflow.ParsedResumeId == null)
                    resume.Jobs = GetJobs(memberModel);
                _candidateResumesCommand.UpdateResume(candidate, resume);
            }
            else
            {
                // Create it.

                resume = new Resume { Citizenship = memberModel.Citizenship };
                if (Pageflow.ParsedResumeId == null)
                    resume.Jobs = GetJobs(memberModel);
                if (!resume.IsEmpty)
                    _candidateResumesCommand.CreateResume(candidate, resume);
            }
        }

        private void UpdateSuggestedJobs(Guid memberId, bool sendSuggestedJobs)
        {
            try
            {
                var category = _settingsQuery.GetCategory("SuggestedJobs");
                var setting = _settingsQuery.GetSettings(memberId).CategorySettings.FirstOrDefault(c => c.CategoryId == category.Id);

                var currentlySending = setting != null && setting.Frequency != Frequency.Never;

                if (!sendSuggestedJobs && currentlySending)
                    _settingsCommand.SetFrequency(memberId, category.Id, Frequency.Never);
                else if (sendSuggestedJobs && !currentlySending)
                    _settingsCommand.SetFrequency(memberId, category.Id, Frequency.Daily);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }
        }

        private void UpdateReferral(Guid memberId, int? externalReferralSourceId)
        {
            // Work through the combinations based on whether or not a referral has already been set.

            var referral = _referralsQuery.GetExternalReferral(memberId);
            if (referral == null)
            {
                if (externalReferralSourceId != null)
                    _referralsCommand.CreateExternalReferral(new ExternalReferral { SourceId = externalReferralSourceId.Value, UserId = memberId });
            }
            else
            {
                if (externalReferralSourceId == null)
                {
                    _referralsCommand.DeleteExternalReferral(memberId);
                }
                else
                {
                    if (referral.SourceId != externalReferralSourceId.Value)
                    {
                        referral.SourceId = externalReferralSourceId.Value;
                        _referralsCommand.UpdateExternalReferral(referral);
                    }
                }
            }
        }

        private void UpdateAffiliationItems(Guid memberId)
        {
            var communityId = ActivityContext.Community.Id;
            if (communityId == null)
                return;

            var items = _affiliationItemsFactory.ConvertAffiliationItems(communityId.Value, new ValueProviderDictionary(ValueProvider));
            if (items != null)
                _memberAffiliationsCommand.SetItems(memberId, communityId.Value, items);
        }

        private static IList<Job> GetJobs(JobDetailsMemberModel memberModel)
        {
            if (string.IsNullOrEmpty(memberModel.JobTitle) && string.IsNullOrEmpty(memberModel.JobCompany))
                return null;
            return new[] { new Job { Title = memberModel.JobTitle, Company = memberModel.JobCompany } };
        }

        private static PhoneNumber GetPhoneNumber(IHavePhoneNumbers parsedResume)
        {
            if (parsedResume.PhoneNumbers == null || parsedResume.PhoneNumbers.Count == 0)
                return null;

            return new PhoneNumber
            {
                Number = parsedResume.PhoneNumbers[0].Number,
                Type = parsedResume.PhoneNumbers[0].Type,
            };
        }

        private LocationReference GetDefaultLocation()
        {
            return _locationQuery.ResolveLocation(ActivityContext.Location.Country, null);
        }

        private IList<LocationReference> GetRelocationLocations(ICollection<int> relocationCountryIds, ICollection<int> relocationCountryLocationIds)
        {
            IList<LocationReference> locations = null;

            if (relocationCountryIds != null && relocationCountryIds.Count != 0)
                locations = relocationCountryIds.Select(countryId => _locationQuery.ResolveLocation(_locationQuery.GetCountry(countryId), null)).ToList();

            if (relocationCountryLocationIds != null && relocationCountryLocationIds.Count != 0)
            {
                if (locations == null)
                    locations = new List<LocationReference>();
                foreach (var locationId in relocationCountryLocationIds)
                    locations.Add(new LocationReference(_locationQuery.GetNamedLocation(locationId)));
            }

            return locations;
        }
    }
}