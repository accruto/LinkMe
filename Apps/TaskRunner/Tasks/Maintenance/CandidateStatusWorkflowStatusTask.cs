using System;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Workflow.CandidateStatusWorkflow;

namespace LinkMe.TaskRunner.Tasks.Maintenance
{
    public class CandidateStatusWorkflowStatusTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<CandidateStatusWorkflowStatusTask>();

        private readonly IChannelManager<IService> _serviceFactory;

        public CandidateStatusWorkflowStatusTask(IChannelManager<IService> serviceFactory)
            : base(EventSource)
        {
            _serviceFactory = serviceFactory;
        }

        public override void ExecuteTask(string[] args)
        {
            var candidateId = new Guid(args[0]);

            var service = _serviceFactory.Create();
            try
            {
                service.LogWorkflow(candidateId);
                _serviceFactory.Close(service);
            }
            catch (Exception)
            {
                _serviceFactory.Abort(service);
                throw;
            }
        }
    }
}
