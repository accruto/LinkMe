using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public class JobAdsCommand
        : IJobAdsCommand
    {
        private readonly IJobAdsRepository _repository;
        private readonly int _expiryDurationDays;
        private readonly int _extendedExpiryDurationDays;

        private class IndustryEqualityComparer
            : IEqualityComparer<Industry>
        {
            public bool Equals(Industry x, Industry y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Industry i)
            {
                return i.GetHashCode();
            }
        }

        private static readonly IndustryEqualityComparer IndustryComparer = new IndustryEqualityComparer();

        public JobAdsCommand(IJobAdsRepository repository, int expiryDurationDays, int extendedExpiryDurationDays)
        {
            _repository = repository;
            _expiryDurationDays = expiryDurationDays;
            _extendedExpiryDurationDays = extendedExpiryDurationDays;
        }

        T IJobAdsCommand.GetJobAd<T>(Guid id)
        {
            return _repository.GetJobAd<T>(id);
        }

        void IJobAdsCommand.CreateJobAd(JobAd jobAd)
        {
            PrepareCreate(jobAd);
            Validate(jobAd);
            _repository.CreateJobAd(jobAd);
        }

        void IJobAdsCommand.UpdateJobAd(JobAd jobAd)
        {
            // Ensure that the status is not changed.

            var existingJobAd = _repository.GetJobAd<JobAdEntry>(jobAd.Id);
            jobAd.Status = existingJobAd.Status;
           
            PrepareUpdate(jobAd);
            Validate(jobAd);
            _repository.UpdateJobAd(jobAd);

            // Fire events.

            var handlers = JobAdUpdated;
            if (handlers != null)
                handlers(this, new JobAdEventArgs(jobAd.Id));
        }

        bool IJobAdsCommand.CanBeOpened(JobAdEntry jobAd)
        {
            return jobAd.Status == JobAdStatus.Open || CanBeOpened(jobAd);
        }

        void IJobAdsCommand.OpenJobAd(JobAd jobAd)
        {
            if (jobAd.Status == JobAdStatus.Open)
                return;
            if (!CanBeOpened(jobAd))
                throw new InvalidOperationException(string.Format("Cannot open the '{0}' job ad, status: {1}, expiry time: {2}.", jobAd.Id, jobAd.Status, jobAd.ExpiryTime));

            var previousStatus = jobAd.Status;

            // If it requires refreshes set it up.

            if (jobAd.Features.IsFlagSet(JobAdFeatures.Refresh))
                _repository.CreateRefresh(jobAd.Id, DateTime.Now);

            // If the expiry time is not explicitly set then set it now, or if it is set for the past.

            var newExpiryTime = jobAd.ExpiryTime == null || jobAd.ExpiryTime.Value < DateTime.Now
                ? RoundExpiryTime(GetDefaultExpiryTime(jobAd.Features))
                : null;

            _repository.ChangeStatus(jobAd.Id, JobAdStatus.Open, newExpiryTime, DateTime.Now);

            jobAd.Status = JobAdStatus.Open;
            if (newExpiryTime != null)
                jobAd.ExpiryTime = newExpiryTime.Value;

            // Fire events.

            var handlers = JobAdOpened;
            if (handlers != null)
                handlers(this, new JobAdOpenedEventArgs(jobAd.Id, previousStatus));
        }

        void IJobAdsCommand.DeleteJobAd(JobAdEntry jobAd)
        {
            if (jobAd.Status == JobAdStatus.Deleted)
                return;
            if (!CanBeDeleted(jobAd))
                throw new InvalidOperationException(string.Format("Cannot delete the '{0}' job ad.", jobAd.Id));

            _repository.DeleteRefresh(jobAd.Id);
            _repository.ChangeStatus(jobAd.Id, JobAdStatus.Deleted, null, DateTime.Now);
            jobAd.Status = JobAdStatus.Deleted;
        }

        bool IJobAdsCommand.CanBeClosed(JobAdEntry jobAd)
        {
            return jobAd.Status == JobAdStatus.Open || CanBeClosed(jobAd);
        }

        void IJobAdsCommand.CloseJobAd(JobAdEntry jobAd)
        {
            if (jobAd.Status == JobAdStatus.Closed)
                return;
            if (!CanBeClosed(jobAd))
                throw new InvalidOperationException(string.Format("Cannot close the '{0}' job ad.", jobAd.Id));

            _repository.DeleteRefresh(jobAd.Id);
            _repository.ChangeStatus(jobAd.Id, JobAdStatus.Closed, null, DateTime.Now);
            jobAd.Status = JobAdStatus.Closed;

            // Fire events.

            var handlers = JobAdClosed;
            if (handlers != null)
                handlers(this, new JobAdClosedEventArgs(jobAd.Id, jobAd.PosterId));
        }

        void IJobAdsCommand.TransferJobAd(Guid toPosterId, JobAdEntry jobAd)
        {
            if (toPosterId == jobAd.PosterId)
                return;
            if (!CanBeTransferred(jobAd))
                throw new InvalidOperationException(string.Format("Cannot transfer the '{0}' job ad.", jobAd.Id));

            _repository.TransferJobAd(toPosterId, jobAd.Id);
            jobAd.PosterId = toPosterId;
        }

        void IJobAdsCommand.RefreshJobAd(JobAdEntry jobAd)
        {
            if (!CanBeRefreshed(jobAd))
                throw new InvalidOperationException(string.Format("Cannot refresh the '{0}' job ad.", jobAd.Id));

            _repository.UpdateRefresh(jobAd.Id, DateTime.Now);

            jobAd.LastUpdatedTime = DateTime.Now;
            _repository.UpdateJobAd(jobAd);

            // Fire events.

            var handlers = JobAdUpdated;
            if (handlers != null)
                handlers(this, new JobAdEventArgs(jobAd.Id));
        }

        DateTime IJobAdsCommand.GetDefaultExpiryTime(JobAdFeatures features)
        {
            return GetDefaultExpiryTime(features);
        }

        Guid? IJobAdsCommand.GetLastUsedLogoId(Guid posterId)
        {
            return _repository.GetLastUsedLogoId(posterId);
        }

        void IJobAdsCommand.CreateApplicationRequirements(Guid jobAdId, string requirementsXml)
        {
            _repository.CreateApplicationRequirements(jobAdId, requirementsXml);
        }

        [Publishes(PublishedEvents.JobAdOpened)]
        public event EventHandler<JobAdOpenedEventArgs> JobAdOpened;

        [Publishes(PublishedEvents.JobAdUpdated)]
        public event EventHandler<JobAdEventArgs> JobAdUpdated;

        [Publishes(PublishedEvents.JobAdClosed)]
        public event EventHandler<JobAdClosedEventArgs> JobAdClosed;

        private static bool CanBeOpened(JobAdEntry jobAd)
        {
            return jobAd.Status == JobAdStatus.Draft
                || (jobAd.Status == JobAdStatus.Closed && jobAd.ExpiryTime > DateTime.Now);
        }

        private static bool CanBeClosed(JobAdEntry jobAd)
        {
            return jobAd.Status == JobAdStatus.Open;
        }

        private static bool CanBeDeleted(JobAdEntry jobAd)
        {
            return jobAd.Status != JobAdStatus.Open;
        }

        private static bool CanBeTransferred(JobAdEntry jobAd)
        {
            return jobAd.Status == JobAdStatus.Draft;
        }

        private static bool CanBeRefreshed(JobAdEntry jobAd)
        {
            return jobAd.Status == JobAdStatus.Open
                && jobAd.Features.IsFlagSet(JobAdFeatures.Refresh);
        }

        private static void PrepareCreate(JobAd jobAd)
        {
            jobAd.Prepare();
            Prepare(jobAd);

            // Set the status to indicate that all job ads start as a draft.

            jobAd.Status = JobAdStatus.Draft;
            jobAd.ExpiryTime = RoundExpiryTime(jobAd.ExpiryTime);
        }

        private static void PrepareUpdate(JobAd jobAd)
        {
            Prepare(jobAd);

            jobAd.LastUpdatedTime = DateTime.Now;
            jobAd.ExpiryTime = RoundExpiryTime(jobAd.ExpiryTime);
        }

        private static void Prepare(JobAd jobAd)
        {
            // Round the creation time to the second.

            jobAd.CreatedTime = new DateTime(jobAd.CreatedTime.Year, jobAd.CreatedTime.Month, jobAd.CreatedTime.Day, jobAd.CreatedTime.Hour, jobAd.CreatedTime.Minute, jobAd.CreatedTime.Second);

            // Remove duplicate industries, and ensure they are not null.

            if (jobAd.Description.Industries != null)
            {
                jobAd.Description.Industries = (from i in jobAd.Description.Industries
                                                where i != null
                                                select i).Distinct(IndustryComparer).ToList();
                if (jobAd.Description.Industries.Count == 0)
                    jobAd.Description.Industries = null;
            }
    
            // Prepare these classes because they may have been added new.

            if (jobAd.Description.Salary != null)
                jobAd.Description.Salary.Prepare();
            if (jobAd.Description.ParsedSalary != null)
                jobAd.Description.ParsedSalary.Prepare();
            if (jobAd.ContactDetails != null)
                jobAd.ContactDetails.Prepare();
            if (jobAd.Description.Location != null)
                jobAd.Description.Location.Prepare();
        }

        private static DateTime? RoundExpiryTime(DateTime? dt)
        {
            // Expire at the end of the specified day.

            return dt == null
                ? (DateTime?) null
                : dt.Value.Date.AddDays(1).AddSeconds(-1);
        }

        private DateTime GetDefaultExpiryTime(JobAdFeatures features)
        {
            return DateTime.Now.Date.AddDays(features.IsFlagSet(JobAdFeatures.ExtendedExpiry) ? _extendedExpiryDurationDays : _expiryDurationDays);
        }

        private static void Validate(JobAd jobAd)
        {
            jobAd.Validate();
        }
    }
}