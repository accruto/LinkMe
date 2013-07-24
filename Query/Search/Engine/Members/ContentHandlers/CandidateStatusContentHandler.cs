using System.Collections.Generic;
using com.browseengine.bobo.api;
using LinkMe.Domain;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class CandidateStatusContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        private readonly HashSet<CandidateStatus> _allStatuses = new HashSet<CandidateStatus>(new List<CandidateStatus>
        {
            CandidateStatus.AvailableNow,
            CandidateStatus.ActivelyLooking,
            CandidateStatus.OpenToOffers,
            CandidateStatus.Unspecified,
            CandidateStatus.NotLooking,
        });

        public CandidateStatusContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            var value = content.Candidate.Status.Encode();
            var field = new Field(FieldName.CandidateStatus, value, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
            field.setOmitTermFreqAndPositions(true);
            _booster.SetBoost(field);
            document.add(field);
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            if (searchQuery.CandidateStatusList == null || _allStatuses.IsSubsetOf(searchQuery.CandidateStatusList))
                return null;

            var selection = new BrowseSelection(FieldName.CandidateStatus);
            selection.setSelectionOperation(BrowseSelection.ValueOperation.ValueOperationOr);

            foreach (var status in searchQuery.CandidateStatusList)
                selection.addValue(status.Encode());

            return selection;
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            return new Sort(new[]
            {
                new SortField(FieldName.CandidateStatus, SortField.INT, !searchQuery.ReverseSortOrder),
                SortField.FIELD_SCORE
            });
        }
    }
}