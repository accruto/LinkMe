using System;
using System.Web;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Referrals
{
    public interface IReferralsManager
    {
        ReadOnlyUrl TrackReferral(HttpContext context);
        void CreateReferral(HttpRequest request, Guid userId);
        void CreateReferral(HttpRequestBase request, Guid userId);
    }
}
