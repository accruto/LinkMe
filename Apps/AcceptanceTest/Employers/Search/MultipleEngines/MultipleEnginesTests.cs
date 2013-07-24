using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using LinkMe.Web.Areas.Api.Models.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.MultipleEngines
{
    [TestClass]
    public class MultipleEnginesTests
        : WebTestClass
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IEmailVerificationsQuery _emailVerificationsQuery = Resolve<IEmailVerificationsQuery>();

        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string Password = "password";
        private const decimal SalaryLowerBound = 100000;
        private static readonly SalaryRate SalaryRate = SalaryRate.Year;

        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _uploadUrl;
        private ReadOnlyUrl _parseUrl;
        private ReadOnlyUrl _activationUrl;

        private string _joinFormId;

        private string _personalDetailsFormId;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlRadioButtonTester _mobileRadioButton;
        private HtmlRadioButtonTester _homeRadioButton;
        private HtmlRadioButtonTester _workRadioButton;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlDropDownListTester _countryIdDropDownList;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlRadioButtonTester _notLookingRadioButton;
        private HtmlRadioButtonTester _openToOffersRadioButton;
        private HtmlRadioButtonTester _activelyLookingRadioButton;
        private HtmlRadioButtonTester _availableNowRadioButton;
        private HtmlTextBoxTester _salaryLowerBoundTextBox;
        private HtmlRadioButtonTester _salaryRateYearRadioBox;
        private HtmlRadioButtonTester _salaryRateHourRadioBox;

        private string _jobDetailsFormId;

        private const int MonitorInterval = 2;

        private WcfTcpHost _host1;
        private IMemberSearchService _service1;
        private WcfTcpHost _host2;
        private IMemberSearchService _service2;

        [TestInitialize]
        public void TestInitialize()
        {
            MemberSearchHost.Stop();
            StartHosts();

            _joinUrl = new ReadOnlyApplicationUrl(true, "~/join");
            _uploadUrl = new ReadOnlyApplicationUrl(true, "~/api/resumes/upload");
            _parseUrl = new ReadOnlyApplicationUrl(true, "~/api/resumes/parse");
            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");

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

            _jobDetailsFormId = "JobDetailsForm";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            StopHosts();
            MemberSearchHost.Start();
        }

        [TestMethod]
        public void TestNewMember()
        {
            var query = new MemberSearchQuery { Name = FirstName.CombineLastName(LastName), IncludeSimilarNames = false };

            // Do some searches.

            var results = _service1.Search(null, null, query);
            Assert.AreEqual(0, results.MemberIds.Count);

            results = _service2.Search(null, null, query);
            Assert.AreEqual(0, results.MemberIds.Count);

            // Add a member.

            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            var member = CreateMember(FirstName, LastName, parsedResume);
            var candidate = CreateCandidate();

            // Join.

            Get(_joinUrl);
            UploadResume(testResume);
            SubmitPersonalDetails(member, candidate);
            Browser.Submit(_jobDetailsFormId);
            member = _membersQuery.GetMember(parsedResume.EmailAddresses[0].Address);
            var activationCode = _emailVerificationsQuery.GetEmailVerification(member.Id, parsedResume.EmailAddresses[0].Address).VerificationCode;
            Get(GetActivationUrl(activationCode));

            // Do some searches again.

            results = _service1.Search(null, null, query);
            Assert.AreEqual(1, results.MemberIds.Count);
            Assert.AreEqual(member.Id, results.MemberIds[0]);

            // Wait for the polling to kick in.

            Thread.Sleep(3 * MonitorInterval * 1000);
            results = _service2.Search(null, null, query);
            Assert.AreEqual(1, results.MemberIds.Count);
            Assert.AreEqual(member.Id, results.MemberIds[0]);
        }

        private ReadOnlyUrl GetActivationUrl(string activationCode)
        {
            var url = _activationUrl.AsNonReadOnly();
            url.QueryString["activationCode"] = activationCode;
            return url;
        }

        private Member CreateMember(string firstName, string lastName, ParsedResume parsedResume)
        {
            return new Member
            {
                FirstName = firstName,
                LastName = lastName,
                IsEnabled = true,
                VisibilitySettings = new VisibilitySettings(),
                Address = new Address
                {
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), parsedResume.Address.Location),
                },
                EmailAddresses = (from e in parsedResume.EmailAddresses select new EmailAddress { Address = e.Address, IsVerified = e.IsVerified }).ToList(),
                PhoneNumbers = (from p in parsedResume.PhoneNumbers select new PhoneNumber { Number = p.Number, Type = p.Type }).ToList(),
            };
        }

        private static Candidate CreateCandidate()
        {
            return new Candidate
            {
                Status = CandidateStatus.OpenToOffers,
                DesiredSalary = new Salary { LowerBound = SalaryLowerBound, Rate = SalaryRate, Currency = Currency.AUD },
            };
        }

        private void UploadResume(TestResume resume)
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

            var parsedResumeId = new JavaScriptSerializer().Deserialize<JsonParsedResumeModel>(Post(_parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } })).Id.Value;

            // Submit the form.

            Get(_joinUrl);

            Browser.SetFormVariable(_joinFormId, "fileReferenceId", fileReferenceId.ToString(), false);
            Browser.SetFormVariable(_joinFormId, "parsedResumeId", parsedResumeId.ToString(), false);
            Browser.Submit(_joinFormId);
        }

        private void SubmitPersonalDetails(IMember member, ICandidate candidate)
        {
            _firstNameTextBox.Text = member.FirstName;
            _lastNameTextBox.Text = member.LastName;
            _emailAddressTextBox.Text = member.GetPrimaryEmailAddress().Address;

            var phoneNumber = member.GetPrimaryPhoneNumber();
            _phoneNumberTextBox.Text = phoneNumber.Number;
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

            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _acceptTermsCheckBox.IsChecked = true;

            Browser.Submit(_personalDetailsFormId);
        }

        private void Select(Country country)
        {
            _countryIdDropDownList.SelectedValue = country.Id.ToString(CultureInfo.InvariantCulture);
        }

        private void StartHosts()
        {
            // The first service is the standard local service.

            var service = Resolve<MemberSearchService>();
            _service1 = service;

            service.InitialiseIndex = true;
            service.RebuildIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.search.members.tcpAddress"),
                BindingName = "linkme.search.members.tcp",
            };

            _host1 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _host1.Open();
            _host1.Start();

            ((IMemberSearchService) service).Clear();

            // The second service represents the remote service.

            service = Resolve<MemberSearchService>("linkme.search.members.otherservice");
            _service2 = service;

            service.InitialiseIndex = true;
            service.RebuildIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.search.members.other.tcpAddress"),
                BindingName = "linkme.search.members.tcp",
            };

            _host2 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _host2.Open();
            _host2.Start();

            ((IMemberSearchService)service).Clear();
        }

        private void StopHosts()
        {
            _host1.Stop();
            _host1.Close();
            _host1 = null;
            _service1 = null;

            _host2.Stop();
            _host2.Close();
            _host2 = null;
            _service2 = null;
        }
    }
}
