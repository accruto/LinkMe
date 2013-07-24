using System;
using JdSoft.Apple.Apns.Feedback;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Services.External.Apple.Notifications
{
    public class PushDevicesFeedbackCommand
        : IPushDevicesFeedbackCommand, IDisposable
    {
        private static readonly EventSource EventSource = new EventSource(typeof(PushDevicesFeedbackCommand));

        private FeedbackService _service;
        private readonly string _p12FileName;
        private readonly string _p12Password;
        private readonly bool _sandbox;
        private bool _disposed;

        private readonly IAppleDevicesQuery _appleDevicesQuery;

        private readonly IAppleDevicesCommand _appleDevicesCommand;

        public PushDevicesFeedbackCommand(string p12FileName, string p12Password, bool sandbox, IAppleDevicesQuery appleDevicesQuery, IAppleDevicesCommand appleDevicesCommand)
        {
            _p12FileName = string.Format(".\\{0}", p12FileName);
            _p12Password = p12Password;
            _sandbox = sandbox;

            _appleDevicesQuery = appleDevicesQuery;

            _appleDevicesCommand = appleDevicesCommand;

            CreateService();
        }

        public void DisableDevices()
        {
            _service.Run();
        }

        private void CreateService()
        {
            _service = new FeedbackService(_sandbox, _p12FileName, _p12Password);
            _service.Feedback += (ServiceFeedback);
        }

        void ServiceFeedback(object sender, Feedback feedback)
        {
            const string method = "Disable Device";

            var device = _appleDevicesQuery.GetDevice(feedback.DeviceToken);

            if (device == null)
                return;

            EventSource.Raise(Event.Information, method, "Disabling device", Event.Arg("deviceToken", feedback.DeviceToken));

            device.Active = false;
            _appleDevicesCommand.UpdateDevice(device);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            const string method = "Service Disposal";

            if (!_disposed)
            {
                if (disposing)
                {
                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Disposing feedback service");

                    _service.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
