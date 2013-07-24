using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders
{
    public class InternalApplication
        : Application
    {
        public bool IsPending { get; set; }

        [StringLength(Constants.CoverLetterTextMaxLength)]
        public string CoverLetterText { get; set; }
        public string ApplicantEmail { get; set; }

        public Guid? ResumeId { get; set; }
        public Guid? ResumeFileId { get; set; }

        public bool IsPositionFeatured { get; set; }
    }
}