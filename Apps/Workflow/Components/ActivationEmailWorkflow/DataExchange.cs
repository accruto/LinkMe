using System;
using System.Workflow.Activities;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Workflow.Design.ActivationEmailWorkflow;

namespace LinkMe.Workflow.Components.ActivationEmailWorkflow
{
    class DataExchange
        : IDataExchange
    {
        private readonly TimeSpan[] _delays;
        private readonly IMembersQuery _membersQuery;
        private readonly IEmailVerificationsQuery _emailVerificationsQuery;
        private readonly IEmailsCommand _emailsCommand;
        private readonly ICandidatesWorkflowCommand _workflowCommand;

        public DataExchange(TimeSpan[] delays, IMembersQuery membersQuery, IEmailVerificationsQuery emailVerificationsQuery, IEmailsCommand emailsCommand, ICandidatesWorkflowCommand workflowCommand)
        {
            _delays = delays;
            _membersQuery = membersQuery;
            _emailVerificationsQuery = emailVerificationsQuery;
            _emailsCommand = emailsCommand;
            _workflowCommand = workflowCommand;
        }

        public void RaiseStopSending(Guid workflowId)
        {
            var handler = StopSending;
            if (handler != null)
                handler(null, new ExternalDataEventArgs(workflowId));
        }

        #region Implementation of IDataExchange

        public event EventHandler<ExternalDataEventArgs> StopSending;

        public void SendEmail(Guid userId, int emailSeqNo)
        {
            var member = _membersQuery.GetMember(userId);

            // Only send if still not activated.

            if (!member.IsActivated)
            {
                var emailVerification = _emailVerificationsQuery.GetEmailVerification(userId, member.GetBestEmailAddress().Address);
                var email = new ActivationEmail(member, emailVerification);
                _emailsCommand.TrySend(email);
            }
        }

        public TimeSpan GetNextDelay(int emailSeqNo)
        {
            var delay = (emailSeqNo < _delays.Length) ? _delays[emailSeqNo] : TimeSpan.MaxValue;
            return delay;
        }

        public void CompleteWorkflow(Guid userId)
        {
            _workflowCommand.DeleteActivationEmailWorkflow(userId);
        }

        #endregion
    }
}
