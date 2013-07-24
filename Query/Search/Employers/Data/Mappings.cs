using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Users.Employers.Data;

namespace LinkMe.Query.Search.Employers.Data
{
    internal enum UserFlags
    {
        None = 0x00,
        MustChangePassword = 0x02,
        Disabled = 0x04,
        EmailBounced = 0x08,
        Activated = 0x20
    }

    internal partial class EmployerIndustryEntity
        : IEmployerIndustryEntity
    {
    }

    internal partial class EmployerEntity
        : IEmployerEntity, IHavePhoneNumberEntity
    {
        IRegisteredUserEntity IEmployerEntity.RegisteredUserEntity
        {
            get { return RegisteredUserEntity; }
        }

        IEnumerable<IEmployerIndustryEntity> IEmployerEntity.EmployerIndustryEntities
        {
            get { return EmployerIndustryEntities.Cast<IEmployerIndustryEntity>(); }
        }

        string IHavePhoneNumberEntity.phoneNumber
        {
            get { return contactPhoneNumber; }
            set { contactPhoneNumber = value; }
        }

        public byte? phoneNumberType
        {
            get { return (byte?)PhoneNumberType.Work; }
            set { }
        }
    }

    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity, IHaveEmailAddressEntity
    {
    }

    internal partial class CommunityAssociationEntity
        : ICommunityAssociationEntity
    {
    }
}
