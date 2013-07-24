using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AjaxPro;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Validation;
using LinkMe.Web.Applications.Ajax;
using LinkMe.Web.UI.Controls.Common.ResumeEditor;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public class AjaxResumeEditor : AjaxEditorBase
    {
        private const string ErrorUnknownJobId = "A user {0} is trying to modify job with ID {1}, but this ID is not present in his/her jobs history list.";
        private const string ErrorUnknownSchoolId = "A user {0} is trying to modify Education record with ID {1}, but this ID is not present in his/her jobs history list.";
        private const string NoRelocationPreferenceSpecified = "Please specify your relocation preference.";

        private static readonly Regex FirstNameRegex = new Regex(RegularExpressions.CompleteFirstNamePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex LastNameRegex = new Regex(RegularExpressions.CompleteLastNamePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new Regex(RegularExpressions.CompletePhoneNumberPattern, RegexOptions.Compiled);

        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Container.Current.Resolve<ICandidateResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Container.Current.Resolve<IResumesQuery>();
        private readonly IIndustriesQuery _industriesQuery = Container.Current.Resolve<IIndustriesQuery>();
        private readonly ISettingsQuery _settingsQuery = Container.Current.Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Container.Current.Resolve<ISettingsCommand>();
        private readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        private Candidate _candidate;
        private Resume _resume;
        private bool _isNew;

        private Resume Resume
        {
            get
            {
                if (_resume == null)
                {
                    var member = LoggedInMember;
                    _candidate = _candidatesCommand.GetCandidate(member.Id);
                    var resume = _candidate.ResumeId == null ? null : _resumesQuery.GetResume(_candidate.ResumeId.Value);
                    if (resume == null)
                    {
                        resume = new Resume();
                        _isNew = true;
                    }
                    else
                    {
                        _isNew = false;
                    }
                    _resume = resume;
                }

                return _resume;
            }
        }

        [AjaxMethod]
        public AjaxResult SaveObjective(string newObjective)
        {
            try
            {
                EnsureMemberLoggedIn();
                Resume.Objective = newObjective;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (Resume.HasObjective() ? 0 : AjaxResultCode.EMPTY));
        }

        [AjaxMethod]
        public AjaxResult SaveSummary(string newSummary)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Summary = newSummary;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (Resume.HasSummary() ? 0 : AjaxResultCode.EMPTY));
        }
        
        [AjaxMethod]
        public AjaxResult SavePrimaryDetails(string firstName, string lastName, string mobilePhone, string workPhone, string homePhone, string[] industryIds, string countryId, string location)
        {
            var errorMessages = new List<string>();
            if (string.IsNullOrEmpty(location))
            {
                errorMessages.Add(ValidationErrorMessages.REQUIRED_FIELD_LOCATION);
            }

            if (string.IsNullOrEmpty(firstName))
            {
                errorMessages.Add(ValidationErrorMessages.REQUIRED_FIELD_FIRST_NAME);
            }
            else if (!FirstNameRegex.IsMatch(firstName))
            {
                errorMessages.Add(ValidationErrorMessages.INVALID_FIRST_NAME);
            }

            if (string.IsNullOrEmpty(lastName))
            {
                errorMessages.Add(ValidationErrorMessages.REQUIRED_FIELD_LAST_NAME);
            }
            else if (!LastNameRegex.IsMatch(lastName))
            {
                errorMessages.Add(ValidationErrorMessages.INVALID_LAST_NAME);
            }

            if (string.IsNullOrEmpty(mobilePhone) && string.IsNullOrEmpty(workPhone) && string.IsNullOrEmpty(homePhone))
            {
                errorMessages.Add(ValidationErrorMessages.REQUIRED_FIELD_CONTACT_PHONE);
            }
            
            if ((!string.IsNullOrEmpty(mobilePhone) && !PhoneRegex.IsMatch(mobilePhone))
                || (!string.IsNullOrEmpty(workPhone) && !PhoneRegex.IsMatch(workPhone))
                || (!string.IsNullOrEmpty(homePhone) && !PhoneRegex.IsMatch(homePhone)))
            {
                errorMessages.Add(ValidationErrorMessages.INVALID_PHONE_NUMBER);
            }

            if (errorMessages.Count != 0)
            {
                return new AjaxResult(AjaxResultCode.FAILURE, ConvertMessagesToString(errorMessages));        
            }

            try
            {
                EnsureMemberLoggedIn();

                var member = LoggedInMember;
                member.FirstName = firstName;
                member.LastName = lastName;

                member.PhoneNumbers = new List<PhoneNumber>();
                if (!string.IsNullOrEmpty(mobilePhone))
                    member.PhoneNumbers.Add(new PhoneNumber { Number = mobilePhone, Type = PhoneNumberType.Mobile });
                if (!string.IsNullOrEmpty(homePhone))
                    member.PhoneNumbers.Add(new PhoneNumber { Number = homePhone, Type = PhoneNumberType.Home });
                if (!string.IsNullOrEmpty(workPhone))
                    member.PhoneNumbers.Add(new PhoneNumber { Number = workPhone, Type = PhoneNumberType.Work });

                var country = _locationQuery.GetCountry(int.Parse(countryId));
                member.Address.Location = _locationQuery.ResolveLocation(country, location);
                _memberAccountsCommand.UpdateMember(member);
                
                var candidate = _candidatesCommand.GetCandidate(member.Id);
                candidate.Industries = (from i in industryIds select _industriesQuery.GetIndustry(ParseUtil.ParseUserInputGuid(i, "industry ID"))).ToList();
                _candidatesCommand.UpdateCandidate(candidate);

                return new AjaxResult(AjaxResultCode.SUCCESS);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AjaxMethod]
        public AjaxResult SaveDesiredJobDetails(string desiredJobTitle,
                                                string desiredSalaryLower, string desiredSalaryUpper,
                                                string desiredSalaryLabel, int desiredJobTypes,
                                                string desiredJobTypesLabel, bool emailSuggestedJobs,
                                                string desiredLocalitiesLabel,
                                                string relocationChoiceLabel,
                                                string relPref,
                                                string selectedLocalities)
        {
            var errorMessages = new List<string>();

            RelocationPreference relocationPreference = RelocationPreference.No;
            try
            {
                relocationPreference = (RelocationPreference)Enum.Parse(typeof(RelocationPreference), relPref);
            }
            catch (ArgumentException)
            {
                errorMessages.Add(NoRelocationPreferenceSpecified);
            }

            if (errorMessages.Count != 0)
            {
                return new AjaxResult(AjaxResultCode.FAILURE, ConvertMessagesToString(errorMessages));
            }

            try
            {
                EnsureMemberLoggedIn();

                var member = LoggedInMember;
                var localitiesText = String.Empty;

                var candidate = _candidatesCommand.GetCandidate(member.Id);
                var desiredSalary = SalaryExtensions.Parse(desiredSalaryLower, desiredSalaryUpper, SalaryRate.Year, true);

                // Update.

                candidate.DesiredJobTitle = desiredJobTitle;
                candidate.DesiredJobTypes = (JobTypes) desiredJobTypes;
                candidate.DesiredSalary = desiredSalary;
                candidate.RelocationPreference = relocationPreference;
                candidate.RelocationLocations = WillingnessToRelocate.GetSelectedLocations(candidate, selectedLocalities);
                _candidatesCommand.UpdateCandidate(candidate);

                if (candidate.RelocationPreference != RelocationPreference.No)
                    localitiesText = TextUtil.TruncateForDisplay(candidate.GetRelocationsDisplayText(), DesiredJob.MaxRelocationLocalitiesLength);
                
                var relocationPreferenceText = DesiredJob.GetRelocationPreference(candidate.RelocationPreference);

                UpdateSuggestedJobs(member.Id, emailSuggestedJobs);

                var userData = new ElementValuesUserData(
                    new[]
                    {
                        desiredSalaryLabel,
                        desiredJobTypesLabel,
                        desiredLocalitiesLabel,
                        relocationChoiceLabel
                    },
                    new[]
                    {
                        candidate.DesiredSalary == null ? string.Empty : candidate.DesiredSalary.GetDisplayText(),
                        candidate.DesiredJobTypes.GetDesiredClauseDisplayText(),
                        localitiesText, relocationPreferenceText
                    });

                return new AjaxResult(AjaxResultCode.SUCCESS, null, userData);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AjaxMethod]
        public AjaxResult SaveJobHistory(string jobIdStr, string jobTitle, string compName, string startYear, string startMonth, string endYear, string endMonth, bool isCurrent, string description)
        {
            Guid returnId;

            try
            {
                EnsureMemberLoggedIn();

                var startDate = ValidateDate(startMonth, startYear);
                if (startDate == null)
                    throw new UserException("Please specify correct start date");

                PartialDate? endDate = null;
                if (!isCurrent)
                {
                    endDate = ValidateDate(endMonth, endYear);
                    if (endDate == null)
                        throw new UserException("Please specify correct end date");
                    if (startDate > endDate)
                        throw new UserException("The end date should be after the start date");
                }

                if (string.IsNullOrEmpty(jobTitle))
                    throw new UserException("The job title is required.");
                if (string.IsNullOrEmpty(compName))
                    throw new UserException("The company name is required.");

                IList<Job> newJobs;
                Job targetJob = null;
                if (jobIdStr == NoRecordId)
                {
                    targetJob = new Job {Id = Guid.NewGuid()};
                    newJobs = Resume.Jobs;
                    newJobs.Add(targetJob);
                }
                else
                {
                    var jobId = ParseUtil.ParseUserInputGuid(jobIdStr, "job ID");
                    newJobs = new List<Job>();
                    foreach (var job in Resume.Jobs)
                    {
                        if (job.Id == jobId)
                            targetJob = job;
                        newJobs.Add(job);
                    }
                }

                if (targetJob == null)
                {
                    HandleException(String.Format(ErrorUnknownJobId, LoggedInMember.Id, jobIdStr));
                    return new AjaxResult(AjaxResultCode.FAILURE, "Unknown Job ID");
                }

                targetJob.Title = jobTitle;
                targetJob.Company = compName;
                targetJob.Dates = new PartialDateRange(startDate, endDate.Value);
                targetJob.Description = description;

                Resume.Jobs = newJobs;
                UpdateResume();

                returnId = targetJob.Id;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            
            // Returning new order of things

            var jobs = Resume.Jobs;
            var jobIds = new Guid?[jobs.Count];
            for (var index = 0; index < jobs.Count; index++)
                jobIds[index] = jobs[index].Id;

            return new AjaxResult(AjaxResultCode.SUCCESS, returnId.ToString("n"), new IdArrayUserData(jobIds));
        }

        private static PartialDate? ValidateDate(string month, string year)
        {
            PartialDate? date;

            if (string.IsNullOrEmpty(month))
                return PartialDate.TryParse("Jan " + year, out date) ? date : null;
            return PartialDate.TryParse(month + " " + year, out date) ? date : null;
        }

        [AjaxMethod]
        public AjaxResult DeleteJobHistory(string jobIdStr)
        {
            try
            {
                var jobId = ParseUtil.ParseUserInputGuid(jobIdStr, "job ID");

                EnsureMemberLoggedIn();

                var jobs = new List<Job>();
                foreach (var job in Resume.Jobs)
                {
                    if (job.Id != jobId)
                        jobs.Add(job);
                }

                Resume.Jobs = jobs;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            
            return new AjaxResult(AjaxResultCode.SUCCESS | (Resume.Jobs.Count == 0 ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveSkills(string newSkills)
        {
            try
            {
                EnsureMemberLoggedIn();
                Resume.Skills = newSkills;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (Resume.HasSkills() ? 0 : AjaxResultCode.EMPTY));

        }
        
        [AjaxMethod]
        public AjaxResult SaveEducation(string schoolIdStr, string institution, string city, string qualification, string major, string completed, string description)
        {
            try
            {
                EnsureMemberLoggedIn();

                // Require at least the Qualification and Institution. This is a bit arbitrary, as LENS
                // doesn't really care what details are supplied.

                if (string.IsNullOrEmpty(qualification))
                    throw new UserException("The qualification is required.");
                if (string.IsNullOrEmpty(institution))
                    throw new UserException("The institution is required.");

                IList<School> newSchools;
                School targetSchool = null;
                if (schoolIdStr == NoRecordId)
                {
                    targetSchool = new School {Id = Guid.NewGuid()};
                    newSchools = Resume.Schools;
                    newSchools.Add(targetSchool);
                } 
                else
                {
                    var schoolId = ParseUtil.ParseUserInputGuid(schoolIdStr, "education record ID");
                    newSchools = new List<School>();
                    foreach (var school in Resume.Schools)
                    {
                        if (school.Id == schoolId)
                            targetSchool = school;
                        newSchools.Add(school);
                    }
                }

                if (targetSchool == null)
                {
                    HandleException(String.Format(ErrorUnknownSchoolId, LoggedInMember.Id, schoolIdStr));
                    return new AjaxResult(AjaxResultCode.FAILURE, "Unknown Education ID");
                }

                targetSchool.Institution = institution;
                targetSchool.City = city;
                targetSchool.Degree = qualification;
                targetSchool.Major = major;
                targetSchool.CompletionDate = string.IsNullOrEmpty(completed) ? null : new PartialCompletionDate(PartialDate.Parse(completed));
                targetSchool.Description = description;

                Resume.Schools = newSchools;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            var schools = Resume.Schools;
            var schoolIds = new Guid?[schools.Count];
            for (var index = 0; index < schools.Count; index++)
                schoolIds[index] = schools[index].Id;

            return new AjaxResult(AjaxResultCode.SUCCESS, schoolIdStr, new IdArrayUserData(schoolIds));
        }

        [AjaxMethod]
        public AjaxResult DeleteEducation(string schoolIdStr)
        {
            try
            {
                EnsureMemberLoggedIn();

                var schoolId = ParseUtil.ParseUserInputGuid(schoolIdStr, "education record ID");

                var schools = new List<School>();
                foreach (var school in Resume.Schools)
                {
                    if (school.Id != schoolId)
                        schools.Add(school);
                }

                Resume.Schools = schools;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasSchools() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveCourses(string courses)
        {
            try
            {
                EnsureMemberLoggedIn();

                if (string.IsNullOrEmpty(courses))
                    Resume.Courses.Clear();
                else
                    Resume.Courses = courses.Split('\n');

                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasCourses() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveAwards(string awards)
        {
            try
            {
                EnsureMemberLoggedIn();

                if (string.IsNullOrEmpty(awards))
                    Resume.Awards.Clear();
                else
                    Resume.Awards = awards.Split('\n');

                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasAwards() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveProfessional(string professional)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Professional = professional;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasProfessional() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveInterests(string interests)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Interests = interests;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasInterests() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveCitizenship(string citizenship)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Citizenship = citizenship;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasCitizenship() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveAffiliation(string affiliations)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Affiliations = affiliations;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasAffiliations() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveOther(string other)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Other = other;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasOther() ? AjaxResultCode.EMPTY : 0));
        }

        [AjaxMethod]
        public AjaxResult SaveReferences(string refs)
        {
            try
            {
                EnsureMemberLoggedIn();

                Resume.Referees = refs;
                UpdateResume();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return new AjaxResult(AjaxResultCode.SUCCESS | (!Resume.HasReferees() ? AjaxResultCode.EMPTY : 0));
        }

        private void UpdateResume()
        {
            if (_isNew)
                _candidateResumesCommand.CreateResume(_candidate, _resume);
            else
                _candidateResumesCommand.UpdateResume(_candidate, _resume);
            _isNew = false;
        }

        private void UpdateSuggestedJobs(Guid memberId, bool sendSuggestedJobs)
        {
            var category = _settingsQuery.GetCategory("SuggestedJobs");
            var setting = _settingsQuery.GetSettings(memberId).CategorySettings.Where(c => c.CategoryId == category.Id).FirstOrDefault();

            var currentlySending = setting.Frequency != Frequency.Never;

            if (!sendSuggestedJobs && currentlySending)
                _settingsCommand.SetFrequency(memberId, category.Id, Frequency.Never);
            else if (sendSuggestedJobs && !currentlySending)
                _settingsCommand.SetFrequency(memberId, category.Id, Frequency.Daily);
        }
    }
}
