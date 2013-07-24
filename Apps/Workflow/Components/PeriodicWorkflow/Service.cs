using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Workflow.PeriodicWorkflow;

namespace LinkMe.Workflow.Components.PeriodicWorkflow
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service
        : ServiceBase, IService
    {
        private readonly IWorker _worker;
        private readonly DataExchange _dataExchange; 
        private readonly EventSource _logger;
        private readonly object[] _locks = new object[16];

        public Service(IDbConnectionFactory connectionFactory, IWorker worker)
            : base(connectionFactory)
        {
            _worker = worker;
            _logger = new EventSource(worker.GetType());
            _dataExchange = new DataExchange(worker, _logger);

            for (int i = 0; i < _locks.Length; i++)
                _locks[i] = new object();
        }

        public override void OnOpen()
        {
            base.OnOpen();
            AddDataExchange(_dataExchange);
        }

        #region Implementation of IService

        void IService.CreateWorkflow(Guid userId, TimeSpan frequency)
        {
            #region Log
            const string method = "CreateWorkflow";
            _logger.Raise(Event.FlowEnter, method, Event.Arg("userId", userId));
            #endregion

            try
            {
                lock (GetLock(userId))
                {
                    var workflowId = _worker.GetWorkflowId(userId);
                    if (!workflowId.HasValue)
                    {
                        CreateWorkflowCore(userId, frequency);
                    }
                    else
                    {
                        #region Log
                        _logger.Raise(Event.Information, method, "Workflow already exists.", Event.Arg("userId", userId), Event.Arg("workflowId", workflowId.Value));
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                #region Log
                _logger.Raise(Event.Error, method, e, null, Event.Arg("userId", userId));
                #endregion
                throw;
            }

            #region Log
            _logger.Raise(Event.FlowExit, method, Event.Arg("userId", userId));
            #endregion
        }

        void IService.OnFrequencyChanged(Guid userId, TimeSpan frequency)
        {
            #region Log
            const string method = "OnFrequencyChanghed";
            _logger.Raise(Event.FlowEnter, method, Event.Arg("userId", userId));
            #endregion

            try
            {
                lock (GetLock(userId))
                {
                    var workflowId = _worker.GetWorkflowId(userId);
                    var handled = false;

                    if (workflowId.HasValue)
                    {
                        try
                        {
                            _dataExchange.OnDelayChanged(workflowId.Value, frequency);
                            handled = true;
                        }
                        catch (EventDeliveryFailedException e)
                        {
                            if (!IsWorkflowNotFound(e))
                                throw;

                            #region Log
                            _logger.Raise(Event.Warning, method, "The workflow instance is not found by Workflow Runtime. It will be recreated again.", Event.Arg("userId", userId), Event.Arg("workflowId", workflowId.Value));
                            #endregion
                        }
                    }

                    if (!handled && frequency != TimeSpan.MaxValue)
                        CreateWorkflowCore(userId, frequency);
                }
            }
            catch (Exception e)
            {
                #region Log
                _logger.Raise(Event.Error, method, e, null, Event.Arg("userId", userId));
                #endregion
                throw;
            }


            #region Log
            _logger.Raise(Event.FlowExit, method, Event.Arg("userId", userId));
            #endregion
        }

        #endregion

        #region Overridables

        #endregion

        private object GetLock(Guid candidateId)
        {
            return _locks[(uint)candidateId.GetHashCode() % _locks.Length];
        }

        private void CreateWorkflowCore(Guid userId, TimeSpan frequency)
        {
            // Create new workflow instance.

            var parameters = new Dictionary<string, object>
                                 {
                                     { "UserId", userId },
                                     { "Delay", frequency },
                                     { "LastRunTime", DateTime.MinValue },
                                 };

            WorkflowInstance workflow = CreateWorkflow<Design.PeriodicWorkflow.PeriodicWorkflow>(parameters);

            // Record workflow instance in LinkMe database.

            _worker.AttachWorkflow(userId, workflow.InstanceId);

            // Start running the workflow.

            workflow.Start();
        }

        private static bool IsWorkflowNotFound(EventDeliveryFailedException e)
        {
            Exception cause = e.GetBaseException();
            return (cause.Data != null && cause.Data.Contains("WorkflowNotFound")) ? true : false;
        }
    }
}