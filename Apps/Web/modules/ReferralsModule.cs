using System.Web;
using LinkMe.Apps.Asp.Modules;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Referrals;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Modules
{
    public class ReferralsModule
        : HttpModule
    {
        private readonly IReferralsManager _referralsManager = Container.Current.Resolve<IReferralsManager>();

        protected override void OnBeginRequest()
        {
            var request = HttpContext.Current.Request;

            // Avoid error messages caused by VS.NET's request for a non-existent file.

            if (request.HttpMethod == "GET")
            {
                var redirectUrl = _referralsManager.TrackReferral(HttpContext.Current);
                if (redirectUrl != null)
                    NavigationManager.RedirectPermanently(redirectUrl);
            }
        }
    }
}