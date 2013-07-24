using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Communications.Settings
{
    public class NonMemberSettings
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public bool SuppressSuggestedCandidatesEmails { get; set; }
    }
}
