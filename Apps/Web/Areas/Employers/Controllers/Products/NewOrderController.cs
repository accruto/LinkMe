using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Pageflows;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Affiliations;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Employers.Orders.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Accounts;
using LinkMe.Web.Areas.Employers.Models.Products;
using LinkMe.Web.Areas.Employers.Routes;
using EmployerJoin = LinkMe.Web.Models.Accounts.EmployerJoin;

namespace LinkMe.Web.Areas.Employers.Controllers.Products
{
    public class NewOrderController
        : EmployersPageflowController<NewOrderPageflow>
    {
        private static readonly PageflowRoutes Routes = new PageflowRoutes
        {
            new PageflowRoute("Choose", ProductsRoutes.Choose),
            new PageflowRoute("Account", ProductsRoutes.Account),
            new PageflowRoute("Payment", ProductsRoutes.Payment),
            new PageflowRoute("Receipt", ProductsRoutes.Receipt)
        };

        private readonly IEmployerOrdersCommand _employerOrdersCommand;
        private readonly IEmployerOrdersQuery _employerOrdersQuery;
        private readonly IProductsQuery _productsQuery;
        private readonly ICreditsQuery _creditsQuery;
        private readonly ICouponsQuery _couponsQuery;
        private readonly IOrdersCommand _ordersCommand;
        private readonly IOrdersQuery _ordersQuery;
        private readonly IAccountsManager _accountsManager;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ILoginAuthenticationCommand _loginAuthenticationCommand;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IIndustriesQuery _industriesQuery;

        public NewOrderController(IPageflowEngine pageflowEngine, IEmployerOrdersCommand employerOrdersCommand, IEmployerOrdersQuery employerOrdersQuery, IProductsQuery productsQuery, ICreditsQuery creditsQuery, ICouponsQuery couponsQuery, IOrdersCommand ordersCommand, IOrdersQuery ordersQuery, IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, ILoginAuthenticationCommand loginAuthenticationCommand, IAuthenticationManager authenticationManager, IIndustriesQuery industriesQuery)
            : base(Routes, pageflowEngine)
        {
            _employerOrdersCommand = employerOrdersCommand;
            _employerOrdersQuery = employerOrdersQuery;
            _productsQuery = productsQuery;
            _creditsQuery = creditsQuery;
            _couponsQuery = couponsQuery;
            _ordersCommand = ordersCommand;
            _ordersQuery = ordersQuery;
            _accountsManager = accountsManager;
            _loginCredentialsQuery = loginCredentialsQuery;
            _loginAuthenticationCommand = loginAuthenticationCommand;
            _authenticationManager = authenticationManager;
            _industriesQuery = industriesQuery;
        }

        public ActionResult NewOrder()
        {
            return View();
        }

        public ActionResult Choose(string couponCode)
        {
            if (!string.IsNullOrEmpty(couponCode))
                Pageflow.CouponCode = couponCode;

            return ChooseView(Pageflow.ContactProductId);
        }

        [HttpPost, ButtonClicked("purchase")]
        public ActionResult Choose(Guid contactProductId)
        {
            try
            {
                // Save the products.

                Pageflow.ContactProductId = contactProductId;

                // Go to the next page.

                return Next();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            return ChooseView(contactProductId);
        }

        [HttpPost, ActionName("Choose"), ButtonClicked("Cancel")]
        public ActionResult ChooseCancel()
        {
            return NewOrderCancel();
        }

        public ActionResult Account()
        {
            // Show the page.

            var coupon = GetCoupon(Pageflow.CouponId);
            var order = PrepareOrder(Pageflow.ContactProductId, coupon, Pageflow.UseDiscount, Pageflow.CreditCard);

            return AccountView(order, Pageflow.Login, Pageflow.Join, Pageflow.AcceptTerms);
        }

        [HttpPost, ButtonClicked("back")]
        public ActionResult Account(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe, EmployerJoin join, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            try
            {
                // Try to process the login first.

                if (!loginModel.IsEmpty || (rememberMe != null && rememberMe.IsChecked))
                {
                    loginModel.RememberMe = rememberMe != null && rememberMe.IsChecked;
                    loginModel.Prepare();
                    loginModel.Validate();
                    Save(loginModel, new EmployerJoin(), false);
                }
                else if (!join.IsEmpty || acceptTerms.IsChecked)
                {
                    if (acceptTerms == null || !acceptTerms.IsChecked)
                        ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, new NewOrderErrorHandler());

                    join.Prepare();
                    join.Validate();
                    Save(new Login(), join, acceptTerms != null && acceptTerms.IsChecked);

                    // Check for an existing login.

                    if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = join.JoinLoginId }))
                        throw new DuplicateUserException();
                }

