using System;
using System.Threading;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Search;
using LinkMe.Query.Search.Engine.JobAds.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.MultipleEngines
{
    [TestClass]
    public class MultipleEnginesTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        private const string Title = "Archeologist";

        private HtmlTextBoxTester _titleTextBox;
        private HtmlTextBoxTester _positionTitleTextBox;
        private HtmlTextBoxTester _bulletPoint1TextBox;
        private HtmlTextBoxTester _bulletPoint2TextBox;
        private HtmlTextBoxTester _bulletPoint3TextBox;
        private HtmlTextAreaTester _summaryTextBox;
        private HtmlTextAreaTester _contentTextBox;
        private HtmlTextBoxTester _companyNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlListBoxTester _industryIdsListBox;
        private HtmlCheckBoxTester _residencyRequiredCheckBox;
        private HtmlCheckBoxTester _fullTimeCheckBox;
        private HtmlCheckBoxTester _hideContactDetailsCheckBox;
        private HtmlButtonTester _previewButton;
        private HtmlButtonTester _publishButton;

        private ReadOnlyUrl _newJobAdUrl;

        private const int MonitorInterval = 2;

        private WcfTcpHost _searchHost1;
        private IJobAdSearchService _searchService1;
        private WcfTcpHost _searchHost2;
        private IJobAdSearchService _searchService2;
        private WcfTcpHost _sortHost1;
        private IJobAdSortService _sortService1;
        private WcfTcpHost _sortHost2;
        private IJobAdSortService _sortService2;

        [TestInitialize]
        public void TestInitialize()
        {
            JobAdSearchHost.Stop();
            JobAdSortHost.Stop();
            StartSearchHosts();
            StartSortHosts();

            _titleTextBox = new HtmlTextBoxTester(Browser, "Title");
            _positionTitleTextBox = new HtmlTextBoxTester(Browser, "PositionTitle");
            _bulletPoint1TextBox = new HtmlTextBoxTester(Browser, "BulletPoint1");
            _bulletPoint2TextBox = new HtmlTextBoxTester(Browser, "BulletPoint2");
            _bulletPoint3TextBox = new HtmlTextBoxTester(Browser, "BulletPoint3");
            _summaryTextBox = new HtmlTextAreaTester(Browser, "Summary");
            _contentTextBox = new HtmlTextAreaTester(Browser, "Content");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _companyNameTextBox = new HtmlTextBoxTester(Browser, "CompanyName");
            _residencyRequiredCheckBox = new HtmlCheckBoxTester(Browser, "ResidencyRequired");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _fullTimeCheckBox = new HtmlCheckBoxTester(Browser, "FullTime");
            _hideContactDetailsCheckBox = new HtmlCheckBoxTester(Browser, "HideContactDetails");

            _previewButton = new HtmlButtonTester(Browser, "preview");
            _publishButton = new HtmlButtonTester(Browser, "publish");

            _newJobAdUrl = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            StopSortHosts();
            StopSearchHosts();
            JobAdSortHost.Start();
            JobAdSearchHost.Start();
        }

        [TestMethod]
        public void TestNewJobAd()
        {
            var member = CreateMember();
            var folder = _jobAdFoldersQuery.GetFolders(member)[0];

            var searchQuery = new JobAdSearchQuery { AdTitle = Expression.Parse(Title) };
            var sortQuery = new JobAdSortQuery();

            // Do some searches.

            var results = _searchService1.Search(null, searchQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            results = _searchService2.Search(null, searchQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            results = _sortService1.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            results = _sortService2.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            // Add a job ad.

            var employer = CreateEmployer();

            LogIn(employer);
            Get(_newJobAdUrl);
            CreateJobAd(employer.EmailAddress.Address);
            _previewButton.Click();
            _publishButton.Click();
            LogOut();

            // Add it to the folder.

            var jobAd = _jobAdsQuery.GetJobAds<JobAdEntry>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open))[0];
            _memberJobAdListsCommand.AddJobAdToFolder(member, folder, jobAd.Id);

            // Do some searches again.

            results = _searchService1.Search(null, searchQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            results = _sortService1.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            // Wait for the polling to kick in.

            Thread.Sleep(3 * MonitorInterval * 1000);
            results = _searchService2.Search(null, searchQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            results = _sortService2.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);
        }

        [TestMethod]
        public void TestCloseJobAd()
        {
            var member = CreateMember();
            var folder = _jobAdFoldersQuery.GetFolders(member)[0];

            var searchQuery = new JobAdSearchQuery { AdTitle = Expression.Parse(Title) };
            var sortQuery = new JobAdSortQuery();

            // Add a job ad.

            var employer = CreateEmployer();

            LogIn(employer);
            Get(_newJobAdUrl);
            CreateJobAd(employer.EmailAddress.Address);
            _previewButton.Click();
            _publishButton.Click();
            LogOut();

            // Add it to the folder.

            var jobAd = _jobAdsQuery.GetJobAds<JobAdEntry>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open))[0];
            _memberJobAdListsCommand.AddJobAdToFolder(member, folder, jobAd.Id);

            // Wait for the polling to kick in.

            Thread.Sleep(3 * MonitorInterval * 1000);

            // Do some searches.

            var results = _searchService1.Search(null, searchQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            results = _searchService2.Search(null, searchQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            results = _sortService1.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            results = _sortService2.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            // Close the job ad.

            _jobAdsCommand.CloseJobAd(jobAd);

            // Do some searches again.

            results = _searchService1.Search(null, searchQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            results = _sortService1.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);

            // Wait for the polling to kick in.

            Thread.Sleep(3 * MonitorInterval * 1000);
            results = _searchService2.Search(null, searchQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            results = _sortService2.SortFolder(member.Id, folder.Id, sortQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, results.JobAdIds[0]);
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        private Employer CreateEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            return employer;
        }

        private void CreateJobAd(string emailAddress)
        {
            _titleTextBox.Text = Title;
            _positionTitleTextBox.Text = "Code Monkey Position - lots of peanuts";
            _bulletPoint1TextBox.Text = "lots of peanuts";
            _bulletPoint2TextBox.Text = "and bananas";
            _bulletPoint3TextBox.Text = "and work hours";
            _summaryTextBox.Text = "Code monkey positon available - lots of hours, and bananas";
            _contentTextBox.Text = "Code monkey positon available - lots of hours, and bananas and peanuts";
            _companyNameTextBox.Text = "Acme";
            _emailAddressTextBox.Text = emailAddress;
            _hideContactDetailsCheckBox.IsChecked = false;
            _locationTextBox.Text = "Armadale, Vic";
            _industryIdsListBox.SelectedValues = new[] { _industryIdsListBox.Items[1].Value };
            _residencyRequiredCheckBox.IsChecked = true;
            _fullTimeCheckBox.IsChecked = true;
        }

        private void StartSearchHosts()
        {
            // The first service is the standard local service.

            var service = Resolve<JobAdSearchService>();
            _searchService1 = service;

            service.InitialiseIndex = true;
            service.RebuildIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.search.jobads.tcpAddress"),
                BindingName = "linkme.search.jobads.tcp",
            };

            _searchHost1 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _searchHost1.Open();
            _searchHost1.Start();

            ((IJobAdSearchService)service).Clear();

            // The second service represents the remote service.

            service = Resolve<JobAdSearchService>("linkme.search.jobads.otherservice");
            _searchService2 = service;

            service.InitialiseIndex = true;
            service.RebuildIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.search.jobads.other.tcpAddress"),
                BindingName = "linkme.search.jobads.tcp",
            };

            _searchHost2 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _searchHost2.Open();
            _searchHost2.Start();

            ((IJobAdSearchService)service).Clear();
        }

        private void StopSearchHosts()
        {
            _searchHost1.Stop();
            _searchHost1.Close();
            _searchHost1 = null;
            _searchService1 = null;

            _searchHost2.Stop();
            _searchHost2.Close();
            _searchHost2 = null;
            _searchService2 = null;
        }

        private void StartSortHosts()
        {
            // The first service is the standard local service.

            var service = Resolve<JobAdSortService>();
            _sortService1 = service;

            service.InitialiseIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.sort.jobads.tcpAddress"),
                BindingName = "linkme.sort.jobads.tcp",
            };

            _sortHost1 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _sortHost1.Open();
            _sortHost1.Start();

            ((IJobAdSortService)service).Clear();

            // The second service represents the remote service.

            service = Resolve<JobAdSortService>("linkme.sort.jobads.otherservice");
            _sortService2 = service;

            service.InitialiseIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.sort.jobads.other.tcpAddress"),
                BindingName = "linkme.sort.jobads.tcp",
            };

            _sortHost2 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _sortHost2.Open();
            _sortHost2.Start();

            ((IJobAdSortService)service).Clear();
        }

        private void StopSortHosts()
        {
            _sortHost1.Stop();
            _sortHost1.Close();
            _sortHost1 = null;
            _sortService1 = null;

            _sortHost2.Stop();
            _sortHost2.Close();
            _sortHost2 = null;
            _sortService2 = null;
        }
    }
}