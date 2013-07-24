using System;
using System.Collections.Generic;
using System.ServiceModel;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.Security;
using LinkMe.Common;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using LinkMe.Utility;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Integration.JobG8
{
    [ServiceBehavior(Namespace = "http://www.linkme.com.au/integration/jobg8")]
    public class AdvertPostService : IAdvertPostService
    {
        #region Private fields

        private static readonly EventSource<AdvertPostService> EventSource = new EventSource<AdvertPostService>();

        private readonly string _jobPosterUserId;
        private readonly LocationMapper _locationMapper;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;
        private readonly IExternalJobAdsCommand _externalJobAdsCommand;
        private readonly IEmployersQuery _employersQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IServiceAuthenticationManager _serviceAuthenticationManager;
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand;

        #endregion

        #region Nested classes

        private class PostAdvertReport
        {
            public int JobAds { get; set; }
            public int Failed { get; set; }
            public int Posted { get; set; }
            public int Duplicates { get; set; }
        }

        private class AmendAdvertReport
        {
            public int JobAds { get; set; }
            public int Failed { get; set; }
            public int Updated { get; set; }
        }

        private class DeleteAdvertReport
        {
            public int JobAds { get; set; }
            public int Failed { get; set; }
            public int NotFound { get; set; }
            public int Closed { get; set; }
        }

        #endregion

        [InjectionConstructor]
        public AdvertPostService([Dependency("linkme.integration.jobg8.jobPoster")] string jobPosterUserId, IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IEmployersQuery employersQuery, ILoginCredentialsQuery loginCredentialsQuery, IServiceAuthenticationManager serviceAuthenticationManager, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand)
        {
            _jobPosterUserId = jobPosterUserId;
            _locationMapper = new LocationMapper(locationQuery);
            _industriesQuery = industriesQuery;
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
            _externalJobAdsCommand = externalJobAdsCommand;
            _employersQuery = employersQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
            _serviceAuthenticationManager = serviceAuthenticationManager;
            _jobAdIntegrationReportsCommand = jobAdIntegrationReportsCommand;
        }
            
        #region IAdvertPostService Members

        public PostAdvertResponseMessage PostAdvert(PostAdvertRequestMessage request)
        {
            const string method = "PostAdvert";
            EventSource.Raise(Event.Information, method, Event.Arg("request", request));

            IntegratorUser integratorUser;
            IEmployer jobPoster;
            CheckUser(request.UserCredentials, out integratorUser, out jobPoster);

            var errors = new List<string>();
            var report = new PostAdvertReport();

            foreach (var postAd in request.PostAdvert.Adverts.PostAdvert)
            {
                report.JobAds++;

                try
                {
                    if (_jobAdIntegrationQuery.GetOpenJobAdIds(integratorUser.Id, postAd.JobReference).Count != 0)
                    {
                        // Already exists as a JobG8 ad

                        var message = string.Format("Job ad to be posted, '{0}', already exists.", postAd.JobReference);
                        EventSource.Raise(Event.Error, method, message, Event.Arg("postAd", postAd));
                        errors.Add(message);
                        report.Duplicates++;
                    }
                    else
                    {
                        var jobAd = MapJobAd(postAd);
                        jobAd.PosterId = jobPoster.Id;
                        jobAd.Features = _jobAdIntegrationQuery.GetDefaultFeatures();
                        jobAd.FeatureBoost = _jobAdIntegrationQuery.GetDefaultFeatureBoost();
                        jobAd.Integration.IntegratorUserId = integratorUser.Id;

                        if (_externalJobAdsCommand.CanCreateJobAd(jobAd))
                        {
                            _jobAdsCommand.CreateJobAd(jobAd);

                            // The application form, for now, needs to be directly saved into the database.

                            _jobAdsCommand.CreateApplicationRequirements(jobAd.Id, postAd.ApplicationFormXML);
                            _jobAdsCommand.OpenJobAd(jobAd);

                            report.Posted++;
                        }
                        else
                        {
                            var message = string.Format("Job ad to be posted, '{0}' ([{1}] '{2}')," + " was already posted by another integrator.", postAd.JobReference, postAd.ClientReference, postAd.Position);
                            EventSource.Raise(Event.Error, method, message, Event.Arg("postAd", postAd));
                            errors.Add(message);
                        }
                    }
                }
                catch (ServiceEndUserException e)
                {
                    EventSource.Raise(Event.Error, method, e, null, Event.Arg("postAd", postAd));
                    errors.Add(string.Format("JobReference='{0}': {1}", postAd.JobReference, e.Message));
                    report.Failed++;
                }
                catch (Exception e)
                {
                    EventSource.Raise(Event.Error, method, e, null, Event.Arg("postAd", postAd));
                    errors.Add(string.Format("JobReference='{0}': Unexpected error.", postAd.JobReference));
                    report.Failed++;
                }
            }

            // Record it.

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportPostEvent { Success = true, IntegratorUserId = integratorUser.Id, PosterId = jobPoster.Id, JobAds = report.JobAds, Failed = report.Failed, Posted = report.Posted, Duplicates = report.Duplicates });

            var response = new PostAdvertResponseMessage
            {
                PostAdvertResponse = new Response { Success = string.Join("\r\n", errors.ToArray()) }
            };

            EventSource.Raise(Event.Information, method, string.Format("{0} ads processed; {1} new ads, {2} duplicate ads, {3} errors.", report.JobAds, report.Posted, report.Duplicates, report.Failed), Event.Arg("response", response));
            return response;
        }

        public AmendAdvertResponseMessage AmendAdvert(AmendAdvertRequestMessage request)
        {
            const string method = "AmendAdvert";
            EventSource.Raise(Event.Information, method, Event.Arg("request", request));

            IntegratorUser integratorUser;
            IEmployer jobPoster;
            CheckUser(request.UserCredentials, out integratorUser, out jobPoster);

            var errors = new List<string>();
            var report = new AmendAdvertReport();

            foreach (var amendAd in request.AmendAdvert.Adverts.AmendAdvert)
            {
                report.JobAds++;

                try
                {
                    var jobAd = _externalJobAdsCommand.GetExistingJobAd(integratorUser.Id, amendAd.JobReference);
                    if (jobAd != null)
                    {
                        MapJobAd(amendAd, jobAd);

                        // Make sure the job is open.

                        _jobAdsCommand.UpdateJobAd(jobAd);
                        _jobAdsCommand.OpenJobAd(jobAd);

                        report.Updated++;
                    }
                    else
                    {
                        EventSource.Raise(Event.Warning, method, "Job ad not found. No action has been taken.", Event.Arg("amendAd", amendAd));
                    }
                }
                catch (ServiceEndUserException e)
                {
                    EventSource.Raise(Event.Error, method, e, null, Event.Arg("amendAd", amendAd));
                    errors.Add(string.Format("JobReference='{0}': {1}", amendAd.JobReference, e.Message));
                    report.Failed++;
                }
                catch (Exception e)
                {
                    EventSource.Raise(Event.Error, method, e, null, Event.Arg("amendAd", amendAd));
                    errors.Add(string.Format("JobReference='{0}': Unexpected error.", amendAd.JobReference));
                    report.Failed++;
                }
            }

            // Record it.

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportPostEvent { Success = true, IntegratorUserId = integratorUser.Id, PosterId = jobPoster.Id, JobAds = report.JobAds, Closed = 0, Failed = report.Failed, Posted = 0, Updated = report.Updated });

            var response = new AmendAdvertResponseMessage
            {
                AmendAdvertResponse = new Response { Success = string.Join("\r\n", errors.ToArray()) }
            };

            EventSource.Raise(Event.Information, method, Event.Arg("response", response));
            return response;
        }

        public DeleteAdvertResponseMessage DeleteAdvert(DeleteAdvertRequestMessage request)
        {
            const string method = "DeleteAdvert";
            EventSource.Raise(Event.Information, method, Event.Arg("request", request));

            IntegratorUser integratorUser;
            IEmployer jobPoster;
            CheckUser(request.UserCredentials, out integratorUser, out jobPoster);

            var errors = new List<string>();
            var report = new DeleteAdvertReport();

            foreach (var deleteAd in request.DeleteAdvert.Adverts.DeleteAdvert)
            {
                report.JobAds++;

                try
                {
                    var ids = _jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, deleteAd.JobReference);
                    var jobAds = _jobAdsQuery.GetJobAds<JobAd>(ids);
                    foreach (var jobAd in jobAds)
                        _jobAdsCommand.CloseJobAd(jobAd);
                    var closed = ids.Count;

                    if (closed > 0)
                    {
                        report.Closed += closed;
                    }
                    else
                    {
                        var message = string.Format("Job ad to be deleted, '{0}', was not found.", deleteAd.JobReference);
                        EventSource.Raise(Event.Error, method, message, Event.Arg("deleteAd", deleteAd));
                        errors.Add(message);
                        report.NotFound++;
                    }
                }
                catch (ServiceEndUserException e)
                {
                    EventSource.Raise(Event.Error, method, e, null, Event.Arg("postAd", deleteAd));
                    errors.Add(string.Format("JobReference='{0}': {1}", deleteAd.JobReference, e.Message));
                    report.Failed++;
                }
                catch (Exception e)
                {
                    EventSource.Raise(Event.Error, method, e, null, Event.Arg("postAd", deleteAd));
                    errors.Add(string.Format("JobReference='{0}': Unexpected error.", deleteAd.JobReference));
                    report.Failed++;
                }
            }

            // Record it.

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportCloseEvent { IntegratorUserId = integratorUser.Id, Success = true, JobAds = report.JobAds, Closed = report.Closed, Failed = report.Failed, NotFound = report.NotFound });

            var response = new DeleteAdvertResponseMessage
            {
                DeleteAdvertResponse = new Response { Success = string.Join("\r\n", errors.ToArray()) }
            };

            EventSource.Raise(Event.Information, method, string.Format("{0} ads processed; {1} ads deleted, {2} ads not found, {3} errors.", report.JobAds, report.Closed, report.NotFound, report.Failed), Event.Arg("response", response));
            return response;
        }

        #endregion

        private void CheckUser(Credentials credentials, out IntegratorUser integratorUser, out IEmployer jobPoster)
        {
            const string method = "CheckUser";

            try
            {
                if (credentials == null)
                    throw new ServiceEndUserException("UserCredentials are not specified.");

                integratorUser = _serviceAuthenticationManager.AuthenticateRequest(credentials.Username, credentials.Password, IntegratorPermissions.PostJobAds);

                jobPoster = GetJobPoster(_jobPosterUserId);
            }
            catch (ServiceEndUserException e)
            {
                var ex = new FaultException(e.Message);
                EventSource.Raise(Event.Error, method, ex, null);
                throw ex;
            }
            catch (UserException e)
            {
                var ex = new FaultException(e.Message);
                EventSource.Raise(Event.Error, method, ex, null);
                throw ex;
            }
            catch (Exception e)
            {
                EventSource.Raise(Event.Error, method, e, null);
                throw;
            }
        }

        private Employer GetJobPoster(string jobPosterUserId)
        {
            if (string.IsNullOrEmpty(jobPosterUserId))
                throw new ServiceEndUserException("The job poster user ID has not been specified.");

            var jobPosterId = _loginCredentialsQuery.GetUserId(jobPosterUserId);
            if (jobPosterId == null)
                throw new ServiceEndUserException("There is no job poster with user ID '" + jobPosterUserId + "'.");
            var jobPoster = _employersQuery.GetEmployer(jobPosterId.Value);
            if (jobPoster == null)
                throw new ServiceEndUserException("There is no job poster with user ID '" + jobPosterUserId + "'.");

            return jobPoster;
        }

        private JobAd MapJobAd(PostAdvert postAd)
        {
            var workHours = postAd.WorkHoursSpecified? postAd.WorkHours : (WorkHours?)null;
            var pay = postAd.PayAmountSpecified? postAd.PayAmount : (decimal?)null;
            var payMin = postAd.PayMinimumSpecified ? postAd.PayMinimum : (decimal?)null;
            var payMax = postAd.PayMaximumSpecified ? postAd.PayMaximum : (decimal?)null;

            var jobAd = new JobAd
            {
                Integration = 
                {
                    IntegratorReferenceId = postAd.JobReference,
                    ExternalReferenceId = postAd.ClientReference,
                    ExternalApplyUrl = postAd.RedirectionUrl,
                },
                Title = postAd.Position,
                ContactDetails = new ContactDetails
                {
                    LastName = TextUtil.Truncate(postAd.Contact, DomainConstants.PersonNameMaxLength),
                    PhoneNumber = postAd.Telephone,
                    CompanyName = postAd.AdvertiserName,
                },
                Description =
                {
                    Industries = new List<Industry> { _industriesQuery.GetIndustry(postAd.Classification) },
                    PositionTitle = (postAd.SubClassification == "Other" ? null : postAd.SubClassification),
                    Content = postAd.Description,
                    JobTypes = MapJobTypes(postAd.EmploymentType, workHours),
                    ResidencyRequired = (postAd.VisaRequired == VisaRequired.MustBeEligible),
                    Salary = MapSalary(postAd.PayPeriod, pay, payMin, payMax),
                    Location = _locationMapper.Map(postAd.Country, postAd.Location, postAd.Area, postAd.PostCode),
                },
            };
            return jobAd;
        }

        private void MapJobAd(AmendAdvert amendAd, JobAd jobAd)
        {
            var workHours = amendAd.WorkHoursSpecified ? amendAd.WorkHours : (WorkHours?)null;
            var pay = amendAd.PayAmountSpecified ? amendAd.PayAmount : (decimal?)null;
            var payMin = amendAd.PayMinimumSpecified ? amendAd.PayMinimum : (decimal?)null;
            var payMax = amendAd.PayMaximumSpecified ? amendAd.PayMaximum : (decimal?)null;

            jobAd.Title = amendAd.Position;
            jobAd.ContactDetails = new ContactDetails
            {
                LastName = TextUtil.Truncate(amendAd.Contact, DomainConstants.PersonNameMaxLength),
                PhoneNumber = amendAd.Telephone
            };
            jobAd.Description.Content = amendAd.Description;
            jobAd.Description.JobTypes = MapJobTypes(amendAd.EmploymentType, workHours);
            jobAd.Description.ResidencyRequired = (amendAd.VisaRequired == VisaRequired.MustBeEligible);
            jobAd.Description.Salary = MapSalary(amendAd.PayPeriod, pay, payMin, payMax);
            jobAd.Description.Location = _locationMapper.Map(amendAd.Country, amendAd.Location, amendAd.Area, amendAd.PostCode);
        }

        private static JobTypes MapJobTypes(EmploymentType employmentType, WorkHours? workHours)
        {
            var jobTypes = JobTypes.None;

            switch (employmentType)
            {
                case EmploymentType.Contract:
                    jobTypes |= JobTypes.Contract;
                    break;

                case EmploymentType.Temporary:
                    jobTypes |= JobTypes.Temp;
                    break;
            }

            if (workHours != null)
            {
                switch (workHours)
                {
                    case WorkHours.PartTime:
                        jobTypes |= JobTypes.PartTime;
                        break;

                    case WorkHours.FullTime:
                        jobTypes |= JobTypes.FullTime;
                        break;
                }
            }

            if (jobTypes == JobTypes.None)
                jobTypes = JobTypes.FullTime;

            return jobTypes;
        }

        private static Salary MapSalary(PayPeriod? payPeriod, decimal? pay, decimal? payMin, decimal? payMax)
        {
            var rateType = SalaryRate.Year;

            //Handle null payperiods and default to yearly.
            if (payPeriod.HasValue)
            {
                switch (payPeriod)
                {
                    case PayPeriod.Day:
                        rateType = SalaryRate.Day;
                        break;

                    case PayPeriod.Hourly:
                        rateType = SalaryRate.Hour;
                        break;

                    case PayPeriod.Monthly:
                        rateType = SalaryRate.Month;
                        break;

                    case PayPeriod.Weekly:
                        rateType = SalaryRate.Week;
                        break;
                }
            }

            decimal? lowerBoundary = null;
            if (payMin.HasValue)
                lowerBoundary = payMin.Value;
            else if (pay.HasValue)
                lowerBoundary = pay.Value;

            decimal? upperBoundary = null;
            if (payMax.HasValue)
                upperBoundary = payMax.Value;

            return new Salary {LowerBound = lowerBoundary, UpperBound = upperBoundary, Rate = rateType, Currency = Currency.AUD};
        }
    }
}
