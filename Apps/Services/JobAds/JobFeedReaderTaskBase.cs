using System;
using System.Collections.Generic;
using System.Diagnostics;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;

namespace LinkMe.Apps.Services.JobAds
{
    public interface IJobFeedReader<TPost>
    {
        IEnumerable<TPost> GetPosts();
    }

    public interface IJobFeedMapper<TPost>
    {
        string GetPostId(TPost post);
        bool IsIgnored(TPost post);
        void ApplyPostData(TPost post, JobAd jobAd);
    }

    public abstract class JobFeedReaderTaskBase<TPost>
        : Task
    {
        private readonly EventSource _logger;
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;
        private readonly IExternalJobAdsCommand _externaljobAdsCommand;
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand; 

        private int _progressInterval = 5 * 60 * 1000; // in milliseconds, default = 5 min.
        public TimeSpan ProgressInterval
        {
            set { _progressInterval = (int)value.TotalMilliseconds; }
        }

        protected JobFeedReaderTaskBase(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand, EventSource logger)
            : base(logger)
        {
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
            _externaljobAdsCommand = externalJobAdsCommand;
            _jobAdIntegrationReportsCommand = jobAdIntegrationReportsCommand;
            _logger = logger;
        }

        protected void ProcessJobFeed(IJobFeedReader<TPost> reader, IJobFeedMapper<TPost> mapper, IntegratorUser integratorUser, IEmployer jobPoster)
        {
            const string method = "ExecuteTaskCore";
            if (integratorUser == null)
                throw new ArgumentNullException("integratorUser");
            if (jobPoster == null)
                throw new ArgumentNullException("jobPoster");

            var oldJobAdIds = GetOpenJobAdsSnapshot(integratorUser.Id, jobPoster.Id);
            var posts = reader.GetPosts();

            var report = new Report();

            #region Log
            var timer = Stopwatch.StartNew();
            var count = 0;
            _logger.Raise(Event.Information, method, "Processing job posts...");
            #endregion

            foreach (var post in posts)
            {
                report.JobAds++;

                var postId = mapper.GetPostId(post);
                try
                {
                    var existingJobAd = _externaljobAdsCommand.GetExistingJobAd(integratorUser.Id, postId);
                    if (existingJobAd == null)
                    {
                        if (mapper.IsIgnored(post))
                        {
                            report.Ignored++;
                        }
                        else
                        {
                            // Create new job ad.

                            var jobAd = new JobAd
                            {
                                PosterId = jobPoster.Id,
                                Features = _jobAdIntegrationQuery.GetDefaultFeatures(),
                                FeatureBoost = _jobAdIntegrationQuery.GetDefaultFeatureBoost(),
                                Integration =
                                {
                                    IntegratorUserId = integratorUser.Id,
                                    IntegratorReferenceId = postId,
                                },
                            };

                            mapper.ApplyPostData(post, jobAd);

                            if (_externaljobAdsCommand.CanCreateJobAd(jobAd))
                            {
                                _jobAdsCommand.CreateJobAd(jobAd);
                                _jobAdsCommand.OpenJobAd(jobAd);
                                report.Posted++;
                            }
                            else
                            {
                                report.Duplicates++;
                            }
                        }
                    }
                    else
                    {
                        if (mapper.IsIgnored(post))
                        {
                            _jobAdsCommand.CloseJobAd(existingJobAd);
                            report.Closed++;
                        }
                        else
                        {
                            if (_jobAdsCommand.CanBeOpened(existingJobAd))
                            {
                                existingJobAd.Features = _jobAdIntegrationQuery.GetDefaultFeatures();
                                existingJobAd.FeatureBoost = _jobAdIntegrationQuery.GetDefaultFeatureBoost();
                                mapper.ApplyPostData(post, existingJobAd);
                                _jobAdsCommand.UpdateJobAd(existingJobAd);
                                _jobAdsCommand.OpenJobAd(existingJobAd);
                                oldJobAdIds.Remove(existingJobAd.Id);
                                report.Updated++;
                            }
                            else
                            {
                                report.Ignored++;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    report.Failed++;
                    #region Log
                    _logger.Raise(Event.Error, method, "Error processing the job post.", e, new StandardErrorHandler(), Event.Arg("postingId", postId));
                    #endregion
                }

                #region Log
                count++;
                if (timer.ElapsedMilliseconds > _progressInterval)
                {
                    _logger.Raise(Event.Information, method, string.Format("{0} job post rows processed.", count));
                    timer.Reset();
                    timer.Start();
                }
                #endregion
            }

            if (report.Updated != 0)
            {
                // Close job ads in the snapshot which were not updated.

                #region Log

                _logger.Raise(Event.Information, method, string.Format("Closing {0} job ads...", oldJobAdIds.Count));

                #endregion

                var jobAds = _jobAdsQuery.GetJobAds<JobAd>(oldJobAdIds);
                foreach (var jobAd in jobAds)
                    _jobAdsCommand.CloseJobAd(jobAd);
                report.Closed += oldJobAdIds.Count;
            }
            else
            {
                #region Log

                _logger.Raise(Event.Information, method, "No job ads were updated, therefore skipping close job ads portion to preserve jobads...");

                #endregion
            }

            ReportResults(integratorUser, jobPoster, report);
        }

        private HashSet<Guid> GetOpenJobAdsSnapshot(Guid integratorUserId, Guid jobPosterId)
        {
            return new HashSet<Guid>(_jobAdIntegrationQuery.GetOpenJobAdIds(integratorUserId, jobPosterId));
        }

        private void ReportResults(IntegratorUser integratorUser, IHasId<Guid> jobPoster, Report report)
        {
            const string method = "ReportResults";

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportPostEvent
            {
                Success = true,
                IntegratorUserId = integratorUser.Id,
                PosterId = jobPoster.Id,
                JobAds = report.JobAds,
                Closed = report.Closed,
                Duplicates = report.Duplicates,
                Failed = report.Failed,
                Posted = report.Posted,
                Updated = report.Updated,
                Ignored = report.Ignored,
            });

            var message = string.Format("Processing complete. {0} job ads created, {1} updated,"
                + " {2} closed, {3} failed, {4} were ignored as duplicates of those from another source,"
                    + " {5} were ignored as coming from the ignored source.",
                report.Posted, report.Updated, report.Closed, report.Failed, report.Duplicates, report.Ignored);

            _logger.Raise(Event.Information, method, message);
        }

        private class Report
        {
            public int JobAds { get; set; }
            public int Posted { get; set; }
            public int Ignored { get; set; }
            public int Duplicates { get; set; }
            public int Closed { get; set; }
            public int Updated { get; set; }
            public int Failed { get; set; }
        }
    }
}