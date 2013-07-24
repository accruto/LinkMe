using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Devices.Apple
{
    public class AppleDevice
        : IAppleDevice
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        public string DeviceToken { get; set; }
        public bool Active { get; set; }
    }
}
