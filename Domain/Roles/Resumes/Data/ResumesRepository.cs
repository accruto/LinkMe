using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Resumes.Data
{
    public class ResumesRepository
        : Repository, IResumesRepository
    {
        private static readonly Func<ResumesDataContext, Guid, ResumeEntity> GetResumeEntity
            = CompiledQuery.Compile((ResumesDataContext dc, Guid id)
                => (from r in dc.ResumeEntities
                    where r.id == id
                    select r).SingleOrDefault());

        private static readonly Func<ResumesDataContext, Guid, Resume> GetResumeQuery
            = CompiledQuery.Compile((ResumesDataContext dc, Guid id)
                => (from r in dc.ResumeEntities
                    where r.id == id
                    select r.Map()).SingleOrDefault());

        private static readonly Func<ResumesDataContext, string, IQueryable<Resume>> GetAllResumesQuery
            = CompiledQuery.Compile((ResumesDataContext dc, string ids)
                => from r in dc.ResumeEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on r.id equals i.value
                   select r.Map());

        private static readonly Func<ResumesDataContext, Guid, IQueryable<Job>> GetJobsQuery
            = CompiledQuery.Compile((ResumesDataContext dc, Guid resumeId)
                => from e in dc.ResumeJobEntities
                   where e.resumeId == resumeId
                   select e.Map());

        private static readonly Func<ResumesDataContext, string, IQueryable<Tuple<Guid, Job>>> GetAllJobsQuery
            = CompiledQuery.Compile((ResumesDataContext dc, string resumeIds)
                => from e in dc.ResumeJobEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, resumeIds) on e.resumeId equals i.value
                   select new Tuple<Guid, Job>(e.resumeId, e.Map()));

        private static readonly Func<ResumesDataContext, Guid, IQueryable<School>> GetSchoolsQuery
            = CompiledQuery.Compile((ResumesDataContext dc, Guid resumeId)
                => from e in dc.ResumeSchoolEntities
                   where e.resumeId == resumeId
                   select e.Map());

        private static readonly Func<ResumesDataContext, string, IQueryable<Tuple<Guid, School>>> GetAllSchoolsQuery
            = CompiledQuery.Compile((ResumesDataContext dc, string resumeIds)
                => from e in dc.ResumeSchoolEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, resumeIds) on e.resumeId equals i.value
                   select new Tuple<Guid, School>(e.resumeId, e.Map()));

        private static readonly Func<ResumesDataContext, Guid, ParsedResumeEntity> GetParsedResumeEntity
            = CompiledQuery.Compile((ResumesDataContext dc, Guid id)
                => (from r in dc.ParsedResumeEntities
                    where r.id == id
                    select r).SingleOrDefault());

        public ResumesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IResumesRepository.CreateResume(Resume resume)
        {
            using (var dc = CreateContext())
            {
                dc.ResumeEntities.InsertOnSubmit(resume.Map());
                dc.SubmitChanges();
            }
        }

        void IResumesRepository.UpdateResume(Resume resume)
        {
            using (var dc = CreateContext())
            {
                var entity = GetResumeEntity(dc, resume.Id);
                if (entity != null)
                {
                    DeleteJobsSchools(dc, entity);
                    resume.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        Resume IResumesRepository.GetResume(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResume(dc, id);
            }
        }

        IList<Resume> IResumesRepository.GetResumes(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetResumes(dc, ids);
            }
        }

        void IResumesRepository.CreateParsedResume(ParsedResume parsedResume)
        {
            using (var dc = CreateContext())
            {
                dc.ParsedResumeEntities.InsertOnSubmit(parsedResume.Map());
                dc.SubmitChanges();
            }
        }

        void IResumesRepository.DeleteParsedResume(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetParsedResumeEntity(dc, id);
                if (entity != null)
                {
                    if (entity.ResumeEntity != null)
                    {
                        dc.ResumeEntities.DeleteOnSubmit(entity.ResumeEntity);
                        entity.ResumeEntity = null;
                    }

                    dc.ParsedResumeEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        ParsedResume IResumesRepository.GetParsedResume(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetParsedResume(dc, id);
            }
        }

        private static Resume GetResume(ResumesDataContext dc, Guid id)
        {
            var resume = GetResumeQuery(dc, id);
            if (resume != null)
            {
                resume.Jobs = GetList(GetJobsQuery(dc, resume.Id).ToList());
                resume.Schools = GetList(GetSchoolsQuery(dc, resume.Id).ToList());
            }
            return resume;
        }

        private static IList<Resume> GetResumes(ResumesDataContext dc, IEnumerable<Guid> ids)
        {
            var resumeIds = new SplitList<Guid>(ids).ToString();
            var resumes = GetAllResumesQuery(dc, resumeIds).ToList();
            var allJobs = (from e in GetAllJobsQuery(dc, resumeIds)
                           group e by e.Item1).ToDictionary(g => g.Key, g => (from e in g select e.Item2).ToList());
            var allSchools = (from e in GetAllSchoolsQuery(dc, resumeIds)
                              group e by e.Item1).ToDictionary(g => g.Key, g => (from e in g select e.Item2).ToList());

            foreach (var resume in resumes)
            {
                List<Job> jobs;
                if (allJobs.TryGetValue(resume.Id, out jobs))
                    resume.Jobs = jobs;
                List<School> schools;
                if (allSchools.TryGetValue(resume.Id, out schools))
                    resume.Schools = schools;
            }

            return resumes.ToList();
        }

        private static ParsedResume GetParsedResume(ResumesDataContext dc, Guid id)
        {
            var parsedResumeEntity = GetParsedResumeEntity(dc, id);
            if (parsedResumeEntity == null)
                return null;

            var parsedResume = parsedResumeEntity.Map();
            if (parsedResumeEntity.resumeId != null)
            {
                parsedResume.Resume = GetResumeQuery(dc, parsedResumeEntity.resumeId.Value);
                if (parsedResume.Resume != null)
                {
                    parsedResume.Resume.Jobs = GetList(GetJobsQuery(dc, parsedResume.Resume.Id).ToList());
                    parsedResume.Resume.Schools = GetList(GetSchoolsQuery(dc, parsedResume.Resume.Id).ToList());
                }
            }

            return parsedResume;
        }

        private static IList<T> GetList<T>(IList<T> list)
        {
            return list.Count == 0 ? null : list;
        }

        private static void DeleteJobsSchools(ResumesDataContext dc, ResumeEntity entity)
        {
            if (entity.ResumeJobEntities != null && entity.ResumeJobEntities.Count > 0)
            {
                dc.ResumeJobEntities.DeleteAllOnSubmit(entity.ResumeJobEntities);
                entity.ResumeJobEntities = null;
            }

            if (entity.ResumeSchoolEntities != null && entity.ResumeSchoolEntities.Count > 0)
            {
                dc.ResumeSchoolEntities.DeleteAllOnSubmit(entity.ResumeSchoolEntities);
                entity.ResumeSchoolEntities = null;
            }
        }

        private ResumesDataContext CreateContext()
        {
            return CreateContext(c => new ResumesDataContext(c));
        }
    }
}
