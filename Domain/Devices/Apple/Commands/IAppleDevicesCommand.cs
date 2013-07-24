namespace LinkMe.Domain.Devices.Apple.Commands
{
    public interface IAppleDevicesCommand
    {
        void CreateDevice(AppleDevice device);
        void UpdateDevice(AppleDevice device);
    }
}
