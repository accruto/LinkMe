<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<Member>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Welcome to LinkMe</h1>
    </div>
    
    <div class="section">
        <div class="section-head"></div>
        <div class="section-body">
<%  using (Html.RenderForm())
    { %>        
            <p>Please check your email <span class="blue"><%= Model.EmailAddresses[0].Address %></span> to verify your account.</p>
	        <p>
	            If you are experiencing other problems trying to access
	            our site, please contact our Member Support Hotline on <%= LinkMe.Apps.Agents.Constants.PhoneNumbers.FreecallHtml %>
	            or <%= LinkMe.Apps.Agents.Constants.PhoneNumbers.InternationalHtml %>.
	        </p>
<%  } %>	        
        </div>
        <div class="section-foot"></div>
    </div>

</asp:Content>

