using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.Runtime;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Workflow.CandidateStatusWorkflow;

namespace LinkMe.Workflow.Components.CandidateStatusWorkflow
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service
        : ServiceBase, IService
    {
        private static readonly EventSource EventSource = new EventSource<Service>();

        private readonly object[] _locks = new object[16];
        private readonly ICandidatesWorkflowCommand _candidatesWorkflowCommand;
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly IEmailsCommand _emailsCommand;
        private DataExchange _dataExchange;

        #region Optional Dependency Properties

        public TimeSpan ActivelyLookingConfirmationTimeout { get; set; }
        public TimeSpan ActivelyLookingResponseTimeout { get; set; }
        public TimeSpan AvailableNowConfirmationLongTimeout { get; set; }
        public TimeSpan AvailableNowConfirmationShortTimeout { get; set; }
        public TimeSpan AvailableNowResponseLongTimeout { get; set; }
        public TimeSpan AvailableNowResponseShortTimeout { get; set; }

        #endregion

        public Service(
            IDbConnectionFactory connectionFactory,
            ICandidatesWorkflowCommand candidatesWorkflowCommand,
            ICandidatesCommand candidatesCommand,
            IMembersQuery membersQuery,
            IEmailsCommand emailsCommand)
            : base(connectionFactory)
        {
            for (int i = 0; i < _locks.Length; i++)
                _locks[i] = new object();

            _membersQuery = membersQuery;
            _emailsCommand = emailsCommand;
            _candidatesWorkflowCommand = candidatesWorkflowCommand;
            _candidatesCommand = candidatesCommand;

            ActivelyLookingConfirmationTimeout = TimeSpan.FromDays(30);
            ActivelyLookingResponseTimeout = TimeSpan.FromDays(7);
            AvailableNowConfirmationLongTimeout = TimeSpan.FromDays(30);
            AvailableNowConfirmationShortTimeout = TimeSpan.FromDays(7);
            AvailableNowResponseLongTimeout = TimeSpan.FromDays(7);
            AvailableNowResponseShortTimeout = TimeSpan.FromDays(7);
        }

        public override void OnOpen()
        {
            base.OnOpen();

            _dataExchange = new DataExchange(
                _candidatesWorkflowCommand,
                _candidatesCommand,
                _membersQuery,
                _emailsCommand,
                ActivelyLookingConfirmationTimeout,
                ActivelyLookingResponseTimeout,
                AvailableNowConfirmationLongTimeout,
                AvailableNowConfirmationShortTimeout,
                AvailableNowResponseLongTimeout,
                AvailableNowResponseShortTimeout);

            AddDataExchange(_dataExchange);
        }

        #region Implementation of IService

        void IService.CreateWorkflow(Guid candidateId, CandidateStatus status)
        {
            #region Log
            const string method = "CreateWorkflow";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId), Event.Arg("status", status));
            #endregion

            try
            {
                lock (GetLock(candidateId))
                {
                    var workflowId = _candidatesWorkflowCommand.GetStatusWorkflowId(candidateId);
                    if (!workflowId.HasValue)
                    {
                        CreateWorkflow(candidateId, status, false);
                    }
                    else
                    {
                        #region Log
                        EventSource.Raise(Event.Information, method, "Workflow already exists.", Event.Arg("candidateId", candidateId), Event.Arg("workflowId", workflowId.Value));
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId), Event.Arg("status", status));
                #endregion
                throw;
            }

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId), Event.Arg("status", status));
            #endregion
        }

        void IService.DeleteWorkflow(Guid candidateId)
        {
            #region Log
            const string method = "DeleteWorkflow";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                lock (GetLock(candidateId))
                {
                    var workflowId = _candidatesWorkflowCommand.GetStatusWorkflowId(candidateId);
                    if (workflowId.HasValue)
                    {
                        // Create new workflow instance.

                        DeleteWorkflow(workflowId.Value);

                        // Record workflow instance in LinkMe database.

                        _candidatesWorkflowCommand.DeleteStatusWorkflow(candidateId);
                    }
                }
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId));
            #endregion
        }

        void IService.LogWorkflow(Guid candidateId)
        {
            #region Log
            const string method = "LogWorkflow";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId));
            #endregion

            try
            {
                lock (GetLock(candidateId))
                {
                    var workflowId = _candidatesWorkflowCommand.GetStatusWorkflowId(candidateId);
                    if (workflowId.HasValue)
                    {
                        #region Log
                        EventSource.Raise(Event.Information, method, "Workflow exists.", Event.Arg("candidateId", candidateId), Event.Arg("workflowId", workflowId.Value));
                        #endregion

                        var workflow = GetWorkflow(workflowId.Value);
                        if (workflow == null)
                        {
                            #region Log
                            EventSource.Raise(Event.Information, method, "Workflow not found.", Event.Arg("candidateId", candidateId), Event.Arg("workflowId", workflowId.Value));
                            #endregion
                        }
                        else
                        {
                            #region Log
                            EventSource.Raise(Event.Information, method, "Workflow found.", Event.Arg("candidateId", candidateId), Event.Arg("workflowId", workflowId.Value));
                            #endregion

                            LogActivities(workflow);
                        }
                    }
                    else
                    {
                        #region Log
                        EventSource.Raise(Event.Information, method, "Workflow does not exist.", Event.Arg("candidateId", candidateId));
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId));
                #endregion
                throw;
            }

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId));
            #endregion
        }

        void IService.OnStatusChanged(Guid candidateId, CandidateStatus status)
        {
            #region Log
            const string method = "OnStatusChanged";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId), Event.Arg("status", status));
            #endregion

            HandleExternalEvent(candidateId, status, i => _dataExchange.OnStatusChanged(i, status.ToState()), method);

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId), Event.Arg("status", status));
            #endregion
        }

        void IService.OnActivelyLookingConfirmed(Guid candidateId)
        {
            #region Log
            const string method = "OnConfirmLookingClicked";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId));
            #endregion

            HandleExternalEvent(candidateId, CandidateStatus.ActivelyLooking, i => _dataExchange.OnActivelyLookingConfirmed(i), method);

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId));
            #endregion
        }

        void IService.OnActivelyLookingUpgraded(Guid candidateId)
        {
            #region Log
            const string method = "OnUpgradeLookingClicked";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId));
            #endregion

            HandleExternalEvent(candidateId, CandidateStatus.AvailableNow, i => _dataExchange.OnActivelyLookingUpgraded(i), method);

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId));
            #endregion
        }

        void IService.OnAvailableNowConfirmed(Guid candidateId)
        {
            #region Log
            const string method = "OnConfirmAvailableDailyClicked";
            EventSource.Raise(Event.FlowEnter, method, Event.Arg("candidateId", candidateId));
            #endregion

            HandleExternalEvent(candidateId, CandidateStatus.AvailableNow, i => _dataExchange.OnAvailableNowConfirmedWithShortReminder(i), method);

            #region Log
            EventSource.Raise(Event.FlowExit, method, Event.Arg("candidateId", candidateId));
            #endregion
        }

        #endregion

        private void CreateWorkflow(Guid candidateId, CandidateStatus status, bool ignoreTimeoutOnce)
        {
            // Create new workflow instance.

            var parameters = new Dictionary<string, object>
            {
                { "CandidateId", candidateId },
                { "State", status.ToState() },
                { "IgnoreTimeoutOnce", ignoreTimeoutOnce },
            };

            var workflow = CreateWorkflow<Design.CandidateStatusWorkflow.Workflow>(parameters);

            // Record workflow instance in LinkMe database.

            _candidatesWorkflowCommand.AddStatusWorkflow(candidateId, workflow.InstanceId);

            // Start running the workflow.

            workflow.Start();
        }

        private object GetLock(Guid candidateId)
        {
            return _locks[(uint)candidateId.GetHashCode() % _locks.Length];
        }

        private void HandleExternalEvent(Guid candidateId, CandidateStatus status, Action<Guid> action, string method)
        {
            try
            {
                lock (GetLock(candidateId))
                {
                    var workflowId = _candidatesWorkflowCommand.GetStatusWorkflowId(candidateId);
                    var handled = false;

                    if (workflowId.HasValue)
                    {
                        try
                        {
                            action(workflowId.Value);
                            handled = true;
                        }
                        catch (EventDeliveryFailedException e)
                        {
                            #region Log
                            EventSource.Raise(Event.Warning, method, "The workflow instance is not found by Workflow Runtime or is damaged. It will be recreated again.", e, null, Event.Arg("candidateId", candidateId), Event.Arg("workflowId", workflowId.Value));
                            #endregion
                        }
                    }

                    if (!handled && (status == CandidateStatus.AvailableNow || status == CandidateStatus.ActivelyLooking))
                        CreateWorkflow(candidateId, status, false);
                }
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, e, null, Event.Arg("candidateId", candidateId), Event.Arg("status", status));
                #endregion
                throw;
            }
        }

        private void LogActivities(WorkflowInstance workflow)
        {
            const string method = "LogActivities";

            var executor = GetExecutor(workflow);
            if (executor == null)
            {
                #region Log
                EventSource.Raise(Event.Information, method, "No executor.");
                #endregion
            }
            else
            {
                #region Log
                EventSource.Raise(Event.Information, method, "Found executor, type '" + executor.GetType() + "'.");
                #endregion

                var rootActivity = GetRootActivity(workflow);
                if (rootActivity == null)
                {
                    #region Log
                    EventSource.Raise(Event.Information, method, "No root activity.");
                    #endregion
                }
                else
                {
                    #region Log
                    EventSource.Raise(Event.Information, method, "Found root activity.");
                    #endregion

                    LogActivity(rootActivity);
                }
            }
        }

        private static Activity GetRootActivity(object executor)
        {
            const string method = "GetRootActivity";

            var field = executor.GetType().GetField("rootActivity", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
            if (field == null)
            {
                #region Log
                EventSource.Raise(Event.Information, method, "Executor has no field 'rootActivity'.");
                #endregion

                return null;
            }
            
            var value = field.GetValue(executor);
            if (value == null)
            {
                #region Log
                EventSource.Raise(Event.Information, method, "'rootActivity' has null value.");
                #endregion
            }
            else
            {
                #region Log
                EventSource.Raise(Event.Information, method, "'rootActivity' has type '" + value.GetType() + "'");
                #endregion
            }

            return value as Activity;
        }

        private static void LogActivity(Activity activity)
        {
            const string method = "LogActivity";

            #region Log
            EventSource.Raise(Event.Information, method, "Activity '" + activity.Name + "' has status '" + activity.ExecutionStatus + "' and result '" + activity.ExecutionResult + "'.");
            #endregion

            if (activity is CompositeActivity)
            {
                foreach (var childActivity in ((CompositeActivity) activity).Activities)
                    LogActivity(childActivity);
            }
        }
    }
}
