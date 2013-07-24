namespace LinkMe.Framework.Utility.Validation
{
    public class CircularValidationError
        : ValidationError
    {
        public CircularValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }
}
