using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberAlerts;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberAlerts
{
    [TestClass]
    public class JobSearchAlertEmailTests
        : EmailTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();

        private const string JobTitle = "Drunk";
        private const string LongJobTitle = "Temporary part-time libraries North-West inter-library loan business unit administration assistant";
        private const string Location = "Melbourne VIC 3000";
        private const string LongLocation = "Llanfairpwllgwyngyllgogerychwyrndrobwllllantysiliogogogoch";
        private const string LongContent = "Further develop your niche skills in Microsoft technologies such as BizTalk or SharePoint in this sizeable development company with strong Microsoft capabilities. Boasting a variety of large scale and enterprise wide development projects in a variety of industries your design skills will be challenged as will your technical skills through peer development and a global Microsoft knowledge sharing. With the growth experienced and ambitious plans for 2007-08 the client will be assessing for capability to move into a Senior role within the next six months.";
        private const string SingleKeyword = "Developer";
        private const string OrKeywords = "Developer or Architect";
        private const string AndKeywords = "Developer Architect";
        private const string ComplexKeywords = "Developer or (Technical Architect)";

        private const int MaximumResults = 20;
        private const int MaximumSubjectLength = 300;
        private const string StartHighlightTag = "<span style=\"background-color: #ffff99\">";
        private const string EndHighlightTag = "</span>";

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create a member and search.

            var member = CreateMember(community);
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year}, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // Send the email.

            return new JobSearchAlertEmail(member, search.Results.TotalMatches, CreateEmailResults(search.Results, search.Criteria, MaximumResults), search.Criteria, Guid.Empty);
        }

        [TestMethod]
        public void TestFrequency()
        {
            var definition = _settingsQuery.GetDefinition("JobSearchAlertEmail");
            var category = _settingsQuery.GetCategory("MemberAlert");

            // Create a member and search.

            var member = CreateMember();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year }, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // No settings so notification should get through.

            SendIt(member, search, false);
            _emailServer.AssertEmailSent();
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            // Turn off.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Immediately);
            SendIt(member, search, false);
            _emailServer.AssertEmailSent();
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            // Set to monthly.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent(); 
            
            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Monthly);
            SendIt(member, search, false);
            _emailServer.AssertEmailSent();
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-40));
            SendIt(member, search, false);
            _emailServer.AssertEmailSent();
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-20));
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            // Set to weekly.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Weekly);
            SendIt(member, search, false);
            _emailServer.AssertEmailSent();
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-3));
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            // Set to daily.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Daily);
            SendIt(member, search, false);
            _emailServer.AssertEmailSent();
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(-1));
            SendIt(member, search, false);
            _emailServer.AssertEmailSent();

            _settingsCommand.SetLastSentTime(member.Id, definition.Id, DateTime.Now.AddDays(0));
            SendIt(member, search, false);
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create a member and search.

            var member = CreateMember();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year}, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // Send the email.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestEmailContent.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestAnonymousEmailContents()
        {
            // Create a member and search.

            var contact = CreateAnonymousContact();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(contact.Id, JobTitle, SingleKeyword, location, new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year }, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // Send the email.

            var templateEmail = SendIt(contact, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestAnonymousEmailContent.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, contact, search, bodyTemplate);
        }

        [TestMethod]
        public void TestDisabled()
        {
            // Create a member and search.

            var member = CreateMember();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year}, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // Disable.

            member.IsEnabled = false;

            // Send the email.

            SendIt(member, search, true);

            // Check.

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestDeactivated()
        {
            // Create a member and search.

            var member = CreateMember();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year}, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // Deactivate.

            member.IsActivated = false;

            // Send the email.

            SendIt(member, search, true);

            // Check.

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestLongTitleAndLocation()
        {
            // Create a member and search.

            var member = CreateMember();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year}, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, true);

            // Send the email.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestLongTitleAndLocation.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestSubject()
        {
            // Create a member.

            var member = CreateMember();

            // Create some results.

            var employer = CreateEmployer();
            var jobAdIds = CreateResults(employer, 1);

            // Nothing.

            AssertSubject(member, jobAdIds, null, null, null, null, null, JobTypes.None, MaximumSubjectLength);

            // Job title.

            AssertSubject(member, jobAdIds, JobTitle, null, null, null, null, JobTypes.None, MaximumSubjectLength);

            // Keywords

            AssertSubject(member, jobAdIds, null, SingleKeyword, null, null, null, JobTypes.None, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, OrKeywords, null, null, null, JobTypes.None, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, AndKeywords, null, null, null, JobTypes.None, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, ComplexKeywords, null, null, null, JobTypes.None, MaximumSubjectLength);

            // Location

            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            AssertSubject(member, jobAdIds, null, null, location, null, null, JobTypes.None, MaximumSubjectLength);

            // Job type.

            AssertSubject(member, jobAdIds, null, null, null, null, null, JobTypes.FullTime, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, null, null, null, null, JobTypes.Contract | JobTypes.FullTime, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, null, null, null, null, JobTypes.All, MaximumSubjectLength);

            // Salary

            AssertSubject(member, jobAdIds, null, null, null, new Salary {LowerBound = 50000, UpperBound = null, Rate = SalaryRate.Year}, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, null, null, new Salary {LowerBound = null, UpperBound = 50000, Rate = SalaryRate.Year}, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, null, null, new Salary {LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Year}, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, null, null, new Salary {LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Month}, null, JobTypes.Contract, MaximumSubjectLength);

            // Industries.

            var industriesQuery = Resolve<IIndustriesQuery>();
            var industry0 = industriesQuery.GetIndustries()[0];
            var industry1 = industriesQuery.GetIndustries()[1];
            AssertSubject(member, jobAdIds, null, null, null, null, new[] { industry0 }, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, null, null, null, new[] { industry0, industry1 }, JobTypes.Contract, MaximumSubjectLength);

            // Combinations.

            AssertSubject(member, jobAdIds, JobTitle, null, null, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, SingleKeyword, null, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, null, SingleKeyword, location, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, JobTitle, SingleKeyword, location, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(member, jobAdIds, JobTitle, SingleKeyword, location, new Salary { LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Year }, new[] { industry0, industry1 }, JobTypes.Contract, MaximumSubjectLength);
        }

        [TestMethod]
        public void TestNoCriteria()
        {
            // Create a member and search.

            var member = CreateMember();
            var search = CreateSearch(member.Id, null, null, null, null, null, JobTypes.None);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 1, false);

            // Send the email.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestNoCriteria.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestNoResults()
        {
            // Create a member and search.

            var member = CreateMember();
            var search = CreateSearch(member.Id, JobTitle, null, null, null, null, JobTypes.None);

            // No results.

            var employer = CreateEmployer();
            CreateResults(employer, search, 0, false);

            // Send.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestNoResults.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestResultsNoLocation()
        {
            // Create a member and search.

            var member = CreateMember();
            var search = CreateSearch(member.Id, JobTitle, null, null, null, null, JobTypes.None);

            // No location.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.Location = null;
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsNoLocation.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestResultsSalary()
        {
            // Create a member and search.

            var member = CreateMember();
            var search = CreateSearch(member.Id, JobTitle, null, null, null, null, JobTypes.None);

            // Lower and upper bound.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsSalaryLU.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);

            // Lower bound.

            jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.Salary = new Salary { LowerBound = 100000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            templateEmail = SendIt(member, search, true);

            // Check.

            bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsSalaryL.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);

            // Upper bound.

            jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.Salary = new Salary { LowerBound = null, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            templateEmail = SendIt(member, search, true);

            // Check.

            bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsSalaryU.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);

            // None.

            jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.Salary = null;
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            templateEmail = SendIt(member, search, true);

            // Check.

            bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsSalaryNone.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestResultsNoJobTypes()
        {
            // Create a member and search.

            var member = CreateMember();
            var search = CreateSearch(member.Id, JobTitle, null, null, null, null, JobTypes.None);

            // No location.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.JobTypes = JobTypes.None;
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsNoJobTypes.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestResultsNoIndustries()
        {
            // Create a member and search.

            var member = CreateMember();
            var search = CreateSearch(member.Id, JobTitle, null, null, null, null, JobTypes.None);

            // No location.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.Description.Industries = null;
            _jobAdsCommand.UpdateJobAd(jobAd);
            search.Results = new JobAdSearchResults {TotalMatches = 1, JobAdIds = new List<Guid> {jobAd.Id}};

            // Send.

            var templateEmail = SendIt(member, search, true);

            // Check.

            var bodyTemplate = File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsNoIndustries.htm", RuntimeEnvironment.GetSourceFolder()));
            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        [TestMethod]
        public void TestResults2()
        {
            TestResults(2, File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResults2.htm", RuntimeEnvironment.GetSourceFolder())));
        }

        [TestMethod]
        public void TestResultsLessMax()
        {
            TestResults(MaximumResults - 10, File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsLessMax.htm", RuntimeEnvironment.GetSourceFolder())));
        }

        [TestMethod]
        public void TestResultsMax()
        {
            TestResults(MaximumResults, File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsMax.htm", RuntimeEnvironment.GetSourceFolder())));
        }

        [TestMethod]
        public void TestResultsMoreMax()
        {
            TestResults(MaximumResults + 10, File.ReadAllText(FileSystem.GetAbsolutePath(@"Apps\Agents\Test\Communications\Emails\MemberAlerts\TestResultsMoreMax.htm", RuntimeEnvironment.GetSourceFolder())));
        }

        private void TestResults(int count, string bodyTemplate)
        {
            // Create a member and search.

            var member = CreateMember();
            var industry = Resolve<IIndustriesQuery>().GetIndustries()[0];
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, _australia, Location);
            var search = CreateSearch(member.Id, JobTitle, SingleKeyword, location, new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year}, new[] { industry }, JobTypes.Contract);

            // Create some results.

            var employer = CreateEmployer();
            CreateResults(employer, search, count, false);

            // Send.

            var templateEmail = SendIt(member, search, true);

            // Check.

            AssertMail(templateEmail, member, search, bodyTemplate);
        }

        private void AssertMail(TemplateEmail templateEmail, ICommunicationUser member, JobAdSearchExecution search, string bodyTemplate)
        {
            var email = _emailServer.AssertEmailSent();

            // Assert addresses.

            email.AssertAddresses(Return, Return, member);
            AssertCompatibleAddresses(email);

            // Assert subject.

            email.AssertSubject(GetSubject(search.Criteria, MaximumSubjectLength));

            // Assert body.

            email.AssertHtmlViewChecks(null, false, null);
            email.AssertHtmlView(GetBody(bodyTemplate, templateEmail, search, member.Id, member.UserType == UserType.Anonymous));

            // Assert attachments.

            email.AssertNoAttachments();
        }

        private static JobAdSearchExecution CreateSearch(Guid memberId, string jobTitle, string keywords, LocationReference location, Salary salary, IList<Industry> industries, JobTypes jobTypes)
        {
            // Create a criteria determined by what is passed in.

            JobAdSearchCriteria criteria;
            if (salary != null || jobTypes != JobTypes.None || industries != null)
            {
                Guid[] industryIds = null;
                if (industries != null)
                {
                    industryIds = new Guid[industries.Count];
                    for (var index = 0; index < industries.Count; ++index)
                        industryIds[index] = industries[index].Id;
                }

                criteria = new JobAdSearchCriteria
                               {
                                   AdTitle = jobTitle,
                                   Location = location,
                                   Salary = salary,
                                   JobTypes = jobTypes,
                                   IndustryIds = industryIds
                               };
                criteria.SetKeywords(keywords);
            }
            else
            {
                criteria = new JobAdSearchCriteria {AdTitle = jobTitle, Location = location};
                criteria.SetKeywords(keywords);
            }

            // Create the search.

            return new JobAdSearchExecution { SearcherId = memberId, Criteria = criteria, Context = "SimpleJobSearch_2.3" };
        }

        private static string GetSubject(JobAdSearchCriteria criteria, int maximumLength)
        {
            var criteriaText = GetCriteriaText(criteria);
            var subject = HtmlUtil.StripHtmlTags("Job alert" + (criteriaText.Length == 0 ? string.Empty : " - " + criteriaText));

            // Truncate if exceeds the limit.

            return TextUtil.TruncateForDisplay(subject, maximumLength);
        }

        private string GetBody(string bodyTemplate, TemplateEmail templateEmail, JobAdSearchExecution search, Guid userId, bool isAnonymous)
        {
            var body = new StringBuilder(bodyTemplate);
            body.Replace("$(InsecureRootPath)", InsecureRootPath);
            body.Replace("$(ViewAllUrl)", GetTinyUrl(templateEmail, false, "~/search/jobs", new QueryStringGenerator(new JobAdSearchCriteriaConverter(_locationQuery, _industriesQuery)).GenerateQueryString(search.Criteria)));
            body.Replace("$(EditAlertsUrl)", GetTinyUrl(templateEmail, true, "~/members/searches/saved"));
            body.Replace("$(Date)", DateTime.Now.ToShortDateString());
            body.Replace("$(ManageEmailsUrl)", GetTinyUrl(templateEmail, true, "~/members/settings"));
            body.Replace("$(ContactUsUrl)", GetTinyUrl(templateEmail, false, "~/contactus"));
            body.Replace("$(UnsubscribeUrl)", isAnonymous ? GetTinyUrl(templateEmail, false, "~/members/searches/" + search.Id + "/delete") : GetTinyUrl(templateEmail, false, "~/accounts/settings/unsubscribe", "userId", userId.ToString("N"), "category", "MemberAlert"));
            body.Replace("$(TrackingUrl)", GetTrackingPixelUrl(templateEmail));

            for (var i = 0; i < search.Results.JobAdIds.Count; i++)
            {
                body.Replace("$(JobAdUrl" + i.ToString("D2") + ")",
                    GetTinyUrl(templateEmail, false, "~/jobs/" + search.Results.JobAdIds[i]));
            }

            return body.ToString();
        }

        private static string GetCriteriaText(JobAdSearchCriteria criteria)
        {
            var text = string.Empty;
            if (criteria.AdTitleExpression != null)
                text += TextUtil.TrimEndBracketsFromExpression(criteria.AdTitleExpression.GetUserExpression());

            if (criteria.KeywordsExpression != null)
                text += (text.Length == 0 ? string.Empty : ", ") + TextUtil.TrimEndBracketsFromExpression(
                    criteria.KeywordsExpression.GetUserExpression());

            if (criteria.Location != null)
                text += (text.Length == 0 ? string.Empty : ", ") + criteria.Location;

            if (criteria.Salary != null)
                text += (text.Length == 0 ? string.Empty : ", ") + criteria.Salary.GetDisplayText();

            if (criteria.IndustryIds != null && criteria.IndustryIds.Count > 0)
                text += (text.Length == 0 ? string.Empty : ", ") + GetIndustriesText(criteria.IndustryIds);

            if (criteria.JobTypes != JobTypes.All)
                text += (text.Length == 0 ? string.Empty : ", ") + GetJobTypesText(criteria.JobTypes);

            return (text.Length == 0 ? "All jobs" : text);
        }

        private static string GetJobTypesText(JobTypes types)
        {
            var subject = string.Empty;
            if ((types & JobTypes.FullTime) != 0)
                subject = GetJobTypesText(subject, "Full time");
            if ((types & JobTypes.PartTime) != 0)
                subject = GetJobTypesText(subject, "Part time");
            if ((types & JobTypes.Contract) != 0)
                subject = GetJobTypesText(subject, "Contract");
            if ((types & JobTypes.Temp) != 0)
                subject = GetJobTypesText(subject, "Temp");
            if ((types & JobTypes.JobShare) != 0)
                subject = GetJobTypesText(subject, "Job share");
            return subject;
        }

        private static string GetJobTypesText(string subject, string text)
        {
            if (!string.IsNullOrEmpty(subject))
                subject += " OR ";
            subject += text;
            return subject;
        }

        private static string GetIndustriesText(IEnumerable<Guid> industryIds)
        {
            var text = string.Empty;
            foreach (var industryId in industryIds)
            {
                var industry = Resolve<IIndustriesQuery>().GetIndustry(industryId);
                if (!string.IsNullOrEmpty(text))
                    text += " OR ";
                text += industry.Name;
            }

            return text;
        }

        private void AssertSubject(ICommunicationUser member, ICollection<Guid> jobAdIds, string jobTitle, string keywords, LocationReference location, Salary salary, IList<Industry> industries, JobTypes jobTypes, int maximumLength)
        {
            // Create the search.

            var search = CreateSearch(member.Id, jobTitle, keywords, location, salary, industries, jobTypes);
            search.Results = new JobAdSearchResults {TotalMatches = jobAdIds.Count, JobAdIds = jobAdIds.ToList()};

            // Send the email.

            SendIt(member, search, true);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertSubject(GetSubject(search.Criteria, maximumLength));
        }

        private Guid[] CreateResults(IEmployer employer, int results)
        {
            // Create jobs.

            var jobAdIds = new Guid[results];
            for (var index = 0; index < results; ++index)
            {
                var jobAd = _jobAdsCommand.PostTestJobAd(employer);
                jobAdIds[index] = jobAd.Id;
            }

            return jobAdIds;
        }

        private void CreateResults(IEmployer employer, JobAdSearchExecution search, int results, bool useLongForms)
        {
            // Create jobs.

            var jobAdIds = new Guid[results];
            for (var index = 0; index < results; ++index)
            {
                var jobAd = _jobAdsCommand.PostTestJobAd(employer);

                if (useLongForms)
                {
                    jobAd.Title = LongJobTitle;
                    _locationQuery.ResolvePostalSuburb(jobAd.Description.Location, _australia, LongLocation);
                    jobAd.Description.Content = LongContent;
                    _jobAdsCommand.UpdateJobAd(jobAd);
                }

                jobAdIds[index] = jobAd.Id;
            }

            // Create the results.

            search.Results = new JobAdSearchResults {TotalMatches = results, JobAdIds = jobAdIds.ToList()};
        }

        private TemplateEmail SendIt(ICommunicationUser member, JobAdSearchExecution search, bool resetLastSentTime)
        {
            var templateEmail = new JobSearchAlertEmail(member, search.Results.TotalMatches, CreateEmailResults(search.Results, search.Criteria, MaximumResults), search.Criteria, Guid.Empty);

            _emailsCommand.TrySend(templateEmail);

            if (resetLastSentTime)
            {
                // The last sent time will mean that an email might not be sent.
                // These tests are looking at  the contents so bypass that.

                var definition = _settingsQuery.GetDefinition(templateEmail.Definition);
                _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);
            }

            return templateEmail;
        }

        private IList<JobSearchAlertEmailResult> CreateEmailResults(JobAdSearchResults searchResults, JobAdSearchCriteria criteria, int maximumResults)
        {
            var emailResults = new List<JobSearchAlertEmailResult>();
            if (searchResults.JobAdIds.Count > 0)
            {
                var highlighter = new JobSearchHighlighter(criteria, StartHighlightTag, EndHighlightTag);
                AppendResults(emailResults, searchResults, maximumResults, highlighter, criteria.KeywordsExpression != null);
            }
            return emailResults;
        }

        private void AppendResults(ICollection<JobSearchAlertEmailResult> emailResults, JobAdSearchResults searchResults, int maximumResults, JobSearchHighlighter highlighter, bool haveKeywords)
        {
            foreach (var jobAdId in searchResults.JobAdIds)
            {
                // Get the job ad for the result.

                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
                if (jobAd != null)
                {
                    AppendResult(emailResults, jobAd, highlighter, haveKeywords);
                    if (emailResults.Count == maximumResults)
                        return;
                }
            }
        }

        private static void AppendResult(ICollection<JobSearchAlertEmailResult> emailResults, JobAd jobAd, JobSearchHighlighter highlighter, bool haveKeywords)
        {
            var emailResult = new JobSearchAlertEmailResult
            {
                JobAdId = jobAd.Id.ToString(),
                Title = highlighter.HighlightTitle(jobAd.Title),
                Location = jobAd.GetLocationDisplayText()
            };

            if (jobAd.Description.Salary != null)
                emailResult.Salary = jobAd.Description.Salary.GetDisplayText();

            emailResult.PostedAge = jobAd.GetPostedDisplayText();
            emailResult.PostedDate = jobAd.CreatedTime.ToShortDateString();
            emailResult.JobType = jobAd.Description.JobTypes.GetDisplayText(", ", false, false);

            if (jobAd.Description.Industries != null)
                emailResult.Industry = jobAd.Description.Industries.GetCriteriaIndustriesDisplayText();

            Summarize(jobAd, highlighter, haveKeywords, out emailResult.Digest, out emailResult.Fragments);

            emailResults.Add(emailResult);
        }

        private static void Summarize(JobAd jobAd, JobSearchHighlighter highlighter, bool haveKeywords, out string digestHtml, out string fragmentsHtml)
        {
            if (haveKeywords)
            {
                // Show highlighted short summary + best highlighted content fragments.

                string digestText;
                string bodyText;
                highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, false, out digestText, out bodyText);

                digestHtml = highlighter.HighlightContent(digestText, false);
                fragmentsHtml = highlighter.GetBestContent(bodyText);
            }
            else
            {
                // Show long summary without highlighting.

                string digestText;
                string bodyText;
                highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, true, out digestText, out bodyText);

                digestHtml = HttpUtility.HtmlEncode(digestText).Replace("\x2022", "&#x2022;");
                fragmentsHtml = string.Empty;
            }
        }
    }
}