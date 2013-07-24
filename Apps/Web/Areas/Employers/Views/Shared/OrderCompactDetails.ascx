<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.OrderDetailsModel>" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.Orders"%>
<%@ Import Namespace="LinkMe.Web.Domain.Products"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Products"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Orders"%>

<table class="order">
    <tbody>
<%  for (var i = 0; i < Model.Order.Items.Count; ++i)
    {
        var item = Model.Order.Items[i];
        var product = Model.OrderProducts[i];
        var adjustment = product.GetPrimaryCreditAdjustment(); %>
        <tr class="item item_<%= (i % 2) == 0 ? "odd" : "even" %> order-row<%= i == Model.Order.Items.Count-1 ? " divider" : "" %>">
            <td class="row-title"><%= Model.Credits[adjustment.CreditId].Description %></td>
            <td class="row-price"><%= Html.Price(item.Price, item.Currency) %></td>
        </tr>
<%  } %>
        <tr class="subtotal_order-row order-row after-divider">
            <td class="row-title">Total (excl. GST)</td>
            <td class="row-price"><%= Html.Price(Model.Order.Price, Model.Order.Currency) %></td>
        </tr>
<%  for (var index = 0; index < Model.Order.Adjustments.Count; ++index)
    {
        var adjustment = Model.Order.Adjustments[index]; %>
                <tr class="order-row <%= Html.AdjustmentLabelCssClass(adjustment) %>">
                    <td class="row-title"><%= Html.AdjustmentLabelText(Model.Order, adjustment, Model.Products) %></td>
                    <td class="row-price"><%= Html.Price(adjustment.AdjustedPrice - adjustment.InitialPrice, Model.Order.Currency)%></td>
                </tr>
<%      if (index == Model.Order.Adjustments.Count - 1)
        { %>
                    <tr class="order-row last_order-adjustment order-adjustment">
                        <td class="row-title">Total (incl. GST)</td>
                        <td class="row-price"><%= Html.Price(adjustment.AdjustedPrice, Model.Order.Currency)%></td>
                    </tr>
<%      }
    } %>
    </tbody>                
</table>

<%  if (Model.Order.Adjustments.Count(a => a.GetType() == typeof(BundleAdjustment)) > 0)
    { %>
<p class="bundle-discount">You are receiving the 10% bundle discount.</p>
<%  } %>

<img class="payment-options" src="<%= Images.Block.PaymentOptions %>" alt="Pay with VISA, American Express or Mastercard" />
