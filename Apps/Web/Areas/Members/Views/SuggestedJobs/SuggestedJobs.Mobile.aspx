<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SuggestedJobsListMobileModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets ID="RegisterStyleSheets1" runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceMobileFolder) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts ID="RegisterJavaScripts1" runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceJobAdItem)%>
    </mvc:RegisterJavaScripts>
    <script type="text/javascript">
        var criteria = {};
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="mobilefolder">
        <div class="title">Suggested jobs</div>
<%  if (Model.Results.JobAds.Count > 0)
    {
        foreach (var j in Model.Results.JobAdIds)
            Html.RenderPartial("JobAdListView", Model.Results.JobAds[j]);
    }
    else
    { %>
            <div class="noresults">
                <div class="desc">Unfortunately, there aren't any suggested jobs for you at the moment.<br /><br />Update your profile on a computer to help us to find you suitable jobs.</div>
                <ul>
                    <li><%= Html.RouteRefLink("Start a new job search", HomeRoutes.Home) %></li>
                </ul>
            </div>
<%  } %>
    </div>
</asp:Content>