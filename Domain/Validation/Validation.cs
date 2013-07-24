using System;
using System.Text.RegularExpressions;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Validation
{
    public class FirstNameAttribute
        : RegexAttribute
    {
        public FirstNameAttribute()
            : base(RegularExpressions.CompleteFirstName, Constants.NameMinLength, Constants.NameMaxLength)
        {
        }
    }

    public class LastNameAttribute
        : RegexAttribute
    {
        public LastNameAttribute()
            : base(RegularExpressions.CompleteLastName, Constants.NameMinLength, Constants.NameMaxLength)
        {
        }
    }

    public class PhoneNumberAttribute
        : RegexAttribute
    {
        public PhoneNumberAttribute()
            : base(RegularExpressions.CompletePhoneNumber, Constants.PhoneNumberMinLength, Constants.PhoneNumberMaxLength)
        {
        }
    }

    public class PhoneNumberValidator
        : RegexValidator
    {
        public PhoneNumberValidator()
            : base(RegularExpressions.CompletePhoneNumber, Constants.PhoneNumberMinLength, Constants.PhoneNumberMaxLength)
        {
        }
    }

    [Serializable]
    public class PostalSuburbValidationError
        : ValidationError
    {
        public PostalSuburbValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class PostalSuburbValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            // In general data is entered as a country / location pair.
            // This attribute only checks the location aspect of that pair if
            // it has been entered, so if a country is set, but no location is entered
            // then this attribute will consider that valid - that is the address.Location.IsCountry
            // check here. If something else needs to be done then another attribute should be created.

            LocationReference location;
            if (value is Address)
            {
                var address = value as Address;
                location = address.Location;
            }
            else if (value is LocationReference)
            {
                location = value as LocationReference;
            }
            else
            {
                return true;
            }

            if (location == null || location.IsCountry)
                return true;

            // Only do a check if the country can actually resolve locations.
            // For those countries that cannot resolve this would always be invalid,
            // so we need to allow anything through in these cases.

            if (location.Country != null && !location.Country.CanResolveLocations)
                return true;

            // This will be set if and only if the location is fully resolved to a postal suburb.

            return location.PostalSuburb != null;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new PostalSuburbValidationError(name) };
        }
    }

    public class PostalSuburbAttribute
        : ValidationAttribute
    {
        public PostalSuburbAttribute()
            : base(new PostalSuburbValidator())
        {
        }
    }
}