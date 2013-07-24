using System;

namespace LinkMe.Query.Search.JobAds
{
    public static class JobAdSearchCriteriaExtensions
    {
        public static JobAdSearchCriteria Clone(this JobAdSearchCriteria criteria)
        {
            return (JobAdSearchCriteria)((ICloneable)criteria).Clone();
        }

        public static T Clone<T>(this T criteria)
            where T : JobAdSearchCriteria
        {
            return (T) ((ICloneable) criteria).Clone();
        }

        public static void PrepareCriteria(this JobAdSearchCriteria criteria)
        {
            // MaxRecency means infinity.

            if (criteria.Recency != null && criteria.Recency.Value.Days == JobAdSearchCriteria.MaxRecency)
                criteria.Recency = null;

            // Zero to MaxSalary means no restriction.

            if (criteria.Salary != null && criteria.Salary.UpperBound == JobAdSearchCriteria.MaxSalary && criteria.Salary.LowerBound == JobAdSearchCriteria.MinSalary)
                criteria.Salary = null;

            // Keep distance with location.

            if (criteria.Location == null)
            {
                criteria.Distance = null;
            }
            else
            {
                if (criteria.Distance == null)
                    criteria.Distance = criteria.EffectiveDistance;
            }
        }
    }
}