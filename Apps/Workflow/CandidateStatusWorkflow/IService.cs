using System;
using System.ServiceModel;
using LinkMe.Domain;

namespace LinkMe.Workflow.CandidateStatusWorkflow
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void CreateWorkflow(Guid candidateId, CandidateStatus status);

        [OperationContract(IsOneWay = true)]
        void DeleteWorkflow(Guid candidateId);

        [OperationContract(IsOneWay = true)]
        void LogWorkflow(Guid candidateId);

        [OperationContract(IsOneWay = true)]
        void OnStatusChanged(Guid candidateId, CandidateStatus status);

        [OperationContract(IsOneWay = true)]
        void OnActivelyLookingConfirmed(Guid candidateId);

        [OperationContract(IsOneWay = true)]
        void OnActivelyLookingUpgraded(Guid candidateId);

        [OperationContract(IsOneWay = true)]
        void OnAvailableNowConfirmed(Guid candidateId);
    }
}
