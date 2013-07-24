using System;
using System.Collections.Generic;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Query.Reports.DailyReports;
using LinkMe.Query.Reports.DailyReports.Queries;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using LinkMe.Query.Reports.Roles.Communications;
using LinkMe.Query.Reports.Roles.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class StatsEmailTests
        : EmailTests
    {
        private const string Subject = "LinkMe Daily Stats";

        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IResumeReportsCommand _resumeReportsCommand = Resolve<IResumeReportsCommand>();
        private readonly IResumeReportsQuery _resumeReportsQuery = Resolve<IResumeReportsQuery>();
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IDailyReportsQuery _dailyReportsQuery = Resolve<IDailyReportsQuery>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create users.

            const int interval = -1;

            CreateMember(1, false);
            CreateMember(2, false);
            CreateMember(3, false, DateTime.Now.AddDays(interval - 1));

            CreateEmployer(4);
            CreateEmployer(5);
            CreateEmployer(6, DateTime.Now.AddDays(interval - 1));

            CreateRecruiter(7);
            CreateRecruiter(8);
            CreateRecruiter(9, DateTime.Now.AddDays(interval - 1));

            // Send.

            return new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create users.

            const int interval = -1;

            CreateMember(1, false);
            CreateMember(2, false);
            CreateMember(3, false, DateTime.Now.AddDays(interval - 1));

            CreateEmployer(4);
            CreateEmployer(5);
            CreateEmployer(6, DateTime.Now.AddDays(interval - 1));

            CreateRecruiter(7);
            CreateRecruiter(8);
            CreateRecruiter(9, DateTime.Now.AddDays(interval - 1));

            // Send.

            var dailyReport = _dailyReportsQuery.GetDailyReport(DayRange.Yesterday);
            var templateEmail = new StatsEmail(dailyReport);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, AllStaff);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, GetContent(dailyReport)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestResumeCount()
        {
            // Create users.

            CreateMember(1, true);
            var member2 = CreateMember(2, true);
            var member3 = CreateMember(3, true);
            CreateMember(4, false);

            // Send.

            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Yesterday)));

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">New resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Uploaded resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Reloaded resumes:</td>\r\n            <td style=\"text-align:left;\">0</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Edited resumes:</td>\r\n            <td style=\"text-align:left;\">0</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Updated resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");

            // Delete the UploadResume event for member2, but add an EditResume event.

            var events = _resumeReportsQuery.GetResumeEvents(member2.Id);
            _resumeReportsCommand.DeleteResumeEvent(events[0].Id);

            var candidate = _candidatesCommand.GetCandidate(member2.Id);
            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = member2.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = events[0].Time });
            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Yesterday)));
            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">New resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Uploaded resumes:</td>\r\n            <td style=\"text-align:left;\">2</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Reloaded resumes:</td>\r\n            <td style=\"text-align:left;\">0</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Edited resumes:</td>\r\n            <td style=\"text-align:left;\">1</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Updated resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");

            // Delete the UploadResume event for member2, but add an ReloadResume event.

            events = _resumeReportsQuery.GetResumeEvents(member3.Id);

            candidate = _candidatesCommand.GetCandidate(member2.Id);
            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = member3.Id, ResumeId = candidate.ResumeId.Value, Time = events[0].Time });
            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Yesterday)));
            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">New resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Uploaded resumes:</td>\r\n            <td style=\"text-align:left;\">2</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Reloaded resumes:</td>\r\n            <td style=\"text-align:left;\">1</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Edited resumes:</td>\r\n            <td style=\"text-align:left;\">1</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Updated resumes:</td>\r\n            <td style=\"text-align:left;\">3</td>");
        }

        [TestMethod]
        public void TestInvitationCount()
        {
            var startDay = DateTime.Today;

            // Create users.

            var member1 = CreateMember(1, true);
            var member2 = CreateMember(2, true);
            var member3 = CreateMember(3, true);
            var member4 = CreateMember(4, true);

            // Send invitation.

            var invitation = new FriendInvitation { InviterId = member1.Id, InviteeId = member2.Id, Text = "invite", DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);
            _networkingInvitationsCommand.SendInvitation(invitation);

            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Today)));

            // Check. First email is invitation, second is stats.

            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations sent:</td>\r\n            <td style=\"text-align:left;\">1</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations accepted:</td>\r\n            <td style=\"text-align:left;\">0</td>");

            // Accept the invitation.

            _networkingInvitationsCommand.AcceptInvitation(member2.Id, invitation);

            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Today)));

            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations sent:</td>\r\n            <td style=\"text-align:left;\">1</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations accepted:</td>\r\n            <td style=\"text-align:left;\">1</td>");

            // Send invitation to a non-member.

            invitation = new FriendInvitation { InviterId = member2.Id, InviteeEmailAddress = "test@test.linkme.net.au", Text = "invite", DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);
            _networkingInvitationsCommand.SendInvitation(invitation);

            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Today)));

            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations sent:</td>\r\n            <td style=\"text-align:left;\">2</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations accepted:</td>\r\n            <td style=\"text-align:left;\">1</td>");

            // Send and accept invitation but mark it as being sent outside the time period.

            invitation = new FriendInvitation { InviterId = member3.Id, InviteeId = member4.Id, Text = "invite", DonationRequestId = null };
            _networkingInvitationsCommand.CreateInvitation(invitation);
            _networkingInvitationsCommand.SendInvitation(invitation);

            invitation.FirstSentTime = startDay.AddDays(-7);
            invitation.LastSentTime = startDay.AddDays(-7);
            _networkingInvitationsCommand.AcceptInvitation(member4.Id, invitation);

            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(DayRange.Today)));

            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations sent:</td>\r\n            <td style=\"text-align:left;\">2</td>");
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">Invitations accepted:</td>\r\n            <td style=\"text-align:left;\">2</td>");
        }

        [TestMethod]
        public void TestCommunications()
        {
            var stats = _dailyReportsQuery.GetDailyReport(DayRange.Yesterday);

            // Add some templateEmail stats.

            stats.CommunciationReports = new Dictionary<string, CommunicationReport>
            {
                { "FriendInvitationEmail", new CommunicationReport {Sent = 12, Opened = 4, LinksClicked = 8} }
            };
            var templateEmail = new StatsEmail(stats);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, AllStaff);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            //email.AssertHtmlView(GetBody(templateEmail, GetContent()));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestOnlineSales()
        {
            var stats = _dailyReportsQuery.GetDailyReport(DayRange.Yesterday);

            stats.OrderReports = new List<OrderReport>
                                     {
                                         new OrderReport
                                             {
                                                 ClientName = "Test Client",
                                                 OrganisationName = "Test Organisation",
                                                 Products = new[] {"5 Contacts", "10 Applicants"},
                                                 Price = 400
                                             }
                                     };

            var templateEmail = new StatsEmail(stats);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, AllStaff);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();

            email.AssertHtmlViewContains("<td>Test Client</td>");
            email.AssertHtmlViewContains("<td>Test Organisation</td>");
            email.AssertHtmlViewContains("<td>5 Contacts, 10 Applicants</td>");
            email.AssertHtmlViewContains("<td>$400.00</td>");

            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private void CreateEmployer(int index, DateTime createdTime)
        {
            var employer = CreateEmployer(index);
            employer.CreatedTime = createdTime;
            _employerAccountsCommand.UpdateEmployer(employer);
        }

        private Employer CreateRecruiter(int index)
        {
            return _employerAccountsCommand.CreateTestRecruiter(index, _organisationsCommand.CreateTestOrganisation(0));
        }

        private void CreateRecruiter(int index, DateTime createdTime)
        {
            var employer = CreateRecruiter(index);
            employer.CreatedTime = createdTime;
            _employerAccountsCommand.UpdateEmployer(employer);
        }

        private Member CreateMember(int index, bool withResume)
        {
            Member member;
            if (withResume)
            {
                member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesCommand.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate); 
                _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = member.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1)});
            }
            else
            {
                member = _memberAccountsCommand.CreateTestMember(index);
            }

            return member;
        }

        private void CreateMember(int index, bool withResume, DateTime createdTime)
        {
            var member = CreateMember(index, withResume);
            member.CreatedTime = createdTime;
            _memberAccountsCommand.UpdateMember(member);
        }

        private static string GetContent(DailyReport dailyReport)
        {
            var sb = new StringBuilder();

            // Need to be explicit here instead of calling GetBodyStart because of the extra styling.

            sb.AppendLine();
            sb.AppendLine("        <h2>Statistics for " + DateTime.Today.AddDays(-1).ToShortDateString() + "</h2>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Members</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Total members:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.MemberReport.Total + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Enabled members:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.MemberReport.Enabled + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Active members:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.MemberReport.Active + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">New members:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.MemberReport.New + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Resumes</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Total resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.Total + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Searchable resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.Searchable + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">New resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.New + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Updated resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.Updated + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Uploaded resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.Uploaded + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Reloaded resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.Reloaded + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Edited resumes:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">" + dailyReport.ResumeReport.Edited + "</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Candidate searches</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;width:200px;\"></td>");
            sb.AppendLine("            <td style=\"text-align:right;width:80px;\"><strong>Web</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;width:80px;\"><strong>API</strong></td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>Total searches:</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>0</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>0</strong></td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">New searches:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Filter searches:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Saved searches:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Non-anonymous searches:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Anonymous searches:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Candidate viewings</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;width:200px;\"></td>");
            sb.AppendLine("            <td style=\"text-align:right;width:80px;\"><strong>Web</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;width:80px;\"><strong>API</strong></td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>Total viewings:</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>0</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>0</strong></td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Distinct viewings:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Non-anonymous viewings:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Anonymous viewings:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Candidate accesses</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;width:200px;\"></td>");
            sb.AppendLine("            <td style=\"text-align:right;width:80px;\"><strong>Web</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;width:80px;\"><strong>API</strong></td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>Total accesses:</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>0</strong></td>");
            sb.AppendLine("            <td style=\"text-align:right;\"><strong>0</strong></td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Distinct accesses:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Messages sent:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Phone numbers viewed:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Resumes downloaded:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Resumes sent:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Unlockings:</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("            <td style=\"text-align:right;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Online sales</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Client</td>");
            sb.AppendLine("            <td>Company</td>");
            sb.AppendLine("            <td>Product</td>");
            sb.AppendLine("            <td>Price</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Integration</h3>");
            sb.AppendLine();
            sb.AppendLine("        <h4>Export Feed</h4>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Integrator</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Events</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Successes</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Job Ads</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h4>Export Feed Id</h4>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Integrator</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Events</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Successes</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Job Ads</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h4>Export Post</h4>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Integrator</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Events</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Successes</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Job Ads</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Failed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Posted</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Updated</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h4>Export Close</h4>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Integrator</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Events</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Successes</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Job Ads</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Failed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Closed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Not Found</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h4>Import Post</h4>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Integrator</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Events</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Successes</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Job Ads</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Failed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Posted</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Closed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Updated</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Duplicates</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Ignored</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h4>Import Close</h4>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Integrator</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Events</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Successes</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Job Ads</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Failed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Closed</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Not Found</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Other</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Returning users today:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0 (0%) (0 Members, 0 Employers,");
            sb.AppendLine("            0 Administrators, 0 Community Administrators)</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Returning users week to date:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0 (0%) (0 Members, 0 Employers,");
            sb.AppendLine("            0 Administrators, 0 Community Administrators)</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Returning users month to date:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0 (0%) (0 Members, 0 Employers,");
            sb.AppendLine("            0 Administrators, 0 Community Administrators)</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Total candidate search alerts:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Total job search alerts:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Job searches:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Total open job ads:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Job applications:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0 on LinkMe, 0 redirects to external form</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Invitations sent:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Invitations accepted:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td style=\"text-align:right;\">Invite acceptances over previous 48 hours:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0 %</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("          <tr>");
            sb.AppendLine("            <td style=\"text-align:right;\">Invite acceptances last month:</td>");
            sb.AppendLine("            <td style=\"text-align:left;\">0 %</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Communications breakdown</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Communication</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Sent</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Opened</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">Links Clicked</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("        <h3>Promotion code breakdown</h3>");
            sb.AppendLine();
            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");
            sb.AppendLine("          <tr style=\"background-color: #E5E5E5;\">");
            sb.AppendLine("            <td>Promotion code</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">New members</td>");
            sb.AppendLine("            <td style=\"text-align:center;\">New resumes</td>");
            sb.AppendLine("          </tr>");
            sb.AppendLine();
            sb.AppendLine("        </table>");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}