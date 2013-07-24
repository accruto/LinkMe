using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Users.Employers.Applicants;

namespace LinkMe.Domain.Users.Employers.JobAds.Queries
{
    public class JobAdApplicationSubmissionsQuery
        : JobAdApplicantsComponent, IJobAdApplicationSubmissionsQuery
    {
        public JobAdApplicationSubmissionsQuery(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
            : base(contenderListsCommand, contenderListsQuery)
        {
        }

        bool IJobAdApplicationSubmissionsQuery.HasSubmittedApplication(Guid applicantId, Guid jobAdId)
        {
            return _contenderListsQuery.IsApplicant(jobAdId, applicantId);
        }

        IList<Guid> IJobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(Guid applicantId)
        {
            return _contenderListsQuery.GetApplicantListIds(ListTypes, applicantId);
        }

        IList<Guid> IJobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(Guid applicantId, IEnumerable<Guid> jobAdIds)
        {
            return _contenderListsQuery.GetApplicantListIds(jobAdIds, ListTypes, applicantId);
        }
    }
}