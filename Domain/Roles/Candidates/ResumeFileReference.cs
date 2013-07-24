using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Candidates
{
    public class ResumeFileReference
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid FileReferenceId { get; set; }
        [DefaultNow]
        public DateTime LastUsedTime { get; set; }
        [DefaultNow]
        public DateTime UploadedTime { get; set; }
    }
}
