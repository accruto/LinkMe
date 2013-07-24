using System;
using LinkMe.Framework.Instrumentation;
using LinkMe.Workflow.Design.PeriodicWorkflow;

namespace LinkMe.Workflow.Components.PeriodicWorkflow
{
    public class DataExchange
        : IDataExchange
    {
        private readonly IWorker _worker;
        private readonly EventSource _logger;

        public DataExchange(IWorker worker, EventSource logger)
        {
            _worker = worker;
            _logger = logger;
        }

        #region Implementation of IDataExchange

        public event EventHandler<DelayEventArgs> DelayChanged;

        public void Run(Guid userId, DateTime lastRunTime)
        {
            #region Log
            const string method = "Run";
            _logger.Raise(Event.Flow, method, Event.Arg("userId", userId));
            #endregion

            try
            {
                //ThreadPool.QueueUserWorkItem(RunWorker, new RunRequest(userId, lastRunTime));
                RunAsync(new RunRequest(userId, lastRunTime));
            }
            catch (Exception e)
            {
                #region Log
                _logger.Raise(Event.Error, method, e, null, Event.Arg("userId", userId));
                #endregion
                throw;
            }
        }

        public void CompleteWorkflow(Guid userId)
        {
            #region Log
            const string method = "CompleteWorkflow";
            _logger.Raise(Event.Flow, method, Event.Arg("userId", userId));
            #endregion

            try
            {
                _worker.DetachWorkflow(userId);
            }
            catch (Exception e)
            {
                #region Log
                _logger.Raise(Event.Error, method, e, null, Event.Arg("userId", userId));
                #endregion
                throw;
            }
        }

        #endregion

        public void OnDelayChanged(Guid workflowId, TimeSpan delay)
        {
            var handler = DelayChanged;
            if (handler != null)
                handler(null, new DelayEventArgs(workflowId, delay));
        }

        private class RunRequest
        {
            public readonly Guid UserId;
            public readonly DateTime LastRunTime;

            public RunRequest(Guid userId, DateTime lastRunTime)
            {
                UserId = userId;
                LastRunTime = lastRunTime;
            }
        }

        private void RunAsync(object asyncState)
        {
            const string method = "RunAsync";
            var request = (RunRequest)asyncState;

            #region Log
            _logger.Raise(Event.Flow, method, Event.Arg("userId", request.UserId));
            #endregion

            try
            {
                // Turn "off" by simply not running at the moment.

                //_worker.Run(request.UserId, request.LastRunTime);
            }
            catch (Exception e)
            {
                #region Log
                _logger.Raise(Event.Error, method, e, null, Event.Arg("userId", request.UserId));
                #endregion
            }
        }
    }
}