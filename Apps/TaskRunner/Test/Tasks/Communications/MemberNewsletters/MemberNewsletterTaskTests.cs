using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberUpdates;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Members.Communications.Queries;
using LinkMe.Domain.Users.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.MemberNewsletters
{
    [TestClass]
    public abstract class MemberNewsletterTaskTests
        : TaskTests
    {
        private const string EmailAddressFormat = "test{0}@test.linkme.net.au";

        protected readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        protected readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        protected readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        protected readonly IMemberCommunicationsQuery _memberCommunicationsQuery = Resolve<IMemberCommunicationsQuery>();
        protected readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();

        protected void AssertEmails(IList<Member> expectedMembers)
        {
            var emails = _emailServer.AssertEmailsSent(expectedMembers.Count);
            for (var emailIndex = 0; emailIndex < emails.Length; ++emailIndex)
            {
                emails[emailIndex].AssertAddresses(Return, Return, expectedMembers[emailIndex]);
                emails[emailIndex].AssertHtmlViewContains(expectedMembers[emailIndex].FirstName);
            }
        }

        protected Member CreateMember(int index, Frequency? frequency, DateTime? lastSent, bool enabled, bool activated, DateTime joined)
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
    }
}