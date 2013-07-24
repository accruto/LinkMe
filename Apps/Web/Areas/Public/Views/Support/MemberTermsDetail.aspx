<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Blank.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Support) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title terms-detail">
        <h1>Member terms and conditions</h1>
    </div>
    
	<div class="first_section section forms_v2 terms-detail">
		<div class="section-content">

            <% Html.RenderPartial("MemberTermsContent", false); %>

        </div>
    </div>
    
</asp:Content>