using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Domain;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.TaskRunner.Tasks.Maintenance.Data;

namespace LinkMe.TaskRunner.Tasks.Maintenance
{
    public class MissingCandidatesTask
        : Task
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private static readonly EventSource EventSource = new EventSource<MissingCandidatesTask>();

        public MissingCandidatesTask(IDbConnectionFactory connectionFactory, IExecuteMemberSearchCommand executeMemberSearchCommand)
            : base(EventSource)
        {
            _connectionFactory = connectionFactory;
            _executeMemberSearchCommand = executeMemberSearchCommand;
        }

        public override void ExecuteTask()
        {
            const string method = "ExecuteTask";

            var searchableMembers = GetSearchableMembers();
            EventSource.Raise(Event.Information, method, "Searchable members: " + searchableMembers.Count);

            var searchEngineMembers = GetSearchEngineMembers();
            EventSource.Raise(Event.Information, method, "Search engine members: " + searchEngineMembers.Count);

            IEnumerable<Guid> missingMembers;
            if (searchEngineMembers.Count > searchableMembers.Count)
            {
                missingMembers = searchEngineMembers.Except(searchEngineMembers);
                EventSource.Raise(Event.Information, method, "Members in the search engine not searchable in the database: " + missingMembers.Count());
            }
            else
            {
                missingMembers = searchEngineMembers.Except(searchEngineMembers);
                EventSource.Raise(Event.Information, method, "Members searchable in the database not in the search engine: " + missingMembers.Count());
            }

            Console.WriteLine();

            // Write them out.

            using (var writer = new StreamWriter("SearchableMembers.txt"))
            {
                foreach (var searchableMember in searchableMembers)
                    writer.WriteLine(searchableMember);
            }

            using (var writer = new StreamWriter("SearchEngineMembers.txt"))
            {
                foreach (var searchEngineMember in searchEngineMembers)
                    writer.WriteLine(searchEngineMember);
            }

            using (var writer = new StreamWriter("MissingMembers.txt"))
            {
                foreach (var missingMember in missingMembers)
                    writer.WriteLine(missingMember);
            }
        }

        private IList<Guid> GetSearchableMembers()
        {
            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                dc.CommandTimeout = (int) new TimeSpan(1, 0, 0).TotalSeconds;
                return (from m in dc.SearchableMemberEntities select m.id).ToList();
            }
        }

        private IList<Guid> GetSearchEngineMembers()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("*");
            criteria.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.AvailableNow | CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.Unspecified;
            criteria.HasResume = true;
            criteria.IsActivated = true;
            criteria.IsContactable = true;

            // First determine how many there are.

            var execution = _executeMemberSearchCommand.Search(null, criteria, new Range(0, 1));
            var total = execution.Results.TotalMatches;

            // Need to page.

            var memberIds = new List<Guid>();
            var start = 0;
            const int page = 1000;
            while (start < total)
            {
                execution = _executeMemberSearchCommand.Search(null, criteria, new Range(start, page));
                memberIds.AddRange(execution.Results.MemberIds);

                start += page;
            }

            return memberIds;
        }
    }
}
