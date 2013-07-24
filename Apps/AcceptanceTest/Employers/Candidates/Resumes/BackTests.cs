using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.Applicants.Commands;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Resumes
{
    [TestClass]
    public class BackTests
        : ResumesTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand = Resolve<IEmployerJobAdsCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IJobAdApplicantsCommand _jobAdApplicantsCommand = Resolve<IJobAdApplicantsCommand>();
        private ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _resultsUrl;
        private ReadOnlyUrl _baseCandidateUrl;
        private ReadOnlyUrl _baseFoldersUrl;
        private ReadOnlyUrl _baseBlockListsUrl;
        private ReadOnlyUrl _baseSuggestedCandidatesUrl;
        private ReadOnlyUrl _baseManageCandidatesUrl;
        private HtmlTextBoxTester _keywordsTextBox;
        private HtmlButtonTester _searchButton;
        private const string Title = "The is the title.";
        private const string Content = "The is the content.";

        [TestInitialize]
        public void TestInitialize()
        {
            ClearSearchIndexes();

            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            _resultsUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates/results");
            _baseCandidateUrl = new ReadOnlyApplicationUrl(true, "~/candidates/");
            _baseFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/");
            _baseBlockListsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blocklists/");
            _baseSuggestedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/suggested/");
            _baseManageCandidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates/manage/");

            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestBackToFolder()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            var members = CreateCandidates(40, employer, folder);

            // Go to the folder.

            LogIn(employer);
            var url = GetFolderUrl(folder.Id, null, null);
            Get(url);

            var candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to folder"));
            Assert.AreEqual(url.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());

            // Change the paging.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString.Add("Page", 2.ToString(CultureInfo.InvariantCulture));
            newUrl.QueryString.Add("Items", 10.ToString(CultureInfo.InvariantCulture));
            Get(newUrl);

            candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to folder"));
            Assert.AreEqual(newUrl.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());
        }

        [TestMethod]
        public void TestBackToBlockList()
        {
            var employer = CreateEmployer();
            var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            var members = CreateCandidates(40, employer, blockList);

            // Go to the folder.

            LogIn(employer);
            var url = GetBlockListUrl(blockList.BlockListType, null, null);
            Get(url);

            var candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to block list"));
            Assert.AreEqual(url.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());

            // Change the paging.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString.Add("Page", 2.ToString(CultureInfo.InvariantCulture));
            newUrl.QueryString.Add("Items", 10.ToString(CultureInfo.InvariantCulture));
            Get(newUrl);

            candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to block list"));
            Assert.AreEqual(newUrl.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());
        }

        [TestMethod]
        public void TestBackToSearch()
        {
            var employer = CreateEmployer();
            var members = CreateCandidates(40);

            // Go to the folder.

            LogIn(employer);
            Get(_searchUrl);
            _keywordsTextBox.Text = "business analyst";
            _searchButton.Click();

            var candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to search results"));
            Assert.AreEqual(_resultsUrl.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());
        }

        [TestMethod]
        public void TestBackToSuggestedCandidates()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var members = CreateCandidates(40, jobAd);

            // Go to suggested candidates.

            LogIn(employer);
            var url = GetSuggestedCandidatesUrl(jobAd.Id, null, null);
            Get(url);

            var candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to suggested candidates"));
            Assert.AreEqual(url.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());

            // Change the paging.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString.Add("Page", 2.ToString(CultureInfo.InvariantCulture));
            newUrl.QueryString.Add("Items", 10.ToString(CultureInfo.InvariantCulture));
            Get(newUrl);

            candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to suggested candidates"));
            Assert.AreEqual(newUrl.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());
        }

        [TestMethod]
        public void TestBackToNewCandidates()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var members = CreateNewCandidates(40, jobAd);

            // Go to managed candidates.

            LogIn(employer);
            var url = GetManageCandidatesUrl(jobAd.Id, ApplicantStatus.New, null, null);
            Get(url);

            var candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to manage candidates"));
            Assert.AreEqual(url.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());

            // Change the paging.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString.Add("Page", 2.ToString(CultureInfo.InvariantCulture));
            newUrl.QueryString.Add("Items", 10.ToString(CultureInfo.InvariantCulture));
            Get(newUrl);

            candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to manage candidates"));
            Assert.AreEqual(newUrl.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());
        }

        [TestMethod]
        public void TestBackToShortlistedCandidates()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var members = CreateShortlistedCandidates(40, employer, jobAd);

            // Go to managed candidates.

            LogIn(employer);
            var url = GetManageCandidatesUrl(jobAd.Id, ApplicantStatus.Shortlisted, null, null);
            Get(url);

            var candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to manage candidates"));
            Assert.AreEqual(url.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());

            // Change the paging.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString.Add("Page", 2.ToString(CultureInfo.InvariantCulture));
            newUrl.QueryString.Add("Items", 10.ToString(CultureInfo.InvariantCulture));
            Get(newUrl);

            candidateId = GetFirstCandidateId();
            Get(GetFirstCandidateUrl());
            AssertPath(GetCandidateUrl(members, candidateId));

            Get(GetBackUrl("Back to manage candidates"));
            Assert.AreEqual(newUrl.PathAndQuery.ToLower(), Browser.CurrentUrl.PathAndQuery.ToLower());
        }

        private JobAd CreateJobAd(IEmployer employer)
        {
            var jobAd = new JobAd
            {
                Title = Title,
                Description =
                {
                    Content = Content,
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address
                },
            };

            _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            return jobAd;
        }

        private ReadOnlyUrl GetBackUrl(string expectedText)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='back-action']");
            Assert.AreEqual(expectedText, node.InnerText);
            return new ReadOnlyApplicationUrl(HttpUtility.HtmlDecode(node.Attributes["href"].Value));
        }

        private Guid GetFirstCandidateId()
        {
            return new Guid(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='candidate_search-result search-result js_selectable-item draggable result-container']").Attributes["data-memberid"].Value);
        }

        private ReadOnlyUrl GetFirstCandidateUrl()
        {
            return new ReadOnlyApplicationUrl(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='candidate-name']/a").Attributes["href"].Value);
        }

        private IList<Member> CreateCandidates(int count, IEmployer employer, CandidateFolder folder)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
                _memberSearchService.UpdateMember(member.Id);
                members.Add(member);
            }

            return members;
        }

        private IList<Member> CreateCandidates(int count, IEmployer employer, CandidateBlockList blockList)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member.Id);
                _memberSearchService.UpdateMember(member.Id);
                members.Add(member);
            }

            return members;
        }

        private IList<Member> CreateCandidates(int count, JobAdEntry jobAd)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
                resume.Jobs[0].Title = jobAd.Title;
                _candidateResumesCommand.UpdateResume(candidate, resume);
                _memberSearchService.UpdateMember(member.Id);
                members.Add(member);
            }

            return members;
        }

        private IList<Member> CreateNewCandidates(int count, JobAdEntry jobAd)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                var application = new InternalApplication { ApplicantId = member.Id };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
                members.Add(member);
            }

            return members;
        }

        private IList<Member> CreateShortlistedCandidates(int count, IEmployer employer, JobAdEntry jobAd)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member.Id });
                members.Add(member);
            }

            return members;
        }

        private IEnumerable<Member> CreateCandidates(int count)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                _memberSearchService.UpdateMember(member.Id);
                members.Add(member);
            }

            return members;
        }

        private ReadOnlyUrl GetCandidateUrl(IEnumerable<Member> members, Guid memberId)
        {
            var member = (from m in members where m.Id == memberId select m).Single();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            var sb = new StringBuilder();

            // Location.

            if (!string.IsNullOrEmpty(member.Address.Location.ToString()))
                sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(member.Address.Location.ToString())).ToLower().Replace(' ', '-'));
            else
                sb.Append(member.Address.Location.IsCountry ? member.Address.Location.Country.ToString().ToLower() : "-");

            // Salary.

            sb.Append("/");
            if (candidate.DesiredSalary == null || !member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Salary))
            {
                sb.Append("-");
            }
            else
            {
                var desiredSalary = candidate.DesiredSalary.ToRate(SalaryRate.Year);
                if (desiredSalary.LowerBound != null)
                {
                    if (desiredSalary.UpperBound != null)
                        sb.Append((int)(desiredSalary.LowerBound) / 1000).Append("k-").Append((int)(desiredSalary.UpperBound) / 1000).Append("k");
                    else
                        sb.Append((int)(desiredSalary.LowerBound) / 1000).Append("k-and-above");
                }
                else
                {
                    sb.Append("up-to-").Append((int)(desiredSalary.UpperBound) / 1000).Append("k");
                }
            }

            // Job title.

            sb.Append("/");
            sb.Append(string.IsNullOrEmpty(candidate.DesiredJobTitle)
                ? "-"
                : TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(candidate.DesiredJobTitle)).ToLower().Replace(' ', '-'));

            // Id

            sb.Append("/");
            sb.Append(member.Id.ToString());

            return new ReadOnlyApplicationUrl(_baseCandidateUrl, sb.ToString());
        }

        private ReadOnlyUrl GetFolderUrl(Guid folderId, int? page, int? items)
        {
            if (page == null && items == null)
                return new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId.ToString());

            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            if (items != null)
                queryString.Add("items", items.Value.ToString(CultureInfo.InvariantCulture));
            return new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId.ToString(), queryString);
        }

        private ReadOnlyUrl GetBlockListUrl(BlockListType blockListType, int? page, int? items)
        {
            if (page == null && items == null)
                return new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListType.ToString());

            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            if (items != null)
                queryString.Add("items", items.Value.ToString(CultureInfo.InvariantCulture));
            return new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListType.ToString(), queryString);
        }

        private ReadOnlyUrl GetSuggestedCandidatesUrl(Guid jobAdId, int? page, int? items)
        {
            if (page == null && items == null)
                return new ReadOnlyApplicationUrl(_baseSuggestedCandidatesUrl, jobAdId.ToString());

            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            if (items != null)
                queryString.Add("items", items.Value.ToString(CultureInfo.InvariantCulture));
            return new ReadOnlyApplicationUrl(_baseSuggestedCandidatesUrl, jobAdId.ToString(), queryString);
        }

        private ReadOnlyUrl GetManageCandidatesUrl(Guid jobAdId, ApplicantStatus status, int? page, int? items)
        {
            var queryString = new QueryString();
            queryString["status"] = status.ToString();
            if (page != null)
                queryString.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            if (items != null)
                queryString.Add("items", items.Value.ToString(CultureInfo.InvariantCulture));

            return new ReadOnlyApplicationUrl(_baseManageCandidatesUrl, jobAdId.ToString(), queryString);
        }
    }
}
