using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Files;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Anonymous.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Logos.Commands;
using LinkMe.Domain.Users.Employers.Orders.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Employers.Controllers.Products;
using LinkMe.Web.Areas.Employers.Models.Accounts;
using LinkMe.Web.Areas.Employers.Models.JobAds;
using LinkMe.Web.Areas.Employers.Models.Products;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Controllers;
using LinkMe.Web.Domain.Roles.JobAds;
using LinkMe.Web.Models.Accounts;
using LinkMe.Web.UI.Registered.Employers;

namespace LinkMe.Web.Areas.Employers.Controllers.JobAds
{
    [EnsureHttps]
    public class JobAdController
        : EmployersController
    {
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand;
        private readonly IAnonymousJobAdsCommand _anonymousJobAdsCommand;
        private readonly IEmployerLogosCommand _employerLogosCommand;
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobPostersQuery _jobPostersQuery;
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IProductsQuery _productsQuery;
        private readonly ICouponsQuery _couponsQuery;
        private readonly IOrdersCommand _ordersCommand;
        private readonly IOrdersQuery _ordersQuery;
        private readonly IEmployerAllocationsCommand _employerAllocationsCommand;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IEmployerOrdersCommand _employerOrdersCommand;
        private readonly IEmployerOrdersQuery _employerOrdersQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly IAccountsManager _accountsManager;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ILoginAuthenticationCommand _loginAuthenticationCommand;
        private readonly IAuthenticationManager _authenticationManager;

        public JobAdController(IEmployerJobAdsCommand employerJobAdsCommand, IAnonymousJobAdsCommand anonymousJobAdsCommand, IEmployerLogosCommand employerLogosCommand, IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobPostersQuery jobPostersQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, ICreditsQuery creditsQuery, IProductsQuery productsQuery, ICouponsQuery couponsQuery, IOrdersCommand ordersCommand, IOrdersQuery ordersQuery, IEmployerAllocationsCommand employerAllocationsCommand, IEmployerCreditsQuery employerCreditsQuery, IEmployerOrdersCommand employerOrdersCommand, IEmployerOrdersQuery employerOrdersQuery, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, ILoginAuthenticationCommand loginAuthenticationCommand, IAuthenticationManager authenticationManager)
        {
            _employerJobAdsCommand = employerJobAdsCommand;
            _anonymousJobAdsCommand = anonymousJobAdsCommand;
            _employerLogosCommand = employerLogosCommand;
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobPostersQuery = jobPostersQuery;
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _creditsQuery = creditsQuery;
            _productsQuery = productsQuery;
            _couponsQuery = couponsQuery;
            _ordersCommand = ordersCommand;
            _ordersQuery = ordersQuery;
            _employerAllocationsCommand = employerAllocationsCommand;
            _employerCreditsQuery = employerCreditsQuery;
            _employerOrdersCommand = employerOrdersCommand;
            _employerOrdersQuery = employerOrdersQuery;
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _accountsManager = accountsManager;
            _loginCredentialsQuery = loginCredentialsQuery;
            _loginAuthenticationCommand = loginAuthenticationCommand;
            _authenticationManager = authenticationManager;
        }

        public ActionResult JobAd(Guid? jobAdId)
        {
            if (jobAdId == null)
            {
                // Creating a new job ad.

                return View(GetEditJobAdModel(true, CreateNewJobAd(CurrentEmployer)));
            }

            // Editing an existing job ad.

            var jobAd = GetJobAd(CurrentUser.Id, jobAdId.Value);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            return View(GetEditJobAdModel(false, GetJobAdModel(jobAd)));
        }

        [HttpPost, ActionName("JobAd"), ButtonClicked("preview"), ValidateInput(false)]
        public ActionResult PreviewJobAd(Guid? jobAdId, JobAdModel jobAdModel, HttpPostedFileBase logo, Guid? logoId)
        {
            var isNew = jobAdId == null;

            try
            {
                var employer = CurrentEmployer;
                var anonymousUser = CurrentAnonymousUser;

                if (jobAdId == null)
                {
                    // Creating a new job ad.

                    jobAdId = CreateJobAd(employer, anonymousUser, jobAdModel, logo, logoId).Id;
                }
                else
                {
                    // Editing an existing job ad.

                    var jobAd = GetJobAd(employer != null ? employer.Id : anonymousUser.Id, jobAdId.Value);
                    if (jobAd == null)
                        return NotFound("job ad", "id", jobAdId.Value);

                    // Update the the job ad.

                    UpdateJobAd(employer, anonymousUser, jobAd, jobAdModel, logo, logoId);
                }

                // Preview it.

                return RedirectToRoute(JobAdsRoutes.Preview, new { jobAdId });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(GetEditJobAdModel(isNew, jobAdModel));
        }

        [HttpPost, ActionName("JobAd"), ButtonClicked("save"), ValidateInput(false)]
        public ActionResult SaveJobAd(Guid? jobAdId, JobAdModel jobAdModel, HttpPostedFileBase logo, Guid? logoId)
        {
            var isNew = jobAdId == null;

            try
            {
                var employer = CurrentEmployer;
                var anonymousUser = CurrentAnonymousUser;

                string message;

                if (jobAdId == null)
                {
                    // Creating a new job ad.

                    jobAdId = CreateJobAd(employer, anonymousUser, jobAdModel, logo, logoId).Id;
                    message = "The job ad has been saved as a draft.";
                }
                else
                {
                    // Saving an existing job ad.

                    var jobAd = GetJobAd(employer.Id, jobAdId.Value);
                    if (jobAd == null)
                        return NotFound("job ad", "id", jobAdId.Value);

                    // Update the the job ad.

                    UpdateJobAd(employer, anonymousUser, jobAd, jobAdModel, logo, logoId);
                    message = "The job ad has been saved.";
                }

                // Then go back to the job ad.

                return RedirectToRouteWithConfirmation(JobAdsRoutes.JobAd, new { jobAdId }, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(GetEditJobAdModel(isNew, jobAdModel));
        }

        public ActionResult Preview(Guid jobAdId, JobAdFeaturePack? featurePack)
        {
            var user = CurrentUser;

            var jobAd = GetJobAd(user.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            return View(GetPreviewJobAdModel(user, jobAd, featurePack ?? JobAdFeaturePack.BaseFeaturePack));
        }

        [HttpPost, ActionName("Preview"), ButtonClicked("edit")]
        public ActionResult EditPreviewJobAd(Guid jobAdId)
        {
            return RedirectToRoute(JobAdsRoutes.JobAd, new { jobAdId });
        }

        [HttpPost, ActionName("Preview"), ButtonClicked("publish")]
        public ActionResult PublishJobAd(Guid jobAdId, JobAdFeaturePack? featurePack)
        {
            var user = CurrentUser;

            // Get the job ad.

            var jobAd = GetJobAd(user.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            // If the user is not logged in yet then redirect them.

            var employer = user as IEmployer;
            if (employer == null)
            {
                return featurePack == null || featurePack.Value == JobAdFeaturePack.BaseFeaturePack
                    ? RedirectToRoute(JobAdsRoutes.Account, new { jobAdId })
                    : RedirectToRoute(JobAdsRoutes.Account, new { jobAdId, featurePack });
            }
            
            try
            {
                return CheckPublish(employer, jobAd, featurePack);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(GetPreviewJobAdModel(user, jobAd, featurePack ?? JobAdFeaturePack.BaseFeaturePack));
        }

        [HttpPost, ActionName("Preview"), ButtonClicked("reopen")]
        public ActionResult ReopenJobAd(Guid jobAdId)
        {
            var employer = CurrentEmployer;

            var jobAd = GetJobAd(employer.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            try
            {
                // Open the job ad.

                _employerJobAdsCommand.OpenJobAd(employer, jobAd, true);

                // Redirect.

                return RedirectToUrlWithConfirmation(NavigationManager.GetUrlForPage<EmployerJobAds>(), "\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' has been reopened.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(GetPreviewJobAdModel(employer, jobAd, JobAdFeaturePack.BaseFeaturePack));
        }

        [HttpPost, ActionName("Preview"), ButtonClicked("repost")]
        public ActionResult RepostJobAd(Guid jobAdId, JobAdFeaturePack? featurePack)
        {
            var employer = CurrentEmployer;

            var jobAd = GetJobAd(employer.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            try
            {
                // Create a new job ad based on this one.

                var newJobAd = Copy(jobAd);
                _employerJobAdsCommand.CreateJobAd(employer, newJobAd);

                // Now publish it.

                return CheckPublish(employer, newJobAd, featurePack);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(GetPreviewJobAdModel(employer, jobAd, featurePack ?? JobAdFeaturePack.BaseFeaturePack));
        }

        public ActionResult Account()
        {
            return View(new AccountModel
            {
                Login = new Login(),
                Join = new EmployerJoin(),
                Industries = _industriesQuery.GetIndustries()
            });
        }

        [HttpPost, ButtonClicked("Join")]
        public ActionResult Account(Guid jobAdId, JobAdFeaturePack? featurePack, EmployerJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            try
            {
                // Get the job ad.

                var anonymousUser = CurrentAnonymousUser;
                var jobAd = GetJobAd(anonymousUser.Id, jobAdId);
                if (jobAd == null)
                    return NotFound("job ad", "id", jobAdId);

                // Process the post to check validations.

                if (acceptTerms == null || !acceptTerms.IsChecked)
                    ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, new StandardErrorHandler());

                joinModel.Prepare();
                joinModel.Validate();

                if (acceptTerms != null && acceptTerms.IsChecked)
                {
                    // Check for an existing login.

                    if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = joinModel.JoinLoginId }))
                        throw new DuplicateUserException();

                    // Create the account, transfer the job ad and publish it.

                    var employer = CreateEmployer(joinModel);
                    _employerJobAdsCommand.TransferJobAd(employer, jobAd);

                    // If this job ad is still a draft and a feature pack is requested then need to pay.

                    return CheckPublish(employer, jobAd, featurePack);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(new AccountModel
            {
                Login = new Login(),
                Join = joinModel,
                AcceptTerms = acceptTerms != null && acceptTerms.IsChecked,
                Industries = _industriesQuery.GetIndustries()
            });
        }

        [HttpPost, ButtonClicked("Login")]
        public ActionResult Account(Guid jobAdId, JobAdFeaturePack? featurePack, Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            try
            {
                // Get the job ad.

                var anonymousUser = CurrentAnonymousUser;
                var jobAd = GetJobAd(anonymousUser.Id, jobAdId);
                if (jobAd == null)
                    return NotFound("job ad", "id", jobAdId);

                // Process the post to check validations etc.

                loginModel.RememberMe = rememberMe != null && rememberMe.IsChecked;
                loginModel.Prepare();
                loginModel.Validate();

                // Authenticate.

                var result = _loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = loginModel.LoginId, PasswordHash = LoginCredentials.HashToString(loginModel.Password) });

                switch (result.Status)
                {
                    // Don't stop the user from purchasing if they need to change their password, they can do that next time they log in.

                    case AuthenticationStatus.Authenticated:
                    case AuthenticationStatus.AuthenticatedMustChangePassword:
                    case AuthenticationStatus.AuthenticatedWithOverridePassword:

                        // Log in.

                        _authenticationManager.LogIn(HttpContext, result.User, result.Status);
                        break;

                    default:
                        throw new AuthenticationFailedException();
                }

                // Now that the user has logged in, transfer the job ad and publish it.

                var employer = (IEmployer)result.User;
                _employerJobAdsCommand.TransferJobAd(employer, jobAd);

                return CheckPublish(employer, jobAd, featurePack);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(new AccountModel
            {
                Login = loginModel,
                Join = new EmployerJoin(),
                AcceptTerms = false,
                Industries = _industriesQuery.GetIndustries()
            });
        }

        [HttpPost, ButtonClicked("cancel")]
        public ActionResult Account(Guid jobAdId, JobAdFeaturePack? featurePack)
        {
            return featurePack == null
                ? RedirectToRoute(JobAdsRoutes.Preview, new { jobAdId })
                : RedirectToRoute(JobAdsRoutes.Preview, new { jobAdId, featurePack });
        }

        [EnsureAuthorized(UserType.Employer)]
        public ActionResult Payment(Guid jobAdId, JobAdFeaturePack featurePack)
        {
            var employer = CurrentEmployer;

            var jobAd = GetJobAd(employer.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            var product = _employerOrdersQuery.GetJobAdFeaturePackProduct(featurePack);
            if (product == null)
                return NotFound("feature pack", "featurePack", featurePack);

            var creditCard = new CreditCard { ExpiryDate = GetDefaultExpiryDate() };
            var order = _employerOrdersCommand.PrepareOrder(new[] { product.Id }, null, null, GetCreditCardType(creditCard));

            return View(new PaymentJobAdModel
            {
                AuthoriseCreditCard = false,
                CouponCode = null,
                CreditCard = creditCard,
                OrderDetails = _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()),
                Product = product,
            });
        }

        [HttpPost, EnsureAuthorized(UserType.Employer), ActionName("Payment"), ButtonClicked("Purchase")]
        public ActionResult PaymentPurchase(Guid jobAdId, JobAdFeaturePack featurePack, Guid? couponId, CreditCard creditCard, CheckBoxValue authoriseCreditCard)
        {
            var employer = CurrentEmployer;

            var jobAd = GetJobAd(employer.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            var product = _employerOrdersQuery.GetJobAdFeaturePackProduct(featurePack);
            if (product == null)
                return NotFound("feature pack", "featurePack", featurePack);

            var coupon = GetCoupon(couponId);
            var order = _employerOrdersCommand.PrepareOrder(new[] { product.Id }, coupon, null, GetCreditCardType(creditCard));

            try
            {
                // Validate the coupon first.

                ValidateCoupon(product.Id, couponId, coupon);

                // Check that the terms have been accepted but process the other fields as well.

                if (!authoriseCreditCard.IsChecked)
                    ModelState.AddModelError(new[] { new CreditCardAuthorisationValidationError("authoriseCreditCard") }, new NewOrderErrorHandler());

                // Validate the coupon.

                if (couponId != null && coupon == null)
                    ModelState.AddModelError(new[] { new NotFoundValidationError("coupon", couponId) }, new NewOrderErrorHandler());

                // Validate the credit card.

                creditCard.Validate();

                if (authoriseCreditCard.IsChecked && (couponId == null || coupon != null))
                {
                    // Purchase the order.

                    _employerOrdersCommand.PurchaseOrder(employer.Id, order, CreatePurchaser(employer), creditCard);

                    // Publish the job ad with the appropriate features.

                    jobAd.Features = _employerOrdersQuery.GetJobAdFeatures(featurePack);
                    jobAd.FeatureBoost = _employerOrdersQuery.GetJobAdFeatureBoost(featurePack);
                    _employerJobAdsCommand.UpdateJobAd(employer, jobAd);

                    return Publish(employer, jobAd, order.Id);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            return View(new PaymentJobAdModel
            {
                AuthoriseCreditCard = authoriseCreditCard != null && authoriseCreditCard.IsChecked,
                CouponCode = coupon == null ? null : coupon.Code,
                CreditCard = creditCard,
                OrderDetails = _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()),
                Product = product,
            });
        }

        [HttpPost, EnsureAuthorized(UserType.Employer), ActionName("Payment"), ButtonClicked("Cancel")]
        public ActionResult PaymentCancel(Guid jobAdId, JobAdFeaturePack featurePack)
        {
            return RedirectToRoute(JobAdsRoutes.Preview, new { jobAdId, featurePack });
        }

        [EnsureAuthorized(UserType.Employer)]
        public ActionResult Receipt(Guid jobAdId, Guid orderId)
        {
            var employer = CurrentEmployer;

            var jobAd = GetJobAd(employer.Id, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            var order = _ordersQuery.GetOrder(orderId);

            return View(new ReceiptJobAdModel
            {
                JobAd = _memberJobAdViewsQuery.GetMemberJobAdView(null, jobAd),
                OrderSummary = _employerOrdersQuery.GetOrderSummary(_creditsQuery, _ordersQuery, order, _productsQuery.GetProducts(), employer)
            });
        }

        private Employer CreateEmployer(EmployerJoin join)
        {
            var account = new EmployerAccount
            {
                FirstName = join.FirstName,
                LastName = join.LastName,
                EmailAddress = join.EmailAddress,
                PhoneNumber = join.PhoneNumber,
                OrganisationName = join.OrganisationName,
                Location = join.Location,
                SubRole = join.SubRole,
                IndustryIds = join.IndustryIds
            };

            var credentials = new AccountLoginCredentials
            {
                LoginId = join.JoinLoginId,
                Password = join.JoinPassword,
                ConfirmPassword = join.JoinConfirmPassword,
            };

            return _accountsManager.Join(HttpContext, account, credentials);
        }

        private ActionResult CheckPublish(IEmployer employer, JobAd jobAd, JobAdFeaturePack? featurePack)
        {
            if (jobAd.Status == JobAdStatus.Draft && (featurePack != null && featurePack.Value != JobAdFeaturePack.BaseFeaturePack))
                return RedirectToRoute(JobAdsRoutes.Payment, new { jobAdId = jobAd.Id, featurePack });

            return Publish(employer, jobAd, null);
        }

        private ActionResult Publish(IEmployer employer, JobAd jobAd, Guid? orderId)
        {
            _employerAllocationsCommand.EnsureJobAdCredits(employer);

            // Open it.

            _employerJobAdsCommand.OpenJobAd(employer, jobAd, true);

            if (orderId == null)
            {
                // Redirect based upon preference.

                var jobPoster = _jobPostersQuery.GetJobPoster(employer.Id);
                var url = jobPoster.ShowSuggestedCandidates
                    ? JobAdsRoutes.SuggestedCandidates.GenerateUrl(new { jobAdId = jobAd.Id })
                    : JobAdsRoutes.JobAd.GenerateUrl();
                var message = "<p>\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + jobAd.ExpiryTime.Value.ToShortDateString() + "</b>.</p>";
                return RedirectToUrlWithConfirmation(url, message);
            }

            return RedirectToRoute(JobAdsRoutes.Receipt, new { jobAdId = jobAd.Id, orderId = orderId.Value });
        }

        private JobAd GetJobAd(Guid userId, Guid jobAdId)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null)
                return null;

            // Ensure that the user is the poster.

            return jobAd.PosterId == userId ? jobAd : null;
        }

        private EditJobAdModel GetEditJobAdModel(bool isNew, JobAdModel jobAdModel)
        {
            return new EditJobAdModel
            {
                IsNew = isNew,
                JobAd = jobAdModel,
                Reference = new JobAdReferenceModel
                {
                    DefaultCountry = ActivityContext.Location.Country,
                    Countries = _locationQuery.GetCountries(),
                    Industries = _industriesQuery.GetIndustries(),
                }
            };
        }

        private void CreateLogo(JobAdModel jobAdModel, HttpPostedFileBase logo, Guid? logoId)
        {
            jobAdModel.Logo = new LogoModel();
            if (logo == null && logoId == null)
                return;

            if (logo == null)
            {
                // File already uploaded.

                jobAdModel.Logo.FileReferenceId = logoId;
                return;
            }

            // Uploading a new file.

            var fileName = Path.GetFileName(logo.FileName);
            var fileReference = _employerLogosCommand.SaveLogo(new HttpPostedFileContents(logo), fileName);
            jobAdModel.Logo.FileReferenceId = fileReference.Id;
        }

        private JobAd CreateJobAd(IEmployer employer, AnonymousUser anonymousUser, JobAdModel jobAdModel, HttpPostedFileBase logo, Guid? logoId)
        {
            CreateLogo(jobAdModel, logo, logoId);

            // Prepare it first.

            jobAdModel.Prepare();

            // Update a new instance.

            var jobAd = new JobAd();
            UpdateJobAd(jobAd, jobAdModel);

            // Create the job ad.

            if (employer != null)
                _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            else
                _anonymousJobAdsCommand.CreateJobAd(anonymousUser, jobAd);

            return jobAd;
        }

        private void UpdateJobAd(IEmployer employer, AnonymousUser anonymousUser, JobAd jobAd, JobAdModel jobAdModel, HttpPostedFileBase logo, Guid? logoId)
        {
            // Update the job ad.

            CreateLogo(jobAdModel, logo, logoId);
            UpdateJobAd(jobAd, jobAdModel);

            // Save it.

            if (employer != null)
                _employerJobAdsCommand.UpdateJobAd(employer, jobAd);
            else
                _anonymousJobAdsCommand.UpdateJobAd(anonymousUser, jobAd);
        }

        private void UpdateJobAd(JobAd jobAd, JobAdModel jobAdModel)
        {
            // Validate it first after updating the content.

            jobAdModel.Summary = HtmlUtil.TextToHtml(jobAdModel.Summary);
            jobAdModel.Content = HtmlUtil.TextToHtml(jobAdModel.Content);

            IList<ValidationError> errors = new List<ValidationError>();

            try
            {
                jobAdModel.Validate();
            }
            catch (ValidationErrorsException ex)
            {
                // If there are errors then need to put things back the way they were.

                jobAdModel.Summary = HtmlUtil.HtmlToText(jobAdModel.Summary);
                jobAdModel.Content = HtmlUtil.HtmlToText(jobAdModel.Content);

                errors = ex.Errors.ToList();
            }

            // Need to explicitly check these.

            if (jobAdModel.ContactDetails == null || string.IsNullOrEmpty(jobAdModel.ContactDetails.EmailAddress))
                errors.Add(new RequiredValidationError("email address"));

            if (jobAdModel.ContactDetails != null && !string.IsNullOrEmpty(jobAdModel.ContactDetails.PhoneNumber))
            {
                IValidator validator = new PhoneNumberValidator();
                if (!validator.IsValid(jobAdModel.ContactDetails.PhoneNumber))
                    errors = errors.Concat(validator.GetValidationErrors("PhoneNumber")).ToList();
            }

            if (jobAdModel.ContactDetails != null && !string.IsNullOrEmpty(jobAdModel.ContactDetails.FaxNumber))
            {
                IValidator validator = new PhoneNumberValidator();
                if (!validator.IsValid(jobAdModel.ContactDetails.FaxNumber))
                    errors = errors.Concat(validator.GetValidationErrors("FaxNumber")).ToList();
            }

            if (jobAdModel.IndustryIds.IsNullOrEmpty())
                errors.Add(new RequiredValidationError("industry"));

            if (jobAdModel.JobTypes == JobTypes.None)
                errors.Add(new RequiredValidationError("job type"));

            if (jobAdModel.Location == null || string.IsNullOrEmpty(jobAdModel.Location.ToString()))
                errors.Add(new RequiredValidationError("location"));

            const JobAdFeaturePack featurePack = JobAdFeaturePack.BaseFeaturePack;
            var features = _employerOrdersQuery.GetJobAdFeatures(featurePack);
            var defaultExpiryTime = _jobAdsCommand.GetDefaultExpiryTime(features);
            if (jobAdModel.ExpiryTime != null)
            {
                if (jobAdModel.ExpiryTime.Value > defaultExpiryTime)
                    errors.Add(new JobAdExpiryValidationError("ExpiryTime", (defaultExpiryTime - DateTime.Now).Days + 1));
            }

            if (errors.Count > 0)
                throw new ValidationErrorsException(errors);

            // Assign.

            jobAd.Title = jobAdModel.Title;
            jobAd.ContactDetails = jobAdModel.ContactDetails;
            jobAd.Features = features;
            jobAd.FeatureBoost = _employerOrdersQuery.GetJobAdFeatureBoost(featurePack);

            // If the date has not been changed from the default then let it remain the default.

            jobAd.ExpiryTime = jobAd.ExpiryTime == null && jobAdModel.ExpiryTime != null && jobAdModel.ExpiryTime.Value.Date == defaultExpiryTime.Date
                ? null
                : jobAdModel.ExpiryTime;

            jobAd.Visibility.HideCompany = jobAdModel.HideCompany;
            jobAd.Visibility.HideContactDetails = jobAdModel.HideContactDetails;

            jobAd.Integration.ExternalReferenceId = jobAdModel.ExternalReferenceId;

            jobAd.LogoId = jobAdModel.Logo == null ? null : jobAdModel.Logo.FileReferenceId;

            jobAd.Description.Content = jobAdModel.Content;
            jobAd.Description.CompanyName = jobAdModel.CompanyName;
            jobAd.Description.PositionTitle = jobAdModel.PositionTitle;
            jobAd.Description.ResidencyRequired = jobAdModel.ResidencyRequired;
            jobAd.Description.JobTypes = jobAdModel.JobTypes;
            jobAd.Description.Industries = jobAdModel.IndustryIds == null
                ? new List<Industry>()
                : (from i in jobAdModel.IndustryIds select _industriesQuery.GetIndustry(i)).ToList();
            jobAd.Description.Summary = jobAdModel.Summary;
            jobAd.Description.Salary = jobAdModel.Salary;
            jobAd.Description.Package = jobAdModel.Package;
            jobAd.Description.BulletPoints = jobAdModel.BulletPoints;
            jobAd.Description.Location = jobAdModel.Location;
        }

        private JobAdModel CreateNewJobAd(IEmployer employer)
        {
            return new JobAdModel
            {
                IndustryIds = new List<Guid>(),
                ContactDetails = employer == null
                    ? new ContactDetails()
                    : new ContactDetails
                      {
                          FirstName = employer.FirstName,
                          LastName = employer.LastName,
                          EmailAddress = employer.EmailAddress.Address,
                          PhoneNumber = employer.PhoneNumber == null ? null : employer.PhoneNumber.Number,
                      },
                ExpiryTime = GetExpiryTime(null, JobAdFeatures.None),
                ResidencyRequired = true,
                Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, null),
                Logo = new LogoModel
                {
                    FileReferenceId = employer == null ? null : _jobAdsCommand.GetLastUsedLogoId(employer.Id),
                },
            };
        }

        private JobAdModel GetJobAdModel(JobAd jobAd)
        {
            return new JobAdModel
            {
                Id = jobAd.Id,
                Title = jobAd.Title,
                ExpiryTime = GetExpiryTime(jobAd.ExpiryTime, jobAd.Features),
                Status = jobAd.Status,

                PositionTitle = jobAd.Description.PositionTitle,
                ExternalReferenceId = jobAd.Integration.ExternalReferenceId,

                HideContactDetails = jobAd.Visibility.HideContactDetails,
                HideCompany = jobAd.Visibility.HideCompany,

                ContactDetails = new ContactDetails
                {
                    FirstName = jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.FirstName,
                    LastName = jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.LastName,
                    EmailAddress = jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.EmailAddress,
                    SecondaryEmailAddresses = jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.SecondaryEmailAddresses,
                    PhoneNumber = jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.PhoneNumber,
                    FaxNumber = jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.FaxNumber,
                },

                Logo = new LogoModel
                {
                    FileReferenceId = jobAd.LogoId,
                },

                Summary = HtmlUtil.HtmlToText(jobAd.Description.Summary),
                Content = HtmlUtil.HtmlToText(jobAd.Description.Content),
                Package = jobAd.Description.Package,
                CompanyName = jobAd.Description.CompanyName,
                ResidencyRequired = jobAd.Description.ResidencyRequired,

                JobTypes = jobAd.Description.JobTypes,

                IndustryIds = jobAd.Description.Industries == null ? new List<Guid>() : jobAd.Description.Industries.Select(i => i.Id).ToList(),

                BulletPoints = new[]
                {
                    jobAd.Description.BulletPoints == null || jobAd.Description.BulletPoints.Count < 1 ? null : jobAd.Description.BulletPoints[0],
                    jobAd.Description.BulletPoints == null || jobAd.Description.BulletPoints.Count < 2 ? null : jobAd.Description.BulletPoints[1],
                    jobAd.Description.BulletPoints == null || jobAd.Description.BulletPoints.Count < 3 ? null : jobAd.Description.BulletPoints[2]
                },

                Location = jobAd.Description.Location,
                Salary = jobAd.Description.Salary,
            };
        }

        private PreviewJobAdModel GetPreviewJobAdModel(IUser employer, JobAd jobAd, JobAdFeaturePack featurePack)
        {
            var canBeOpened = _jobAdsCommand.CanBeOpened(jobAd);
            var expiryTime = canBeOpened ? GetExpiryTime(jobAd.ExpiryTime, jobAd.Features) : GetExpiryTime(null, jobAd.Features);

            // For the purposes of previewing the job ad treat it as if it is open no matter what its real status is.

            var status = jobAd.Status;
            jobAd.Status = JobAdStatus.Open;

            // Preview the job ad as an anonymous member.

            var view = _memberJobAdViewsQuery.GetMemberJobAdView(null, jobAd);

            return new PreviewJobAdModel
            {
                JobAd = view,
                JobAdCredits = GetJobAdCredits(employer),
                Status = status,
                ExpiryTime = expiryTime,
                CanBeOpened = canBeOpened,
                JobPoster = employer,
                FeaturePack = featurePack,
                Features = GetFeatures(),
                OrganisationCssFile = employer is IEmployer ? HttpContext.GetOrganisationJobAdCssFile(((IEmployer)employer).Organisation.Id) : null,
            };
        }

        private IDictionary<JobAdFeaturePack, JobAdFeaturesModel> GetFeatures()
        {
            return new[]
            {
                JobAdFeaturePack.BaseFeaturePack,
                JobAdFeaturePack.FeaturePack1,
                JobAdFeaturePack.FeaturePack2
            }.ToDictionary(x => x, GetFeatures);
        }

        private JobAdFeaturesModel GetFeatures(JobAdFeaturePack featurePack)
        {
            var features = _employerOrdersQuery.GetJobAdFeatures(featurePack);
            return new JobAdFeaturesModel
            {
                ShowLogo = features.IsFlagSet(JobAdFeatures.Logo),
                IsHighlighted = features.IsFlagSet(JobAdFeatures.Highlight),
                ExpiryTime = GetExpiryTime(null, features),
            };
        }

        private int? GetJobAdCredits(IUser user)
        {
            var employer = user as IEmployer;
            if (employer == null)
                return 0;

            // Need both applicant and job ad credits.

            var jobAdCreditId = _creditsQuery.GetCredit<JobAdCredit>().Id;
            var applicantCreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id;
            var allocations = _employerCreditsQuery.GetEffectiveActiveAllocations(employer, new[] { jobAdCreditId, applicantCreditId });

            return allocations[applicantCreditId].RemainingQuantity == 0
                ? 0
                : allocations[jobAdCreditId].RemainingQuantity;
        }

        private DateTime GetExpiryTime(DateTime? expiryTime, JobAdFeatures features)
        {
            return expiryTime != null ? expiryTime.Value : _jobAdsCommand.GetDefaultExpiryTime(features);
        }

        private static JobAd Copy(JobAd jobAd)
        {
            return new JobAd
            {
                Title = jobAd.Title,
                Integration =
                {
                    ExternalApplyUrl = jobAd.Integration.ExternalApplyUrl,
                    ExternalReferenceId = jobAd.Integration.ExternalReferenceId,
                    IntegratorReferenceId = jobAd.Integration.IntegratorReferenceId,
                    JobBoardId = jobAd.Integration.JobBoardId
                },
                Description =
                {
                    CompanyName = jobAd.Description.CompanyName,
                    BulletPoints = jobAd.Description.BulletPoints == null
                        ? null
                        : MiscUtils.Clone(jobAd.Description.BulletPoints.ToArray()),
                    Content = jobAd.Description.Content,
                    JobTypes = jobAd.Description.JobTypes,
                    Package = jobAd.Description.Package,
                    PositionTitle = jobAd.Description.PositionTitle,
                    ResidencyRequired = jobAd.Description.ResidencyRequired,
                    Salary = jobAd.Description.Salary == null ? null : jobAd.Description.Salary.Clone(),
                    Summary = jobAd.Description.Summary,
                    Location = jobAd.Description.Location == null ? null : jobAd.Description.Location.Clone(),
                    Industries = jobAd.Description.Industries == null
                        ? null
                        : new List<Industry>(jobAd.Description.Industries)
                },
                ContactDetails = jobAd.ContactDetails == null
                    ? null
                    : new ContactDetails
                    {
                        FirstName = jobAd.ContactDetails.FirstName,
                        LastName = jobAd.ContactDetails.LastName,
                        CompanyName = jobAd.ContactDetails.CompanyName,
                        EmailAddress = jobAd.ContactDetails.EmailAddress,
                        FaxNumber = jobAd.ContactDetails.FaxNumber,
                        PhoneNumber = jobAd.ContactDetails.PhoneNumber
                    }
            };
        }


        private static CreditCardType GetCreditCardType(CreditCard creditCard)
        {
            return creditCard == null ? default(CreditCardType) : creditCard.CardType;
        }

        private static ExpiryDate GetDefaultExpiryDate()
        {
            // Make the default next moth.

            return new ExpiryDate(DateTime.Now.AddMonths(1));
        }

        private Coupon GetCoupon(Guid? couponId)
        {
            return couponId == null
                ? null
                : _couponsQuery.GetCoupon(couponId.Value);
        }

        private void ValidateCoupon(Guid productId, Guid? couponId, Coupon coupon)
        {
            if (couponId == null)
                return;

            if (coupon == null)
                throw new CouponNotFoundException();

            var user = CurrentRegisteredUser;
            _ordersCommand.ValidateCoupon(coupon, user == null ? (Guid?)null : user.Id, new[] { productId });
        }

        private Purchaser CreatePurchaser(IEmployer employer)
        {
            return new Purchaser
            {
                Id = employer.Id,
                EmailAddress = employer.EmailAddress.Address,
                IpAddress = HttpContext.Request.UserHostAddress,
            };
        }
    }
}