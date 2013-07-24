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
    public class MemberNewsletterTaskAlreadySentSetTests
        : MemberNewsletterTaskTests
    {
        private const int Period = 30;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestAlreadySentBeforePeriod()
        {
            var joined = DateTime.Now.AddDays(-3 * Period);
            var lastSent = DateTime.Now.AddDays(-2 * Period);

            var member = CreateMember(0, Frequency.Monthly, lastSent, true, true, joined);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            AssertEmails(new[] { member });
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestAlreadySentAfterPeriod()
        {
            var joined = DateTime.Now.AddDays(-0.5 * Period);
            var lastSent = DateTime.Now.AddDays(-0.3 * Period);

            CreateMember(0, Frequency.Monthly, lastSent, true, true, joined);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestAlreadySentNotEnabled()
        {
            var joined = DateTime.Now.AddDays(-3 * Period);
            var lastSent = DateTime.Now.AddDays(-2 * Period);

            CreateMember(0, Frequency.Monthly, lastSent, false, true, joined);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestAlreadySentNotActivated()
        {
            var joined = DateTime.Now.AddDays(-3 * Period);
            var lastSent = DateTime.Now.AddDays(-2 * Period);

            CreateMember(0, Frequency.Monthly, lastSent, true, false, joined);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestAlreadySentFrequencyNever()
        {
            var joined = DateTime.Now.AddDays(-3 * Period);
            var lastSent = DateTime.Now.AddDays(-2 * Period);

            CreateMember(0, Frequency.Never, lastSent, true, true, joined);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotSent()
        {
            var joined = DateTime.Now.AddDays(-3 * Period);
            CreateMember(0, Frequency.Monthly, null, true, true, joined);
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
            new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range());
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotSentMaxMembers()
        {
            const int period = 30;
            const int totalMembers = 25;
            const int maxMembers = 10;

            var joined = DateTime.Now.AddDays(-3 * period);
            var lastSent = DateTime.Now.AddDays(-2 * Period);
            var totalExpectedMembers = new SortedList<DateTime, Member>();

            for (var index = 0; index < totalMembers; ++index)
            {
                var joinedTime = joined.AddDays(index);
                var lastSentTime = lastSent.AddDays(index);
                var member = CreateMember(index, Frequency.Monthly, lastSentTime, true, true, joinedTime);
                totalExpectedMembers.Add(joinedTime, member);
            }

            var leftMembers = totalMembers;
            while (leftMembers > 0)
            {
                // Execute the task.

                new EmailMemberNewsletterTask(_emailsCommand, _settingsQuery, _memberCommunicationsQuery, _membersQuery).ExecuteAlreadySent(new Range(0, maxMembers));

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