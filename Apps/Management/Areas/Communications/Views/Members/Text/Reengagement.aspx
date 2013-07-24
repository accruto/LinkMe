<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Reengagement" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>

March 2012

Over <%= (Model.TotalContactsLastMonth / 1000) + ",000" %> candidates were contacted about jobs last month

Hi <%= Model.Member.FirstName %>,

You joined LinkMe.com.au a while back. It's been too long since we've heard from you, <% if (Model.JobSearch != null) { %> and we wanted to let you know that there are now <%= Model.JobSearch.TotalMatches.ToString("N0") %> jobs matching "<%= Model.JobSearch.Description %>".<% } else { %>so we thought we'd remind you that we're still here helping people find their perfect job. How about we help you too?<% } %>

Search at <%= GetActivationUrl(Html, Model.ActivationCode, false, "~/search/jobs")%> over 30,000 jobs advertisements available on our job board today

OR

Login at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/login")%> to update your LinkMe profile (which you created when you joined) and let employers search for you

Even if you're not actively looking for a job, you can update your availability status at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile")%> and let employers know that you're open to discussing suitable job opportunities. You never know what you might be missing.

BE FOUND BY EMPLOYERS TODAY

Did you know that employers contacted <%= Model.TotalContactsLastWeek.ToString("N0") %> LinkMe candidates last week?

You have already set up a basic LinkMe profile, so why not update your profile at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile")%> and add your name to the list.

Good luck with your job search,

The LinkMe Team


YOUR PROFILE SUMMARY

Work status: <%= Model.Candidate.Status.GetDisplayText() %>
Desired job: <%= Model.Candidate.DesiredJobTitle %>
Primary phone: <%= Model.Member.GetPrimaryPhoneNumber() != null ? Model.Member.GetPrimaryPhoneNumber().Number : "" %>

Your profile has been viewed <%= Model.TotalViewed %> times by employers and recruiters this month

Profile completion: <%= Model.ProfilePercentComplete %>%
Your profile is <%= Model.ProfilePercentComplete == 100 ? "" : "in"%>complete.

Get expert advice on improving your resume: Ask us at <%= GetActivationUrl(Html, Model.ActivationCode, false, "~/members/resources") %> today
<% if (Model.ProfilePercentComplete == 100) { %>
Changed your contact details recently? Started a new job or completed a course?
Update your profile at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile")%> to ensure employers will contact you directly
<% } else { %>
Please review your profile at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile")%> to increase your chances of being found by employers.
<% } %>


YOUR PERSONALISED JOBS

<% foreach (var j in Model.SuggestedJobs) {
	if (j.Features.IsFlagSet(JobAdFeatures.Highlight)) { %>

Featured job: <%= j.Title %> at <%= GetActivationUrl(Html, Model.ActivationCode, true, j.GenerateUrl()) %>
<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
	if (string.IsNullOrEmpty(location) && j.Description.Location != null)
		location = j.Description.Location.Country.Name; %>
<%= location %>
Posted <%= j.CreatedTime.ToString("dd/MM/yyyy")%>
<% } else { %>
<%= j.Title %> at <%= GetActivationUrl(Html, Model.ActivationCode, true, j.GenerateUrl()) %>
<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
	if (string.IsNullOrEmpty(location) && j.Description.Location != null)
		location = j.Description.Location.Country.Name; %>
<%= location %>
Posted <%= j.CreatedTime.ToString("dd/MM/yyyy")%>
<% } %>
<% } %>

Edit your resume at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile")%> to improve the quality of suggested jobs.

You have received this email because you have registered at LinkMe.com.au. You can edit your settings at <%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/settings") %> to modify the frequency of emails or to unsubscribe at <%= GetActivationUrl(Html, Model.ActivationCode, false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category)%>.