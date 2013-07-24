namespace LinkMe.Framework.Utility.Validation
{
    public interface IValidator
    {
        bool IsValid(object value);
        ValidationError[] GetValidationErrors(string name);
    }

    public abstract class Validator
        : IValidator
    {
        protected static bool IsValidLength(int length, int? minimumLength, int? maximumLength)
        {
            if (minimumLength != null && length < minimumLength)
                return false;
            if (maximumLength != null && length > maximumLength)
                return false;
            return true;
        }

        bool IValidator.IsValid(object value)
        {
            return IsValid(value);
        }

        ValidationError[] IValidator.GetValidationErrors(string name)
        {
            return GetValidationErrors(name);
        }

        protected abstract bool IsValid(object value);
        protected abstract ValidationError[] GetValidationErrors(string name);
    }
}
