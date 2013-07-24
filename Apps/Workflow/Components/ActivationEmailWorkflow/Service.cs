using System;
using System.Collections.Generic;
using System.ServiceModel;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Workflow.ActivationEmailWorkflow;

namespace LinkMe.Workflow.Components.ActivationEmailWorkflow
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service
        : ServiceBase, IService
    {
        private readonly ICandidatesWorkflowCommand _workflowCommand;
        private DataExchange _dataExchange;
        private readonly IMembersQuery _membersQuery;
        private readonly IEmailVerificationsQuery _emailVerificationsQuery;
        private readonly IEmailsCommand _emailsCommand;

        private static readonly TimeSpan[] DefaultDelays = new[]
        {
            TimeSpan.FromDays(1),
            TimeSpan.FromDays(1),
            TimeSpan.FromDays(1),
            TimeSpan.FromDays(7),
            TimeSpan.FromDays(7),
        };

        public TimeSpan[] Delays { private get; set; }

        public Service(IDbConnectionFactory connectionFactory, ICandidatesWorkflowCommand workflowCommand, IMembersQuery membersQuery, IEmailVerificationsQuery emailVerificationsQuery, IEmailsCommand emailsCommand)
            : base(connectionFactory)
        {
            _workflowCommand = workflowCommand;
            _membersQuery = membersQuery;
            _emailVerificationsQuery = emailVerificationsQuery;
            _emailsCommand = emailsCommand;

            Delays = DefaultDelays;
        }

        public override void OnOpen()
        {
            base.OnOpen();
            _dataExchange = new DataExchange(Delays, _membersQuery, _emailVerificationsQuery, _emailsCommand, _workflowCommand);
            AddDataExchange(_dataExchange);
        }

        #region Implementation of IService

        void IService.StartSending(Guid userId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "UserId", userId },
            };

            var workflow = CreateWorkflow<Design.ActivationEmailWorkflow.ActivationEmailWorkflow>(parameters);
            _workflowCommand.AddActivationEmailWorkflow(userId, workflow.InstanceId);
            workflow.Start();
        }

        void IService.StopSending(Guid userId)
        {
            var workflowId = _workflowCommand.GetActivationEmailWorkflowId(userId);
            if (workflowId == null)
                return;

            _dataExchange.RaiseStopSending(workflowId.Value);
        }

        #endregion
    }
}
