using System;
using System.Reflection;
using System.ServiceModel.Syndication;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Reports.Roles.Integration.Commands;

namespace LinkMe.Apps.Services.External.PageUp
{
    public class JobFeedReaderTask
        : JobFeedReaderTaskBase<SyndicationItem>
    {
        private static readonly EventSource<JobFeedReaderTask> Logger = new EventSource<JobFeedReaderTask>();
        private readonly IExternalJobAdsQuery _externalJobAdsQuery;
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILocationQuery _locationQuery;

        public string IntegratorUserLoginId { private get; set; }
        public string JobPosterLoginId { private get; set; }
        public string RemoteUrl { private get; set; }

        public JobFeedReaderTask(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, IExternalJobAdsQuery externalJobAdsQuery, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand, IIntegrationQuery integrationQuery, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
            : base(jobAdsCommand, jobAdsQuery, jobAdIntegrationQuery, externalJobAdsCommand, jobAdIntegrationReportsCommand, Logger)
        {
            _externalJobAdsQuery = externalJobAdsQuery;
            _integrationQuery = integrationQuery;
            _industriesQuery = industriesQuery;
            _locationQuery = locationQuery;
            IntegratorUserLoginId = "PageUpPeople";
        }

        public override void ExecuteTask(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException(
                    string.Format("Syntax:\r\n{0} IntegratorUserLoginId JobPosterLoginId RemoteUrl", Assembly.GetEntryAssembly().GetName().Name));
            }

            if (args[0] != "*")
                IntegratorUserLoginId = args[0];

            if (args[1] != "*")
                JobPosterLoginId = args[1];

            if (args[2] != "*")
                RemoteUrl = args[2];

            ExecuteTask();
        }

        public override void ExecuteTask()
        {
            if (string.IsNullOrEmpty(IntegratorUserLoginId))
                throw new ApplicationException("The IntegratorUserLoginId is not set.");

            if (string.IsNullOrEmpty(JobPosterLoginId))
                throw new ApplicationException("The JobPosterLoginId is not set.");

            if (string.IsNullOrEmpty(RemoteUrl))
                throw new ApplicationException("The RemoteUrl is not set.");

            var reader = new JobFeedReader(RemoteUrl);
            var mapper = new JobFeedMapper(_industriesQuery, _locationQuery);

            var integratorUser = _integrationQuery.GetIntegratorUser(IntegratorUserLoginId);
            if (integratorUser == null)
                throw new ApplicationException("The integrator user with login id '" + IntegratorUserLoginId + "' cannot be found.");

            var jobPoster = _externalJobAdsQuery.GetJobPoster(integratorUser, JobPosterLoginId);
            if (jobPoster == null)
                throw new ApplicationException("The job poster with login id '" + JobPosterLoginId + "' cannot be found.");

            ProcessJobFeed(reader, mapper, integratorUser, jobPoster);
        }
    }
}
