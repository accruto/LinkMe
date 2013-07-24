using System;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks
{
	public class UpdateJobAdsTask
        : Task
	{
	    private static readonly EventSource EventSource = new EventSource<UpdateJobAdsTask>();
	    private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;

        public UpdateJobAdsTask(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery)
	        : base(EventSource)
	    {
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
	    }

        public override void ExecuteTask()
        {
            CloseExpiredJobAds();
            RefreshJobAds();
        }

	    private void CloseExpiredJobAds()
	    {
	        const string method = "CloseExpiredJobAds";
	        EventSource.Raise(Event.Information, method, "Closing expired job ads");

	        var ids = _jobAdsQuery.GetExpiredJobAdIds();
	        var count = 0;

	        foreach (var id in ids)
	        {
	            try
	            {
	                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(id);
	                if (jobAd != null)
	                {
	                    _jobAdsCommand.CloseJobAd(jobAd);
	                    ++count;
	                }
	            }
	            catch (Exception ex)
	            {
	                EventSource.Raise(Event.Error, method, string.Format("Could not close the '{0}' job ad.", id), ex);
	            }
	        }

	        EventSource.Raise(Event.Information, method, string.Format("{0} expired job ads were closed.", count));
	    }

        private void RefreshJobAds()
        {
            const string method = "RefreshJobAds";
            EventSource.Raise(Event.Information, method, "Refreshing job ads");

            var ids = _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.Date.AddDays(-7));
            var count = 0;

            foreach (var id in ids)
            {
                try
                {
                    var jobAd = _jobAdsQuery.GetJobAd<JobAd>(id);
                    if (jobAd != null)
                    {
                        _jobAdsCommand.RefreshJobAd(jobAd);
                        ++count;
                    }
                }
                catch (Exception ex)
                {
                    EventSource.Raise(Event.Error, method, string.Format("Could not refresh the '{0}' job ad.", id), ex);
                }
            }

            EventSource.Raise(Event.Information, method, string.Format("{0} job ads were refreshed.", count));
        }
	}
}
