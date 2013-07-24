using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Members;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignFileTaskMemberTest
        : CampaignsTaskTests
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestMethod]
        public void TestSend()
        {
            // Create some members.

            var members = CreateMembers(0, 6);

            // Default criteria should return everyone.

            TestSend(members.Take(3));
        }

        private void TestSend(IEnumerable<Member> members)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(0, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            _campaignsCommand.UpdateCriteria(campaign.Id, new MemberSearchCriteria());

            // Run the task.

            using (var file = _filesCommand.SaveTempFile(CreateFileContents(members), "LoginIds.txt"))
            {
                new CampaignFileTask().ExecuteTask(new[] { campaign.Name, file.FilePaths[0] });
                var emails = _emailServer.AssertEmailsSent(members.Count()).OrderBy(e => e.To[0].Address).ToArray();
                for (var memberIndex = 0; memberIndex < emails.Length; ++memberIndex)
                    Assert.AreEqual(members.ElementAt(memberIndex).GetBestEmailAddress().Address, emails[memberIndex].To[0].Address);
            }
        }

        private static string CreateFileContents(IEnumerable<Member> members)
        {
            var sb = new StringBuilder();
            foreach (var member in members)
                sb.AppendLine(member.GetBestEmailAddress().Address);
            return sb.ToString();
        }
    }
}