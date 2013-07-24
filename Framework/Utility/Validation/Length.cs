namespace LinkMe.Framework.Utility.Validation
{
    public class LengthValidationError
        : ValidationError
    {
        private readonly int _length;

        public LengthValidationError(string name, int length)
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

    public class LengthRangeValidationError
        : ValidationError
    {
        private readonly int _minimumLength;
        private readonly int _maximumLength;

        public LengthRangeValidationError(string name, int minimumLength, int maximumLength)
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

    public class MinimumLengthValidationError
        : ValidationError
    {
        private readonly int _minimumLength;

        public MinimumLengthValidationError(string name, int minimumLength)
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

    public class MaximumLengthValidationError
        : ValidationError
    {
        private readonly int _maximumLength;

        public MaximumLengthValidationError(string name, int maximumLength)
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
}
