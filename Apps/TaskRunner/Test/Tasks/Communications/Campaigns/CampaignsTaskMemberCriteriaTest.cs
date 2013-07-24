using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Members;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskMemberCriteriaTest
        : CampaignsTaskTests
    {
        [TestMethod]
        public void TestDefaultCriteria()
        {
            var index = 0;
            var criteria = new MemberSearchCriteria();

            // No members.

            TestSearch(++index, criteria, new Member[0]);

            // Create some members.

            var members = CreateMembers(0, 6);

            // Default criteria should return everyone.

            criteria = new MemberSearchCriteria();
            TestSearch(++index, criteria, members);
        }

        [TestMethod]
        public void TestIndustriesCriteria()
        {
            var index = 0;
            var industries = GetIndustries();

            // Create some members in one industry.

            var members0 = CreateMembers(0, 6, industries[0]);

            // Create some members in another industry.

            var members1 = CreateMembers(6, 8, industries[1]);

            // Create some members in both.

            var members01 = CreateMembers(14, 4, industries[0], industries[1]);

            // Create some members in neither.

            var members2 = CreateMembers(18, 2, industries[2]);

            // Create some members in none.

            var members = CreateMembers(20, 5);

            // Default criteria should return everyone.

            var criteria = new MemberSearchCriteria();
            TestSearch(++index, criteria, members0.Concat(members1).Concat(members01).Concat(members2).Concat(members));

            // Industry 0

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[0].Id}};
            TestSearch(++index, criteria, members0.Concat(members01));

            // Industry 1

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[1].Id}};
            TestSearch(++index, criteria, members1.Concat(members01));

            // Industry 2

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[2].Id}};
            TestSearch(++index, criteria, members2);

            // Industry 0 & 1

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[0].Id, industries[1].Id}};
            TestSearch(++index, criteria, members0.Concat(members1).Concat(members01));

            // Industry 0 & 2

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[0].Id, industries[2].Id}};
            TestSearch(++index, criteria, members0.Concat(members01).Concat(members2));

            // Industry 1 & 2

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[1].Id, industries[2].Id}};
            TestSearch(++index, criteria, members1.Concat(members01).Concat(members2));

            // Industry 0, 1 & 2

            criteria = new MemberSearchCriteria {IndustryIds = new[] {industries[0].Id, industries[1].Id, industries[2].Id}};
            TestSearch(++index, criteria, members0.Concat(members1).Concat(members01).Concat(members2));

            // All

            criteria = new MemberSearchCriteria {IndustryIds = industries.Select(i => i.Id).ToArray()};
            TestSearch(++index, criteria, members0.Concat(members1).Concat(members01).Concat(members2).Concat(members));
        }

        private static IList<Industry> GetIndustries()
        {
            return Resolve<IIndustriesQuery>().GetIndustries();
        }

        private void TestSearch(int index, Criteria criteria, IEnumerable<Member> expected)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(index, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);

            // Run the task.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(expected.Count()).OrderBy(e => e.To[0].Address).ToArray();
            for (var memberIndex = 0; memberIndex < emails.Length; ++memberIndex)
                Assert.AreEqual(expected.ElementAt(memberIndex).GetBestEmailAddress().Address, emails[memberIndex].To[0].Address);
        }
    }
}