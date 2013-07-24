using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Workflow.CandidateStatusWorkflow;

namespace LinkMe.TaskRunner.Tasks
{
    public class CandidateStatusCreatorTask
        : Task
    {
        private static readonly EventSource Logger = new EventSource<CandidateStatusCreatorTask>();

        private readonly IChannelManager<IService> _workflowProxyFactory;
        private readonly ICandidatesWorkflowQuery _candidatesWorkflowQuery;

        public CandidateStatusCreatorTask(IChannelManager<IService> workflowProxyFactory, ICandidatesWorkflowQuery candidatesWorkflowQuery)
            : base(Logger)
        {
            _workflowProxyFactory = workflowProxyFactory;
            _candidatesWorkflowQuery = candidatesWorkflowQuery;
        }

        public override void ExecuteTask()
        {
            ExecuteTask(1000, 1000);
        }

        public override void ExecuteTask(string[] args)
        {
            int? count = null;
            int? slowdownMilliseconds = null;

            if (args.Length > 0)
                count = int.Parse(args[0]);

            if (args.Length > 1)
                slowdownMilliseconds = int.Parse(args[1]);

            ExecuteTask(count, slowdownMilliseconds);
        }

        private void ExecuteTask(int? count, int? slowdownMilliseconds)
        {
            const string method = "ExecuteTask";

            IEnumerable<Tuple<Guid, CandidateStatus>> candidates = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            if (count.HasValue && count.Value != 0)
                candidates = candidates.Take(count.Value);

            Logger.Raise(Event.Information, method, string.Format("Creating CandidateStatus workflows for {0} candidates...", candidates.Count()));

            var workflowProxy = _workflowProxyFactory.Create();
            try
            {
                foreach (var candidate in candidates)
                {
                    workflowProxy.CreateWorkflow(candidate.Item1, candidate.Item2);

                    // Wait a little to avoid a burst in workflow activities.

                    if (slowdownMilliseconds.HasValue && slowdownMilliseconds.Value != 0)
                        Thread.Sleep(slowdownMilliseconds.Value);
                }

                _workflowProxyFactory.Close(workflowProxy);
            }
            catch (Exception)
            {
                _workflowProxyFactory.Abort(workflowProxy);
                throw;
            }
        }
    }
}