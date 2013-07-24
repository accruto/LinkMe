using System;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Communities
{
    public abstract class CommunityTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestInitialize]
        public void CommunityTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSearchHost.ClearIndex();
        }

        protected Community CreateCommunity(int index)
        {
            switch (index)
            {
                case 1:
                    return TestCommunity.UniMelbArts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

                default:
                    return TestCommunity.Scouts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            }
        }

        protected JobAd PostJobAd(string jobTitle, int index, IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd(GetJobTitle(jobTitle, index), "The content for job #" + index);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private static string GetJobTitle(string jobTitle, int index)
        {
            return jobTitle + " job #" + index;
        }

        protected Employer CreateEmployer(int index, Community community)
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(index);
            if (community != null)
            {
                organisation.AffiliateId = community.Id;
                _organisationsCommand.UpdateOrganisation(organisation);
            }

            return _employerAccountsCommand.CreateTestEmployer(index, organisation);
        }

        protected virtual Member CreateMember(int index, Community community)
        {
            return _memberAccountsCommand.CreateTestMember(index, community == null ? (Guid?)null : community.Id);
        }
    }
}