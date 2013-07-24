using System;
using System.Text;
using LinkMe.AcceptanceTest.Emails;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Alerts
{
    [TestClass]
    public abstract class AlertsTests
        : EmailsTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        private readonly IAnonymousUsersCommand _anonymousUsersCommand = Resolve<IAnonymousUsersCommand>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IAnonymousUsersQuery _anonymousUsersQuery = Resolve<IAnonymousUsersQuery>();
        protected readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        protected readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        protected const string BusinessAnalyst = "business analyst";
        private const string EmailAddressFormat = "homer{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";

        protected HtmlButtonTester _unsubscribeButton;

        protected ReadOnlyUrl _settingsUrl;
        protected ReadOnlyUrl _savedUrl;
        private ReadOnlyUrl _baseJobUrl;
        protected ReadOnlyUrl _contactUsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _unsubscribeButton = new HtmlButtonTester(Browser, "Unsubscribe");

            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
            _savedUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/saved");
            _baseJobUrl = new ReadOnlyApplicationUrl(true, "~/jobs/");
            _contactUsUrl = new ReadOnlyApplicationUrl("~/faqs/setting-up-your-profile/09d11385-0213-4157-a5a9-1b2a74e6887e");

            JobAdSearchHost.ClearIndex();
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected AnonymousContact CreateAnonymousContact(int index)
        {
            var contactDetails = new ContactDetails
            {
                EmailAddress = string.Format(EmailAddressFormat, index),
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
            };
            return _anonymousUsersCommand.CreateContact(new AnonymousUser(), contactDetails);
        }

        protected JobAd PostJobAd(IEmployer employer, string jobTitle)
        {
            var jobAd = employer.CreateTestJobAd(jobTitle);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        protected void ExecuteTask()
        {
            new EmailJobSearchAlertsTask(_emailsCommand, _jobAdsQuery, _executeJobAdSearchCommand, _jobAdSearchAlertsCommand, _jobAdSearchAlertsQuery, _jobAdSearchesQuery, _membersQuery, _anonymousUsersQuery).ExecuteTask();
        }

        protected ReadOnlyUrl GetSearchUrl(string title)
        {
            var searchUrl = _searchUrl.AsNonReadOnly();
            searchUrl.QueryString["AdTitle"] = title;
            return searchUrl;
        }

        protected ReadOnlyUrl GetResultsUrl()
        {
            return _resultsUrl;
        }

        protected ReadOnlyUrl GetJobAdUrl(JobAd jobAd)
        {
            return GetJobAdUrl(jobAd.Id, jobAd.Title, jobAd.Description.Location, jobAd.Description.Industries[0]);
        }

        protected ReadOnlyUrl GetJobAdUrl(Guid jobAdId, string jobAdTitle, LocationReference location, Industry industry)
        {
            var sb = new StringBuilder();

            // Location.

            if (!string.IsNullOrEmpty(location.ToString()))
                sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(location.ToString())).ToLower().Replace(' ', '-'));
            else
                sb.Append("-");

            // Industry. If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            var industrySb = new StringBuilder();
            if (industry != null)
                industrySb.Append(industry.UrlName);
            sb.Append("/");
            if (industrySb.Length == 0)
                sb.Append("-");
            else
                sb.Append(industrySb);

            // Job title.

            sb.Append("/");
            sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(jobAdTitle)).ToLower().Replace(' ', '-'));

            // Id

            sb.Append("/");
            sb.Append(jobAdId.ToString());

            return new ReadOnlyApplicationUrl(_baseJobUrl, sb.ToString());
        }
    }
}
