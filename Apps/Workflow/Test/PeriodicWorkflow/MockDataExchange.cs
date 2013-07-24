using System;
using LinkMe.Workflow.Design.PeriodicWorkflow;

namespace LinkMe.Apps.Workflow.Test.PeriodicWorkflow
{
    internal class MockDataExchange
        : IDataExchange
    {
        public Guid UserId { get; private set; }
        public DateTime LastSentTime { get; private set; }
        public int Emails { get; private set; }
        public bool IsComplete { get; private set; }

        #region Implementation of IDataExchange

        public event EventHandler<DelayEventArgs> DelayChanged;

        void IDataExchange.Run(Guid userId, DateTime lastRunTime)
        {
            Emails++;
            UserId = userId;
            LastSentTime = lastRunTime;
        }

        void IDataExchange.CompleteWorkflow(Guid userId)
        {
            UserId = userId;
            IsComplete = true;
        }

        #endregion

        public void Reset()
        {
            Emails = 0;
            UserId = Guid.Empty;
            LastSentTime = DateTime.MinValue;
            IsComplete = false;
        }

        public void RaiseDelayChanged(Guid workflowId, TimeSpan delay)
        {
            var handler = DelayChanged;
            if (handler != null)
                handler(null, new DelayEventArgs(workflowId, delay));
        }
    }
}