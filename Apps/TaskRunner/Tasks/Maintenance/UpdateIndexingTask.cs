using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Sql;
using LinkMe.TaskRunner.Tasks.Maintenance.Data;

namespace LinkMe.TaskRunner.Tasks.Maintenance
{
    public class UpdateIndexingTask
        : Task
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private static readonly EventSource EventSource = new EventSource<UpdateIndexingTask>();

        public UpdateIndexingTask(IDbConnectionFactory connectionFactory)
            : base(EventSource)
        {
            _connectionFactory = connectionFactory;
        }

        public override void ExecuteTask(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException(string.Format("Syntax:\r\n{0} age", Assembly.GetEntryAssembly().GetName().Name));

            var age = DateTime.Now.Date.AddDays(-1 * int.Parse(args[0]));

            UpdateMemberIndexing(age);
            UpdateJobAdIndexing(age);
        }

        private void UpdateMemberIndexing(DateTime minLastUpdatedTime)
        {
            const string method = "UpdateMemberIndexing";

            var memberIds = GetOutdatedMemberIds(minLastUpdatedTime);

            EventSource.Raise(Event.Information, method, "Updating indexes for " + memberIds.Count + " members.");

            // Give enough time for the indexing to notice.

            var modifiedTime = DateTime.Now.AddMinutes(5);
            foreach (var memberId in memberIds)
                UpdateMemberIndexing(memberId, modifiedTime);

            EventSource.Raise(Event.Information, method, "Updated indexes for " + memberIds.Count + " members.");
        }

