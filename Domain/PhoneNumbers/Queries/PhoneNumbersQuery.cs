using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Domain.PhoneNumbers.Queries
{
    public class PhoneNumbersQuery
        : IPhoneNumbersQuery
    {
        private readonly ILocationQuery _locationQuery;
        private readonly int _australiaId;

        public PhoneNumbersQuery(ILocationQuery locationQuery)
        {
            _locationQuery = locationQuery;
            _australiaId = _locationQuery.GetCountry("Australia").Id;
        }

        PhoneNumberType IPhoneNumbersQuery.GetPhoneNumberType(string number, Country country)
        {
            return GetPhoneNumberType(number, country);
        }

        PhoneNumber IPhoneNumbersQuery.GetPhoneNumber(string number, Country country)
        {
            return string.IsNullOrEmpty(number)
                       ? null
                       : new PhoneNumber {Number = number, Type = GetPhoneNumberType(number, country)};
        }

        private PhoneNumberType GetPhoneNumberType(string number, Country country)
        {
            return country.Id == _australiaId
                       ? GetAustralianPhoneNumberType(number.Trim())
                       : PhoneNumberType.Work;
        }

        private static PhoneNumberType GetAustralianPhoneNumberType(string number)
        {
            // If the first digits are 04 it is mobile.

            if (number.Length >= 2 && number.StartsWith("04"))
                return PhoneNumberType.Mobile;
            return PhoneNumberType.Work;
        }
    }
}