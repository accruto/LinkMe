using com.browseengine.bobo.api;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class LocationContentHandler
        : LocationFieldHandler, IContentHandler
    {
        public LocationContentHandler(IBooster booster, ILocationQuery locationQuery)
            : base(FieldName.Location, FieldName.Relocations, booster, locationQuery)
        {
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            var location = content.Member.Address != null
                ? content.Member.Address.Location
                : null;

            var relocationLocations = content.Candidate.RelocationPreference == RelocationPreference.No
                ? null
                : content.Candidate.RelocationLocations;

            AddContent(document, location, relocationLocations);
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            return GetFilter(searchQuery.Location, null, searchQuery.Distance, searchQuery.IncludeRelocating, searchQuery.IncludeInternational);
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return searchQuery.Location == null ? null : GetSort(searchQuery.Location, searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }
    }
}
