using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.MemberUpdates;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.MemberNewsletter
{
    [TestClass]
    public class EmailMemberNewsletterTests
        : MemberNewsletterTests
    {
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();

        private const string EmailAddressFormat = "test{0}@test.linkme.net.au";

        private int _maxMembers = -1;

        [TestMethod]
        public void TestAll()
        {
            _maxMembers = -1;

            var index = 0;
            var expectedMembers = new List<Member>();

            AddNotSent(ref index, expectedMembers, 1, _maxMembers);
            AddAlreadySentWithoutSetting(ref index, expectedMembers, 1, _maxMembers);
            AddAlreadySentWithSetting(ref index, expectedMembers, 1, _maxMembers);

            // Execute the task.

            Execute(true);
            AssertEmails(expectedMembers);
            Execute(true);
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestMultiple()
        {
            // Make sure it is unlimited.

            _maxMembers = -1;

            var index = 0;
            var expectedMembers = new List<Member>();

            AddNotSent(ref index, expectedMembers, 2, _maxMembers);
            AddAlreadySentWithoutSetting(ref index, expectedMembers, 2, _maxMembers);
            AddAlreadySentWithSetting(ref index, expectedMembers, 2, _maxMembers);

            // Execute the task.

            Execute(true);
            AssertEmails(expectedMembers);
            Execute(true);
            _emailServer.AssertNoEmailSent();
        }

        protected override string GetTaskArgs()
        {
            return _maxMembers != -1 ? _maxMembers.ToString() : base.GetTaskArgs();
        }

        private void AddNotSent(ref int index, ICollection<Member> expectedMembers, int count, int maxMembers)
        {
            const int period = 50;
            var joined = DateTime.Now.AddDays(-1 * period);

            // These are the members that should be returned, ie:
            // - they are active
            // - they have not received a status update email yet
            // - they have not turned off update emails

            var addedMembers = new List<Member>();
            for (var i = 0; i < count; ++i)
                addedMembers.Add(CreateMember(++index, null, null, true, true, joined));

            var enumerable = maxMembers == -1 ? addedMembers : addedMembers.OrderBy(m => m.CreatedTime).Take(maxMembers);
            foreach (var member in enumerable)
                expectedMembers.Add(member);
        }

        private void AddAlreadySentWithoutSetting(ref int index, ICollection<Member> expectedMembers, int count, int maxMembers)
        {
            // These are the members that should be returned, ie:
            // - they are active
            // - they have already received a status update email but not within the period
            // - they have not turned off update emails
            // - they have not explicitly set their frequency

            const int period = 30;
            var joined = DateTime.Now.AddDays(-3 * period);
            var lastSent = DateTime.Now.AddDays(-2 * period);

            var addedMembers = new SortedList<DateTime, Member>();
            for (var i = 0; i < count; ++i)
            {
                var lastSentTime = lastSent.AddMinutes(-1 * index);
                addedMembers.Add(lastSentTime, CreateMember(++index, null, lastSentTime, true, true, joined.AddMinutes(index)));

                // Sent around the period.

                lastSentTime = DateTime.Now.AddDays(-1 * period - 1).AddMinutes(-1 * index);
                addedMembers.Add(lastSentTime, CreateMember(++index, null, lastSentTime, true, true, joined.AddMinutes(index)));
            }

            var enumerable = maxMembers == -1 ? addedMembers.Select(p => p.Value) : addedMembers.Select(p => p.Value).Take(maxMembers);
            foreach (var member in enumerable)
                expectedMembers.Add(member);
        }

        private void AddAlreadySentWithSetting(ref int index, ICollection<Member> expectedMembers, int count, int maxMembers)
        {
            const int period = 30;
            var joined = DateTime.Now.AddDays(-3 * period);
            var lastSent = DateTime.Now.AddDays(-2 * period);

            // These are the members that should be returned, ie:
            // - they are active
            // - they have already received a status update email outside of the period
            // - they have explicitly set their frequency
            // - they have not turned off update emails

            var addedMembers = new SortedList<DateTime, Member>();
            for (var i = 0; i < count; ++i)
            {
                var lastSentTime = lastSent.AddMinutes(-1 * index);
                addedMembers.Add(lastSentTime, CreateMember(++index, Frequency.Monthly, lastSentTime, true, true, joined.AddMinutes(index)));

                // Sent around the period.

                lastSentTime = DateTime.Now.AddDays(-1 * period - 1).AddMinutes(-1 * index);
                addedMembers.Add(lastSentTime, CreateMember(++index, Frequency.Monthly, lastSentTime, true, true, joined.AddMinutes(index)));
            }

            var enumerable = maxMembers == -1 ? addedMembers.Select(p => p.Value) : addedMembers.Select(p => p.Value).Take(maxMembers);
            foreach (var member in enumerable)
                expectedMembers.Add(member);
        }

        private Member CreateMember(int index, Frequency? frequency, DateTime? lastSent, bool enabled, bool activated, DateTime joined)
        {
            var member = _memberAccountsCommand.CreateTestMember(string.Format(EmailAddressFormat, index));
            member.IsEnabled = enabled;
            member.IsActivated = activated;
            member.CreatedTime = joined;
            _memberAccountsCommand.UpdateMember(member);

            var definition = _settingsQuery.GetDefinition(typeof(MemberNewsletterEmail).Name);
            if (frequency != null)
                _settingsCommand.SetFrequency(member.Id, definition.CategoryId, frequency.Value);
            if (lastSent != null)
                _settingsCommand.SetLastSentTime(member.Id, definition.Id, lastSent.Value);

            return member;
        }

        private void AssertEmails(IList<Member> expectedMembers)
        {
            // Need to sort.

            expectedMembers = expectedMembers.OrderBy(m => m.GetBestEmailAddress().Address).ToList();

            var emails = _emailServer.AssertEmailsSent(expectedMembers.Count).OrderBy(e => e.To[0].Address).ToList();
            for (var emailIndex = 0; emailIndex < emails.Count; ++emailIndex)
            {
                emails[emailIndex].AssertAddresses(Return, Return, expectedMembers[emailIndex]);
                emails[emailIndex].AssertHtmlViewContains(expectedMembers[emailIndex].FirstName);
            }
        }
    }
}