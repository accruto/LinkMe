using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Registered.Employers
{
    public partial class EmployerSubHeader
        : LinkMeUserControl
    {
        private static readonly ICreditsQuery _creditsQuery = Container.Current.Resolve<ICreditsQuery>();
        private static readonly IEmployerCreditsQuery _employerCreditsQuery = Container.Current.Resolve<IEmployerCreditsQuery>();

        protected IList<Tuple<Credit, Allocation>> Allocations { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Get credit allocations.

            var allocations = GetCreditAllocations();
            Allocations = allocations == null
                ? new List<Tuple<Credit, Allocation>>()
                : (from p in allocations
                   where (p.Key is ContactCredit && p.Value.RemainingQuantity != null)
                   || (p.Key is ApplicantCredit && p.Value.RemainingQuantity != null && p.Value.RemainingQuantity != 0)
                   || (p.Key is JobAdCredit && p.Value.RemainingQuantity != null && p.Value.RemainingQuantity != 0)
                   select Tuple.Create(p.Key, p.Value))
                   .OrderBy(p => p.Item1 is ContactCredit ? 0 : p.Item1 is ApplicantCredit ? 1 : 2)
                   .ToList();
        }

        private IEnumerable<KeyValuePair<Credit, Allocation>> GetCreditAllocations()
        {
            if (LoggedInEmployer != null)
            {
                var contactCredit = _creditsQuery.GetCredit<ContactCredit>();
                var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
                var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();

                var allocations = _employerCreditsQuery.GetEffectiveActiveAllocations(LoggedInEmployer, new[] {contactCredit.Id, jobAdCredit.Id, applicantCredit.Id});
                return new Dictionary<Credit, Allocation>
                {
                    {contactCredit, allocations[contactCredit.Id]},
                    {jobAdCredit, allocations[jobAdCredit.Id]},
                    {applicantCredit, allocations[applicantCredit.Id]},
                };
            }

            return new Dictionary<Credit, Allocation>();
        }
    }
}