using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.Test.SoapJobG8;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostAdvertRequest=LinkMe.Apps.Integration.Test.SoapJobG8.PostAdvertRequest;

namespace LinkMe.Apps.Integration.Test.JobG8.WebServices
{
    [TestClass]
    public abstract class WebServiceTests
        : JobG8Tests
    {
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();

        [TestInitialize]
        public void WebServiceTestsInitialize()
        {
            JobAdSearchHost.ClearIndex();
            JobAdSortHost.ClearIndex();
        }

        protected AdvertPostService CreateService()
        {
            return new AdvertPostService
            {
                Url = new ReadOnlyApplicationUrl("~/JobG8/AdvertPostService.svc").AbsoluteUri,
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password,
                },
            };
        }

        protected PostAdvertRequest CreateRequest(Employer employer, string position)
        {
            return new PostAdvertRequest
            {
                Adverts = new PostAdvertRequestAdverts
                {
                    AccountNumber = employer.GetLoginId(),
                    PostAdvert = new[]
                    {
                        new PostAdvertRequestAdvertsPostAdvert
                        {
                            JobReference = "RefABCD/1235",
                            ClientReference = "RefABCD",
                            Classification = "Accounting",
                            SubClassification = "Accountant",
                            Position = position,
                            Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                            Location = "Sydney",
                            Area = "Sydney Inner",
                            PostCode = "2000",
                            Country = "Australia",
                            VisaRequired = PostAdvertRequestAdvertsPostAdvertVisaRequired.Applicantsmustbeeligibletoworkinthespecifiedlocation,
                            PayPeriod = PostAdvertRequestAdvertsPostAdvertPayPeriod.Annual,
                            PayAmount = 100000, PayAmountSpecified = true,
                            Currency = "AUS",
                            Contact = "John Bloomfield",
                            EmploymentType = PostAdvertRequestAdvertsPostAdvertEmploymentType.Permanent,
                            WorkHoursSpecified = false,
                            WorkHours = default(PostAdvertRequestAdvertsPostAdvertWorkHours),
                            RedirectionUrl = "http://www.jobg8.com/Redirection.aspx?jbid=52&amp;jid=5384886&amp;email=[[candidateemailaddress]]",
                        }
                    }
                }
            };
        }

        protected PostAdvertRequest CreateRequest(Employer employer, string position1, string position2)
        {
            return new PostAdvertRequest
            {
                Adverts = new PostAdvertRequestAdverts
                {
                    AccountNumber = employer.GetLoginId(),
                    PostAdvert = new[]
                    {
                        new PostAdvertRequestAdvertsPostAdvert
                        {
                            JobReference = "RefABCD/1235",
                            ClientReference = "RefABCD",
                            Classification = "Accounting",
                            SubClassification = "Accountant",
                            Position = position1,
                            Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                            Location = "Sydney",
                            Area = "Sydney Inner",
                            PostCode = "2000",
                            Country = "Australia",
                            VisaRequired = PostAdvertRequestAdvertsPostAdvertVisaRequired.Applicantsmustbeeligibletoworkinthespecifiedlocation,
                            PayPeriod = PostAdvertRequestAdvertsPostAdvertPayPeriod.Annual,
                            PayAmount = 100000, PayAmountSpecified = true,
                            Currency = "AUS",
                            Contact = "John Bloomfield",
                            EmploymentType = PostAdvertRequestAdvertsPostAdvertEmploymentType.Permanent,
                            WorkHoursSpecified = false,
                            WorkHours = default(PostAdvertRequestAdvertsPostAdvertWorkHours),
                        },
                        new PostAdvertRequestAdvertsPostAdvert
                        {
                            JobReference = "RefABCD/1236",
                            ClientReference = "RefABCD",
                            Classification = "Accounting",
                            SubClassification = "Accountant",
                            Position = position2,
                            Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                            Location = "Sydney",
                            Area = "Sydney Inner",
                            PostCode = "2000",
                            Country = "Australia",
                            VisaRequired = PostAdvertRequestAdvertsPostAdvertVisaRequired.Applicantsmustbeeligibletoworkinthespecifiedlocation,
                            PayPeriod = PostAdvertRequestAdvertsPostAdvertPayPeriod.Annual,
                            PayAmount = 100000, PayAmountSpecified = true,
                            Currency = "AUS",
                            Contact = "John Bloomfield",
                            EmploymentType = PostAdvertRequestAdvertsPostAdvertEmploymentType.Permanent,
                            WorkHoursSpecified = false,
                            WorkHours = default(PostAdvertRequestAdvertsPostAdvertWorkHours),
                        }
                    }
                }
            };
        }

        protected IList<JobAd> Search(string keywords)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(keywords);

            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);
            return _jobAdsQuery.GetJobAds<JobAd>(execution.Results.JobAdIds);
        }

        protected static void AssertJobAds(PostAdvertRequest request, IList<JobAd> jobAds)
        {
            Assert.AreEqual(request.Adverts.PostAdvert.Length, jobAds.Count);
            foreach (var advert in request.Adverts.PostAdvert)
            {
                var jobReference = advert.JobReference;
                var jobAd = (from j in jobAds where j.Integration.IntegratorReferenceId == jobReference select j).Single();
                Assert.AreEqual(advert.ClientReference, jobAd.Integration.ExternalReferenceId);
                Assert.AreEqual(advert.Position, jobAd.Title);
                Assert.AreEqual(advert.RedirectionUrl, jobAd.Integration.ExternalApplyUrl);
            }
        }
    }
}