using System;
using System.Collections.Generic;
using System.Workflow.Activities;
using LinkMe.Workflow.Design.CandidateStatusWorkflow;

namespace LinkMe.Apps.Workflow.Test.CandidateStatusWorkflow
{
    internal enum MockEmail
    {
        ConfirmLookingRequest,
        ConfirmAvailableRequest,
        PassiveNotification,
        LookingNotification
    }

    internal class MockDataExchange
        : IDataExchange
    {
        private IList<MockEmail> _emails = new List<MockEmail>();

        public IList<MockEmail> Emails
        {
            get { return _emails; }
        }

        public Guid UserId { get; private set; }
        public bool IsComplete { get; private set; }
        public State State { get; private set; }

        private readonly TimeSpan _shortTimeout;
        private readonly TimeSpan _longTimeout;

        public MockDataExchange(double shortTimeout, double longTimeout)
        {
            _shortTimeout = TimeSpan.FromMilliseconds(shortTimeout);
            _longTimeout = TimeSpan.FromMilliseconds(longTimeout);
        }

        public void Reset(State state)
        {
            _emails = new List<MockEmail>();
            UserId = Guid.Empty;
            IsComplete = false;
            State = state;
        }

        #region Event helpers

        public void RaiseStatusChanged(Guid workflowId, State state)
        {
            var handler = StatusChanged;
            if (handler != null)
                handler(null, new StateEventArgs(workflowId, state));
        }

        public void OnActivelyLookingConfirmed(Guid workflowId)
        {
            var handler = ActivelyLookingConfirmed;
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        public void OnActivelyLookingUpgraded(Guid workflowId)
        {
            var handler = ActivelyLookingUpgraded;
            if (handler != null)
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        public void OnAvailableNowConfirmedWithLongReminder(Guid workflowId)
        {
            var handler = AvailableNowConfirmedWithLongReminder;
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        public void OnAvailableNowConfirmedWithShortReminder(Guid workflowId)
        {
            var handler = AvailableNowConfirmedWithShortReminder;
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        #endregion

        #region Implementation of IDataExchange

        public event EventHandler<StateEventArgs> StatusChanged;
        public event EventHandler<ExternalDataEventArgs> ActivelyLookingConfirmed;
        public event EventHandler<ExternalDataEventArgs> ActivelyLookingUpgraded;
        public event EventHandler<ExternalDataEventArgs> AvailableNowConfirmedWithLongReminder;
        public event EventHandler<ExternalDataEventArgs> AvailableNowConfirmedWithShortReminder;

        TimeSpan IDataExchange.GetActivelyLookingConfirmationTimeout()
        {
            return _shortTimeout;
        }

        TimeSpan IDataExchange.GetActivelyLookingResponseTimeout()
        {
            return _shortTimeout;
        }

        TimeSpan IDataExchange.GetAvailableNowConfirmationLongTimeout()
        {
            return _longTimeout;
        }

        TimeSpan IDataExchange.GetAvailableNowConfirmationShortTimeout()
        {
            return _shortTimeout;
        }

        TimeSpan IDataExchange.GetAvailableNowResponseLongTimeout()
        {
            return _longTimeout;
        }

        TimeSpan IDataExchange.GetAvailableNowResponseShortTimeout()
        {
            return _shortTimeout;
        }

        void IDataExchange.UpdateStatus(Guid candidateId, State state)
        {
            if (candidateId == Guid.Empty)
                throw new ArgumentException("Candidate ID is empty. Check that it is properly bound.");
            UserId = candidateId;
            State = state;
        }

        void IDataExchange.CreateActivelyLookingConfirmationRequest(Guid candidateId)
        {
            if (candidateId == Guid.Empty)
                throw new ArgumentException("Candidate ID is empty. Check that it is properly bound.");
            UserId = candidateId;
            Emails.Add(MockEmail.ConfirmLookingRequest);
        }

        void IDataExchange.CreateAvailableNowConfirmationRequest(Guid candidateId)
        {
            if (candidateId == Guid.Empty)
                throw new ArgumentException("Candidate ID is empty. Check that it is properly bound.");
            UserId = candidateId;
            Emails.Add(MockEmail.ConfirmAvailableRequest);
        }

        void IDataExchange.CreatePassiveNotification(Guid candidateId)
        {
            if (candidateId == Guid.Empty)
                throw new ArgumentException("Candidate ID is empty. Check that it is properly bound.");
            UserId = candidateId;
            Emails.Add(MockEmail.PassiveNotification);
        }

        void IDataExchange.CreateActivelyLookingNotification(Guid candidateId)
        {
            if (candidateId == Guid.Empty)
                throw new ArgumentException("Candidate ID is empty. Check that it is properly bound.");
            UserId = candidateId;
            Emails.Add(MockEmail.LookingNotification);
        }

        void IDataExchange.CompleteWorkflow(Guid candidateId)
        {
            IsComplete = true;
        }

        #endregion
    }
}
