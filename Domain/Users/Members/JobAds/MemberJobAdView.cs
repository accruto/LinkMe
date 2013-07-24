using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds
{
    public class JobAdIntegrationView
        : IJobAdIntegration
    {
        private readonly JobAd _jobAd;

        internal JobAdIntegrationView(JobAd jobAd)
        {
            _jobAd = jobAd;
        }

        public string ExternalApplyUrl
        {
            get { return _jobAd.Integration.ExternalApplyUrl; }
        }

        public string ExternalApplyApiUrl
        {
            get { return _jobAd.Integration.ExternalApplyApiUrl; }
        }

        public Guid? IntegratorUserId
        {
            get { return _jobAd.Integration.IntegratorUserId; }
        }

        public string ExternalReferenceId
        {
            get { return _jobAd.Integration.ExternalReferenceId; }
        }

        public ApplicationRequirements ApplicationRequirements
        {
            get { return _jobAd.Integration.ApplicationRequirements; }
        }
    }

    public class JobAdDescriptionView
    {
        protected readonly JobAd _jobAd;

        internal JobAdDescriptionView(JobAd jobAd)
        {
            _jobAd = jobAd;
        }

        public LocationReference Location
        {
            get { return _jobAd.Description.Location; }
        }

        public Salary Salary
        {
            get { return _jobAd.Description.Salary; }
        }

        public IList<Industry> Industries
        {
            get { return _jobAd.Description.Industries; }
        }

        public IList<string> BulletPoints
        {
            get { return _jobAd.Description.BulletPoints; }
        }

        public string Content
        {
            get { return _jobAd.Description.Content; }
        }

        public string PositionTitle
        {
            get { return _jobAd.Description.PositionTitle; }
        }

        public JobTypes JobTypes
        {
            get { return _jobAd.Description.JobTypes; }
        }

        public string Package
        {
            get { return _jobAd.Description.Package; }
        }

        public bool ResidencyRequired
        {
            get { return _jobAd.Description.ResidencyRequired; }
        }

        public string Summary
        {
            get { return _jobAd.Description.Summary; }
        }
    }

    public class MemberJobAdDescriptionView
        : JobAdDescriptionView
    {
        private readonly string _companyName;

        internal MemberJobAdDescriptionView(JobAd jobAd, string companyName)
            : base(jobAd)
        {
            _companyName = companyName;
        }

        public string CompanyName
        {
            get { return _jobAd.Visibility.HideCompany ? null : _companyName; }
        }
    }

    public class JobAdApplicationView
        : IJobAdApplication
    {
        private readonly JobAd _jobAd;
        private readonly JobAdProcessing _processing;

        internal JobAdApplicationView(JobAd jobAd, JobAdProcessing processing)
        {
            _jobAd = jobAd;
            _processing = processing;
        }

        public bool IncludeCoverLetter
        {
            get
            {
                // Either it is an internal job ad or it is managed externally and can include a cover letter.

                return _processing == JobAdProcessing.ManagedInternally
                    ||
                    (
                        _processing == JobAdProcessing.ManagedExternally
                        && _jobAd.Integration.ApplicationRequirements != null
                        && _jobAd.Integration.ApplicationRequirements.IncludeCoverLetter
                    );
            }
        }

        public IList<IApplicationQuestion> Questions
        {
            get
            {
                return _jobAd.Integration.ApplicationRequirements == null || _jobAd.Integration.ApplicationRequirements.Questions == null
                    ? null
                    : _jobAd.Integration.ApplicationRequirements.Questions.Cast<IApplicationQuestion>().ToList();
            }
        }
    }

    public class JobAdView
        : IJobAd
    {
        protected readonly JobAd _jobAd;
        private readonly JobAdIntegrationView _integration;
        private readonly JobAdDescriptionView _description;
        private readonly JobAdApplicationView _application;
        private readonly JobAdProcessing _processing;

        public JobAdView(JobAd jobAd, JobAdProcessing processing)
        {
            _jobAd = jobAd;
            _integration = new JobAdIntegrationView(jobAd);
            _description = new JobAdDescriptionView(jobAd);
            _application = new JobAdApplicationView(jobAd, processing);
            _processing = processing;
        }

        public Guid Id
        {
            get { return _jobAd.Id; }
        }

        public Guid PosterId
        {
            get { return _jobAd.PosterId; }
        }

        public DateTime CreatedTime
        {
            get { return _jobAd.CreatedTime; }
        }

        public DateTime? ExpiryTime
        {
            get { return _jobAd.ExpiryTime; }
        }

        public JobAdStatus Status
        {
            get { return _jobAd.Status; }
        }

        public string Title
        {
            get { return _jobAd.Title; }
        }

        public JobAdFeatureBoost FeatureBoost
        {
            get { return _jobAd.FeatureBoost; }
        }

        public JobAdFeatures Features
        {
            get { return _jobAd.Features; }
        }

        public JobAdProcessing Processing
        {
            get { return _processing;  }
        }

        public Guid? LogoId
        {
            get { return _jobAd.LogoId; }
        }

        public JobAdIntegrationView Integration
        {
            get { return _integration; }
        }

        IJobAdIntegration IJobAd.Integration
        {
            get { return _integration; }
        }

        public JobAdDescriptionView Description
        {
            get { return _description; }
        }

        public JobAdApplicationView Application
        {
            get { return _application; }
        }

        IJobAdApplication IJobAd.Application
        {
            get { return _application; }
        }
    }

    public class JobAdApplicantView
    {
        private readonly bool _hasViewed;
        private readonly bool _hasApplied;
        private readonly bool _isFlagged;
        private readonly bool _isInMobileFolder;

        public JobAdApplicantView(bool hasViewed, bool hasApplied, bool isFlagged, bool isInMobileFolder)
        {
            _hasViewed = hasViewed;
            _hasApplied = hasApplied;
            _isFlagged = isFlagged;
            _isInMobileFolder = isInMobileFolder;
        }

        public bool HasViewed
        {
            get { return _hasViewed; }
        }

        public bool HasApplied
        {
            get { return _hasApplied; }
        }

        public bool IsFlagged
        {
            get { return _isFlagged; }
        }

        public bool IsInMobileFolder
        {
            get { return _isInMobileFolder; }
        }
    }

    public class MemberJobAdView
        : JobAdView
    {
        private readonly ContactDetails _contactDetails;
        private readonly MemberJobAdDescriptionView _description;
        private readonly JobAdApplicantView _applicant;

        public MemberJobAdView(JobAd jobAd, JobAdProcessing processing, ContactDetails contactDetails, string companyName, bool hasViewed, bool hasApplied, bool isFlagged, bool isInMobileFolder)
            : base(jobAd, processing)
        {
            _description = new MemberJobAdDescriptionView(jobAd, companyName);
            _applicant = new JobAdApplicantView(hasViewed, hasApplied, isFlagged, isInMobileFolder);
            _contactDetails = contactDetails;
        }

        public ContactDetails ContactDetails
        {
            get
            {
                // Check whether contact details are hidden.

                if (_contactDetails == null || _jobAd.Visibility.HideContactDetails)
                    return null;

                // Check whether the company name needs to be hidden.

                if (!_jobAd.Visibility.HideCompany)
                    return _contactDetails;

                // Construct a new contact details without the company.

                var contactDetails = new ContactDetails
                {
                    CompanyName = null,
                    EmailAddress = _contactDetails.EmailAddress,
                    FaxNumber = _contactDetails.FaxNumber,
                    FirstName = _contactDetails.FirstName,
                    Id = _contactDetails.Id,
                    LastName = _contactDetails.LastName,
                    PhoneNumber = _contactDetails.PhoneNumber,
                    SecondaryEmailAddresses = _contactDetails.SecondaryEmailAddresses,
                };

                // If the company name was the only thing set then return null.

                return contactDetails.IsEmpty ? null : contactDetails;
            }
        }

        public new MemberJobAdDescriptionView Description
        {
            get { return _description; }
        }

        public JobAdApplicantView Applicant
        {
            get { return _applicant; }
        }
    }
}
