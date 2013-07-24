using System;
using System.Web.Mvc;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public abstract class BaseModelBinder
        : IModelBinder
    {
        public abstract object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);

        protected void AddToModel(ControllerContext controllerContext, string name, ValueProviderResult result)
        {
            if (controllerContext != null && !controllerContext.Controller.ViewData.ModelState.ContainsKey(name))
                controllerContext.Controller.ViewData.ModelState.Add(name, new ModelState { Value = result });
        }

        protected static string GetValue(ModelBindingContext bindingContext, string key)
        {
            var result = GetResult(bindingContext, key);
            if (result == null)
                return null;

            // Only return non-null/non-empty values.

            var rawValue = result.RawValue;
            if (rawValue is string)
            {
                var stringValue = (string) rawValue;
                return string.IsNullOrEmpty(stringValue) ? null : stringValue;
            }

            if (rawValue is string[])
            {
                var arrayValue = (string[]) rawValue;
                if (arrayValue.Length == 1 && !string.IsNullOrEmpty(arrayValue[0]))
                    return arrayValue[0];
                return null;
            }

            return null;
        }

        private static ValueProviderResult GetResult(ModelBindingContext bindingContext, string key)
        {
            // Try using the prefix first.

            if (!string.IsNullOrEmpty(bindingContext.ModelName) && bindingContext.ModelName != key)
            {
                var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
                if (result != null && result.RawValue != null)
                    return result;
            }

            // Fallback.

            return bindingContext.FallbackToEmptyPrefix
                ? bindingContext.ValueProvider.GetValue(key)
                : null;
        }
    }

    public class ModelBinder
        : IModelBinder
    {
        private readonly IDeconverter _deconverter;
        private readonly IErrorHandler _errorHandler;

        public ModelBinder(IDeconverter deconverter, IErrorHandler errorHandler)
        {
            _deconverter = deconverter;
            _errorHandler = errorHandler;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return _deconverter.Deconvert(new ModelBinderValues(bindingContext), new ModelBinderErrors(bindingContext, _errorHandler));
        }
    }

    public class GenericModelBinder
        : BaseModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            IGetValues values = new ModelBinderValues(bindingContext);
            return values.GetStringValue(bindingContext.ModelName);
        }
    }
}
