using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.Domain.Users.Administrators
{
    public static class AdministratorsHtmlExtensions
    {
        public static string AccountManagerFullName(this HtmlHelper html, IList<Administrator> accountManagers, Guid? accountManagerId)
        {
            if (accountManagerId == null)
                return string.Empty;

            var accountManager = (from a in accountManagers
            where a.Id == accountManagerId.Value
            select a).SingleOrDefault();

            return html.AccountManagerFullName(accountManager);
        }

        public static string AccountManagerFullName(this HtmlHelper html, Administrator accountManager)
        {
            return accountManager == null
                ? string.Empty
                : html.Encode(accountManager.FullName);
        }
    }
}