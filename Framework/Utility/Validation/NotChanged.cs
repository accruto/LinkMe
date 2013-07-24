namespace LinkMe.Framework.Utility.Validation
{
    public class NotChangedValidationError
        : ValidationError
    {
        private readonly string _from;

        public NotChangedValidationError(string name, string from)
            : base(name)
        {
            _from = from;
        }

        public string From
        {
            get { return _from; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }
}
