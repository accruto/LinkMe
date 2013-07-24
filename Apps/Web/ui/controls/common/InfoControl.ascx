<%@ Import namespace="LinkMe.Common"%>
<%@ Import namespace="LinkMe.Web.Employers"%>
<%@ Import namespace="LinkMe.Web"%>
<%@ Import namespace="LinkMe.Web.UI"%>
<%@ Import namespace="LinkMe.Web.UI.Registered.Networkers"%>
<%@ Import namespace="LinkMe.Web.UI.Unregistered"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="InfoControl.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.InfoControl" %>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<asp:PlaceHolder ID="notimpl" runat="server" Visible="false">
    <p>Sidebar component not yet implemented!</p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="interim_member_login" runat="server" Visible="false">
    <p>
        Have employers discover your resume on LinkMe.
    </p>
    <p style="font-size:120%; font-weight:bold; text-align:center">
        <a href="<%= AccountsRoutes.Join.GenerateUrl() %>">Join now for free</a>
    </p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="interim_employer_login" runat="server" Visible="false">
    <p style="font-size:120%; font-weight:bold; text-align:center">
        <a href="<%= HttpContext.Current.GetEmployerLoginUrl() %>">Log in to LinkMe</a>
    </p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="what_is_shown_to_job_hunters" runat="server" Visible="false">
    <p>All fields are shown unless otherwise stated. </p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="job_ad_title_advice" runat="server" Visible="false">
	<p>The first rule of copy writing is: &ldquo;If you can't say it great, say it 
		straight&rdquo;. In-house titles are often ambiguous; make yours
		clear and concise. Candidates skim through job titles, so be upfront 
		about the role or position.
	</p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="job_ad_writing_advice" runat="server" Visible="false">
	<p>See it from the candidate's point of view. Don't assume they know
		anything about your company; instead, write a couple of sentences about it and
		highlight any social opportunities and work challenges.
	</p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="job_ad_salary_advice" runat="server" Visible="false">
	<p>The salary indicates the job level to avoid time-wasting mismatches.</p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="employer_courtesy" runat="server" Visible="false">
    <p class="attention-grabber">
        <a href="<%= LinkMe.Web.Areas.Employers.Routes.SearchRoutes.Search.GenerateUrl() %>" title="Find Staff" name="employer_courtesy/FindCandidatesNow">Find candidates now</a>
    </p>
		
    <ul>
        <li>A great range of candidates </li>
        <li>Candidate recommendations for your job </li>
    </ul>		
    <a class="affordance-link" href="<%= LinkMe.Web.Areas.Employers.Routes.HomeRoutes.Home.GenerateUrl() %>" title="Employers" name="employer_courtesy/EmployerSite">Employer site</a>
</asp:PlaceHolder>

<asp:PlaceHolder ID="join_privacy" runat="server" Visible="false">
	<p>
		<img src="<%= new ApplicationUrl("~/ui/img/padlock.gif") %>" alt="Padlock" align="right" />
		We will only send you LinkMe related content. See our
		<a href="<%= SupportRoutes.Privacy.GenerateUrl() %>">privacy statement</a> for details.
	</p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="join_details" runat="server" Visible="false">
	<ul>
		<li>
			It helps recruiters and employers contact you about the right job.
		</li>
		<li>
			You can get notified (via email) when your ideal job is advertised.
		</li>
	</ul>
</asp:PlaceHolder>

<asp:PlaceHolder ID="member_join" runat="server" Visible="false">
    <p>
        Looking for a job?
    </p>
    <ul>
        <li>Post your resume on LinkMe and have jobs find you</li>
        <li>Create a personal profile for friends and colleagues</li>
        <li>Stay in touch with work colleagues and friends</li>
        <li>Keep full control of your privacy</li>
        <li>Find your friends on LinkMe</li>
    </ul>
</asp:PlaceHolder>

<asp:PlaceHolder ID="employer_join" runat="server" Visible="false">
    <p>
        Join now and benefit with our recruitment solutions:
    </p>
    <ul>
        <li>Fresh database of resumes growing every day</li>
        <li>Search resumes for free</li>
        <li>Save time - don't wait for responses to job ads, find candidates instantly</li>
        <li>New better search options - be general or specific in your hunt for the right person</li>
        <li>Cost effective packages to suit your recruitment needs and save you money</li>
    </ul>
</asp:PlaceHolder>

