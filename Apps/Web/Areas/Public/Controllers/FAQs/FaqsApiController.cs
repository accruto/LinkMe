using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Members.Controllers;

namespace LinkMe.Web.Areas.Public.Controllers.Faqs
{
    public class FaqsApiController
        : MembersApiController
    {
        private readonly IFaqsCommand _faqsCommand;

        public FaqsApiController(IFaqsCommand faqsCommand)
        {
            _faqsCommand = faqsCommand;
        }

        [HttpPost]
        public ActionResult MarkFaqHelpful(Guid id)
        {
            try
            {
                _faqsCommand.MarkHelpful(id);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult MarkFaqNotHelpful(Guid id)
        {
            try
            {
                _faqsCommand.MarkNotHelpful(id);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}