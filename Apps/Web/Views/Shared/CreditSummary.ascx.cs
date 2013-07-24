using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Credits;
using LinkMe.Web.Mvc;

namespace LinkMe.Web.Views.Shared
{
    public class CreditSummary
        : ViewUserControl
    {
        protected IList<Tuple<Credit, Allocation>> Allocations { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Get credit allocations.

            var allocations = ViewData.GetCreditAllocations();
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
    }
}