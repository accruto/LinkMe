using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Reports.DailyReports.Queries;

namespace LinkMe.TaskRunner.Tasks
{
	public class ReportingEmailTask : Task
	{
	    private readonly IDailyReportsQuery _dailyReportsQuery = Container.Current.Resolve<IDailyReportsQuery>();
	    private readonly IEmailsCommand _emailsCommand = Container.Current.Resolve<IEmailsCommand>();

        private static readonly EventSource EventSource = new EventSource(typeof(ReportingEmailTask));

	    public ReportingEmailTask()
	        : base(EventSource)
	    {
	    }

        public override void ExecuteTask()
		{
            // Generate the general one.

            var day = DayRange.Yesterday;
            _emailsCommand.TrySend(new StatsEmail(_dailyReportsQuery.GetDailyReport(day)));
		}
    }
}
