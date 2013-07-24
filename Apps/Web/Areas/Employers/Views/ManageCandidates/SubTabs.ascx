<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ManageCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>

<%  var status = Model.ApplicantStatus == ApplicantStatus.NotSubmitted || Model.ApplicantStatus == ApplicantStatus.Removed
    	? ApplicantStatus.New
        : Model.ApplicantStatus; %>
	    
<div class="categories-tab-container">
	<div class="categories-tab new-category-tab <%= status == ApplicantStatus.New ? "active" : "" %>" categoryName="<%= ApplicantStatus.New.ToString("G") %>"><span>New (<span class="count new-candidates-count"><%= Model.JobAd.ApplicantCounts.New %></span>)</span></div>
	<div class="categories-tab shortlisted-category-tab <%= status == ApplicantStatus.Shortlisted ? "active" : "" %>" categoryName="<%= ApplicantStatus.Shortlisted.ToString("G") %>"><span>Shortlisted (<span class="count shortlisted-candidates-count"><%= Model.JobAd.ApplicantCounts.ShortListed %></span>)</span></div>
	<div class="categories-tab rejected-category-tab <%= status == ApplicantStatus.Rejected ? "active" : "" %>" categoryName="<%= ApplicantStatus.Rejected.ToString("G") %>"><span>Rejected (<span class="count rejected-candidates-count"><%= Model.JobAd.ApplicantCounts.Rejected %></span>)</span></div>
	<span class="category-indicator" currentCategory="<%= status.ToString("G") %>"></span>
	<span class="categories-description">
		<span class="new-category-desc active" categoryName="<%= ApplicantStatus.New.ToString("G") %>">These candidates have applied directly for the role</span>
		<span class="shortlisted-category-desc" categoryName="<%= ApplicantStatus.Shortlisted.ToString("G") %>">You have added these candidates to the role</span>
		<span class="rejected-category-desc" categoryName="<%= ApplicantStatus.Rejected.ToString("G") %>">You have rejected these candidates</span>
	</span>
</div>