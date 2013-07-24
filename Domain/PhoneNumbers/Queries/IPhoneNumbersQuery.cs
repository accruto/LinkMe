using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;

namespace LinkMe.Domain.PhoneNumbers.Queries
{
    public interface IPhoneNumbersQuery
    {
        PhoneNumberType GetPhoneNumberType(string number, Country country);
        PhoneNumber GetPhoneNumber(string number, Country country);
    }
}