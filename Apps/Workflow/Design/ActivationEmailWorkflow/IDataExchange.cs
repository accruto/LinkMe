using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.ActivationEmailWorkflow
{
    [ExternalDataExchange]
    public interface IDataExchange
	{
        event EventHandler<ExternalDataEventArgs> StopSending;

        void SendEmail(Guid userId, int emailSeqNo);
        TimeSpan GetNextDelay(int emailSeqNo);
        void CompleteWorkflow(Guid userId);
    }
}
    