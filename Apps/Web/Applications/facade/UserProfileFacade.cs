using System;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Configuration;
using LinkMe.Web.Areas.Accounts.Routes;

namespace LinkMe.Web.Applications.Facade
{
	public static class UserProfileFacade
	{
        private static ReadOnlyUrl ActivationUrl { get { return AccountsRoutes.Activation.GenerateUrl(); } }
        private static readonly IEmailVerificationsQuery EmailVerificationQuery = Container.Current.Resolve<IEmailVerificationsQuery>();

        public static string GetDeveloperActivationLink(IMember member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            if (!ApplicationContext.Instance.GetBoolProperty(ApplicationContext.ENABLE_DEVELOPER_SHORTCUTS))
                return "";

            var activation = EmailVerificationQuery.GetEmailVerification(member.Id, member.GetBestEmailAddress().Address);
            if (activation == null)
                return "";

            var url = ActivationUrl.AsNonReadOnly();
            url.QueryString["activationCode"] = activation.VerificationCode;

            return "<div class='debug-note'><strong>DEV ONLY:</strong> <a id=\"dodgyActivateLink\" href=\"" +
                url + "\">Activate this user</a></div>";
        }
	}
}