        private void UpdateMemberIndexing(Guid memberId, DateTime modifiedTime)
        {
            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = (from i in dc.MemberIndexingEntities where i.memberId == memberId select i).SingleOrDefault();
                if (entity == null)
                    dc.MemberIndexingEntities.InsertOnSubmit(new MemberIndexingEntity { memberId = memberId, modifiedTime = modifiedTime });
                else
                    entity.modifiedTime = modifiedTime;
                dc.SubmitChanges();
            }
        }

        private void UpdateJobAdIndexing(DateTime minLastUpdatedTime)
        {
            const string method = "UpdateJobAdIndexing";
            EventSource.Raise(Event.Information, method, "Updating job ad indexing.", Event.Arg("minLastUpdatedTime", minLastUpdatedTime));

            var jobAdIds = GetOutdatedJobAdIds(minLastUpdatedTime);

            EventSource.Raise(Event.Information, method, "Updating indexes for " + jobAdIds.Count + " job ads.");

            // Give enough time for the indexing to notice.

            var modifiedTime = DateTime.Now.AddMinutes(5);
            foreach (var jobAdId in jobAdIds)
                UpdateJobAdIndexing(jobAdId, modifiedTime);

            EventSource.Raise(Event.Information, method, "Updated indexes for " + jobAdIds.Count + " job ads.");
        }

        private void UpdateJobAdIndexing(Guid jobAdId, DateTime modifiedTime)
        {
            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = (from i in dc.JobAdIndexingEntities where i.jobAdId == jobAdId select i).SingleOrDefault();
                if (entity == null)
                    dc.JobAdIndexingEntities.InsertOnSubmit(new JobAdIndexingEntity { jobAdId = jobAdId, modifiedTime = modifiedTime });
                else
                    entity.modifiedTime = modifiedTime;
                dc.SubmitChanges();
            }
        }

        private IList<Guid> GetOutdatedMemberIds(DateTime minLastUpdatedTime)
        {
            const string method = "GetOutdatedMemberIds";
            EventSource.Raise(Event.Information, method, "Getting outdated members.", Event.Arg("minLastUpdatedTime", minLastUpdatedTime));

            var memberIds = new List<Guid>();

            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                dc.CommandTimeout = (int)new TimeSpan(1, 0, 0).TotalSeconds;

                // Choose all members who have a resume but do not have a indexing entry.

                var outdatedMemberIds = (from m in dc.MemberEntities
                                         join c in dc.CandidateEntities on m.id equals c.id
                                         join cr in dc.CandidateResumeEntities on c.id equals cr.candidateId
                                         join r in dc.ResumeEntities on cr.resumeId equals r.id
                                         where !(from i in dc.MemberIndexingEntities where i.memberId == m.id select m).Any()
                                         && dc.GetLatestDate(m.lastEditedTime, c.lastEditedTime, r.lastEditedTime) > minLastUpdatedTime
                                         select m.id).ToList();
                memberIds = memberIds.Concat(outdatedMemberIds).ToList();

                EventSource.Raise(Event.Information, method, "Found " + outdatedMemberIds.Count + " members who have a resume but do not have an indexing entry.");

                // Choose all members who don't have a resume and do not have an indexing entry.

                outdatedMemberIds = (from m in dc.MemberEntities
                                     join c in dc.CandidateEntities on m.id equals c.id
                                     where !(from cr in dc.CandidateResumeEntities
                                             join r in dc.ResumeEntities on cr.resumeId equals r.id
                                             where cr.candidateId == c.id
                                             select r).Any()
                                     && !(from i in dc.MemberIndexingEntities where i.memberId == m.id select m).Any()
                                     && dc.GetLatestDate(m.lastEditedTime, c.lastEditedTime, null) > minLastUpdatedTime
                                     select m.id).ToList();
                memberIds = memberIds.Concat(outdatedMemberIds).ToList();

                EventSource.Raise(Event.Information, method, "Found " + outdatedMemberIds.Count + " members who do not have a resume and do not have an indexing entry.");

                // Choose all members who have a resume and are out of date.

                outdatedMemberIds = (from m in dc.MemberEntities
                                     join c in dc.CandidateEntities on m.id equals c.id
                                     join cr in dc.CandidateResumeEntities on c.id equals cr.candidateId
                                     join r in dc.ResumeEntities on cr.resumeId equals r.id
                                     join i in dc.MemberIndexingEntities on m.id equals i.memberId
                                     where dc.GetLatestDate(m.lastEditedTime, c.lastEditedTime, r.lastEditedTime) > minLastUpdatedTime
                                     && dc.GetLatestDate(m.lastEditedTime, c.lastEditedTime, r.lastEditedTime) > i.modifiedTime
                                     select m.id).ToList();
                memberIds = memberIds.Concat(outdatedMemberIds).ToList();

                EventSource.Raise(Event.Information, method, "Found " + outdatedMemberIds.Count + " members who have a resume and are out of date.");

                // Choose all members who don't have a resume and are out of date.

                outdatedMemberIds = (from m in dc.MemberEntities
                                     join c in dc.CandidateEntities on m.id equals c.id
                                     join i in dc.MemberIndexingEntities on m.id equals i.memberId
                                     where !(from cr in dc.CandidateResumeEntities
                                             join r in dc.ResumeEntities on cr.resumeId equals r.id
                                             where cr.candidateId == c.id
                                             select r).Any()
                                     where dc.GetLatestDate(m.lastEditedTime, c.lastEditedTime, null) > minLastUpdatedTime
                                     && dc.GetLatestDate(m.lastEditedTime, c.lastEditedTime, null) > i.modifiedTime
                                     select m.id).ToList();
                memberIds = memberIds.Concat(outdatedMemberIds).ToList();

                EventSource.Raise(Event.Information, method, "Found " + outdatedMemberIds.Count + " members who have don't have a resume and are out of date.");
            }

            return memberIds;
        }

        private IList<Guid> GetOutdatedJobAdIds(DateTime minLastUpdatedTime)
        {
            const string method = "GetOutdatedJobAdIds";
            EventSource.Raise(Event.Information, method, "Getting outdated job ads.", Event.Arg("minLastUpdatedTime", minLastUpdatedTime));

            var jobAdIds = new List<Guid>();

            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                dc.CommandTimeout = (int)new TimeSpan(1, 0, 0).TotalSeconds;

                // Choose all job ads which do not have an indexing entry.

                var outdatedJobAdIds = (from j in dc.JobAdEntities
                                        where !(from i in dc.JobAdIndexingEntities where i.jobAdId == j.id select j).Any()
                                        && j.lastUpdatedTime >= minLastUpdatedTime
                                        select j.id).ToList();
                jobAdIds = jobAdIds.Concat(outdatedJobAdIds).ToList();

                EventSource.Raise(Event.Information, method, "Found " + outdatedJobAdIds.Count + " job ads which do not have an indexing entry.");

                // Choose all job ads that are out of date.

                outdatedJobAdIds = (from j in dc.JobAdEntities
                                    join i in dc.JobAdIndexingEntities on j.id equals i.jobAdId
                                    where j.lastUpdatedTime > i.modifiedTime
                                    && j.lastUpdatedTime > minLastUpdatedTime
                                    select j.id).ToList();
                jobAdIds = jobAdIds.Concat(outdatedJobAdIds).ToList();

                EventSource.Raise(Event.Information, method, "Found " + outdatedJobAdIds.Count + " job ads which are out of date.");
            }

            return jobAdIds;
        }
    }
}
