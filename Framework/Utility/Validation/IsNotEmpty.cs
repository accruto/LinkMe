namespace LinkMe.Framework.Utility.Validation
{
    public interface ICanBeEmpty
    {
        bool IsEmpty { get; }
    }

    public class IsNotEmptyValidationError
        : ValidationError
    {
        public IsNotEmptyValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class IsNotEmptyValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (!(value is ICanBeEmpty))
                return true;
            return !((ICanBeEmpty) value).IsEmpty;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new IsNotEmptyValidationError(name) };
        }
    }

    public class IsNotEmptyAttribute
        : ValidationAttribute
    {
        public IsNotEmptyAttribute()
            : base(new IsNotEmptyValidator())
        {
        }
    }
}
