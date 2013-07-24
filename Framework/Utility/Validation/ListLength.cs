using System;
using System.Collections;

namespace LinkMe.Framework.Utility.Validation
{
    public class ListLengthValidator
        : Validator
    {
        private readonly int? _minimumLength;
        private readonly int? _maximumLength;

        public ListLengthValidator(int? minimumLength, int? maximumLength)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public ListLengthValidator(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        protected override bool IsValid(object value)
        {
            if (value is IList)
            {
                var list = (IList)value;
                if (_minimumLength != null && list.Count < _minimumLength)
                    return false;
                return _maximumLength == null || list.Count <= _maximumLength;
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

    public class ListLengthAttribute
        : ValidationAttribute
    {
        public ListLengthAttribute(int minimumLength, int maximumLength)
            : base(new ListLengthValidator(minimumLength, maximumLength))
        {
        }

        public ListLengthAttribute(int maximumLength)
            : base(new ListLengthValidator(maximumLength))
        {
        }
    }
}
