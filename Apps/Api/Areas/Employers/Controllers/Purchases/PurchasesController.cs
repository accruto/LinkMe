using System;
using LinkMe.Apps.Api.Areas.Employers.Models.Purchases;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.External.Apple.AppStore;
using System.Web.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers.Purchases
{
    [EnsureHttps, ApiEnsureAuthorized]
    public class PurchasesController
        : ApiController
    {
        private readonly EventSource _eventSource = new EventSource<PurchasesController>();

        private readonly ISendAppleCommand _purchasesCommand;
        private readonly IOrdersCommand _ordersCommand;

        public PurchasesController(ISendAppleCommand purchasesCommand, IOrdersCommand ordersCommand)
        {
            _purchasesCommand = purchasesCommand;
            _ordersCommand = ordersCommand;
        }

        /// <summary>
        /// Take an encoded transaction receipt from the app and pass it onto 
        /// Apple for verification. Record the details 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Verify(VerifyModel verifyModel)
        {
            var response = _purchasesCommand.Verify(verifyModel.EncodedTransactionReceipt);

            if (response.Status != 0)
                ModelState.AddModelError("Verification", "Verification Failed");
            else
            {
                try
                {
                    //Record the purchase
                    var employer = CurrentEmployer;

                    //Apple productIds are alphanumeric plus '_' and '.'
                    //ProductIds supplied from the app are our Guids with '_' subbed for '-'

                    var productId = new Guid(response.Receipt.ProductId.Replace('_','-'));

                    var order = _ordersCommand.PrepareOrder(new[] { productId }, null, null, null);
                    _ordersCommand.RecordOrder(employer.Id, order,
                        CreatePurchaser(employer),
                        CreateReceipt(response));
                }
                catch (Exception ex)
                {
                    _eventSource.Raise(Event.Error, "Apple Verification", ex, new StandardErrorHandler());
                    ModelState.AddModelError("Verification", "There was a problem adding the credits you purchased to your account. Please contact LinkMe immediately to have these added manually.");
                }
            }

            return Json(new JsonResponseModel());
        }

        private static AppleReceipt CreateReceipt(JsonVerificationResponse response)
        {
            return new AppleReceipt
                       {
                           ExternalTransactionId = response.Receipt.TransactionId,
                           ExternalTransactionTime = response.Receipt.PurchaseDate.Value,
                       };
        }

        private static Purchaser CreatePurchaser(IEmployer employer)
        {
            return new Purchaser
                       {
                           Id = employer.Id, 
                           EmailAddress = employer.EmailAddress.Address
                       };
        }
    }
}