<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.OrderSummaryModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>

<div class="shadowed-section shadowed_section section">
    <div class="section-head"></div>
    <div class="section-body">
        <div class="section-title">
            <h2>Receipt</h2>

            <div id="print-receipt">
                    <ul class="horizontal-actions actions">
                    <li><%= Html.RouteRefLink("Print receipt", ProductsRoutes.Print, new { id = Model.OrderDetails.Order.Id }, new { @class = "print-action", target = "_blank" })%></li>
                </ul>
            </div>
        </div>
        <div class="section-content">
                
            <% Html.RenderPartial("ReceiptSellerDetails"); %>
            <% Html.RenderPartial("OrderSummary", Model); %>

        </div>
    </div>
    <div class="section-foot"></div>
            
</div>
