using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Affiliations.Affiliates
{
    public class ItcraLinkAffiliationsProvider
        : IAffiliationsProvider
    {
        private const string StatusName = "ItcraLinkMemberStatus";
        private const string MemberIdName = "ItcraLinkMemberId";

        AffiliationItems IAffiliationsProvider.ConvertAffiliationItems(IDictionary<string, string> items)
        {
            if (items == null)
                return null;

            ItcraLinkMemberStatus? status = null;
            string memberId = null;

            string value;
            if (items.TryGetValue(StatusName, out value))
                status = (ItcraLinkMemberStatus) Enum.Parse(typeof (ItcraLinkMemberStatus), value);

            if (items.TryGetValue(MemberIdName, out value))
                memberId = value;

            if (status != null || memberId != null)
                return new ItcraLinkAffiliationItems { Status = status, MemberId = memberId };

            return null;
        }

        IDictionary<string, string> IAffiliationsProvider.ConvertAffiliationItems(AffiliationItems items)
        {
            if (items == null || !(items is ItcraLinkAffiliationItems))
                return null;

            var memberId = ((ItcraLinkAffiliationItems)items).MemberId;
            var status = ((ItcraLinkAffiliationItems)items).Status;

            if (string.IsNullOrEmpty(memberId) && status == null)
                return null;

            var dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(memberId))
                dictionary[MemberIdName] = memberId;
            if (status != null)
                dictionary[StatusName] = status.Value.ToString();
            return dictionary;
        }
    }
}