                // Go to the previous page.

                return Previous();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            var coupon = GetCoupon(Pageflow.CouponId);
            var order = PrepareOrder(Pageflow.ContactProductId, coupon, Pageflow.UseDiscount, Pageflow.CreditCard);
            return AccountView(order, loginModel, join, acceptTerms != null && acceptTerms.IsChecked);
        }

        [HttpPost, ButtonClicked("Join")]
        public ActionResult Account(EmployerJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            try
            {
                // Process the post to check validations.

                if (acceptTerms == null || !acceptTerms.IsChecked)
                    ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, new NewOrderErrorHandler());

                joinModel.Prepare();
                joinModel.Validate();
                Save(new Login(), joinModel, acceptTerms != null && acceptTerms.IsChecked);

                if (acceptTerms != null && acceptTerms.IsChecked)
                {
                    // Check for an existing login.

                    if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = joinModel.JoinLoginId }))
                        throw new DuplicateUserException();

                    // Go to the next page.

                    return Next();
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            var coupon = GetCoupon(Pageflow.CouponId);
            var order = PrepareOrder(Pageflow.ContactProductId, coupon, Pageflow.UseDiscount, Pageflow.CreditCard);
            return AccountView(order, null, joinModel, acceptTerms != null && acceptTerms.IsChecked);
        }

        [HttpPost, ButtonClicked("Login")]
        public ActionResult Account(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            try
            {
                // Process the post to check validations etc.

                loginModel.RememberMe = rememberMe != null && rememberMe.IsChecked;
                loginModel.Prepare();
                loginModel.Validate();
                Save(loginModel, new EmployerJoin(), false);

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

                // Go to the next page.

                return Next();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            var coupon = GetCoupon(Pageflow.CouponId);
            var order = PrepareOrder(Pageflow.ContactProductId, coupon, Pageflow.UseDiscount, Pageflow.CreditCard);
            return AccountView(order, loginModel, null, false);
        }

        [HttpPost, ActionName("Account"), ButtonClicked("cancel")]
        public ActionResult AccountCancel()
        {
            return NewOrderCancel();
        }

        public ActionResult Payment()
        {
            var coupon = GetCoupon(Pageflow.CouponId);
            var order = PrepareOrder(Pageflow.ContactProductId, coupon, Pageflow.UseDiscount, Pageflow.CreditCard);
            return PaymentView(Pageflow.ContactProductId, order, Pageflow.CouponCode, Pageflow.UseDiscount, Pageflow.CreditCard, Pageflow.AuthoriseCreditCard);
        }

        [HttpPost, ButtonClicked("back")]
        public ActionResult Payment(CheckBoxValue useDiscount, Guid? couponId, CreditCard creditCard, CheckBoxValue authoriseCreditCard)
        {
            var coupon = GetCoupon(couponId);

            try
            {
                if (ShouldProcessPaymentPost(couponId, creditCard, authoriseCreditCard))
                {
                    // Validate the coupon first.

                    ValidateCoupon(Pageflow.ContactProductId, couponId, coupon);

                    // Check that the terms have been accepted but process the other fields as well.

                    if (authoriseCreditCard == null || !authoriseCreditCard.IsChecked)
                        ModelState.AddModelError(new[] { new CreditCardAuthorisationValidationError("authoriseCreditCard") }, new NewOrderErrorHandler());

                    // Validate the credit card.

                    creditCard.Validate();

                    // Save the data.

                    Pageflow.UseDiscount = useDiscount != null && useDiscount.IsChecked;
                    Pageflow.CreditCard = creditCard;
                    Pageflow.AuthoriseCreditCard = authoriseCreditCard != null && authoriseCreditCard.IsChecked;
                }

                // Go to the previous page.

                return Previous();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            var order = PrepareOrder(Pageflow.ContactProductId, coupon, useDiscount != null && useDiscount.IsChecked, Pageflow.CreditCard);
            return PaymentView(Pageflow.ContactProductId, order, Pageflow.CouponCode, useDiscount != null && useDiscount.IsChecked, creditCard, authoriseCreditCard != null && authoriseCreditCard.IsChecked);
        }

        [HttpPost, ActionName("Payment"), ButtonClicked("purchase")]
        public ActionResult PaymentAccept(CheckBoxValue useDiscount, Guid? couponId, CreditCard creditCard, CheckBoxValue authoriseCreditCard)
        {
            var coupon = GetCoupon(couponId);
            var order = PrepareOrder(Pageflow.ContactProductId, coupon, useDiscount != null && useDiscount.IsChecked, creditCard);

            try
            {
                // Validate the coupon first.

                ValidateCoupon(Pageflow.ContactProductId, couponId, coupon);

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
                    // Create the user if needed.

                    var employer = CurrentEmployer ?? CreateEmployer();

                    // Purchase the order.

                    _employerOrdersCommand.PurchaseOrder(employer.Id, order, CreatePurchaser(employer), creditCard);

                    // Save the data.

                    Pageflow.UseDiscount = useDiscount != null && useDiscount.IsChecked;
                    Pageflow.CreditCard = creditCard;
                    Pageflow.AuthoriseCreditCard = true;
                    Pageflow.OrderId = order.Id;

                    // Go to the next page.

                    return Next();
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new NewOrderErrorHandler());
            }

            // Show the user the errors.

            return PaymentView(Pageflow.ContactProductId, order, Pageflow.CouponCode, useDiscount != null && useDiscount.IsChecked, creditCard, authoriseCreditCard != null && authoriseCreditCard.IsChecked);
        }

        [HttpPost, ActionName("Payment"), ButtonClicked("cancel")]
        public ActionResult PaymentCancel()
        {
            return NewOrderCancel();
        }

        public ActionResult Receipt()
        {
            var order = _ordersQuery.GetOrder(Pageflow.OrderId);
            var products = _productsQuery.GetProducts();
            return View(new NewOrderReceiptModel { OrderSummary = _employerOrdersQuery.GetOrderSummary(_creditsQuery, _ordersQuery, order, products, CurrentRegisteredUser) });
        }

        private Employer CreateEmployer()
        {
            var account = new EmployerAccount
            {
                FirstName = Pageflow.Join.FirstName,
                LastName = Pageflow.Join.LastName,
                EmailAddress = Pageflow.Join.EmailAddress,
                PhoneNumber = Pageflow.Join.PhoneNumber,
                OrganisationName = Pageflow.Join.OrganisationName,
                Location = Pageflow.Join.Location,
                SubRole = Pageflow.Join.SubRole,
                IndustryIds = Pageflow.Join.IndustryIds
            };

            var credentials = new AccountLoginCredentials
            {
                LoginId = Pageflow.Join.JoinLoginId,
                Password = Pageflow.Join.JoinPassword,
                ConfirmPassword = Pageflow.Join.JoinConfirmPassword,
            };

            return _accountsManager.Join(HttpContext, account, credentials);
        }

        private ActionResult NewOrderCancel()
        {
            // Cancel the wizard.

            CancelPageflow();

            // Redirect away.

            return User.Id() == null ? RedirectToRoute(HomeRoutes.Home) : RedirectToUrl(HttpContext.GetHomeUrl());
        }

        private ActionResult ChooseView(Guid contactProductId)
        {
            // Generate an order using the default credit card.

            var order = _employerOrdersCommand.PrepareOrder(_productsQuery, _creditsQuery, new[] { contactProductId }, null, null, default(CreditCardType));

            return View(new NewOrderChooseModel
            {
                SelectedContactProductId = contactProductId,
                OrderDetails = _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()),
                ContactProducts = _employerOrdersQuery.GetContactProducts(),
            });
        }

        private ActionResult AccountView(Order order, Login login, EmployerJoin join, bool acceptTerms)
        {
            return View(new NewOrderAccountModel
            {
                Account = new AccountModel
                {
                    Login = login ?? new Login(),
                    Join = join ?? new EmployerJoin(),
                    AcceptTerms = acceptTerms,
                    Industries = _industriesQuery.GetIndustries()
                },
                OrderDetails = _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()),
            });
        }

        private ActionResult PaymentView(Guid productId, Order order, string couponCode, bool useDiscount, CreditCard creditCard, bool authoriseCreditCard)
        {
            if (creditCard == null)
                creditCard = new CreditCard();
            if (creditCard.ExpiryDate == ExpiryDate.MinValue)
                creditCard.ExpiryDate = GetDefaultExpiryDate();

            // Show the page.

            return View(new NewOrderPaymentModel
            {
                ProductId = productId,
                Payment = new PaymentModel
                {
                    UseDiscount = useDiscount,
                    CouponCode = couponCode,
                    OrderDetails = _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()),
                    CreditCard = creditCard,
                    AuthoriseCreditCard = authoriseCreditCard,
                }
            });
        }

        private void Save(Login login, EmployerJoin join, bool acceptTerms)
        {
            // Save the employer.

            Pageflow.Login = login;
            Pageflow.Join = join;
            Pageflow.AcceptTerms = acceptTerms;
        }

        private static bool ShouldProcessPaymentPost(Guid? couponId, CreditCard creditCard, CheckBoxValue authoriseCreditCard)
        {
            var defaultExpiryDate = GetDefaultExpiryDate();

            // Only process if something has been entered for one or more of the fields and the defaults have been changed.

            return couponId != null
                || !string.IsNullOrEmpty(creditCard.CardNumber)
                || !string.IsNullOrEmpty(creditCard.Cvv)
                || !string.IsNullOrEmpty(creditCard.CardHolderName)
                || creditCard.CardType != 0
                || creditCard.ExpiryDate.Month != defaultExpiryDate.Month
                || creditCard.ExpiryDate.Year != defaultExpiryDate.Year
                || authoriseCreditCard.IsChecked;
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

        private void ValidateCoupon(Guid contactProductId, Guid? couponId, Coupon coupon)
        {
            if (couponId == null)
                return;

            if (coupon == null)
                throw new CouponNotFoundException();

            var user = CurrentRegisteredUser;
            _ordersCommand.ValidateCoupon(coupon, user == null ? (Guid?)null : user.Id, new[] { contactProductId });
        }

        private Order PrepareOrder(Guid contactProductId, Coupon coupon, bool useDiscount, CreditCard creditCard)
        {
            return _employerOrdersCommand.PrepareOrder(_productsQuery, _creditsQuery, new[] { contactProductId }, coupon, useDiscount ? new VecciDiscount { Percentage = 0.2m } : null, GetCreditCardType(creditCard));
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

        private static CreditCardType GetCreditCardType(CreditCard creditCard)
        {
            return creditCard == null ? default(CreditCardType) : creditCard.CardType;
        }

        protected override NewOrderPageflow CreatePageflow()
        {
            return new NewOrderPageflow
            {
                HasInitialUser = User.IsAuthenticated(),
                ContactProductId = _employerOrdersQuery.GetDefaultContactProductId(),
            };
        }
    }
}