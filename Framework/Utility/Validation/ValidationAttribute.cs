using System;

namespace LinkMe.Framework.Utility.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class ValidationAttribute
        : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        private readonly IValidator _validator;

        protected ValidationAttribute(IValidator validator)
        {
            _validator = validator;
        }

        public override bool IsValid(object value)
        {
            return _validator.IsValid(value);
        }

        public virtual ValidationError[] GetValidationErrors(string name)
        {
            return _validator.GetValidationErrors(name);
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateAttribute
        : Attribute
    {
    }
}
