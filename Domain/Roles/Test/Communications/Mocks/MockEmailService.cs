using System.ServiceModel;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MockEmailService
        : IMockEmailService
    {
        public IMockEmailServer EmailServer { get; set; }

        void IMockEmailService.Send(MockEmail email)
        {
            EmailServer.Send(email);
        }
    }
}
