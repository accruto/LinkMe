using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobAd
        : JobAdEntry
    {
        private JobAdDescription _description;

        public JobAd()
        {
            _description = new JobAdDescription();
        }

        [Prepare, Validate]
        public JobAdDescription Description
        {
            get { return _description; }
            set { _description = value ?? new JobAdDescription(); }
        }
    }
}