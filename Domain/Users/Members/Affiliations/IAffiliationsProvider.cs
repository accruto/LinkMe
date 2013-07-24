using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Affiliations
{
    public interface IAffiliationsProvider
    {
        AffiliationItems ConvertAffiliationItems(IDictionary<string, string> items);
        IDictionary<string, string> ConvertAffiliationItems(AffiliationItems items);
    }
}
