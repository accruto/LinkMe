using System;
using System.Globalization;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Security
{
    public class LoginIdAttribute
        : StringLengthAttribute
    {
        public LoginIdAttribute()
            : base(Constants.LoginIdMaxLength)
        {
        }
    }

    public interface IHavePasswords
    {
        string Password { get; }
        string ConfirmPassword { get; }
    }

    public class PasswordAttribute
        : StringLengthAttribute
    {
        public PasswordAttribute()
            : base(Constants.PasswordMinLength, Constants.PasswordMaxLength)
        {
        }
    }

    public class AdministratorPasswordValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (!(value is string))
                return true;

            var password = (string)value;

            // Stronger password restrictions are applied.

            if (password.Length < Constants.AdministratorPasswordMinLength || password.Length > Constants.PasswordMaxLength)
                return false;

            var haveUpper = false;
            var haveLower = false;
            var haveDigit = false;

            foreach (var c in password)
            {
                switch (char.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.DecimalDigitNumber:
                        haveDigit = true;
                        break;

                    case UnicodeCategory.LowercaseLetter:
                        haveLower = true;
                        break;

                    case UnicodeCategory.UppercaseLetter:
                        haveUpper = true;
                        break;
                }
            }

            if (!haveUpper)
                return false;
            if (!haveLower)
                return false;
            if (!haveDigit)
                return false;

            return true;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new RegexLengthRangeValidationError(name, Constants.AdministratorPasswordMinLength, Constants.PasswordMaxLength) };
        }
    }

    public class AdministratorPasswordAttribute
        : ValidationAttribute
    {
        public AdministratorPasswordAttribute()
            : base(new AdministratorPasswordValidator())
        {
        }
    }

    public class PasswordsValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            var havePasswords = value as IHavePasswords;

            // It is not invalid if it doesn't have passwords.

            if (havePasswords == null)
                return true;
            return havePasswords.Password == havePasswords.ConfirmPassword;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new DifferentValidationError("ConfirmPassword", "Password") };
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PasswordsAttribute
        : ValidationAttribute
    {
        public PasswordsAttribute()
            : base(new PasswordsValidator())
        {
        }
    }
}