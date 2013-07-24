using System;
using com.browseengine.bobo.api;
using LinkMe.Domain.Resources;
using LinkMe.Query.Resources;
using org.apache.lucene.document;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine.Resources.ContentHandlers
{
    internal class ItemTypeContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        public ItemTypeContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, ResourceContent content)
        {
            var field = new Field(FieldName.ItemType, GetFieldValue(content.Resource), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
            field.setOmitTermFreqAndPositions(true);
            _booster.SetBoost(field);
            document.add(field);
        }

        Filter IContentHandler.GetFilter(ResourceSearchQuery searchQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(ResourceSearchQuery searchQuery)
        {
            if (searchQuery.ResourceType == null)
                return null;

            var selection = new BrowseSelection(FieldName.ItemType);
            selection.addValue(searchQuery.ResourceType.Value.Encode());

            return selection;
        }

        Sort IContentHandler.GetSort(ResourceSearchQuery sortQuery)
        {
            return null;
        }

        private static string GetFieldValue(Resource resource)
        {
            if (resource is Article)
                return ResourceType.Article.Encode();
            if (resource is QnA)
                return ResourceType.QnA.Encode();
            if (resource is Video)
                return ResourceType.Video.Encode();
            if (resource is Faq)
                return ResourceType.Faq.Encode();

            throw new NotImplementedException("Unrecognized Resource Item Type");
        }
    }
}