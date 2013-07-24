using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Files;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
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
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Photos.Commands;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Members.Models.Profiles;

namespace LinkMe.Web.Areas.Members.Controllers.Profiles
{
    [EnsureHttps, ApiEnsureAuthorized(UserType.Member)]
    public class ProfilesApiController
        : MembersApiController
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumeFilesQuery _resumeFilesQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly IEmailsCommand _emailsCommand;
        private readonly IMemberAccountsCommand _memberAccountsCommand;
        private readonly IAccountVerificationsCommand _accountVerificationsCommand;
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly ICandidateResumesCommand _candidateResumesCommand;
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand;
        private readonly IParseResumesCommand _parseResumesCommand;
        private readonly IMemberPhotosCommand _memberPhotosCommand;
        private readonly IFilesCommand _filesCommand;
        private readonly IFilesQuery _filesQuery;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;

        public ProfilesApiController(ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IResumesQuery resumesQuery, ICandidatesQuery candidatesQuery, IResumeFilesQuery resumeFilesQuery, IMemberStatusQuery memberStatusQuery, IEmailsCommand emailsCommand, IMemberAccountsCommand memberAccountsCommand, IAccountVerificationsCommand accountVerificationsCommand, ICandidatesCommand candidatesCommand, ICandidateResumesCommand candidateResumesCommand, ICandidateResumeFilesCommand candidateResumeFilesCommand, IParseResumesCommand parseResumesCommand, IMemberPhotosCommand memberPhotosCommand, IFilesCommand filesCommand, IFilesQuery filesQuery, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _resumesQuery = resumesQuery;
            _candidatesQuery = candidatesQuery;
            _resumeFilesQuery = resumeFilesQuery;
            _memberStatusQuery = memberStatusQuery;
            _emailsCommand = emailsCommand;
            _memberAccountsCommand = memberAccountsCommand;
            _accountVerificationsCommand = accountVerificationsCommand;
            _candidatesCommand = candidatesCommand;
            _candidateResumesCommand = candidateResumesCommand;
            _candidateResumeFilesCommand = candidateResumeFilesCommand;
            _parseResumesCommand = parseResumesCommand;
            _memberPhotosCommand = memberPhotosCommand;
            _filesCommand = filesCommand;
            _filesQuery = filesQuery;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
        }

