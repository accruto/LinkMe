using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public class JobAdIntegrationQuery
        : IJobAdIntegrationQuery
    {
        private readonly IJobAdsRepository _repository;

        public JobAdIntegrationQuery(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> IJobAdIntegrationQuery.GetJobAdIds(Guid integratorUserId, Guid posterId, string externalReferenceId)
        {
            return _repository.GetJobAdIds(integratorUserId, posterId, externalReferenceId);
        }

        IList<Guid> IJobAdIntegrationQuery.GetJobAdIds(string externalReferenceId)
        {
            return _repository.GetJobAdIds(externalReferenceId);
        }

        IList<Guid> IJobAdIntegrationQuery.GetOpenJobAdIds(string referenceId, string title)
        {
            return _repository.GetOpenJobAdIds(referenceId, title);
        }

        IList<Guid> IJobAdIntegrationQuery.GetJobAdIds(Guid integratorUserId, Guid posterId)
        {
            return _repository.GetJobAdIds(integratorUserId, posterId);
        }

        IList<Guid> IJobAdIntegrationQuery.GetJobAdIds(Guid integratorUserId, string integratorReferenceId)
        {
            return _repository.GetJobAdIds(integratorUserId, integratorReferenceId);
        }

        IList<Guid> IJobAdIntegrationQuery.GetOpenJobAdIds(Guid integratorUserId, Guid posterId)
        {
            return _repository.GetOpenJobAdIds(integratorUserId, posterId);
        }

        IList<Guid> IJobAdIntegrationQuery.GetOpenJobAdIds(Guid integratorUserId, string integratorReferenceId)
        {
            return _repository.GetOpenJobAdIds(integratorUserId, integratorReferenceId);
        }

        IList<Guid> IJobAdIntegrationQuery.GetOpenJobAdIds(IEnumerable<Guid> excludedIntegratorUserIds)
        {
            return _repository.GetOpenJobAdIds(excludedIntegratorUserIds);
        }

        IList<Guid> IJobAdIntegrationQuery.GetOpenJobAdIds(IEnumerable<Guid> industryIds, DateTime? modifiedSince)
        {
            return _repository.GetOpenJobAdIds(industryIds, modifiedSince);
        }

        JobAdFeatures IJobAdIntegrationQuery.GetDefaultFeatures()
        {
            return JobAdFeatures.ExtendedExpiry;
        }

        JobAdFeatureBoost IJobAdIntegrationQuery.GetDefaultFeatureBoost()
        {
            return JobAdFeatureBoost.None;
        }
    }
}
