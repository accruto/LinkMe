namespace LinkMe.Framework.Utility.Validation
{
    public class StringLengthValidator
        : Validator
    {
        private readonly int? _minimumLength;
        private readonly int? _maximumLength;

        public StringLengthValidator(int minimumLength, int maximumLength)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public StringLengthValidator(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        protected override bool IsValid(object value)
        {
            if (value is string)
                return IsValidLength(((string)value).Length, _minimumLength, _maximumLength);

            if (value is string[])
            {
                var a = (string[])value;
                foreach (var avalue in a)
                {
                    if (!IsValidLength(avalue.Length, _minimumLength, _maximumLength))
                        return false;
                }
            }

            return true;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            if (_minimumLength != null && _maximumLength != null)
            {
                if (_minimumLength.Value == _maximumLength.Value)
                    return new[] { new LengthValidationError(name, _minimumLength.Value) };
                return new[] { new LengthRangeValidationError(name, _minimumLength.Value, _maximumLength.Value) };
            }
            if (_minimumLength != null)
                return new[] { new MinimumLengthValidationError(name, _minimumLength.Value) };
            if (_maximumLength != null)
                return new[] { new MaximumLengthValidationError(name, _maximumLength.Value) };
            return new ValidationError[0];
        }
    }

    public class StringLengthAttribute
        : ValidationAttribute
    {
        public StringLengthAttribute(int minimumLength, int maximumLength)
            : base(new StringLengthValidator(minimumLength, maximumLength))
        {
        }

        public StringLengthAttribute(int maximumLength)
            : base(new StringLengthValidator(maximumLength))
        {
        }
    }
}
