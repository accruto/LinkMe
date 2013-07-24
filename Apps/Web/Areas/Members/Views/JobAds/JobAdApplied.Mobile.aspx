<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceApplied) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceApplied) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<%  var contact = Model.JobAd.ContactDetails.GetContactDetailsDisplayText(); %>
    <div class="applied">
        <div class="title"><div class="icon tick"></div><%= Model.JobAd.Processing == JobAdProcessing.ManagedInternally ? "Your application has been submitted - good luck!" : "We hope your application is successful" %></div>
        <div class="contact">Please contact <b><%= string.IsNullOrEmpty(contact) ? "the advertiser" : contact %></b> directly if you have any questions about the progress of your application.</div>
<%  if (Model.SuggestedJobs.Count > 0)
    { %>
            <div class="suggestedjobs">
                <div class="title">You might also be interested in:</div>
<%      for (var i = 0; i < Model.SuggestedJobs.Count && i < 2; i++)
        {
            var jobAd = Model.SuggestedJobs[i]; %>
                    <div class="row <%= jobAd.Features.IsFlagSet(JobAdFeatures.Highlight) ? "featured" : "" %>" id="<%= jobAd.Id %>" data-url="<%= jobAd.GenerateJobAdUrl() %>">
                        <div class="titleline">
                            <div class="icon new <%= jobAd.IsNew() ? "active" : "" %>"></div>
                            <div class="icon jobtype" jobtypes="<%= jobAd.Description.JobTypes %>"></div>
                            <div class="title"><%= jobAd.Title%></div>
                        </div>
                        <div class="company"><%= jobAd.ContactDetails.GetContactDetailsDisplayText()%></div>
                        <div class="location"><%= jobAd.Description.Location%></div>
                        <div class="salary"><%= jobAd.Description.Salary.GetJobAdDisplayText()%></div>
                        <div class="date">Posted <%= jobAd.CreatedTime.GetDateAgoText()%></div>
                        <div class="featured"></div>
                    </div>
<%      } %>
            </div>
<%  } %>
    </div>
</asp:Content>