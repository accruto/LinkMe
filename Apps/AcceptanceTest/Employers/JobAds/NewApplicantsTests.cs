using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds
{
    [TestClass]
    public class NewApplicantsTests
        : JobAdsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private ReadOnlyUrl _manageCandidatesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _manageCandidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates/manage/");
        }

        [TestMethod]
        public void TestApplicant()
        {
            // Create the job ad.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateMember();

            // Apply for the job.

            ApiLogIn(member);
            AssertJsonSuccess(Apply(jobAd.Id));
            LogOut();

            // Log in as employer.

            LogIn(employer);
            Get(new ReadOnlyApplicationUrl(_manageCandidatesUrl, jobAd.Id.ToString()));
            AssertPageContains("<span>New (<span class=\"count new-candidates-count\">1</span>)</span>");

            AssertPageContains(member.FullName);

            // Follow the candidate link.

            Get(GetCandidateUrl());
            AssertPageContains(member.FullName);
        }

        private Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        private JsonResponseModel Apply(Guid jobAdId)
        {
            return Deserialize<JsonResponseModel>(Post(GetApiApplyWithProfileUrl(jobAdId)));
        }

        private static ReadOnlyUrl GetApiApplyWithProfileUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/applywithprofile");
        }

        private ReadOnlyUrl GetCandidateUrl()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='basic-details_holder']/div[@class='basic-details js_toggler']/div[@class='basic-details-sublayout']/div[@class='candidate-name']//a");
            Assert.IsNotNull(node);
            return new ReadOnlyApplicationUrl(node.Attributes["href"].Value);
        }
    }
}