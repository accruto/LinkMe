using System;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Views.Shared
{
    public class Pager
        : ViewUserControl<PaginatedList>
    {
        private ReadOnlyUrl _baseUrl;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // This pager makes the following assumptions:
            // - The url can be of the forms:
            //     /x/y/z
            //     /x/y/z/
            //     /x/y/z/n
            //     /x/y/z/n/
            //   where n is some number.
            // - If n is not present then n = 1.
            // - In all cases the base url will be: /x/y/z/

            var clientUrl = ClientUrl;
            var path = clientUrl.Path;
            Url nonReadOnlyUrl = null;
            ReadOnlyUrl url;

            if (!path.EndsWith("/"))
            {
                nonReadOnlyUrl = clientUrl.AsNonReadOnly();
                nonReadOnlyUrl.Path += "/";
                url = nonReadOnlyUrl;
            }
            else
            {
                url = clientUrl;
            }

            // Check to see whether the last segment is a number.

            var segments = url.Path.Split(new [] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length > 0)
            {
                int page;
                if (int.TryParse(segments[segments.Length - 1], out page))
                {
                    if (nonReadOnlyUrl == null)
                    {
                        nonReadOnlyUrl = url.AsNonReadOnly();
                        url = nonReadOnlyUrl;
                    }

                    nonReadOnlyUrl.Path = "/" + string.Join("/", segments, 0, segments.Length - 1) + "/";
                }
            }

            _baseUrl = url;

        }

        protected ReadOnlyUrl GetUrl(int page)
        {
            return new ReadOnlyApplicationUrl(_baseUrl, page.ToString());
        }
    }
}
