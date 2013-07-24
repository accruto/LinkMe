using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public interface IJobAdIntegrationQuery
    {
        IList<Guid> GetJobAdIds(Guid integratorUserId, Guid posterId);
        IList<Guid> GetJobAdIds(Guid integratorUserId, Guid posterId, string externalReferenceId);
        IList<Guid> GetJobAdIds(string externalReferenceId);
        IList<Guid> GetJobAdIds(Guid integratorUserId, string integratorReferenceId);

        IList<Guid> GetOpenJobAdIds(Guid integratorUserId, Guid posterId);
        IList<Guid> GetOpenJobAdIds(string referenceId, string title);
        IList<Guid> GetOpenJobAdIds(Guid integratorUserId, string integratorReferenceId);

        IList<Guid> GetOpenJobAdIds(IEnumerable<Guid> excludedIntegratorUserIds);
        IList<Guid> GetOpenJobAdIds(IEnumerable<Guid> industryIds, DateTime? modifiedSince);

        JobAdFeatures GetDefaultFeatures();
        JobAdFeatureBoost GetDefaultFeatureBoost();
    }
}
