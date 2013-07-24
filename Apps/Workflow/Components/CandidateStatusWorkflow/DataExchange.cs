using System;
using System.Workflow.Activities;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Workflow.Design.CandidateStatusWorkflow;

namespace LinkMe.Workflow.Components.CandidateStatusWorkflow
{
	public class DataExchange
        : IDataExchange
	{
        private static readonly EventSource EventSource = new EventSource<DataExchange>();

        private readonly ICandidatesWorkflowCommand _candidatesWorkflowCommand;
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly IMembersQuery _membersQuery;
	    private readonly IEmailsCommand _emailsCommand;

	    private readonly TimeSpan _activelyLookingConfirmationTimeout;
        private readonly TimeSpan _activelyLookingResponseTimeout;
        private readonly TimeSpan _availableNowConfirmationLongTimeout;
        private readonly TimeSpan _availableNowConfirmationShortTimeout;
        private readonly TimeSpan _availableNowResponseLongTimeout;
        private readonly TimeSpan _availableNowResponseShortTimeout;

        public DataExchange(
            ICandidatesWorkflowCommand candidatesWorkflowCommand,
            ICandidatesCommand candidatesCommand,
            IMembersQuery membersQuery,
            IEmailsCommand emailsCommand,
            TimeSpan activelyLookingConfirmationTimeout,
            TimeSpan activelyLookingResponseTimeout, 
            TimeSpan availableNowConfirmationLongTimeout,
            TimeSpan availableNowConfirmationShortTimeout,
            TimeSpan availableNowResponseLongTimeout,
            TimeSpan availableNowResponseShortTimeout)
	    {
	        _candidatesWorkflowCommand = candidatesWorkflowCommand;
            _candidatesCommand = candidatesCommand;
	        _membersQuery = membersQuery;
            _emailsCommand = emailsCommand;

	        _activelyLookingConfirmationTimeout = activelyLookingConfirmationTimeout;
	        _activelyLookingResponseTimeout = activelyLookingResponseTimeout;
	        _availableNowConfirmationLongTimeout = availableNowConfirmationLongTimeout;
	        _availableNowConfirmationShortTimeout = availableNowConfirmationShortTimeout;
            _availableNowResponseLongTimeout = availableNowResponseLongTimeout;
            _availableNowResponseShortTimeout = availableNowResponseShortTimeout;
        }

	    #region Implementation of IDataExchange

	    public event EventHandler<StateEventArgs> StatusChanged;
	    public event EventHandler<ExternalDataEventArgs> ActivelyLookingConfirmed;
        public event EventHandler<ExternalDataEventArgs> ActivelyLookingUpgraded;
	    public event EventHandler<ExternalDataEventArgs> AvailableNowConfirmedWithLongReminder;
	    public event EventHandler<ExternalDataEventArgs> AvailableNowConfirmedWithShortReminder;

        TimeSpan IDataExchange.GetActivelyLookingConfirmationTimeout()
	    {
	        return _activelyLookingConfirmationTimeout;
	    }

        TimeSpan IDataExchange.GetActivelyLookingResponseTimeout()
	    {
            return _activelyLookingResponseTimeout;
	    }

        TimeSpan IDataExchange.GetAvailableNowConfirmationLongTimeout()
	    {
            return _availableNowConfirmationLongTimeout;
	    }

        TimeSpan IDataExchange.GetAvailableNowConfirmationShortTimeout()
	    {
            return _availableNowConfirmationShortTimeout;
	    }

        TimeSpan IDataExchange.GetAvailableNowResponseLongTimeout()
	    {
            return _availableNowResponseLongTimeout;
	    }

        TimeSpan IDataExchange.GetAvailableNowResponseShortTimeout()
        {
            return _availableNowResponseShortTimeout;
        }

	    void IDataExchange.UpdateStatus(Guid candidateId, State state)
        {
            #region Log
            const string method = "UpdateStatus";
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", candidateId), Event.Arg("state", state));
            #endregion

            try
            {
                var candidate = _candidatesCommand.GetCandidate(candidateId);
                if (candidate != null)
                {
                    candidate.Status = state.ToCandidateStatus();
                    _candidatesCommand.UpdateCandidate(candidate);
                }
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId), Event.Arg("state", state));
                #endregion
            }
        }

        void IDataExchange.CreateActivelyLookingConfirmationRequest(Guid candidateId)
	    {
            #region Log
            const string method = "CreateActivelyLookingConfirmationRequest";
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                SendEmailAsync(new EmailRequest(candidateId, to => new CandidateLookingConfirmationEmail(to)));
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }
        }

        void IDataExchange.CreateAvailableNowConfirmationRequest(Guid candidateId)
	    {
            #region Log
            const string method = "CreateAvailableNowConfirmationRequest";
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                SendEmailAsync(new EmailRequest(candidateId, to => new CandidateAvailableConfirmationEmail(to)));
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }
        }

        void IDataExchange.CreatePassiveNotification(Guid candidateId)
	    {
            #region Log
            const string method = "CreatePassiveNotification";
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                SendEmailAsync(new EmailRequest(candidateId, to => new CandidatePassiveNotificationEmail(to)));
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }
        }

        void IDataExchange.CreateActivelyLookingNotification(Guid candidateId)
	    {
            #region Log
            const string method = "CreateActivelyLookingNotification";
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                SendEmailAsync(new EmailRequest(candidateId, to => new CandidateLookingNotificationEmail(to)));
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }
        }

        void IDataExchange.CompleteWorkflow(Guid candidateId)
	    {
            #region Log
            const string method = "CompleteWorkflow";
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                _candidatesWorkflowCommand.DeleteStatusWorkflow(candidateId);
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }
        }

	    #endregion

        #region Event Wrappers

        public void OnStatusChanged(Guid workflowId, State state)
        {
            var handler = StatusChanged;
            if (handler != null)
                handler(null, new StateEventArgs(workflowId, state));
        }

        public void OnActivelyLookingConfirmed(Guid workflowId)
        {
            var handler = ActivelyLookingConfirmed;
            if (handler != null)
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
            if (handler != null)
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        public void OnAvailableNowConfirmedWithShortReminder(Guid workflowId)
        {
            var handler = AvailableNowConfirmedWithShortReminder;
            if (handler != null)
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        #endregion

        private class EmailRequest
        {
            public readonly Guid CandidateId;
            public readonly Func<ICommunicationUser, TemplateEmail> CreateEmail;

            public EmailRequest(Guid candidateId, Func<ICommunicationUser, TemplateEmail> createEmail)
            {
                CandidateId = candidateId;
                CreateEmail = createEmail;
            }
        }

        private void SendEmailAsync(object asyncState)
        {
            const string method = "SendEmailAsync";
            var request = (EmailRequest)asyncState;

            #region Log
            EventSource.Raise(Event.Flow, method, Event.Arg("candidateId", request.CandidateId));
            #endregion

            try
            {
                var to = _membersQuery.GetMember(request.CandidateId);
                var email = request.CreateEmail(to);
                _emailsCommand.TrySend(email);
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", request.CandidateId));
                #endregion
            }
        }
	}
}
