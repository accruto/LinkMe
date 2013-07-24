using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobAdIntegration
        : IJobAdIntegration
    {
        public Guid? IntegratorUserId { get; set; }
        [StringLength(Constants.MaxIntegratorReferenceIdLength)]
        public string IntegratorReferenceId { get; set; }
        [StringLength(Constants.MaxExternalReferenceIdLength)]
        public string ExternalReferenceId { get; set; }
        [StringLength(Constants.MaxExternalApplyUrlLength)]
        public string ExternalApplyUrl { get; set; }
        [StringLength(Constants.MaxExternalApplyUrlLength)]
        public string ExternalApplyApiUrl { get; set; }

        public string JobBoardId { get; set; }
        public ApplicationRequirements ApplicationRequirements { get; set; }
    }
}