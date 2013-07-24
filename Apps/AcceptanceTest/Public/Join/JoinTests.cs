using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Api.Models.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public abstract class JoinTests
        : WebTestClass
    {
        protected readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        protected readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        protected readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        protected readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly IReferralsQuery _referralsQuery = Resolve<IReferralsQuery>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        protected const string FirstName = "Waylon";
        protected const string LastName = "Smithers";
        protected const string EmailAddress = "waylon@test.linkme.net.au";
        protected const string Country = "Australia";
        protected const string Location = "Norlane VIC 3214";
        protected const string MobilePhoneNumber = "1111 1111";
        protected const string Password = "password";
        protected const decimal SalaryLowerBound = 100000;
        protected static readonly SalaryRate SalaryRate = SalaryRate.Year;
        protected static readonly Gender Gender = Gender.Female;
        protected static readonly PartialDate? DateOfBirth = new PartialDate(1970, 1);

        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _personalDetailsUrl;
        private ReadOnlyUrl _jobDetailsUrl;
        private ReadOnlyUrl _activateUrl;
        protected ReadOnlyUrl _uploadUrl;
        protected ReadOnlyUrl _parseUrl;

        protected string _joinFormId;

        protected string _personalDetailsFormId;
        protected HtmlTextBoxTester _firstNameTextBox;
        protected HtmlTextBoxTester _lastNameTextBox;
        protected HtmlTextBoxTester _emailAddressTextBox;
        protected HtmlTextBoxTester _phoneNumberTextBox;
        protected HtmlRadioButtonTester _mobileRadioButton;
        protected HtmlRadioButtonTester _homeRadioButton;
        protected HtmlRadioButtonTester _workRadioButton;
        protected HtmlPasswordTester _passwordTextBox;
        protected HtmlPasswordTester _confirmPasswordTextBox;
        protected HtmlCheckBoxTester _acceptTermsCheckBox;
        protected HtmlDropDownListTester _countryIdDropDownList;
        protected HtmlTextBoxTester _locationTextBox;
        protected HtmlRadioButtonTester _notLookingRadioButton;
        protected HtmlRadioButtonTester _openToOffersRadioButton;
        protected HtmlRadioButtonTester _activelyLookingRadioButton;
        protected HtmlRadioButtonTester _availableNowRadioButton;
        protected HtmlTextBoxTester _salaryLowerBoundTextBox;
        protected HtmlRadioButtonTester _salaryRateYearRadioBox;
        protected HtmlRadioButtonTester _salaryRateHourRadioBox;
        protected HtmlCheckBoxTester _resumeVisibilityCheckBox;
        protected HtmlCheckBoxTester _nameVisibilityCheckBox;
        protected HtmlCheckBoxTester _phoneNumbersVisibilityCheckBox;
        protected HtmlCheckBoxTester _recentEmployersVisibilityCheckBox;
        protected HtmlCheckBoxTester _notSalaryVisibilityCheckBox;

        protected string _jobDetailsFormId;
        protected HtmlTextBoxTester _jobTitleTestBox;
        protected HtmlTextBoxTester _jobCompanyTextBox;
        protected HtmlDropDownListTester _recentProfessionDropDownList;
        protected HtmlDropDownListTester _recentSeniorityDropDownList;
        protected HtmlDropDownListTester _highestEducationLevelDropDownList;
        protected HtmlListBoxTester _industryIdsListBox;
        protected HtmlTextBoxTester _desiredJobTitleTextBox;
        protected HtmlCheckBoxTester _fullTimeCheckBox;
        protected HtmlCheckBoxTester _partTimeCheckBox;
        protected HtmlCheckBoxTester _contractCheckBox;
        protected HtmlCheckBoxTester _tempCheckBox;
        protected HtmlCheckBoxTester _jobShareCheckBox;
        protected HtmlCheckBoxTester _aboriginalCheckBox;
        protected HtmlCheckBoxTester _torresIslanderCheckBox;
        protected HtmlRadioButtonTester _maleRadioButton;
        protected HtmlRadioButtonTester _femaleRadioButton;
        protected HtmlDropDownListTester _dateOfBirthMonthDropDownList;
        protected HtmlDropDownListTester _dateOfBirthYearDropDownList;
        protected HtmlTextBoxTester _citizenshipTextBox;
        protected HtmlRadioButtonTester _notApplicableRadioButton;
        protected HtmlRadioButtonTester _citizenRadioButton;
        protected HtmlRadioButtonTester _unrestrictedWorkVisaRadioButton;
        protected HtmlRadioButtonTester _restrictedWorkVisaRadioButton;
        protected HtmlRadioButtonTester _noWorkVisaRadioButton;
        protected HtmlTextBoxTester _secondaryPhoneNumberTextBox;
        protected HtmlRadioButtonTester _secondaryMobileRadioButton;
        protected HtmlRadioButtonTester _secondaryHomeRadioButton;
        protected HtmlRadioButtonTester _secondaryWorkRadioButton;
        protected HtmlTextBoxTester _secondaryEmailAddressTextBox;
        protected HtmlRadioButtonTester _relocationPreferenceNoRadioButton;
        protected HtmlRadioButtonTester _relocationPreferenceYesRadioButton;
        protected HtmlRadioButtonTester _relocationPreferenceWouldConsiderRadioButton;
        protected HtmlListBoxTester _relocationCountryIdsListBox;
        protected HtmlListBoxTester _relocationCountryLocationIdsListBox;
        protected HtmlDropDownListTester _externalReferralSourceIdDropDownListTester;
        protected HtmlCheckBoxTester _sendSuggestedJobsCheckBox;

        private string _homeJoinFormId;
        private HtmlTextBoxTester _homeFirstNameTextBox;
        private HtmlTextBoxTester _homeLastNameTextBox;
        private HtmlTextBoxTester _homeEmailAddressTextBox;
        private HtmlPasswordTester _homeJoinPasswordTextBox;
        private HtmlPasswordTester _homeJoinConfirmPasswordTextBox;
        private HtmlCheckBoxTester _homeAcceptTermsCheckBox;

        [TestInitialize]
        public void JoinTestsInitialize()
        {
            _joinUrl = new ReadOnlyApplicationUrl(true, "~/join");
            _personalDetailsUrl = new ReadOnlyApplicationUrl(true, "~/join/personaldetails");
            _jobDetailsUrl = new ReadOnlyApplicationUrl(true, "~/join/jobdetails");
            _activateUrl = new ReadOnlyApplicationUrl(true, "~/join/activate");
            _uploadUrl = new ReadOnlyApplicationUrl(true, "~/api/resumes/upload");
            _parseUrl = new ReadOnlyApplicationUrl(true, "~/api/resumes/parse");

            _joinFormId = "JoinForm";

            _personalDetailsFormId = "PersonalDetailsForm";
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _mobileRadioButton = new HtmlRadioButtonTester(Browser, "Mobile");
            _homeRadioButton = new HtmlRadioButtonTester(Browser, "Home");
            _workRadioButton = new HtmlRadioButtonTester(Browser, "Work");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmPassword");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _countryIdDropDownList = new HtmlDropDownListTester(Browser, "CountryId");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _notLookingRadioButton = new HtmlRadioButtonTester(Browser, "NotLooking");
            _openToOffersRadioButton = new HtmlRadioButtonTester(Browser, "OpenToOffers");
            _activelyLookingRadioButton = new HtmlRadioButtonTester(Browser, "ActivelyLooking");
            _availableNowRadioButton = new HtmlRadioButtonTester(Browser, "AvailableNow");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _salaryRateYearRadioBox = new HtmlRadioButtonTester(Browser, "SalaryRateYear");
            _salaryRateHourRadioBox = new HtmlRadioButtonTester(Browser, "SalaryRateHour");
            _resumeVisibilityCheckBox = new HtmlCheckBoxTester(Browser, "ResumeVisibility");
            _nameVisibilityCheckBox = new HtmlCheckBoxTester(Browser, "NameVisibility");
            _phoneNumbersVisibilityCheckBox = new HtmlCheckBoxTester(Browser, "PhoneNumbersVisibility");
            _recentEmployersVisibilityCheckBox = new HtmlCheckBoxTester(Browser, "RecentEmployersVisibility");
            _notSalaryVisibilityCheckBox = new HtmlCheckBoxTester(Browser, "NotSalaryVisibility");

            _jobDetailsFormId = "JobDetailsForm";
            _jobTitleTestBox = new HtmlTextBoxTester(Browser, "JobTitle");
            _jobCompanyTextBox = new HtmlTextBoxTester(Browser, "JobCompany");
            _recentProfessionDropDownList = new HtmlDropDownListTester(Browser, "RecentProfession");
            _recentSeniorityDropDownList = new HtmlDropDownListTester(Browser, "RecentSeniority");
            _highestEducationLevelDropDownList = new HtmlDropDownListTester(Browser, "HighestEducationLevel");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _desiredJobTitleTextBox = new HtmlTextBoxTester(Browser, "DesiredJobTitle");
            _fullTimeCheckBox = new HtmlCheckBoxTester(Browser, "FullTime");
            _partTimeCheckBox = new HtmlCheckBoxTester(Browser, "PartTime");
            _contractCheckBox = new HtmlCheckBoxTester(Browser, "Contract");
            _tempCheckBox = new HtmlCheckBoxTester(Browser, "Temp");
            _jobShareCheckBox = new HtmlCheckBoxTester(Browser, "JobShare");
            _aboriginalCheckBox = new HtmlCheckBoxTester(Browser, "Aboriginal");
            _torresIslanderCheckBox = new HtmlCheckBoxTester(Browser, "TorresIslander");
            _maleRadioButton = new HtmlRadioButtonTester(Browser, "Male");
            _femaleRadioButton = new HtmlRadioButtonTester(Browser, "Female");
            _dateOfBirthMonthDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthMonth");
            _dateOfBirthYearDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthYear");
            _citizenshipTextBox = new HtmlTextBoxTester(Browser, "Citizenship");
            _notApplicableRadioButton = new HtmlRadioButtonTester(Browser, "NotApplicable");
            _citizenRadioButton = new HtmlRadioButtonTester(Browser, "Citizen");
            _unrestrictedWorkVisaRadioButton = new HtmlRadioButtonTester(Browser, "UnrestrictedWorkVisa");
            _restrictedWorkVisaRadioButton = new HtmlRadioButtonTester(Browser, "RestrictedWorkVisa");
            _noWorkVisaRadioButton = new HtmlRadioButtonTester(Browser, "NoWorkVisa");
            _secondaryPhoneNumberTextBox = new HtmlTextBoxTester(Browser, "SecondaryPhoneNumber");
            _secondaryMobileRadioButton = new HtmlRadioButtonTester(Browser, "Mobile");
            _secondaryHomeRadioButton = new HtmlRadioButtonTester(Browser, "Home");
            _secondaryWorkRadioButton = new HtmlRadioButtonTester(Browser, "Work");
            _secondaryEmailAddressTextBox = new HtmlTextBoxTester(Browser, "SecondaryEmailAddress");
            _relocationPreferenceNoRadioButton = new HtmlRadioButtonTester(Browser, "RelocationPreferenceNo");
            _relocationPreferenceYesRadioButton = new HtmlRadioButtonTester(Browser, "RelocationPreferenceYes");
            _relocationPreferenceWouldConsiderRadioButton = new HtmlRadioButtonTester(Browser, "RelocationPreferenceWouldConsider");
            _relocationCountryIdsListBox = new HtmlListBoxTester(Browser, "RelocationCountryIds");
            _relocationCountryLocationIdsListBox = new HtmlListBoxTester(Browser, "RelocationCountryLocationIds");
            _externalReferralSourceIdDropDownListTester = new HtmlDropDownListTester(Browser, "ExternalReferralSourceId");
            _sendSuggestedJobsCheckBox = new HtmlCheckBoxTester(Browser, "SendSuggestedJobs");

            _homeJoinFormId = "JoinForm";
            _homeFirstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _homeLastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _homeEmailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _homeJoinPasswordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _homeJoinConfirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _homeAcceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
        }

        protected void Select(Country country)
        {
            _countryIdDropDownList.SelectedValue = country.Id.ToString(CultureInfo.InvariantCulture);
        }

        protected void Select(Profession? profession)
        {
            _recentProfessionDropDownList.SelectedValue = profession == null ? string.Empty : profession.Value.ToString();
        }

        protected void Select(Seniority? seniority)
        {
            _recentSeniorityDropDownList.SelectedValue = seniority == null ? string.Empty : seniority.Value.ToString();
        }

        protected void Select(EducationLevel? educationLevel)
        {
            _highestEducationLevelDropDownList.SelectedValue = educationLevel == null ? string.Empty : educationLevel.Value.ToString();
        }

        protected void Select(IEnumerable<Industry> industries)
        {
            // Clear all out first.

            _industryIdsListBox.SelectedValues = new List<string>();

            // Now select.

            if (industries != null)
                _industryIdsListBox.SelectedValues = (from i in industries select i.Id.ToString()).ToList();
        }

        protected void SelectRelocationCountryIds(IEnumerable<LocationReference> locations)
        {
            // Clear all out first.

            _relocationCountryIdsListBox.SelectedValues = new List<string>();

            // Now select.

            if (locations != null)
                _relocationCountryIdsListBox.SelectedValues = (from l in locations where l.IsCountry select l.Country.Id.ToString(CultureInfo.InvariantCulture)).ToList();
        }

        protected void SelectRelocationCountryLocationIds(IEnumerable<LocationReference> locations)
        {
            // Clear all out first.

            _relocationCountryLocationIdsListBox.SelectedValues = new List<string>();

            // Now select.

            if (locations != null)
                _relocationCountryLocationIdsListBox.SelectedValues = (from l in locations where !l.IsCountry select l.NamedLocation.Id.ToString(CultureInfo.InvariantCulture)).ToList();
        }

        protected void Select(PartialDate? dateOfBirth)
        {
            _dateOfBirthMonthDropDownList.SelectedValue = dateOfBirth == null || dateOfBirth.Value.Month == null ? string.Empty : dateOfBirth.Value.Month.Value.ToString(CultureInfo.InvariantCulture);
            _dateOfBirthYearDropDownList.SelectedValue = dateOfBirth == null ? string.Empty : dateOfBirth.Value.Year.ToString(CultureInfo.InvariantCulture);
        }

        protected void Select(int? referralSourceId)
        {
            _externalReferralSourceIdDropDownListTester.SelectedValue = referralSourceId == null ? "" : referralSourceId.Value.ToString(CultureInfo.InvariantCulture);
        }

        protected PartialDate? GetSelectedDateOfBirth()
        {
            var month = _dateOfBirthMonthDropDownList.SelectedValue;
            var year = _dateOfBirthYearDropDownList.SelectedValue;

            if (string.IsNullOrEmpty(year))
                return null;

            return string.IsNullOrEmpty(month)
                ? new PartialDate(int.Parse(year))
                : new PartialDate(int.Parse(year), int.Parse(month));
        }

        protected IList<Guid> GetSelectedIndustryIds()
        {
            return (from i in _industryIdsListBox.Items where i.IsSelected select new Guid(i.Value)).ToList();
        }

        protected IList<int> GetSelectedRelocationCountryIds()
        {
            return (from i in _relocationCountryIdsListBox.Items where i.IsSelected select int.Parse(i.Value)).ToList();
        }

        protected IList<int> GetSelectedRelocationCountryLocationIds()
        {
            return (from i in _relocationCountryLocationIdsListBox.Items where i.IsSelected select int.Parse(i.Value)).ToList();
        }

        protected void SetVisibility(ProfessionalVisibility visibility)
        {
            _resumeVisibilityCheckBox.IsChecked = visibility.IsFlagSet(ProfessionalVisibility.Resume);

            if (visibility.IsFlagSet(ProfessionalVisibility.Resume))
            {
                _nameVisibilityCheckBox.IsChecked = visibility.IsFlagSet(ProfessionalVisibility.Name);
                _phoneNumbersVisibilityCheckBox.IsChecked = visibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers);
                _recentEmployersVisibilityCheckBox.IsChecked = visibility.IsFlagSet(ProfessionalVisibility.RecentEmployers);
            }
            else
            {
                _nameVisibilityCheckBox.IsChecked = false;
                _phoneNumbersVisibilityCheckBox.IsChecked = false;
                _recentEmployersVisibilityCheckBox.IsChecked = false;
            }

            _notSalaryVisibilityCheckBox.IsChecked = !visibility.IsFlagSet(ProfessionalVisibility.Salary);
        }

        protected Member CreateMember(string firstName, string lastName, string emailAddress)
        {
            return new Member
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress, IsVerified = false } },
                IsEnabled = true,
                VisibilitySettings = new VisibilitySettings(),
                Address = new Address
                {
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), null),
                },
            };
        }

        protected void UpdateMember(Member member, string phoneNumber, PhoneNumberType phoneNumberType, string location)
        {
            member.Address = new Address
            {
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), location),
            };
            member.PhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber {Number = phoneNumber, Type = phoneNumberType},
            };
        }

        protected void UpdateMember(Member member, Gender gender, PartialDate? dateOfBirth)
        {
            member.Gender = gender;
            member.DateOfBirth = dateOfBirth;
        }

        protected void UpdateCandidate(Candidate candidate, decimal salaryLowerBound, SalaryRate salaryRate)
        {
            candidate.DesiredSalary = new Salary { LowerBound = salaryLowerBound, Rate = salaryRate, Currency = Currency.AUD };
        }

        protected void UpdateResume(Resume resume, IEnumerable<Job> jobs)
        {
            resume.Jobs = (from j in jobs select j.Clone()).ToList();
        }

        protected Candidate CreateCandidate()
        {
            return new Candidate
            {
                Status = CandidateStatus.OpenToOffers,
            };
        }

        protected Resume CreateResume()
        {
            return new Resume();
        }

        protected void AssertNoMember(string emailAddress)
        {
            Assert.IsNull(_loginCredentialsQuery.GetUserId(emailAddress));
        }

        protected void AssertMember(IMember expectedMember, ICandidate expectedCandidate, IResume expectedResume, Guid? expectedFileReferenceId, bool expectedSendSuggestedJobs, int? expectedReferralSourceId)
        {
            var memberId = _loginCredentialsQuery.GetUserId(expectedMember.EmailAddresses[0].Address);

            Assert.IsNotNull(memberId);
            var credentials = _loginCredentialsQuery.GetCredentials(memberId.Value);
            var member = _membersQuery.GetMember(memberId.Value);
            var candidate = _candidatesQuery.GetCandidate(memberId.Value);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            var fileReference = resume == null ? null : _candidateResumeFilesQuery.GetResumeFile(resume.Id);
            var fileReferenceId = fileReference == null ? (Guid?)null : fileReference.FileReferenceId;

            AssertCredentials(credentials);
            AssertMember(expectedMember, member);
            AssertCandidate(expectedCandidate, candidate);
            AssertResume(expectedResume, resume);
            Assert.AreEqual(expectedFileReferenceId, fileReferenceId);
            AssertExternalReferral(expectedReferralSourceId, memberId.Value);
            AssertSuggestedJobs(expectedSendSuggestedJobs, memberId.Value);
        }

        private static void AssertCredentials(LoginCredentials credentials)
        {
            Assert.IsFalse(credentials.MustChangePassword);
        }

        private static void AssertMember(IMember expectedMember, Member member)
        {
            Assert.IsFalse(member.IsActivated);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsNull(member.PhotoId);

            Assert.AreEqual(expectedMember.FirstName, member.FirstName);
            Assert.AreEqual(expectedMember.LastName, member.LastName);
            Assert.AreEqual(expectedMember.DateOfBirth, member.DateOfBirth);
            Assert.AreEqual(expectedMember.EthnicStatus, member.EthnicStatus);
            Assert.AreEqual(expectedMember.Gender, member.Gender);
            Assert.AreEqual(expectedMember.IsActivated, member.IsActivated);
            Assert.AreEqual(expectedMember.IsEnabled, member.IsEnabled);
            Assert.AreEqual(expectedMember.PhotoId, member.PhotoId);
            Assert.AreEqual(expectedMember.VisibilitySettings.Professional.EmploymentVisibility, member.VisibilitySettings.Professional.EmploymentVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Professional.PublicVisibility, member.VisibilitySettings.Professional.PublicVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.FirstDegreeVisibility, member.VisibilitySettings.Personal.FirstDegreeVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.SecondDegreeVisibility, member.VisibilitySettings.Personal.SecondDegreeVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.PublicVisibility, member.VisibilitySettings.Personal.PublicVisibility);

            if (expectedMember.EmailAddresses == null)
            {
                Assert.IsNull(member.EmailAddresses);
            }
            else
            {
                Assert.IsNotNull(member.EmailAddresses);
                Assert.AreEqual(expectedMember.EmailAddresses.Count, member.EmailAddresses.Count);
                for (var index = 0; index < expectedMember.EmailAddresses.Count; ++index)
                    Assert.AreEqual(expectedMember.EmailAddresses[index], member.EmailAddresses[index]);
            }

            if (expectedMember.PhoneNumbers == null)
            {
                Assert.IsNull(member.PhoneNumbers);
            }
            else
            {
                Assert.IsNotNull(member.PhoneNumbers);
                Assert.AreEqual(expectedMember.PhoneNumbers.Count, member.PhoneNumbers.Count);
                for (var index = 0; index < expectedMember.PhoneNumbers.Count; ++index)
                    Assert.AreEqual(expectedMember.PhoneNumbers[index], member.PhoneNumbers[index]);
            }

            if (expectedMember.Address == null)
            {
                Assert.IsNull(member.Address);
            }
            else
            {
                if (expectedMember.Address.Location == null)
                    Assert.IsNull(member.Address.Location);
                else
                    Assert.AreEqual(expectedMember.Address.Location, member.Address.Location);
            }
        }

        private static void AssertCandidate(ICandidate expectedCandidate, Candidate candidate)
        {
            Assert.AreEqual(expectedCandidate.Status, candidate.Status);
            Assert.AreEqual(expectedCandidate.DesiredJobTitle, candidate.DesiredJobTitle);
            Assert.AreEqual(expectedCandidate.DesiredJobTypes, candidate.DesiredJobTypes);
            Assert.AreEqual(expectedCandidate.DesiredSalary, candidate.DesiredSalary);
            Assert.AreEqual(expectedCandidate.HighestEducationLevel, candidate.HighestEducationLevel);
            Assert.AreEqual(expectedCandidate.RecentProfession, candidate.RecentProfession);
            Assert.AreEqual(expectedCandidate.RecentSeniority, candidate.RecentSeniority);
            Assert.AreEqual(expectedCandidate.VisaStatus, candidate.VisaStatus);

            if (expectedCandidate.Industries == null)
            {
                Assert.IsNull(candidate.Industries);
            }
            else
            {
                Assert.AreEqual(expectedCandidate.Industries.Count, candidate.Industries.Count);
                foreach (var expectedIndustry in expectedCandidate.Industries)
                {
                    var expectedIndustryId = expectedIndustry.Id;
                    Assert.IsTrue((from i in candidate.Industries where i.Id == expectedIndustryId select i).Any());
                }
            }

            Assert.AreEqual(expectedCandidate.RelocationPreference, candidate.RelocationPreference);
            if (expectedCandidate.RelocationLocations == null)
            {
                Assert.IsNull(candidate.RelocationLocations);
            }
            else
            {
                Assert.AreEqual(expectedCandidate.RelocationLocations.Count, candidate.RelocationLocations.Count);
                foreach (var expectedLocation in expectedCandidate.RelocationLocations)
                {
                    var location = expectedLocation;
                    Assert.IsTrue((from l in candidate.RelocationLocations where Equals(l, location) select l).Any());
                }
            }
        }

        private static void AssertResume(IResume expectedResume, Resume resume)
        {
            if (expectedResume.IsEmpty)
            {
                Assert.IsTrue(resume == null || resume.IsEmpty);
            }
            else
            {
                Assert.IsNotNull(resume);

                Assert.AreEqual(expectedResume.Skills, resume.Skills);
                Assert.AreEqual(expectedResume.Objective, resume.Objective);
                Assert.AreEqual(expectedResume.Summary, resume.Summary);
                Assert.AreEqual(expectedResume.Other, resume.Other);
                Assert.AreEqual(expectedResume.Citizenship, resume.Citizenship);
                Assert.AreEqual(expectedResume.Affiliations, resume.Affiliations);
                Assert.AreEqual(expectedResume.Professional, resume.Professional);
                Assert.AreEqual(expectedResume.Interests, resume.Interests);
                Assert.AreEqual(expectedResume.Referees, resume.Referees);

                if (expectedResume.Courses == null)
                {
                    Assert.IsNull(resume.Courses);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Courses.Count, resume.Courses.Count);
                    for (var index = 0; index < expectedResume.Courses.Count; ++index)
                        Assert.AreEqual(expectedResume.Courses[index], resume.Courses[index]);
                }

                if (expectedResume.Awards == null)
                {
                    Assert.IsNull(resume.Awards);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Awards.Count, resume.Awards.Count);
                    for (var index = 0; index < expectedResume.Awards.Count; ++index)
                        Assert.AreEqual(expectedResume.Awards[index], resume.Awards[index]);
                }

                if (expectedResume.Schools == null)
                {
                    Assert.IsNull(resume.Schools);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Schools.Count, resume.Schools.Count);
                    for (var index = 0; index < expectedResume.Schools.Count; ++index)
                        AssertSchool(expectedResume.Schools[index], resume.Schools[index]);
                }

                if (expectedResume.Jobs == null)
                {
                    Assert.IsNull(resume.Jobs);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Jobs.Count, resume.Jobs.Count);
                    for (var index = 0; index < expectedResume.Jobs.Count; ++index)
                        AssertJob(expectedResume.Jobs[index], resume.Jobs[index]);
                }
            }
        }

        private static void AssertJob(IJob expectedJob, IJob job)
        {
            Assert.AreEqual(expectedJob.Title, job.Title);
            Assert.AreEqual(expectedJob.Description, job.Description);
            Assert.AreEqual(expectedJob.Company, job.Company);
            Assert.AreEqual(expectedJob.Dates, job.Dates);
        }

        private static void AssertSchool(ISchool expectedSchool, ISchool school)
        {
            Assert.AreEqual(expectedSchool.CompletionDate, school.CompletionDate);
            Assert.AreEqual(expectedSchool.Institution, school.Institution);
            Assert.AreEqual(expectedSchool.Degree, school.Degree);
            Assert.AreEqual(expectedSchool.Major, school.Major);
            Assert.AreEqual(expectedSchool.Description, school.Description);
            Assert.AreEqual(expectedSchool.City, school.City);
            Assert.AreEqual(expectedSchool.Country, school.Country);
        }

        private void AssertExternalReferral(int? expectedReferralSourceId, Guid memberId)
        {
            var referral = _referralsQuery.GetExternalReferral(memberId);
            if (expectedReferralSourceId == null)
            {
                Assert.IsNull(referral);
            }
            else
            {
                Assert.IsNotNull(referral);
                Assert.AreEqual(expectedReferralSourceId.Value, referral.SourceId);
            }
        }

        protected void AssertAffiliationReferral(string expectedPromotionCode, Guid memberId)
        {
            var referral = _referralsQuery.GetAffiliationReferral(memberId);
            Assert.IsNotNull(referral);
            Assert.AreEqual(expectedPromotionCode, referral.PromotionCode);
        }

        private void AssertSuggestedJobs(bool expectedSendSuggestedJobs, Guid memberId)
        {
            var category = _settingsQuery.GetCategory("SuggestedJobs");
            var frequency = Frequency.Never;
            var settings = _settingsQuery.GetSettings(memberId);

            if (settings != null)
            {
                var setting = settings.CategorySettings.FirstOrDefault(c => c.CategoryId == category.Id);

                if (setting != null && setting.Frequency.HasValue)
                    frequency = setting.Frequency.Value;
            }

            Assert.AreEqual(expectedSendSuggestedJobs, frequency != Frequency.Never);
        }

        protected Guid UploadResume(TestResume resume)
        {
            Guid fileReferenceId;

            // Upload the file.

            const string fileName = "Resume.doc";
            using (var tempFiles = _filesCommand.SaveTempFile(resume.GetData(), fileName))
            {
                var files = new NameValueCollection { { "file", tempFiles.FilePaths[0] } };
                fileReferenceId = new JavaScriptSerializer().Deserialize<JsonResumeModel>(Post(_uploadUrl, null, files)).Id;
            }

            // Parse the file.

            var parsedResumeId = new JavaScriptSerializer().Deserialize<JsonParsedResumeModel>(Post(_parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } })).Id;

            // Submit the form.

            Get(_joinUrl);
            SubmitJoin(fileReferenceId, parsedResumeId);
            return fileReferenceId;
        }

        protected void SubmitJoin()
        {
            SubmitJoin(null, null);
        }

        protected void SubmitJoin(Guid? fileReferenceId, Guid? parsedResumeId)
        {
            AssertPath(_joinUrl);
            if (fileReferenceId != null)
                Browser.SetFormVariable(_joinFormId, "fileReferenceId", fileReferenceId.Value.ToString(), false);
            if (parsedResumeId != null)
                Browser.SetFormVariable(_joinFormId, "parsedResumeId", parsedResumeId.Value.ToString(), false);
            Browser.Submit(_joinFormId);
        }

        protected void SubmitPersonalDetails(Guid instanceId, IMember member, ICandidate candidate, string password)
        {
            SubmitPersonalDetails(instanceId, member, candidate, password, password, true);
        }

        protected void SubmitPersonalDetails(Guid instanceId, IMember member, ICandidate candidate, string password, string confirmPassword, bool acceptTerms)
        {
            EnterPersonalDetails(instanceId, member, candidate);

            _passwordTextBox.Text = password;
            _confirmPasswordTextBox.Text = confirmPassword;
            _acceptTermsCheckBox.IsChecked = acceptTerms;

            Browser.Submit(_personalDetailsFormId);
        }

        protected void SubmitPersonalDetails(Guid instanceId, IMember member, ICandidate candidate)
        {
            EnterPersonalDetails(instanceId, member, candidate);
            Browser.Submit(_personalDetailsFormId);
        }

        private void EnterPersonalDetails(Guid instanceId, IMember member, ICandidate candidate)
        {
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            _firstNameTextBox.Text = member.FirstName;
            _lastNameTextBox.Text = member.LastName;
            _emailAddressTextBox.Text = member.GetPrimaryEmailAddress().Address;

            var phoneNumber = member.GetPrimaryPhoneNumber();
            _phoneNumberTextBox.Text = phoneNumber == null ? string.Empty : phoneNumber.Number;

            if (phoneNumber != null)
            {
                switch (phoneNumber.Type)
                {
                    case PhoneNumberType.Mobile:
                        _mobileRadioButton.IsChecked = true;
                        break;

                    case PhoneNumberType.Home:
                        _homeRadioButton.IsChecked = true;
                        break;

                    case PhoneNumberType.Work:
                        _workRadioButton.IsChecked = true;
                        break;
                }
            }

            Select(member.Address.Location.Country);
            _locationTextBox.Text = member.Address.Location.ToString();

            switch (candidate.Status)
            {
                case CandidateStatus.NotLooking:
                    _notLookingRadioButton.IsChecked = true;
                    break;

                case CandidateStatus.OpenToOffers:
                    _openToOffersRadioButton.IsChecked = true;
                    break;

                case CandidateStatus.ActivelyLooking:
                    _activelyLookingRadioButton.IsChecked = true;
                    break;

                case CandidateStatus.AvailableNow:
                    _availableNowRadioButton.IsChecked = true;
                    break;
            }

            _salaryLowerBoundTextBox.Text = candidate.DesiredSalary == null || candidate.DesiredSalary.LowerBound == null
                ? null
                : candidate.DesiredSalary.LowerBound.ToString();

            if (candidate.DesiredSalary != null && candidate.DesiredSalary.Rate == SalaryRate.Hour)
                _salaryRateHourRadioBox.IsChecked = true;
            else
                _salaryRateYearRadioBox.IsChecked = true;

            SetVisibility(member.VisibilitySettings.Professional.EmploymentVisibility);
        }

        protected void AssertPersonalDetails(Guid instanceId, IMember member, ICandidate candidate)
        {
            AssertAllPersonalDetails(instanceId, member, candidate);
            Assert.IsFalse(_passwordTextBox.IsVisible);
            Assert.IsFalse(_confirmPasswordTextBox.IsVisible);
            Assert.IsFalse(_acceptTermsCheckBox.IsVisible);
        }

        protected void AssertPersonalDetails(Guid instanceId, IMember member, ICandidate candidate, string password, string confirmPassword, bool acceptTerms)
        {
            AssertAllPersonalDetails(instanceId, member, candidate);
            Assert.AreEqual(password ?? string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(confirmPassword ?? string.Empty, _confirmPasswordTextBox.Text);
            Assert.AreEqual(acceptTerms, _acceptTermsCheckBox.IsChecked);
        }

        private void AssertAllPersonalDetails(Guid instanceId, IMember member, ICandidate candidate)
        {
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            // Names.

            Assert.AreEqual(member.FirstName ?? string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(member.LastName ?? string.Empty, _lastNameTextBox.Text);

            // Email address.

            Assert.AreEqual(member.EmailAddresses == null ? string.Empty : member.GetPrimaryEmailAddress().Address ?? string.Empty, _emailAddressTextBox.Text);

            // Phone number.

            var phoneNumber = member.GetPrimaryPhoneNumber();
            Assert.AreEqual(phoneNumber == null ? string.Empty : phoneNumber.Number, _phoneNumberTextBox.Text);
            Assert.AreEqual(phoneNumber == null || phoneNumber.Type == PhoneNumberType.Mobile, _mobileRadioButton.IsChecked);
            Assert.AreEqual(phoneNumber != null && phoneNumber.Type == PhoneNumberType.Home, _homeRadioButton.IsChecked);
            Assert.AreEqual(phoneNumber != null && phoneNumber.Type == PhoneNumberType.Work, _workRadioButton.IsChecked);

            // Location.

            Assert.AreEqual(member.Address == null ? _locationQuery.GetCountry(Country).Id.ToString(CultureInfo.InvariantCulture) : member.Address.Location.Country.Id.ToString(CultureInfo.InvariantCulture), _countryIdDropDownList.SelectedItem.Value);
            Assert.AreEqual(member.Address == null ? string.Empty : member.Address.Location.ToString(), _locationTextBox.Text);

            // Status.

            Assert.AreEqual(candidate.Status == CandidateStatus.NotLooking, _notLookingRadioButton.IsChecked);
            Assert.AreEqual(candidate.Status == CandidateStatus.OpenToOffers, _openToOffersRadioButton.IsChecked);
            Assert.AreEqual(candidate.Status == CandidateStatus.ActivelyLooking, _activelyLookingRadioButton.IsChecked);
            Assert.AreEqual(candidate.Status == CandidateStatus.AvailableNow, _availableNowRadioButton.IsChecked);

            // Salary.

            Assert.AreEqual(candidate.DesiredSalary == null || candidate.DesiredSalary.LowerBound == null ? string.Empty : candidate.DesiredSalary.LowerBound.ToString(), _salaryLowerBoundTextBox.Text);
            Assert.AreEqual(candidate.DesiredSalary == null || candidate.DesiredSalary.Rate == SalaryRate.Year, _salaryRateYearRadioBox.IsChecked);
            Assert.AreEqual(candidate.DesiredSalary != null && candidate.DesiredSalary.Rate == SalaryRate.Hour, _salaryRateHourRadioBox.IsChecked);

            // Visibility.

            var visibility = member.VisibilitySettings.Professional.EmploymentVisibility;
            Assert.AreEqual(visibility.IsFlagSet(ProfessionalVisibility.Resume), _resumeVisibilityCheckBox.IsChecked);
            Assert.AreEqual(visibility.IsFlagSet(ProfessionalVisibility.Resume) && visibility.IsFlagSet(ProfessionalVisibility.Name), _nameVisibilityCheckBox.IsChecked);
            Assert.AreEqual(visibility.IsFlagSet(ProfessionalVisibility.Resume) && visibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers), _phoneNumbersVisibilityCheckBox.IsChecked);
            Assert.AreEqual(visibility.IsFlagSet(ProfessionalVisibility.Resume) && visibility.IsFlagSet(ProfessionalVisibility.RecentEmployers), _recentEmployersVisibilityCheckBox.IsChecked);
            Assert.AreEqual(!visibility.IsFlagSet(ProfessionalVisibility.Salary), _notSalaryVisibilityCheckBox.IsChecked);
        }

        protected void SubmitJobDetails(Guid instanceId, IMember member, ICandidate candidate, IResume resume, bool sendSuggestedJobs, int? referralSourceId, bool resumeUploaded)
        {
            EnterJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, resumeUploaded);
            Browser.Submit(_jobDetailsFormId);
        }

        protected void EnterJobDetails(Guid instanceId, IMember member, ICandidate candidate, IResume resume, bool sendSuggestedJobs, int? referralSourceId, bool resumeUploaded)
        {
            AssertUrl(GetJobDetailsUrl(instanceId));

            if (!resumeUploaded)
            {
                _jobTitleTestBox.Text = resume.Jobs == null || resume.Jobs.Count == 0 ? null : resume.Jobs[0].Title;
                _jobCompanyTextBox.Text = resume.Jobs == null || resume.Jobs.Count == 0 ? null : resume.Jobs[0].Company;
            }

            Select(candidate.RecentProfession);
            Select(candidate.RecentSeniority);
            Select(candidate.HighestEducationLevel);
            Select(candidate.Industries);

            _desiredJobTitleTextBox.Text = candidate.DesiredJobTitle;

            _fullTimeCheckBox.IsChecked = candidate.DesiredJobTypes.IsFlagSet(JobTypes.FullTime);
            _partTimeCheckBox.IsChecked = candidate.DesiredJobTypes.IsFlagSet(JobTypes.PartTime);
            _contractCheckBox.IsChecked = candidate.DesiredJobTypes.IsFlagSet(JobTypes.Contract);
            _tempCheckBox.IsChecked = candidate.DesiredJobTypes.IsFlagSet(JobTypes.Temp);
            _jobShareCheckBox.IsChecked = candidate.DesiredJobTypes.IsFlagSet(JobTypes.JobShare);
            
            _aboriginalCheckBox.IsChecked = member.EthnicStatus.IsFlagSet(EthnicStatus.Aboriginal);
            _torresIslanderCheckBox.IsChecked = member.EthnicStatus.IsFlagSet(EthnicStatus.TorresIslander);

            switch (member.Gender)
            {
                case Gender.Male:
                    _maleRadioButton.IsChecked = true;
                    break;

                case Gender.Female:
                    _femaleRadioButton.IsChecked = true;
                    break;
            }

            Select(member.DateOfBirth);

            _citizenshipTextBox.Text = resume.IsEmpty ? null : resume.Citizenship;

            switch (candidate.VisaStatus)
            {
                case VisaStatus.Citizen:
                    _citizenRadioButton.IsChecked = true;
                    break;

                case VisaStatus.NotApplicable:
                    _notApplicableRadioButton.IsChecked = true;
                    break;

                case VisaStatus.NoWorkVisa:
                    _noWorkVisaRadioButton.IsChecked = true;
                    break;

                case VisaStatus.RestrictedWorkVisa:
                    _restrictedWorkVisaRadioButton.IsChecked = true;
                    break;

                case VisaStatus.UnrestrictedWorkVisa:
                    _unrestrictedWorkVisaRadioButton.IsChecked = true;
                    break;
            }

            var secondaryPhoneNumber = member.GetSecondaryPhoneNumber();
            _secondaryPhoneNumberTextBox.Text = secondaryPhoneNumber == null ? string.Empty : secondaryPhoneNumber.Number;

            if (secondaryPhoneNumber != null)
            {
                switch (secondaryPhoneNumber.Type)
                {
                    case PhoneNumberType.Mobile:
                        _secondaryMobileRadioButton.IsChecked = true;
                        break;

                    case PhoneNumberType.Home:
                        _secondaryHomeRadioButton.IsChecked = true;
                        break;

                    case PhoneNumberType.Work:
                        _secondaryWorkRadioButton.IsChecked = true;
                        break;
                }
            }
            else
            {
                _secondaryMobileRadioButton.IsChecked = true;
            }

            var secondaryEmailAddress = member.GetSecondaryEmailAddress();
            _secondaryEmailAddressTextBox.Text = secondaryEmailAddress == null ? string.Empty : secondaryEmailAddress.Address;

            switch (candidate.RelocationPreference)
            {
                case RelocationPreference.Yes:
                    _relocationPreferenceYesRadioButton.IsChecked = true;
                    break;

                case RelocationPreference.WouldConsider:
                    _relocationPreferenceWouldConsiderRadioButton.IsChecked = true;
                    break;

                default:
                    _relocationPreferenceNoRadioButton.IsChecked = true;
                    break;
            }

            SelectRelocationCountryIds(candidate.RelocationLocations);
            SelectRelocationCountryLocationIds(candidate.RelocationLocations);

            Select(referralSourceId);
            _sendSuggestedJobsCheckBox.IsChecked = sendSuggestedJobs;
        }

        protected void AssertJobDetails(Guid instanceId, IMember member, ICandidate candidate, IResume resume, bool sendSuggestedJobs, int? referralSourceId, bool resumeUploaded)
        {
            AssertUrl(GetJobDetailsUrl(instanceId));

            // Job.

            if (resumeUploaded)
            {
                Assert.IsFalse(_jobTitleTestBox.IsVisible);
                Assert.IsFalse(_jobCompanyTextBox.IsVisible);
            }
            else
            {
                Assert.AreEqual(resume.Jobs == null || resume.Jobs.Count == 0 ? string.Empty : (resume.Jobs[0].Title ?? string.Empty), _jobTitleTestBox.Text);
                Assert.AreEqual(resume.Jobs == null || resume.Jobs.Count == 0 ? string.Empty : (resume.Jobs[0].Company ?? string.Empty), _jobCompanyTextBox.Text);
            }

            // Status.

            Assert.AreEqual(candidate.RecentProfession == null ? string.Empty : candidate.RecentProfession.ToString(), _recentProfessionDropDownList.SelectedItem.Value);
            Assert.AreEqual(candidate.RecentSeniority == null ? string.Empty : candidate.RecentSeniority.ToString(), _recentSeniorityDropDownList.SelectedItem.Value);
            Assert.AreEqual(candidate.HighestEducationLevel == null ? string.Empty : candidate.HighestEducationLevel.ToString(), _highestEducationLevelDropDownList.SelectedItem.Value);

            if (candidate.Industries == null)
            {
                Assert.AreEqual(0, GetSelectedIndustryIds().Count);
            }
            else
            {
                var industryIds = candidate.Industries.Select(i => i.Id).ToList();
                var selectedIndustryIds = GetSelectedIndustryIds();
                Assert.AreEqual(industryIds.Count, selectedIndustryIds.Count);
                Assert.IsTrue(industryIds.All(selectedIndustryIds.Contains));
            }

            // Desired job.

            Assert.AreEqual(candidate.DesiredJobTitle ?? string.Empty, _desiredJobTitleTextBox.Text);

            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.FullTime), _fullTimeCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.PartTime), _partTimeCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.Contract), _contractCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.Temp), _tempCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.JobShare), _jobShareCheckBox.IsChecked);

            // Ethnic status.

            Assert.AreEqual(member.EthnicStatus.IsFlagSet(EthnicStatus.Aboriginal), _aboriginalCheckBox.IsChecked);
            Assert.AreEqual(member.EthnicStatus.IsFlagSet(EthnicStatus.TorresIslander), _torresIslanderCheckBox.IsChecked);

            // Gender.

            Assert.AreEqual(member.Gender == Gender.Male, _maleRadioButton.IsChecked);
            Assert.AreEqual(member.Gender == Gender.Female, _femaleRadioButton.IsChecked);

            // Date of birth.

            Assert.AreEqual(member.DateOfBirth == null || member.DateOfBirth.Value.Month == null ? string.Empty : member.DateOfBirth.Value.Month.Value.ToString(CultureInfo.InvariantCulture), _dateOfBirthMonthDropDownList.SelectedItem.Value);
            Assert.AreEqual(member.DateOfBirth == null ? string.Empty : member.DateOfBirth.Value.Year.ToString(CultureInfo.InvariantCulture), _dateOfBirthYearDropDownList.SelectedItem.Value);

            // Citizenship.

            Assert.AreEqual(resume.Citizenship ?? string.Empty, _citizenshipTextBox.Text);

            // Vias status.

            Assert.AreEqual(candidate.VisaStatus == VisaStatus.Citizen, _citizenRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.NotApplicable, _notApplicableRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.NoWorkVisa, _noWorkVisaRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.RestrictedWorkVisa, _restrictedWorkVisaRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.UnrestrictedWorkVisa, _unrestrictedWorkVisaRadioButton.IsChecked);

            // Secondary phone.

            var phoneNumber = member.GetSecondaryPhoneNumber();
            Assert.AreEqual(phoneNumber == null ? string.Empty : phoneNumber.Number, _secondaryPhoneNumberTextBox.Text);
            Assert.AreEqual(phoneNumber == null || phoneNumber.Type == PhoneNumberType.Mobile, _secondaryMobileRadioButton.IsChecked);
            Assert.AreEqual(phoneNumber != null && phoneNumber.Type == PhoneNumberType.Home, _secondaryHomeRadioButton.IsChecked);
            Assert.AreEqual(phoneNumber != null && phoneNumber.Type == PhoneNumberType.Work, _secondaryWorkRadioButton.IsChecked);

            // Secondary email address.

            var emailAddress = member.GetSecondaryEmailAddress();
            Assert.AreEqual(emailAddress == null ? string.Empty : emailAddress.Address, _secondaryEmailAddressTextBox.Text);

            // Relocation.

            Assert.AreEqual(candidate.RelocationPreference == RelocationPreference.No, _relocationPreferenceNoRadioButton.IsChecked);
            Assert.AreEqual(candidate.RelocationPreference == RelocationPreference.Yes, _relocationPreferenceYesRadioButton.IsChecked);
            Assert.AreEqual(candidate.RelocationPreference == RelocationPreference.WouldConsider, _relocationPreferenceWouldConsiderRadioButton.IsChecked);

            if (candidate.RelocationLocations == null)
            {
                Assert.AreEqual(0, GetSelectedRelocationCountryIds().Count);
                Assert.AreEqual(0, GetSelectedRelocationCountryLocationIds().Count);
            }
            else
            {
                var countryIds = (from l in candidate.RelocationLocations
                                  where l.IsCountry
                                  select l.Country.Id).ToList();
                var countryLocationIds = (from l in candidate.RelocationLocations
                                          where !l.IsCountry
                                          select l.NamedLocation.Id).ToList();

                var selectedCountryIds = GetSelectedRelocationCountryIds();
                Assert.AreEqual(countryIds.Count, selectedCountryIds.Count);
                Assert.IsTrue(countryIds.All(selectedCountryIds.Contains));

                var selectedCountryLocationIds = GetSelectedRelocationCountryLocationIds();
                Assert.AreEqual(countryLocationIds.Count, selectedCountryLocationIds.Count);
                Assert.IsTrue(countryLocationIds.All(selectedCountryLocationIds.Contains));
            }

            // Referral.

            Assert.AreEqual(referralSourceId == null ? "" : referralSourceId.Value.ToString(CultureInfo.InvariantCulture), _externalReferralSourceIdDropDownListTester.SelectedItem.Value);

            // Suggested jobs.

            Assert.AreEqual(sendSuggestedJobs, _sendSuggestedJobsCheckBox.IsChecked);
        }

        protected void HomeJoin(IMember member, string password)
        {
            Get(HomeUrl);

            _homeFirstNameTextBox.Text = member.FirstName;
            _homeLastNameTextBox.Text = member.LastName;
            _homeEmailAddressTextBox.Text = member.GetBestEmailAddress().Address;
            _homeJoinPasswordTextBox.Text = password;
            _homeJoinConfirmPasswordTextBox.Text = password;
            _homeAcceptTermsCheckBox.IsChecked = true;
            Browser.Submit(_homeJoinFormId);

            AssertUrl(_joinUrl);
        }

        protected ReadOnlyUrl GetJoinUrl()
        {
            return GetJoinUrl(null);
        }

        protected ReadOnlyUrl GetJoinUrl(Guid? instanceId)
        {
            return GetUrl(_joinUrl, instanceId);
        }

        protected ReadOnlyUrl GetPersonalDetailsUrl(Guid? instanceId)
        {
            return GetUrl(_personalDetailsUrl, instanceId);
        }

        protected ReadOnlyUrl GetJobDetailsUrl(Guid? instanceId)
        {
            return GetUrl(_jobDetailsUrl, instanceId);
        }

        protected ReadOnlyUrl GetActivateUrl(Guid? instanceId)
        {
            return GetUrl(_activateUrl, instanceId);
        }

        private static ReadOnlyUrl GetUrl(ReadOnlyUrl baseUrl, Guid? instanceId)
        {
            if (instanceId == null)
                return baseUrl;

            var url = baseUrl.AsNonReadOnly();
            url.QueryString["instanceId"] = instanceId.ToString();
            return url;
        }

        protected Guid GetInstanceId()
        {
            var url = new ReadOnlyUrl(Browser.CurrentUrl);
            return new Guid(url.QueryString["instanceId"]);
        }
    }
}
