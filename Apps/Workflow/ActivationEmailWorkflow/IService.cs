using System;
using System.ServiceModel;

namespace LinkMe.Workflow.ActivationEmailWorkflow
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void StartSending(Guid userId);

        [OperationContract(IsOneWay = true)]
        void StopSending(Guid userId);
    }
}
