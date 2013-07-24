using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinkMe.Framework.Utility.Validation
{
    public static class ValidationExtensions
    {
        public static IEnumerable<ValidationError> GetValidationErrors<T>(this T instance)
            where T : class
        {
            if (instance == null)
                return new ValidationError[0];

            // Get errors for the instance itself.

            var type = instance.GetType();
            var errors = (from a in (ValidationAttribute[]) type.GetCustomAttributes(typeof(ValidationAttribute), true)
                          where !a.IsValid(instance)
                          select a.GetValidationErrors(null)).SelectMany(e => e);

            // Get errors for all property values checking against Validation attributes.

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            errors = errors.Concat(from p in properties
                                   from e in GetValidationErrors(p, p.GetValue(instance, null))
                                   select e);

            // Iterate over all properties that themselves need validating.

            var validateProperties = from p in properties
                                     where p.GetCustomAttributes(typeof(ValidateAttribute), true).Length > 0
                                     select p;

            foreach (var validateProperty in validateProperties)
            {
                var propertyValue = validateProperty.GetValue(instance, null);
                if (propertyValue != null)
                {
                    // If it is enumerable then enumerate else set the default for the property itself.

                    if (propertyValue is IEnumerable)
                    {
                        foreach (var propertyValueItem in (IEnumerable) propertyValue)
                            errors = errors.Concat(propertyValueItem.GetValidationErrors());
                    }
                    else
                    {
                        errors = errors.Concat(propertyValue.GetValidationErrors());
                    }
                }
            }

            return errors;
        }

        private static IEnumerable<ValidationError> GetValidationErrors(PropertyInfo propertyInfo, object value)
        {
            // If there is a RequiredAttribute that fails then simply return that as there is little point in doing the others.

            var requiredAttributes = (RequiredAttribute[]) propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true);
            if (requiredAttributes.Length > 0)
            {
                var errors = (from a in requiredAttributes
                              where !a.IsValid(value)
                              select a.GetValidationErrors(propertyInfo.Name)).SelectMany(e => e);

                if (errors.Count() > 0)
                    return errors;
            }

            // Get them all.

            return (from a in (ValidationAttribute[]) propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true)
                    where !a.IsValid(value)
                    select a.GetValidationErrors(propertyInfo.Name)).SelectMany(e => e);
        }

        public static void Validate<T>(this T instance)
            where T : class
        {
            var errors = instance.GetValidationErrors();
            if (errors.Any())
                throw new ValidationErrorsException(errors);
        }
    }
}
