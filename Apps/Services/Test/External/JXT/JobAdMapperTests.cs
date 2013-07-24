using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.External.Jxt;
using LinkMe.Apps.Services.External.Jxt.Schema;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkMe.Apps.Mocks.Hosts;

namespace LinkMe.Apps.Services.Test.External.Jxt
{
    [TestClass]
    public class JobAdMapperTests
        : JxtFeedTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJxtQuery _jxtQuery = Resolve<IJxtQuery>();

        [TestMethod]
        public void TestCanMapProductionFeed()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            MapProductionFeed(FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\JXT\SampleFeed.xml", RuntimeEnvironment.GetSourceFolder()), false);
        }

        private void MapProductionFeed(string file, bool saveParsedData)
        {
            if (saveParsedData)
            {
                JobAdSearchHost.Start();
                JobAdSortHost.Start();
            }

            const string postId = "123456";
            var jobPosterId = CreateEmployer(0).Id;
            var integrationUserId = _jxtQuery.GetIntegratorUser().Id;

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
                        ExternalApplyUrl = post.ApplicationMethod.Value,
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

        private Employer CreateEmployer(int index)
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();

            var unlimited = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, OwnerId = unlimited.Id, InitialQuantity = null });

            return unlimited;
        }
    }
}