namespace LinkMe.Framework.Utility.Validation
{
    public class RequiredValidationError
        : ValidationError
    {
        public RequiredValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class RequiredValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (value == null)
                return false;
            return !(value is string) || !string.IsNullOrEmpty((string)value);
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new RequiredValidationError(name) };
        }
    }

    public class RequiredAttribute
        : ValidationAttribute
    {
        public RequiredAttribute()
            : base(new RequiredValidator())
        {
        }
    }
}
