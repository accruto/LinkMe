using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Affiliations.Affiliates
{
    public class AffiliationItemsFactory
        : IAffiliationItemsFactory
    {
        private readonly IDictionary<Guid, IAffiliationsProvider> _providers = new Dictionary<Guid, IAffiliationsProvider>();

        public AffiliationItemsFactory(Guid[] ids, IAffiliationsProvider[] providers)
        {
            for (var index = 0; index < ids.Length; ++index)
                _providers[ids[index]] = providers[index];
        }

        AffiliationItems IAffiliationItemsFactory.ConvertAffiliationItems(Guid affiliateId, IDictionary<string, string> items)
        {
            if (items == null)
                return null;

            IAffiliationsProvider provider;
            return !_providers.TryGetValue(affiliateId, out provider)
                ? null
                : provider.ConvertAffiliationItems(items);
        }

        IDictionary<string, string> IAffiliationItemsFactory.ConvertAffiliationItems(Guid affiliateId, AffiliationItems items)
        {
            if (items == null)
                return null;

            IAffiliationsProvider provider;
            return !_providers.TryGetValue(affiliateId, out provider)
                ? null
                : provider.ConvertAffiliationItems(items);
        }
    }
}
