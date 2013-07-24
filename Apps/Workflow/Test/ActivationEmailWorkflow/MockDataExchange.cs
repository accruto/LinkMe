using System;
using System.Workflow.Activities;
using LinkMe.Workflow.Design.ActivationEmailWorkflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Workflow.Test.ActivationEmailWorkflow
{
    class MockDataExchange
        : IDataExchange
    {
        private double[] _delays;

        public Guid UserId { get; private set; }
        public int Emails { get; private set; }
        public bool IsComplete { get; private set; }

        public void Reset()
        {
            _delays = null;
            UserId = Guid.Empty;
            Emails = 0;
            IsComplete = false;
        }

        public void SetDelays(params double[] delays)
        {
            _delays = delays;
        }

        public void RaiseStopSending(Guid workflowId)
        {
            var handler = StopSending;
            if (handler != null)
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        #region Implementation of IDataExchange

        public event EventHandler<ExternalDataEventArgs> StopSending;

        void IDataExchange.SendEmail(Guid userId, int emailSeqNo)
        {
            UserId = userId;
            Assert.AreEqual(Emails, emailSeqNo);
            Emails++;
        }

        TimeSpan IDataExchange.GetNextDelay(int emailSeqNo)
        {
            return emailSeqNo < _delays.Length
                ? TimeSpan.FromMilliseconds(_delays[emailSeqNo])
                : TimeSpan.MaxValue;
        }

        void IDataExchange.CompleteWorkflow(Guid userId)
        {
            UserId = userId;
            IsComplete = true;
        }

        #endregion
    }
}
