using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public class EmailAddress
    {
        public string Address { get; set; }
        public bool IsVerified { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (EmailAddress))
                return false;
            return Equals((EmailAddress) obj);
        }

        public bool Equals(EmailAddress other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.Address, Address)
                && other.IsVerified.Equals(IsVerified);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Address != null ? Address.GetHashCode() : 0)*397) ^ IsVerified.GetHashCode();
            }
        }
    }

    public class EmailAddressesValidator
        : Validator
    {
        private ValidationError[] _validationErrors;

        protected override bool IsValid(object value)
        {
            _validationErrors = new ValidationError[0];
            if (value is IList<EmailAddress>)
            {
                foreach (var emailAddress in (IList<EmailAddress>) value)
                {
                    var validator = (IValidator)EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.SingleEmail, false);
                    if (!validator.IsValid(emailAddress.Address))
                        _validationErrors = _validationErrors.Concat(validator.GetValidationErrors(null)).ToArray();
                }
            }

            return _validationErrors.Length == 0;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return (from e in _validationErrors
                    select e is EmailAddressHostValidationError ? (ValidationError) new EmailAddressHostValidationError(name) : new EmailAddressValidationError(name)).ToArray();
        }
    }

    public class EmailAddressesAttribute
        : ValidationAttribute
    {
        public EmailAddressesAttribute()
            : base(new EmailAddressesValidator())
        {
        }
    }
}
