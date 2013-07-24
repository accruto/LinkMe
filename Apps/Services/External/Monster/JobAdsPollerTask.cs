using System;
using System.Collections.Generic;
using System.Reflection;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Apps.Services.External.Monster.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Query.Reports.Roles.Integration.Commands;

namespace LinkMe.Apps.Services.External.Monster
{
    public class JobAdsPollerTask
        : JobFeedReaderTaskBase<Job>
    {
        private static readonly EventSource<JobAdsPollerTask> Logger = new EventSource<JobAdsPollerTask>();

        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IExternalJobAdsQuery _externalJobAdsQuery;
        private readonly ICareerOneQuery _careerOneQuery;

        public string[] IgnoredCompanies { private get; set; }

        public string RemoteBaseUrl { private get; set; }
        public string RemoteUsername { private get; set; }
        public string RemotePassword { private get; set; }

        public JobAdsPollerTask(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, IExternalJobAdsQuery externalJobAdsQuery, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, ICareerOneQuery careerOneQuery)
            : base(jobAdsCommand, jobAdsQuery, jobAdIntegrationQuery, externalJobAdsCommand, jobAdIntegrationReportsCommand, Logger)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _externalJobAdsQuery = externalJobAdsQuery;
            _careerOneQuery = careerOneQuery;
        }

        public override void ExecuteTask()
        {
            ExecuteTaskFiles(null);
        }

        public override void ExecuteTask(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException(
                    string.Format("Syntax:\r\n{0} username password baseUrl [file1 [...]]", Assembly.GetEntryAssembly().GetName().Name));
            }

            if (args[0] != "*")
                RemoteUsername = args[0];

            if (args[1] != "*")
                RemotePassword = args[1];

            if (args[2] != "*")
                RemoteBaseUrl = args[2];

            string[] files = null;
            if (args.Length > 3)
            {
                int fileCount = args.Length - 3;
                files = new string[fileCount];
                Array.Copy(args, 3, files, 0, fileCount);
            }

            ExecuteTaskFiles(files);
        }

        private void ExecuteTaskFiles(IList<string> files)
        {
            var reader = new JobFeedReader(RemoteBaseUrl, RemoteUsername, RemotePassword, files, Logger);
            var mapper = new JobAdMapper(_locationQuery, _industriesQuery, IgnoredCompanies);

            var integratorUser = _careerOneQuery.GetIntegratorUser();
            var jobPoster = _externalJobAdsQuery.GetJobPoster(integratorUser);
            ProcessJobFeed(reader, mapper, integratorUser, jobPoster);
        }
    }
}
