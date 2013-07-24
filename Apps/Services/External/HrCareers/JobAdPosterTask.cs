using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Services.External.HrCareers.Queries;
using LinkMe.Apps.Services.External.HrCareers.Schema;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using LinkMe.Query.Search.JobAds.Commands;

namespace LinkMe.Apps.Services.External.HrCareers
{
    public class JobAdPosterTask
        : Task
    {
        private static readonly EventSource<JobAdPosterTask> Logger = new EventSource<JobAdPosterTask>();
        private const int MaxDescriptionLength = 64000;

        private readonly IChannelManager<ISyndicate> _serviceManager;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand;
        private readonly IntegratorUser _integratorUser;
        private readonly HrJobAdSearcher _searcher;
        private readonly JobAdMapper _mapper;

        public string RemoteUsername { private get; set; }
        public string RemotePassword { private get; set; }
        public int RemoteCompanyId { private get; set; }
        public Guid? VerticalId { private get; set; }

        private Guid[] _excludedIntegratorIds = new Guid[0];
        public string[] ExcludedIntegrators
        {
            set { _excludedIntegratorIds = Array.ConvertAll(value, GetIntegratorId); }
        }

        public JobAdPosterTask(IChannelManager<ISyndicate> channelManager, IHrCareersQuery hrCareersQuery, ILocationQuery locationQuery, IExecuteJobAdSearchCommand jobAdSearch, IIntegrationQuery integrationQuery, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand, IWebSiteQuery webSiteQuery, IJobAdsQuery jobAdsQuery)
            : base(Logger)
        {
            _serviceManager = channelManager;
            _jobAdsQuery = jobAdsQuery;
            _integrationQuery = integrationQuery;
            _jobAdIntegrationReportsCommand = jobAdIntegrationReportsCommand;

            _integratorUser = hrCareersQuery.GetIntegratorUser();
            _searcher = new HrJobAdSearcher(jobAdSearch);
            _mapper = new JobAdMapper(locationQuery, webSiteQuery);
        }

        public override void ExecuteTask()
        {
            ExecuteTaskCore(null);
        }

        public override void ExecuteTask(string[] args)
        {
            ExecuteTaskCore(int.Parse(args[0]));
        }

        private void ExecuteTaskCore(int? maxPostCount)
        {
            const string method = "ExecuteTask";

            // Check properties.

            if (string.IsNullOrEmpty(RemoteUsername))
                throw new ArgumentException("RemoteUsername property must be specified.");

            if (string.IsNullOrEmpty(RemotePassword))
                throw new ArgumentException("RemotePassword property must be specified.");

            if (RemoteCompanyId == 0)
                throw new ArgumentException("RemoteCompanyId property must be specified.");

            _mapper.VerticalId = VerticalId;

            // Extract and map jobs to post.
            #region Log
            Logger.Raise(Event.Information, method, "Searching for HR related job ad...");
            #endregion

            var searchResults = _searcher.Search(); // map: jobAdId -> categoryId[]
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(searchResults.Select(r => r.Key)).Where(jobAd => !IsExcluded(jobAd));

            var posts = new List<Job>();
            var i = 0;

            foreach (var jobAd in jobAds)
            {
                i++;

                try
                {
                    var post = _mapper.Map(jobAd, searchResults[jobAd.Id]);
                    if (post.description.Length < MaxDescriptionLength)
                    {
                        posts.Add(post);
                    }
                }
                catch (Exception e)
                {
                    #region Log
                    Logger.Raise(Event.NonCriticalError, method, "Unable to map job ad.", e, null, Event.Arg("JobAdId", jobAd.Id));
                    #endregion
                }

                if (maxPostCount.HasValue && i >= maxPostCount)
                {
                    // Prepare request.
                    PostJobs(method, posts);

                    i = 0;
                    posts = new List<Job>();
                }
            }

            if (posts.Count > 0)
            {
                // Prepare request.
                PostJobs(method, posts);
            }
        }

        private void PostJobs(string method, List<Job> posts)
        {
            #region Log
            Logger.Raise(Event.Information, method, string.Format("Posting {0} job ads...", posts.Count));
            #endregion

            var request = new JobCollection { companyid = RemoteCompanyId, jobs = posts.ToArray() };
            var serializer = new XmlSerializer(typeof(JobCollection));
            var writer = new StringWriter();
            serializer.Serialize(writer, request);
            writer.Flush();
            var requestXml = writer.ToString();
            string response;

            // Send request.

            var service = _serviceManager.Create();
            try
            {
                response = service.Sync(requestXml, RemoteUsername, RemotePassword);
            }
            catch (Exception ex)
            {
                _serviceManager.Abort(service);

                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = posts.Count, JobAds = posts.Count });

                #region Log
                Logger.Raise(Event.CriticalError, method, string.Format("Aborting Service. {0}", ex));
                #endregion

                throw;
            }
            _serviceManager.Close(service);

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = true, IntegratorUserId = _integratorUser.Id, Posted = posts.Count, JobAds = posts.Count });

            #region Log
            Logger.Raise(Event.Information, method, string.Format("Processing complete. {0}", response));
            #endregion
        }

        private Guid GetIntegratorId(string integratorName)
        {
            var integrator = _integrationQuery.GetIntegratorUser(integratorName);
            if (integrator == null)
            {
                throw new ApplicationException("There is no IntegratorUser with username '"
                    + integratorName + "'.");
            }

            return integrator.Id;
        }

        private bool IsExcluded(JobAdEntry jobAd)
        {
            if (jobAd.Integration == null || jobAd.Integration.IntegratorUserId == null)
                return false;

            return _excludedIntegratorIds.Contains(jobAd.Integration.IntegratorUserId.Value);
        }
    }
}
