using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Pageflows;
using LinkMe.Domain.Products;
using EmployerJoin = LinkMe.Web.Models.Accounts.EmployerJoin;

namespace LinkMe.Web.Areas.Employers.Models.Products
{
    public class NewOrderPageflow
        : Pageflow
    {
        private readonly PageflowStep _chooseStep = new PageflowStep("Choose");
        private readonly PageflowStep _accountStep = new PageflowStep("Account");
        private readonly PageflowStep _paymentStep = new PageflowStep("Payment");
        private readonly PageflowStep _receiptStep = new PageflowStep("Receipt");

        public bool HasInitialUser { get; set; }
        public Login Login { get; set; }
        public EmployerJoin Join { get; set; }
        public Guid OrderId { get; set; }
        public Guid ContactProductId { get; set; }
        public string CouponCode { get; set; }
        public Guid? CouponId { get; set; }
        public bool UseDiscount { get; set; }
        public bool AcceptTerms { get; set; }
        public CreditCard CreditCard { get; set; }
        public bool AuthoriseCreditCard { get; set; }

        protected override IList<PageflowStep> GetSteps()
        {
            _accountStep.IsEnabled = !HasInitialUser;
            return new[] { _chooseStep, _accountStep, _paymentStep, _receiptStep };
        }

        protected override PageflowStep MoveToNext(PageflowStep currentStep)
        {
            switch (currentStep.Name)
            {
                case "Choose":
                    return HasInitialUser ? _paymentStep : _accountStep;

                case "Account":
                    return _paymentStep;

                case "Payment":
                    Complete();
                    return _receiptStep;

                default:
                    return null;
            }
        }

        protected override PageflowStep MoveToPrevious(PageflowStep currentStep)
        {
            switch (currentStep.Name)
            {
                case "Account":
                    return _chooseStep;

                case "Payment":
                    return HasInitialUser ? _chooseStep : _accountStep;

                default:
                    return null;
            }
        }
    }
}