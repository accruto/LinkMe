namespace LinkMe.Framework.Utility.Validation
{
    public class NumericMinimumValueValidationError
        : ValidationError
    {
        private readonly int _minimum;

        public NumericMinimumValueValidationError(string name, int minimum)
            : base(name)
        {
            _minimum = minimum;
        }
        
        public int Minimum
        {
            get { return _minimum; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _minimum };
        }
    }

    public class NumericMaximumValueValidationError
        : ValidationError
    {
        private readonly int _maximum;

        public NumericMaximumValueValidationError(string name, int maximum)
            : base(name)
        {
            _maximum = maximum;
        }

        public int Maximum
        {
            get { return _maximum; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _maximum };
        }
    }

    public class NumericValueRangeValidationError
        : ValidationError
    {
        private readonly int _minimum;
        private readonly int _maximum;

        public NumericValueRangeValidationError(string name, int minimum, int maximum)
            : base(name)
        {
            _minimum = minimum;
            _maximum = maximum;
        }

        public int Minimum
        {
            get { return _minimum; }
        }

        public int Maximum
        {
            get { return _maximum; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), _minimum, _maximum };
        }
    }

    public class NumericValueValidator
        : Validator
    {
        private readonly int? _minimum;
        private readonly int? _maximum;

        public NumericValueValidator(int minimum, int maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
        }

        public NumericValueValidator(int minimum)
        {
            _minimum = minimum;
        }

        protected override bool IsValid(object value)
        {
            if (value is int)
            {
                var i = (int)value;
                if (_minimum != null && i < _minimum.Value)
                    return false;
                return _maximum == null || i <= _maximum.Value;
            }

            return true;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            if (_minimum != null && _maximum != null)
                return new[] { new NumericValueRangeValidationError(name, _minimum.Value, _maximum.Value) };
            if (_minimum != null)
                return new[] { new NumericMinimumValueValidationError(name, _minimum.Value) };
            if (_maximum != null)
                return new[] { new NumericMinimumValueValidationError(name, _maximum.Value) };
            return new ValidationError[0];
        }
    }

    public class NumericValueAttribute
        : ValidationAttribute
    {
        public NumericValueAttribute(int minimum, int maximum)
            : base(new NumericValueValidator(minimum, maximum))
        {
        }

        public NumericValueAttribute(int minimum)
            : base(new NumericValueValidator(minimum))
        {
        }
    }
}
