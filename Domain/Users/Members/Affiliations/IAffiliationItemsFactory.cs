using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Affiliations
{
    public interface IAffiliationItemsFactory
    {
        AffiliationItems ConvertAffiliationItems(Guid affiliateId, IDictionary<string, string> items);
        IDictionary<string, string> ConvertAffiliationItems(Guid affiliateId, AffiliationItems items);
    }
}
