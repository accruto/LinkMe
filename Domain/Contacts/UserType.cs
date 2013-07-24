using System;

namespace LinkMe.Domain.Contacts
{
    [Flags]
    public enum UserType
    {
        Anonymous = 0x0000,
        Administrator = 0x0001,
        Employer = 0x0002,
        Member = 0x0004,
        Custodian = 0x0010,
    }
}