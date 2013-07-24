using LinkMe.Apps.Api.Areas.Accounts.Controllers;
using LinkMe.Apps.Api.Areas.Accounts.Models;
using LinkMe.Apps.Asp.Routing;
using System.Web.Mvc;

namespace LinkMe.Apps.Api.Areas.Accounts.Routes
{
    public class AccountsRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<AccountsApiController, LoginModel>(1, "login", c => c.LogIn);
            context.MapAreaRoute<AccountsApiController>(1, "logout", c => c.LogOut);
            context.MapAreaRoute<AccountsApiController, ChangePasswordModel>(1, "accounts/changepassword", c => c.ChangePassword);
            context.MapAreaRoute<AccountsApiController, string>(1, "accounts/newpassword", c => c.NewPassword);
            context.MapAreaRoute<AccountsApiController, string>(1, "registerDevice", c => c.RegisterDevice);
            context.MapAreaRoute<AccountsApiController>(1, "unviewedSummary", c => c.UnviewedAlertedCandidates);
        }
    }
}