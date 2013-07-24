using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Controllers.Faqs;

namespace LinkMe.Web.Areas.Public.Routes
{
    public class FaqsRoutes
    {
        public static RouteReference Faqs { get; private set; }

        public static RouteReference Subcategory { get; private set; }
        public static RouteReference PartialSubcategory { get; private set; }

        public static RouteReference Search { get; private set; }
        public static RouteReference PartialSearch { get; private set; }

        public static RouteReference Faq { get; private set; }
        public static RouteReference PartialFaq { get; private set; }

        public static RouteReference Hash { get; private set; }

        public static RouteReference ApiMarkHelpful { get; private set; }
        public static RouteReference ApiMarkNotHelpful { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Faqs = context.MapAreaRoute<FaqsController>("faqs", c => c.Faqs);

            Subcategory = context.MapAreaRoute<FaqsController, Guid>("faqs/{subcategory}/{subcategoryId}", c => c.Subcategory);
            PartialSubcategory = context.MapAreaRoute<FaqsController, Guid>("faqs/subcategory/partial", c => c.PartialSubcategory);

            Faq = context.MapAreaRoute<FaqsController, Guid, string>("faqs/{subcategory}/{title}/{id}", c => c.Faq);
            PartialFaq = context.MapAreaRoute<FaqsController, Guid, string>("faqs/faq/partial", c => c.PartialFaq);

            Search = context.MapAreaRoute<FaqsController, Guid?, string>("faqs/search", c => c.Search);
            PartialSearch = context.MapAreaRoute<FaqsController, Guid?, string>("faqs/search/partial", c => c.PartialSearch);

            Hash = context.MapAreaRoute<FaqsController, Guid?, Guid?, Guid?, string>("faqs/hash", c => c.Hash);

            ApiMarkHelpful = context.MapAreaRoute<FaqsApiController, Guid>("faqs/api/helpful", c => c.MarkFaqHelpful);
            ApiMarkNotHelpful = context.MapAreaRoute<FaqsApiController, Guid>("faqs/api/nothelpful", c => c.MarkFaqNotHelpful);

            context.MapRedirectRoute("ui/unregistered/common/emailfeedbackform.aspx", Faqs);

            context.MapRedirectRoute("faq", Faqs);
            context.MapAreaRoute<FaqsController, string, string>(false, "faq/{subcategory}/{subcategoryId}", c => c.OldSubcategoryFaqs);
            context.MapAreaRoute<FaqsController, string, string>(false, "faqs/{subcategory}/{subcategoryId}", c => c.OldSubcategoryFaqs);
            context.MapRedirectRoute("faq/{subcategory}/{title}/{id}", Faq, new { subcategory = new RedirectRouteValue(), title = new RedirectRouteValue(), id = new RedirectRouteValue() });
            context.MapRedirectRoute("faq/search", Search);
        }
    }
}
