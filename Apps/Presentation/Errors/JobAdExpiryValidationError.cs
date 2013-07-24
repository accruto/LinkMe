using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Presentation.Errors
{
    public class JobAdExpiryValidationError
        : ValidationError
    {
        private readonly int _days;

        public JobAdExpiryValidationError(string name, int days)
            : base(name)
        {
            _days = days;
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { _days };
        }
    }
}
