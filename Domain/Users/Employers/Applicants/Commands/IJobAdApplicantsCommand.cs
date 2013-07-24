using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.Applicants.Commands
{
    public interface IJobAdApplicantsCommand
    {
        /// <summary>
        /// Shortlist applicants from a jobAd provided they are new
        /// </summary>
        /// <param name="employer">The current employer (for security)</param>
        /// <param name="jobAd">The jobAd to shortlist the applicant(s) for</param>
        /// <param name="applicantIds">The applicants to shortlist</param>
        /// <returns>Updated counts (i.e. after the shortlisting(s)) of the number of applicants in each status</returns>
        IDictionary<ApplicantStatus, int> ShortlistApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds);
        /// <summary>
        /// Reject applicants from a jobAd provided they are new or shortlisted
        /// </summary>
        /// <param name="employer">The current employer (for security)</param>
        /// <param name="jobAd">The jobAd to reject the applicant(s) from</param>
        /// <param name="applicantIds">The applicants to reject</param>
        /// <returns>Updated counts (i.e. after the rejection(s)) of the number of applicants in each status</returns>
        IDictionary<ApplicantStatus, int> RejectApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds);
        /// <summary>
        /// Remove applicants from a jobAd provided they were manually added (i.e. have a null applicationId)
        /// To remove an applicant that applied (i.e. a non-null applicationId) change the status to 'removed' using RemoveApplicant
        /// </summary>
        /// <param name="employer">The current employer (for security)</param>
        /// <param name="jobAd">The jobAd to delete the applicant(s) from</param>
        /// <param name="applicantIds">The applicants to remove</param>
        /// <returns>Updated counts (i.e. after the removal(s)) of the number of applicants in each status</returns>
        IDictionary<ApplicantStatus, int> RemoveApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds);

        IDictionary<ApplicantStatus, int> UndoShortlistApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds, ApplicantStatus? previousStatus);
        IDictionary<ApplicantStatus, int> UndoRejectApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds, ApplicantStatus previousStatus);
        IDictionary<ApplicantStatus, int> UndoRemoveApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds);
    }
}