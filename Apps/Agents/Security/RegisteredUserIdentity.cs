using System;
using System.Security.Principal;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Security
{
    public class RegisteredUserIdentity
        : GenericIdentity
    {
        private readonly Guid _id;
        private readonly UserType _userType;
        private readonly bool _isActivated;

        public RegisteredUserIdentity(Guid id, UserType userType, bool isActivated)
            : base(id.ToString("n"))
        {
            _id = id;
            _userType = userType;
            _isActivated = isActivated;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public UserType UserType
        {
            get { return _userType; }
        }

        public bool IsActivated
        {
            get { return _isActivated; }
        }

        public override bool IsAuthenticated
        {
            get { return true; }
        }

        public string FullName { get; set; }
        public bool NeedsReset { get; set; }
        public IUser User { get; set; }
    }
}