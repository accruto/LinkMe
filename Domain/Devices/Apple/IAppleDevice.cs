using System;

namespace LinkMe.Domain.Devices.Apple
{
    public interface IAppleDevice
    {
        Guid Id { get; set; }
        string DeviceToken { get; set; }
        bool Active { get; set; }
    }
}
