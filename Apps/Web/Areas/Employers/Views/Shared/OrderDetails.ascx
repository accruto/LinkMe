<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.Orders"%>
<%@ Import Namespace="LinkMe.Web.Domain.Products"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.OrderDetailsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Products"%>

<table class="order list">
    <thead>
        <tr>
            <th>Product</th>
            <th class="header-quantity">Quantity</th>
            <th class="header-price">Price</th>
        </tr>
    </thead>
    <tbody>
<%  for (var i = 0; i < Model.Order.Items.Count; ++i)
    {
        var item = Model.Order.Items[i];
        var product = Model.OrderProducts[i];
        var adjustment = product.GetPrimaryCreditAdjustment(); %>
                <tr class="item item_<%= (i % 2) == 0 ? "odd" : "even" %> order-row<%= i == Model.Order.Items.Count-1 ? " divider" : "" %>">
                    <td class="row-title"><%= product.Description %></td>
                    <td class="row-quantity"><%= adjustment != null && adjustment.Quantity != null ? adjustment.Quantity.Value.ToString(CultureInfo.InvariantCulture) : "1" %></td>
                    <td class="row-price"><%= Html.Price(item.Price, item.Currency, false) %></td>
                </tr>
<%  } %>
        <tr class="subtotal_order-row order-row after-divider">
            <td></td>
            <td class="row-title">Total (excl. GST)</td>
            <td class="row-price"><%= Html.Price(Model.Order.Price, Model.Order.Currency, false) %></td>
        </tr>
<%  for (var index = 0; index < Model.Order.Adjustments.Count; ++index)
    {
        var adjustment = Model.Order.Adjustments[index]; %>
                <tr class="order-row <%= Html.AdjustmentLabelCssClass(adjustment) %>">
                    <td></td>
                    <td class="row-title"><%= Html.AdjustmentLabelText(Model.Order, adjustment, Model.Products)%></td>
                    <td class="row-price"><%= Html.Price(adjustment.AdjustedPrice - adjustment.InitialPrice, Model.Order.Currency, false)%></td>
                </tr>
<%      if (index == Model.Order.Adjustments.Count - 1)
        { %>
                    <tr class="order-row last_order-adjustment order-adjustment">
                        <td></td>
                        <td class="row-title">Total amount payable (incl. GST)</td>
                        <td class="row-price"><%= Html.Price(adjustment.AdjustedPrice, Model.Order.Currency, false)%></td>
                    </tr>
<%      }
    } %>
    </tbody>                
</table>
