using System;

namespace LinkMe.Framework.Utility.Validation
{
    public class ArrayLengthValidator
        : Validator
    {
        private readonly int? _minimumLength;
        private readonly int? _maximumLength;

        public ArrayLengthValidator(int? minimumLength, int? maximumLength)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public ArrayLengthValidator(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        protected override bool IsValid(object value)
        {
            if (value is Array)
            {
                var array = (Array)value;
                if (_minimumLength != null && array.Length < _minimumLength)
                    return false;
                return _maximumLength == null || array.Length <= _maximumLength;
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
            return _maximumLength != null
                ? new[] { new MaximumLengthValidationError(name, _maximumLength.Value) }
                : new ValidationError[0];
        }
    }

    public class ArrayLengthAttribute
        : ValidationAttribute
    {
        public ArrayLengthAttribute(int minimumLength, int maximumLength)
            : base(new ArrayLengthValidator(minimumLength, maximumLength))
        {
        }

        public ArrayLengthAttribute(int maximumLength)
            : base(new ArrayLengthValidator(maximumLength))
        {
        }
    }
}
