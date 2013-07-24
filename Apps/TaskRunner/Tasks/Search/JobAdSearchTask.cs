using System;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;

namespace LinkMe.TaskRunner.Tasks.Search
{
    public class JobAdSearchTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<JobAdSearchTask>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;

        public JobAdSearchTask(IExecuteJobAdSearchCommand executeJobAdSearchCommand)
            : base(EventSource)
        {
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
        }

        public override void ExecuteTask(string[] args)
        {
            var keywords = args[0];
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(keywords);
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Console.WriteLine("Total results: " + execution.Results.TotalMatches);
            foreach (var jobAdId in execution.Results.JobAdIds)
                Console.WriteLine(jobAdId);
        }
    }
}
