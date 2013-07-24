using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Services.JobG8;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Apps.Services.External.JobG8.Schema;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class JobG8Tests
        : ManagedExternallyTests
    {
        private abstract class QuestionControlTester
        {
            private readonly string _id;

            protected QuestionControlTester(string id)
            {
                _id = id;
            }

            public string Id
            {
                get { return _id; }
            }

            public object Value { get; protected set; }
        }

        private class QuestionTextBoxTester
            : QuestionControlTester
        {
            private readonly HtmlTextBoxTester _textBox;

            public QuestionTextBoxTester(HttpClient httpClient, string id)
                : base(id)
            {
                _textBox = new HtmlTextBoxTester(httpClient, "Question" + id);
            }

            public string Text
            {
                set
                {
                    _textBox.Text = value;
                    Value = value;
                }
            }
        }

        private class QuestionDropDownListTester
            : QuestionControlTester
        {
            private readonly HtmlDropDownListTester _dropDownList;

            public QuestionDropDownListTester(HttpClient httpClient, string id)
                : base(id)
            {
                _dropDownList = new HtmlDropDownListTester(httpClient, "Question" + id);
            }

            public int SelectedValue
            {
                set
                {
                    Value = value;
                    _dropDownList.SelectedValue = value.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private readonly IJobG8Query _jobG8Query = Resolve<IJobG8Query>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly IResumeFilesQuery _resumeFilesQuery = Resolve<IResumeFilesQuery>();

        private const string ApplyForm = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><Application JobReference=\"JS081021171146/AMY LEE/350723\" JobBoardID=\"52\" ResponseURI=\"http://www.jobg8.com/WebServices/ApplicationResponse.asmx\"><Question ID=\"11\" FormatID=\"15\"><Required>true</Required><Name>Email</Name><Text>Email Address</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"78_605\" FormatID=\"111\"><Required>true</Required><Name>Reason behind eligibility</Name><Text>What qualifies you to work in Australia?</Text><Type>COMBOBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows><Answers><Answer ID=\"1518\"><Text>Australian Citizen</Text></Answer><Answer ID=\"1519\"><Text>Australian Permanent Resident or NZ Citizen</Text></Answer><Answer ID=\"1520\"><Text>Working Holiday Visa</Text></Answer><Answer ID=\"1521\"><Text>Temporary Resident Visa</Text></Answer><Answer ID=\"1523\"><Text>Sponsorship Required</Text></Answer><Answer ID=\"1541\"><Text>Student Visa</Text></Answer></Answers></Question><Question ID=\"CoverLetter\" FormatID=\"54\"><Required>false</Required><Name>Cover Letter Upload</Name><Text>Upload a covering letter.</Text><Type>FILEUPLOAD</Type><Size>255</Size><Width>275</Width><Rows>0</Rows></Question><Question ID=\"CV\" FormatID=\"53\"><Required>true</Required><Name>CV Upload</Name><Text>Upload your CV.</Text><Type>FILEUPLOAD</Type><Size>255</Size><Width>275</Width><Rows>0</Rows></Question><Question ID=\"2\" FormatID=\"2\"><Required>true</Required><Name>First Name</Name><Text>First Name</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"3\" FormatID=\"5\"><Required>true</Required><Name>Last Name</Name><Text>Surname</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"8\" FormatID=\"12\"><Required>true</Required><Name>Home Telephone</Name><Text>Home Telephone No.</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"19\" FormatID=\"31\"><Required>false</Required><Name>Notice / Availability</Name><Text>Availability / Notice period?</Text><Type>COMBOBOX</Type><Size>100</Size><Width>0</Width><Rows>0</Rows><Answers><Answer ID=\"80\"><Text>Immediate</Text></Answer><Answer ID=\"81\"><Text>1 Week</Text></Answer><Answer ID=\"82\"><Text>2 Weeks</Text></Answer><Answer ID=\"83\"><Text>3 Weeks</Text></Answer><Answer ID=\"84\"><Text>1 Month</Text></Answer><Answer ID=\"85\"><Text>3 Months</Text></Answer><Answer ID=\"86\"><Text>&gt; 3 Months</Text></Answer></Answers></Question><Question ID=\"13\" FormatID=\"18\"><Required>false</Required><Name>Salary Expectation</Name><Text>Salary Expectation AUD</Text><Type>COMBOBOX</Type><Size>100</Size><Width>0</Width><Rows>0</Rows><Answers><Answer ID=\"19\"><Text>0 - 30,000</Text></Answer><Answer ID=\"20\"><Text>30,000 - 40,000</Text></Answer><Answer ID=\"21\"><Text>40,000 - 50,000</Text></Answer><Answer ID=\"22\"><Text>50,000 - 60,000</Text></Answer><Answer ID=\"23\"><Text>60,000 - 80,000</Text></Answer><Answer ID=\"24\"><Text>80,000 - 100,000</Text></Answer><Answer ID=\"25\"><Text>100,000 - 125,000</Text></Answer><Answer ID=\"26\"><Text>125,000 - 150,000</Text></Answer><Answer ID=\"27\"><Text>&gt; 150,000</Text></Answer></Answers></Question><Question ID=\"10\" FormatID=\"14\"><Required>false</Required><Name>Mobile</Name><Text>Mobile No.</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows><Languages><Language Locale=\"1031\"><Text>Mobiltelefon</Text></Language><Language Locale=\"1033\"><Text>Cell Phone</Text></Language><Language Locale=\"1036\"><Text>No de portable</Text></Language></Languages></Question></Application>";
        private const string CoverLetterFileName = "Cover Letter.txt";
        private const string JobBoardId = "52";
        private const string ExternalApplyUrl = "http://www.google.com?jbid=52&jid=11514&email=[[candidateemailaddress]]";

        private static readonly IDictionary<string, int> FormatIds = new Dictionary<string, int>
        {
            {"2", 2},
            {"11", 15},
            {"3", 5},
            {"78_605", 111},
            {"8", 12},
            {"10", 14},
            {"19", 31},
            {"13", 18},
        };
            
        protected const int VisaValue = 1520;
        protected const string PhoneNumberValue = "99998888";
        protected const string MobileValue = "77776666";
        protected const int AvailabilityValue = 84;
        protected const int SalaryValue = 24;

        private HtmlTextAreaTester _questionsCoverLetterTextBox;
        private QuestionDropDownListTester _visaDropDownList;
        private QuestionTextBoxTester _phoneNumberTextBox;
        private QuestionTextBoxTester _mobileTextBox;
        private QuestionDropDownListTester _availabilityDropDownList;
        private QuestionDropDownListTester _salaryDropDownList;
        private QuestionControlTester[] _questionTesters;
        private HtmlButtonTester _sendButton;

        private IMockJobG8Server _jobG8Server;

        [TestInitialize]
        public void JobG8TestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _jobG8Server = JobG8Host.Start();
            _jobG8Server.ClearRequests();

            _questionsCoverLetterTextBox = new HtmlTextAreaTester(Browser, "CoverLetter");
            _visaDropDownList = new QuestionDropDownListTester(Browser, "78_605");
            _phoneNumberTextBox = new QuestionTextBoxTester(Browser, "8");
            _mobileTextBox = new QuestionTextBoxTester(Browser, "10");
            _availabilityDropDownList = new QuestionDropDownListTester(Browser, "19");
            _salaryDropDownList = new QuestionDropDownListTester(Browser, "13");
            _questionTesters = new QuestionControlTester[] { _visaDropDownList, _phoneNumberTextBox, _mobileTextBox, _availabilityDropDownList, _salaryDropDownList };
            _sendButton = new HtmlButtonTester(Browser, "send");
        }

        protected abstract bool HasRedirectionUrl { get; }

        protected override JobAd CreateJobAd(IEmployer employer)
        {
            return CreateJobAd(employer, ApplyForm);
        }

        protected JobAd CreateJobAd(IEmployer employer, string requirementsXml)
        {
            var jobAd = base.CreateJobAd(employer);
            jobAd.Integration.ExternalApplyUrl = HasRedirectionUrl ? ExternalApplyUrl : null;
            jobAd.Integration.ExternalApplyApiUrl = null;
            jobAd.Integration.IntegratorUserId = _jobG8Query.GetIntegratorUser().Id;
            _jobAdsCommand.UpdateJobAd(jobAd);
            _jobAdsCommand.CreateApplicationRequirements(jobAd.Id, requirementsXml);
            jobAd.Integration.JobBoardId = JobBoardId;
            return jobAd;
        }

        protected void AnswerQuestions(int visaValue, string phoneNumberValue, string mobileValue, int availabilityValue, int salaryValue)
        {
            Assert.IsFalse(_questionsCoverLetterTextBox.IsVisible);

            _visaDropDownList.SelectedValue = visaValue;
            _phoneNumberTextBox.Text = phoneNumberValue;
            _mobileTextBox.Text = mobileValue;
            _availabilityDropDownList.SelectedValue = availabilityValue;
            _salaryDropDownList.SelectedValue = salaryValue;
            _sendButton.Click();
        }

        protected void AnswerQuestions()
        {
            AnswerQuestions(VisaValue, PhoneNumberValue, MobileValue, AvailabilityValue, SalaryValue);
        }

        protected void AnswerQuestions(string coverLetterText, int visaValue, string phoneNumberValue, string mobileValue, int availabilityValue, int salaryValue)
        {
            Assert.IsTrue(_questionsCoverLetterTextBox.IsVisible);
            _questionsCoverLetterTextBox.Text = coverLetterText;

            _visaDropDownList.SelectedValue = visaValue;
            _phoneNumberTextBox.Text = phoneNumberValue;
            _mobileTextBox.Text = mobileValue;
            _availabilityDropDownList.SelectedValue = availabilityValue;
            _salaryDropDownList.SelectedValue = salaryValue;
            _sendButton.Click();
        }

        protected void AnswerQuestions(string coverLetterText)
        {
            AnswerQuestions(coverLetterText, VisaValue, PhoneNumberValue, MobileValue, AvailabilityValue, SalaryValue);
        }

        protected void AssertJobG8Request(Member member, JobAd jobAd, InternalApplication application)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = application.ResumeId == null || candidate.ResumeId == null
                ? null
                : _resumesQuery.GetResume(candidate.ResumeId.Value);
            var view = new EmployerMemberView(member, candidate, resume, null, ProfessionalContactDegree.Applicant, false, false);
            var resumeFile = _resumeFilesQuery.GetResumeFile(view, resume);

            AssertJobG8Request(member, jobAd, application, resumeFile.FileName, GetResumeValue(resumeFile));
        }

        protected void AssertJobG8Request(AnonymousContact contact, JobAd jobAd, InternalApplication application, FileReference fileReference)
        {
            AssertJobG8Request(contact, jobAd, application, fileReference.FileName, GetResumeValue(fileReference));
        }

        protected void AssertJobG8Request(Member member, JobAd jobAd, InternalApplication application, FileReference fileReference)
        {
            AssertJobG8Request(member, jobAd, application, fileReference.FileName, GetResumeValue(fileReference));
        }

        private static string GetResumeValue(ResumeFile resumeFile)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(resumeFile.Contents));
        }

        private string GetResumeValue(FileReference fileReference)
        {
            using (var stream = _filesQuery.OpenFile(fileReference))
            {
                var streamLength = (int)stream.Length;
                var buffer = new byte[streamLength];
                stream.Read(buffer, 0, streamLength);
                return Convert.ToBase64String(buffer);
            }
        }

        protected void AssertNoJobG8Request()
        {
            var requests = _jobG8Server.GetRequests();
            Assert.AreEqual(0, requests.Count);
        }

        private void AssertJobG8Request(ICommunicationRecipient user, JobAdEntry jobAd, InternalApplication application, string resumeFileName, string resumeValue)
        {
            var requests = _jobG8Server.GetRequests();
            Assert.AreEqual(1, requests.Count);
            var response = Deserialize<ApplicationResponse>(requests[0].Body.ApplicationXml);

            Assert.AreEqual(jobAd.Integration.JobBoardId, response.ApplicationAnswer.JobBoardID);
            Assert.AreEqual(jobAd.Integration.IntegratorReferenceId, response.ApplicationAnswer.JobReference);

            // FirstName, LastName, EmailAddress + whichever questions have values.

            var count = 3 + (from q in _questionTesters where q.Value != null select q).Count();
            Assert.AreEqual(count, response.ApplicationAnswer.Questions.Question.Length);

            AssertFirstName(user.FirstName, response);
            AssertLastName(user.LastName, response);
            AssertEmailAddress(user.EmailAddress, response);

            AssertQuestions(response);

            AssertCoverLetter(application.CoverLetterText, response);
            AssertResume(resumeFileName, resumeValue, response);
        }

        private void AssertQuestions(ApplicationResponse response)
        {
            foreach (var questionTester in _questionTesters)
                AssertAnswerQuestion(questionTester.Id, FormatIds[questionTester.Id], questionTester.Value, response);
        }

        private static void AssertResume(string fileName, string value, ApplicationResponse response)
        {
            var cv = response.ApplicationAnswer.Files.CV;
            Assert.AreEqual(fileName, cv.Filename);
            Assert.AreEqual(value, cv.Value);
        }

        private static void AssertCoverLetter(string coverLetterText, ApplicationResponse response)
        {
            var coverLetter = response.ApplicationAnswer.Files.CoverLetter;
            if (coverLetterText == null)
            {
                if (response.ApplicationAnswer.Files.CoverLetter != null)
                {
                    Assert.IsNull(coverLetter.Filename);
                    Assert.IsNull(coverLetter.Value);
                }
            }
            else
            {
                Assert.AreEqual(CoverLetterFileName, coverLetter.Filename);
                Assert.AreEqual(Convert.ToBase64String(Encoding.ASCII.GetBytes(coverLetterText)), coverLetter.Value);
            }
        }

        private static void AssertFirstName(string firstName, ApplicationResponse response)
        {
            AssertAnswerQuestion("2", 2, firstName, response);
        }

        private static void AssertLastName(string lastName, ApplicationResponse response)
        {
            AssertAnswerQuestion("3", 5, lastName, response);
        }

        private static void AssertEmailAddress(string emailAddress, ApplicationResponse response)
        {
            AssertAnswerQuestion("11", 15, emailAddress, response);
        }

        private static void AssertAnswerQuestion(string id, int formatId, object item, ApplicationResponse response)
        {
            var question = GetQuestion(response, id);
            if (item == null)
            {
                Assert.IsNull(question);
            }
            else
            {
                Assert.IsNotNull(question);
                Assert.AreEqual(formatId, question.FormatID);
                Assert.IsTrue(question.FormatIDSpecified);
                Assert.AreEqual(1, question.Items.Length);
                Assert.AreEqual(item, question.Items[0]);
            }
        }

        private static AnswerQuestion GetQuestion(ApplicationResponse response, string id)
        {
            return (from q in response.ApplicationAnswer.Questions.Question where q.ID == id select q).SingleOrDefault();
        }

        private static T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        protected override string GetAppliedContact(IEmployer employer, JobAd jobAd)
        {
            return jobAd.ContactDetails.CompanyName;
        }

        protected void AssertAppliedUrl(JobAd jobAd, Guid applicationId, string emailAddress)
        {
            if (!string.IsNullOrEmpty(jobAd.Integration.ExternalApplyUrl))
            {
                var url = new Url(jobAd.Integration.ExternalApplyUrl);
                Assert.AreEqual("[[candidateemailaddress]]", url.QueryString["email"]);
                url.QueryString["email"] = emailAddress;
                AssertUrl(url);
            }
            else
            {
                AssertUrl(GetAppliedUrl(jobAd.Id));
                AssertAppliedContact(jobAd.ContactDetails.CompanyName);
            }
        }
    }
}