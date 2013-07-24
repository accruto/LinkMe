using System;
using System.Collections.Generic;
using System.Web.Routing;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Resources;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class ResourcesRoutesExtensions
    {
        public static ReadOnlyUrl GenerateUrl(this Article article, IList<Category> categories)
        {
            return ResourcesRoutes.Article.GenerateUrl(article.GetRouteValues(categories));
        }

        public static ReadOnlyUrl GenerateUrl(this Video video, IList<Category> categories)
        {
            return ResourcesRoutes.Video.GenerateUrl(video.GetRouteValues(categories));
        }

        public static ReadOnlyUrl GenerateUrl(this QnA qna, IList<Category> categories)
        {
            return ResourcesRoutes.QnA.GenerateUrl(qna.GetRouteValues(categories));
        }

        public static ReadOnlyUrl GenerateUrl<TResource>(this TResource resource, IList<Category> categories)
            where TResource : Resource
        {
            if (resource is Article)
                return (resource as Article).GenerateUrl(categories);

            if (resource is Video)
                return (resource as Video).GenerateUrl(categories);

            return (resource as QnA).GenerateUrl(categories);
        }

        public static ReadOnlyUrl GenerateUrl(this Subcategory subcategory, ResourceType resourceType, IList<Category> categories)
        {
            return GetCategoryRoute(resourceType).GenerateUrl(subcategory.GetRouteValues(categories));
        }

        public static ReadOnlyUrl GenerateUrl(this Category category, ResourceType resourceType)
        {
            return GetCategoryRoute(resourceType).GenerateUrl(category.GetRouteValues());
        }

        public static ReadOnlyUrl GenerateUrl(this ResourceSearchCriteria criteria, int? page, IList<Category> categories)
        {
            return criteria.CategoryId == null && criteria.SubcategoryId == null
                ? GetRoute(criteria.ResourceType).GenerateUrl(criteria.GetRouteValues(page, categories))
                : GetCategoryRoute(criteria.ResourceType).GenerateUrl(criteria.GetRouteValues(page, categories));
        }

        private static RouteReference GetRoute(ResourceType resourceType)
        {
            return resourceType == ResourceType.QnA
                ? ResourcesRoutes.QnAs
                : resourceType == ResourceType.Video
                    ? ResourcesRoutes.Videos
                    : ResourcesRoutes.Articles;
        }

        private static RouteReference GetCategoryRoute(ResourceType resourceType)
        {
            return resourceType == ResourceType.QnA
                ? ResourcesRoutes.CategoryQnAs
                : resourceType == ResourceType.Video
                    ? ResourcesRoutes.CategoryVideos
                    : ResourcesRoutes.CategoryArticles;
        }

        private static string GetUrlSegmentTitle(string title)
        {
            // To avoid extra long segments cut off at end of first statement.

            var fullstop = title.IndexOf('.');
            var question = title.IndexOf('?');
            if (fullstop == -1 && question == -1)
                return title;

            var index = fullstop == -1
                ? question
                : question == -1
                    ? fullstop
                    : Math.Min(fullstop, question);

            return title.Substring(0, index);
        }

        public static RouteValueDictionary GetRouteValues(this ResourceSearchCriteria criteria, int? page, IList<Category> categories)
        {
            var routeValues = criteria.SubcategoryId != null
                ? categories.GetSubcategory(criteria.SubcategoryId.Value).GetRouteValues(categories)
                : criteria.CategoryId != null
                    ? categories.GetCategory(criteria.CategoryId.Value).GetRouteValues()
                    : new RouteValueDictionary();

            if (!string.IsNullOrEmpty(criteria.Keywords))
                routeValues.Add(ResourceSearchCriteriaKeys.Keywords, criteria.Keywords);
            if (page != null && page.Value != 1)
                routeValues.Add("Page", page.Value);

            return routeValues;
        }

        private static RouteValueDictionary GetRouteValues(this Resource resource, IList<Category> categories)
        {
            return new RouteValueDictionary
            {
                { "id", resource.Id },
                {
                    "category",
                    categories.GetCategoryBySubcategory(resource.SubcategoryId).Name.EncodeUrlSegment()
                        + "-"
                        + categories.GetSubcategory(resource.SubcategoryId).Name.EncodeUrlSegment()
                        + "-"
                        + GetUrlSegmentTitle(resource.Title).EncodeUrlSegment()
                },
            };
        }

        private static RouteValueDictionary GetRouteValues(this Subcategory subcategory, IEnumerable<Category> categories)
        {
            return new RouteValueDictionary
            {
                { "category", categories.GetCategoryBySubcategory(subcategory.Id).Name.EncodeUrlSegment() + "-" + subcategory.Name.EncodeUrlSegment() },
                { "subcategoryId", subcategory.Id },
            };
        }

        private static RouteValueDictionary GetRouteValues(this Category category)
        {
            return new RouteValueDictionary
            {
                { "category", category.Name.EncodeUrlSegment() },
                { "categoryId", category.Id },
            };
        }
    }
}
