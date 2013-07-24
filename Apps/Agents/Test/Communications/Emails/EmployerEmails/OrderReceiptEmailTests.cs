using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Preparation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class OrderReceiptEmailTests
        : EmailTests
    {
        private const decimal TaxRate = 0.1m;

        private readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create an employer and order.

            var employer = CreateEmployer();

            var product = GetProduct<ContactCredit>();

            // Prepare the order.

            var order = new Order { Items = new List<OrderItem>(), Adjustments = new List<OrderAdjustment>() };
            PrepareOrder(order, product);
            var products = new[] { product };
            var credits = new[] { _creditsQuery.GetCredit(product.GetPrimaryCreditAdjustment().CreditId) };
            var adjustments = new[] { product.GetPrimaryCreditAdjustment() };
            var receipt = PurchaseOrder(CreateCreditCard());

            return new OrderReceiptEmail(employer, order, products, credits, adjustments, receipt);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer and order.

            var employer = CreateEmployer();

            var product = GetProduct<ContactCredit>();

            // Prepare the order.

            var order = new Order { Items = new List<OrderItem>(), Adjustments = new List<OrderAdjustment>() };
            PrepareOrder(order, product);
            var products = new [] {product};
            var credits = new[] { _creditsQuery.GetCredit(product.GetPrimaryCreditAdjustment().CreditId) };
            var adjustments = new[] { product.GetPrimaryCreditAdjustment() };
            var receipt = PurchaseOrder(CreateCreditCard());

            var communication = new OrderReceiptEmail(employer, order, products, credits, adjustments, receipt);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject(order));
            email.AssertHtmlView(GetBody(communication, employer, GetContent(employer, order, products, receipt)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static CreditCardReceipt PurchaseOrder(CreditCard creditCard)
        {
            // Save a copy.

            return new CreditCardReceipt
                       {
                           Id = Guid.NewGuid(),
                           Time = DateTime.Now,
                           ExternalTransactionId = Guid.NewGuid().ToString(),
                           ExternalTransactionTime = DateTime.Now,
                           CreditCard = new CreditCardSummary
                                            {
                                                Pan = GetPan(creditCard.CardNumber),
                                                Type = CreditCardType.Visa,
                                            }
                       };
        }

        private static string GetPan(string cardNumber)
        {
            return cardNumber.Substring(0, 6) + "..." + cardNumber.Substring(cardNumber.Length - 3, 3);
        }

        private static void PrepareOrder(Order order, Product product)
        {
            var item = new OrderItem { ProductId = product.Id, Price = product.Price, Currency = product.Currency };
            order.Items.Add(item);
            SetPrices(order);

            order.AdjustedPrice = GetAdjustedPrice(order.Price);
            order.Adjustments.Add(new TaxAdjustment { InitialPrice = order.Price, AdjustedPrice = order.AdjustedPrice, TaxRate = TaxRate });

            order.Prepare();
        }

        private static void SetPrices(Order order)
        {
            // The total price comes from adding up individual purchases.

            Currency currency = null;
            var price = order.Items.Sum(item => GetPrice(item, ref currency));

            order.Price = price;
            order.Currency = currency ?? Currency.AUD;
        }

        private static decimal GetPrice(OrderItem item, ref Currency currency)
        {
            if (currency == null)
            {
                currency = item.Currency;
                return item.Price;
            }

            if (currency == item.Currency)
                return item.Price;

            // Need to convert when there are more currencies ...

            return item.Price;
        }

        private static decimal GetAdjustedPrice(decimal priceExclTax)
        {
            return priceExclTax * (1 + TaxRate);
        }

        private string GetContent(ICommunicationRecipient employer, Order order, IList<Product> products, CreditCardReceipt receipt)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div style=\"font-family: Verdana, Arial, Helvetica, sans-serif;font-size:80%;margin-top:10px;padding-left:0;text-align:left;width:950px;\">");
            sb.AppendLine("  <div style=\"float:left;width:694px;\">");
            sb.AppendLine("    <div style=\"clear:left;margin-top:0;overflow:hidden;width:auto;padding:10px;margin-right:10px;margin-bottom:30px;border:1px solid #CCCCCC;\">");
            sb.AppendLine();
            sb.AppendLine("      <div style=\"padding-right:8px;padding-left:6px;margin-bottom:20px;padding:7px;margin:0;\">");
            sb.AppendLine("        <div style=\"float:right;\">");
            sb.AppendLine("          <div>");
            sb.AppendLine("            <div style=\"font-weight:bold;\">LinkMe Pty Ltd</div>");
            sb.AppendLine("            <div>");
            sb.AppendLine("              <div>PO Box 408</div>");
            sb.AppendLine("              <span>Neutral Bay</span>");
            sb.AppendLine("              <abbr title=\"New South Wales\">NSW</abbr>");
            sb.AppendLine("              <span>2089</span>");
            sb.AppendLine("            </div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div>");
            sb.AppendLine("            <span>ABN</span>: ");
            sb.AppendLine("            <span>95 111 132 953</span>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");
            sb.AppendLine();
            sb.AppendLine("      <div style=\"position:relative;border-width:1px medium 1px 1px 1px;border-style:solid none solid solid;border-left:1px solid #E0E0E0;border-color:#E8E8E8 -moz-use-text-color #CCCCCC #E0E0E0;padding:0; margin:0;border: medium none;backgroun:none repeat scroll 0 0 transparent;margin-top:0;\">");
            sb.AppendLine("        <h1 style=\"margin-top:0;margin-bottom:4px;font-weight:bold;font-family:Arial,helvetica,sans-serif;paffing:0;margin:0;font-size:175%;color:#214263\">Receipt</h1>");
            sb.AppendLine("      </div>");
            sb.AppendLine();
            sb.AppendLine("      <div style=\"padding-right:8px;padding-left:6px;margin-bottom:20px;padding:7px;margin:0;\">");
            sb.AppendLine("        <div style=\"margin-top:10px;margin-bottom:20px\">");
            sb.AppendLine("          <span>Recipient: </span><span style=\"font-weight:bold\">" + employer.FullName + "</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");
            sb.AppendLine();
            sb.AppendLine("      <div style=\"padding-right:8px;padding-left:6px;margin-bottom:20px;padding:7px;margin:0;\">");
            sb.AppendLine("        <fieldset style=\"padding:0;margin:0;border:medium none;\">");
            sb.AppendLine("          <div style=\"padding-top:0;\">");
            sb.AppendLine("            <label for=\"ConfirmationCode\" style=\"margin-right:10px;margin-left:3px;font-family:Verdana;line-height:14px;padding:0;margin:0;font-weight:bold;color:#4477AA;width:175px;text-align:right;position:absolute;margin-top:3px;font-size:100%\">Order #</label>");
            sb.AppendLine("            <div style=\"line-height:14px;padding:0;margin:0;position:relative;margin-right:190px;left:190px\">");
            sb.AppendLine("              <input id=\"ConfirmationCode\" name=\"ConfirmationCode\" value=\"" + order.ConfirmationCode + "\" type=\"text\" style=\"margin:0;line-height:16px;color:#333333;padding:2px;border:medium none;height:1.3em;width:20em;padding-left:1px;padding-bottom:1px\" />");
            sb.AppendLine("            </div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"padding-top:12px;\">");
            sb.AppendLine("            <label for=\"Time\" style=\"margin-right:10px;margin-left:3px;font-family:Verdana;line-height:14px;padding:0;margin:0;font-weight:bold;color:#4477AA;width:175px;text-align:right;position:absolute;margin-top:3px;font-size:100%\">Date</label>");
            sb.AppendLine("            <div style=\"line-height:14px;padding:0;margin:0;position:relative;margin-right:190px;left:190px\">");
            sb.AppendLine("              <input id=\"Time\" name=\"Time\" value=\"" + order.Time.ToShortDateString() + " " + order.Time.ToShortTimeString() + "\" type=\"text\" style=\"margin:0;line-height:16px;color:#333333;padding:2px;border:medium none;height:1.3em;width:20em;padding-left:1px;padding-bottom:1px\" />");
            sb.AppendLine("            </div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"padding-top:12px;\">");
            sb.AppendLine("            <label for=\"Pan\" style=\"margin-right:10px;margin-left:3px;font-family:Verdana;line-height:14px;padding:0;margin:0;font-weight:bold;color:#4477AA;width:175px;text-align:right;position:absolute;margin-top:3px;font-size:100%\">Credit Card</label>");
            sb.AppendLine("            <div style=\"line-height:14px;padding:0;margin:0;position:relative;margin-right:190px;left:190px\">");
            sb.AppendLine("              <input id=\"Pan\" name=\"Pan\" value=\"" + receipt.CreditCard.Pan + "\" type=\"text\" style=\"margin:0;line-height:16px;color:#333333;padding:2px;border:medium none;height:1.3em;width:20em;padding-left:1px;padding-bottom:1px\" />");
            sb.AppendLine("            </div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </fieldset>");
            sb.AppendLine();
            sb.AppendLine("        <table style=\"font-family:Verdana;font-size:100%;width:100%;padding:0;margin:0;border-spacing:0;border-collapse:separate;margin-top:30px;top:0;border-top:medium none\">");
            sb.AppendLine("          <thead>");
            sb.AppendLine("            <tr>");
            sb.AppendLine("              <th style=\"font-family:Verdana;font-size:100%;text-align:left;vertical-align:top;padding:5px 20px 5px 7px; font-weight:bold;border-bottom:1px solid #DADADA;background-color: #E8E8E8; border-left:1px solid #E4E4E4;\">Product</th>");
            sb.AppendLine("              <th style=\"font-family:Verdana;font-size:100%;text-align:left;vertical-align:top;padding:5px 20px 5px 7px; font-weight:bold;border-bottom:1px solid #DADADA;background-color: #E8E8E8; border-left:1px solid #E4E4E4;\">Quantity</th>");
            sb.AppendLine("              <th style=\"font-family:Verdana;font-size:100%;text-align:left;vertical-align:top;padding:5px 20px 5px 7px; font-weight:bold;border-bottom:1px solid #DADADA;background-color: #E8E8E8; border-left:1px solid #E4E4E4;\">Price</th>");
            sb.AppendLine("            </tr>");
            sb.AppendLine("          </thead>");
            sb.AppendLine();
            sb.AppendLine("          <tbody>");

            sb.AppendLine();
            for (var index = 0; index < order.Items.Count; ++index)
            {
                var item = order.Items[index];
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"text-align:left;border-spacing:0;border-collapse:separate;vertical-align:top;padding:7px;font-weight:normal;font-size:100%;font-family:Verdana;border-left:1px solid #F0F0F0;border-bottom:1px solid #DDDDDD;background-color:#F5F5F5;\">" + _creditsQuery.GetCredit(products[index].GetPrimaryCreditAdjustment().CreditId).Description + "</td>");
                sb.AppendLine("                <td style=\"text-align:left;border-spacing:0;border-collapse:separate;vertical-align:top;padding:7px;font-weight:normal;font-size:100%;font-family:Verdana;border-left:1px solid #F0F0F0;border-bottom:1px solid #DDDDDD;background-color:#F5F5F5;\">" + products[index].GetPrimaryCreditAdjustment().Quantity.Value + "</td>");
                sb.AppendLine("                <td style=\"text-align:left;border-spacing:0;border-collapse:separate;vertical-align:top;padding:7px;font-weight:normal;font-size:100%;font-family:Verdana;border-left:1px solid #F0F0F0;border-bottom:1px solid #DDDDDD;background-color:#F5F5F5;\">" + GetPrice(item.Price, item.Currency) + "</td>");
                sb.AppendLine("            </tr>");
                sb.AppendLine();
            }

            sb.AppendLine("            <tr>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\"></td>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\">Total (excl. GST)</td>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\">" + GetPrice(order.Price, order.Currency) + "</td>");
            sb.AppendLine("            </tr>");
            sb.AppendLine();
            sb.AppendLine("            <tr>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\"></td>");
            sb.AppendLine();
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\">Add GST</td>");
            sb.AppendLine();
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\">" + GetPrice(order.Adjustments[0].AdjustedPrice - order.Adjustments[0].InitialPrice, order.Currency) + "</td>");
            sb.AppendLine("            </tr>");
            sb.AppendLine();
            sb.AppendLine("            <tr>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\"></td>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\">Total amount payable (incl. GST)</td>");
            sb.AppendLine("              <td style=\"font-family:Verdana;font-size:100%;vertical-align:top;padding:7px;font-weight:normal;\">" + GetPrice(order.AdjustedPrice, order.Currency) + "</td>");
            sb.AppendLine("            </tr>");
            sb.AppendLine();
            sb.AppendLine("          </tbody>");
            sb.AppendLine("        </table>");
            sb.AppendLine();
            sb.AppendLine("      </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"clearer\" />");
            return sb.ToString();
        }

        private static string GetPrice(decimal price, Currency currency)
        {
            return price.ToString("C", currency.CultureInfo);
            //            return Math.Round(price, currency.CultureInfo.NumberFormat.CurrencyDecimalDigits) == Math.Round(price)
            //                ? price.ToString("C0", currency.CultureInfo)
            //                : price.ToString("C", currency.CultureInfo);
        }

        private static string GetSubject(Order order)
        {
            return "Receipt for Order # " + order.ConfirmationCode;
        }

        private static CreditCard CreateCreditCard()
        {
            return new CreditCard
                       {
                           CardNumber = "4444333322221111",
                           Cvv = "123",
                           ExpiryDate = new ExpiryDate(DateTime.Now.Date.AddYears(1)),
                       };
        }

        private Product GetProduct<T>()
            where T : Credit
        {
            // Select the first product that has a single credit adjustment for the given type.

            var credit = _creditsQuery.GetCredit<T>();
            return (from p in _productsQuery.GetProducts()
            where p.CreditAdjustments.Count == 1
                && p.GetPrimaryCreditAdjustment().CreditId == credit.Id
            select p).First();
        }
    }
}