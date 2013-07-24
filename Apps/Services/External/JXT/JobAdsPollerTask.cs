using System;
using System.Reflection;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Apps.Services.External.Jxt.Schema;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Query.Reports.Roles.Integration.Commands;

namespace LinkMe.Apps.Services.External.Jxt
{
    public class JobAdsPollerTask
        : JobFeedReaderTaskBase<Job>
    {
        private static readonly EventSource<JobAdsPollerTask> Logger = new EventSource<JobAdsPollerTask>();

        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IExternalJobAdsQuery _externalJobAdsQuery;
        private readonly IJxtQuery _jxtQuery;

        public string[] IgnoredCompanies { private get; set; }

        public string RemoteBaseUrl { private get; set; }

        public JobAdsPollerTask(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, IExternalJobAdsQuery externalJobAdsQuery, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IJxtQuery jxtQuery)
            : base(jobAdsCommand, jobAdsQuery, jobAdIntegrationQuery, externalJobAdsCommand, jobAdIntegrationReportsCommand, Logger)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _externalJobAdsQuery = externalJobAdsQuery;
            _jxtQuery = jxtQuery;
        }

        public override void ExecuteTask()
        {
            ExecuteTaskCore(null);
        }

        public override void ExecuteTask(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException(
                    string.Format("Syntax:\r\n{0} baseUrl [file]", Assembly.GetEntryAssembly().GetName().Name));
            }

            if (args[0] != "*")
                RemoteBaseUrl = args[0];

            var filename = string.Empty;
            if (args.Length > 1)
            {
                filename = args[1];
            }

            ExecuteTaskCore(filename);
        }

        private void ExecuteTaskCore(string filename)
        {
            var reader = new JobFeedReader(RemoteBaseUrl, filename, Logger);
            var mapper = new JobAdMapper(_locationQuery, _industriesQuery, IgnoredCompanies);

            var integratorUser = _jxtQuery.GetIntegratorUser();
            var jobPoster = _externalJobAdsQuery.GetJobPoster(integratorUser);
            ProcessJobFeed(reader, mapper, integratorUser, jobPoster);
        }
    }
}
