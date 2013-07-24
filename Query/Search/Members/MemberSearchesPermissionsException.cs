using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Search.Members
{
    public class MemberSearchesPermissionsException
        : PermissionsException
    {
        private readonly Guid _searchId;

        public MemberSearchesPermissionsException(IUser owner, Guid searchId)
            : base(owner)
        {
            _searchId = searchId;
        }

        public Guid SearchId
        {
            get { return _searchId; }
        }
    }
}