using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.Candidates.Data
{
    public class CandidateReportsRepository
        : ReportsRepository<CandidatesDataContext>, ICandidateReportsRepository
    {
        [Flags]
        private enum UserFlags
        {
            Disabled = 0x04,
        }

        private enum EventType
        {
            UploadResume = 8,
            EditResume = 9,
            ReloadResume = 17,
        }
        
        private static readonly Func<CandidatesDataContext, DateTime, int> GetResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTime time)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    where !(from e in dc.ResumeEventEntities
                            where e.resumeId == r.id
                            && e.time >= time
                            && e.resumeCreated
                            select e).Any()
                    select r).Count());

        private static readonly Func<CandidatesDataContext, DateTime, int> GetSearchableResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTime time)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join m in dc.SearchableMemberEntities on c.candidateId equals m.id
                    where !(from e in dc.ResumeEventEntities
                            where e.resumeId == r.id
                            && e.time >= time
                            && e.resumeCreated
                            select e).Any()
                    select r).Count());

        private static readonly Func<CandidatesDataContext, DateTimeRange, int> GetNewResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTimeRange timeRange)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join e in dc.ResumeEventEntities on r.id equals e.resumeId
                    where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                    && e.resumeCreated
                    select c.candidateId).Distinct().Count());

        private static readonly Func<CandidatesDataContext, DateTimeRange, int> GetUploadedResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTimeRange timeRange)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join e in dc.ResumeEventEntities on r.id equals e.resumeId
                    where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                    && e.eventType == (int) EventType.UploadResume
                    select c.candidateId).Distinct().Count());

        private static readonly Func<CandidatesDataContext, DateTimeRange, int> GetReloadedResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTimeRange timeRange)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join e in dc.ResumeEventEntities on r.id equals e.resumeId
                    where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                    && e.eventType == (int) EventType.ReloadResume
                    select c.candidateId).Distinct().Count());

        private static readonly Func<CandidatesDataContext, DateTimeRange, int> GetEditedResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTimeRange timeRange)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join e in dc.ResumeEventEntities on r.id equals e.resumeId
                    where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                    && e.eventType == (int) EventType.EditResume
                    select c.candidateId).Distinct().Count());
                
        private static readonly Func<CandidatesDataContext, DateTimeRange, int> GetUpdatedResumes
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTimeRange timeRange)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join e in dc.ResumeEventEntities on r.id equals e.resumeId
                    where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                    select c.candidateId).Distinct().Count());

        private static readonly Func<CandidatesDataContext, DateTimeRange, IQueryable<Guid>> GetUpdatedResumeCandidateIds
            = CompiledQuery.Compile((CandidatesDataContext dc, DateTimeRange timeRange)
                => (from r in dc.ResumeEntities
                    join c in dc.CandidateResumeEntities on r.id equals c.resumeId
                    join e in dc.ResumeEventEntities on r.id equals e.resumeId
                    where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                    select c.candidateId).Distinct());

        private static readonly Func<CandidatesDataContext, Guid, ResumeEventEntity> GetResumeEventEntity
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id)
                => (from e in dc.ResumeEventEntities
                    where e.id == id
                    select e).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, IQueryable<ResumeEvent>> GetResumeEvents
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => from e in dc.ResumeEventEntities
                   where e.candidateId == candidateId
                   orderby e.time
                   select e.Map());

        public CandidateReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int ICandidateReportsRepository.GetResumes(DateTime time)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetResumes(dc, time);
            }
        }

        int ICandidateReportsRepository.GetSearchableResumes(DateTime time)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetSearchableResumes(dc, time);
            }
        }

        int ICandidateReportsRepository.GetNewResumes(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetNewResumes(dc, timeRange);
            }
        }

        int ICandidateReportsRepository.GetUploadedResumes(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetUploadedResumes(dc, timeRange);
            }
        }

        int ICandidateReportsRepository.GetReloadedResumes(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetReloadedResumes(dc, timeRange);
            }
        }

        int ICandidateReportsRepository.GetEditedResumes(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetEditedResumes(dc, timeRange);
            }
        }

        int ICandidateReportsRepository.GetUpdatedResumes(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetUpdatedResumes(dc, timeRange);
            }
        }

        IList<Guid> ICandidateReportsRepository.GetUpdatedResumeCandidateIds(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetUpdatedResumeCandidateIds(dc, timeRange).ToList();
            }
        }

        IList<Guid> ICandidateReportsRepository.GetCandidateStatuses(CandidateStatus status)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in dc.RegisteredUserEntities
                        join m in dc.MemberEntities on u.id equals m.id
                        join c in dc.CandidateEntities on m.id equals c.id
                        where (u.flags & (int)UserFlags.Disabled) == 0
                        && c.status == (byte)status
                        select u.id).ToList();
            }
        }

        IList<Guid> ICandidateReportsRepository.GetEthnicStatuses(EthnicStatus status)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in dc.RegisteredUserEntities
                        join m in dc.MemberEntities on u.id equals m.id
                        where (u.flags & (int)UserFlags.Disabled) == 0
                        && m.ethnicFlags == (byte)status
                        select u.id).ToList();
            }
        }

        void ICandidateReportsRepository.CreateResumeEvent(ResumeEvent evt)
        {
            using (var dc = CreateDataContext(false))
            {
                dc.ResumeEventEntities.InsertOnSubmit(evt.Map());
                dc.SubmitChanges();
            }
        }

        void ICandidateReportsRepository.DeleteResumeEvent(Guid evtId)
        {
            using (var dc = CreateDataContext(false))
            {
                var entity = GetResumeEventEntity(dc, evtId);
                if (entity != null)
                {
                    dc.ResumeEventEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        IList<ResumeEvent> ICandidateReportsRepository.GetResumeEvents(Guid candidateId)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetResumeEvents(dc, candidateId).ToList();
            }
        }

        protected override CandidatesDataContext CreateDataContext(IDbConnection connection)
        {
            return new CandidatesDataContext(connection);
        }
    }
}
