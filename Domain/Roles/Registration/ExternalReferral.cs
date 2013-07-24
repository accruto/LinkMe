using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Registration
{
    public class ExternalReferral
    {
        [IsSet]
        public Guid UserId { get; set; }
        public int SourceId { get; set; }
    }
}
