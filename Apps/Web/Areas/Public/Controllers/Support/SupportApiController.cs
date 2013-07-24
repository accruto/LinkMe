using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Public.Models.Support;
using LinkMe.Apps.Asp.Mvc.Models;

namespace LinkMe.Web.Areas.Public.Controllers.Support
{
    public class SupportApiController
        : ApiController
    {
        private readonly IFaqsQuery _faqsQuery;
        private readonly IEmailsCommand _emailsCommand;

        public SupportApiController(IFaqsQuery faqsQuery, IEmailsCommand emailsCommand)
        {
            _faqsQuery = faqsQuery;
            _emailsCommand = emailsCommand;
        }

        [HttpPost]
        public ActionResult SendContactUs(EmailUsModel emailUs)
        {
            try
            {
                emailUs.Prepare();
                emailUs.Validate();

                var subcategory = emailUs.SubcategoryId == null
                    ? null
                    : _faqsQuery.GetSubcategory(emailUs.SubcategoryId.Value);

                _emailsCommand.TrySend(new ContactUsEmail(emailUs.From, emailUs.Name, emailUs.UserType, emailUs.PhoneNumber, subcategory, emailUs.Message));

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}
