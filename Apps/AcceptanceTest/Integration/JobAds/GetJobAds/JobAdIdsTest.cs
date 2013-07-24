using System;
using System.Collections.Generic;
using System.Xml;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.GetJobAds
{
    [TestClass]
    public class JobAdIdsTest
        : IntegrationTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();

        [TestMethod]
        public void TestJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            // Get.

            var response = JobAdIds(integratorUser);
            Assert.AreEqual(jobAd.Id, GetJobAdId(response));
        }

        [TestMethod]
        public void TestCareerOneJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            // Get.

            var response = JobAdIds(integratorUser);
            Assert.AreEqual(jobAd.Id, GetJobAdId(response));

            // Make it a CareerOne job ad.

            jobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Should still be returned.

            response = JobAdIds(integratorUser);
            Assert.AreEqual(jobAd.Id, GetJobAdId(response));
        }

        [TestMethod]
        public void TestCommunityJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();

            var community = TestCommunity.UniMelbArts.CreateTestCommunity(_communitiesCommand, _verticalsCommand);
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            organisation.AffiliateId = community.Id;
            _organisationsCommand.UpdateOrganisation(organisation);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);

            PostJobAd(employer);

            // Get.

            var response = JobAdIds(integratorUser);
            Assert.IsNull(GetJobAdId(response));
        }

        private static Guid? GetJobAdId(string response)
        {
            var document = new XmlDocument();
            document.LoadXml(response);
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("lm", Apps.Services.Constants.XmlSerializationNamespace);
            var nodes = document.SelectNodes("/lm:GetJobAdIdsResponse/lm:JobAd", nsmgr);
            Assert.IsNotNull(nodes);
            if (nodes.Count == 0)
                return null;
            return new Guid(nodes[0].Attributes["id"].Value);
        }

        private JobAd PostJobAd(IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd();
            jobAd.Description.Summary = "Summary";
            jobAd.Description.Industries = new List<Industry> { _industriesQuery.GetIndustries()[0] };
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private static string JobAdIds(IntegratorUser user)
        {
            return Get(new ReadOnlyApplicationUrl("~/jobadids"), user, Password, true);
        }
    }
}