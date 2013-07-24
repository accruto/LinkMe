using LinkMe.Apps.Asp.Security;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Public.Models.Logins
{
    public enum LoginReason
    {
        AddJobAd,
        Apply,
        SaveSearch
    }

    public class LoginModel
    {
        public Login Login { get; set; }
        public LoginReason? Reason { get; set; }
        public MemberJoin Join { get; set; }
        public bool AcceptTerms { get; set; }
    }
}