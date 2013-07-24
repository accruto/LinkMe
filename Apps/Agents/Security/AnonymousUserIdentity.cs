using System;
using System.Security.Principal;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Security
{
    public class AnonymousUserIdentity
        : GenericIdentity
    {
        private readonly Guid _id;

        public AnonymousUserIdentity(Guid id)
            : base(id.ToString("n"))
        {
            _id = id;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public UserType PreferredUserType { get; set; }

        public override bool IsAuthenticated
        {
            get { return false; }
        }
    }
}