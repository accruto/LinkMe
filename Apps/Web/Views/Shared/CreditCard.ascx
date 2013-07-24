<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.CreditCard" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<%= Html.TextBoxField(Model, c => c.CardHolderName).WithLabel("Name on card").WithLargerWidth() %>
<%= Html.CreditCardTypeField(Model, c => c.CardType)
        .WithLabel("Card type")
        .WithHelpText("A " + AmexSurcharge.ToString("F1") + "% surcharge will be applied to all American Express payments") %>
<%= Html.TextBoxField(Model, c => c.CardNumber)
        .WithLabel("Credit card number")
        .WithHelpText("Digits only, no spaces or dashes")%>
<%= Html.TextBoxField(Model, c => c.Cvv)
        .WithLabel("Security code")
        .WithHelpText("<p><strong>Visa</strong> and <strong>MasterCard</strong>: This is the last 3 digits on the signature stripe of your card.</p>" + 
                    "<p><strong>American Express</strong>: This is the 4 digits on the front of the card above the account number.</p>") %>
<%= Html.ControlsField()
        .WithCssPrefix("monthyear")
        .WithLabel("Expiry date")
        .Add(Html.DropDownListField(Model, "ExpiryMonth", c => c.ExpiryDate.Month.ToString("D2"), ExpiryMonths).WithCssPrefix("month").WithLabel("/"))
        .Add(Html.DropDownListField(Model, "ExpiryYear", c => (c.ExpiryDate.Year % 100).ToString("D2"), ExpiryYears).WithCssPrefix("year").WithLabel(""))%>

