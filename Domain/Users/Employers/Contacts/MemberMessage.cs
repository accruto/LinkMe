using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Employers.Contacts
{
    [Serializable]
    public abstract class MemberMessage
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [Required, StringLength(Views.Constants.MaxMessageSubjectLength)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [EmailAddress]
        public string From { get; set; }
        public bool SendCopy { get; set; }
        public IList<Guid> AttachmentIds { get; set; }
    }
}