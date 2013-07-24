using com.browseengine.bobo.api;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class DesiredJobTypesContentHandler
        : JobTypesFieldHandler, IContentHandler
    {
        public DesiredJobTypesContentHandler(IBooster booster)
            : base(FieldName.DesiredJobTypes, string.Empty, booster)
        {
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            AddContent(document, content.Candidate.DesiredJobTypes);
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
            return GetSelection(searchQuery.DesiredJobTypes);
        }
    }
}