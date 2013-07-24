namespace LinkMe.Framework.Utility.Validation
{
    public class NotFoundValidationError
        : ValidationError
    {
        private readonly object _value;

        public NotFoundValidationError(string name, object value)
            : base(name)
        {
            _value = value;
        }

        public object Value
        {
            get { return _value; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new[] { new ValidationErrorName(Name), Value };
        }
    }
}
