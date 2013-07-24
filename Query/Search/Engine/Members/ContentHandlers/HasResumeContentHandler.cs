using com.browseengine.bobo.api;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.util;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class HasResumeContentHandler
        : IContentHandler
    {
        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            document.add(new NumericField(FieldName.HasResume).setIntValue(content.Resume == null || content.Resume.IsEmpty ? 0 : 1));
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            if (searchQuery.HasResume == null)
                return null;

            var filter = new TermsFilter();
            filter.addTerm(new Term(FieldName.HasResume, NumericUtils.intToPrefixCoded(searchQuery.HasResume.Value ? 1 : 0)));
            return filter;
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }
    }
}
