using System;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Sql;
using LinkMe.TaskRunner.Tasks.Maintenance.Data;

namespace LinkMe.TaskRunner.Tasks.Maintenance
{
    public class DeleteOldTinyUrlsTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<DeleteOldTinyUrlsTask>();
        private readonly IDbConnectionFactory _connectionFactory;

        public DeleteOldTinyUrlsTask(IDbConnectionFactory connectionFactory)
            : base(EventSource)
        {
            _connectionFactory = connectionFactory;
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";

            if (args.Length < 2)
                throw new ArgumentException("Need at least 2 arguments, \"days batch <iterations>\"");

            var days = int.Parse(args[0]);
            var batch = int.Parse(args[1]);
            var iterations = args.Length > 2 ? int.Parse(args[2]) : 1;
            var total = 0;

            EventSource.Raise(Event.Information, method, "Starting to delete old tiny urls.", Event.Arg("days", days), Event.Arg("batch", batch), Event.Arg("iterations", iterations));

            for (var iteration = 0; iteration < iterations; ++iteration)
            {
                EventSource.Raise(Event.Information, method, "Deleting old tiny urls.", Event.Arg("days", days), Event.Arg("batch", batch), Event.Arg("iteration", iteration));

                using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
                {
                    dc.CommandTimeout = (int) new TimeSpan(2, 0, 0).TotalSeconds;
                    var count = dc.DeleteOldTinyUrls(days, batch);
                    total += count;

                    EventSource.Raise(Event.Information, method, "Old tiny urls deleted.", Event.Arg("count", count), Event.Arg("total", total));
                }
            }
        }
    }
}
