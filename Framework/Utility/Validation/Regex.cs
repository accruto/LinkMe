using System.Text.RegularExpressions;

namespace LinkMe.Framework.Utility.Validation
{
    public class RegexValidationError
        : ValidationError
    {
        public RegexValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class RegexLengthValidationError
        : ValidationError
    {
        private readonly int _length;

        public RegexLengthValidationError(string name, int length)
            : base(name)
        {
            _length = length;
        }

        public int Length
        {
            get { return _length; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _length };
        }
    }

    public class RegexLengthRangeValidationError
        : ValidationError
    {
        private readonly int _minimumLength;
        private readonly int _maximumLength;

        public RegexLengthRangeValidationError(string name, int minimumLength, int maximumLength)
            : base(name)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public int MinimumLength
        {
            get { return _minimumLength; }
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _minimumLength, _maximumLength };
        }
    }

    public class RegexMinimumLengthValidationError
        : ValidationError
    {
        private readonly int _minimumLength;

        public RegexMinimumLengthValidationError(string name, int minimumLength)
            : base(name)
        {
            _minimumLength = minimumLength;
        }

        public int MinimumLength
        {
            get { return _minimumLength; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _minimumLength };
        }
    }

    public class RegexMaximumLengthValidationError
        : ValidationError
    {
        private readonly int _maximumLength;

        public RegexMaximumLengthValidationError(string name, int maximumLength)
            : base(name)
        {
            _maximumLength = maximumLength;
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _maximumLength };
        }
    }

    public class NumericValidationError
        : RegexValidationError
    {
        public NumericValidationError(string name)
            : base(name)
        {
        }
    }

    public class NumericLengthRangeValidationError
        : RegexLengthRangeValidationError
    {
        public NumericLengthRangeValidationError(string name, int minimumLength, int maximumLength)
            : base(name, minimumLength, maximumLength)
        {
        }
    }

    public class AlphaNumericValidationError
        : RegexValidationError
    {
        public AlphaNumericValidationError(string name)
            : base(name)
        {
        }
    }

    public class AlphaNumericLengthRangeValidationError
        : RegexLengthRangeValidationError
    {
        public AlphaNumericLengthRangeValidationError(string name, int minimumLength, int maximumLength)
            : base(name, minimumLength, maximumLength)
        {
        }
    }

    public class RegexValidator
        : Validator
    {
        private readonly Regex _regex;
        private readonly int? _minimumLength;
        private readonly int? _maximumLength;

        public RegexValidator(Regex regex, int minimumLength, int maximumLength)
        {
            _regex = regex;
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public RegexValidator(Regex regex, int maximumLength)
        {
            _regex = regex;
            _maximumLength = maximumLength;
        }

        public RegexValidator(Regex regex)
        {
            _regex = regex;
        }

        protected int? MinimumLength
        {
            get { return _minimumLength; }
        }

        protected int? MaximumLength
        {
            get { return _maximumLength; }
        }

        protected override bool IsValid(object value)
        {
            if (!(value is string))
                return true;

            // Check the length first. A zero length doesn't count.

            var s = (string)value;
            if (s.Length == 0)
                return true;
            if (!IsValidLength(s.Length, _minimumLength, _maximumLength))
                return false;

            // Not set is valid, if not then add a Required attribute as well.

            if (string.IsNullOrEmpty(s))
                return true;

            // Check against the regex.

            var isValid = _regex.IsMatch(s);
            return isValid;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            if (_minimumLength != null && _maximumLength != null)
            {
                if (_minimumLength.Value == _maximumLength.Value)
                    return new[] { new RegexLengthValidationError(name, _minimumLength.Value) };
                return new[] { new RegexLengthRangeValidationError(name, _minimumLength.Value, _maximumLength.Value) };
            }
            if (_minimumLength != null)
                return new[] { new RegexMinimumLengthValidationError(name, _minimumLength.Value) };
            if (_maximumLength != null)
                return new[] { new RegexMaximumLengthValidationError(name, _maximumLength.Value) };
            return new[] { new RegexValidationError(name) };
        }
    }

    public class AlphaNumericValidator
        : RegexValidator
    {
        public AlphaNumericValidator(int minimumLength, int maximumLength)
            : base(RegularExpressions.CompleteAlphaNumeric, minimumLength, maximumLength)
        {
        }

        public AlphaNumericValidator(int maximumLength)
            : base(RegularExpressions.CompleteAlphaNumeric, maximumLength)
        {
        }

        public AlphaNumericValidator()
            : base(RegularExpressions.CompleteAlphaNumeric)
        {
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            if (MinimumLength != null && MaximumLength != null)
                return new[] { new AlphaNumericLengthRangeValidationError(name, MinimumLength.Value, MaximumLength.Value) };
            return new[] { new AlphaNumericValidationError(name) };
        }
    }

    public class NumericValidator
        : RegexValidator
    {
        public NumericValidator(int minimumLength, int maximumLength)
            : base(RegularExpressions.CompleteNumeric, minimumLength, maximumLength)
        {
        }

        public NumericValidator(int maximumLength)
            : base(RegularExpressions.CompleteNumeric, maximumLength)
        {
        }

        public NumericValidator()
            : base(RegularExpressions.CompleteNumeric)
        {
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            if (MinimumLength != null && MaximumLength != null)
                return new[] { new NumericLengthRangeValidationError(name, MinimumLength.Value, MaximumLength.Value) };
            return new[] { new NumericValidationError(name) };
        }
    }

    public abstract class RegexAttribute
        : ValidationAttribute
    {
        protected RegexAttribute(Regex regex, int minimumLength, int maximumLength)
            : base(new RegexValidator(regex, minimumLength, maximumLength))
        {
        }

        protected RegexAttribute(Regex regex, int maximumLength)
            : base(new RegexValidator(regex, maximumLength))
        {
        }

        protected RegexAttribute(Regex regex)
            : base(new RegexValidator(regex))
        {
        }
    }

    public class AlphaNumericAttribute
        : ValidationAttribute
    {
        public AlphaNumericAttribute(int minimumLength, int maximumLength)
            : base(new AlphaNumericValidator(minimumLength, maximumLength))
        {
        }

        public AlphaNumericAttribute()
            : base(new AlphaNumericValidator())
        {
        }
    }

    public class NumericAttribute
        : ValidationAttribute
    {
        public NumericAttribute(int minimumLength, int maximumLength)
            : base(new NumericValidator(minimumLength, maximumLength))
        {
        }

        public NumericAttribute()
            : base(new NumericValidator())
        {
        }
    }
}
