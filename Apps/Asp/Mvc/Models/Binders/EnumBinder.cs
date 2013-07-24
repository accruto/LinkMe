using System;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    /// <summary>
    /// To be used with an enum that can take on one of its values. The value of the enum is set to that value in the provider,
    /// eg JobsToSearchCriteria enum, JobsToSearchCriteria=LastThreeJobs.
    /// If no value is provided then null will be returned.
    /// Example: EnumDropDownListField, ie a drop down where each item corresponds to one of the enum flags.
    /// </summary>
    public class EnumBinder<TEnum>
        : BaseModelBinder
        where TEnum : struct
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            var value = GetValue(bindingContext, string.IsNullOrEmpty(bindingContext.ModelName) ? typeof(TEnum).Name : bindingContext.ModelName);
            if (value == null)
                return null;

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
