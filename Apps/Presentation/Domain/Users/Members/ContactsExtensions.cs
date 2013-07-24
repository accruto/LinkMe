using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Apps.Presentation.Domain.Users.Members
{
    public static class ContactsExtensions
    {
        public const string NameHiddenText = "Name hidden";
        public const string EmailHiddenText = "Email hidden";

        public static string GetFullNameDisplayText(this PersonalView view)
        {
            return view.FullName ?? "[" + NameHiddenText + "]";
        }

        public static string GetFirstNameDisplayText(this PersonalView view)
        {
            return view.FirstName ?? "[" + NameHiddenText + "]";
        }

        public static string GetEmailAddressDisplayText(this PersonalView view)
        {
            var emailAddress = view.GetBestEmailAddress();
            return emailAddress == null || emailAddress.Address == null ? "[" + EmailHiddenText + "]" : emailAddress.Address;
        }

        public static string GetFullNameDisplayText(this ProfessionalView view)
        {
            return view.FullName ?? "Anonymous member";
        }
    }
}