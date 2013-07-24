using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders
{
    public abstract class Application
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [IsSet]
        public Guid PositionId { get; set; }
        [IsSet]
        public Guid ApplicantId { get; set; }
    }
}