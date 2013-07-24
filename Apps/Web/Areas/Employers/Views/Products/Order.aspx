<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.OrderSummaryModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Products"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.Employer) %>
        <%= Html.StyleSheet(StyleSheets.OrdersAndReceipts) %>
        <%= Html.StyleSheet(StyleSheets.Folders) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.Overlay) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftAjax) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftMvcAjax) %>
        <%= Html.JavaScript(JavaScripts.CustomCheckbox) %>
        <%= Html.JavaScript(JavaScripts.AlignWith) %>
        <%= Html.JavaScript(JavaScripts.DesktopMenu) %>
        <%= Html.JavaScript(JavaScripts.TextOverflow) %>
        <%= Html.JavaScript(JavaScripts.Download) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.SectionCollapsible) %>
        <%= Html.JavaScript(JavaScripts.Slider) %>
        <%= Html.JavaScript(JavaScripts.EApi) %>
        <%= Html.JavaScript(JavaScripts.EmployersApi) %>
        <%= Html.JavaScript(JavaScripts.Credits) %>
        <%= Html.JavaScript(JavaScripts.Overlay) %>
        <%= Html.JavaScript(JavaScripts.Actions) %>
        <%= Html.JavaScript(JavaScripts.Folders) %>
        <%= Html.JavaScript(JavaScripts.FlagLists) %>
        <%= Html.JavaScript(JavaScripts.Search) %>
        <%= Html.JavaScript(JavaScripts.BlockLists) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JsonManipulations) %>
        <%= Html.JavaScript(JavaScripts.Tabs) %>
        <%= Html.JavaScript(JavaScripts.ToggleCheckbox) %>
        <%= Html.JavaScript(JavaScripts.Tooltips) %>
        <%= Html.JavaScript(JavaScripts.CenterAlign) %>
        <%= Html.JavaScript(JavaScripts.HoverIntent) %>
        <%= Html.JavaScript(JavaScripts.Notes) %>
    </mvc:RegisterJavaScripts>
    
    <script language="javascript" type="text/javascript">
    
        var candidateContext = {
            isAnonymous: false,
        };
        
    </script>
    
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li><%= Html.RouteRefLink("Credits", ProductsRoutes.Credits) %></li>
        <li><%= Html.RouteRefLink("Orders", ProductsRoutes.Orders) %></li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div>
	    <div class="section">
		    <div class="section-content">
			    <ul class="plain_action-list action-list">
				    <li><%= Html.RouteRefLink("New search", SearchRoutes.Search, null, new {@class = "new-search-action"}) %></li>
			    </ul>
		    </div>
	    </div>

        <% Html.RenderPartial("FoldersSection"); %>
        <% Html.RenderPartial("BlockListsSection"); %>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Order # <%= Model.OrderDetails.Order.ConfirmationCode %></h1>
    </div>
    
    <div class="forms_v2">
        <div class="section">
            <div class="section-content">
                <ul class="action-list">
                    <li><%= Html.RouteRefLink("View all orders", ProductsRoutes.Orders, null)%></li>
                    <li><%= Html.RouteRefLink("Print", ProductsRoutes.Print, new { id = Model.OrderDetails.Order.Id }, new { @class = "print-receipt-action", target = "_blank" })%></li>
                </ul>
            </div>
        </div>
    
        <div class="light_px690_shadowed_section px690_shadowed_section shadowed_section section">

            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Receipt</h2>
                </div>
                <div class="section-content">
                
                    <% Html.RenderPartial("ReceiptSellerDetails"); %>
                    <% Html.RenderPartial("OrderSummary", Model); %>

                </div>
            </div>
            <div class="section-foot"></div>
            
        </div>
        
    </div>

    <script type="text/javascript" language="javascript">
        (function($) {

            $(".js_folders_collapsible").makeFoldersSectionCollapsible(false);
            $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(false);

            initializeFolders(candidateContext);
            initializeBlockLists();

        })(jQuery);
    </script>
        
</asp:Content>
