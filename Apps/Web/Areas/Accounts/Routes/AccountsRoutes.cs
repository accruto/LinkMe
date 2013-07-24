using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Areas.Accounts.Controllers;
using LinkMe.Web.Areas.Accounts.Models;

namespace LinkMe.Web.Areas.Accounts.Routes
{
    public static class AccountsRoutes
    {
        public static RouteReference Activation { get; private set; }
        public static RouteReference Verification { get; private set; }
        public static RouteReference NotActivated { get; private set; }
        public static RouteReference NotVerified { get; private set; }
        public static RouteReference ActivationSent { get; private set; }
        public static RouteReference VerificationSent { get; private set; }
        public static RouteReference ChangeEmail { get; private set; }
        public static RouteReference ChangePassword { get; private set; }
        public static RouteReference MustChangePassword { get; private set; }
        public static RouteReference NewPassword { get; private set; }

        public static RouteReference ApiLogIn { get; private set; }
        public static RouteReference ApiLogOut { get; private set; }
        public static RouteReference PreferredUserType { get; private set; }

        public static RouteReference Unsubscribe { get; private set; }

        public static RouteReference Welcome { get; private set; }

        public static RouteReference ApiLinkedInLogin { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Activation = context.MapAreaRoute<ActivationController, string>("accounts/activation", c => c.Activation);
            context.MapRedirectRoute("activation.aspx", Activation);
            context.MapRedirectRoute("ui/unregistered/accountactivationform.aspx", Activation);

            Verification = context.MapAreaRoute<ActivationController, string>("accounts/verification", c => c.Verification);
            NotActivated = context.MapAreaRoute<ActivationController>("accounts/notactivated", c => c.NotActivated);
            NotVerified = context.MapAreaRoute<ActivationController>("accounts/notverified", c => c.NotVerified);
            ActivationSent = context.MapAreaRoute<ActivationController>("accounts/activationsent", c => c.ActivationSent);
            context.MapRedirectRoute("ui/registered/common/activationemailsentform.aspx", NotActivated);

            VerificationSent = context.MapAreaRoute<ActivationController>("accounts/verificationsent", c => c.VerificationSent);
            ChangeEmail = context.MapAreaRoute<ActivationController>("accounts/changeemail", c => c.ChangeEmail);
            ChangePassword = context.MapAreaRoute<PasswordController>("accounts/changepassword", c => c.ChangePassword);
            MustChangePassword = context.MapAreaRoute<PasswordController>("accounts/mustchangepassword", c => c.MustChangePassword);
            NewPassword = context.MapAreaRoute<PasswordController, string>("accounts/newpassword", c => c.NewPassword);

            context.MapRedirectRoute("guests/RequestNewPassword.aspx", NewPassword);
            context.MapRedirectRoute("ui/unregistered/common/RequestNewPassword.aspx", NewPassword);
            context.MapRedirectRoute("ui/registered/common/ChangeEmailForm.aspx", ChangeEmail);

            ApiLogIn = context.MapAreaRoute<LoginApiController, LoginModel>("accounts/api/login", c => c.LogIn);
            ApiLogOut = context.MapAreaRoute<LoginApiController>("accounts/api/logout", c => c.LogOut);
            PreferredUserType = context.MapAreaRoute<AccountsApiController, UserType>("accounts/api/preferredusertype", c => c.PreferredUserType);

            Welcome = context.MapAreaRoute<ActivationController>("accounts/welcome", c => c.Welcome);

            Unsubscribe = context.MapAreaRoute<SettingsController, UnsubscribeRequestModel>("accounts/settings/unsubscribe", c => c.Unsubscribe);
            context.MapRedirectRoute("unsubscribe.aspx", Unsubscribe);

            ApiLinkedInLogin = context.MapAreaRoute<LinkedInApiController, LinkedInApiProfile>("accounts/linkedin/api/login", c => c.LogIn);
        }
    }
}