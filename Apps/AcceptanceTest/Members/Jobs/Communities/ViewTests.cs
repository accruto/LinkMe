using System;
using System.Net;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Communities
{
    public abstract class ViewTests
        : CommunityTests
    {
        [TestMethod]
        public void TestNonCommunityJobNonCommunityMember()
        {
            TestJobMember(false, false);
        }

        [TestMethod]
        public void TestNonCommunityJobCommunityMember()
        {
            TestJobMember(false, true);
        }

        [TestMethod]
        public void TestCommunityJobNonCommunityMember()
        {
            TestJobMember(true, false);
        }

        [TestMethod]
        public void TestCommunityJobCommunityMember()
        {
            TestJobMember(true, true);
        }

        [TestMethod]
        public void TestCommunityJobOtherCommunityMember()
        {
            var community1 = CreateCommunity(1);
            var community2 = CreateCommunity(2);

            // Create the job ad.

            var employer = CreateEmployer(0, community1);
            var jobAd = PostJobAd(Analyst, 0, employer);

            // Create the member and search.

            var member = CreateMember(0, community2);
            if (member != null)
                LogIn(member);
            View(jobAd, true, false, member);
        }

        private void TestJobMember(bool isCommunityJobAd, bool isCommunityMember)
        {
            var community = CreateCommunity(1);

            // Create the job ad.

            var employer = CreateEmployer(0, isCommunityJobAd ? community : null);
            var jobAd = PostJobAd(Analyst, 0, employer);

            // Create the member and search.

            var member = CreateMember(0, isCommunityMember ? community : null);
            if (member != null)
                LogIn(member);
            View(jobAd, isCommunityJobAd, isCommunityMember, member);
        }

        private void View(JobAd jobAd, bool isCommunityJobAd, bool isCommunityMember, Member member)
        {
            var url = new ReadOnlyApplicationUrl(true, "~/jobs/" + jobAd.GetJobRelativePath());

            if (isCommunityJobAd)
            {
                // It is a community job ad, so must be a community member and logged in.

                if (isCommunityMember && member != null)
                {
                    Get(url);
                    AssertUrl(url);
                    AssertJobAd(jobAd);
                }
                else
                {
                    if (member == null)
                    {
                        // Needs to log in.

                        var loginUrl = LogInUrl.AsNonReadOnly();
                        loginUrl.QueryString.Add("returnUrl", url.Path);
                        Get(url);
                        AssertUrl(loginUrl);
                    }
                    else
                    {
                        Get(HttpStatusCode.NotFound, url);
                    }
                }
            }
            else
            {
                // If not a community job then can always be seen.

                Get(url);
                AssertUrl(url);
                AssertJobAd(jobAd);
            }
        }

        private void AssertJobAd(JobAdEntry jobAd)
        {
            AssertPageContains(jobAd.Title);
        }
    }
}