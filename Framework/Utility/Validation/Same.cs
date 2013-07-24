namespace LinkMe.Framework.Utility.Validation
{
    public class SameValidationError
        : ValidationError
    {
        private readonly string _otherName;

        public SameValidationError(string name, string otherName)
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
