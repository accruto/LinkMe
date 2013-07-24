using System;
using System.IO;
using System.Net;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility.Wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.JobSearch
{
    [TestClass, Ignore]
    public class IntegrationTests
        : TestClass
    {
        private readonly SecurityUsernameToken _usernameToken = new SecurityUsernameToken
        {
            ValueType = "AD",
            Username = new Username { Value = "SV_UUDH" },
            Password = new Password { Value = "r6RvZ868" }
        };

        private const string OrgCode = "UUDH";
        private const string SiteCode = "M310";
        private const long EmployerId = 43781208;

        private WcfHttpChannelManager<IPublicVacancy> _channelManager;

        [TestInitialize]
        public void TestInitialize()
        {
            ApplicationSetUp.SetCurrentApplication(WebSite.Management);

            // This callback is not required in PROD; necessary to run with Fiddler.
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, cert, chain, error) => true;

            _channelManager = new WcfHttpChannelManager<IPublicVacancy>("https://ecsn.gov.au/employment/publicvacancylodgement.asmx", "JobSearchTest");
        }

        [TestMethod]
        public void AddVacancyCanAuthenticate()
        {
            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody()
            };

            CanAuthenticate(request, (c, r) => c.AddVacancy(r), EsiExecutionStatus.Failed, r => r.Header.executionStatus);
        }

        [TestMethod]
        public void UpdateVacancyCanAuthenticate()
        {
            var request = new UpdateVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new UpdateVacancyRequestBody()
            };

            CanAuthenticate(request, (c, r) => c.UpdateVacancy(r), EsiExecutionStatus.Failed, r => r.Header.executionStatus);
        }

        [TestMethod]
        public void DeleteVacancyCanAuthenticate()
        {
            var request = new DeleteVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new DeleteVacancyRequestBody()
            };

            CanAuthenticate(request, (c, r) => c.DeleteVacancy(r), EsiExecutionStatus.Failed, r => r.Header.executionStatus);
        }

        [TestMethod]
        public void GetVacancyDetailsCanAuthenticate()
        {
            var request = new GetVacancyDetailsRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new GetVacancyDetailsRequestBody { vacancyID = 206232582 }
            };

            CanAuthenticate(request, (c, r) => c.GetVacancyDetails(r), EsiExecutionStatus.Failed, r => r.Header.executionStatus);
        }

        [TestMethod]
        public void CanSubmitTextJob()
        {
            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody
                {
                    orgCode = OrgCode,
                    siteCode = SiteCode,
                    employerID = EmployerId,
                    vacancyTitle = "Estimating Manager II",
                    occupationCode = "1112",
                    vacancySuburb = "BENTLEIGH",
                    vacancyPostcode = "3204",
                    stateCode = "VIC",
                    vacancyDescription = "Description goes here",
                    positionLimit = 1,
                    workType = "F", //full time
                    duration = "N", //contract
                    daysToExpiry = 30,
                    howToApplyCode = "PSD", // please see description
                    contactName = "John Smith",
                    contactPhoneAreaCode = "03",
                    contactPhoneNumber = "85089161",
                    vacancyType = "H", // normal
                    returnMatchesFlag = false,
                    yourReference = string.Empty,
                }
            };

            var response = Call(request, (c, r) => c.AddVacancy(r));
            AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        [TestMethod]
        public void CanSubmitNoSuburbJob()
        {
            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody
                {
                    orgCode = OrgCode,
                    siteCode = SiteCode,
                    employerID = EmployerId,
                    vacancyTitle = "Estimating Manager II",
                    occupationCode = "1112",
                    vacancySuburb = " ", //"null, //"BENTLEIGH",
                    vacancyPostcode = "3204",
                    stateCode = "VIC",
                    vacancyDescription = "Description goes here",
                    positionLimit = 1,
                    workType = "F", //full time
                    duration = "N", //contract
                    daysToExpiry = 30,
                    howToApplyCode = "PSD", // please see description
                    contactName = "John Smith",
                    contactPhoneAreaCode = "03",
                    contactPhoneNumber = "85089161",
                    vacancyType = "H", // normal
                    returnMatchesFlag = false,
                    yourReference = string.Empty,
                }
            };

            var response = Call(request, (c, r) => c.AddVacancy(r));
            //AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        [TestMethod]
        public void CanSubmitNoStateJob()
        {
            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody
                {
                    orgCode = OrgCode,
                    siteCode = SiteCode,
                    employerID = EmployerId,
                    vacancyTitle = "Estimating Manager II",
                    occupationCode = "1112",
                    vacancySuburb = "BENTLEIGH",
                    vacancyPostcode = "3204",
                    stateCode = null, //"VIC",
                    vacancyDescription = "Description goes here",
                    positionLimit = 1,
                    workType = "F", //full time
                    duration = "N", //contract
                    daysToExpiry = 30,
                    howToApplyCode = "PSD", // please see description
                    contactName = "John Smith",
                    contactPhoneAreaCode = "03",
                    contactPhoneNumber = "85089161",
                    vacancyType = "H", // normal
                    returnMatchesFlag = false,
                    yourReference = string.Empty,
                }
            };

            var response = Call(request, (c, r) => c.AddVacancy(r));
            //AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        [TestMethod]
        public void CanSubmitNoContactNameJob()
        {
            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody
                {
                    orgCode = OrgCode,
                    siteCode = SiteCode,
                    employerID = EmployerId,
                    vacancyTitle = "Estimating Manager II",
                    occupationCode = "1112",
                    vacancySuburb = "BENTLEIGH",
                    vacancyPostcode = "3204",
                    stateCode = "VIC",
                    vacancyDescription = "Description goes here",
                    positionLimit = 1,
                    workType = "F", //full time
                    duration = "N", //contract
                    daysToExpiry = 30,
                    howToApplyCode = "PSD", // please see description
                    contactName = null, //"John Smith",
                    contactPhoneAreaCode = "03",
                    contactPhoneNumber = "85089161",
                    vacancyType = "H", // normal
                    returnMatchesFlag = false,
                    yourReference = string.Empty,
                }
            };

            var response = Call(request, (c, r) => c.AddVacancy(r));
            AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        [TestMethod]
        public void CanSubmitFormattedTextJob()
        {
            var description = "A Casual and Full Time opportunity now exist for customer service driven contact center operators, based in our state of the art Monitoring Centre servicing residential and commercial customer alarms.  \n  \nFamily owned and operated, SNP Security is Australia's largest integrated security solutions provider and strive to be an industry ?Employer of Choice?. Our values are fundamental to SNP Security's success, they are the foundation of our company, define who we are and set us apart from the competition. Five core values resonate at SNP Security: Customer Focus, Teamwork, Leadership, Growth and Development, and Family Company Values.  \n  \nYou will work along side an experienced team of operators and customer service staff servicing our key residential and commercial customers across Australia.  \n  \nTo be considered, you must posses:  \n?              Strong attention to detail \n?              Provisional Class 1A Security Licence - minimum requirement\n  \nAs an online role, a level of computer savviness will be required ? experience using MAS would be highly regarded but is not essential. \n  \nSNP employees have access to discounts at hundreds of stores, ongoing training and development and more. \n  \nInterested applicants can apply by visiting the SNP website at jobs.snpsecurity.com.au and applying for Vacancy 95: Monitoring Operator. Be sure to attach a resume with cover letter specifying what type of security licence you hold and what skills you possess that make you suitable for our vacancy.  \n  \nApplications close Friday 5th March 2010. \n  \nM/L 400674602";
            description = description.Replace("\xA0", "&nbsp;").Replace("\n", "<br/>");

            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody
                {
                    orgCode = OrgCode,
                    siteCode = SiteCode,
                    employerID = EmployerId,
                    vacancyTitle = "Monitoring Operator",
                    occupationCode = "1112",
                    vacancySuburb = "BENTLEIGH",
                    vacancyPostcode = "3204",
                    stateCode = "VIC",
                    vacancyDescription = description,
                    positionLimit = 1,
                    workType = "F", //Full time
                    duration = "N", //Contract
                    daysToExpiry = 30,
                    howToApplyCode = "PSD", // please see description
                    contactName = "John Smith",
                    contactPhoneAreaCode = "03",
                    contactPhoneNumber = "85089161",
                    vacancyType = "H", // normal
                    returnMatchesFlag = false,
                    yourReference = string.Empty,
                }
            };

            var response = Call(request, (c, r) => c.AddVacancy(r));
            AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        [TestMethod]
        public void CanSubmitHtmlJob()
        {
            var request = new AddVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new AddVacancyRequestBody
                {
                    orgCode = OrgCode,
                    siteCode = SiteCode,
                    employerID = EmployerId,
                    vacancyTitle = "Estimating Manager II",
                    occupationCode = "1112",
                    vacancySuburb = "BENTLEIGH",
                    vacancyPostcode = "3204",
                    stateCode = "VIC",
                    vacancyDescription = "<ul><li>Melbourne CBD</li><li>$60k+ Super</li><li>A great perm opportunity to work in an internationally acclaimed accounting firm</li></ul><b><span style=\"text-decoration:underline\">The Company</span></b>&nbsp;<br> This is a successful, mid tier accounting firm who has made its mark in the industry world wide. This organisation is growing rapidly and looking to expand on its current team. <br> &nbsp;<br> <b><span style=\"text-decoration:underline\">The Role</span></b><br> <ul> <li>Has an awareness of and builds rapport with key clients and staff </li> <li>Appointments entered as agreed.&nbsp; </li> <li>Answers all incoming calls for Partner's </li> <li>Messages are carefully recorded and communicated onto Partner </li> <li>Arrangements for passing messages on to Partners at clients' premises should be organised directly with the party concerned. </li> <li>Requests for meetings sensitively screened and prioritised </li> <li>Work is expected to be completed quickly and accurately </li> <li>Responsible for reviewing letters and document layout of any correspondence generated by your Partner's </li> <li>Responsible for all typing required by the Partner's.. </li> <li>Organises travel and accommodation for Partner's in accordance with firm policy </li> <li>Typed material may include: </li> <li>General correspondence, </li> <li>Dictation, </li> <li>Proposals and </li> <li>Presentation work </li> </ul> <br> This is organisation is looking for someone who has at least 2+ years professional services experience. It is also required that you have prior experience supporting high level executives.<br> &nbsp;<br> Applications will NOT be considered if this is not the case.&nbsp;<br> &nbsp;<br> <b><span style=\"text-decoration:underline\">Apply Today</span></b><br> Please send your resume by clicking on the apply now button",
                    positionLimit = 1,
                    workType = "F", //full time
                    duration = "N", //contract
                    daysToExpiry = 30,
                    howToApplyCode = "PSD", // please see description
                    contactName = "John Smith",
                    contactPhoneAreaCode = "03",
                    contactPhoneNumber = "85089161",
                    vacancyType = "H", // normal
                    returnMatchesFlag = false,
                    yourReference = string.Empty,
                }
            };

            var response = Call(request, (c, r) => c.AddVacancy(r));
            AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);

            var getRequest = new GetVacancyDetailsRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new GetVacancyDetailsRequestBody
                {
                    vacancyID = response.Body.vacancyID
                }
            };

            var getResponse = Call(getRequest, (c, r) => c.GetVacancyDetails(r));
            DeleteVacancy(getResponse.Body.vacancyID, getResponse.Body.integrityControlNumber);
        }

        [TestMethod]
        public void SubmitFailingJobTest()
        {
            var jobAdsQuery = Resolve<IJobAdsQuery>();
            var jobAd = jobAdsQuery.GetJobAd<JobAd>(new Guid("7b480950-0815-4cec-8e35-3b677a2eb692"));
            var mapper = new JobAdMapper(Resolve<IIndustriesQuery>());
            var request = new AddVacancyRequestMessage
                              {
                                  Body = mapper.CreateAddRequestBody(jobAd),
                                  Security = new Services.External.JobSearch.Security {UsernameToken = _usernameToken}
                              };

            request.Body.orgCode = OrgCode;
            request.Body.siteCode = SiteCode;
            request.Body.employerID = EmployerId;

            var ser = new System.Xml.Serialization.XmlSerializer(request.GetType());
            var stream = new StringWriter();
            ser.Serialize(stream, request);

            var response = Call(request, (c, r) => c.AddVacancy(r));
            AssertSuccess(response);
            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        [TestMethod]
        public void CanUpdateJob()
        {
            AssertServiceAvailable();

            var addRequest = new AddVacancyRequestMessage
                                 {
                                     Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                                     Body = new AddVacancyRequestBody
                                                {
                                                    orgCode = OrgCode,
                                                    siteCode = SiteCode,
                                                    employerID = EmployerId,
                                                    vacancyTitle = "Estimating Manager II",
                                                    occupationCode = "1112",
                                                    vacancySuburb = "BENTLEIGH",
                                                    vacancyPostcode = "3204",
                                                    stateCode = "VIC",
                                                    vacancyDescription = "Description goes here",
                                                    positionLimit = 1,
                                                    workType = "F", //full time
                                                    duration = "N", //Contract
                                                    daysToExpiry = 30,
                                                    howToApplyCode = "PSD", // please see description
                                                    contactName = "John Smith",
                                                    contactPhoneAreaCode = "03",
                                                    contactPhoneNumber = "85089161",
                                                    vacancyType = "H", // normal
                                                    returnMatchesFlag = false,
                                                    yourReference = string.Empty,
                                                }
                                 };

            var channel = _channelManager.Create();
            try
            {
                var addResponse = channel.AddVacancy(addRequest);

                if (addResponse.Header.executionStatus == EsiExecutionStatus.Failed)
                {
                    _channelManager.Close(channel);

                    var errorMessages = string.Empty;
                    foreach (var message in addResponse.Header.Messages)
                    {
                        errorMessages += "\r\n" + message.text;
                    }

                    Assert.Fail(string.Format("AddVacancy failed with messages : {0}", errorMessages));
                }

                new GetVacancyDetailsRequestMessage
                    {
                        Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                        Body = new GetVacancyDetailsRequestBody
                                   {
                                       vacancyID = addResponse.Body.vacancyID
                                   }
                    };

                var updateRequest = new UpdateVacancyRequestMessage
                                        {
                                            Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                                            Body = new UpdateVacancyRequestBody
                                                       {
                                                           orgCode = OrgCode,
                                                           siteCode = SiteCode,
                                                           employerID = EmployerId,
                                                           vacancyID = addResponse.Body.vacancyID,
                                                           integrityControlNumber = addResponse.Body.integrityControlNumber,
                                                           detailsIntegrityControlNumber = addResponse.Body.detailsIntegrityControlNumber,
                                                           agentIntegrityControlNumber = addResponse.Body.agentIntegrityControlNumber,
                                                           contactIntegrityControlNumber = addResponse.Body.contactIntegrityControlNumber,
                                                           contactAddressIntegrityControlNumber = addResponse.Body.contactAddressIntegrityControlNumber,
                                                           vacancyTitle = "Estimating Manager II",
                                                           occupationCode = "1112",
                                                           vacancySuburb = "BENTLEIGH",
                                                           vacancyPostcode = "3204",
                                                           stateCode = "VIC",
                                                           vacancyDescription = "Updated description goes here",
                                                           positionLimit = 1,
                                                           workType = "F", //full time
                                                           duration = "N", //Contract
                                                           daysToExpiry = 30,
                                                           howToApplyCode = "PSD", // please see description
                                                           contactName = "John Smith",
                                                           contactPhoneAreaCode = "03",
                                                           contactPhoneNumber = "85089161",
                                                           vacancyType = "H", // normal
                                                           yourReference = string.Empty,
                                                       }
                                        };

                var updateResponse = channel.UpdateVacancy(updateRequest);

                DeleteVacancy(addResponse.Body.vacancyID, updateResponse.Body.integrityControlNumber);

                if (updateResponse.Header.executionStatus == EsiExecutionStatus.Failed)
                {
                    _channelManager.Close(channel);

                    var errorMessages = string.Empty;
                    foreach (var message in updateResponse.Header.Messages)
                    {
                        errorMessages += "\r\n" + message.text;
                    }

                    Assert.Fail(string.Format("UpdateVacancy failed with messages : {0}", errorMessages));
                }
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }

            _channelManager.Close(channel);
        }

        [TestMethod, Ignore]
        public void DeleteJob()
        {
            const long vacancyToDelete = 2206429676;

            var getRequest = new GetVacancyDetailsRequestMessage
                                 {
                                     Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                                     Body = new GetVacancyDetailsRequestBody
                                                {
                                                    vacancyID = vacancyToDelete
                                                }
                                 };

            var channel = _channelManager.Create();
            var response = channel.GetVacancyDetails(getRequest);
            _channelManager.Close(channel);

            if (response.Header.executionStatus == EsiExecutionStatus.Failed)
            {
                var errorMessages = string.Empty;
                foreach (var message in response.Header.Messages)
                {
                    errorMessages += "\r\n" + message.text;
                }

                Assert.Fail(string.Format("AddVacancy failed with messages : {0}", errorMessages));
            }

            if (response.Body.statusCode == "D")
            {
                Assert.Fail("Vacancy already deleted");
            }

            DeleteVacancy(response.Body.vacancyID, response.Body.integrityControlNumber);
        }

        private void CanAuthenticate<TRequest, TResponse>(TRequest request, Func<IPublicVacancy, TRequest, TResponse> call, EsiExecutionStatus expectedStatus, Func<TResponse, EsiExecutionStatus> getStatus)
        {
            AssertServiceAvailable();

            var channel = _channelManager.Create();
            try
            {
                var response = call(channel, request);
                _channelManager.Close(channel);

                Assert.AreEqual(expectedStatus, getStatus(response));
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
        }

        private TResponse Call<TRequest, TResponse>(TRequest request, Func<IPublicVacancy, TRequest, TResponse> call)
        {
            AssertServiceAvailable();
            var channel = _channelManager.Create();
            try
            {
                var response = call(channel, request);
                _channelManager.Close(channel);
                return response;
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
        }

        private static void AssertServiceAvailable()
        {
            var now = DateTime.Now;

            if (now.DayOfWeek >= DayOfWeek.Monday && now.DayOfWeek <= DayOfWeek.Friday && now.Hour >= 8 && now.Hour <= 23)
                return;

            if (now.DayOfWeek == DayOfWeek.Saturday && now.Hour >= 9 && now.Hour <= 13)
                return;

            Assert.Inconclusive("Service is unavailable.");
        }

        private static void AssertSuccess(AddVacancyResponseMessage response)
        {
            if (response.Header.executionStatus == EsiExecutionStatus.Failed)
            {
                var errorMessages = string.Empty;
                foreach (var message in response.Header.Messages)
                    errorMessages += "\r\n" + message.text;
                Assert.Fail(string.Format("AddVacancy failed with messages : {0}", errorMessages));
            }
        }

        private void DeleteVacancy(long vacancyId, int integrityControlNumber)
        {
            var request = new DeleteVacancyRequestMessage
            {
                Security = new Services.External.JobSearch.Security { UsernameToken = _usernameToken },
                Body = new DeleteVacancyRequestBody
                {
                    vacancyID = vacancyId,
                    integrityControlNumber = integrityControlNumber,
                }
            };

            Call(request, (c, r) => c.DeleteVacancy(r));
        }
    }
}