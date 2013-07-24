using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public class GuidArrayBinder
        : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Expecting an array of string ids. Use the model name directly.

            var rawValue = GetRawValue(bindingContext, bindingContext.ModelName);

            if (rawValue == null)
            {
                // Try a simple pluralised form - should I do this, probably not but it makes it so convenient ...

                var modelName = bindingContext.ModelName;
                if (modelName.EndsWith("s"))
                {
                    modelName = modelName.Substring(0, modelName.Length - 1);
                    rawValue = GetRawValue(bindingContext, modelName);
                }
            }

            if (rawValue == null)
            {
                // Try the name with "[]" at the end, particularly for JSON requests.

                rawValue = GetRawValue(bindingContext, bindingContext.ModelName + "[]");
            }

            if (rawValue == null)
                return null;

            var guids = new List<Guid>();
            foreach (var value in rawValue)
            {
                // Need to convert the list of ids as strings.

                try
                {
                    guids.Add(new Guid(value));
                }
                catch (ArgumentException)
                {
                    // Ignore all errors for now.
                }
                catch (FormatException)
                {
                    // Ignore all errors for now.
                }
            }

            return guids.ToArray();
        }

        private static string[] GetRawValue(ModelBindingContext bindingContext, string modelName)
        {
            var result = bindingContext.ValueProvider.GetValue(modelName);
            if (result == null || !(result.RawValue is string[]))
                return null;

            var array = (string[]) result.RawValue;
            return array.Length == 0 ? null : array;
        }
    }
}
