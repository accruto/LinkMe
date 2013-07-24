using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public class ModelBinderValues
        : Values, IGetValues
    {
        private readonly ModelBindingContext _bindingContext;

        public ModelBinderValues(ModelBindingContext bindingContext)
        {
            _bindingContext = bindingContext;
        }

        string IGetValues.GetStringValue(string key)
        {
            return GetStringValue(key);
        }

        string[] IGetValues.GetStringArrayValue(string key)
        {
            return GetStringArrayValue(key);
        }

        bool? IGetValues.GetBooleanValue(string key)
        {
            return GetBooleanValue(key);
        }

        int? IGetValues.GetIntValue(string key)
        {
            return GetIntValue(key);
        }

        int[] IGetValues.GetIntArrayValue(string key)
        {
            return GetIntArrayValue(key);
        }

        decimal? IGetValues.GetDecimalValue(string key)
        {
            return GetDecimalValue(key);
        }

        Guid? IGetValues.GetGuidValue(string key)
        {
            return GetGuidValue(key);
        }

        Guid[] IGetValues.GetGuidArrayValue(string key)
        {
            return GetGuidArrayValue(key);
        }

        DateTime? IGetValues.GetDateTimeValue(string key)
        {
            return GetDateTimeValue(key);
        }

        PartialDate? IGetValues.GetPartialDateValue(string key)
        {
            return GetPartialDateValue(key);
        }

        TEnum? IGetValues.GetValue<TEnum>(string key)
        {
            return GetValue<TEnum>(key);
        }

        TEnum? IGetValues.GetFlagsValue<TEnum>()
        {
            return GetFlagsValue<TEnum>();
        }

        IList<TValue> IGetValues.GetArrayValue<TValue>(string key)
        {
            throw new NotImplementedException();
        }

        TValue IGetValues.GetChildValue<TValue>(string key)
        {
            var type = typeof(TValue);
            if (ModelBinders.Binders.ContainsKey(type))
            {
                var binder = ModelBinders.Binders[type];

                return binder.BindModel(new ControllerContext(), _bindingContext) as TValue;
            }

            throw new NotImplementedException();
        }

        protected override object GetValue(string key)
        {
            var result = GetResult(key);
            return result == null ? null : result.RawValue;
        }

        private ValueProviderResult GetResult(string key)
        {
            // Try using the prefix first.

            if (!string.IsNullOrEmpty(_bindingContext.ModelName) && _bindingContext.ModelName != key)
            {
                var result = _bindingContext.ValueProvider.GetValue(_bindingContext.ModelName + "." + key);
                if (result != null && result.RawValue != null)
                    return result;
            }

            // Fallback.

            return _bindingContext.FallbackToEmptyPrefix
                ? _bindingContext.ValueProvider.GetValue(key)
                : null;
        }
    }
}
