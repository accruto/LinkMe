using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Employers.Contacts
{
    public class MemberMessageAttachment
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime UploadedTime { get; set; }
        [IsSet]
        public Guid FileReferenceId { get; set; }
    }
}