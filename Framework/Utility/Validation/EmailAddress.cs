using System;
using System.Collections.Concurrent;
using System.Net.Mail;
using LinkMe.Environment;

namespace LinkMe.Framework.Utility.Validation
{
    public enum EmailAddressValidationMode
    {
        /// <summary>
        /// Allow only the username part of the email (that should come before the '@').
        /// </summary>
        UserOnly,
        /// <summary>
        /// Allow only a single, complete email address.
        /// </summary>
        SingleEmail,
        /// <summary>
        /// Allow one or more complete email addresses, separated by commas or semicolons.
        /// </summary>
        MultipleEmails,
    }

    public class EmailAddressValidationError
        : RegexMaximumLengthValidationError
    {
        public EmailAddressValidationError(string name)
            : base(name, Constants.EmailAddressMaxLength)
        {
        }
    }

    public class EmailAddressHostValidationError
        : ValidationError
    {
        public EmailAddressHostValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class EmailAddressValidator
        : Validator
    {
        private readonly EmailAddressValidationMode _validationMode;
        private readonly bool _checkDns;
        private readonly ConcurrentDictionary<string, bool> _hostCache;
        private ValidationError _validationError;

        public EmailAddressValidator(EmailAddressValidationMode validationMode, bool checkDns, ConcurrentDictionary<string, bool> hostCache)
        {
            _validationMode = validationMode;
            _checkDns = checkDns;
            _hostCache = hostCache;
        }

        protected override bool IsValid(object value)
        {
            if (value is string)
            {
                // Not set is true.

                _validationError = string.IsNullOrEmpty((string)value)
                    ? null
                    : Validate((string)value);
            }
            else
            {
                _validationError = null;
            }

            return _validationError == null;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return _validationError is EmailAddressHostValidationError
                ? new ValidationError[] { new EmailAddressHostValidationError(name) }
                : new[] { new EmailAddressValidationError(name) };
        }

        private ValidationError Validate(string value)
        {
            switch (_validationMode)
            {
                case EmailAddressValidationMode.UserOnly:
                    return ValidateEmailAddressUser(value);

                case EmailAddressValidationMode.SingleEmail:
                    return ValidateEmailAddress(value, false);

                default:
                    return ValidateEmailAddress(value, true);
            }
        }

        private static ValidationError ValidateEmailAddressUser(string value)
        {
            // Try the regex as a first line of defense.

            var regex = RegularExpressions.CompleteEmailUser;
            if (!regex.IsMatch(value))
                return new EmailAddressValidationError("EmailAddress");

            // Check the length.

            if (value.Length > Constants.EmailAddressMaxLength)
                return new EmailAddressValidationError("EmailAddress");
            return null;
        }

        private ValidationError ValidateEmailAddress(string value, bool allowMultiple)
        {
            // Try the regex as a first line of defense.

            var regex = allowMultiple ?
                RegularExpressions.CompleteMultipleEmailAddresses :
                RegularExpressions.CompleteEmailAddress;

            if (!regex.IsMatch(value))
                return new EmailAddressValidationError("EmailAddress");

            // Check the length.

            if (!allowMultiple && (value.Length > Constants.EmailAddressMaxLength))
                return new EmailAddressValidationError("EmailAddress");

            var emailAddresses = allowMultiple ?
                TextUtil.SplitEmailAddresses(value) : new[] { value };

            foreach (string emailAddress in emailAddresses)
            {
                // Case 4697 - construct a MailAddress to be really sure it's valid.

                MailAddress mailAddress;

                try
                {
                    mailAddress = new MailAddress(emailAddress);
                }
                catch (FormatException)
                {
                    return new EmailAddressValidationError("EmailAddress");
                }

                // Check that host name has MX record in DNS.

                if (_checkDns)
                {
                    var isValid = _hostCache.GetOrAdd(mailAddress.Host, GetHostStatus);
                    if (!isValid)
                        return new EmailAddressHostValidationError("EmailAddress");
                }
            }

            return null;
        }

        private static bool GetHostStatus(string domainName)
        {
            if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev && domainName == RuntimeEnvironment.TestEmailDomain)
                return true;

            var exchangeNames = DnsQuery.GetMxNames(domainName);
            return (exchangeNames.Count > 0);
        }
    }

    public static class EmailAddressValidatorFactory
    {
        private static readonly ConcurrentDictionary<string, bool> HostCache = new ConcurrentDictionary<string, bool>();

        public static EmailAddressValidator CreateValidator(EmailAddressValidationMode validationMode, bool checkDns)
        {
            return new EmailAddressValidator(validationMode, checkDns, HostCache);
        }
    }

    public class EmailAddressAttribute
        : ValidationAttribute
    {
        public EmailAddressAttribute()
            : base(EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.SingleEmail, false))
        {
        }

        public EmailAddressAttribute(bool checkDns)
            : base(EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.SingleEmail, checkDns))
        {
        }

        public EmailAddressAttribute(EmailAddressValidationMode validationMode)
            : base(EmailAddressValidatorFactory.CreateValidator(validationMode, false))
        {
        }

        public EmailAddressAttribute(EmailAddressValidationMode validationMode, bool checkDns)
            : base(EmailAddressValidatorFactory.CreateValidator(validationMode, checkDns))
        {
        }
    }
}
