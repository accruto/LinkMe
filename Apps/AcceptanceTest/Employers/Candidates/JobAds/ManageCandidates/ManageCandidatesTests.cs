using System;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Applicants.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds.ManageCandidates
{
    [TestClass]
    public abstract class ManageCandidatesTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        protected readonly IJobAdApplicantsCommand _jobAdApplicantsCommand = Resolve<IJobAdApplicantsCommand>();
        protected readonly IMemberApplicationsQuery _memberApplicationsQuery = Resolve<IMemberApplicationsQuery>();
        private ReadOnlyUrl _baseManageCandidatesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _baseManageCandidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates/manage/");
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected Member CreateNewCandidate(JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            return member;
        }

        protected Member CreateShortlistedCandidate(IEmployer employer, JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member.Id });
            return member;
        }

        protected Member CreateRejectedCandidate(IEmployer employer, JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            return member;
        }

        protected Member CreateRemovedCandidate(IEmployer employer, JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member.Id });
            return member;
        }

        protected ReadOnlyUrl GetManageCandidatesUrl(Guid jobAdId, ApplicantStatus? status)
        {
            var url = new ApplicationUrl(_baseManageCandidatesUrl, jobAdId.ToString());
            if (status != null)
                url.QueryString["status"] = status.ToString();
            return url;
        }

        protected void AssertNewCandidates(Action<HtmlNode, Member> assert, Guid jobAdId, params Member[] members)
        {
            AssertCandidates(assert, jobAdId, ApplicantStatus.New, members);
        }

        protected void AssertNewCandidates(Guid jobAdId, params Member[] members)
        {
            AssertCandidates(null, jobAdId, ApplicantStatus.New, members);
        }

        protected void AssertShortlistedCandidates(Action<HtmlNode, Member> assert, Guid jobAdId, params Member[] members)
        {
            AssertCandidates(assert, jobAdId, ApplicantStatus.Shortlisted, members);
        }

        protected void AssertShortlistedCandidates(Guid jobAdId, params Member[] members)
        {
            AssertCandidates(null, jobAdId, ApplicantStatus.Shortlisted, members);
        }

        protected void AssertRejectedCandidates(Action<HtmlNode, Member> assert, Guid jobAdId, params Member[] members)
        {
            AssertCandidates(assert, jobAdId, ApplicantStatus.Rejected, members);
        }

        protected void AssertRejectedCandidates(Guid jobAdId, params Member[] members)
        {
            AssertCandidates(null, jobAdId, ApplicantStatus.Rejected, members);
        }

        private void AssertCandidates(Action<HtmlNode, Member> assert, Guid jobAdId, ApplicantStatus status, params Member[] members)
        {
            Get(GetManageCandidatesUrl(jobAdId, status));
            AssertCandidates(assert, members);
        }

        protected void AssertCandidates(Action<HtmlNode, Member> assert, params Member[] members)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='candidate_search-result search-result js_selectable-item draggable result-container']");
            Assert.AreEqual(members.Length, nodes == null ? 0 : nodes.Count);

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var member = (from m in members where m.Id == new Guid(node.Attributes["data-memberid"].Value) select m).SingleOrDefault();
                    Assert.IsNotNull(member);
                    if (assert != null)
                        assert(node, member);
                }
            }
        }
    }
}
