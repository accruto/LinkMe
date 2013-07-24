using System.Collections.Generic;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public class IntArrayBinder
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

            var ids = new List<int>();
            foreach (var value in rawValue)
            {
                // Need to convert the list of ids as strings.

                int intValue;
                if (int.TryParse(value, out intValue))
                    ids.Add(intValue);
            }

            return ids.ToArray();
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
