<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceApply) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceApply) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="apply">
        <div class="title">Application for: <span class="blue"><%= Model.JobAd.Title %></span></div>
        <%
            if (Model.JobAd.Processing == JobAdProcessing.AppliedForExternally)
                Html.RenderPartial("AppliedForExternallyApply", Model);
            else if (Model.JobAd.Processing == JobAdProcessing.ManagedInternally)
                Html.RenderPartial("ManagedInternallyApply", Model);
            else if (Model.JobAd.Processing == JobAdProcessing.ManagedExternally)
                Html.RenderPartial("ManagedExternallyApply", Model);
        %>
    </div>
</asp:Content>