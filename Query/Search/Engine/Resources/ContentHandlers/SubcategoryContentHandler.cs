using System.Collections.Generic;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Query.Resources;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine.Resources.ContentHandlers
{
    internal class SubcategoryContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;
        private readonly IList<Category> _categories;

        internal SubcategoryContentHandler(IBooster booster, IResourcesQuery resourcesQuery, IFaqsQuery faqsQuery)
        {
            _booster = booster;
            var resourceCategories = resourcesQuery.GetCategories();
            var faqCategories = faqsQuery.GetCategories();

            _categories = resourceCategories.Concat(faqCategories).ToList();
        }

        void IContentHandler.AddContent(Document document, ResourceContent content)
        {
            var field = new Field(FieldName.SubcategoryId, content.Resource.SubcategoryId.ToFieldValue(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
            field.setOmitTermFreqAndPositions(true);
            _booster.SetBoost(field);
            document.add(field);
        }

        Filter IContentHandler.GetFilter(ResourceSearchQuery searchQuery)
        {
            if (!searchQuery.SubcategoryId.HasValue && !searchQuery.CategoryId.HasValue)
                return null;

            var filter = new TermsFilter();

            //use subcategory in preference to category
            if (searchQuery.SubcategoryId.HasValue)
            {
                filter.addTerm(new Term(FieldName.SubcategoryId, searchQuery.SubcategoryId.Value.ToFieldValue()));
            }
            else
            {
                var category = _categories.FirstOrDefault(c => c.Id == searchQuery.CategoryId);
                if (category != null)
                {
                    foreach (var subcategory in category.Subcategories)
                        filter.addTerm(new Term(FieldName.SubcategoryId, subcategory.Id.ToFieldValue()));
                }
            }

            return filter;
        }

        Sort IContentHandler.GetSort(ResourceSearchQuery sortQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(ResourceSearchQuery searchQuery)
        {
            return null;
        }
    }
}