<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Print.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.OrderSummaryModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="form">
    
        <div class="shadowed-section shadowed_section section">

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
        
</asp:Content>
