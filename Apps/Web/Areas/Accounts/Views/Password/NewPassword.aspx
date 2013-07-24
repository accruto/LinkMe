<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<NewPasswordModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Models"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.Block.Forms)%>
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.StyleSheet(StyleSheets.Block.Accounts.Passwords) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Request new password</h1>
    </div>
    
    <div class="section">
        <div class="section-content">
            
            <div class="form passwords">

<%  using (Html.RenderForm(Context.GetClientUrl()))
    {
        using (Html.RenderFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>	        
            <p>Enter your username or email address and a new password will be emailed to you.</p>
            
            <%= Html.TextBoxField(Model, m => m.LoginId).WithLabel("Username or email address").WithIsRequired().WithLargerWidth() %>
	        <%= Html.ButtonsField(new SendButton(), new CancelButton()) %>
<%      }
    } %>	        

            </div>
        </div>
    </div>

</asp:Content>

