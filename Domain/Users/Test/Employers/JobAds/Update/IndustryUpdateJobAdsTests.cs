using System.Linq;
using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Update
{
    [TestClass]
    public class IndustryUpdateJobAdsTests
        : UpdateJobAdsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        [TestMethod]
        public void TestAddIndustries()
        {
            var employer = CreateEmployer();
            var industries = _industriesQuery.GetIndustries();

            // No industries.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);

            // Add industries.

            jobAd.Description.Industries = new List<Industry> { industries[2] };
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);
        }

        [TestMethod]
        public void TestRemoveIndustries()
        {
            var employer = CreateEmployer();
            var industries = _industriesQuery.GetIndustries();

            // Create with industries.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                    Industries = new List<Industry> { industries[0], industries[1], }
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            // Remove them by setting to null.

            jobAd.Description.Industries = null;
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);

            // Create with industries.

            jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                    Industries = new List<Industry> { industries[0], industries[1], }
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            // Remove them by clearing.

            jobAd.Description.Industries.Clear();
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Clearing them will actually delete the list.

            jobAd.Description.Industries = null;
            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);
        }

        [TestMethod]
        public void TestUpdateIndustries()
        {
            var employer = CreateEmployer();
            var industries = _industriesQuery.GetIndustries();

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                    Industries = new List<Industry>
                    {
                        industries[0],
                        industries[1],
                    }
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            // Add more.

            jobAd.Description.Industries.Add(industries[2]);
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);

            // Remove some.

            jobAd.Description.Industries.RemoveAt(0);
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);

            // Replace.

            jobAd.Description.Industries[0] = industries[3];
            jobAd.Description.Industries[1] = industries[4];
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertIndustries(jobAd.Description.Industries, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Industries);
        }

        private static void AssertIndustries(ICollection<Industry> expectedIndustries, ICollection<Industry> industries)
        {
            if (expectedIndustries == null)
            {
                Assert.IsNull(industries);
            }
            else
            {
                Assert.IsNotNull(industries);
                Assert.AreEqual(expectedIndustries.Count, industries.Count);

                var orderedExpectedIndustries = expectedIndustries.OrderBy(i => i.Name).ToList();
                var orderedIndustries = industries.OrderBy(i => i.Name).ToList();
                for (var index = 0; index < expectedIndustries.Count; ++index)
                    Assert.AreEqual(orderedExpectedIndustries[index].Id, orderedIndustries[index].Id);
            }
        }
    }
}