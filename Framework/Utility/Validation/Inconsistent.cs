namespace LinkMe.Framework.Utility.Validation
{
    public class InconsistentValidationError
        : ValidationError
    {
        private readonly string _otherName;

        public InconsistentValidationError(string name, string otherName)
            : base(name)
        {
            _otherName = otherName;
        }

        public string OtherName
        {
            get { return _otherName; }
        }
    }
}