        [HttpPost]
        public ActionResult Visibility(VisibilityModel visibilityModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Update.
                
                var visibility = member.VisibilitySettings.Professional.EmploymentVisibility;
                visibility = visibility.SetFlag(ProfessionalVisibility.Resume, visibilityModel.ShowResume);

                if (visibilityModel.ShowResume)
                {
                    visibility = visibility.SetFlag(ProfessionalVisibility.Name, visibilityModel.ShowName);
                    visibility = visibility.SetFlag(ProfessionalVisibility.PhoneNumbers, visibilityModel.ShowPhoneNumbers);
                    visibility = visibility.SetFlag(ProfessionalVisibility.ProfilePhoto, visibilityModel.ShowProfilePhoto);
                    visibility = visibility.SetFlag(ProfessionalVisibility.RecentEmployers, visibilityModel.ShowRecentEmployers);
                }
                else
                {
                    visibility = visibility.ResetFlag(ProfessionalVisibility.Name);
                    visibility = visibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
                    visibility = visibility.ResetFlag(ProfessionalVisibility.ProfilePhoto);
                    visibility = visibility.ResetFlag(ProfessionalVisibility.RecentEmployers);
                }

                member.VisibilitySettings.Professional.EmploymentVisibility = visibility;
                _memberAccountsCommand.UpdateMember(member);

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult ContactDetails(ContactDetailsMemberModel memberModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Check.

                Validate(memberModel);

                // Update.

                UpdateMember(member, m => UpdateMember(m, memberModel));
                UpdateCandidate(candidate, c => UpdateCandidate(c, memberModel));
                resume = UpdateResume(candidate, resume, r => UpdateResume(r, memberModel));

                SendVerifications(member);

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult DesiredJob(DesiredJobMemberModel memberModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Check.

                memberModel.Prepare();
                memberModel.Validate();

                // Update.

                UpdateMember(member, m => UpdateMember(m, memberModel));
                UpdateCandidate(candidate, c => UpdateCandidate(c, memberModel));
                UpdateSuggestedJobs(member.Id, memberModel.SendSuggestedJobs);

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult CareerObjectives(CareerObjectivesMemberModel memberModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Check.

                memberModel.Prepare();
                memberModel.Validate();

                // Update.

                resume = UpdateResume(candidate, resume, r => UpdateResume(r, memberModel));

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult EmploymentHistory(EmploymentHistoryUpdateModel updateModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                Guid? jobId = null;

                // Update the job first because it may fail validation.

                if (updateModel.Job != null)
                {
                    var job = updateModel.Job;

                    if (job.Id != null)
                    {
                        // It has an id which means it is an existing job which needs to be processed.

                        Validate(job);

                        // Just in case it is not in the list check whether to add it or update it.

                        if (resume == null || resume.Jobs == null || resume.Jobs.All(j => j.Id != job.Id.Value))
                            resume = UpdateResume(candidate, resume, r => AddJob(r, job));
                        else
                            resume = UpdateResume(candidate, resume, r => UpdateJob(r, job));

                        jobId = job.Id;
                    }
                    else if (!job.IsEmpty)
                    {
                        // A new job that needs to be added.

                        job.Prepare();
                        Validate(job);
                        resume = UpdateResume(candidate, resume, r => AddJob(r, job));

                        jobId = job.Id;
                    }
                }

                // Update the candidate.

                UpdateCandidate(candidate, c => UpdateCandidate(c, updateModel.RecentProfession, updateModel.RecentSeniority, updateModel.IndustryIds));

                return Json(new JsonProfileJobModel { JobId = jobId, Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult RemoveJob(Guid id)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Check.

                if (resume == null || resume.Jobs == null || resume.Jobs.All(j => j.Id != id))
                    throw new NotFoundException("job");

                // Update.

                resume = UpdateResume(candidate, resume, r => RemoveJob(r, id));

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult Education(EducationUpdateModel updateModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                Guid? schoolId = null;

                // Update the school first because it may fail validation.

                if (updateModel.School != null)
                {
                    var school = updateModel.School;

                    if (school.Id != null)
                    {
                        // It has an id which means it is an existing school which needs to be processed.

                        Validate(school);

                        // Just in case it is not in the list check whether to add it or update it.

                        if (resume == null || resume.Schools == null || resume.Schools.All(s => s.Id != school.Id.Value))
                            resume = UpdateResume(candidate, resume, r => AddSchool(r, school));
                        else
                            resume = UpdateResume(candidate, resume, r => UpdateSchool(r, school));

                        schoolId = school.Id;
                    }
                    else if (!school.IsEmpty)
                    {
                        // A new job that needs to be added.

                        school.Prepare();
                        Validate(school);
                        resume = UpdateResume(candidate, resume, r => AddSchool(r, school));

                        schoolId = school.Id;
                    }
                }

                // Update.

                UpdateCandidate(candidate, c => UpdateCandidate(c, updateModel.HighestEducationLevel));

                return Json(new JsonProfileSchoolModel { SchoolId = schoolId, Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult RemoveSchool(Guid id)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Check.

                if (resume == null || resume.Schools == null || resume.Schools.All(s => s.Id != id))
                    throw new NotFoundException("school");

                // Update.

                resume = UpdateResume(candidate, resume, r => RemoveSchool(r, id));

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult Other(OtherMemberModel memberModel)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Check.

                memberModel.Prepare();
                memberModel.Validate();

                // Update.

                resume = UpdateResume(candidate, resume, r => UpdateResume(r, memberModel));

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult SetCurrent()
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Update both last updated times.

                UpdateCandidate(candidate, c => c.LastUpdatedTime = DateTime.Now);
                if (resume != null)
                    resume = UpdateResume(candidate, resume, r => r.LastUpdatedTime = DateTime.Now);

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult SendResume()
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Send.

                var resumeFile = _resumeFilesQuery.GetResumeFile(member, member, candidate, resume);
                _emailsCommand.TrySend(new ResumeAttachmentEmail(member, resumeFile));
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                    throw new ValidationErrorsException(new RequiredValidationError("file"));

                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);

                // Do a check first.

                var fileName = Path.GetFileName(file.FileName);
                var fileContents = new HttpPostedFileContents(file);
                _candidateResumeFilesCommand.ValidateFile(fileName, fileContents);

                // Save the file and associate it with the candidate.

                var fileReference = _filesCommand.SaveFile(FileType.Resume, fileContents, fileName);
                _candidateResumesCommand.CreateResumeFile(candidate, fileReference);

                // Must send text/plain mime type for legacy browsers.

                return Json(new JsonResumeFileModel
                {
                    Id = fileReference.Id,
                    Name = fileName,
                    Size = file.ContentLength,
                }, MediaType.Text);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), MediaType.Text);
        }

        [HttpPost]
        public ActionResult Parse(Guid fileReferenceId)
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);

                // Load the resume.

                var fileReference = _filesQuery.GetFileReference(fileReferenceId);
                if (fileReference == null)
                    return JsonNotFound("file");

                // Parse it.

                var parsedResume = _parseResumesCommand.ParseResume(fileReference);
                if (parsedResume.Resume == null || parsedResume.Resume.IsEmpty)
                    throw new InvalidResumeException();

                // Save the parsed resume.

                var resume = parsedResume.Resume;
                _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

                return Json(new JsonProfileModel { Profile = CreateProfileModel(member, candidate, resume) });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                    throw new ValidationErrorsException(new RequiredValidationError("file"));

                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Save it.

                var fileReference = _memberPhotosCommand.SavePhoto(new HttpPostedFileContents(file), Path.GetFileName(file.FileName));

                // Update the member.

                member.PhotoId = fileReference.Id;
                _memberAccountsCommand.UpdateMember(member);

                // Must send text/plain mime type for legacy browsers.

                return Json(new JsonProfilePhotoModel
                {
                    Profile = CreateProfileModel(member, candidate, resume),
                    PhotoId = fileReference.Id,
                }, MediaType.Text);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), MediaType.Text);
        }

        [HttpPost]
        public ActionResult RemovePhoto()
        {
            try
            {
                var member = CurrentMember;

                // Update the member by removing the photo reference.

                UpdateMember(member, m => m.PhotoId = null);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private void UpdateMember(Member member, Action<Member> updateMember)
        {
            updateMember(member);
            _memberAccountsCommand.UpdateMember(member);
        }

        private void UpdateCandidate(Candidate candidate, Action<Candidate> updateCandidate)
        {
            updateCandidate(candidate);
            _candidatesCommand.UpdateCandidate(candidate);
        }

        private Resume UpdateResume(Candidate candidate, Resume resume, Action<Resume> updateResume)
        {
            // Does a resume need to be created.

            if (resume == null)
            {
                resume = new Resume();
                updateResume(resume);

                if (!resume.IsEmpty)
                    _candidateResumesCommand.CreateResume(candidate, resume);
            }
            else
            {
                // Update it.

                updateResume(resume);
                _candidateResumesCommand.UpdateResume(candidate, resume);
            }

            return resume;
        }

        private void UpdateMember(Member member, ContactDetailsMemberModel memberModel)
        {
            // Names.

            member.FirstName = memberModel.FirstName;
            member.LastName = memberModel.LastName;

            // Location.

            var country = _locationQuery.GetCountry(memberModel.CountryId ?? ActivityContext.Location.Country.Id);
            member.Address.Location = _locationQuery.ResolveLocation(country, memberModel.Location);

            // Email addresses.

            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = memberModel.EmailAddress } };
            if (!string.IsNullOrEmpty(memberModel.SecondaryEmailAddress))
                member.EmailAddresses.Add(new EmailAddress { Address = memberModel.SecondaryEmailAddress });

            // Phone numbers.

            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = memberModel.PhoneNumber, Type = memberModel.PhoneNumberType } };
            if (!string.IsNullOrEmpty(memberModel.SecondaryPhoneNumber))
                member.PhoneNumbers.Add(new PhoneNumber { Number = memberModel.SecondaryPhoneNumber, Type = memberModel.SecondaryPhoneNumberType });

            // Others.

            member.EthnicStatus = memberModel.EthnicStatus;
            member.Gender = memberModel.Gender;
            member.DateOfBirth = memberModel.DateOfBirth;
        }

