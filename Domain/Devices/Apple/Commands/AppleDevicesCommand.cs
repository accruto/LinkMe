using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Devices.Apple.Commands
{
    public class AppleDevicesCommand
        : IAppleDevicesCommand
    {
        private readonly IAppleDevicesRepository _repository;

        public AppleDevicesCommand(IAppleDevicesRepository repository)
        {
            _repository = repository;
        }

        void IAppleDevicesCommand.CreateDevice(AppleDevice device)
        {
            CreateDevice(device);
        }

        void IAppleDevicesCommand.UpdateDevice(AppleDevice device)
        {
            UpdateDevice(device);
        }

        private void CreateDevice(AppleDevice device)
        {
            device.Prepare();
            device.Validate();

            _repository.CreateUserAppleDevice(device);
        }

        private void UpdateDevice(AppleDevice device)
        {
            device.Validate();

            _repository.UpdateUserAppleDevice(device);
        }
    }
}