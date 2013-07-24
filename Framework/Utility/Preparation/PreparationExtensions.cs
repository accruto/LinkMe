using System.Collections;
using System.Linq;
using System.Reflection;

namespace LinkMe.Framework.Utility.Preparation
{
    public static class PreparationExtensions
    {
        public static void Prepare<T>(this T instance)
        {
            // Prepare the instance itself.

            var type = instance.GetType();
            var attributes = from a in (InstancePreparationAttribute[]) type.GetCustomAttributes(typeof(InstancePreparationAttribute), true)
                             select a;

            foreach (var attribute in attributes)
                attribute.PrepareValue(instance);

            // Prepare each property that has a preparation attribute.

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var propertyAttributes = from p in properties
                                     from a in (PreparationAttribute[]) p.GetCustomAttributes(typeof(PreparationAttribute), true)
                                     select new { p, a };

            foreach (var propertyAttribute in propertyAttributes)
            {
                object value;
                if (propertyAttribute.a.GetValue(propertyAttribute.p.GetValue(instance, null), out value))
                    propertyAttribute.p.SetValue(instance, value, null);
            }

            // Iterate over all properties that themselves need preparing.

            var prepareProperties = from p in properties
                                    where p.GetCustomAttributes(typeof(PrepareAttribute), true).Length > 0
                                    select p;

            foreach (var prepareProperty in prepareProperties)
            {
                var propertyValue = prepareProperty.GetValue(instance, null);
                if (propertyValue != null)
                {
                    // If it is enumerable then enumerate else set prepare the property itself.

                    if (propertyValue is IEnumerable)
                    {
                        foreach (var propertyValueItem in (IEnumerable)propertyValue)
                            propertyValueItem.Prepare();
                    }
                    else
                    {
                        propertyValue.Prepare();
                    }
                }
            }
        }
    }
}