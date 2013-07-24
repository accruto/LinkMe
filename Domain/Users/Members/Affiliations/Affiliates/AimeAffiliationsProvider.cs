using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Affiliations.Affiliates
{
    public class AimeAffiliationsProvider
        : IAffiliationsProvider
    {
        private const string StatusName = "AimeMemberStatus";

        AffiliationItems IAffiliationsProvider.ConvertAffiliationItems(IDictionary<string, string> items)
        {
            if (items == null)
                return null;

            string value;
            return items.TryGetValue(StatusName, out value)
                ? new AimeAffiliationItems { Status = (AimeMemberStatus)Enum.Parse(typeof(AimeMemberStatus), value) }
                : null;
        }

        IDictionary<string, string> IAffiliationsProvider.ConvertAffiliationItems(AffiliationItems items)
        {
            if (items == null || !(items is AimeAffiliationItems))
                return null;

            var status = ((AimeAffiliationItems)items).Status;
            return status == null
                ? null
                : new Dictionary<string, string> { { StatusName, status.ToString() } };
        }
    }
}
