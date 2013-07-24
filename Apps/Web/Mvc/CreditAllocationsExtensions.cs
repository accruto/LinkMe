using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Mvc
{
    public static class CreditAllocationsExtensions
    {
        private static readonly ICreditsQuery _creditsQuery = Container.Current.Resolve<ICreditsQuery>();
        private static readonly IEmployerCreditsQuery _employerCreditsQuery = Container.Current.Resolve<IEmployerCreditsQuery>();

        public static void SetCreditAllocations(this ViewDataDictionary viewData, Employer employer)
        {
            if (employer != null)
            {
                var contactCredit = _creditsQuery.GetCredit<ContactCredit>();
                var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
                var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();

                var allocations = _employerCreditsQuery.GetEffectiveActiveAllocations(employer, new[] {contactCredit.Id, applicantCredit.Id, jobAdCredit.Id});
                viewData[ViewDataKeys.CreditAllocationsKey] = new Dictionary<Credit, Allocation>
                {
                    {contactCredit, allocations[contactCredit.Id]},
                    {applicantCredit, allocations[applicantCredit.Id]},
                    {jobAdCredit, allocations[jobAdCredit.Id]},
                };
            }
        }

        public static IDictionary<Credit, Allocation> GetCreditAllocations(this ViewDataDictionary viewData)
        {
            return viewData[ViewDataKeys.CreditAllocationsKey] as IDictionary<Credit, Allocation>;
        }

        public static int? GetCreditAllocationQuantity<TCredit>(this ViewDataDictionary viewData)
        {
            var allocations = viewData.GetCreditAllocations();
            if (allocations == null)
                return 0;

            foreach (var pair in allocations)
            {
                if (pair.Key.GetType() == typeof(TCredit))
                    return pair.Value.RemainingQuantity;
            }

            return 0;
        }
    }
}
