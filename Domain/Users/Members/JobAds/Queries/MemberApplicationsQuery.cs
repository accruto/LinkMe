using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Queries;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public class MemberApplicationsQuery
        : IMemberApplicationsQuery
    {
        private readonly IApplicationsQuery _applicationsQuery;

        public MemberApplicationsQuery(IApplicationsQuery applicationsQuery)
        {
            _applicationsQuery = applicationsQuery;
        }

        InternalApplication IMemberApplicationsQuery.GetInternalApplication(Guid id)
        {
            return _applicationsQuery.GetApplication<InternalApplication>(id, true);
        }

        InternalApplication IMemberApplicationsQuery.GetInternalApplication(Guid applicantId, Guid positionId)
        {
            return _applicationsQuery.GetApplication<InternalApplication>(applicantId, positionId, true);
        }

        ExternalApplication IMemberApplicationsQuery.GetExternalApplication(Guid id)
        {
            return _applicationsQuery.GetApplication<ExternalApplication>(id, true);
        }

        ExternalApplication IMemberApplicationsQuery.GetExternalApplication(Guid applicantId, Guid positionId)
        {
            return _applicationsQuery.GetApplication<ExternalApplication>(applicantId, positionId, true);
        }

        IList<Application> IMemberApplicationsQuery.GetApplications(Guid applicantId)
        {
            var internalApplications = _applicationsQuery.GetApplications<InternalApplication>(applicantId, true);
            var externalApplications = _applicationsQuery.GetApplications<ExternalApplication>(applicantId, true);

            // Due to historic reasons it is possible for some older job ads to have both an internal and external application (not quite sure how ...).
            // Choose the internal application in these cases.

            var internalPositionIds = (from a in internalApplications select a.PositionId).ToArray();
            return internalApplications.Cast<Application>().Concat(from a in externalApplications where !internalPositionIds.Contains(a.PositionId) select a).ToList();
        }

        IList<Application> IMemberApplicationsQuery.GetApplications(IEnumerable<Guid> ids)
        {
            var applicationIds = ids.ToList();
            return _applicationsQuery.GetApplications<InternalApplication>(applicationIds, true).Cast<Application>().Concat(_applicationsQuery.GetApplications<ExternalApplication>(applicationIds, true)).ToList();
        }
    }
}
