using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.External.MyCareer.Queries;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;
using Microsoft.VisualBasic.FileIO;

namespace LinkMe.TaskRunner.Tasks.MyCareer
{
    public class JobAdsPollerTask : Task
    {
        private static readonly EventSource<JobAdsPollerTask> EventSource = new EventSource<JobAdsPollerTask>();
        private const string JobPosterLoginId = "MyCareer-jobs";

        private readonly LocationMapper _locationMapper;
        private readonly SalaryParser _salaryParser = new SalaryParser();

        private readonly IMyCareerQuery _myCareerQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;
        private readonly IExternalJobAdsCommand _externalJobAdsCommand;
        private int _progressInterval; // in milliseconds

        public string RemoteFileUrl { get; set; }

        public JobAdsPollerTask(IMyCareerQuery myCareerQuery, IEmployersQuery employersQuery, ILoginCredentialsQuery loginCredentialsQuery, IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, ILocationQuery locationQuery)
            : base(EventSource)
        {
            _myCareerQuery = myCareerQuery;
            _employersQuery = employersQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
            _externalJobAdsCommand = externalJobAdsCommand;
            _locationMapper = new LocationMapper(locationQuery);

            _progressInterval = 5*60*1000; // default = 5 min.
        }

        public TimeSpan ProgressInterval
        {
            get { return TimeSpan.FromMilliseconds(_progressInterval); }
            set { _progressInterval = (int)value.TotalMilliseconds; }
        }

        public override void ExecuteTask()
        {
            if (string.IsNullOrEmpty(RemoteFileUrl))
                throw new InvalidOperationException("RemoteFileUrl property is not set.");

            ExecuteTaskCore(RemoteFileUrl);
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";

            switch (args.Length)
            {
                case 1:
                    ExecuteTaskCore(args[0]);
                    break;

                default:
                    EventSource.Raise(Event.Error, method, string.Format("Syntax:\r\n{0} <filePath>"
                        + "\r\n OR\r\n{0} <remoteFileUrl>",
                        Assembly.GetEntryAssembly().GetName().Name));
                    break;
            }
        }

