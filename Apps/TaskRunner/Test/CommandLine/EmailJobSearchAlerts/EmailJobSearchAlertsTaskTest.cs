using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.EmailJobSearchAlerts
{
    [TestClass]
    public class EmailJobSearchAlertsTaskTest
        : CommandLineTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();

        private const string EmployerUserId = "employer";
        private const string MemberEmailAddressFormat = "member{0}@test.linkme.net.au";

        private static readonly Country Australia = LocationQuery.GetCountry("Australia");
        private static readonly Country NewZealand = LocationQuery.GetCountry("New Zealand");

        private const string MelbourneVic3000 = "Melbourne VIC 3000";
        private const string ArmadaleVic3143 = "Armadale VIC 3143";
        private const string NorlaneVic3214 = "Norlane VIC 3214";
        private const string SydneyNsw2000 = "Sydney NSW 2000";

        private const string Melbourne = "Melbourne";
        private const string Sydney = "Sydney";

        private const string Vic = "VIC";
        private const string Nsw = "NSW";

        private const string Auckland = "Auckland";
        private const string NorthIsland = "North Island";

        [TestInitialize]
        public void TestInitialize()
        {
            JobAdSearchHost.ClearIndex();
        }

        protected override string GetTaskGroup()
        {
            return "EmailJobSearchAlerts";
        }

        [TestMethod]
        public void SearchLocalityTest()
        {
            var index = 0;
            IList<string> titles = new List<string>();

            // Create jobs.

            PostJobAds();

            // Search within 50km of Melbourne VIC 3000.

            titles.Clear();
            AddTitles(titles, Australia, MelbourneVic3000);
            AddTitles(titles, Australia, ArmadaleVic3143);
            AddTitles(titles, Australia, Melbourne);
            //AddTitles(titles, Australia, Vic);
            PerformSearch(ref index, Australia, null, MelbourneVic3000, titles);

            // Search within 50km of Norlane VIC 3124.

            titles.Clear();
            AddTitles(titles, Australia, NorlaneVic3214);
            //AddTitles(titles, Australia, Vic);
            PerformSearch(ref index, Australia, null, NorlaneVic3214, titles);

            // Search within 50km of Sydney NSW 2000.

            titles.Clear();
            AddTitles(titles, Australia, SydneyNsw2000);
            AddTitles(titles, Australia, Sydney);
            //AddTitles(titles, Australia, Nsw);
            PerformSearch(ref index, Australia, null, SydneyNsw2000, titles);
        }

        private void PostJobAds()
        {
            var employer = CreateEmployer();

            // Create jobs in the localities.

            PostJobAds(employer, Australia, MelbourneVic3000);
            PostJobAds(employer, Australia, ArmadaleVic3143);
            PostJobAds(employer, Australia, NorlaneVic3214);
            PostJobAds(employer, Australia, SydneyNsw2000);

            // Create jobs in the regions.

            PostJobAds(employer, Australia, Melbourne);
            PostJobAds(employer, Australia, Sydney);

            // Create jobs in the subdivisions.

            PostJobAds(employer, Australia, Vic);
            PostJobAds(employer, Australia, Nsw);

            // Create jobs in the countries.

            PostJobAds(employer, Australia, "");
            PostJobAds(employer, NewZealand, "");

            // Create some New Zealand jobs.

            PostJobAds(employer, NewZealand, Auckland);
            PostJobAds(employer, NewZealand, NorthIsland);
        }

        private void PostJobAds(IEmployer employer, Country country, string location)
        {
            var locationReference = new LocationReference();
            LocationQuery.ResolveLocation(locationReference, country, location);
            PostJobAds(employer, country, locationReference);
        }

        private void PostJobAds(IEmployer employer, Country country, LocationReference location)
        {
            for (var index = 0; index < GetJobCount(location); ++index)
                PostJobAd(employer, index, country, location);
        }

        private void PostJobAd(IEmployer employer, int jobIndex, Country country, LocationReference location)
        {
            var jobAd = employer.CreateTestJobAd(GetJobTitle(jobIndex, country, location), "The content for job #" + jobIndex + " in " + location, GetIndustry(), location);
            _jobAdsCommand.PostJobAd(jobAd);
        }

        private static Industry GetIndustry()
        {
            return Resolve<IIndustriesQuery>().GetIndustries()[0];
        }

        private static int GetJobCount(LocationReference location)
        {
            switch (location.ToString())
            {
                default:
                    return 1;
            }
        }

        protected static string GetJobTitle(int jobIndex, Country country, LocationReference location)
        {
            return ((location.NamedLocation is CountrySubdivision && location.NamedLocation.Name == null) ? country.Name : location.ToString()) + ": " + jobIndex;
        }

        private static void AddTitles(ICollection<string> titles, Country country, string location)
        {
            var locationReference = new LocationReference();
            LocationQuery.ResolveLocation(locationReference, country, location);
            AddTitles(titles, country, locationReference);
        }

        private static void AddTitles(ICollection<string> titles, Country country, LocationReference location)
        {
            for (var index = 0; index < GetJobCount(location); ++index)
                titles.Add(GetJobTitle(index, country, location));
        }

        private void PerformSearch(ref int index, Country country, int? distance, string location, IList<string> titles)
        {
            // Create member.

            var member = CreateMember(++index);

            // Create a job search.

            var locationReference = LocationQuery.ResolveLocation(country, location);
            CreateTestSavedJobSearchAlert(member, null, locationReference, distance, DateTime.Now.AddDays(-1));

            // Get the email.

            Execute(true);
            var email = _emailServer.AssertEmailSent();
            Assert.IsNotNull(email, "No email or jobs found.");
            AssertTitles(email, titles, titles.Count, true);
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(string.Format(MemberEmailAddressFormat, index));
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(EmployerUserId, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected static void AssertTitles(MockEmail email, IList<string> locations, int totalMatches, bool checkEachLocation)
        {
            if (totalMatches == 1)
                email.AssertHtmlViewContains("<strong>1 new job</strong>");
            else
                email.AssertHtmlViewContains("<strong>" + totalMatches + " new jobs</strong>");

            if (locations.Count != totalMatches)
            {
                var check = "top " + locations.Count + " out of total";
                email.AssertHtmlViewContains(check);
            }

            if (checkEachLocation)
            {
                foreach (var location in locations)
                    email.AssertHtmlViewContains(location);
            }
        }

        private void CreateTestSavedJobSearchAlert(IHasId<Guid> jobSearcher, string jobTitle, LocationReference location, int? distance, DateTime whenSaved)
        {
            var criteria = new JobAdSearchCriteria
            {
                AdTitle = jobTitle,
                Location = location,
                Distance = distance
            };

            var execution = new JobAdSearchExecution { SearcherId = jobSearcher.Id, Criteria = criteria, Context = "Test" };
            execution.Results = _executeJobAdSearchCommand.Search(null, execution.Criteria, null).Results;

            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            var search = new JobAdSearch { OwnerId = jobSearcher.Id, Name = "Test Search Alert", Criteria = execution.Criteria.Clone() };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(jobSearcher.Id, search, whenSaved);
        }
    }
}