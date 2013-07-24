<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SearchesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.RecentJobSearches)%>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.RecentJobSearches)%>
    </mvc:RegisterJavaScripts>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <div class="header">My <%= Model.Type == SearchesType.Recent ? "recent" : "favourite" %> searches</div>
    <ul class="breadcrumbs">
        <li>Candidate site</li>
        <li>Candidate centre</li>
        <li>My <%= Model.Type == SearchesType.Recent ? "recent" : "favourite" %> searches</li>
    </ul>    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="prompt success">
        <div class="leftbar"></div>
        <div class="bg">
            <div class="text">An email alert "<span></span>" has been successfully created.</div>
        </div>
        <div class="rightbar">
            <div class="icon close"></div>
        </div>
    </div>
    <div class="tabs">
        <div class="tab Recent<%= Model.Type == SearchesType.Recent ? " active" : "" %>" url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.PartialRecentSearches, new { Page = 1 })) %>"></div>
        <div class="tab Favourite<%= Model.Type == SearchesType.Saved ? " active" : "" %>" url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.PartialSavedSearches, new { Page = 1 })) %>"></div>
    </div>
    <div class="tabcontent">
        <div class="tab Recent<%= Model.Type == SearchesType.Recent ? " active" : "" %>">
            <div class="topbar"></div>
<%  if (Model.Type == SearchesType.Recent)
    {
        Html.RenderPartial("Recent", Model);
    } %>            
        </div>
        <div class="tab Favourite<%= Model.Type == SearchesType.Saved ? " active" : "" %>">
            <div class="topbar"></div>
<%  if (Model.Type == SearchesType.Saved)
    {
        Html.RenderPartial("Saved", Model);
    } %>            
        </div>
    </div>
    <div class="overlay rename">
        <%= Html.TextBoxField("NewName", string.Empty).WithLabel("New search name").WithAttribute("data-watermark", "")%>
        <div class="button rename" url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiRenameSearch)) %>"></div>
        <div class="button cancel"></div>
    </div>
    <div class="overlay delete">
        <div class="text name">Are you sure you want to delete <span></span> ?</div>
        <div class="text undoable">This is not undoable</div>
        <div class="button delete" url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiDeleteSearch)) %>"></div>
        <div class="button cancel"></div>
    </div>
</asp:Content>