<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<LinkMe.Domain.Users.Members.JobAds.MemberJobAdView>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>

<%  var hasLogo = Model.LogoId != null && Model.Features.IsFlagSet(JobAdFeatures.Logo); %>
<div class="summary<%= hasLogo ? " with-logo" : "" %>">
    <div class="bg">
        <div class="logo-item" <%= hasLogo ? "" : "style=\"display:none;\"" %>>
            <img src="<%= hasLogo ? JobAdsRoutes.Logo.GenerateUrl(new { jobAdId = Model.Id }) : null %>" class="logo" />
        </div>
        <div class="item">
            <div class="title">Location</div>
<%  var location = Model.Description.Location == null ? null : Model.Description.Location.ToString();
    if (string.IsNullOrEmpty(location) && Model.Description.Location != null)
        location = Model.Description.Location.Country.Name; %>
            <div class="desc"><%= location %></div>
        </div>
        <div class="item">
            <div class="title">Salary</div>
            <div class="desc"><%= Model.Description.Salary.GetJobAdDisplayText()%></div>
        </div>
        <div class="item">
            <div class="title">Industry</div>
            <div class="desc"><%= Model.Description.Industries == null ? "" : string.Join(", ", (from i in Model.Description.Industries select i.Name).ToArray())%></div>
        </div>
        <div class="item">
            <div class="title">Reference no.</div>
            <div class="desc"><%= Model.Integration.ExternalReferenceId %></div>
        </div>
        <div class="item">
            <div class="title">Contact</div>
            <div class="desc"><%= Model.ContactDetails.GetContactDetailsDisplayText() %></div>
        </div>
        <div class="item">
            <div class="title">Date listed</div>
            <div class="desc"><%= Model.CreatedTime.ToString("dd MMMM yyyy")%> (<%= Model.GetPostedDisplayText() %>)</div>
        </div>
    </div>
<%  if (!Model.Description.BulletPoints.IsNullOrEmpty())
    { %>
    <div class="item bulletpoints">
        <ul class="bulletpoints">
<%      foreach (var bp in Model.Description.BulletPoints)
        { %>
            <li><div class="point"></div><%= bp%></li>
<%      } %>
        </ul>
    </div>
<%  } %>
</div>
<div class="content"><%= Model.Description.Content.GetContentDisplayHtml(Context.Request.IsSecureConnection)%></div>
<%  if (Model.Status == JobAdStatus.Closed)
    { %>
<div class="togglecontent">
    <div class="text">This job is no longer advertised</div>
    <a class="togglelink collapse" href="javascript:void(0);">Show job description</a>
    <div class="togglebutton collapse"></div>
</div>
<%  } %>
