using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.CandidateStatusWorkflow
{
    [ExternalDataExchange]
    public interface IDataExchange
	{
        event EventHandler<StateEventArgs> StatusChanged;
        event EventHandler<ExternalDataEventArgs> ActivelyLookingConfirmed;
        event EventHandler<ExternalDataEventArgs> ActivelyLookingUpgraded;
        event EventHandler<ExternalDataEventArgs> AvailableNowConfirmedWithLongReminder;
        event EventHandler<ExternalDataEventArgs> AvailableNowConfirmedWithShortReminder;

        void UpdateStatus(Guid candidateId, State state);
        void CompleteWorkflow(Guid candidateId);

        TimeSpan GetActivelyLookingConfirmationTimeout();
        TimeSpan GetActivelyLookingResponseTimeout();
        TimeSpan GetAvailableNowConfirmationLongTimeout();
        TimeSpan GetAvailableNowConfirmationShortTimeout();
        TimeSpan GetAvailableNowResponseLongTimeout();
        TimeSpan GetAvailableNowResponseShortTimeout();

        void CreateActivelyLookingConfirmationRequest(Guid candidateId);
        void CreateAvailableNowConfirmationRequest(Guid candidateId);
        void CreatePassiveNotification(Guid candidateId);
        void CreateActivelyLookingNotification(Guid candidateId);
    }
}
