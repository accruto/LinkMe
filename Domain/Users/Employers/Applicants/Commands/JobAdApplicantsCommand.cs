using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.Applicants.Commands
{
    public class JobAdApplicantsCommand
        : JobAdApplicantsComponent, IJobAdApplicantsCommand
    {
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;
        private static readonly int[] JobAdIncompatibleTypes = new[] { (int)BlockListType.Permanent };

        public JobAdApplicantsCommand(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery, IJobAdApplicantsQuery jobApplicantsQuery)
            : base(contenderListsCommand, contenderListsQuery)
        {
            _jobAdApplicantsQuery = jobApplicantsQuery;
        }

        #region ShortlistApplicant

        /// <summary>
        /// Shortlist applicants from a jobAd provided they are new or previously removed or rejected
        /// </summary>
        /// <param name="employer">The current employer (for security)</param>
        /// <param name="jobAd">The jobAd to shortlist the applicant(s) for</param>
        /// <param name="applicantIds">The applicants to shortlist</param>
        /// <returns>Updated counts (i.e. after the shortlisting(s)) of the number of applicants in each status</returns>
        IDictionary<ApplicantStatus, int> IJobAdApplicantsCommand.ShortlistApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds)
        {
            if (!CanAccessList(employer, jobAd))
                throw new AddApplicantPermissionsException(employer, jobAd.Id);

            // Ensure the list exists.

            var list = EnsureList(jobAd);

            // Shortlist those Applicants who are either new, rejected, or removed; these are the only valid transitions

            var applicantsToShortlist = from e in _contenderListsQuery.GetEntries<ApplicantListEntry>(list.Id, null, applicantIds)
                                        where e.ApplicantStatus == ApplicantStatus.New
                                        || e.ApplicantStatus == ApplicantStatus.Rejected
                                        || e.ApplicantStatus == ApplicantStatus.Removed
                                        select e.ApplicantId;
            _contenderListsCommand.ChangeStatus(list.Id, applicantsToShortlist, ApplicantStatus.Shortlisted);

            // Now deal with new additions; CreateContenderListEntries handles already-existing entries.

            _contenderListsCommand.CreateEntries(list.Id, applicantIds, ApplicantStatus.Shortlisted);

            // Now remove from incompatible lists (eg. permanent blocklists).

            _contenderListsCommand.DeleteEntries(employer.Id, JobAdIncompatibleTypes, applicantIds);

            return _jobAdApplicantsQuery.GetApplicantCounts(employer, list);
        }

        #endregion

        #region RejectApplicant

        /// <summary>
        /// Reject applicants from a jobAd provided they are new or shortlisted
        /// </summary>
        /// <param name="employer">The current employer (for security)</param>
        /// <param name="jobAd">The jobAd to reject the applicant(s) from</param>
        /// <param name="applicantIds">The applicants to reject</param>
        /// <returns>Updated counts (i.e. after the rejection(s)) of the number of applicants in each status</returns>
        IDictionary<ApplicantStatus, int> IJobAdApplicantsCommand.RejectApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds)
        {
            if (!CanAccessList(employer, jobAd))
                throw new AddApplicantPermissionsException(employer, jobAd.Id);

            // Ensure the list exists.

            var list = EnsureList(jobAd);

            // Reject those Applicants who are either new or shortlisted; these are the only valid transitions

            var applicantsToReject = from e in _contenderListsQuery.GetEntries<ApplicantListEntry>(list.Id, null, applicantIds)
                                     where e.ApplicantStatus == ApplicantStatus.New
                                     || e.ApplicantStatus == ApplicantStatus.Shortlisted
                                     select e.ApplicantId;
            _contenderListsCommand.ChangeStatus(list.Id, applicantsToReject, ApplicantStatus.Rejected);

            return _jobAdApplicantsQuery.GetApplicantCounts(employer, list);
        }

        #endregion

        #region RemoveApplicant

        /// <summary>
        /// Change applicants status to Removed
        /// </summary>
        /// <param name="employer">The current employer (for security)</param>
        /// <param name="jobAd">The jobAd to delete the applicant(s) from</param>
        /// <param name="applicantIds">The applicants to remove</param>
        /// <returns>Updated counts (i.e. after the removal(s)) of the number of applicants in each status</returns>
        IDictionary<ApplicantStatus, int> IJobAdApplicantsCommand.RemoveApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds)
        {
            if (!CanAccessList(employer, jobAd))
                throw new AddApplicantPermissionsException(employer, jobAd.Id);

            // Ensure the list exists.

            var list = EnsureList(jobAd);

            // Remove those Applicants who are rejected; this are the only valid transitions

            var applicantsToRemove = from e in _contenderListsQuery.GetEntries<ApplicantListEntry>(list.Id, null, applicantIds)
                                     where e.ApplicantStatus == ApplicantStatus.Rejected
                                     select e.ApplicantId;
            _contenderListsCommand.ChangeStatus(list.Id, applicantsToRemove, ApplicantStatus.Removed);

            return _jobAdApplicantsQuery.GetApplicantCounts(employer, list);
        }

        #endregion

        #region UndoShortlistApplicant

        IDictionary<ApplicantStatus, int> IJobAdApplicantsCommand.UndoShortlistApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds, ApplicantStatus? previousStatus)
        {
            // Ensure the list exists.

            var list = EnsureList(jobAd);

            if (!CanAccessList(employer, list))
                throw new AddApplicantPermissionsException(employer, list.Id);

            var entries = _contenderListsQuery.GetEntries<ApplicantListEntry>(list.Id, null, applicantIds);

            if (previousStatus.HasValue)
            {
                //The status to be returned to is explicit; change to that.

                var applicantsToUpdate = from e in entries
                                         where e.ApplicantStatus == ApplicantStatus.Shortlisted
                                         select e.ApplicantId;
                _contenderListsCommand.ChangeStatus(list.Id, applicantsToUpdate, previousStatus.Value);
            }
            else
            {
                var applicantsToDelete = from e in entries
                                         where e.ApplicantStatus == ApplicantStatus.Shortlisted
                                         && e.ApplicationId == null
                                         select e.ApplicantId;
                _contenderListsCommand.DeleteEntries(list.Id, applicantsToDelete);

                // Move the list entries associated with applications back to new.

                var applicantsToUpdate = from e in entries
                                         where e.ApplicantStatus == ApplicantStatus.Shortlisted
                                         && e.ApplicationId != null
                                         select e.ApplicantId;
                _contenderListsCommand.ChangeStatus(list.Id, applicantsToUpdate, ApplicantStatus.New);
            }

            return _jobAdApplicantsQuery.GetApplicantCounts(employer, list);
        }

        #endregion

        #region UndoRejectApplicant

        IDictionary<ApplicantStatus, int> IJobAdApplicantsCommand.UndoRejectApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds, ApplicantStatus statusToReturnTo)
        {
            // Ensure the list exists.

            var list = EnsureList(jobAd);

            if (!CanAccessList(employer, list))
                throw new AddApplicantPermissionsException(employer, list.Id);

            var entries = _contenderListsQuery.GetEntries<ApplicantListEntry>(list.Id, null, applicantIds);

            // Move the list entries back to the specified status (s/be new or shortlisted).

            var applicantsToUpdate = from e in entries
                                     where e.ApplicantStatus == ApplicantStatus.Rejected
                                     select e.ApplicantId;
            _contenderListsCommand.ChangeStatus(list.Id, applicantsToUpdate, statusToReturnTo);

            return _jobAdApplicantsQuery.GetApplicantCounts(employer, list);
        }

        #endregion

        #region UndoRemoveApplicant

        IDictionary<ApplicantStatus, int> IJobAdApplicantsCommand.UndoRemoveApplicants(IEmployer employer, JobAdEntry jobAd, IEnumerable<Guid> applicantIds)
        {
            // Ensure the list exists.

            var list = EnsureList(jobAd);

            if (!CanAccessList(employer, list))
                throw new AddApplicantPermissionsException(employer, list.Id);

            var entries = _contenderListsQuery.GetEntries<ApplicantListEntry>(list.Id, null, applicantIds);

            // Move the list entries back to rejected.

            var applicantsToUpdate = from e in entries
                                     where e.ApplicantStatus == ApplicantStatus.Removed
                                     select e.ApplicantId;
            _contenderListsCommand.ChangeStatus(list.Id, applicantsToUpdate, ApplicantStatus.Rejected);

            return _jobAdApplicantsQuery.GetApplicantCounts(employer, list);
        }

        #endregion

        private static bool CanAccessList(IHasId<Guid> employer, ApplicantList applicantList)
        {
            if (employer == null || applicantList == null)
                return false;
            return employer.Id == applicantList.PosterId;
        }

        private static bool CanAccessList(IHasId<Guid> employer, IJobAd jobAd)
        {
            if (employer == null || jobAd == null)
                return false;
            return employer.Id == jobAd.PosterId;
        }
    }
}