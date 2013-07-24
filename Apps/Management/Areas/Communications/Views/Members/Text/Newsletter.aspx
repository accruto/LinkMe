<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Newsletter" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>

<%= DateTime.Now.ToString("MMMM yyyy") %>

More than <%= (Model.TotalJobAds / 1000) + ",000" %> jobs added this month.

Hi <%= Model.Member.FirstName %>

Welcome to the September edition of LinkMe.com.au’s newsletter.

This month we consider how your online profile can affect your chances of finding a job at <%= Html.TinyUrl(false, "~/members/resources/article/Career-management-Manage-your-career-Social-Media-Your-Online-Footprint/5844bc0b-e8e8-4f2d-bebd-8d455dd3d077")%>. Paul Jury, MD Recruitment, Australia & New Zealand, at Talent2 http://www.talent2.com gives us his perspective and has some great tips on how to avoid the pitfalls of social media.

As a reminder, your LinkMe profile is your passport to a new career. We encourage you to review and update your profile at <%= Html.TinyUrl(true, "~/members/profile")%> on a regular basis.


QUESTION OF THE WEEK

Does your resumePass or Fail?

Does your professional resume stand out from the competition? Will your resume meet the expectations of employers or hiring managers or will it end up in the... Read more	at <%= Html.TinyUrl(false, "~/members/resources/answeredQuestion/Resume-writing-Resume-writing-tips-Does-your-resume-Pass-or-Fail/c531330b-f8ed-4998-bd1a-af59bffcd7e4")%>

Need help with your career?	
Ask us and receive expert advice at <%= Html.TinyUrl(false, "~/members/resources") %>

INDUSTRY TRENDS

Social media: Your online footprint

by Paul Jury, MD Recruitment, Australia & New Zealand, at Talent2

We’ve all done it… made a controversial 'wall' comment, written a cheeky blog post or suffered as friends tag questionable photos of a big night out. It's a bit of harmless fun... Read more at <%= Html.TinyUrl(false, "~/members/resources/article/Career-management-Manage-your-career-Social-Media-Your-Online-Footprint/5844bc0b-e8e8-4f2d-bebd-8d455dd3d077")%>

LINKME NEWS

LinkMe launches mobile site

You can now search and apply for jobs on LinkMe.com.au direct from your mobile device. Visit http://www.linkme.com.au today using your smart phone and see how easy it is.

YOUR PROFILE SUMMARY

Work status: <%= Model.Candidate.Status.GetDisplayText() %>
Desired job: <%= Model.Candidate.DesiredJobTitle %>
Primary phone: <%= Model.Member.GetPrimaryPhoneNumber() != null ? Model.Member.GetPrimaryPhoneNumber().Number : "" %>

Your profile has been viewed <%= Model.TotalViewed %> times by employers and recruiters this month

Profile completion: <%= Model.ProfilePercentComplete %>%
Your profile is <%= Model.ProfilePercentComplete == 100 ? "" : "in"%>complete.

<% if (Model.ProfilePercentComplete == 100) { %>
Changed your contact details recently? Started a new job or completed a course?
Update your profile at <%= Html.TinyUrl(true, "~/members/profile")%> to ensure employers will contact you directly
<% } else { %>
Please review your profile at <%= Html.TinyUrl(true, "~/members/profile")%> to increase your chances of being found by employers.
<% } %>


YOUR PERSONALISED JOBS

<% foreach (var j in Model.SuggestedJobs) {
	if (j.Features.IsFlagSet(JobAdFeatures.Highlight)) { %>

Featured job: <%= j.Title %> at <%= Html.TinyUrl(true, j.GenerateUrl()) %>
<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
	if (string.IsNullOrEmpty(location) && j.Description.Location != null)
		location = j.Description.Location.Country.Name; %>
<%= location %>
Posted <%= j.CreatedTime.ToString("dd/MM/yyyy")%>
<% } else { %>
<%= j.Title %> at <%= Html.TinyUrl(true, j.GenerateUrl()) %>
<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
	if (string.IsNullOrEmpty(location) && j.Description.Location != null)
		location = j.Description.Location.Country.Name; %>
<%= location %>
Posted <%= j.CreatedTime.ToString("dd/MM/yyyy")%>
<% } %>
<% } %>

Edit your resume at <%= Html.TinyUrl(true, "~/members/profile")%> to improve the quality of suggested jobs.

CAREER AND RESUME RESOURCES

Your questions answered by career experts
Advice about job hunting and job interviews
Resume and cover letter hints and tips
Insight about the Australian job market
Advice specific to students and grads

CONNECT TO LINKME

Like LinkMe on Facebook at <%= Html.TinyUrl(true, "http://www.facebook.com/LinkMeAus") %>
Follow LinkMe on Twitter at <%= Html.TinyUrl(true, "http://www.twitter.com/LinkMeJobs") %>

Connect to LinkMe for the latest news, advice and hints on Australian jobs and managing your career.

You have received this email because you have registered at LinkMe.com.au. You can edit your settings at <%= Html.TinyUrl(true, "~/members/settings") %> to modify the frequency of emails or unsubscribe at <%= Html.TinyUrl(false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category)%>.