using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Contenders.Data
{
    public class ApplicationsRepository
        : Repository, IApplicationsRepository
    {
        private static readonly Func<ContendersDataContext, Guid, JobApplicationEntity> GetJobApplicationEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid id)
                => (from a in dc.JobApplicationEntities
                    where a.id == id
                    select a).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, ExternalApplicationEntity> GetExternalApplicationEntity
            = CompiledQuery.Compile((ContendersDataContext dc, Guid id)
                => (from a in dc.ExternalApplicationEntities
                    where a.id == id
                    select a).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, bool, InternalApplication> GetInternalApplication
            = CompiledQuery.Compile((ContendersDataContext dc, Guid id, bool includePending)
                => (from a in dc.JobApplicationEntities
                    where a.id == id
                    && !a.isDeleted
                    && (!a.isPending || includePending)
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, ExternalApplication> GetExternalApplication
            = CompiledQuery.Compile((ContendersDataContext dc, Guid id)
                => (from a in dc.ExternalApplicationEntities
                    where a.id == id
                    && !a.isDeleted
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, Guid, bool, InternalApplication> GetInternalApplicationByPositionAndApplicant
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicantId, Guid positionId, bool includePending)
                => (from a in dc.JobApplicationEntities
                    where a.jobAdId == positionId
                    && a.applicantId == applicantId
                    && !a.isDeleted
                    && (!a.isPending || includePending)
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ContendersDataContext, Guid, Guid, ExternalApplication> GetExternalApplicationByPositionAndApplicant
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicantId, Guid positionId)
                => (from a in dc.ExternalApplicationEntities
                    where a.positionId == positionId
                    && a.applicantId == applicantId
                    && !a.isDeleted
                    select a.Map()).SingleOrDefault());

        private static readonly Func<ContendersDataContext, string, bool, IQueryable<InternalApplication>> GetInternalApplications
            = CompiledQuery.Compile((ContendersDataContext dc, string ids, bool includePending)
                => from a in dc.JobApplicationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on a.id equals i.value
                   where !a.isDeleted
                    && (!a.isPending || includePending)
                   orderby a.createdTime descending
                   select a.Map());

        private static readonly Func<ContendersDataContext, string, IQueryable<ExternalApplication>> GetExternalApplications
            = CompiledQuery.Compile((ContendersDataContext dc, string ids)
                => from a in dc.ExternalApplicationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on a.id equals i.value
                   where !a.isDeleted
                   orderby a.createdTime descending
                   select a.Map());

        private static readonly Func<ContendersDataContext, Guid, bool, IQueryable<InternalApplication>> GetInternalApplicationsByPositionId
            = CompiledQuery.Compile((ContendersDataContext dc, Guid positionId, bool includePending)
                => from a in dc.JobApplicationEntities
                   where a.jobAdId == positionId
                   && !a.isDeleted
                    && (!a.isPending || includePending)
                   orderby a.createdTime descending
                   select a.Map());

        private static readonly Func<ContendersDataContext, Guid, IQueryable<ExternalApplication>> GetExternalApplicationsByPositionId
            = CompiledQuery.Compile((ContendersDataContext dc, Guid positionId)
                => from a in dc.ExternalApplicationEntities
                   where a.positionId == positionId
                   && !a.isDeleted
                   orderby a.createdTime descending
                   select a.Map());

        private static readonly Func<ContendersDataContext, Guid, bool, IQueryable<InternalApplication>> GetInternalApplicationsForApplicant
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicantId, bool includePending)
                => from a in dc.JobApplicationEntities
                   where a.applicantId == applicantId
                   && !a.isDeleted
                    && (!a.isPending || includePending)
                   orderby a.createdTime descending
                   select a.Map());

        private static readonly Func<ContendersDataContext, Guid, IQueryable<ExternalApplication>> GetExternalApplicationsForApplicant
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicantId)
                => (from g in
                        (from a in dc.ExternalApplicationEntities
                         where a.applicantId == applicantId
                         group a by new {a.applicantId, a.positionId})
                    select (from v in g orderby v.createdTime select v.Map()).Take(1).Single()));

        private static readonly Func<ContendersDataContext, Guid, string, bool, IQueryable<InternalApplication>> GetInternalApplicationsForPositions
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicantId, string positionIds, bool includePending)
                => from a in dc.JobApplicationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, positionIds) on a.jobAdId equals i.value
                   where a.applicantId == applicantId
                   && !a.isDeleted
                   && (!a.isPending || includePending)
                   orderby a.createdTime descending
                   select a.Map());

        private static readonly Func<ContendersDataContext, Guid, string, IQueryable<ExternalApplication>> GetExternalApplicationsForPositions
            = CompiledQuery.Compile((ContendersDataContext dc, Guid applicantId, string positionIds)
                => from a in dc.ExternalApplicationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, positionIds) on a.positionId equals i.value
                   where a.applicantId == applicantId
                   && !a.isDeleted
                   orderby a.createdTime descending
                   select a.Map());

        public ApplicationsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IApplicationsRepository.CreateApplication<TApplication>(TApplication application)
        {
            using (var dc = CreateContext())
            {
                if (application is InternalApplication)
                    dc.JobApplicationEntities.InsertOnSubmit((application as InternalApplication).Map());
                else if (application is ExternalApplication)
                    dc.ExternalApplicationEntities.InsertOnSubmit((application as ExternalApplication).Map());
                dc.SubmitChanges();
            }
        }

        void IApplicationsRepository.UpdateApplication<TApplication>(TApplication application)
        {
            using (var dc = CreateContext())
            {
                if (application is InternalApplication)
                {
                    var entity = GetJobApplicationEntity(dc, application.Id);
                    if (entity != null)
                        (application as InternalApplication).MapTo(entity);
                }
                else if (application is ExternalApplication)
                {
                    var entity = GetExternalApplicationEntity(dc, application.Id);
                    if (entity != null)
                        (application as ExternalApplication).MapTo(entity);
                }

                dc.SubmitChanges();

            }
        }

        void IApplicationsRepository.DeleteApplication<TApplication>(Guid id)
        {
            using (var dc = CreateContext())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                {
                    var entity = GetJobApplicationEntity(dc, id);
                    if (entity != null)
                        entity.isDeleted = true;
                }
                else if (typeof(TApplication) == typeof(ExternalApplication))
                {
                    var entity = GetExternalApplicationEntity(dc, id);
                    if (entity != null)
                        entity.isDeleted = true;
                }

                dc.SubmitChanges();
            }
        }

        TApplication IApplicationsRepository.GetApplication<TApplication>(Guid id, bool includePending)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                    return GetInternalApplication(dc, id, includePending) as TApplication;
                if (typeof(TApplication) == typeof(ExternalApplication))
                    return GetExternalApplication(dc, id) as TApplication;
                return null;
            }
        }

        TApplication IApplicationsRepository.GetApplication<TApplication>(Guid applicantId, Guid positionId, bool includePending)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                    return GetInternalApplicationByPositionAndApplicant(dc, applicantId, positionId, includePending) as TApplication;
                if (typeof(TApplication) == typeof(ExternalApplication))
                    return GetExternalApplicationByPositionAndApplicant(dc, applicantId, positionId) as TApplication;
                return null;
            }
        }

        IList<TApplication> IApplicationsRepository.GetApplications<TApplication>(Guid applicantId, bool includePending)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                    return GetInternalApplicationsForApplicant(dc, applicantId, includePending).Cast<TApplication>().ToList();
                if (typeof(TApplication) == typeof(ExternalApplication))
                    return GetExternalApplicationsForApplicant(dc, applicantId).Cast<TApplication>().ToList();
                return new List<TApplication>();
            }
        }

        IList<TApplication> IApplicationsRepository.GetApplications<TApplication>(IEnumerable<Guid> ids, bool includePending)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                    return GetInternalApplications(dc, new SplitList<Guid>(ids).ToString(), includePending).Cast<TApplication>().ToList();
                if (typeof(TApplication) == typeof(ExternalApplication))
                    return GetExternalApplications(dc, new SplitList<Guid>(ids).ToString()).Cast<TApplication>().ToList();
                return new List<TApplication>();
            }
        }

        IList<TApplication> IApplicationsRepository.GetApplications<TApplication>(Guid applicantId, IEnumerable<Guid> positionIds, bool includePending)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                    return GetInternalApplicationsForPositions(dc, applicantId, new SplitList<Guid>(positionIds).ToString(), includePending).Cast<TApplication>().ToList();
                if (typeof(TApplication) == typeof(ExternalApplication))
                    return GetExternalApplicationsForPositions(dc, applicantId, new SplitList<Guid>(positionIds).ToString()).Cast<TApplication>().ToList();
                return new List<TApplication>();
            }
        }

        IList<TApplication> IApplicationsRepository.GetApplicationsByPositionId<TApplication>(Guid positionId, bool includePending)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (typeof(TApplication) == typeof(InternalApplication))
                    return GetInternalApplicationsByPositionId(dc, positionId, includePending).Cast<TApplication>().ToList();
                if (typeof(TApplication) == typeof(ExternalApplication))
                    return GetExternalApplicationsByPositionId(dc, positionId).Cast<TApplication>().ToList();
                return new List<TApplication>();
            }
        }

        private ContendersDataContext CreateContext()
        {
            return CreateContext(c => new ContendersDataContext(c));
        }
    }
}