using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.MemberNewsletters
{
    [TestClass]
    public class MemberNewsletterTaskAllTests
        : MemberNewsletterTaskTests
    {
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IMemberAffiliationsCommand _memberAffiliationsCommand = Resolve<IMemberAffiliationsCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestAll()
        {
            var index = 0;
            var expectedMembers = new SortedList<DateTime, Member>();

            AddNotSent(ref index, expectedMembers);
            AddAlreadySent(ref index, expectedMembers, false);
            AddAlreadySent(ref index, expectedMembers, true);

            // Execute the task.

            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            AssertAllEmails(expectedMembers);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNoCommunityMembers()
        {
            // Whilst some copy etc gets sorted out for specific communities no members sould actually get the email so check this.

            var index = 0;
            var expectedMembers = new SortedList<string, Member>();
            var community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Not sent.

            var joined = DateTime.Now.AddDays(-40);
            var member = CreateMember(++index, null, null, true, true, joined);
            _memberAffiliationsCommand.SetAffiliation(member.Id, community.Id);

            member = CreateMember(++index, null, null, true, true, joined);
            expectedMembers.Add(member.GetBestEmailAddress().Address, member);

            // Already sent.

            var lastSent = DateTime.Now.AddDays(-35);
            member = CreateMember(++index, null, lastSent.AddMinutes(-1 * index), true, true, joined.AddMinutes(index));
            _memberAffiliationsCommand.SetAffiliation(member.Id, community.Id);

            member = CreateMember(++index, null, lastSent.AddMinutes(-1 * index), true, true, joined.AddMinutes(index));
            expectedMembers.Add(member.GetBestEmailAddress().Address, member);

            // Execute the task.

            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            AssertEmails(expectedMembers.Values);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestDisabled()
        {
            var members = new Member[0];
            CreateMember(0, null, null, false, false, DateTime.Now.AddDays(-40));

            // Execute.

            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            AssertEmails(members);
        }

        [TestMethod]
        public void TestInactive()
        {
            var members = new Member[0];
            CreateMember(0, null, null, true, false, DateTime.Now.AddDays(-40));

            // Execute.

            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            AssertEmails(members);
        }

        [TestMethod]
        public void TestDeactived()
        {
            var members = new Member[0];
            var member = CreateMember(0, null, null, true, true, DateTime.Now.AddDays(-40));
            _emailVerificationsCommand.UnverifyEmailAddress(member.GetPrimaryEmailAddress().Address, "reason");

            // Execute.

            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteTask();
            AssertEmails(members);
        }

        private void AddNotSent(ref int index, IDictionary<DateTime, Member> expectedMembers)
        {
            const int period = 30;
            var joined = DateTime.Now.AddDays(-1 * period);

            // These are the members that should be returned, ie:
            // - they are active
            // - they have not received a status update email yet
            // - they have not turned off update emails

            var joinedTime = joined.AddMinutes(-1 * index);

            var member = CreateMember(++index, null, null, true, true, joinedTime);
            expectedMembers.Add(joinedTime, member);

            joinedTime = joined.AddMinutes(-1 * index);
            member = CreateMember(++index, null, null, true, true, joinedTime);
            expectedMembers.Add(joinedTime, member);

            // Not enabled.

            CreateMember(++index, null, null, false, true, joined);

            // Not activated.

            CreateMember(++index, null, null, true, false, joined);

            // Suppressed emails.

            CreateMember(++index, Frequency.Never, null, true, true, joined);
        }

        private void AddAlreadySent(ref int index, IDictionary<DateTime, Member> expectedMembers, bool frequencySet)
        {
            const int period = 30;

            // These are the members that should be returned, ie:
            // - they are active
            // - they have already received a status update email but not within the period
            // - they have not turned off update emails
            // - they have not explicitly set their frequency

            var frequency = frequencySet ? Frequency.Monthly : (Frequency?) null;
            var joined = DateTime.Now.AddDays(-3 * period);
            var lastSent = DateTime.Now.AddDays(-2 * period);

            var lastSentTime = lastSent.AddMinutes(-1 * index);
            var member = CreateMember(++index, frequency, lastSentTime, true, true, joined.AddMinutes(index));
            expectedMembers.Add(lastSentTime, member);

            // Not enabled.

            CreateMember(++index, frequency, lastSent.AddMinutes(-1 * index), false, true, joined.AddMinutes(index));

            // Not activated.

            CreateMember(++index, frequency, lastSent.AddMinutes(-1 * index), true, false, joined.AddMinutes(index));

            // Suppressed emails.

            CreateMember(++index, Frequency.Never, lastSent.AddMinutes(-1 * index), true, true, joined.AddMinutes(index));
        }

        private void AssertAllEmails(ICollection<KeyValuePair<DateTime, Member>> expectedMembers)
        {
            // The order of members will be all mixed up because members are getting emails for different readings.
            // Get all emails from both collections and then compare.

            var emails = _emailServer.AssertEmailsSent(expectedMembers.Count).Select(e => e.To[0].Address).OrderBy(e => e).ToList();
            var expectedEmails = expectedMembers.Select(m => m.Value.GetBestEmailAddress().Address).OrderBy(e => e).ToList();
            for (var index = 0; index < emails.Count; ++index)
                Assert.AreEqual(expectedEmails[index], emails[index]);
        }
    }
}