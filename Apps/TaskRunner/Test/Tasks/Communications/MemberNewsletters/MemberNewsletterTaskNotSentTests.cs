using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.MemberNewsletters
{
    [TestClass]
    public class MemberNewsletterTaskNotSentTests
        : MemberNewsletterTaskTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNotSent()
        {
            var member = CreateMember(0, null, null, true, true, DateTime.Now.AddDays(-50));
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            AssertEmails(new[] { member });
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotSentJustJoined()
        {
            CreateMember(0, null, null, true, true, DateTime.Now.AddDays(-10));
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotEnabled()
        {
            CreateMember(0, null, null, false, true, DateTime.Now.AddDays(-50));
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotActivated()
        {
            CreateMember(0, null, null, true, false, DateTime.Now.AddDays(-50));
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestFrequencyNever()
        {
            CreateMember(0, Frequency.Never, null, true, true, DateTime.Now.AddDays(-50));
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestAlreadySent()
        {
            CreateMember(0, null, DateTime.Now.AddDays(-30), true, true, DateTime.Now.AddDays(-50));
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotSentMaxMembers()
        {
            const int period = 100;
            const int totalMembers = 25;
            const int maxMembers = 10;

            var joined = DateTime.Now.AddDays(-1 * period);
            var totalExpectedMembers = new SortedList<DateTime, Member>();

            for (var index = 0; index < totalMembers; ++index)
            {
                var joinedTime = joined.AddDays(index);
                var member = CreateMember(index, null, null, true, true, joinedTime);
                totalExpectedMembers.Add(joinedTime, member);
            }

            var leftMembers = totalMembers;
            while (leftMembers > 0)
            {
                // Execute the task.

                new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range(0, maxMembers));

                // Check for emails.

                var expectedMembers = totalExpectedMembers.Skip(totalMembers - leftMembers).Take(maxMembers).Select(p => p.Value).ToList();
                var emails = _emailServer.AssertEmailsSent(expectedMembers.Count);

                // The members should be the same but not necessarily in the same order.

                for (var emailIndex = 0; emailIndex < emails.Length; ++emailIndex)
                {
                    var email = emails[emailIndex];
                    var expectedMember = (from m in expectedMembers where m.EmailAddresses[0].Address == email.To[0].Address select m).Single();

                    email.AssertAddresses(Return, Return, expectedMember);
                    email.AssertHtmlViewContains(expectedMember.FirstName);
                }

                leftMembers = leftMembers - maxMembers;
            }

            // Execute again, checking for no emails.

            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteNotSent(new Range());
            _emailServer.AssertNoEmailSent();
        }
    }
}