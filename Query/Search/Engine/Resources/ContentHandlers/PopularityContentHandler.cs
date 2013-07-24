using com.browseengine.bobo.api;
using LinkMe.Query.Resources;
using org.apache.lucene.document;
using org.apache.lucene.search;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine.Resources.ContentHandlers
{
    internal class PopularityContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        public PopularityContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, ResourceContent content)
        {
            var field = new Field(FieldName.Popularity, Encode(content.Views), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
            field.setOmitTermFreqAndPositions(true);
            _booster.SetBoost(field);
            document.add(field);
        }

        Filter IContentHandler.GetFilter(ResourceSearchQuery searchQuery)
        {
            return null;
        }

        Sort IContentHandler.GetSort(ResourceSearchQuery sortQuery)
        {
            return new Sort(new[]
            {
                //reverse to make highest to lowest the default
                new SortField(FieldName.Popularity, SortField.INT, !sortQuery.ReverseSortOrder),
                SortField.FIELD_SCORE
            });
        }

        BrowseSelection IContentHandler.GetSelection(ResourceSearchQuery searchQuery)
        {
            return null;
        }

        private static string Encode(int viewCount)
        {
            return NumericUtils.intToPrefixCoded(viewCount);
        }
    }
}