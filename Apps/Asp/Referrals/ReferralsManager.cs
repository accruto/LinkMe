using System;
using System.Web;
using LinkMe.Apps.Asp.Cookies;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Apps.Asp.Referrals
{
    public class ReferralsManager
        : IReferralsManager
    {
        private readonly IReferralsCommand _referralsCommand;

        public ReferralsManager(IReferralsCommand referralsCommand)
        {
            _referralsCommand = referralsCommand;
        }

        // If needed this could all be made a bit more generic and configurable but will do for now.

        private const string ReferrerCodeParameter = "ref";
        private const string CidParameter = "cid";

        public const string ReferrerCodeCookie = "LM.Ref.ReferrerCode";
        public const string PromoCodeCookie = "LM.Ref.PromoCode";
        public const string ReferrerUrlCookie = "LM.Ref.ReferrerUrl";

        private static readonly TimeSpan Expires = new TimeSpan(30, 0, 0, 0);

        ReadOnlyUrl IReferralsManager.TrackReferral(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // Track the referrer if it's external to the site and it is not already saved.

            var referrer = context.Request.UrlReferrer;
            if (referrer != null
                && string.IsNullOrEmpty(context.Request.Cookies.GetCookieValue(ReferrerUrlCookie))
                && !NavigationManager.IsInternalUrl(referrer))
            {
                SetCookie(context.Response, ReferrerUrlCookie, referrer.AbsoluteUri);
            }

            // Check for codes.

            var url = TrackCode(context, Constants.PromoCodeParameter, PromoCodeCookie);
            if (url != null)
                return url;

            url = TrackCode(context, ReferrerCodeParameter, ReferrerCodeCookie);
            if (url != null)
                return url;

            // Check for a cid code.  Not sure if they are supposed to be used anywhere but lots of external pages link to us using them.
            // Do a redirect to try to claim some more PageRank.

            url = RemoveCode(context, CidParameter);
            if (url != null)
                return url;

            return null;
        }

        void IReferralsManager.CreateReferral(HttpRequest request, Guid userId)
        {
            CreateReferral(request.Cookies, userId);
        }

        void IReferralsManager.CreateReferral(HttpRequestBase request, Guid userId)
        {
            CreateReferral(request.Cookies, userId);
        }

        private void CreateReferral(HttpCookieCollection cookies, Guid userId)
        {
            var pcode = cookies.GetCookieValue(PromoCodeCookie);
            var refcode = cookies.GetCookieValue(ReferrerCodeCookie);
            var refererUrl = cookies.GetCookieValue(ReferrerUrlCookie);

            if (string.IsNullOrEmpty(pcode) && string.IsNullOrEmpty(refcode) && string.IsNullOrEmpty(refererUrl))
                return;

            var referral = new AffiliationReferral { RefereeId = userId, PromotionCode = pcode, ReferralCode = refcode, RefererUrl = refererUrl };
            _referralsCommand.CreateAffiliationReferral(referral);
        }

        private static ReadOnlyUrl TrackCode(HttpContext context, string requestParameter, string cookieName)
        {
            var code = context.Request.QueryString[requestParameter];
            if (!string.IsNullOrEmpty(code))
            {
                SetCookie(context.Response, cookieName, code);
                var url = context.GetClientUrl().AsNonReadOnly();
                url.QueryString.Remove(requestParameter);

                // One integrator was using query string parameters incorrectly, simply appending &pcode=xxx.  Look for it explicitly and remove.

                var end = "&" + requestParameter + "=" + code;
                if (url.Path.EndsWith(end))
                    url.Path = url.Path.Substring(0, url.Path.Length - end.Length);

                return url;
            }

            return null;
        }

        private static ReadOnlyUrl RemoveCode(HttpContext context, string requestParameter)
        {
            var cid = context.Request.QueryString[requestParameter];
            if (!string.IsNullOrEmpty(cid))
            {
                var url = context.GetClientUrl().AsNonReadOnly();
                url.QueryString.Remove(requestParameter);
                return url;
            }

            return null;
        }

        private static void SetCookie(HttpResponse response, string key, string value)
        {
            response.Cookies.SetCookie(key, value, Expires);
        }
    }
}