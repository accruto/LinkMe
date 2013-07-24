using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.Domain.Users.Members
{
    public static class FriendsHtmlExtensions
    {
        private static readonly string[] Digits = new[] { "no", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        public static string GetFriendsCountDisplayText(this int count)
        {
            var ns = count < 10 ? Digits[count] : count.ToString();
            var subject = count == 1 ? "friend" : "friends";
            return "You have " + ns + " " + subject + ".";
        }

        public static string GetFriendsCountDisplayText(this int count, IMember member)
        {
            var ns = count < 10 ? Digits[count] : count.ToString();
            var subject = count == 1 ? "friend" : "friends";
            return HtmlUtil.TextToHtml(member.FirstName) + " has " + ns + " " + subject + ".";
        }

        public static string GetRepresenteesCountDisplayText(this int count)
        {
            var ns = count < 10 ? Digits[count] : count.ToString();
            var subject = count == 1 ? "person" : "people";
            return "You are representing " + ns + " " + subject + ".";
        }
    }
}
