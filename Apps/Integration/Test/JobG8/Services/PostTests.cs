using System;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8.Services
{
    [TestClass]
    public class PostTests
        : AdvertPostServiceTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        [TestMethod]
        public void TestPostSampleJob()
        {
            var employer = CreateEmployer();
            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                PostAdvert = CreatePostAdvert(employer, false),
            };

            var response = PostAdvert(employer, request);
            Assert.AreEqual("", response);
            AssertJobAd(employer.Id, null, "http://www.jobg8.com/WebServices/ApplicationResponse.asmx");
        }

        [TestMethod]
        public void TestDuplicateAdHasPriorityOverCareerOne()
        {
            const string integratorReferenceId = "RefABCD/1235";
            const string externalReferenceId = "RefABCD";

            // Create another job ad with the same title and external reference ID.

            var employer = CreateEmployer();
            var otherEmployer = CreateEmployer("other");

            var otherJobAd = otherEmployer.CreateTestJobAd("Chartered Accountant");
            otherJobAd.Integration.ExternalReferenceId = externalReferenceId;
            otherJobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            _jobAdsCommand.PostJobAd(otherJobAd);

            // Post the JobG8 as as normal.

            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                PostAdvert = new PostAdvertRequest
                {
                    Adverts = new PostAdverts
                    {
                        AccountNumber = employer.GetLoginId(),
                        PostAdvert = new[] 
                        {
                            new PostAdvert
                            {
                                JobReference = integratorReferenceId,
                                ClientReference = externalReferenceId,
                                Classification = "Accounting",
                                SubClassification = "Accountant",
                                Position = "Chartered Accountant",
                                Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                                Location = "Sydney",
                                Area = "Sydney Inner",
                                PostCode = "2000",
                                Country = "Australia",
                                EmploymentType = EmploymentType.Permanent,
                                VisaRequired = VisaRequired.MustBeEligible,
                                PayPeriod = PayPeriod.Annual,
                                PayAmount = 100000, PayAmountSpecified = true,
                                Currency = "AUS",
                                Contact = "John Bloomfield",
                            }
                        }
                    }
                }
            };

            // Check that it was still posted.

            var response = PostAdvert(employer, request);
            Assert.AreEqual("", response);
            AssertJobAd(employer.Id, null, null);

            // Check that the job ad was posted.

            var jobG8IntegratorUser = _jobG8Query.GetIntegratorUser();
            var jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(_jobAdIntegrationQuery.GetJobAdIds(jobG8IntegratorUser.Id, employer.Id, otherJobAd.Integration.ExternalReferenceId)[0]);
            Assert.AreEqual(jobG8IntegratorUser.Id, jobAd.Integration.IntegratorUserId);
            Assert.AreEqual(integratorReferenceId, jobAd.Integration.IntegratorReferenceId);
            Assert.AreEqual(externalReferenceId, jobAd.Integration.ExternalReferenceId);
            Assert.AreEqual(JobAdStatus.Open, jobAd.Status);

            // Check that the other job ad was closed.

            jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(otherJobAd.Id);
            Assert.AreNotEqual(jobG8IntegratorUser.Id, jobAd.Integration.IntegratorUserId);
            Assert.AreNotEqual(integratorReferenceId, jobAd.Integration.IntegratorReferenceId);
            Assert.AreEqual(externalReferenceId, jobAd.Integration.ExternalReferenceId);
            Assert.AreEqual(JobAdStatus.Closed, jobAd.Status);
        }

        [TestMethod]
        public void TestAmendJob()
        {
            var employer = CreateEmployer();
            var postRequest = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                PostAdvert = CreatePostAdvert(employer, false),
            };

            var response = PostAdvert(employer, postRequest);
            Assert.AreEqual("", response);
            AssertJobAd(employer.Id, null, "http://www.jobg8.com/WebServices/ApplicationResponse.asmx");

            var amendRequest = new AmendAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                AmendAdvert = CreateAmendAdvert(employer),
            };

            response = AmendAdvert(employer, amendRequest);
            Assert.AreEqual("", response);
            AssertJobAd(employer.Id, null, "http://www.jobg8.com/WebServices/ApplicationResponse.asmx");
        }

        [TestMethod]
        public void TestAmendJobWithRedirectionUrl()
        {
            var employer = CreateEmployer();
            var postRequest = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                PostAdvert = CreatePostAdvert(employer, true),
            };

            var response = PostAdvert(employer, postRequest);
            Assert.AreEqual("", response);
            AssertJobAd(employer.Id, "http://www.jobg8.com/Redirection.aspx?jbid=11&jid=11514&email=[[candidateemailaddress]]", "http://www.jobg8.com/WebServices/ApplicationResponse.asmx");

            var amendRequest = new AmendAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                AmendAdvert = CreateAmendAdvert(employer),
            };

            response = AmendAdvert(employer, amendRequest);
            Assert.AreEqual("", response);
            AssertJobAd(employer.Id, "http://www.jobg8.com/Redirection.aspx?jbid=11&jid=11514&email=[[candidateemailaddress]]", "http://www.jobg8.com/WebServices/ApplicationResponse.asmx");
        }

        private static AmendAdvertRequest CreateAmendAdvert(IUser employer)
        {
            return new AmendAdvertRequest
            {
                Adverts = new AmendAdverts
                {
                    AccountNumber = employer.GetLoginId(),
                    AmendAdvert = new[] 
                    {
                        new AmendAdvert
                        {
                            JobReference = "RefABCD/1235",
                            Position = "Chartered Accountant",
                            Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                            Location = "Sydney",
                            Area = "Sydney Inner",
                            PostCode = "2000",
                            Country = "Australia",
                            EmploymentType = EmploymentType.Permanent,
                            VisaRequired = VisaRequired.MustBeEligible,
                            PayPeriod = PayPeriod.Annual,
                            PayAmount = 100000, PayAmountSpecified = true,
                            Currency = "AUS",
                            Contact = "John Bloomfield",
                        }
                    }
                }
            };
        }

        private static PostAdvertRequest CreatePostAdvert(IUser employer, bool includeRedirectionUrl)
        {
            return new PostAdvertRequest
            {
                Adverts = new PostAdverts
                {
                    AccountNumber = employer.GetLoginId(),
                    PostAdvert = new[] 
                    {
                        new PostAdvert
                        {
                            JobReference = "RefABCD/1235",
                            ClientReference = "RefABCD",
                            Classification = "Accounting",
                            SubClassification = "Accountant",
                            Position = "Chartered Accountant",
                            Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                            Location = "Sydney",
                            Area = "Sydney Inner",
                            PostCode = "2000",
                            Country = "Australia",
                            EmploymentType = EmploymentType.Permanent,
                            VisaRequired = VisaRequired.MustBeEligible,
                            PayPeriod = PayPeriod.Annual,
                            PayAmount = 100000, PayAmountSpecified = true,
                            Currency = "AUS",
                            Contact = "John Bloomfield",
                            //ApplicationURL = "http://www.jobg8.com/Application.aspx?hBWkKEe21n4g",
                            RedirectionUrl = includeRedirectionUrl ? "http://www.jobg8.com/Redirection.aspx?jbid=11&jid=11514&email=[[candidateemailaddress]]" : null,

                            ApplicationFormXML = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><Application JobReference=\"JS00046521/406080\" JobBoardID=\"52\" ResponseURI=\"http://www.jobg8.com/WebServices/ApplicationResponse.asmx\"><Question ID=\"11\" FormatID=\"15\"><Required>true</Required><Name>Email</Name><Text>Email Address</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"78_605\" FormatID=\"111\"><Required>true</Required><Name>Reason behind eligibility</Name><Text>What qualifies you to work in Australia?</Text><Type>COMBOBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows><Answers><Answer ID=\"1518\"><Text>Australian Citizen</Text></Answer><Answer ID=\"1519\"><Text>Australian Permanent Resident or NZ Citizen</Text></Answer><Answer ID=\"1520\"><Text>Working Holiday Visa</Text></Answer><Answer ID=\"1521\"><Text>Temporary Resident Visa</Text></Answer><Answer ID=\"1523\"><Text>Sponsorship Required</Text></Answer><Answer ID=\"1541\"><Text>Student Visa</Text></Answer></Answers></Question><Question ID=\"CV\" FormatID=\"53\"><Required>true</Required><Name>CV Upload</Name><Text>Upload your CV.</Text><Type>FILEUPLOAD</Type><Size>255</Size><Width>275</Width><Rows>0</Rows></Question><Question ID=\"2\" FormatID=\"2\"><Required>true</Required><Name>First Name</Name><Text>First Name</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"3\" FormatID=\"6\"><Required>true</Required><Name>Last Name</Name><Text>Last Name</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"8\" FormatID=\"12\"><Required>false</Required><Name>Home Telephone</Name><Text>Home Telephone No.</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"19\" FormatID=\"31\"><Required>false</Required><Name>Notice / Availability</Name><Text>Availability / Notice period?</Text><Type>COMBOBOX</Type><Size>100</Size><Width>0</Width><Rows>0</Rows><Answers><Answer ID=\"80\"><Text>Immediate</Text></Answer><Answer ID=\"81\"><Text>1 Week</Text></Answer><Answer ID=\"82\"><Text>2 Weeks</Text></Answer><Answer ID=\"83\"><Text>3 Weeks</Text></Answer><Answer ID=\"84\"><Text>1 Month</Text></Answer><Answer ID=\"85\"><Text>3 Months</Text></Answer><Answer ID=\"86\"><Text>&gt; 3 Months</Text></Answer></Answers></Question><Question ID=\"13\" FormatID=\"18\"><Required>false</Required><Name>Salary Expectation</Name><Text>Salary Expectation AUD</Text><Type>COMBOBOX</Type><Size>100</Size><Width>0</Width><Rows>0</Rows><Answers><Answer ID=\"19\"><Text>0 - 30,000</Text></Answer><Answer ID=\"20\"><Text>30,000 - 40,000</Text></Answer><Answer ID=\"21\"><Text>40,000 - 50,000</Text></Answer><Answer ID=\"22\"><Text>50,000 - 60,000</Text></Answer><Answer ID=\"23\"><Text>60,000 - 80,000</Text></Answer><Answer ID=\"24\"><Text>80,000 - 100,000</Text></Answer><Answer ID=\"25\"><Text>100,000 - 125,000</Text></Answer><Answer ID=\"26\"><Text>125,000 - 150,000</Text></Answer><Answer ID=\"27\"><Text>&gt; 150,000</Text></Answer></Answers></Question></Application>",

                            /*ApplicationFormXML = @"<Application JobReference='RefACBD/1235' JobBoardID='2' ResponseURI='http://www.jobg8.com/WebServices/ApplicationResponse.asmx'>
	                                <Question ID='2' FormatID='2'>
		                                <Required>true</Required>
		                                <Name>First Name</Name>
		                                <Text>First Name</Text>
		                                <Type>TEXTBOX</Type>
		                                <Size>100</Size>
		                                <Width>200</Width>
		                                <Rows>0</Rows>
	                                </Question>
	                                <Question ID='3' FormatID='5'>
		                                <Required>true</Required>
		                                <Name>Last Name</Name>
		                                <Text>Surname</Text>
		                                <Type>TEXTBOX</Type>
		                                <Size>100</Size>
		                                <Width>200</Width>
		                                <Rows>0</Rows>
	                                </Question>
	                                <Question ID='11' FormatID='15'>
		                                <Required>true</Required>
		                                <Name>Email</Name>
		                                <Text>Email Address</Text>
		                                <Type>TEXTBOX</Type>
		                                <Size>100</Size>
		                                <Width>200</Width>
		                                <Rows>0</Rows>
	                                </Question>
	                                <Question ID='CV' FormatID='53'>
		                                <Required>true</Required>
		                                <Name>CV Upload</Name>
		                                <Text>Upload your CV.</Text>
		                                <Type>FILEUPLOAD</Type>
		                                <Size>255</Size>
		                                <Width>275</Width>
		                                <Rows>0</Rows>
	                                </Question>
	                                <Question ID='CoverLetter' FormatID='999'>
		                                <Required>true</Required>
		                                <Name>CoverLetter Upload</Name>
		                                <Text>Upload your cover letter.</Text>
		                                <Type>FILEUPLOAD</Type>
		                                <Size>255</Size>
		                                <Width>275</Width>
		                                <Rows>0</Rows>
	                                </Question>
	                                <Question ID='20' FormatID='34'>
		                                <Required>true</Required>
		                                <Name>Reason behind Eligibility</Name>
		                                <Text>What qualifies you to work in Australia?</Text>
		                                <Type>COMBOBOX</Type>
		                                <Size>100</Size>
		                                <Width>0</Width>
		                                <Rows>0</Rows>
		                                <Answers>
			                                <Answer ID='349'>
				                                <Text>Australian / New Zealand Citizen</Text>
			                                </Answer>
			                                <Answer ID='350'>
				                                <Text>Australian / New Zealand permanent resident</Text>
			                                </Answer>
			                                <Answer ID='351'>
				                                <Text>Working visa</Text>
			                                </Answer>
			                                <Answer ID='352'>
				                                <Text>Sponsorship required</Text>
			                                </Answer>
		                                </Answers>
	                                </Question>
                                </Application>"*/
                        }
                    }
                }
            };
        }

        private void AssertJobAd(Guid jobPosterId, string expectedExternalApplyUrl, string expectedExternalApplyApiUrl)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(jobPosterId, JobAdStatus.Open));
            Assert.AreEqual(1, jobAds.Count);
            Assert.AreEqual("RefABCD/1235", jobAds[0].Integration.IntegratorReferenceId);
            Assert.AreEqual("RefABCD", jobAds[0].Integration.ExternalReferenceId);
            Assert.AreEqual(expectedExternalApplyUrl, jobAds[0].Integration.ExternalApplyUrl);
            Assert.AreEqual(expectedExternalApplyApiUrl, jobAds[0].Integration.ExternalApplyApiUrl);
        }
    }
}