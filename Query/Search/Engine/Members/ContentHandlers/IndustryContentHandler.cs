using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class IndustryContentHandler
        : IndustryFieldHandler, IContentHandler
    {
        public IndustryContentHandler(IBooster booster, IIndustriesQuery industriesQuery)
            : base(FieldName.Industries, booster, industriesQuery)
        {
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            AddContent(document, content.Candidate.Industries == null ? null : from i in content.Candidate.Industries select i.Id);
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            return null;
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return GetSelection(searchQuery.IndustryIds);
        }
    }
}