using System;
using System.Collections.Generic;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display
{
    [TestClass]
    public abstract class DisplayTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string Country = "Australia";
        private const string Title = "JobTitle";
        private const string Content = "This is the job content";
        private ReadOnlyUrl _baseJobUrl;

        [TestInitialize]
        public void DisplayTestsInitialize()
        {
            _baseJobUrl = new ReadOnlyApplicationUrl(true, "~/jobs/");
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected virtual Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected JobAd PostJobAd(IEmployer employer, Action<JobAd> action)
        {
            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = Title,
                Description =
                {
                    Content = Content,
                }
            };

            if (action != null)
                action(jobAd);

            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }

        protected JobAd PostJobAd(IEmployer employer)
        {
            return PostJobAd(employer, null);
        }

        protected LocationReference GetLocation(string location)
        {
            return _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), location);
        }

        protected Industry GetIndustry(string industry)
        {
            return _industriesQuery.GetIndustry(industry);
        }

        protected ReadOnlyUrl GetJobUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(_baseJobUrl, jobAdId.ToString());
        }

        protected ReadOnlyUrl GetJobUrl(JobAd jobAd)
        {
            return GetJobUrl(jobAd.Id, jobAd.Title, jobAd.Description.Location, jobAd.Description.Industries);
        }

        protected ReadOnlyUrl GetJobUrl(Guid jobAdId, string jobAdTitle, LocationReference location, IList<Industry> industries)
        {
            var sb = new StringBuilder();

            // Location.

            if (location == null)
            {
                sb.Append("-");
            }
            else
            {
                sb.Append(!string.IsNullOrEmpty(location.ToString())
                    ? TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(location.ToString())).ToLower().Replace(' ', '-')
                    : location.Country.Name.ToLower());
            }

            // Industry. If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            var industrySb = new StringBuilder();
            if (industries != null && industries.Count > 0)
                industrySb.Append(industries[0].UrlName);
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
