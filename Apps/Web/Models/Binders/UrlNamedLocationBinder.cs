using System.Web.Mvc;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Web.Models.Binders
{
    public class UrlNamedLocationBinder
        : BaseModelBinder
    {
        private readonly ILocationQuery _locationQuery;
        private readonly string[] _suffixes;

        public UrlNamedLocationBinder(ILocationQuery locationQuery, params string[] suffixes)
        {
            _locationQuery = locationQuery;
            _suffixes = suffixes ?? new string[0];
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var locationUrlName = GetLocationUrlName(bindingContext);
            return locationUrlName != null
                ? _locationQuery.ResolveUrlNamedLocation(ActivityContext.Current.Location.Country, locationUrlName)
                : null;
        }

        private string GetLocationUrlName(ModelBindingContext bindingContext)
        {
            IGetValues values = new ModelBinderValues(bindingContext);
            var locationUrlName = values.GetStringValue(bindingContext.ModelName);
            if (string.IsNullOrEmpty(locationUrlName))
                return null;

            // Remove any suffix.

            foreach (var suffix in _suffixes)
            {
                if (locationUrlName.EndsWith(suffix))
                    return locationUrlName.Substring(0, locationUrlName.Length - suffix.Length);
            }

            return locationUrlName;
        }
    }
}