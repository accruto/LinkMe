using System;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Domain.Roles.Affiliations.Verticals
{
    public static class VerticalsExtensions
    {
        public static ReadOnlyUrl GetDeletedRedirectUrl(this Vertical vertical, ReadOnlyUrl url, string nonVerticalHost)
        {
            // Check whether it is deleted.

            if (!vertical.IsDeleted)
                return null;

            var redirectUrl = url.AsNonReadOnly();
            redirectUrl.Host = nonVerticalHost;
            return redirectUrl;
        }

        public static ReadOnlyUrl GetAlternativeHostRedirectUrl(this Vertical vertical, ReadOnlyUrl url)
        {
            // If the url corresponds to a vertical's alternative hosts then redirect to the primary host.

            var redirectUrl = GetAlternativeHostRedirectUrl(vertical, url, v => v.SecondaryHost);
            return redirectUrl ?? GetAlternativeHostRedirectUrl(vertical, url, v => v.TertiaryHost);
        }

        private static ReadOnlyUrl GetAlternativeHostRedirectUrl(Vertical vertical, ReadOnlyUrl url, Func<Vertical, string> getHost)
        {
            var host = getHost(vertical);
            if (host != null)
            {
                // Redirect if not consistent with the host but consistent with the secondary host.

                if (!url.Host.EndsWith(vertical.Host) && url.Host.EndsWith(host))
                {
                    var redirectUrl = url.AsNonReadOnly();
                    redirectUrl.Host = redirectUrl.Host.Replace(host, vertical.Host);
                    return redirectUrl;
                }
            }

            return null;
        }
    }
}
