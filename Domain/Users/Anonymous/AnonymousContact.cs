using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Anonymous
{
    public class AnonymousContact
        : IUser, ICommunicationUser
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }

        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName.CombineLastName(LastName); }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return null; }
        }

        UserType ICommunicationUser.UserType
        {
            get { return UserType.Anonymous; }
        }

        bool ICommunicationUser.IsEnabled
        {
            get { return true; }
        }

        bool ICommunicationUser.IsActivated
        {
            get { return true; }
        }

        bool ICommunicationUser.IsEmailAddressVerified
        {
            get { return true; }
        }
    }
}
