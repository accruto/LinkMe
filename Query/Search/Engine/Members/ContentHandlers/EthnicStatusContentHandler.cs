using System.Collections.Generic;
using com.browseengine.bobo.api;
using LinkMe.Domain;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.util;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class EthnicStatusContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        private static readonly EthnicStatus[] AllStatuses = new[]
        {
             EthnicStatus.Aboriginal,
             EthnicStatus.TorresIslander
        };

        public EthnicStatusContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            foreach (var status in Split(content.Member.EthnicStatus))
            {
                var value = Encode(status);
                var field = new Field(FieldName.EthnicStatus, value, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                _booster.SetBoost(field);
                document.add(field);
            }
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            if (!searchQuery.EthnicStatus.HasValue || searchQuery.EthnicStatus.Value == default(EthnicStatus))
                return null;

            var filter = new TermsFilter();
            foreach (var status in Split(searchQuery.EthnicStatus.Value))
                filter.addTerm(new Term(FieldName.EthnicStatus, Encode(status)));
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

        private static string Encode(EthnicStatus status)
        {
            return NumericUtils.intToPrefixCoded((int)status);
        }

        private static IEnumerable<EthnicStatus> Split(EthnicStatus flags)
        {
            if (flags == default(EthnicStatus))
                yield break;

            foreach (var status in AllStatuses)
            {
                if ((flags & status) != 0)
                    yield return status;
            }
        }
    }
}