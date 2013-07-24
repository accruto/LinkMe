namespace LinkMe.Framework.Utility.Validation
{
    public class DuplicateValidationError
        : ValidationError
    {
        public DuplicateValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }
}
