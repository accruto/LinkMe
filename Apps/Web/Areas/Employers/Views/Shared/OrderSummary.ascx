<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<LinkMe.Web.Areas.Employers.Models.OrderSummaryModel>" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.Orders"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<div id="order-summary">
    <% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft)) { %>
        <%= Html.TextBoxField(Model.Purchaser, p => p.FullName).WithLargerWidth().WithLabel("Recipient").WithIsReadOnly() %>
        <%= Html.TextBoxField(Model.OrderDetails.Order, o => o.ConfirmationCode).WithLabel("Order #").WithIsReadOnly() %>
        <%= Html.TextBoxField(Model.OrderDetails.Order, "Time", o => Html.OrderTime(o.Time)).WithLabel("Date").WithIsReadOnly()%>
        <%= Html.TextBoxField(Model.Receipt, r => r.CreditCard.Pan).WithLabel("Credit Card").WithIsReadOnly()%>
    <% } %>
</div>

<div id="order-details">
    <% Html.RenderPartial("OrderDetails", Model.OrderDetails); %>
</div>