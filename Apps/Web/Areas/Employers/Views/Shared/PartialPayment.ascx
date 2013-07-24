<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.PaymentModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models" %>
<%@ Import Namespace="LinkMe.Web.Html" %>

<%  Html.RenderPartial("Discount", Model); %>

<div class="shadowed-section shadowed_section section">
    <div class="section-head"></div>
    <div class="section-body">
        <div class="section-title">
            <h2>Coupon</h2>
        </div>
        <div class="section-content">
<%  using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
    { %>
            <%= Html.TextBoxField(Model, m => m.CouponCode).WithLabel("Coupon code") %>
            <%= Html.ButtonsField().Add(new ApplyButton()) %>
<%  } %>
        </div>
    </div>
    <div class="section-foot"></div>
</div>

<div class="shadowed-section shadowed_section section">
    <div class="section-head"></div>
    <div class="section-body">
        <div class="section-title">
            <h2>Order summary</h2>
        </div>
        <div class="section-content">
            <div id="order-details">
                <% Html.RenderPartial("OrderDetails", Model.OrderDetails); %>
            </div>
        </div>
    </div>
    <div class="section-foot"></div>
</div>

<%  using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
    { %>
<div class="shadowed-section shadowed_section section">
    <div class="section-head"></div>
    <div class="section-body">
        <div class="section-title">
            <h2>Purchaser details</h2>
        </div>
        <div class="section-content">
            
            <% Html.RenderPartial("CreditCard", Model.CreditCard); %>
            <% Html.PartialField("AuthoriseCreditCard", new CheckBoxValue { IsChecked = Model.AuthoriseCreditCard }).WithCssPrefix("checkbox").Render(); %>
            <%= Html.Hidden("CouponId") %>
                    
        </div>
    </div>
    <div class="section-foot"></div>
</div>
<%  } %>

