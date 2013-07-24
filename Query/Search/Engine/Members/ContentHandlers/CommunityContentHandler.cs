using LinkMe.Domain.Contacts;
using com.browseengine.bobo.api;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class CommunityContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        public CommunityContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            if (content.Member.AffiliateId != null
                && content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                && content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Communities))
            {
                var field = new Field(FieldName.Community, content.Member.AffiliateId.Value.ToFieldValue(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                _booster.SetBoost(field);
                document.add(field);
            }
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            if (searchQuery.CommunityId == null)
                return null;

            var filter = new TermsFilter();
            filter.addTerm(new Term(FieldName.Community, searchQuery.CommunityId.Value.ToFieldValue()));
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