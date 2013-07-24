using System;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Users.Members
{
    public class FirstNameChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public FirstNameChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class LastNameChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public LastNameChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class EmailAddressChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public EmailAddressChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class HomePhoneNumberChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public HomePhoneNumberChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class MobilePhoneNumberChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public MobilePhoneNumberChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class WorkPhoneNumberChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public WorkPhoneNumberChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class ProfilePhotoChangedEventArgs
        : PropertyChangedEventArgs
    {
        public readonly Guid MemberId;
        public readonly Guid? PhotoId;

        public ProfilePhotoChangedEventArgs(Guid memberId, Guid? photoId)
        {
            MemberId = memberId;
            PhotoId = photoId;
        }
    }

    public class LocationChangedEventArgs
        : PropertyChangedEventArgs<LocationReference>
    {
        public LocationChangedEventArgs(LocationReference from, LocationReference to)
            : base(from, to)
        {
        }
    }
}
