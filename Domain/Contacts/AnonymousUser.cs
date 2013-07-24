using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Contacts
{
    public class AnonymousUser
        : IAnonymousUser
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
    }
}
