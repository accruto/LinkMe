using System.Web;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Context
{
    internal class HttpContextLocationContext
        : ILocationContext
    {
        private readonly Country _defaultCountry;

        public HttpContextLocationContext(Country defaultCountry)
        {
            _defaultCountry = defaultCountry;
        }

        void ILocationContext.Reset()
        {
            HttpContext.Current.Items["Country"] = null;
        }

        Country ILocationContext.Country
        {
            get
            {
                var country = HttpContext.Current.Items["Country"] as Country;
                return country ?? _defaultCountry;
            }
            set
            {
                HttpContext.Current.Items["Country"] = value;
            }
        }
    }
}
