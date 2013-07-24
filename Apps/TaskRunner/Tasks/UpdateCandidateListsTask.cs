using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.TaskRunner.Tasks
{
    public class UpdateCandidateListsTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<UpdateCandidateListsTask>();
        private readonly IDbConnectionFactory _connectionFactory;

        public UpdateCandidateListsTask(IDbConnectionFactory connectionFactory)
            : base(EventSource)
        {
            _connectionFactory = connectionFactory;
        }

        public override void ExecuteTask()
        {
            const string method = "ExecuteTask";

            var lists = new List<Tuple<Guid, string>>();

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var reader = DatabaseHelper.ExecuteReader(connection, "SELECT id, name FROM dbo.CandidateList WHERE listType = 6", new TimeSpan(0, 0, 10, 0)))
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid(0);
                        var name = reader.GetString(1);
                        lists.Add(new Tuple<Guid, string>(id, name));
                    }
                }
            }

            EventSource.Raise(Event.Information, method, "Updating " + lists.Count + " candidate lists.");

            var count = 0;
            foreach (var list in lists)
                UpdateCandidateList(ref count, list);
        }

        private void UpdateCandidateList(ref int count, Tuple<Guid, string> list)
        {
            const string method = "UpdateCandidateList";

            // If the name is a GUID it is really a job applicant list.

            if (!IsGuid(list.Item2))
            {
                // Update its type.

                UpdateCandidateList(list.Item1);
                if (++count % 100 == 0)
                    EventSource.Raise(Event.Information, method, "Updated " + count + " candidate lists.");
            }
        }

        private void UpdateCandidateList(Guid id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                DatabaseHelper.ExecuteNonQuery(connection, "UPDATE dbo.CandidateList SET listType = 0 WHERE id = @id", "@id", id);
            }
        }

        private static bool IsGuid(string name)
        {
            try
            {
                new Guid(name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
