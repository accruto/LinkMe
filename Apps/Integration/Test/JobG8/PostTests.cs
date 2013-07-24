using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public class PostTests
        : AdvertPostServiceTests
    {
        private const string Password = "password";
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();
        private readonly IJobG8Query _jobG8Query = Resolve<IJobG8Query>();

        [TestInitialize]
        public void TestInitialize()
        {
            JobAdSearchHost.Start();
            JobAdSortHost.Start();
            JobAdSentimentAnalysisHost.Start();
        }

        [TestMethod]
        public void TestPostSampleJob()
        {
            var employer = CreateEmployer(0);
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
                                ApplicationFormXML = @"
                                    <Application JobReference='RefACBD/1235' JobBoardID='2' ResponseURI='http://www.jobg8.com/WebServices/ApplicationResponse.asmx'>
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
                                    </Application>"
                            }
                        }
                    }
                }
            };

            var response = PostAdvert(employer, request);
            Assert.AreEqual(response, "");
            AssertJobAd(employer.Id);
        }

        [TestMethod]
        public void TestDuplicateAdHasPriorityOverCareerOne()
        {
            const string integratorReferenceId = "RefABCD/1235";
            const string externalReferenceId = "RefABCD";

            // Create another job ad with the same title and external reference ID.

            var employer = CreateEmployer(0);
            var otherEmployer = CreateEmployer(1);

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
            Assert.AreEqual(response, "");
            AssertJobAd(employer.Id);

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
    }
}
