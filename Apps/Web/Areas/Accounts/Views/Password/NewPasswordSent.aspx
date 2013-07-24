<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.Block.Forms)%>
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
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
            <p>Your new password has been emailed to you.</p>
	        <%= Html.ButtonsField(new DoneButton()) %>
<%      }
    } %>	        
            </div>
        </div>
    </div>

</asp:Content>

