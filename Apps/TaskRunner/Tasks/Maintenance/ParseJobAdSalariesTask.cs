using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Services.JobAds.Salaries;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks.Maintenance
{
    class ParseJobAdSalariesTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<ParseJobAdSalariesTask>();

        private readonly IJobAdSalariesParserCommand _jobAdSalariesParserCommand;

        public ParseJobAdSalariesTask(IJobAdSalariesParserCommand jobAdSalariesParserCommand)
            : base(EventSource)
        {
            _jobAdSalariesParserCommand = jobAdSalariesParserCommand;
        }

        public override void ExecuteTask(string[] args)
        {
            var limitToOpenJobAds = true;

            if (args != null && args.Length > 0)
            {
                bool.TryParse(args[0], out limitToOpenJobAds);
            }

            _jobAdSalariesParserCommand.ParseJobAdSalaries(limitToOpenJobAds);
        }
    }
}
