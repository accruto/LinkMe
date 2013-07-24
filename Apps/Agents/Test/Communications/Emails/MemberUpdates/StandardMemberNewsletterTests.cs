using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberUpdates;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberUpdates
{
    [TestClass]
    public class StandardMemberNewsletterTests
        : MemberNewsletterTests
    {
        private const string Email = "homer@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            return null;
        }

        [TestMethod]
        public void TestFrequency()
        {
            var definition = _settingsQuery.GetDefinition("MemberNewsletterEmail");
            var category = _settingsQuery.GetCategory("MemberUpdate");

            // Create a member.

            var member = CreateMember();

            // No settings so notification should get through.

            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            // Turn off.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            // Set to monthly.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Monthly);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-40));
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-20));
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            // Set to weekly.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Weekly);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-3));
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            // Set to daily.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Daily);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-1));
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(0));
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestContents()
        {
            // Create a member.

            var member = CreateMember();

            // Send the email.

            var communication = new MemberNewsletterEmail(member);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
            AssertLinks(email.GetHtmlView().Body, null);
        }

        [TestMethod]
        public void TestWorkStatus()
        {
            // Create the member.

            var member = CreateMember();

            // Send the email.

            var communication = new MemberNewsletterEmail(member);
            _emailsCommand.TrySend(communication);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
            AssertLinks(email.GetHtmlView().Body, null);
        }

        [TestMethod]
        public void TestResumeCreateReminder()
        {
            var member = CreateMember(false);
            
            // Send the email.

            var communication = new MemberNewsletterEmail(member);
            _emailsCommand.TrySend(communication);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
            AssertLinks(email.GetHtmlView().Body, null);
        }

        [TestMethod]
        public void TestResumeUpdateReminder()
        {
            var member = CreateMember(true);

            // Send the email.

            var communication = new MemberNewsletterEmail(member);
            _emailsCommand.TrySend(communication);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
            AssertLinks(email.GetHtmlView().Body, null);
        }

        [TestMethod]
        public void TestErrors()
        {
            // Create a member but do not save them.

            var member = new Member
                             {
                                 CreatedTime = DateTime.Now,
                                 IsActivated = true,
                                 IsEnabled = true,
                                 FirstName = FirstName,
                                 LastName = LastName,
                                 EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = Email } },
                             };

            member.CreatedTime = DateTime.Now.AddDays(-20);

            // No email should be sent.

            var communication = new MemberNewsletterEmail(member);
            try
            {
                _emailsCommand.TrySend(communication);
            }
            catch (Exception)
            {
                // An exception should be thrown because the member is not known.
            }

            _emailServer.AssertNoEmailSent();
        }

        private Member CreateMember(bool hasResume)
        {
            var member = CreateMember();
            member.CreatedTime = DateTime.Now.AddDays(-20);
            _memberAccountsCommand.UpdateMember(member);

            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.Status = CandidateStatus.ActivelyLooking;
            _candidatesCommand.UpdateCandidate(candidate);

            if (hasResume)
                _candidateResumesCommand.CreateResume(candidate, new Resume { LastUpdatedTime = DateTime.Now.AddDays(-10) });

            return member;
        }
    }
}