<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="System.Web.Mvc.ViewPage<LinkMe.Web.Areas.Employers.Models.JobAds.ReceiptJobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets ID="RegisterStyleSheets1" runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts ID="RegisterJavaScripts1" runat="server">
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <% Html.RenderPartial("SecurePay", false); %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Receipt</h1>
    </div>
    
    <div class="form">
        <div class="section">
            <div class="section-content">
                <p>
                    '<%= HtmlUtil.HtmlToText(Model.JobAd.Title) %>' was successfully published.
                </p>
                <p>
                    It will expire on <b><%= Model.JobAd.ExpiryTime.Value.ToShortDateString() %></b>.
                </p>
                <p>
                    Your receipt is below (which you can <%= Html.RouteRefLink("print", ProductsRoutes.Print, new { id = Model.OrderSummary.OrderDetails.Order.Id }, new { @class = "print-receipt-action", target = "_blank" })%>)
                    and we've also sent a copy to your email address.
                </p>
            </div>
        </div>

        <div class="section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-content">
                    <ul class="actions">
                        <li><%= Html.RouteRefLink("View suggested candidates for this job ad", JobAdsRoutes.SuggestedCandidates, new { jobAdId = Model.JobAd.Id }) %></li>
                        <li><%= Html.RouteRefLink("Search for candidates", SearchRoutes.Search) %></li>
                        <li><%= Html.RouteRefLink("Post another job ad", JobAdsRoutes.JobAd) %></li>
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