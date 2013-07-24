using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Public.Controllers.Join
{
    [ApiEnsureNotAuthorized]
    public class JoinApiController
        : ApiController
    {
        private readonly IAccountsManager _accountsManager;

        public JoinApiController(IAccountsManager accountsManager)
        {
            _accountsManager = accountsManager;
        }

        [HttpPost]
        public ActionResult Join(MemberJoin joinModel, bool acceptTerms)
        {
            try
            {
                joinModel = joinModel ?? new MemberJoin();

                // Process the post to check validations.

                if (acceptTerms)
                {
                    // Try to join.

                    var account = new MemberAccount
                    {
                        FirstName = joinModel.FirstName,
                        LastName = joinModel.LastName,
                        EmailAddress = joinModel.EmailAddress,
                    };

                    var credentials = new AccountLoginCredentials
                    {
                        LoginId = joinModel.EmailAddress,
                        Password = joinModel.JoinPassword,
                        ConfirmPassword = joinModel.JoinConfirmPassword,
                    };

                    _accountsManager.Join(HttpContext, account, credentials, true);
                }
                else
                {
                    ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, new StandardErrorHandler());

                    // Not accepting terms so cannot proceed but also check whether any other fields fail validation.

                    joinModel.Prepare();
                    joinModel.Validate();
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}
