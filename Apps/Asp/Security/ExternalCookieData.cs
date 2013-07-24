using System;
using System.Web;

namespace LinkMe.Apps.Asp.Security
{
    public class ExternalCookieData
    {
        private readonly string _externalId;
        private readonly string _emailAddress;
        private readonly string _name;

        public ExternalCookieData(string externalId, string emailAddress, string name)
        {
            _externalId = externalId;
            _emailAddress = emailAddress;
            _name = name;
        }

        public string ExternalId
        {
            get { return _externalId; }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string CookieValue
        {
            get { return HttpUtility.UrlEncode(ExternalId + "|" + EmailAddress + "|" + Name); }
        }

        public static ExternalUserData ParseCookieValue(string cookieValue)
        {
            var parts = HttpUtility.UrlDecode(cookieValue).Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3)
                return null;

            var userData = new ExternalUserData
            {
                ExternalId = parts[0],
                EmailAddress = parts[1],
            };

            var name = parts[2];
            var pos = name.IndexOf(' ');
            userData.FirstName = pos == -1 ? string.Empty : name.Substring(0, pos).Trim();
            userData.LastName = pos == -1 ? name.Trim() : name.Substring(pos + 1).Trim();
            return userData;
        }
    }
}
