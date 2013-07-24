using LinkMe.Framework.Utility;
using com.browseengine.bobo.api;
using LinkMe.Domain;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class VisaStatusContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        public VisaStatusContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            var value = content.Candidate.VisaStatus.Encode();
            var field = new Field(FieldName.VisaStatus, value, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
            field.setOmitTermFreqAndPositions(true);
            _booster.SetBoost(field);
            document.add(field);
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            if (searchQuery.VisaStatusList.IsNullOrEmpty())
                return null;

            var filter = new TermsFilter();
            foreach (var status in searchQuery.VisaStatusList)
            {
                filter.addTerm(new Term(FieldName.VisaStatus, status.Encode()));

                // Everyone who does not have a visa status is considered a citizen.

                if (status == VisaStatus.Citizen)
                {
                    filter.addTerm(new Term(FieldName.VisaStatus, ((VisaStatus?) null).Encode()));
                    filter.addTerm(new Term(FieldName.VisaStatus, VisaStatus.NotApplicable.Encode()));
                }
            }
            return filter;
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return null;
        }
    }
}