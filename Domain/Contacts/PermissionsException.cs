using System;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Contacts
{
    public abstract class PermissionsException
        : UserException
    {
        private readonly Guid? _userId;

        protected PermissionsException(IHasId<Guid> user)
        {
            _userId = user == null ? (Guid?)null : user.Id;
        }

        protected PermissionsException(Guid? userId)
        {
            _userId = userId;
        }

        public Guid? UserId
        {
            get { return _userId; }
        }
    }
}
