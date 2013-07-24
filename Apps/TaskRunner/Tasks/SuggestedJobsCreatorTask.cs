using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Instrumentation;
using LinkMe.Workflow.PeriodicWorkflow;

namespace LinkMe.TaskRunner.Tasks
{
    public class SuggestedJobsCreatorTask
        : Task
    {
        private static readonly EventSource Logger = new EventSource<SuggestedJobsCreatorTask>();

        private readonly ICandidatesWorkflowQuery _candidatesWorkflowQuery;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly Guid _categoryId;

        public SuggestedJobsCreatorTask(ICandidatesWorkflowQuery candidatesWorkflowQuery, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand)
            : base(Logger)
        {
            _candidatesWorkflowQuery = candidatesWorkflowQuery;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
            _categoryId = _settingsQuery.GetCategory("SuggestedJobs").Id;
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

            IEnumerable<Tuple<Guid, CandidateStatus>> candidates = _candidatesWorkflowQuery.GetCandidatesWithoutSuggestedJobsWorkflow();

            if (count.HasValue && count.Value != 0)
                candidates = candidates.Take(count.Value);

            Logger.Raise(Event.Information, method, string.Format("Creating SuggestedJobs workflows for {0} candidates...", candidates.Count()));

            foreach (var candidate in candidates)
            {
                // Set the email frequency. This will indirectly result in workflow creation.

                _settingsCommand.SetFrequency(candidate.Item1, _categoryId, candidate.Item2.SuggestedJobsFrequency());

                // Wait a little to avoid a burst in workflow activities.

                if (slowdownMilliseconds.HasValue && slowdownMilliseconds.Value != 0)
                    Thread.Sleep(slowdownMilliseconds.Value);
            }
        }
    }
}