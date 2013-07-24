<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<FolderListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceMobileFolder) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceJobAdItem)%>
    </mvc:RegisterJavaScripts>
    <script type="text/javascript">
        var criteria = {};
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="mobilefolder">
        <div class="title">My saved jobs</div>
<%  if (Model.Results.JobAdIds.Count > 0)
    {
        foreach (var j in Model.Results.JobAdIds)
            Html.RenderPartial("JobAdListView", Model.Results.JobAds[j]);
    }
    else
    { %>
            <div class="noresults">
                <div class="desc">You have no saved jobs.</div>
                <ul>
                    <li><%= Html.RouteRefLink("Search for jobs", HomeRoutes.Home) %>&nbsp;<span>now</span></li>
                </ul>
            </div>
<%  } %>
    </div>
</asp:Content>