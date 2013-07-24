using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public class CheckBoxBinder
        : BaseModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // The Html.CheckBox outputs an input field and a hidden field, both with the same name.
            // If the checkbox is checked then the form will contain "true,false" whilst if unchecked
            // it will contain "false".

            // Expecting an array of strings.
            // The name may come through with a trailing "[]" to indicate an array so check for that after trying the name itself.

            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName) ??
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[]");
            if (result == null)
                return null;

            // Put result in model to cause control repopulation.

            var array = result.RawValue as string[];
            if (array == null)
            {
                AddToModel(controllerContext, bindingContext.ModelName, result);
                return null;
            }

            // This is here because the ASP.NET tester uses a hashtable which does not maintain order
            // when multiple valued form variables are used, as is the case for ASP.NET MVC.
            // Make sure the "true" comes first, otherwise the Html.CheckBox rendering will be no good.

            if (array.Length == 2)
            {
                if (array[0] == "false" && array[1] == "true")
                {
                    array[0] = "true";
                    array[1] = "false";
                }
            }

            foreach (var value in array)
            {
                bool bvalue;
                if (bool.TryParse(value, out bvalue) && bvalue)
                {
                    AddToModel(controllerContext, bindingContext.ModelName, result);
                    return new CheckBoxValue { IsChecked = true };
                }
            }

            AddToModel(controllerContext, bindingContext.ModelName, result);
            return new CheckBoxValue { IsChecked = false };
        }
    }
}