        private void ExecuteTaskCore(string url)
        {
            const string method = "ExecuteTaskCore";
            EventSource.Raise(Event.Information, method, "Initializing...");

            var stats = new Stats();

            var integrator = GetIntegrator();
            var jobPoster = GetJobPoster();
            var oldJobAdIds = new HashSet<Guid>(GetJobAdsSnapshot(integrator.Id, jobPoster.Id));

            EventSource.Raise(Event.Information, method, "Starting the download...");
            var stream = DownloadJobPosts(url);

            #region Log
            var timer = Stopwatch.StartNew();
            var count = 0;
            EventSource.Raise(Event.Information, method, "Processing job posts...");
            #endregion

            string prevAdvertId = null;

            foreach (var post in ParseJobPosts(stream))
            {
                try
                {
                    if (post.AdvertID != prevAdvertId)
                    {
                        var jobAd = _externalJobAdsCommand.GetExistingJobAd(integrator.Id, post.AdvertID);
                        if (jobAd == null)
                        {
                            // Create new job ad.

                            var newJobAd = new JobAd
                            {
                                PosterId = jobPoster.Id,
                                Features = _jobAdIntegrationQuery.GetDefaultFeatures(),
                                FeatureBoost = _jobAdIntegrationQuery.GetDefaultFeatureBoost(),
                                Integration =
                                {
                                    IntegratorUserId = integrator.Id,
                                    IntegratorReferenceId = post.AdvertID,
                                }
                            };
                            ApplyJobPostData(post, newJobAd);

                            if (_externalJobAdsCommand.CanCreateJobAd(newJobAd))
                            {
                                try
                                {
                                    _jobAdsCommand.CreateJobAd(newJobAd);
                                    _jobAdsCommand.OpenJobAd(newJobAd);
                                    stats.NewAds++;
                                }
                                catch (Exception ex)
                                {
                                    stats.FailedAds++;
                                    #region Log
                                    EventSource.Raise(Event.NonCriticalError, method, "Job ad validation failed.", ex, new StandardErrorHandler(), Event.Arg("AdvertID", post.AdvertID));
                                    #endregion
                                }
                            }
                            else
                            {
                                stats.DuplicateAds++;
                            }
                        }
                        else
                        {
                            // Update the existing job ad.

                            ApplyJobPostData(post, jobAd);
                            jobAd.ExpiryTime = DateTime.Now.AddDays(30);

                            try
                            {
                                _jobAdsCommand.UpdateJobAd(jobAd);
                                _jobAdsCommand.OpenJobAd(jobAd);
                                oldJobAdIds.Remove(jobAd.Id);
                                stats.UpdatedAds++;
                            }
                            catch (Exception ex)
                            {
                                stats.FailedAds++;
                                #region Log
                                EventSource.Raise(Event.NonCriticalError, method, "Job ad validation failed.", ex, new StandardErrorHandler(), Event.Arg("AdvertID", post.AdvertID));
                                #endregion
                            }
                        }

                        prevAdvertId = post.AdvertID;
                    }
                }
                catch (Exception e)
                {
                    stats.FailedAds++;
                    #region Log
                    EventSource.Raise(Event.Error, method, "Error processing the job post.",
                        Event.Arg("AdvertID", post.AdvertID), Event.Arg("exception", e));
                    #endregion
                }

                #region Log

                count++;
                if (timer.ElapsedMilliseconds > _progressInterval)
                {
                    EventSource.Raise(Event.Information, method, string.Format("{0} job post rows processed.", count));
                    timer.Reset();
                    timer.Start();
                }
                #endregion
            }

            // Close job ads in the snapshot which were not updated.

            EventSource.Raise(Event.Information, method, string.Format("Closing {0} job ads...", oldJobAdIds.Count));

            var oldJobAds = _jobAdsQuery.GetJobAds<JobAd>(oldJobAdIds);
            foreach (var oldJobAd in oldJobAds)
                _jobAdsCommand.CloseJobAd(oldJobAd);
            stats.ClosedAds = oldJobAdIds.Count;

            ReportResults(stats);
        }

        private static Stream DownloadJobPosts(string url)
        {
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            var zipStream = new ZipInputStream(responseStream);
            var zipEntry = zipStream.GetNextEntry();
            if (zipEntry == null)
                throw new InvalidOperationException("The compressed stream appears to be empty.");

            return zipStream;
        }

        private static IEnumerable<JobPost> ParseJobPosts(Stream stream)
        {
            const string method = "ParseJobPosts";

            var parser = new TextFieldParser(stream);
            parser.SetDelimiters(",");
            parser.ReadLine(); // skip the heading

            while (!parser.EndOfData)
            {
                string[] fields;
                try
                {
                    fields = parser.ReadFields();
                    if (fields.Length == 0)
                        continue;
                }
                catch (MalformedLineException e)
                {
                    EventSource.Raise(Event.Warning, method, "Skipping a malformed line.", e, new StandardErrorHandler());
                    continue;
                }

                if (fields.Length < 16)
                {
                    var allFields = new string[16];
                    fields.CopyTo(allFields, 0);
                    fields = allFields;
                }

                var post = new JobPost
                           {
                               AdvertID = fields[0],
                               RecruiterName = fields[1],
                               JobReference = fields[2],
                               JobTitle = fields[3],
                               JobDescription = fields[4],
                               RecruiterPhone = fields[5],
                               RecruiterFax = fields[6],
                               RecruiterEmail = fields[7],
                               RecruiterContact = fields[8],
                               Salary = fields[9],
                               Location = fields[10],
                               RecruiterURL = fields[11],
                               CVFormat = fields[12],
                               StartDate = fields[13],
                               Duration = fields[14],
                               OpenToRestrictions = fields[15]
                           };

                yield return post;
            }
        }

