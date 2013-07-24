using System;

namespace LinkMe.Framework.Utility.Validation
{
    public class IsSetValidationError
        : ValidationError
    {
        public IsSetValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class IsSetValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (value is Guid)
                return (Guid)value != Guid.Empty;

            if (value is DateTime)
            {
                var dt = (DateTime)value;
                return dt != DateTime.MinValue && dt != DateTime.MaxValue;
            }

            return false;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new IsSetValidationError(name) };
        }
    }

    public class IsSetAttribute
        : ValidationAttribute
    {
        public IsSetAttribute()
            : base(new IsSetValidator())
        {
        }
    }
}
