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
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
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
    public class FriendInvitationEmailTests
        : EmailTests
    {
        private const string MessageText = "Do it now";
        private const string RequiresEncodingMessageText = @"Do & it
now";

        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1, community != null ? community.Id : (Guid?)null);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            return new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            // No settings so notification should get through.

            _emailsCommand.TrySend(new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("MemberToMemberNotification");
            _settingsCommand.SetFrequency(invitee.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(invitee.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailDeactivatedMember()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            invitee.IsActivated = false;
            _memberAccountsCommand.UpdateMember(invitee);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var emailVerification = new EmailVerification { UserId = invitee.Id, EmailAddress = invitee.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, emailVerification, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, emailVerification);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsNullMessageText()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = null, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsEmptyMessageText()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = string.Empty, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsRequiresEncodingMessageText()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = RequiresEncodingMessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailProfileDetails()
        {
            var index = 0;

            // Create the inviter.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All;
            _memberAccountsCommand.UpdateMember(inviter);

            // All access, no properties set.

            index++;
            var invitee = _memberAccountsCommand.CreateTestMember(index);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // All access, 1 friend.

            index++;
            var friend = _memberAccountsCommand.CreateTestMember(index);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, friend.Id);

            index++;
            invitee = _memberAccountsCommand.CreateTestMember(index);

            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 1);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // All access, 2 friends.

            index++;
            friend = _memberAccountsCommand.CreateTestMember(index);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, friend.Id);

            index++;
            invitee = _memberAccountsCommand.CreateTestMember(index);

            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 2);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // No access, 2 friends.

            inviter.VisibilitySettings.Personal.FirstDegreeVisibility &= ~PersonalVisibility.FriendsList;
            _memberAccountsCommand.UpdateMember(inviter);

            index++;
            invitee = _memberAccountsCommand.CreateTestMember(index);

            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);
            
            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // All access, current jobs set.

            inviter.VisibilitySettings.Personal.FirstDegreeVisibility |= PersonalVisibility.FriendsList;
            _memberAccountsCommand.UpdateMember(inviter);

            var candidate = _candidatesCommand.GetCandidate(inviter.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            index++;
            invitee = _memberAccountsCommand.CreateTestMember(index);

            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 2);
            _emailsCommand.TrySend(templateEmail);
            
            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);

            // No access, current jobs set.

            inviter.VisibilitySettings.Personal.FirstDegreeVisibility &= ~PersonalVisibility.CurrentJobs;
            _memberAccountsCommand.UpdateMember(inviter);

            index++;
            invitee = _memberAccountsCommand.CreateTestMember(index);

            invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, null, 2);
            _emailsCommand.TrySend(templateEmail);
            
            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailMultipleCurrentJobs()
        {
            // Create the inviter.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All;
            _memberAccountsCommand.UpdateMember(inviter);

            var resume = new Resume
            {
                Jobs = new List<Job>
                {
                    new Job
                    {
                        Dates = new PartialDateRange(new PartialDate(1997, 1)),
                        Title = "Developer",
                        Description = "Developer description",
                        Company= "Developer employer",
                    },
                    new Job
                    {
                        Dates = new PartialDateRange(new PartialDate(2005, 1)),
                        Title = "Tester",
                        Description = "Tester description",
                        Company = "Tester employer",
                    }
                }
            };
            var candidate = _candidatesCommand.GetCandidate(inviter.Id);
            _candidateResumesCommand.CreateResume(candidate, resume);

            // Send the invitation.

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText, DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new FriendInvitationEmail(invitee, inviter, invitation, null, GetCurrentJobs(inviter.Id), 0);
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertHtmlViewContains("Tester");
            email.AssertHtmlViewDoesNotContain("Developer");
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "Personal network invitation";
        }

        private void AssertHtmlView(MockEmail email, TemplateEmail templateEmail, Member inviter, RegisteredUser invitee, Request invitation, EmailVerification emailVerification)
        {
            var friendCount = Resolve<IMemberContactsQuery>().GetFirstDegreeContacts(inviter.Id).Count;
            email.AssertHtmlView(GetBody(templateEmail, invitee, GetContent(templateEmail, inviter, invitee, friendCount, invitation, emailVerification)));
        }

        private string GetContent(TemplateEmail templateEmail, Member inviter, IRegisteredUser invitee, int friendCount, Request invitation, EmailVerification emailVerification)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + invitee.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + inviter.FullName + " has sent you a request to join their personal network.");
            sb.AppendLine("</p>");

            // Show details if set.

            var currentJobs = GetCurrentJobs(inviter.Id);
            var view = new PersonalView(inviter, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);
            var currentJobsVisible = view.CanAccess(PersonalVisibility.CurrentJobs)
                && !currentJobs.IsNullOrEmpty()
                    && currentJobs.Count > 0;
            var contactCountVisible = view.CanAccess(PersonalVisibility.FriendsList)
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

            // Show individual text if set.

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

            // Link depends on whether the invitee is activated.

            sb.AppendLine("<p>");
            sb.AppendLine();
            if (emailVerification == null)
            {
                sb.AppendLine("  <a class=\"major-action\" href=\"" + GetTinyUrl(templateEmail, true, "~/members/friends/Invitations.aspx") + "\">Respond to " + inviter.FirstName.MakeNamePossessive() + " personal invitation</a>");
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("  <a class=\"major-action\" href=\"" + GetTinyUrl(templateEmail, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode, "isInvite", "true") + "\">Reactivate");
                sb.AppendLine("  your account and respond to " + inviter.FirstName.MakeNamePossessive() + " friend request</a>");
                sb.AppendLine();
            }

            sb.AppendLine("</p>");
            sb.AppendLine("<p><strong>Did you know that 60% of jobs are found through people you ");
            sb.AppendLine("know?</strong></p>");

            return sb.ToString();
        }

        private IList<Job> GetCurrentJobs(Guid inviterId)
        {
            var candidate = _candidatesCommand.GetCandidate(inviterId);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return resume == null ? null : resume.CurrentJobs;
        }
    }
}