        private IntegratorUser GetIntegrator()
        {
            return _myCareerQuery.GetIntegratorUser();
        }

        private Employer GetJobPoster()
        {
            var jobPosterId = _loginCredentialsQuery.GetUserId(JobPosterLoginId);
            if (jobPosterId == null)
                throw new InvalidOperationException("There is no job poster with user ID '" + JobPosterLoginId + "'.");
            var jobPoster = _employersQuery.GetEmployer(jobPosterId.Value);
            if (jobPoster == null)
                throw new InvalidOperationException("There is no job poster with user ID '" + JobPosterLoginId + "'.");

            return jobPoster;
        }

        private IEnumerable<Guid> GetJobAdsSnapshot(Guid integratorUserId, Guid jobPosterId)
        {
            var now = DateTime.Now;
            return from j in _jobAdsQuery.GetJobAds<JobAdEntry>(_jobAdIntegrationQuery.GetJobAdIds(integratorUserId, jobPosterId)) where j.ExpiryTime > now select j.Id;
        }

        private void ApplyJobPostData(JobPost post, JobAd jobAd)
        {
            const string method = "ApplyJobPostData";

            jobAd.Integration.ExternalReferenceId = post.JobReference;
            jobAd.Title = post.JobTitle;
            jobAd.Description.Content = post.JobDescription;

            if (jobAd.ContactDetails == null)
                jobAd.ContactDetails = new ContactDetails();

            jobAd.ContactDetails.PhoneNumber = post.RecruiterPhone;
            jobAd.ContactDetails.FaxNumber = post.RecruiterFax;
            jobAd.ContactDetails.EmailAddress = post.RecruiterEmail;
            jobAd.ContactDetails.LastName = post.RecruiterContact;
            jobAd.ContactDetails.CompanyName = post.RecruiterName;

            Salary salary;
            if (!_salaryParser.TryParse(post.Salary, out salary))
            {
                #region Log
                EventSource.Raise(Event.Warning, method, "Unable to parse salary; the default value will be used.",
                    Event.Arg("AdvertID", post.AdvertID), Event.Arg("Salary", post.Salary));
                #endregion
            }
            jobAd.Description.Salary = salary;

            LocationReference locationReference;
            if (!_locationMapper.TryMap(post.Location, out locationReference))
            {
                #region Log
                EventSource.Raise(Event.Warning, method, "Unable to map location; the partial value will be used.",
                    Event.Arg("AdvertID", post.AdvertID), Event.Arg("Location", post.Location));
                #endregion
            }
            jobAd.Description.Location = locationReference;

            jobAd.Integration.ExternalApplyUrl = post.RecruiterURL;
        }

        private static void ReportResults(Stats stats)
        {
            const string method = "ReportResults";

            var message = string.Format("Processing complete. {0} job ads created, {1} updated,"
               + " {2} closed, {3} failed, {4} were ignored as duplicates of those from another source.",
               stats.NewAds, stats.UpdatedAds, stats.ClosedAds, stats.FailedAds, stats.DuplicateAds);

            EventSource.Raise(Event.Information, method, message);
        }

        private class JobPost
        {
            public string AdvertID;
            public string RecruiterName;
            public string JobReference;
            public string JobTitle;
            public string JobDescription;
            public string RecruiterPhone;
            public string RecruiterFax;
            public string RecruiterEmail;
            public string RecruiterContact;
            public string Salary;
            public string Location;
            public string RecruiterURL;
            public string CVFormat;
            public string StartDate;
            public string Duration;
            public string OpenToRestrictions;

        }

        private class Stats
        {
            public int NewAds;
            public int UpdatedAds;
            public int DuplicateAds;
            public int ClosedAds;
            public int FailedAds;
        }
    }
}