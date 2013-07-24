using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Affiliations.Affiliates
{
    public class FinsiaAffiliationsProvider
        : IAffiliationsProvider
    {
        private const string MemberIdName = "FinsiaMemberId";

        AffiliationItems IAffiliationsProvider.ConvertAffiliationItems(IDictionary<string, string> items)
        {
            if (items == null)
                return null;

            string value;
            return items.TryGetValue(MemberIdName, out value)
                ? new FinsiaAffiliationItems { MemberId = value }
                : null;
        }

        IDictionary<string, string> IAffiliationsProvider.ConvertAffiliationItems(AffiliationItems items)
        {
            if (items == null || !(items is FinsiaAffiliationItems))
                return null;

            var memberId = ((FinsiaAffiliationItems) items).MemberId;
            return string.IsNullOrEmpty(memberId)
                ? null
                : new Dictionary<string, string> { { MemberIdName, memberId } };
        }
    }
}
