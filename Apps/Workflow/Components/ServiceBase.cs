using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Workflow.Components
{
    public abstract class ServiceBase
        : Framework.Host.IChannelAware
    {
        private readonly WorkflowRuntime _workflowRuntime = new WorkflowRuntime();
        private readonly IDbConnectionFactory _connectionFactory;

        #region Optional Dependency Properties

        public TimeSpan OwnershipDuration { private get; set; }
        public TimeSpan PollingInterval { private get; set; }

        #endregion

        protected ServiceBase(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            // Set defaults.

            OwnershipDuration = TimeSpan.FromSeconds(10);
            PollingInterval = TimeSpan.FromMinutes(2);
        }

        public void AddServices(params object[] services)
        {
            foreach (var service in services)
                _workflowRuntime.AddService(service);
        }

        public void AddDataExchange(object instance)
        {
            var service = _workflowRuntime.GetService<ExternalDataExchangeService>();
            if (service == null)
                throw new InvalidOperationException("WorkflowRuntime does not contain ExternalDataExchangeService.");

            service.AddService(instance);
        }

        protected WorkflowInstance CreateWorkflow<T>(Dictionary<string, object> parameters)
        {
            return _workflowRuntime.CreateWorkflow(typeof(T), parameters);
        }

        protected void DeleteWorkflow(Guid instanceId)
        {
            var instance = _workflowRuntime.GetWorkflow(instanceId);
            if (instance != null)
                instance.Terminate("");
        }

        protected WorkflowInstance GetWorkflow(Guid instanceId)
        {
            return _workflowRuntime.GetWorkflow(instanceId);
        }

        protected object GetExecutor(WorkflowInstance instance)
        {
            return _workflowRuntime.GetType().InvokeMember("Load", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, _workflowRuntime, new object[] { instance.InstanceId, null, instance });
        }

        #region Implementation of IChannelAware

        public virtual void OnOpen()
        {
            _workflowRuntime.AddService(new ExternalDataExchangeService());

            var parameters = new NameValueCollection
            {
                {"ConnectionString", _connectionFactory.ConnectionString},
                {"UnloadOnIdle", "true"},
                {"LoadIntervalSeconds", PollingInterval.TotalSeconds.ToString()}
            };

            _workflowRuntime.AddService(new SqlWorkflowPersistenceService(parameters));
        }

        public virtual void OnClose()
        {
            _workflowRuntime.Dispose();
        }

        public virtual void OnStart()
        {
            _workflowRuntime.StartRuntime();
        }

        public virtual void OnStop()
        {
            _workflowRuntime.StopRuntime();
        }

        public virtual void OnPause()
        { }

        public virtual void OnContinue()
        { }

        public virtual void OnShutdown()
        { }

        #endregion
    }
}
