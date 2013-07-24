using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class ApiEnsureAuthorizedAttribute
        : AuthorizedAttribute
    {
        private readonly IAccountsManager _accountsManager = Container.Current.Resolve<IAccountsManager>();
        private static readonly UserType[] AllUserTypes = new[] { UserType.Administrator, UserType.Member, UserType.Employer, UserType.Custodian };
        private UserType[] _userTypes = AllUserTypes;
        private bool AttemptAutoLogin { get; set; }

        public UserType[] UserTypes
        {
            get { return _userTypes; }
            set { _userTypes = (value == null || value.Length == 0) ? AllUserTypes : value; }
        }

        public ApiEnsureAuthorizedAttribute()
            : this(null, false)
        {
        }

        public ApiEnsureAuthorizedAttribute(UserType userType)
            : this(userType, false)
        {
        }

        public ApiEnsureAuthorizedAttribute(UserType userType, bool attemptAutoLogin)
        {
            _userTypes = AllUserTypes.Contains(userType) ? new[] { userType } : AllUserTypes;

            AttemptAutoLogin = attemptAutoLogin;
        }

        private ApiEnsureAuthorizedAttribute(UserType? userType, bool attemptAutoLogin)
        {
            _userTypes = userType == null || !AllUserTypes.Contains(userType.Value)
                ? AllUserTypes
                : new[] { userType.Value };

            AttemptAutoLogin = attemptAutoLogin;
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check that the current user matches the role.

            var user = filterContext.Controller.ViewData.GetCurrentRegisteredUser();

            if (user == null && AttemptAutoLogin)
            {
                var result = TryAutoLogin(filterContext.HttpContext);

                if (result.Status == AuthenticationStatus.AuthenticatedAutomatically)
                {
                    user = result.User as RegisteredUser;
                    filterContext.Controller.ViewData.SetCurrentRegisteredUser(user);
                }
            }

            if (user == null)
            {
                SetUnauthorized(filterContext);
            }
            else
            {
                if (!_userTypes.Contains(user.UserType))
                    SetUnauthorized(filterContext);
            }
        }

        private static void SetUnauthorized(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.SkipAuthorization = true;

            var exception = new UnauthorizedException();
            var model = new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError>
                {
                    new JsonError
                    {
                        Code = ((IErrorHandler)new StandardErrorHandler()).GetErrorCode(exception),
                        Message = ((IErrorHandler)new StandardErrorHandler()).FormatErrorMessage(exception),
                    },
                }
            };

            filterContext.Result = new JsonResponseResult(model, JsonRequestBehavior.AllowGet, null);
        }

        private AuthenticationResult TryAutoLogin(HttpContextBase context)
        {
            // They may be automatically logged in because they have previously selected "remember me".

            return _accountsManager.TryAutoLogIn(context);
        }
    }
}
