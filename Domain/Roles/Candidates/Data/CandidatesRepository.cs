using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Candidates.Data
{
    public class CandidatesRepository
        : Repository, ICandidatesRepository
    {
        private static readonly CandidateStatus[] WorkflowStatusList = new[] { CandidateStatus.AvailableNow, CandidateStatus.ActivelyLooking };

        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private static readonly DataLoadOptions CandidateLoadOptions;

        private static readonly Func<CandidatesDataContext, Guid, CandidateWorkflowEntity> GetWorkflowEntity
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from w in dc.CandidateWorkflowEntities
                    where w.candidateId == candidateId
                    select w).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, Guid?> GetStatusWorkflowId
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from w in dc.CandidateWorkflowEntities
                    where w.candidateId == candidateId
                    select w.statusWorkflowId).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, Guid?> GetSuggestedJobsWorkflowId
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from w in dc.CandidateWorkflowEntities
                    where w.candidateId == candidateId
                    select w.suggestedJobsWorkflowId).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, Guid?> GetActivationEmailWorkflowId
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from w in dc.CandidateWorkflowEntities
                    where w.candidateId == candidateId
                    select w.activationEmailWorkflowId).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, IQueryable<Tuple<Guid, CandidateStatus>>> GetCandidatesWithoutStatusWorkflow
            = CompiledQuery.Compile((CandidatesDataContext dc)
                => from c in dc.CandidateEntities
                   where Equals(c.CandidateWorkflowEntity.statusWorkflowId, null)
                   && WorkflowStatusList.Contains((CandidateStatus)c.status)
                   select Tuple.Create(c.id, (CandidateStatus)c.status));

        private static readonly Func<CandidatesDataContext, IQueryable<Tuple<Guid, CandidateStatus>>> GetCandidatesWithoutSuggestedJobsWorkflow
            = CompiledQuery.Compile((CandidatesDataContext dc)
                => from c in dc.CandidateEntities
                   where c.CandidateWorkflowEntity.suggestedJobsWorkflowId == null
                   select Tuple.Create(c.id, (CandidateStatus)c.status));

        private static readonly Func<CandidatesDataContext, Guid, IIndustriesQuery, Candidate> GetCandidateQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id, IIndustriesQuery industriesQuery)
                => (from c in dc.CandidateEntities
                    where c.id == id
                    let resumeId = (from r in dc.CandidateResumeEntities
                                    where r.candidateId == c.id
                                    select (Guid?)r.resumeId).SingleOrDefault()
                    select c.Map(resumeId, industriesQuery)).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, Guid, CandidateResumeEntity> GetCandidateResumeEntity
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId, Guid resumeId)
                => (from c in dc.CandidateResumeEntities
                    where c.candidateId == candidateId
                    && c.resumeId == resumeId
                    select c).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, IQueryable<RelocationLocationEntity>> GetRelocationLocationEntitiesQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => from c in dc.RelocationLocationEntities
                   where c.candidateId == candidateId
                   && c.LocationReferenceEntity != null
                   select c);

        private static readonly Func<CandidatesDataContext, Guid, ILocationQuery, IQueryable<LocationReference>> GetRelocationLocationsQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId, ILocationQuery locationQuery)
                => from c in dc.RelocationLocationEntities
                   where c.candidateId == candidateId
                   && c.LocationReferenceEntity != null
                   select c.LocationReferenceEntity.Map(locationQuery));

        private static readonly Func<CandidatesDataContext, Guid, IQueryable<CandidateIndustryEntity>> GetCandidateIndustryEntitiesQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => from c in dc.CandidateIndustryEntities
                   where c.candidateId == candidateId
                   select c);

        private static readonly Func<CandidatesDataContext, Guid, IIndustriesQuery, IQueryable<Industry>> GetIndustriesQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId, IIndustriesQuery industriesQuery)
                => from c in dc.CandidateIndustryEntities
                   where c.candidateId == candidateId
                   select industriesQuery.GetIndustry(c.industryId));

        private static readonly Func<CandidatesDataContext, string, ILocationQuery, IQueryable<Tuple<Guid, LocationReference>>> GetAllRelocationLocationsQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, string candidateIds, ILocationQuery locationQuery)
                => from c in dc.RelocationLocationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, candidateIds) on c.candidateId equals i.value
                   where c.LocationReferenceEntity != null
                   select new Tuple<Guid, LocationReference>(c.candidateId, c.LocationReferenceEntity.Map(locationQuery)));

        private static readonly Func<CandidatesDataContext, string, IIndustriesQuery, IQueryable<Tuple<Guid, Industry>>> GetAllIndustriesQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, string candidateIds, IIndustriesQuery industriesQuery)
                => from c in dc.CandidateIndustryEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, candidateIds) on c.candidateId equals i.value
                   select new Tuple<Guid, Industry>(c.candidateId, industriesQuery.GetIndustry(c.industryId)));

        private static readonly Func<CandidatesDataContext, Guid, CandidateEntity> GetCandidateEntityQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id)
                => (from c in dc.CandidateEntities
                    where c.id == id
                    select c).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, string, IIndustriesQuery, IQueryable<Candidate>> GetCandidatesQuery
            = CompiledQuery.Compile((CandidatesDataContext dc, string ids, IIndustriesQuery industriesQuery)
                => from c in dc.CandidateEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on c.id equals i.value
                   let resumeId = (from r in dc.CandidateResumeEntities
                                   where r.candidateId == c.id
                                   select (Guid?)r.resumeId).SingleOrDefault()
                   select c.Map(resumeId, industriesQuery));

        private static readonly Func<CandidatesDataContext, Guid, Diary> GetDiary
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id)
                => (from d in dc.CandidateDiaryEntities
                    where d.id == id
                    select d.Map()).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, Diary> GetDiaryByCandidateId
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from c in dc.CandidateEntities
                    join d in dc.CandidateDiaryEntities on c.diaryId equals d.id
                    where c.id == candidateId
                    select d.Map()).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, CandidateDiaryEntryEntity> GetDiaryEntryEntity
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id)
                => (from e in dc.CandidateDiaryEntryEntities
                    where e.id == id
                    select e).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, DiaryEntry> GetDiaryEntry
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id)
                => (from e in dc.CandidateDiaryEntryEntities
                    where e.id == id
                    && !e.deleted
                    select e.Map()).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, IQueryable<DiaryEntry>> GetDiaryEntries
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid diaryId)
                => from e in dc.CandidateDiaryEntryEntities
                   where e.diaryId == diaryId
                   && !e.deleted
                   orderby e.startTime descending
                   select e.Map());

        private static readonly Func<CandidatesDataContext, Guid, CandidateResumeFileEntity> GetResumeFileEntity
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid id)
                => (from f in dc.CandidateResumeFileEntities
                    where f.id == id
                    select f).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, Guid, ResumeFileReference> GetResumeFile
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId, Guid fileReferenceId)
                => (from f in dc.CandidateResumeFileEntities
                    where f.candidateId == candidateId
                    && f.fileId == fileReferenceId
                    select f.Map()).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, ResumeFileReference> GetResumeFileByResumeId
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid resumeId)
                => (from r in dc.CandidateResumeEntities
                    join f in dc.CandidateResumeFileEntities on r.parsedFromFileId equals f.id
                    where r.resumeId == resumeId
                    select f.Map()).SingleOrDefault());

        private static readonly Func<CandidatesDataContext, Guid, bool> HasResumeFiles
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from f in dc.CandidateResumeFileEntities
                    where f.candidateId == candidateId
                    select f).Any());

        private static readonly Func<CandidatesDataContext, Guid, IQueryable<ResumeFileReference>> GetResumeFiles
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => from f in dc.CandidateResumeFileEntities
                   where f.candidateId == candidateId
                   orderby f.lastUsedTime descending
                   select f.Map());

        private static readonly Func<CandidatesDataContext, Guid, ResumeFileReference> GetLastUsedResumeFile
            = CompiledQuery.Compile((CandidatesDataContext dc, Guid candidateId)
                => (from l in
                        (from f in dc.CandidateResumeFileEntities
                         where f.candidateId == candidateId
                         orderby f.lastUsedTime descending
                         select f).Take(1)
                    select l.Map()).SingleOrDefault());

        static CandidatesRepository()
        {
            CandidateLoadOptions = new DataLoadOptions();
            CandidateLoadOptions.LoadWith<CandidateEntity>(c => c.RelocationLocationEntities);
            CandidateLoadOptions.LoadWith<CandidateEntity>(c => c.CandidateIndustryEntities);
            CandidateLoadOptions.LoadWith<RelocationLocationEntity>(r => r.LocationReferenceEntity);
            CandidateLoadOptions.LoadWith<CandidateEntity>(c => c.CandidateResumeEntities);
        }

        public CandidatesRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
            : base(dataContextFactory)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
        }

        void ICandidatesRepository.CreateCandidate(Candidate candidate)
        {
            // Due to some issues with deadlocks do all these separately.

            using (var dc = CreateContext())
            {
                dc.CandidateEntities.InsertOnSubmit(candidate.Map());
                dc.SubmitChanges();
            }

            InsertRelocations(candidate);
            InsertIndustries(candidate);
        }

        void ICandidatesRepository.UpdateCandidate(Candidate candidate)
        {
            // Due to some issues with deadlocks do all these separately.

            using (var dc = CreateContext())
            {
                var entity = GetCandidateEntity(dc, candidate.Id);
                if (entity != null)
                {
                    candidate.MapTo(entity);
                    dc.SubmitChanges();
                }
            }

            DeleteRelocations(candidate.Id);
            InsertRelocations(candidate);
            DeleteIndustries(candidate.Id);
            InsertIndustries(candidate);
        }

        Candidate ICandidatesRepository.GetCandidate(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCandidate(dc, id);
            }
        }

        IList<Candidate> ICandidatesRepository.GetCandidates(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCandidates(dc, ids);
            }
        }

        void ICandidatesRepository.AddResume(Guid candidateId, Guid resumeId, Guid? parsedFromFileId)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateResumeEntities.InsertOnSubmit(new CandidateResumeEntity { candidateId = candidateId, resumeId = resumeId, parsedFromFileId = parsedFromFileId });
                dc.SubmitChanges();
            }
        }

        void ICandidatesRepository.UpdateResume(Guid candidateId, Guid resumeId, Guid? parsedFromFileId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCandidateResumeEntity(dc, candidateId, resumeId);
                if (entity != null)
                {
                    entity.parsedFromFileId = parsedFromFileId;
                    dc.SubmitChanges();
                }
            }
        }

        void ICandidatesRepository.AddStatusWorkflow(Guid candidateId, Guid workflowId)
        {
            using (var dc = CreateContext())
            {
                var workflow = GetWorkflowEntity(dc, candidateId);
                if (workflow == null)
                {
                    workflow = new CandidateWorkflowEntity { candidateId = candidateId, statusWorkflowId = workflowId };
                    dc.CandidateWorkflowEntities.InsertOnSubmit(workflow);
                }
                else
                {
                    workflow.statusWorkflowId = workflowId;
                }

                dc.SubmitChanges();
            }
        }

        void ICandidatesRepository.DeleteStatusWorkflow(Guid candidateId)
        {
            using (var dc = CreateContext())
            {
                var workflow = GetWorkflowEntity(dc, candidateId);
                if (workflow != null)
                {
                    workflow.statusWorkflowId = null;
                    dc.SubmitChanges();
                }
            }
        }

        Guid? ICandidatesRepository.GetStatusWorkflowId(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetStatusWorkflowId(dc, candidateId);
            }
        }

        IList<Tuple<Guid, CandidateStatus>> ICandidatesRepository.GetCandidatesWithoutStatusWorkflow()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCandidatesWithoutStatusWorkflow(dc).ToList();
            }
        }

        void ICandidatesRepository.AddSuggestedJobsWorkflow(Guid candidateId, Guid workflowId)
        {
            using (var dc = CreateContext())
            {
                var workflow = GetWorkflowEntity(dc, candidateId);
                if (workflow == null)
                {
                    workflow = new CandidateWorkflowEntity { candidateId = candidateId, suggestedJobsWorkflowId = workflowId };
                    dc.CandidateWorkflowEntities.InsertOnSubmit(workflow);
                }
                else
                {
                    workflow.suggestedJobsWorkflowId = workflowId;
                }

                dc.SubmitChanges();
            }
        }

        void ICandidatesRepository.DeleteSuggestedJobsWorkflow(Guid candidateId)
        {
            using (var dc = CreateContext())
            {
                var workflow = GetWorkflowEntity(dc, candidateId);
                if (workflow != null)
                {
                    workflow.suggestedJobsWorkflowId = null;
                    dc.SubmitChanges();
                }
            }
        }

        Guid? ICandidatesRepository.GetSuggestedJobsWorkflowId(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetSuggestedJobsWorkflowId(dc, candidateId);
            }
        }

        IList<Tuple<Guid, CandidateStatus>> ICandidatesRepository.GetCandidatesWithoutSuggestedJobsWorkflow()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCandidatesWithoutSuggestedJobsWorkflow(dc).ToList();
            }
        }

        void ICandidatesRepository.AddActivationEmailWorkflow(Guid candidateId, Guid workflowId)
        {
            using (var dc = CreateContext())
            {
                var workflow = GetWorkflowEntity(dc, candidateId);
                if (workflow == null)
                {
                    workflow = new CandidateWorkflowEntity { candidateId = candidateId, activationEmailWorkflowId = workflowId };
                    dc.CandidateWorkflowEntities.InsertOnSubmit(workflow);
                }
                else
                {
                    workflow.activationEmailWorkflowId = workflowId;
                }

                dc.SubmitChanges();
            }
        }

        void ICandidatesRepository.DeleteActivationEmailWorkflow(Guid candidateId)
        {
            using (var dc = CreateContext())
            {
                var workflow = GetWorkflowEntity(dc, candidateId);
                if (workflow != null)
                {
                    workflow.activationEmailWorkflowId = null;
                    dc.SubmitChanges();
                }
            }
        }

        Guid? ICandidatesRepository.GetActivationEmailWorkflowId(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetActivationEmailWorkflowId(dc, candidateId);
            }
        }

        void ICandidatesRepository.CreateDiary(Guid candidateId, Diary diary)
        {
            using (var dc = CreateContext())
            {
                // Assign the diary to the candidate.

                var candidate = GetCandidateEntity(dc, candidateId);
                if (candidate != null)
                    candidate.diaryId = diary.Id;

                // Insert the diary entity itself.

                dc.CandidateDiaryEntities.InsertOnSubmit(diary.Map());
                dc.SubmitChanges();
            }
        }

        Diary ICandidatesRepository.GetDiary(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDiary(dc, id);
            }
        }

        Diary ICandidatesRepository.GetDiaryByCandidateId(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDiaryByCandidateId(dc, candidateId);
            }
        }

        void ICandidatesRepository.CreateDiaryEntry(Guid diaryId, DiaryEntry entry)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateDiaryEntryEntities.InsertOnSubmit(entry.Map(diaryId));
                dc.SubmitChanges();
            }
        }

        void ICandidatesRepository.UpdateDiaryEntry(DiaryEntry entry)
        {
            using (var dc = CreateContext())
            {
                var entity = GetDiaryEntryEntity(dc, entry.Id);
                if (entity != null)
                {
                    entry.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void ICandidatesRepository.DeleteDiaryEntry(Guid entryId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetDiaryEntryEntity(dc, entryId);
                if (entity != null)
                {
                    entity.deleted = true;
                    dc.SubmitChanges();
                }
            }
        }

        DiaryEntry ICandidatesRepository.GetDiaryEntry(Guid entryId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDiaryEntry(dc, entryId);
            }
        }

        IList<DiaryEntry> ICandidatesRepository.GetDiaryEntries(Guid diaryId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDiaryEntries(dc, diaryId).ToList();
            }
        }

        void ICandidatesRepository.CreateResumeFile(Guid candidateId, ResumeFileReference resumeFileReference)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateResumeFileEntities.InsertOnSubmit(resumeFileReference.Map(candidateId));
                dc.SubmitChanges();
            }
        }

        void ICandidatesRepository.UpdateResumeFile(ResumeFileReference resumeFileReference)
        {
            using (var dc = CreateContext())
            {
                var entity = GetResumeFileEntity(dc, resumeFileReference.Id);
                if (entity != null)
                {
                    resumeFileReference.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        ResumeFileReference ICandidatesRepository.GetResumeFile(Guid candidateId, Guid fileReferenceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResumeFile(dc, candidateId, fileReferenceId);
            }
        }

        ResumeFileReference ICandidatesRepository.GetResumeFile(Guid resumeId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResumeFileByResumeId(dc, resumeId);
            }
        }

        bool ICandidatesRepository.HasResumeFiles(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return HasResumeFiles(dc, candidateId);
            }
        }

        IList<ResumeFileReference> ICandidatesRepository.GetResumeFiles(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResumeFiles(dc, candidateId).ToList();
            }
        }

        ResumeFileReference ICandidatesRepository.GetLastUsedResumeFile(Guid candidateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLastUsedResumeFile(dc, candidateId);
            }
        }

        private static CandidateEntity GetCandidateEntity(CandidatesDataContext dc, Guid id)
        {
            return GetCandidateEntityQuery(dc, id);
        }

        private Candidate GetCandidate(CandidatesDataContext dc, Guid id)
        {
            var candidate = GetCandidateQuery(dc, id, _industriesQuery);
            if (candidate == null)
                return null;
            candidate.RelocationLocations = GetList(GetRelocationLocationsQuery(dc, id, _locationQuery).ToList());
            candidate.Industries = GetList(GetIndustriesQuery(dc, id, _industriesQuery).ToList());
            return candidate;
        }

        private IList<Candidate> GetCandidates(CandidatesDataContext dc, IEnumerable<Guid> ids)
        {
            var candidateIds = new SplitList<Guid>(ids).ToString();
            var candidates = GetCandidatesQuery(dc, candidateIds, _industriesQuery).ToList();
            GetRelocationLocations(dc, candidateIds, candidates);
            GetIndustries(dc, candidateIds, candidates);
            return candidates;
        }

        private void GetRelocationLocations(CandidatesDataContext dc, string candidateIds, IEnumerable<Candidate> candidates)
        {
            var allLocations = (from l in GetAllRelocationLocationsQuery(dc, candidateIds, _locationQuery)
                                group l by l.Item1).ToDictionary(g => g.Key, g => (from l in g select l.Item2).ToList());
            foreach (var candidate in candidates)
            {
                List<LocationReference> locations;
                if (allLocations.TryGetValue(candidate.Id, out locations))
                    candidate.RelocationLocations = GetList(locations);
            }
        }

        private void GetIndustries(CandidatesDataContext dc, string candidateIds, IEnumerable<Candidate> candidates)
        {
            var allIndustries = (from e in GetAllIndustriesQuery(dc, candidateIds, _industriesQuery)
                                 group e by e.Item1).ToDictionary(g => g.Key, g => (from e in g select e.Item2).ToList());
            foreach (var candidate in candidates)
            {
                List<Industry> industries;
                if (allIndustries.TryGetValue(candidate.Id, out industries))
                    candidate.Industries = GetList(industries);
            }
        }

        private void DeleteRelocations(Guid candidateId)
        {
            using (var dc = CreateContext())
            {
                dc.LoadOptions = CandidateLoadOptions;
                var entities = GetRelocationLocationEntitiesQuery(dc, candidateId).ToList();
                if (entities.Count > 0)
                {
                    dc.LocationReferenceEntities.DeleteAllOnSubmit(from e in entities where e.LocationReferenceEntity != null select e.LocationReferenceEntity);
                    dc.RelocationLocationEntities.DeleteAllOnSubmit(entities);
                    dc.SubmitChanges();
                }
            }
        }

        private void DeleteIndustries(Guid candidateId)
        {
            using (var dc = CreateContext())
            {
                dc.LoadOptions = CandidateLoadOptions;
                var entities = GetCandidateIndustryEntitiesQuery(dc, candidateId).ToList();
                if (entities.Count > 0)
                {
                    dc.CandidateIndustryEntities.DeleteAllOnSubmit(entities);
                    dc.SubmitChanges();
                }
            }
        }

        private void InsertRelocations(Candidate candidate)
        {
            if (candidate.RelocationLocations != null && candidate.RelocationLocations.Count > 0)
            {
                using (var dc = CreateContext())
                {
                    dc.RelocationLocationEntities.InsertAllOnSubmit(candidate.RelocationLocations.Map(candidate.Id));
                    dc.SubmitChanges();
                }
            }
        }

        private void InsertIndustries(Candidate candidate)
        {
            if (candidate.Industries != null && candidate.Industries.Count > 0)
            {
                using (var dc = CreateContext())
                {
                    dc.CandidateIndustryEntities.InsertAllOnSubmit(candidate.Industries.Map(candidate.Id));
                    dc.SubmitChanges();
                }
            }
        }

        private static IList<T> GetList<T>(IList<T> list)
        {
            return list.Count == 0 ? null : list;
        }

        private CandidatesDataContext CreateContext()
        {
            return CreateContext(c => new CandidatesDataContext(c));
        }
    }
}
