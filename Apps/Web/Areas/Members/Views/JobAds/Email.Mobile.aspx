<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Domain.Contacts" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceEmailJobAd) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceEmailJobAd) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="emailjobad">
        <div class="title">Email <span><%= Model.JobAd.Title %></span> to yourself or a friend</div>
        <div class="desc">Use commas to email this job to more than one person.</div>
        <%  var member = CurrentMember;
            var fullName = member == null ? null : member.FullName;
            var emailAddress = member == null ? null : member.GetBestEmailAddress().Address; %>
	    <%= Html.TextBoxField("FromName", fullName)
            .WithLabel("Your name").WithIsReadOnly(!string.IsNullOrEmpty(fullName))%>
	    <%= Html.TextBoxField("FromEmailAddress", emailAddress)
            .WithLabel("Your e-mail").WithIsReadOnly(!string.IsNullOrEmpty(emailAddress))%>
	    <%= Html.TextBoxField("ToName", "")
            .WithLabel("Your friend's name(s)") %>
	    <%= Html.TextBoxField("ToEmailAddress", "")
            .WithLabel("Your friend's email address(es)") %>
        <div class="errorinfo">
            Send failed. Please try again
            <ul>
            </ul>
        </div>
        <div class="buttons">
            <div class="wrapper"><div class="button send" data-url="<%= Html.RouteRefUrl(JobAdsRoutes.ApiEmailJobAds) %>" data-succurl="<%= Html.RouteRefUrl(JobAdsRoutes.EmailJobAdSent, new { jobAdId = Model.JobAd.Id }) %>" id="<%= Model.JobAd.Id %>">SEND</div></div>
            <div class="wrapper"><a class="button cancel" href="<%= Model.JobAd.GenerateJobAdUrl() %>">CANCEL</a></div>
        </div>
    </div>
</asp:Content>