using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Text.Synonyms.Data
{
    public static class Mappings
    {
        public static IEnumerable<EquivalentTermEntity> Map(this SynonymGroup synonymGroup)
        {
            return from s in synonymGroup.Terms
                   select new EquivalentTermEntity { equivalentGroupId = synonymGroup.Id, searchTerm = s };
        }
    }
}