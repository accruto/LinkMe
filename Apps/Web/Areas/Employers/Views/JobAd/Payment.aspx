<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.JobAds.PaymentJobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets ID="RegisterStyleSheets1" runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.JobAds.JobAd) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts ID="RegisterJavaScripts1" runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Employers.Orders) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <% Html.RenderPartial("SecurePay", false); %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Payment</h1>
    </div>

    <div class="form">
<%  using (Html.RenderForm(Context.GetClientUrl()))
    { %>
        <% Html.RenderPartial("PartialPayment", Model); %>

        <div class="right-buttons-section section">
            <div class="section-body">
                <%= Html.ButtonsField(new PurchaseButton(), new CancelButton()) %>
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
                productIds: ['<%= Model.Product.Id %>']
            });
        });
    </script>

</asp:Content>