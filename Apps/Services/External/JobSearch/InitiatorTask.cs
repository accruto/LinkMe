using System;
using System.Diagnostics;
using System.Linq;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Apps.Agents.Tasks;

namespace LinkMe.Apps.Services.External.JobSearch
{
    public class InitiatorTask
        : Task
    {
        private static readonly EventSource Logger = new EventSource<InitiatorTask>();

        private readonly IChannelManager<IJobAdExporter> _channelManager;
        private readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IJobAdExportCommand _exportCommand;

        private int _progressInterval = 5*60*1000; // in milliseconds, default = 5 min.
        public TimeSpan ProgressInterval
        {
            set { _progressInterval = (int)value.TotalMilliseconds; }
        }

        public string[] ExcludedIntegrators { private get; set; }

        public InitiatorTask(IChannelManager<IJobAdExporter> channelManager, IJobAdIntegrationQuery jobAdIntegrationQuery, IIntegrationQuery integrationQuery, IJobAdExportCommand exportCommand)
            : base(Logger)
        {
            _channelManager = channelManager;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
            _integrationQuery = integrationQuery;
            _exportCommand = exportCommand;
        }

        public override void ExecuteTask()
        {
            ExecuteTaskCore(null);
        }

        public override void ExecuteTask(string[] args)
        {
            int? count = null;

            if (args.Length > 0)    
                count = int.Parse(args[0]);

            ExecuteTaskCore(count);
        }

        private void ExecuteTaskCore(int? count)
        {
            const string method = "ExecuteTaskCore";

            if (!ServiceAvailable())
            {
                Logger.Raise(Event.Information, method, "Processing delayed due to DEEWR service unavailability. Available times are M-F 0800-2300 and Sat 0900-1300.");
                return;
            }

            var excludedIntegratorIds = Array.ConvertAll(ExcludedIntegrators, GetIntegratorId);

            var jobAdIds = _jobAdIntegrationQuery.GetOpenJobAdIds(excludedIntegratorIds)
                .Where(jobAdId => !IsPublished(jobAdId));

            if (count.HasValue && count.Value != 0)
                jobAdIds = jobAdIds.Take(count.Value);

            var channel = _channelManager.Create();

            #region Log
            var timer = Stopwatch.StartNew();
            int processedCount = 0;
            Logger.Raise(Event.Information, method, "Processing started...");
            #endregion

            foreach (var jobAdId in jobAdIds)
            {
                try
                {
                    channel.Add(jobAdId);
                }
                catch (Exception)
                {
                    _channelManager.Abort(channel);
                    throw;
                }

                #region Log
                processedCount++;
                if (timer.ElapsedMilliseconds > _progressInterval)
                {
                    Logger.Raise(Event.Information, method, string.Format("{0} job ads exported...", processedCount));
                    timer.Reset();
                    timer.Start();
                }
                #endregion
            }

            #region Log
            Logger.Raise(Event.Information, method, string.Format("Processing complte. {0} job ads were exported.", processedCount));
            #endregion

            _channelManager.Close(channel);
        }

        private static bool ServiceAvailable()
        {
            var now = DateTime.Now;

            if (now.DayOfWeek >= DayOfWeek.Monday && now.DayOfWeek <= DayOfWeek.Friday && now.Hour >= 8 && now.Hour <= 23)
                return true;

            if (now.DayOfWeek == DayOfWeek.Saturday && now.Hour >= 9 && now.Hour <= 13)
                return true;

            return false;
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

        private bool IsPublished(Guid jobAdId)
        {
            bool found = (_exportCommand.GetJobSearchId(jobAdId) != null);
            return found;
        }
    }
}
