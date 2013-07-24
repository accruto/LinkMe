using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Donations.Commands;
using LinkMe.Domain.Donations.Queries;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberToMemberNotifications
{
    [TestClass]
    public class ContactInvitationEmailTests
        : EmailTests
    {
        private const string InviterEmailAddress = "inviter@test.linkme.net.au";
        private const string InviterFirstName = "Barney";
        private const string InviterFirstNameWithAnS = "Ross";
        private const string InviterLastName = "Gumble";
        private const string FriendFirstName = "Lenny";
        private const string FriendLastName = "Leonard";
        private const string To = "invitee@test.linkme.net.au";
        private const decimal DonationAmount = 3;
        private const string MessageText = "Do it now";
        private const string RequiresEncodingMessageText = @"Do & it
now";

        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IDonationsQuery _donationsQuery = Resolve<IDonationsQuery>();
        private readonly IDonationsCommand _donationsCommand = Resolve<IDonationsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);

            // Create donation.

            DonationRequest request;
            DonationRecipient recipient;
            CreateDonationRequest(out request, out recipient);

            // Create the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = MessageText };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            return new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 0);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            /*            Group group = new Group();
            group.Name = "Working from home";
            group.Description = "I can swear, throw things around, drink beer!";

            Member sender = _memberAccountsCommand.CreateTestMember("a@aaaa.aa", "", "Sender", "Surname");
            Member recipient = _memberAccountsCommand.CreateTestMember("b@bbbbb.bb", "", "Recepient", "Surname");
            TestObjectMother.SaveUser(recipient);

            GroupJoinInvitation invitation = new GroupJoinInvitation(group, sender.Contributor, recipient.Contributor, MessageText);

            // No settings so notification should get through.

            EmailsCommand.TrySend(new GroupFriendInvitationEmail(invitation, true));
            SmtpServer.AssertEmailSent();

            // Turn off.

            var category = SettingsCommand.GetCategory("MemberGroupNotification");
            SettingsCommand.SetFrequency(recipient.Id, category.Id, CommunicationFrequency.Never);
            EmailsCommand.TrySend(new GroupFriendInvitationEmail(invitation, true));
            SmtpServer.AssertNoEmailSent();

            // Turn on.

            SettingsCommand.SetFrequency(recipient.Id, category.Id, CommunicationFrequency.Immediately);
            EmailsCommand.TrySend(new GroupFriendInvitationEmail(invitation, true));
            SmtpServer.AssertEmailSent();*/
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);

            // Create donation.

            DonationRequest request;
            DonationRecipient recipient;
            CreateDonationRequest(out request, out recipient);

            // Create the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = MessageText, DonationRequestId = request == null ? (Guid?) null : request.Id };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, request, recipient, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = To } } }, GetContent(templateEmail, inviter, invitation, request, recipient)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsNoMessageText()
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);

            // Create donation.

            DonationRequest request;
            DonationRecipient recipient;
            CreateDonationRequest(out request, out recipient);

            // Create the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = null, DonationRequestId = request == null ? (Guid?)null : request.Id };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, request, recipient, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = To } } }, GetContent(templateEmail, inviter, invitation, request, recipient)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsRequiresEncodingMessageText()
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);

            // Create donation.

            DonationRequest request;
            DonationRecipient recipient;
            CreateDonationRequest(out request, out recipient);

            // Create the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = RequiresEncodingMessageText, DonationRequestId = request == null ? (Guid?)null : request.Id };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, request, recipient, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = To } } }, GetContent(templateEmail, inviter, invitation, request, recipient)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsNoDonation()
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);

            // Create the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = To } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsNoMessageTextNoDonation()
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);

            // Create the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = null, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = To } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsPossessiveName()
        {
            // Create the invitation.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstNameWithAnS, InviterLastName);

            // Create the invitation.

            DonationRequest request;
            DonationRecipient recipient;
            CreateDonationRequest(out request, out recipient);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = MessageText, DonationRequestId = request == null ? (Guid?)null : request.Id };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, request, recipient, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = To } } }, GetContent(templateEmail, inviter, invitation, request, recipient)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailProfileDetails()
        {
            int memberIndex = 0;

            // Create the inviter.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All;
            _memberAccountsCommand.UpdateMember(inviter);

            // All access, no properties set.

            memberIndex++;

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = memberIndex + To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // All access, 1 friend.

            memberIndex++;
            Member friend = _memberAccountsCommand.CreateTestMember(memberIndex + To, FriendFirstName, FriendLastName);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, friend.Id);

            memberIndex++;
            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = memberIndex + To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 1);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // All access, 2 friends.

            memberIndex++;
            friend = _memberAccountsCommand.CreateTestMember(memberIndex + To, FriendFirstName, FriendLastName);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, friend.Id);

            memberIndex++;
            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = memberIndex + To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 2);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // No access, 2 friends.

            inviter.VisibilitySettings.Personal.FirstDegreeVisibility &= ~PersonalVisibility.FriendsList;
            _memberAccountsCommand.UpdateMember(inviter);

            memberIndex++;
            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = memberIndex + To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // All access, current jobs set.

            inviter.VisibilitySettings.Personal.FirstDegreeVisibility |= PersonalVisibility.FriendsList;
            _memberAccountsCommand.UpdateMember(inviter);
            var candidate = _candidatesCommand.GetCandidate(inviter.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            memberIndex++;
            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = memberIndex + To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 2);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // No access, current jobs set.

            inviter.VisibilitySettings.Personal.FirstDegreeVisibility &= ~PersonalVisibility.CurrentJobs;
            _memberAccountsCommand.UpdateMember(inviter);

            memberIndex++;
            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = memberIndex + To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, null, 2);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailMultipleCurrentJobs()
        {
            // Create the inviter.

            Member inviter = _memberAccountsCommand.CreateTestMember(InviterEmailAddress, InviterFirstName, InviterLastName);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All;
            IList<Job> jobs = new List<Job>
                                  {
                                      new Job
                                          {
                                              Dates = new PartialDateRange(new PartialDate(1997, 1)),
                                              Title = "Developer",
                                              Description = "Developer description",
                                              Company = "Developer employer"
                                          },
                                      new Job
                                          {
                                              Dates = new PartialDateRange(new PartialDate(2005, 1)),
                                              Title = "Tester",
                                              Description = "Tester description",
                                              Company = "Tester employer"
                                          },
                                  };

            var candidate = _candidatesCommand.GetCandidate(inviter.Id);
            _candidateResumesCommand.CreateResume(candidate, new Resume { Jobs = jobs });

            // Send the invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = To, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new ContactInvitationEmail(inviter, invitation, null, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = invitation.InviteeEmailAddress } } }, GetContent(templateEmail, inviter, invitation, null, null)));
            email.AssertHtmlViewContains("Tester");
            email.AssertHtmlViewDoesNotContain("Developer");
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "Personal network invitation";
        }

        private string GetContent(TemplateEmail templateEmail, Member inviter, Request invitation, DonationRequest donationRequest, DonationRecipient donationRecipient)
        {
            var friendCount = Resolve<IMemberContactsQuery>().GetFirstDegreeContacts(inviter.Id).Count;

            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + inviter.FullName + " has sent you a request to join their personal network.");
            sb.AppendLine("</p>");

            // Show details if set.

            var currentJobs = GetCurrentJobs(inviter.Id);
            var view = new PersonalView(inviter, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);
            bool currentJobsVisible = view.CanAccess(PersonalVisibility.CurrentJobs)
                && !currentJobs.IsNullOrEmpty()
                && currentJobs.Count > 0;
            bool contactCountVisible = view.CanAccess(PersonalVisibility.FriendsList)
                && friendCount > 0;

            sb.AppendLine();
            if (currentJobsVisible || contactCountVisible)
            {
                sb.AppendLine("<div style=\"padding-left: 30px; font-family: Arial, Helvetica, sans-serif;\">");
                sb.AppendLine("  <p>");
                sb.AppendLine("    " + inviter.FullName);
                sb.AppendLine();
            }

            if (currentJobsVisible)
            {
                sb.AppendLine("    <br />");

                var currentJob = (from j in currentJobs
                                  where !string.IsNullOrEmpty(j.Title)
                                  && j.Dates.Start != null
                                  orderby j.Dates.Start.Value descending
                                  select j.Title).FirstOrDefault();

                sb.AppendLine("    " + currentJob);
                sb.AppendLine();
            }

            if (contactCountVisible)
            {
                sb.AppendLine();
                sb.AppendLine("    <br />");
                sb.AppendLine("    already has " + friendCount + " friend" + (friendCount > 1 ? "s" : ""));
                sb.AppendLine("    in their network who may be able to help you in your career.");
            }

            if (currentJobsVisible || contactCountVisible)
            {
                sb.AppendLine();
                sb.AppendLine("  </p>");
                sb.AppendLine("</div>");
                sb.AppendLine();
            }

            sb.AppendLine("<p>");
            sb.AppendLine("  A profile on our network will help position your career for success.");
            sb.AppendLine("  1000s of employers are searching for talent like you.");
            sb.AppendLine("</p>");

            sb.AppendLine();
            if (donationRequest != null)
            {
                sb.AppendLine("<p>We'll also donate " + donationRequest.Amount.ToString("C") + " to the " + donationRecipient.Name + " when you join.</p>");
                sb.AppendLine();
            }

            sb.AppendLine();
            if (!string.IsNullOrEmpty(invitation.Text))
            {
                sb.AppendLine("<p>");
                sb.AppendLine("  " + inviter.FirstName.MakeNamePossessive() + " personal message:");
                sb.AppendLine("  <br />");
                sb.AppendLine("  " + HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(invitation.Text)));
                sb.AppendLine("</p>");
                sb.AppendLine();
            }

            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, false, "~/Join.aspx", "inviteId", invitation.Id.ToString("N")) + "\">Join " + inviter.FirstName.MakeNamePossessive() + " network</a>");
            sb.AppendLine("</p>");

            return sb.ToString();
        }

        private void CreateDonationRequest(out DonationRequest donationRequest, out DonationRecipient donationRecipient)
        {
            var recipients = _donationsQuery.GetRecipients();
            if (recipients.Count > 0)
            {
                donationRecipient = _donationsQuery.GetRecipients()[0];
                donationRequest = new DonationRequest {Amount = DonationAmount, RecipientId = donationRecipient.Id};
                _donationsCommand.CreateRequest(donationRequest);
            }
            else
            {
                donationRequest = null;
                donationRecipient = null;
            }
        }

        private IList<Job> GetCurrentJobs(Guid inviterId)
        {
            var candidate = _candidatesCommand.GetCandidate(inviterId);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return resume == null ? null : resume.CurrentJobs;
        }
    }
}