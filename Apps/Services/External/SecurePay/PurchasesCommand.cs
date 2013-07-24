using System;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class PurchasesCommand
        : IPurchasesCommand
    {
        private static readonly EventSource EventSource = new EventSource<PurchasesCommand>();

        private readonly ISendSecurePayCommand _sendSecurePayCommand;
        private readonly bool _useAntiFraud;
        private readonly Country _country;
        private readonly string _merchantId;
        private readonly string _password;

        public PurchasesCommand(ISendSecurePayCommand sendSecurePayCommand, ILocationQuery locationQuery, bool useAntiFraud, string merchantId, string password, string antiFraudMerchantId, string antiFraudPassword, string country)
        {
            _sendSecurePayCommand = sendSecurePayCommand;
            _useAntiFraud = useAntiFraud;
            _merchantId = _useAntiFraud ? antiFraudMerchantId : merchantId;
            _password = _useAntiFraud ? antiFraudPassword : password;
            _country = locationQuery.GetCountry(country);
        }

        CreditCardReceipt IPurchasesCommand.PurchaseOrder(Order order, Purchaser purchaser, CreditCard creditCard)
        {
            return _useAntiFraud
                ? PurchaseOrder<AntiFraudPaymentRequestTxn, AntiFraudPaymentResponseTxn>(order, purchaser, creditCard)
                : PurchaseOrder<StandardPaymentRequestTxn, StandardPaymentResponseTxn>(order, purchaser, creditCard);
        }

        private CreditCardReceipt PurchaseOrder<TRequestTxn, TResponseTxn>(Order order, Purchaser purchaser, CreditCard creditCard)
            where TRequestTxn : PaymentRequestTxn, new()
            where TResponseTxn : PaymentResponseTxn, new()
        {
            const string method = "PurchaseOrder";

            PaymentRequestMessage<TRequestTxn> requestMessage;
            try
            {
                // Create the request message.

                requestMessage = Mappings.Map<TRequestTxn>(order, purchaser, creditCard, _country, _merchantId, _password);
                requestMessage.Prepare();
                requestMessage.Validate();
            }
            catch (Exception ex)
            {
                // This shouldn't happen at this stage.

                EventSource.Raise(Event.Error, method, "Cannot create a valid request message.", ex);
                throw new PurchaseSystemException(ex);
            }

            PaymentResponseMessage<TResponseTxn> responseMessage;
            try
            {
                responseMessage = _sendSecurePayCommand.Send<TRequestTxn, TResponseTxn>(requestMessage);
            }
            catch (Exception ex)
            {
                throw new PurchaseSystemException(ex);
            }

            // Check and map.

            Check(responseMessage);
            return Mappings.Map(responseMessage);
        }

        private static void Check<TResponseTxn>(PaymentResponseMessage<TResponseTxn> message)
            where TResponseTxn : PaymentResponseTxn, new()
        {
            const string method = "Check";

            // First check the status for effective system errors.

            if (message.Status.StatusCode != 0)
            {
                EventSource.Raise(Event.Error, method, "An invalid status has been returned from the payment gateway.", Event.Arg("StatusCode", message.Status.StatusCode), Event.Arg("StatusDescription", message.Status.StatusDescription));
                throw new SecurePayStatusException(message.Status.StatusCode, message.Status.StatusDescription);
            }

            if (!message.Payment.TxnList.Txn.IsApproved)
            {
                EventSource.Raise(Event.Warning, method, "A credit card process has not been approved by the payment gateway.", Event.Arg("ResponseCode", message.Payment.TxnList.Txn.ResponseCode), Event.Arg("ResponseText", message.Payment.TxnList.Txn.ResponseText));

                // Check for fraud.

                if (message.Payment.TxnList.Txn is AntiFraudPaymentResponseTxn)
                {
                    var antiFraudTxn = message.Payment.TxnList.Txn as AntiFraudPaymentResponseTxn;
                    if (!(string.IsNullOrEmpty(antiFraudTxn.AntiFraudResponseCode) || antiFraudTxn.AntiFraudResponseCode == "0" || antiFraudTxn.AntiFraudResponseCode == "00" || antiFraudTxn.AntiFraudResponseCode == "000"))
                        throw new SecurePayFraudException(antiFraudTxn.AntiFraudResponseCode, antiFraudTxn.AntiFraudResponseText, (FraudGuard)antiFraudTxn.FraudGuard.Clone());
                }

                throw new SecurePayNotApprovedException(message.Payment.TxnList.Txn.ResponseCode, message.Payment.TxnList.Txn.ResponseText);
            }
        }

        RefundReceipt IPurchasesCommand.RefundOrder(Guid orderId, string externalTransactionId)
        {
            // Create the request message.

            var requestMessage = Mappings.Map(orderId, externalTransactionId, _merchantId, _password);
            requestMessage.Prepare();
            requestMessage.Validate();

            // Send it.

            var responseMessage = _sendSecurePayCommand.Send<RefundRequestTxn, RefundResponseTxn>(requestMessage);
            return Mappings.Map(responseMessage);
        }
    }
}
