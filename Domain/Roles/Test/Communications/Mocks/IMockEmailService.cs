using System.ServiceModel;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    [ServiceContract]
    public interface IMockEmailService
    {
        [OperationContract]
        void Send(MockEmail email);
    }
}
