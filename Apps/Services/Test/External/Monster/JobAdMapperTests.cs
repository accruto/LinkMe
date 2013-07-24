using System;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.Monster;
using LinkMe.Apps.Services.External.Monster.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkMe.Apps.Mocks.Hosts;

namespace LinkMe.Apps.Services.Test.External.Monster
{
    [TestClass]
    public class JobAdMapperTests
        : MonsterFeedTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        [TestMethod]
        public void TestCanMapProductionFeed()
        {
            MapProductionFeed(FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\Monster\SampleFeed.xml", RuntimeEnvironment.GetSourceFolder()), false);
        }

        [TestMethod]
        public void TestCanMapProductionFeed2()
        {
            MapProductionFeed(FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\Monster\SampleFeed2.xml", RuntimeEnvironment.GetSourceFolder()), false);
        }

        private void MapProductionFeed(string file, bool saveParsedData)
        {
            if (saveParsedData)
            {
                JobAdSearchHost.Start();
                JobAdSortHost.Start();
            }

            const string postId = "123456";
            var jobPosterId = new Guid("D12C0E2E-E464-491C-96D2-15D79F98E506");
            var integrationUserId = _careerOneQuery.GetIntegratorUser().Id;

            var posts = GetJobFeed(file);
            IJobFeedMapper<Job> mapper = new JobAdMapper(_locationQuery, _industriesQuery, null);

            foreach (var post in posts)
            {
                var jobAd = new JobAd
                {
                    PosterId = jobPosterId,
                    Integration =
                    {
                        IntegratorReferenceId = postId,
                        ExternalApplyUrl = string.Format("http://jobview.careerone.com.au/GetJob.aspx?JobID={0}", postId),
                        IntegratorUserId = integrationUserId,
                    },
                };
                mapper.ApplyPostData(post, jobAd);

                if (saveParsedData)
                {
                    _jobAdsCommand.CreateJobAd(jobAd);
                    _jobAdsCommand.OpenJobAd(jobAd);
                }
            }
        }
    }
}