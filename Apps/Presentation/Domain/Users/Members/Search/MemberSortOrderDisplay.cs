using System.Collections.Generic;
using System.Linq;
using LinkMe.Query.Members;

namespace LinkMe.Apps.Presentation.Domain.Users.Members.Search
{
    public static class MemberSortOrderDisplay
    {
        private static readonly IDictionary<MemberSortOrder, string> Texts = new Dictionary<MemberSortOrder, string>
        {
            {MemberSortOrder.Relevance, "Relevance"},
            {MemberSortOrder.DateUpdated, "Freshness"},
        };

        public static MemberSortOrder[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this MemberSortOrder sortOrder)
        {
            string text;
            return Texts.TryGetValue(sortOrder, out text) ? text : null;
        }
    }
}
