using System;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI.Registered.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds
{
    [TestClass]
    public class ViewJobAdsTests
        : JobAdsTests
    {
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();

        [TestMethod]
        public void TestJobAdsNoApplicants()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            LogIn(employer);
            AssertJobAds(jobAd.Id, 0, 0, 0);
        }

        [TestMethod]
        public void TestJob1Applicant()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = _memberAccountsCommand.CreateTestMember(0);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            LogIn(employer);
            AssertJobAds(jobAd.Id, 0, 1, 0);
            AssertJobAd(jobAd.Id, 0, 1, 0);
        }

        [TestMethod]
        public void TestJob3Applicants()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            for (var index = 0; index < 3; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                var application = new InternalApplication { ApplicantId = member.Id };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            }

            LogIn(employer);
            AssertJobAds(jobAd.Id, 0, 3, 0);
            AssertJobAd(jobAd.Id, 0, 3, 0);
        }

        [TestMethod]
        public void TestCandidateAdded()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Copy the candidate.

            var member = _memberAccountsCommand.CreateTestMember(0);

            LogIn(employer);
            AssertModel(ShortlistCandidates(jobAd.Id, member), 1, 0, 0);

            AssertJobAds(jobAd.Id, 1, 0, 0);
            AssertJobAd(jobAd.Id, 1, 0, 0);
        }

        [TestMethod]
        public void TestCandidateRemoved()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Copy the candidate.

            var member = _memberAccountsCommand.CreateTestMember(0);

            LogIn(employer);
            AssertModel(ShortlistCandidates(jobAd.Id, member), 1, 0, 0);

            AssertJobAds(jobAd.Id, 1, 0, 0);
            AssertJobAd(jobAd.Id, 1, 0, 0);

            // Reject.

            AssertModel(RejectCandidates(jobAd.Id, member), 0, 0, 1);
            AssertJobAds(jobAd.Id, 0, 0, 1);
            AssertJobAd(jobAd.Id, 0, 0, 1);

            // Remove.

            AssertModel(RemoveCandidates(jobAd.Id, member), 0, 0, 0);
            AssertJobAds(jobAd.Id, 0, 0, 0);
            AssertJobAd(jobAd.Id, 0, 0, 0);
        }

        [TestMethod]
        public void Test3CandidatesAdded()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Copy the candidate.

            LogIn(employer);
            for (var index = 0; index < 3; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                AssertModel(ShortlistCandidates(jobAd.Id, member), index + 1, 0, 0);
            }

            AssertJobAds(jobAd.Id, 3, 0, 0);
            AssertJobAd(jobAd.Id, 3, 0, 0);
        }

        [TestMethod]
        public void Test3CandidatesRemoved()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Shortlist the candidate.

            LogIn(employer);
            var members = new Member[3];
            for (var index = 0; index < 3; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                AssertModel(ShortlistCandidates(jobAd.Id, members[index]), index + 1, 0, 0);
            }

            AssertJobAds(jobAd.Id, 3, 0, 0);
            AssertJobAd(jobAd.Id, 3, 0, 0);

            // Reject.

            for (var index = 0; index < 3; ++index)
            {
                AssertModel(RejectCandidates(jobAd.Id, members[2 - index]), 2 - index, 0, index + 1);
                AssertJobAds(jobAd.Id, 2 - index, 0, index + 1);
                AssertJobAd(jobAd.Id, 2 - index, 0, index + 1);
            }

            // Remove.

            for (var index = 0; index < 3; ++index)
            {
                AssertModel(RemoveCandidates(jobAd.Id, members[2 - index]), 0, 0, 2 - index);
                AssertJobAds(jobAd.Id, 0, 0, 2 - index);
                AssertJobAd(jobAd.Id, 0, 0, 2 - index);
            }
        }

        [TestMethod]
        public void TestRejectApplicant()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var members = new Member[3];
            for (var index = 0; index < 3; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                var application = new InternalApplication { ApplicantId = members[index].Id };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            }

            LogIn(employer);
            AssertJobAds(jobAd.Id, 0, 3, 0);
            AssertJobAd(jobAd.Id, 0, 3, 0);

            // Reject the first applicant.

            Get(GetManageCandidatesUrl(jobAd.Id));
            AssertModel(RejectCandidates(jobAd.Id, members[0]), 0, 2, 1);
            AssertJobAds(jobAd.Id, 0, 2, 1);
            AssertJobAd(jobAd.Id, 0, 2, 1);
        }

        [TestMethod]
        public void TestShortlistApplicant()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var members = new Member[3];
            for (var index = 0; index < 3; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                var application = new InternalApplication { ApplicantId = members[index].Id };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            }

            LogIn(employer);
            AssertJobAds(jobAd.Id, 0, 3, 0);
            AssertJobAd(jobAd.Id, 0, 3, 0);

            // Shortlist the first applicant.

            Get(GetManageCandidatesUrl(jobAd.Id));
            AssertModel(ShortlistCandidates(jobAd.Id, members[0]), 1, 2, 0);
            AssertJobAds(jobAd.Id, 1, 2, 0);
            AssertJobAd(jobAd.Id, 1, 2, 0);
        }

        [TestMethod]
        public void TestMix()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Submit some applications.

            var members = new Member[12];

            for (var index = 0; index < 5; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                var application = new InternalApplication { ApplicantId = members[index].Id };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            }

            LogIn(employer);
            AssertJobAds(jobAd.Id, 0, 5, 0);
            AssertJobAd(jobAd.Id, 0, 5, 0);

            // Add some others.

            for (var index = 5; index < 12; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                AssertModel(ShortlistCandidates(jobAd.Id, members[index]), index - 5 + 1, 5, 0);
            }

            AssertJobAds(jobAd.Id, 7, 5, 0);
            AssertJobAd(jobAd.Id, 7, 5, 0);

            // Reject some.

            for (var index = 0; index < 3; ++index)
            {
                Get(GetManageCandidatesUrl(jobAd.Id));
                AssertModel(RejectCandidates(jobAd.Id, members[index]), 7, 5 - index - 1, index + 1);
            }

            AssertJobAds(jobAd.Id, 7, 2, 3);
            AssertJobAd(jobAd.Id, 7, 2, 3);

            // Shortlist others.

            for (var index = 0; index < 2; ++index)
            {
                Get(GetManageCandidatesUrl(jobAd.Id));
                AssertModel(ShortlistCandidates(jobAd.Id, members[index + 3]), 7 + index + 1, 2 - index - 1, 3);
            }

            AssertJobAds(jobAd.Id, 9, 0, 3);
            AssertJobAd(jobAd.Id, 9, 0, 3);
        }

        private void AssertJobAd(Guid jobAdId, int shortlistedApplicants, int newApplicants, int rejectedApplicants)
        {
            Get(GetManageCandidatesUrl(jobAdId));

            AssertShortlistedApplicants(shortlistedApplicants);
            AssertNewApplicants(newApplicants);
            AssertRejectedApplicants(rejectedApplicants);
        }

        private void AssertRejectedApplicants(int applicants)
        {
            AssertPageContains(string.Format("<span>Rejected (<span class=\"count rejected-candidates-count\">{0}</span>)</span>", applicants));
        }

        private void AssertNewApplicants(int applicants)
        {
            AssertPageContains(string.Format("<span>New (<span class=\"count new-candidates-count\">{0}</span>)</span>", applicants));
        }

        private void AssertShortlistedApplicants(int applicants)
        {
            AssertPageContains(string.Format("<span>Shortlisted (<span class=\"count shortlisted-candidates-count\">{0}</span>)</span>", applicants));
        }

        private void AssertJobAds(Guid jobAdId, int shortlistedApplicants, int newApplicants, int rejectedApplicants)
        {
            GetPage<EmployerJobAds>("mode", "Open");
            AssertPage<EmployerJobAds>();

            // Look for the applciants text.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='repeater-details-container']/table/tr[position()=4]");
            Assert.IsNotNull(node);
            var label = node.SelectSingleNode("td[position()=1]/label");
            Assert.IsNotNull(label);
            Assert.AreEqual("Applicants", label.InnerText);
            var td = node.SelectSingleNode("td[position()=2]");
            Assert.AreEqual(GetApplicantsText(jobAdId, shortlistedApplicants, newApplicants, rejectedApplicants).ToLower(), GetPageApplicantsText(td.InnerHtml).ToLower());
        }

        private static string GetApplicantsText(Guid jobAdId, int shortlistedApplicants, int newApplicants, int rejectedApplicants)
        {
            return "<strong>" + shortlistedApplicants + "</strong>" + GetShortlistedApplicantsText(jobAdId, shortlistedApplicants) + " -"
                + "<strong>" + newApplicants + "</strong>" + GetNewApplicantsText(jobAdId, newApplicants) + " -"
                + "<strong>" + rejectedApplicants + "</strong>" + GetRejectedApplicantsText(jobAdId, rejectedApplicants);
        }

        private static string GetNewApplicantsText(Guid? jobAdId, int applicants)
        {
            return GetApplicantsText(jobAdId, applicants, ApplicantStatus.New);
        }

        private static string GetRejectedApplicantsText(Guid? jobAdId, int applicants)
        {
            return GetApplicantsText(jobAdId, applicants, ApplicantStatus.Rejected);
        }

        private static string GetShortlistedApplicantsText(Guid? jobAdId, int applicants)
        {
            return GetApplicantsText(jobAdId, applicants, ApplicantStatus.Shortlisted);
        }

        private static string GetApplicantsText(Guid? jobAdId, int applicants, ApplicantStatus status)
        {
            if (applicants == 0)
                return status.ToString();
            var url = new ApplicationUrl("~/employers/candidates/manage/" + jobAdId, new ReadOnlyQueryString("status", status.ToString()));
            return "<a href=\"" + url.PathAndQuery + "\">" + status + "</a>";
        }

        private static string GetPageApplicantsText(string innerText)
        {
            innerText = innerText.Trim();
            var sb = new StringBuilder();

            while (innerText.Length > 0)
            {
                // Element.

                var pos = GetElementLength(innerText);
                if (pos == -1)
                    return innerText;

                var element = innerText.Substring(0, pos + 1);
                sb.Append(element);
                innerText = innerText.Substring(pos + 1);

                // Text.

                pos = GetTextLength(innerText);
                var text = innerText.Substring(0, pos);
                sb.Append(text.Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("  ", ""));
                innerText = innerText.Substring(pos);
            }

            return sb.ToString();
        }

        private static int GetTextLength(string xml)
        {
            var pos = xml.IndexOf("<strong");
            return pos == -1 ? xml.Length : pos;
        }

        private static int GetElementLength(string xml)
        {
            var pos = xml.IndexOf("<strong");
            if (pos == -1)
                return -1;

            pos = xml.IndexOf(">", pos);
            if (pos == -1)
                return -1;

            pos = xml.IndexOf("</strong", pos);
            if (pos == -1)
                return -1;

            pos = xml.IndexOf(">", pos);
            if (pos == -1)
                return -1;

            return pos;
        }
    }
}