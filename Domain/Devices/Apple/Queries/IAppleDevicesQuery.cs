using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Devices.Apple.Queries
{
    public interface IAppleDevicesQuery
    {
        AppleDevice GetDevice(Guid id);
        AppleDevice GetDevice(string deviceToken);
        IList<AppleDevice> GetDevices(Guid userId);
    }
}
