using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Devices.Apple.Queries
{
    public class AppleDevicesQuery
        : IAppleDevicesQuery
    {
        private readonly IAppleDevicesRepository _repository;

        public AppleDevicesQuery(IAppleDevicesRepository repository)
        {
            _repository = repository;
        }

        AppleDevice IAppleDevicesQuery.GetDevice(Guid id)
        {
            return _repository.GetUserAppleDevice(id);
        }

        AppleDevice IAppleDevicesQuery.GetDevice(string deviceToken)
        {
            return _repository.GetUserAppleDevice(deviceToken);
        }

        IList<AppleDevice> IAppleDevicesQuery.GetDevices(Guid userId)
        {
            return _repository.GetUserAppleDevices(userId);
        }
    }
}
