<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Domain" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceJobAd) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.DeviceJobAd) %>
    </mvc:RegisterJavaScripts>

    <script type="text/javascript">
        var criteria = {};

<%  if (Model.CurrentSearch != null && Model.CurrentSearch.Criteria.JobTypes != JobTypes.All)
    { %>
        criteria["<%= JobAdSearchCriteriaKeys.JobTypes %>"] = "<%= Model.CurrentSearch.Criteria.JobTypes %>";
<%  }
    if (ViewData.ModelState.ContainsKey(ModelStateKeys.Confirmation) && ViewData.ModelState[ModelStateKeys.Confirmation].Errors.Count > 0 && "SHOW ADDED NOTIFICATION".Equals(ViewData.ModelState[ModelStateKeys.Confirmation].Errors[0].ErrorMessage))
    { %>
        var showAddedNotification = true;
<%  }
    else
    { %>
        var showAddedNotification = false;
<%  } %>

        (function($) {
            $(document).ready(function() {
                linkme.members.jobads.api.setUrls({
                    addJobAdsToMobileFolder: "<%= JobAdsRoutes.ApiAddJobAdsToMobileFolder.GenerateUrl() %>",
                    removeJobAdsFromMobileFolder: "<%= JobAdsRoutes.ApiRemoveJobAdsFromMobileFolder.GenerateUrl() %>"
                });
            });
        })(jQuery);
            
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<%  Model.Status.CurrentIndex = 1;
    Model.Status.TotalCount = 1; %>

    <div class="notification added">
        <div class="title">Your job has been saved.</div>
        <div class="desc">You can find your saved jobs here</div>
    </div>
    <div class="jobad">
<%  if (Model.JobAd.Status == JobAdStatus.Closed)
    { %>
        <div class="text closed top">Sorry, this job ad is closed.</div>
<%  }
    if (Model.CurrentSearch != null)
    { %>
        <a class="link back" href="<%= Html.RouteRefUrl(SearchRoutes.Results) %>"><div class="icon back"></div> Back to search results</a>
        <div class="navigation">
            <div class="title"><span class="criteriahtml"><%= Model.CurrentSearch.Criteria == null ? null : Model.CurrentSearch.Criteria.GetCriteriaHtml() %></span></div>
        </div>
<%  } %>
        <div class="header <%= Model.JobAd.Status == JobAdStatus.Closed ? "closed" : "" %>">
<%  if (Model.JobAd.Status != JobAdStatus.Closed)
    { %>
                <%= Html.RouteRefLink("APPLY NOW", JobAdsRoutes.JobAdApply, new { jobAdId = Model.JobAd.Id }, new { @class = "button applynow" }) %>
                <a class="button forward" href="<%= Html.RouteRefUrl(JobAdsRoutes.EmailJobAd, new { jobAdId = Model.JobAd.Id }) %>"></a>
                <div class="button saved <%= CurrentMember == null ? "notloggedin" : "" %> <%= Model.JobAd.Applicant.IsInMobileFolder ? "active" : "" %>" data-url="<%= CurrentMember == null ? Html.RouteRefUrl(JobAdsRoutes.AddJobAdToMobileFolder, new { backTo = "JobAd", jobAdId = Model.JobAd.Id, returnUrl = ViewData.GetClientUrl() }) : Html.RouteRefUrl(JobAdsRoutes.ApiAddJobAdsToMobileFolder) %>" id="<%= Model.JobAd.Id %>"></div>
<%  } %>
        </div>
        <div class="bg">
            <div class="titleline">
                <div class="icon new <%= Model.JobAd.IsNew() ? "active" : "" %>"></div>
                <div class="icon jobtype" jobtypes="<%= Model.JobAd.Description.JobTypes %>"></div>
                <div class="title"><%= Model.JobAd.Title %></div>
            </div>
            <div class="company"><%= Model.JobAd.ContactDetails.GetContactDetailsDisplayText() %></div>
<%  var location = Model.JobAd.Description.Location == null ? null : Model.JobAd.Description.Location.ToString();
    if (string.IsNullOrEmpty(location) && Model.JobAd.Description.Location != null)
        location = Model.JobAd.Description.Location.Country.Name; %>
            <div class="location"><%= location %></div>
<%  var salary = Model.JobAd.Description.Salary.GetJobAdDisplayText(); %>
            <div class="salary"><%= "Not specified".Equals(salary) ? "Salary not specified" : salary %></div>
            <div class="date">Posted <%= Model.JobAd.CreatedTime.GetDateAgoText() %></div>
            <div class="divider"></div>
            <div class="content"><%= Model.JobAd.Description.Content.GetContentDisplayHtml(Context.Request.IsSecureConnection) %></div>
<%  if (Model.JobAd.Status == JobAdStatus.Closed)
    { %>
                <div class="text closed bottom">This job ad is closed</div>
<%  }
    else
    { %>
                <%= Html.RouteRefLink("APPLY NOW", JobAdsRoutes.JobAdApply, new { jobAdId = Model.JobAd.Id }, new { @class = "button applynow" }) %>
<%  } %>
        </div>
        <div class="bottombar"></div>
<%  if (Model.JobAd.Status == JobAdStatus.Closed && Model.SuggestedJobs.Count > 0)
    { %>
        <div class="similarjobs">
            <div class="title">Similar jobs to this one</div>
<%      for (var index = 0; index < Model.SuggestedJobs.Count && index < 2; index++)
        {
            var jobAd = Model.SuggestedJobs[index]; %>
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

        <div class="clearer"></div>
    
        <% Html.RenderPartial("GoogleHorizontalAds"); %>

    </div>
    
</asp:Content>