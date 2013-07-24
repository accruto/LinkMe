using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Applicants;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Users.Employers.JobAds
{
    public static class ApplicantListsExtensions
    {
        public static int GetShortlistedCount(this JobAdEntry jobAd, IEnumerable<ApplicantList> applicantLists, IDictionary<Guid, IDictionary<ApplicantStatus, int>> counts)
        {
            return GetApplicationStatusCount(jobAd, applicantLists, counts, ApplicantStatus.Shortlisted);
        }

        public static int GetNewCount(this JobAdEntry jobAd, IEnumerable<ApplicantList> applicantLists, IDictionary<Guid, IDictionary<ApplicantStatus, int>> counts)
        {
            return GetApplicationStatusCount(jobAd, applicantLists, counts, ApplicantStatus.New);
        }

        public static int GetRejectedCount(this JobAdEntry jobAd, IEnumerable<ApplicantList> applicantLists, IDictionary<Guid, IDictionary<ApplicantStatus, int>> counts)
        {
            return GetApplicationStatusCount(jobAd, applicantLists, counts, ApplicantStatus.Rejected);
        }

        private static int GetApplicationStatusCount(this IJobAd jobAd, IEnumerable<ApplicantList> applicantLists, IDictionary<Guid, IDictionary<ApplicantStatus, int>> counts, ApplicantStatus status)
        {
            var applicantList = (from a in applicantLists
                                 where a.Id == jobAd.Id
                                 select a).SingleOrDefault();
            return applicantList.GetApplicationStatusCount(counts, status);
        }

        private static int GetApplicationStatusCount(this IHasId<Guid> applicantList, IDictionary<Guid, IDictionary<ApplicantStatus, int>> counts, ApplicantStatus status)
        {
            if (applicantList == null || !counts.ContainsKey(applicantList.Id))
                return 0;

            return counts[applicantList.Id][status];
        }
    }
}
