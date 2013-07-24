using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Industries.Queries;

namespace LinkMe.Web.Models.Binders
{
    public class IndustryBinder
        : BaseModelBinder
    {
        private readonly IIndustriesQuery _industriesQuery;
        private readonly string[] _suffixes;

        public IndustryBinder(IIndustriesQuery industriesQuery, params string[] suffixes)
        {
            _industriesQuery = industriesQuery;
            _suffixes = suffixes ?? new string[0];
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var industryUrlName = GetIndustryUrlName(bindingContext);
            return industryUrlName != null
                ? _industriesQuery.GetIndustryByUrlName(industryUrlName)
                : null;
        }

        private string GetIndustryUrlName(ModelBindingContext bindingContext)
        {
            IGetValues values = new ModelBinderValues(bindingContext);
            var industryUrlName = values.GetStringValue(bindingContext.ModelName);
            if (string.IsNullOrEmpty(industryUrlName))
                return null;

            // Remove any suffix.

            foreach (var suffix in _suffixes)
            {
                if (industryUrlName.EndsWith(suffix))
                    return industryUrlName.Substring(0, industryUrlName.Length - suffix.Length);
            }

            return industryUrlName;
        }
    }
}