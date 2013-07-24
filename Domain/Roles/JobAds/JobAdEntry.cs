using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobAdVisibilitySettings
    {
        public bool HideContactDetails { get; set; }
        public bool HideCompany { get; set; }
    }

    public class JobAdEntry
        : IJobAd, IHaveContactDetails
    {
        private JobAdIntegration _integration;
        private readonly JobAdVisibilitySettings _visibility;

        private class JobAdApplication
            : IJobAdApplication
        {
            public bool IncludeCoverLetter { get; set; }
            public IList<IApplicationQuestion> Questions { get; set; }
        }

        public JobAdEntry()
        {
            _integration = new JobAdIntegration();
            _visibility = new JobAdVisibilitySettings();
        }

        [DefaultNewGuid]
        public Guid Id { get; set; }

        [IsSet]
        public Guid PosterId { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [DefaultNow]
        public DateTime LastUpdatedTime { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public JobAdStatus Status { get; set; }
        public JobAdFeatures Features { get; set; }
        public JobAdFeatureBoost FeatureBoost { get; set; }

        [PrepareHtml, Required, Html, StringLength(Constants.MaxTitleLength)]
        public string Title { get; set; }

        [Prepare, Validate]
        public ContactDetails ContactDetails { get; set; }

        public JobAdVisibilitySettings Visibility
        {
            get { return _visibility; }
        }

        [Prepare, Validate]
        public JobAdIntegration Integration
        {
            get { return _integration; }
            set { _integration = value ?? new JobAdIntegration(); }
        }

        IJobAdIntegration IJobAd.Integration
        {
            get { return _integration; }
        }

        IJobAdApplication IJobAd.Application
        {
            get
            {
                return new JobAdApplication
                {
                    IncludeCoverLetter = _integration.ApplicationRequirements != null && _integration.ApplicationRequirements.IncludeCoverLetter,
                    Questions = _integration.ApplicationRequirements == null || _integration.ApplicationRequirements.Questions == null
                        ? null
                        : _integration.ApplicationRequirements.Questions.Cast<IApplicationQuestion>().ToList()
                };
            }
        }

        public Guid? LogoId { get; set; }
    }
}
