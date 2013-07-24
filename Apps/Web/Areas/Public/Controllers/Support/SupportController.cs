using System.Web.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Web.Areas.Public.Models.Support;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Public.Controllers.Support
{
    public class SupportController
        : PublicController
    {
        private readonly IFaqsQuery _faqsQuery;

        private MemberContext _memberContext;
        private EmployerContext _employerContext;

        protected MemberContext MemberContext
        {
            get { return _memberContext ?? (_memberContext = new MemberContext(HttpContext)); }
        }

        protected EmployerContext EmployerContext
        {
            get { return _employerContext ?? (_employerContext = new EmployerContext(HttpContext)); }
        }

        public SupportController(IFaqsQuery faqsQuery)
        {
            _faqsQuery = faqsQuery;
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Careers()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            // Get the url for the first FAQ page and redirect to there

            var category = _faqsQuery.GetCategory(UserType.Member);
            return RedirectToUrl(category.Subcategories[0].GenerateUrl());
        }

        public ActionResult ContactUsPartial()
        {
            return PartialView(GetContactUsModel());
        }

        private ContactUsModel GetContactUsModel()
        {
            // Use the current user's email address if possible.

            var emailUs = new EmailUsModel { UserType = UserType.Member };

            var user = CurrentRegisteredUser;
            if (user != null)
            {
                emailUs.From = ((ICommunicationRecipient) user).EmailAddress;
                emailUs.Name = user.FullName;

                var member = user as Member;
                if (member != null)
                {
                    emailUs.PhoneNumber = member.GetPrimaryPhoneNumber() == null ? string.Empty : member.GetPrimaryPhoneNumber().Number;
                    emailUs.UserType = UserType.Member;
                }
                else
                {
                    var employer = user as Employer;
                    if (employer != null)
                    {
                        emailUs.PhoneNumber = employer.PhoneNumber == null ? string.Empty : employer.PhoneNumber.Number;
                        emailUs.UserType = UserType.Employer;
                    }
                }
            }

            return new ContactUsModel
            {
                EmailUs = emailUs,
                UserTypes = new[] { UserType.Member, UserType.Employer },
                MemberSubCategories = _faqsQuery.GetCategory(UserType.Member).Subcategories,
                EmployerSubCategories = _faqsQuery.GetCategory(UserType.Employer).Subcategories,
            };
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult PrivacyDetail()
        {
            return View();
        }

        public ActionResult Terms()
        {
            if (CurrentMember != null)
                MemberContext.HideUpdatedTermsReminder();
            else if (CurrentEmployer != null)
                EmployerContext.HideUpdatedTermsReminder();
            return View();
        }

        public ActionResult TermsDetail()
        {
            if (CurrentMember != null)
                MemberContext.HideUpdatedTermsReminder();
            else if (CurrentEmployer != null)
                EmployerContext.HideUpdatedTermsReminder();
            return View();
        }

        public ActionResult MemberTerms()
        {
            if (CurrentMember != null)
                MemberContext.HideUpdatedTermsReminder();
            return View();
        }

        public ActionResult MemberTermsDetail()
        {
            if (CurrentMember != null)
                MemberContext.HideUpdatedTermsReminder();
            return View();
        }

        public ActionResult EmployerTerms()
        {
            if (CurrentEmployer != null)
                EmployerContext.HideUpdatedTermsReminder();
            return View();
        }

        public ActionResult EmployerTermsDetail()
        {
            if (CurrentEmployer != null)
                EmployerContext.HideUpdatedTermsReminder();
            return View();
        }
    }
}
