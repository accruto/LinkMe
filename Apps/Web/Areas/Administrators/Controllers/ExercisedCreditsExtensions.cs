using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;

namespace LinkMe.Web.Areas.Administrators.Controllers
{
    public static class ExercisedCreditsExtensions
    {
        public static IList<Allocation> GetAllocations(this IAllocationsQuery allocationsQuery, IEnumerable<ExercisedCredit> exercisedCredits)
        {
            return allocationsQuery.GetAllocations((from c in exercisedCredits where c.AllocationId != null select c.AllocationId.Value).Distinct());
        }

        public static IDictionary<Guid, Employer> GetEmployers(this IEmployersQuery employersQuery, IEnumerable<ExercisedCredit> exercisedCredits)
        {
            return employersQuery.GetEmployers((from c in exercisedCredits select c.ExercisedById).Distinct()).ToDictionary(e => e.Id, e => e);
        }

        public static IDictionary<Guid, JobAd> GetJobAds(this IJobAdsQuery jobAdsQuery, IEnumerable<ExercisedCredit> exercisedCredits)
        {
            // Assumption: the job ads come from those credits where no-one has been exercised on.

            return jobAdsQuery.GetJobAds<JobAd>((from c in exercisedCredits where c.ExercisedOnId == null && c.ReferenceId != null select c.ReferenceId.Value).Distinct()).ToDictionary(j => j.Id, j => j);
        }

        public static IDictionary<Guid, Member> GetMembers(this IMembersQuery membersQuery, IEnumerable<ExercisedCredit> exercisedCredits)
        {
            // Assumption: the members come from those credits where someone has been exercised on.

            return membersQuery.GetMembers((from c in exercisedCredits where c.ExercisedOnId != null select c.ExercisedOnId.Value).Distinct()).ToDictionary(m => m.Id, m => m);
        }
    }
}
