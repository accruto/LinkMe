using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Members.Models.Profiles;
using LinkMe.Web.Controllers;
using LinkMe.Web.Mvc;

namespace LinkMe.Web.Areas.Members.Controllers.Profiles
{
    [EnsureHttps, EnsureAuthorized(UserType.Member, RequiresActivation = true)]
    public class ProfilesController
        : MembersController
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly IVerticalsQuery _verticalsQuery;
        private readonly ISettingsQuery _settingsQuery;

        private static readonly IList<int?> Months = new int?[] { null }.Concat(from i in Enumerable.Range(1, 12) select (int?)i).ToList();
        private static readonly IList<int?> Years = new int?[] { null }.Concat(from i in Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1).Reverse() select (int?)i).ToList();
        private static readonly IList<int?> JobYears = new int?[] { null }.Concat(from i in Enumerable.Range(1960, DateTime.Now.Year - 1960 + 1).Reverse() select (int?)i).ToList();
        private static readonly IList<int?> SchoolYears = new int?[] { null }.Concat(from i in Enumerable.Range(1940, DateTime.Now.Year - 1940 + 1).Reverse() select (int?)i).ToList();
        private static readonly SalaryRate[] SalaryRates = new[] { SalaryRate.Year, SalaryRate.Hour };

        public ProfilesController(ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IResumesQuery resumesQuery, ICandidatesQuery candidatesQuery, IMemberStatusQuery memberStatusQuery, IVerticalsQuery verticalsQuery, ISettingsQuery settingsQuery)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _resumesQuery = resumesQuery;
            _candidatesQuery = candidatesQuery;
            _memberStatusQuery = memberStatusQuery;
            _verticalsQuery = verticalsQuery;
            _settingsQuery = settingsQuery;
        }

        public new ActionResult Profile()
        {
            // Now that the user has gotten here hide all prompts.

            MemberContext.HideUpdateStatusReminder();

            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return View(CreateMemberModel(member, candidate, resume));
        }

        public ActionResult ContactDetails()
        {
            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return View(CreateContactDetailsModel(member, candidate, resume, _locationQuery.GetCountries()));
        }

        public ActionResult DesiredJob()
        {
            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            return View(CreateDesiredJobModel(member, candidate, _locationQuery.GetCountries()));
        }

        public ActionResult CareerObjectives()
        {
            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return View(CreateCareerObjectivesModel(resume));
        }

        public ActionResult EmploymentHistory()
        {
            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return View(CreateEmploymentHistoryModel(candidate, resume, _industriesQuery.GetIndustries()));
        }

        public ActionResult Education()
        {
            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return View(CreateEducationModel(candidate, resume));
        }

        public ActionResult Other()
        {
            var member = CurrentMember;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return View(CreateOtherModel(resume));
        }

        private MemberModel CreateMemberModel(IMember member, ICandidate candidate, IResume resume)
        {
            return new MemberModel
            {
                Status = CreateStatusModel(member, candidate, resume),
                Visibility = CreateVisibilityModel(member),
            };
        }

        private MemberStatusModel CreateStatusModel(IMember member, ICandidate candidate, IResume resume)
        {
            var lastUpdatedTime = _memberStatusQuery.GetLastUpdatedTime(member, candidate, resume);
           
            return new MemberStatusModel
            {
                PercentComplete = _memberStatusQuery.GetPercentComplete(member, candidate, resume),
                Age = (DateTime.Now - lastUpdatedTime).Days,
                PromptForResumeUpdate = _memberStatusQuery.IsUpdateNeeded(member, candidate, resume),
                MemberStatus = _memberStatusQuery.GetMemberStatus(member, candidate, resume),
                MemberId = member.Id,
            };
        }

        private static VisibilityModel CreateVisibilityModel(IMember member)
        {
            return new VisibilityModel
            {
                ShowResume = member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume),
                ShowRecentEmployers = member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers),
                ShowName = member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name),
                ShowPhoneNumbers = member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers),
                ShowProfilePhoto = member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto),
            };
        }

        private ContactDetailsModel CreateContactDetailsModel(IMember member, ICandidate candidate, IResume resume, IList<Country> countries)
        {
            var primaryPhoneNumber = member.GetPrimaryPhoneNumber();
            var secondaryPhoneNumber = member.GetSecondaryPhoneNumber();

            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();

            return new ContactDetailsModel
            {
                Member = new ContactDetailsMemberModel
                {
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    CountryId = member.Address.Location.Country.Id,
                    Location = member.Address.Location.ToString(),
                    EmailAddress = primaryEmailAddress == null ? null : primaryEmailAddress.Address,
                    SecondaryEmailAddress = secondaryEmailAddress == null ? null : secondaryEmailAddress.Address,
                    PhoneNumber = primaryPhoneNumber == null ? null : primaryPhoneNumber.Number,
                    PhoneNumberType = primaryPhoneNumber == null ? Defaults.PrimaryPhoneNumberType : primaryPhoneNumber.Type,
                    SecondaryPhoneNumber = secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Number,
                    SecondaryPhoneNumberType = secondaryPhoneNumber == null ? Defaults.SecondaryPhoneNumberType : secondaryPhoneNumber.Type,
                    Citizenship = resume == null ? null : resume.Citizenship,
                    VisaStatus = candidate.VisaStatus,
                    EthnicStatus = member.EthnicStatus,
                    Gender = member.Gender,
                    DateOfBirth = member.DateOfBirth,
                    PhotoId = member.PhotoId,
                },
                Reference = new ContactDetailsReferenceModel
                {
                    Countries = countries,
                    Months = Months,
                    Years = Years,
                },
                CanEditContactDetails = CanEditContactDetails(),
            };
        }

        private bool CanEditContactDetails()
        {
            var activityContext = ActivityContext;
            return activityContext == null || _verticalsQuery.CanEditContactDetails(activityContext.Vertical.Id);
        }

        private DesiredJobModel CreateDesiredJobModel(IMember member, ICandidate candidate, IList<Country> countries)
        {
            var visibility = member.VisibilitySettings.Professional.EmploymentVisibility;
            var country = ActivityContext.Location.Country;

            return new DesiredJobModel
            {
                Member = new DesiredJobMemberModel
                {
                    SendSuggestedJobs = GetSuggestedJobsEmailSetting(member.Id),
                    DesiredJobTitle = candidate.DesiredJobTitle,
                    DesiredJobTypes = candidate.DesiredJobTypes,
                    Status = candidate.Status,
                    DesiredSalaryLowerBound = candidate.DesiredSalary == null ? null : candidate.DesiredSalary.LowerBound,
                    DesiredSalaryRate = candidate.DesiredSalary == null ? Defaults.DesiredSalaryRate : candidate.DesiredSalary.Rate,
                    IsSalaryNotVisible = !visibility.IsFlagSet(ProfessionalVisibility.Salary),
                    RelocationPreference = candidate.RelocationPreference,
                    RelocationCountryIds = candidate.RelocationLocations.GetRelocationCountryIds(),
                    RelocationCountryLocationIds = candidate.RelocationLocations.GetRelocationCountryLocationIds(country),
                },
                Reference = new DesiredJobReferenceModel
                {
                    Countries = countries,
                    CountrySubdivisions = (from s in _locationQuery.GetCountrySubdivisions(country) where !s.IsCountry select s).ToList(),
                    Regions = _locationQuery.GetRegions(country),
                    CurrentCountry = country,
                    SalaryRates = SalaryRates,
                },
            };
        }

        private static CareerObjectivesModel CreateCareerObjectivesModel(IResume resume)
        {
            return new CareerObjectivesModel
            {
                Member = new CareerObjectivesMemberModel
                {
                    Objective = resume == null ? null : resume.Objective,
                    Summary = resume == null ? null : resume.Summary,
                    Skills = resume == null ? null : resume.Skills,
                },
            };
        }

        private static EmploymentHistoryModel CreateEmploymentHistoryModel(ICandidate candidate, IResume resume, IList<Industry> industries)
        {
            return new EmploymentHistoryModel
            {
                Member = new EmploymentHistoryMemberModel
                {
                    RecentProfession = candidate.RecentProfession,
                    RecentSeniority = candidate.RecentSeniority,
                    IndustryIds = candidate.Industries == null ? new List<Guid>() : candidate.Industries.Select(i => i.Id).ToList(),
                    Jobs = resume == null || resume.Jobs == null
                        ? new List<JobModel>
                              {
                                  new JobModel()
                              }
                        : (from j in resume.Jobs
                           select new JobModel
                           {
                               Id = j.Id,
                               Company = j.Company,
                               Description = j.Description,
                               StartDate = j.Dates == null ? null : j.Dates.Start,
                               EndDate = j.Dates == null ? null : j.Dates.End,
                               Title = j.Title,
                               IsCurrent = j.IsCurrent,
                           }).ToList()
                },
                Reference = new EmploymentHistoryReferenceModel
                {
                    Industries = industries,
                    Months = Months,
                    Years = JobYears,
                },
            };
        }

        private static EducationModel CreateEducationModel(ICandidate candidate, IResume resume)
        {
            return new EducationModel
            {
                Member = new EducationMemberModel
                {
                    HighestEducationLevel = candidate.HighestEducationLevel,
                    Schools = resume == null || resume.Schools == null
                        ? new List<SchoolModel>
                              {
                                  new SchoolModel()
                              }
                        : (from s in resume.Schools
                           select new SchoolModel
                           {
                               Id = s.Id,
                               EndDate = s.CompletionDate == null ? null : s.CompletionDate.End,
                               Degree = s.Degree,
                               Major = s.Major,
                               Institution = s.Institution,
                               City = s.City,
                               Description = s.Description,
                               IsCurrent = s.IsCurrent,
                           }).ToList()
                },
                Reference = new EducationReferenceModel
                {
                    Months = Months,
                    Years = SchoolYears,
                },
            };
        }

        private static OtherModel CreateOtherModel(IResume resume)
        {
            return new OtherModel
            {
                Member = new OtherMemberModel
                {
                    Courses = resume == null ? null : resume.GetCoursesDisplayText(),
                    Awards = resume == null ? null : resume.GetAwardsDisplayText(),
                    Professional = resume == null ? null : resume.Professional,
                    Interests = resume == null ? null : resume.Interests,
                    Affiliations = resume == null ? null : resume.Affiliations,
                    Other = resume == null ? null : resume.Other,
                    Referees = resume == null ? null : resume.Referees,
                },
            };
        }

        private bool GetSuggestedJobsEmailSetting(Guid memberId)
        {
            var category = _settingsQuery.GetCategory("SuggestedJobs");
            var settings = _settingsQuery.GetSettings(memberId);
            var frequency = Frequency.Never;

            if (settings != null && settings.CategorySettings != null)
            {
                var setting = settings.CategorySettings.FirstOrDefault(c => c.CategoryId == category.Id);

                if (setting != null && setting.Frequency.HasValue)
                    frequency = setting.Frequency.Value;
            }

            return frequency != Frequency.Never;
        }
    }
}