using System.Collections.Generic;

namespace LinkMe.Domain.Contacts
{
    public interface IHavePhoneNumber
    {
        PhoneNumber PhoneNumber { get; set; }
    }

    public interface IHavePhoneNumbers
    {
        IList<PhoneNumber> PhoneNumbers { get; set; }
    }
}
