<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Query.Search.JobAds.JobAdSearchCriteria>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceSaveSearch) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceSaveSearch) %>
    </mvc:RegisterJavaScripts>

    <script type="text/javascript">
        var errorMsg = new Array();
        <% if (!ViewData.ModelState.IsValid && ViewData.ModelState.GetErrorMessages().Length > 0) { %>
        errorMsg.push({ Key : "name", Message : "<%= ViewData.ModelState.GetErrorMessages()[0] %>"  });
        <% } %>
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="savesearch">
        <div class="title">Save as a favourite search</div>
        <div class="errorinfo">
            Save failed. Please try again
            <ul>
            </ul>
        </div>
        <form method="POST" id="SaveSearch" action="<%= Html.RouteRefUrl(SearchRoutes.SaveSearch, new { returnUrl = Html.RouteRefUrl(SearchRoutes.Results) }) %>">
		<%= Html.TextBoxField(Model, "name", m => m.GetDisplayText()).WithLabel(" ")%>
        <%= Html.CheckBoxField("createAlert", true).WithLabelOnRight("Email me updates to these results") %>
        <!--div class="button save" data-url="<%= Html.RouteRefUrl(SearchRoutes.ApiCreateSearchFromCurrent) %>" data-returnurl="<%= Html.RouteRefUrl(SearchRoutes.Results) %>">SAVE</div-->
        <input type="submit" class="button save" value="SAVE" id="SAVE"/>
        </form>
    </div>
</asp:Content>