        private static void UpdateCandidate(Candidate candidate, ContactDetailsMemberModel memberModel)
        {
            candidate.VisaStatus = memberModel.VisaStatus;
        }

        private static void UpdateResume(Resume resume, ContactDetailsMemberModel memberModel)
        {
            resume.Citizenship = memberModel.Citizenship;
        }

        private static void UpdateMember(IMember member, DesiredJobMemberModel memberModel)
        {
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Salary, !memberModel.IsSalaryNotVisible);
        }

        private void UpdateCandidate(Candidate candidate, DesiredJobMemberModel memberModel)
        {
            // Desired job.

            candidate.DesiredJobTitle = memberModel.DesiredJobTitle;
            candidate.DesiredJobTypes = memberModel.DesiredJobTypes;

            // Salary.

            candidate.DesiredSalary = new Salary
            {
                Currency = Currency.AUD,
                LowerBound = memberModel.DesiredSalaryLowerBound,
                Rate = memberModel.DesiredSalaryRate
            };

            // Status.

            if (memberModel.Status != CandidateStatus.Unspecified)
                candidate.Status = memberModel.Status;

            // Relocation.

            candidate.RelocationPreference = memberModel.RelocationPreference;
            candidate.RelocationLocations = GetRelocationLocations(memberModel.RelocationCountryIds, memberModel.RelocationCountryLocationIds);
        }

        private static void UpdateResume(Resume resume, CareerObjectivesMemberModel memberModel)
        {
            resume.Objective = memberModel.Objective;
            resume.Summary = memberModel.Summary;
            resume.Skills = memberModel.Skills;
        }

        private void UpdateCandidate(Candidate candidate, Profession? recentProfession, Seniority? recentSeniority, IEnumerable<Guid> industryIds)
        {
            candidate.RecentProfession = recentProfession;
            candidate.RecentSeniority = recentSeniority;
            candidate.Industries = industryIds == null ? null : _industriesQuery.GetIndustries(industryIds);
        }

        private void Validate(ContactDetailsMemberModel memberModel)
        {
            memberModel.Prepare();

            // Gather all errors.

            var errors = memberModel.GetValidationErrors().ToList();

            // Validate the location separately.

            if (!string.IsNullOrEmpty(memberModel.Location))
            {
                var country = memberModel.CountryId == null
                    ? ActivityContext.Location.Country
                    : _locationQuery.GetCountry(memberModel.CountryId.Value);
                var location = _locationQuery.ResolveLocation(country, memberModel.Location);
                IValidator validator = new PostalSuburbValidator();
                if (!validator.IsValid(location))
                    errors = errors.Concat(validator.GetValidationErrors("Location")).ToList();
                else
                    memberModel.Location = location.ToString();
            }

            // Gender and date of birth are required.

            if (memberModel.Gender == Gender.Unspecified)
                errors.Add(new RequiredValidationError("Gender"));
            if (memberModel.DateOfBirth == null)
                errors.Add(new RequiredValidationError("DateOfBirth"));

            if (errors.Any())
                throw new ValidationErrorsException(errors);
        }

        private static void Validate(JobModel jobModel)
        {
            jobModel.Validate();
            if (jobModel.StartDate == null && jobModel.EndDate == null && (jobModel.IsCurrent == null || !jobModel.IsCurrent.Value))
                throw new ValidationErrorsException(new RequiredValidationError("Date"));
        }

        private static void UpdateJob(Resume resume, JobModel jobModel)
        {
            var job = (from j in resume.Jobs where j.Id == jobModel.Id select j).Single();
            job.Company = jobModel.Company;
            job.Title = jobModel.Title;
            job.Description = jobModel.Description;
            job.Dates = GetDates(jobModel);
        }

        private static void AddJob(Resume resume, JobModel jobModel)
        {
            var job = new Job
            {
                Id = jobModel.Id.Value,
                Company = jobModel.Company,
                Title = jobModel.Title,
                Description = jobModel.Description,
                Dates = GetDates(jobModel),
            };

            if (resume.Jobs == null)
                resume.Jobs = new List<Job> { job };
            else
                resume.Jobs.Add(job);
        }

        private static PartialDateRange GetDates(JobModel jobModel)
        {
            if (jobModel.StartDate == null && jobModel.EndDate == null && (jobModel.IsCurrent == null || !jobModel.IsCurrent.Value))
                return null;

            if (jobModel.StartDate != null)
            {
                return jobModel.EndDate == null
                    ? new PartialDateRange(jobModel.StartDate.Value)
                    : jobModel.StartDate.Value > jobModel.EndDate.Value
                        ? new PartialDateRange(jobModel.EndDate.Value, jobModel.StartDate.Value)
                        : new PartialDateRange(jobModel.StartDate.Value, jobModel.EndDate.Value);
            }
            
            return jobModel.EndDate != null
                ? new PartialDateRange(null, jobModel.EndDate.Value)
                : new PartialDateRange();
        }

        private static void RemoveJob(Resume resume, Guid jobId)
        {
            resume.Jobs = (from j in resume.Jobs where j.Id != jobId select j).ToList();
        }

        private static void UpdateCandidate(Candidate candidate, EducationLevel? highestEducationLevel)
        {
            candidate.HighestEducationLevel = highestEducationLevel;
        }

        private static void Validate(SchoolModel schoolModel)
        {
            schoolModel.Validate();
            if (schoolModel.EndDate == null && (schoolModel.IsCurrent == null || !schoolModel.IsCurrent.Value))
                throw new ValidationErrorsException(new RequiredValidationError("CompletionDate"));
        }

        private static void UpdateSchool(Resume resume, SchoolModel schoolModel)
        {
            var school = (from s in resume.Schools where s.Id == schoolModel.Id.Value select s).Single();
            school.City = schoolModel.City;
            school.Country = null;
            school.CompletionDate = GetCompletionDate(schoolModel);
            school.Degree = schoolModel.Degree;
            school.Description = schoolModel.Description;
            school.Institution = schoolModel.Institution;
            school.Major = schoolModel.Major;
        }

        private static void AddSchool(Resume resume, SchoolModel schoolModel)
        {
            var school = new School
            {
                Id = schoolModel.Id.Value,
                CompletionDate = GetCompletionDate(schoolModel),
                Degree = schoolModel.Degree,
                Major = schoolModel.Major,
                Institution = schoolModel.Institution,
                City = schoolModel.City,
                Description = schoolModel.Description,
            };

            if (resume.Schools == null)
                resume.Schools = new List<School> { school };
            else
                resume.Schools.Add(school);
        }

        private static PartialCompletionDate GetCompletionDate(SchoolModel schoolModel)
        {
            if (schoolModel.EndDate == null && (schoolModel.IsCurrent == null || !schoolModel.IsCurrent.Value))
                return null;

            return schoolModel.EndDate != null
                ? new PartialCompletionDate(schoolModel.EndDate.Value)
                : new PartialCompletionDate();
        }

        private static void RemoveSchool(Resume resume, Guid schoolId)
        {
            resume.Schools = (from s in resume.Schools where s.Id != schoolId select s).ToList();
        }

        private static void UpdateResume(Resume resume, OtherMemberModel memberModel)
        {
            resume.Courses = memberModel.Courses.ParseCourses();
            resume.Awards = memberModel.Awards.ParseAwards();
            resume.Professional = memberModel.Professional;
            resume.Interests = memberModel.Interests;
            resume.Affiliations = memberModel.Affiliations;
            resume.Other = memberModel.Other;
            resume.Referees = memberModel.Referees;
        }

        private void UpdateSuggestedJobs(Guid memberId, bool sendSuggestedJobs)
        {
            try
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

                var currentlySending = frequency != Frequency.Never;

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

        private IList<LocationReference> GetRelocationLocations(ICollection<int> relocationCountryIds, ICollection<int> relocationCountryLocationIds)
        {
            IList<LocationReference> locations = new List<LocationReference>();

            if (relocationCountryIds != null && relocationCountryIds.Count != 0)
            {
                foreach (var countryId in relocationCountryIds)
                    locations.Add(_locationQuery.ResolveLocation(_locationQuery.GetCountry(countryId), null));
            }

            if (relocationCountryLocationIds != null && relocationCountryLocationIds.Count != 0)
            {
                foreach (var locationId in relocationCountryLocationIds)
                    locations.Add(new LocationReference(_locationQuery.GetNamedLocation(locationId)));
            }

            return locations.Count == 0 ? null : locations;
        }

        private MemberStatusModel CreateProfileModel(IMember member, ICandidate candidate, IResume resume)
        {
            var lastUpdatedTime = _memberStatusQuery.GetLastUpdatedTime(member, candidate, resume); 

            return new MemberStatusModel
            {
                PercentComplete = _memberStatusQuery.GetPercentComplete(member, candidate, resume),
                Age = (DateTime.Now - lastUpdatedTime).Days,
                PromptForResumeUpdate = _memberStatusQuery.IsUpdateNeeded(member, candidate, resume),
                MemberStatus = _memberStatusQuery.GetMemberStatus(member, candidate, resume),
            };
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