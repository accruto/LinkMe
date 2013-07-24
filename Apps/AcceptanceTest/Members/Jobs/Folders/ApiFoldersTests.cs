using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Folders
{
    [TestClass]
    public class ApiFoldersTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        private ReadOnlyUrl _baseFoldersUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _baseFoldersUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/folders/api/");
        }

        [TestMethod]
        public void TestAddJobAdsToPrivateFolder()
        {
            TestAddJobAdsToFolder(GetPrivateFolder, AddJobAds);
        }

        [TestMethod]
        public void TestAddJobAdsToMobileFolder()
        {
            TestAddJobAdsToFolder(GetMobileFolder, AddMobileJobAds);
        }

        [TestMethod]
        public void TestRemoveJobAdsFromPrivateFolder()
        {
            TestRemoveJobAdsFromFolder(GetPrivateFolder, RemoveJobAds);
        }

        [TestMethod]
        public void TestRemoveJobAdsFromMobileFolder()
        {
            TestRemoveJobAdsFromFolder(GetMobileFolder, RemoveMobileJobAds);
        }

        [TestMethod]
        public void TestRemoveAllJobAdsFromPrivateFolder()
        {
            TestRemoveAllJobAdsFromFolder(GetPrivateFolder, RemoveAllJobAds);
        }

        [TestMethod]
        public void TestCannotAddToOtherPrivateFolder()
        {
            TestCannotAddJobAdsToOtherFolder(GetPrivateFolder, AddJobAds);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherPrivateFolder()
        {
            TestCannotRemoveJobAdsFromOtherFolder(GetPrivateFolder, RemoveJobAds);
        }

        [TestMethod]
        public void TestCannotRemoveAllFromOtherPrivateFolder()
        {
            TestCannotRemoveAllJobAdsFromOtherFolder(GetPrivateFolder, RemoveAllJobAds);
        }

        private void TestAddJobAdsToFolder(Func<IMember, JobAdFolder> getFolder, Func<JobAdFolder, JobAd[], JsonListCountModel> addJobAds)
        {
            const int count = 3;
            var jobAds = new JobAd[count];
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create member and folder.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var folder = getFolder(member);

            // Log in and add JobAds.

            LogIn(member);
            var model = addJobAds(folder, jobAds);

            // Assert.

            AssertModel(3, model);
            AssertJobAds(member, folder.Id, jobAds);

            // Add again.

            model = addJobAds(folder, jobAds);

            // Assert.

            AssertModel(3, model);
            AssertJobAds(member, folder.Id, jobAds);
        }

        private void TestCannotAddJobAdsToOtherFolder(Func<IMember, JobAdFolder> getFolder, Func<HttpStatusCode, JobAdFolder, JobAd[], JsonListCountModel> addJobAds)
        {
            const int count = 3;
            var jobAds = new JobAd[count];
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create member and folder.

            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var folder = getFolder(member2);

            // Log in and add JobAds.

            LogIn(member1);
            var model = addJobAds(HttpStatusCode.NotFound, folder, jobAds);

            // Assert.

            AssertJsonError(model, null, "400", "The folder cannot be found.");
            AssertJobAds(member1, folder.Id);
            AssertJobAds(member2, folder.Id);
        }

        private void TestRemoveJobAdsFromFolder(Func<IMember, JobAdFolder> getFolder, Func<JobAdFolder, JobAd[], JsonListCountModel> removeJobAds)
        {
            const int count = 3;
            var jobAds = new JobAd[count];
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create employer and folder.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var folder = getFolder(member);
            _memberJobAdListsCommand.AddJobAdsToFolder(member, folder, from j in jobAds select j.Id);

            // Log in and remove JobAds.

            LogIn(member);
            var model = removeJobAds(folder, new[] { jobAds[0], jobAds[2] });

            // Assert.

            AssertModel(1, model);
            AssertJobAds(member, folder.Id, jobAds[1]);

            // Remove again.

            model = removeJobAds(folder, new[] { jobAds[0], jobAds[2] });

            // Assert.

            AssertModel(1, model);
            AssertJobAds(member, folder.Id, jobAds[1]);
        }

        private void TestCannotRemoveJobAdsFromOtherFolder(Func<IMember, JobAdFolder> getFolder, Func<HttpStatusCode, JobAdFolder, JobAd[], JsonListCountModel> removeJobAds)
        {
            const int count = 3;
            var jobAds = new JobAd[count];
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create employer and folder.

            var member1 = _memberAccountsCommand.CreateTestMember(0);
            var member2 = _memberAccountsCommand.CreateTestMember(1);
            var folder = getFolder(member2);
            _memberJobAdListsCommand.AddJobAdsToFolder(member2, folder, from j in jobAds select j.Id);

            // Log in and remove JobAds.

            LogIn(member1);
            var model = removeJobAds(HttpStatusCode.NotFound, folder, new[] { jobAds[0], jobAds[2] });

            // Assert.

            AssertJsonError(model, null, "400", "The folder cannot be found.");
            AssertJobAds(member2, folder.Id, jobAds);
        }

        private void TestRemoveAllJobAdsFromFolder(Func<IMember, JobAdFolder> getFolder, Func<JobAdFolder, JsonListCountModel> removeAllJobAds)
        {
            const int count = 3;
            var jobAds = new JobAd[count];
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create employer and folder.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var folder = getFolder(member);
            _memberJobAdListsCommand.AddJobAdsToFolder(member, folder, from j in jobAds select j.Id);

            // Log in and remove JobAds.

            LogIn(member);
            var model = removeAllJobAds(folder);

            // Assert.

            AssertModel(0, model);
            AssertJobAds(member, folder.Id);

            // Remove again.

            model = removeAllJobAds(folder);

            // Assert.

            AssertModel(0, model);
            AssertJobAds(member, folder.Id);
        }

        private void TestCannotRemoveAllJobAdsFromOtherFolder(Func<IMember, JobAdFolder> getFolder, Func<HttpStatusCode, JobAdFolder, JsonListCountModel> removeAllJobAds)
        {
            const int count = 3;
            var jobAds = new JobAd[count];
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create employer and folder.

            var member1 = _memberAccountsCommand.CreateTestMember(0);
            var member2 = _memberAccountsCommand.CreateTestMember(1);
            var folder = getFolder(member2);
            _memberJobAdListsCommand.AddJobAdsToFolder(member2, folder, from j in jobAds select j.Id);

            // Log in and remove JobAds.

            LogIn(member1);
            var model = removeAllJobAds(HttpStatusCode.NotFound, folder);

            // Assert.

            AssertJsonError(model, null, "400", "The folder cannot be found.");
            AssertJobAds(member2, folder.Id, jobAds);
        }

        private JobAdFolder GetPrivateFolder(IMember member)
        {
            return (from f in _jobAdFoldersQuery.GetFolders(member) where f.FolderType == FolderType.Private select f).First();
        }

        private JobAdFolder GetMobileFolder(IMember member)
        {
            return _jobAdFoldersQuery.GetMobileFolder(member);
        }

        protected void AssertModel(int expectedCount, JsonCountModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedCount, model.Count);
        }

        protected void AssertJobAds(IMember member, Guid folderId, params JobAd[] expectedJobAds)
        {
            var jobAdIds = _jobAdFoldersQuery.GetInFolderJobAdIds(member, folderId);
            Assert.AreEqual(expectedJobAds.Length, jobAdIds.Count);
            foreach (var expectedJobAd in expectedJobAds)
            {
                var expectedJobAdId = expectedJobAd.Id;
                Assert.AreEqual(true, (from e in jobAdIds where e == expectedJobAdId select e).Any());
            }
        }

        private JsonListCountModel AddJobAds(JobAdFolder folder, params JobAd[] jobAds)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id + "/addjobs");
            var response = Post(url, GetParameters(jobAds));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel AddMobileJobAds(JobAdFolder folder, params JobAd[] jobAds)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, "mobile/addjobs");
            var response = Post(url, GetParameters(jobAds));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel AddJobAds(HttpStatusCode expectedStatusCode, JobAdFolder folder, params JobAd[] jobAds)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id + "/addjobs");
            var response = Post(expectedStatusCode, url, GetParameters(jobAds));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveJobAds(JobAdFolder folder, params JobAd[] jobAds)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id + "/removejobs");
            var response = Post(url, GetParameters(jobAds));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveMobileJobAds(JobAdFolder folder, params JobAd[] jobAds)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, "mobile/removejobs");
            var response = Post(url, GetParameters(jobAds));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveJobAds(HttpStatusCode expectedStatusCode, JobAdFolder folder, params JobAd[] jobAds)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id + "/removejobs");
            var response = Post(expectedStatusCode, url, GetParameters(jobAds));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveAllJobAds(JobAdFolder folder)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id + "/removealljobs");
            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveAllJobAds(HttpStatusCode expectedStatusCode, JobAdFolder folder)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id + "/removealljobs");
            var response = Post(expectedStatusCode, url);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private static NameValueCollection GetParameters(IEnumerable<JobAd> jobAds)
        {
            var parameters = new NameValueCollection();
            if (jobAds != null)
            {
                foreach (var jobAd in jobAds)
                    parameters.Add("jobAdId", jobAd.Id.ToString());
            }
            return parameters;
        }
    }
}
