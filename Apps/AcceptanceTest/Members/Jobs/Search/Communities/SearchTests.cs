using System.Collections.Generic;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Communities
{
    public abstract class SearchTests
        : CommunityTests
    {
        protected const string Analyst = "Analyst";

        [TestMethod]
        public void TestNonCommunityJobNonCommunityMember()
        {
            TestJobMember(false, false, false);
        }

        [TestMethod]
        public void TestNonCommunityJobCommunityMember()
        {
            TestJobMember(false, true, false);
        }

        [TestMethod]
        public void TestCommunityJobNonCommunityMember()
        {
            TestJobMember(true, false, false);
        }

        [TestMethod, Ignore]
        public void TestCommunityJobCommunityMember()
        {
            TestJobMember(true, true, false);
        }

        [TestMethod, Ignore]
        public void TestNonCommunityJobNonCommunityMemberOnlyCommunityJobs()
        {
            TestJobMember(false, false, true);
        }

        [TestMethod, Ignore]
        public void TestNonCommunityJobCommunityMemberOnlyCommunityJobs()
        {
            TestJobMember(false, true, true);
        }

        [TestMethod]
        public void TestCommunityJobNonCommunityMemberOnlyCommunityJobs()
        {
            TestJobMember(true, false, true);
        }

        [TestMethod, Ignore]
        public void TestCommunityJobCommunityMemberOnlyCommunityJobs()
        {
            TestJobMember(true, true, true);
        }

        [TestMethod]
        public void TestCommunityJobOtherCommunityMember()
        {
            var community1 = CreateCommunity(1);
            var community2 = CreateCommunity(2);

            // Create the job ad.

            var employer = CreateEmployer(0, community1);
            PostJobAd(Analyst, 0, employer);

            // Create the member and search.

            var member = CreateMember(0, community2);
            if (member != null)
                LogIn(member);
            Search(Analyst, member != null ? false : (bool?)null);

            // Should not be able to see the job ad.

            AssertJobAds();
        }

        [TestMethod]
        public void TestJobsNonCommunityMember()
        {
            TestJobsMember(false);
        }

        [TestMethod, Ignore]
        public void TestJobsCommunityMember()
        {
            TestJobsMember(true);
        }

        protected abstract void Search(string jobTitle, bool? onlyCommunityJobAds);

        private void TestJobMember(bool isCommunityJobAd, bool isCommunityMember, bool onlyCommunityJobAds)
        {
            var community = CreateCommunity(1);

            // Create the job ad.

            var employer = CreateEmployer(0, isCommunityJobAd ? community : null);
            var jobAd = PostJobAd(Analyst, 0, employer);

            // Create the member and search.

            var member = CreateMember(0, isCommunityMember ? community : null);
            if (member != null)
                LogIn(member);
            Search(Analyst, isCommunityMember && member != null ? onlyCommunityJobAds : (bool?)null);

            if (!isCommunityJobAd)
            {
                // If logged in as a community member and checked community only on then cannot see the job ad.

                if (isCommunityMember && member != null && onlyCommunityJobAds)
                    AssertJobAds();
                else
                    AssertJobAds(jobAd, false);
            }
            else
            {
                // It is a community job ad, so must be a community member and logged in.

                if (isCommunityMember && member != null)
                    AssertJobAds(jobAd, true);
                else
                    AssertJobAds();
            }
        }

        private void TestJobsMember(bool isCommunityMember)
        {
            var community = CreateCommunity(1);

            // Create the job ads, one for the community, one without.

            var employer1 = CreateEmployer(1, community);
            var jobAd1 = PostJobAd(Analyst, 1, employer1);

            var employer2 = CreateEmployer(2, null);
            var jobAd2 = PostJobAd(Analyst, 2, employer2);

            // Create the member and search.

            var member = CreateMember(0, isCommunityMember ? community : null);
            if (member != null)
                LogIn(member);
            Search(Analyst, isCommunityMember && member != null ? false : (bool?)null);

            // A community member logged in can see both.

            if (isCommunityMember && member != null)
                AssertJobAds(new[] { jobAd1, jobAd2 }, new[] { true, false });
            else
                AssertJobAds(jobAd2, false);
        }

        private void AssertJobAds()
        {
            AssertJobAds(new JobAd[0], new bool[0]);
        }

        private void AssertJobAds(JobAd jobAd, bool isFeatured)
        {
            AssertJobAds(new[] { jobAd }, new[] { isFeatured });
        }

        private void AssertJobAds(IList<JobAd> jobAds, IList<bool> isFeatureds)
        {
            var jobads = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='row']/div");
            var titles = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='row']//div[@class='column title']/a");
            Assert.AreEqual(jobads == null ? 0 : jobAds.Count, titles == null ? 0 : titles.Count);

            // Order is important.

            if (jobads != null)
            {
                for (var index = 0; index < jobAds.Count; ++index)
                {
                    Assert.AreEqual(isFeatureds[index], jobads[index].Attributes["class"].Value.Contains("featured_jobad"));
                    Assert.AreEqual(jobAds[index].Title, titles[index].InnerText);
                }
            }
        }
    }
}