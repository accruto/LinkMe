using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.JobAds.Commands
{
    public abstract class JobApplicationPermissionsException
        : PermissionsException
    {
        protected JobApplicationPermissionsException(IUser employer)
            : base(employer)
        {
        }
    }

    public class AddApplicantPermissionsException
        : JobApplicationPermissionsException
    {
        private readonly Guid _jobId;

        public AddApplicantPermissionsException(IUser employer, Guid jobId)
            : base(employer)
        {
            _jobId = jobId;
        }

        public Guid JobId
        {
            get { return _jobId; }
        }
    }

    public class ManageApplicationPermissionsException
        : JobApplicationPermissionsException
    {
        private readonly Guid _applicationId;

        public ManageApplicationPermissionsException(IUser employer, Guid applicationId)
            : base(employer)
        {
            _applicationId = applicationId;
        }

        public Guid ApplicationId
        {
            get { return _applicationId; }
        }
    }
}