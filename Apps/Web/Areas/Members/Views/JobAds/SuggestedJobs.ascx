<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<IList<MemberJobAdView>>" %>
<%@ Import Namespace="LinkMe.Domain.Users.Members.JobAds"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds" %>

<%
    var i = 0;
    foreach (var jobAd in Model) {
        var jobTypes = new List<JobTypes>();
        var primaryJobType = JobTypes.None;
        foreach (var t in new List<JobTypes> { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare })
            if ((jobAd.Description.JobTypes & t) == t) jobTypes.Add(t);
        if (jobTypes.Count > 0) primaryJobType = jobTypes[0];
%>
	<div class="suggestedjob" id="<%= jobAd.Id %>" url="<%= jobAd.GenerateJobAdUrl() %>">
		<% if (i > 0) { %>
		    <div class="divider"></div>
		<% } %>
		<div class="bg">
		    <% if (jobAd.IsNew())
		       { %>
		    <div class="isnew"></div>
            <% } %>
		    <div class="jobtype <%= primaryJobType %>"></div>
		    <div class="title" title="<%= jobAd.Title %>"><%= jobAd.Title %></div>
		    <div class="location" title="<%= jobAd.Description.Location %>"><%= jobAd.Description.Location%></div>
		    <div class="postdate" title="<%= jobAd.CreatedTime.GetJobPostedDateDisplayText()%>"><%= jobAd.CreatedTime.GetJobPostedDateDisplayText()%></div>
		</div>
	</div>
<%
        i++;
    }
%>