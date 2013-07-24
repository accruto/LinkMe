using System;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public abstract class JobsTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        protected ReadOnlyUrl _baseJobUrl;
        private ReadOnlyUrl _jobUrl;
        protected ReadOnlyUrl _searchUrl;
        protected ReadOnlyUrl _searchResultsUrl;

        [TestInitialize]
        public void JobsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();

            _baseJobUrl = new ReadOnlyApplicationUrl(true, "~/jobs/");
            _jobUrl = new ReadOnlyApplicationUrl("~/jobs/Job.aspx");
            _searchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _searchResultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
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

        protected ReadOnlyUrl GetJobAdUrl(Guid jobAdId)
        {
            var url = _jobUrl.AsNonReadOnly();
            url.QueryString.Add("jobAdId", jobAdId.ToString());
            return url;
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected virtual JobAd CreateJobAd(IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd();
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }
    }
}