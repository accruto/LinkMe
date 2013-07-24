using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Devices.Apple
{
    public interface IAppleDevicesRepository
    {
        void CreateUserAppleDevice(AppleDevice userDevice);
        void UpdateUserAppleDevice(AppleDevice userDevice);

        AppleDevice GetUserAppleDevice(Guid id);
        AppleDevice GetUserAppleDevice(string deviceToken);
        IList<AppleDevice> GetUserAppleDevices(Guid userId);

    }
}