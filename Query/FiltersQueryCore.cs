using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Query
{
    public class FiltersQueryCore
    {
        internal static IEnumerable<Guid> GetIncludeList(IEnumerable<Guid> currentIds, IEnumerable<Guid> newIds)
        {
            // Return the intersection.

            return currentIds == null ? newIds : currentIds.Intersect(newIds);
        }

        internal static IEnumerable<Guid> GetExcludeList(IEnumerable<Guid> currentIds, IEnumerable<Guid> newIds)
        {
            // Return the union.

            return currentIds == null ? newIds : currentIds.Union(newIds);
        }
    }
}
