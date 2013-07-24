<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<NewOrderReceiptModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Products"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Products.NewOrder) %>
    </mvc:RegisterStyleSheets>
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
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Receipt</h1>
    </div>
    
    <div class="form">
        <div class="section">
            <div class="section-body">
                <p>
                    Thanks for buying credit.
                    Your receipt is below (which you can <%= Html.RouteRefLink("print", ProductsRoutes.Print, new { id = Model.OrderSummary.OrderDetails.Order.Id }, new { @class = "print-receipt-action", target = "_blank" })%>)
                    and we've also sent a copy to your email address.
                </p>
            </div>
        </div>
    
        <div class="section">
            <div class="section-head"></div>
            <div class="section-body">
                <div>
                    <h2>Getting started</h2>
                </div>
                <div class="section-content">
                    <p>You can start using your credits right away.</p>
                    <ul class="actions">
                        <li><%= Html.RouteRefLink("Search for candidates", SearchRoutes.Search) %></li>
                        <li><%= Html.RouteRefLink("Post a job ad", JobAdsRoutes.JobAd)%></li>
                    </ul>
                </div>
            </div>
            <div class="section-foot"></div>
        </div>
    
        <% Html.RenderPartial("PartialReceipt", Model.OrderSummary); %>
        
    </div>
    
<%  var price = (int) Model.OrderSummary.OrderDetails.Order.Price; %>    
    
    <!-- Google Code for Online purchase Conversion Page -->
    <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 954048662;
        var google_conversion_language = "en";
        var google_conversion_format = "3";
        var google_conversion_color = "ffffff";
        var google_conversion_label = "oo5ICKKqugMQlsH2xgM";
        var google_conversion_value = <%= price %>;
        /* ]]> */
    </script>
    <script type="text/javascript" src="https://www.googleadservices.com/pagead/conversion.js">
    </script>
    <noscript>
        <div style="display:inline;">
            <img height="1" width="1" style="border-style:none;" alt="" src="https://www.googleadservices.com/pagead/conversion/954048662/?value=<%= price %>&label=oo5ICKKqugMQlsH2xgM&amp;guid=ON&amp;script=0"/>
        </div>
    </noscript>

</asp:Content>
