<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.Products.NewOrderPaymentModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Products.NewOrder) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Employers.Orders) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">New order</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
        <div class="section-body">
            <% Html.RenderPartial("WizardSteps", Model.Steps); %>
        </div>
    </div>
    <div class="hint_section section">
        <div class="section-body">
            <p>We use SecurePay to ensure all payments are securely processed.</p>
            <img src="<%= Images.Block.SecurePay %>" style="display: block; margin: 0 auto; padding-top: 5px;" alt="Payment Gateway by SecurePay" />
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Payment</h1>
    </div>

    <div class="form">
<%  using (Html.RenderForm(Context.GetClientUrl()))
    { %>
        <% Html.RenderPartial("PartialPayment", Model.Payment); %>

        <div class="right-buttons-section section">
            <div class="section-body">
                <%= Html.ButtonsField(new BackButton(), new PurchaseButton(), new CancelButton()) %>
            </div>
        </div>
<%  } %>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.employers.orders.order.ready({
                urls: {
                    apiCouponUrl: '<%= Html.RouteRefUrl(CouponsRoutes.Coupon) %>',
                    prepareOrderUrl: '<%= Html.RouteRefUrl(ProductsRoutes.PrepareOrder) %>'
                },
                productIds: ['<%= Model.ProductId %>']
            });
        });
    </script>
        
</asp:Content>
