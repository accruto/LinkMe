using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Recruiters
{
    public class VerifiedOrganisation
        : Organisation, IHaveContactDetails
    {
        public Guid VerifiedById { get; set; }
        public Guid AccountManagerId { get; set; }
        [Prepare, Validate]
        public ContactDetails ContactDetails { get; set; }

        public override bool IsVerified
        {
            get { return true; }
        }
    }
}