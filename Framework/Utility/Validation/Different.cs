namespace LinkMe.Framework.Utility.Validation
{
    public class DifferentValidationError
        : ValidationError
    {
        private readonly string _otherName;

        public DifferentValidationError(string name, string otherName)
            : base(name)
        {
            _otherName = otherName;
        }

        public string OtherName
        {
            get { return _otherName; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name), new ValidationErrorName(OtherName) };
        }
    }
}
