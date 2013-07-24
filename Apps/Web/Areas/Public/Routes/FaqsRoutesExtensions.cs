using System.Collections.Generic;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Routes
{
    public static class FaqsRoutesExtensions
    {
        public static ReadOnlyUrl GenerateUrl(this Subcategory subcategory)
        {
            return FaqsRoutes.Subcategory.GenerateUrl(new
            {
                subcategory = subcategory.Name.EncodeUrlSegment(),
                subcategoryId = subcategory.Id,
            });
        }

        public static ReadOnlyUrl GenerateUrl(this Faq faq, IEnumerable<Category> categories)
        {
            return FaqsRoutes.Faq.GenerateUrl(new
            {
                id = faq.Id,
                subcategory = categories.GetSubcategory(faq.SubcategoryId).Name.EncodeUrlSegment(),
                title = faq.Title.EncodeUrlSegment(),
            });
        }
    }
}
