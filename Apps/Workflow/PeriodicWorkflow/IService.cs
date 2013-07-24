using System;
using System.ServiceModel;

namespace LinkMe.Workflow.PeriodicWorkflow
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void CreateWorkflow(Guid userId, TimeSpan frequency);

        [OperationContract(IsOneWay = true)]
        void OnFrequencyChanged(Guid userId, TimeSpan frequency);
    }
}