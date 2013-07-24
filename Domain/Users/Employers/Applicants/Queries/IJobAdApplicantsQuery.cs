using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.Applicants.Queries
{
    public interface IJobAdApplicantsQuery
    {
        InternalApplication GetApplication(Guid applicationId);
        InternalApplication GetApplication(Guid applicantId, Guid positionId);
        IList<InternalApplication> GetApplications(IEmployer employer, Guid applicantId);
        IList<InternalApplication> GetApplicationsByPositionId(Guid positionId);

        ApplicantList GetApplicantList(IEmployer employer, IJobAd jobAd);

        int GetApplicantCount(IEmployer employer, ApplicantList applicantList);
        IDictionary<ApplicantStatus, int> GetApplicantCounts(IEmployer employer, ApplicantList applicantList);
        IDictionary<Guid, IDictionary<ApplicantStatus, int>> GetApplicantCounts(IEmployer employer, IEnumerable<ApplicantList> applicantLists);

        ApplicantStatus GetApplicantStatus(Guid applicationId);
        IList<Guid> GetApplicantIds(Guid jobAdId);
        IList<Guid> GetApplicantIds(Guid jobAdId, ApplicantStatus status);
    }
}