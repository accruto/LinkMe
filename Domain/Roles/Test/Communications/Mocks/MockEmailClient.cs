using System;
using System.Net.Mail;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    public class MockEmailClient
        : IEmailClient
    {
        private readonly IChannelManager<IMockEmailService> _serviceManager;

        public MockEmailClient(IChannelManager<IMockEmailService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        void IEmailClient.Send(MailMessage message)
        {
            var service = _serviceManager.Create();
            try
            {
                service.Send(new MockEmail(message));
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }
    }
}
