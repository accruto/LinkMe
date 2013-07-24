using com.browseengine.bobo.api;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using LuceneFilter = org.apache.lucene.search.Filter;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.Members
{
    internal interface IContentHandler
    {
        void AddContent(Document document, MemberContent content);
        LuceneFilter GetFilter(MemberSearchQuery query);
        LuceneSort GetSort(MemberSearchQuery query);
        BrowseSelection GetSelection(MemberSearchQuery query);
    }
}