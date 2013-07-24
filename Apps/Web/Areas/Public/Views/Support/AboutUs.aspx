<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts ID="RegisterJavaScripts1" runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1>About LinkMe</h1>
    </div>
    
	<div class="first_section section forms_v2">
		<div class="section-content">

            <ul class="terms-links">
                <li><%= Html.RouteRefLink("General terms and conditions", SupportRoutes.Terms)%></li>
                <li><%= Html.RouteRefLink("Member terms and conditions", SupportRoutes.MemberTerms)%></li>
                <li><%= Html.RouteRefLink("Employer terms and conditions", SupportRoutes.EmployerTerms)%></li>
                <li><%= Html.RouteRefLink("Privacy statement", SupportRoutes.Privacy)%></li>
                <li><%= Html.RouteRefLink("Careers at LinkMe", SupportRoutes.Careers)%></li>
            </ul>
            <br />
            
            <p>
                LinkMe takes the traditional recruiting model and shakes things up.
            </p>
            <p>
                For job seekers and candidates, uploading your resume to LinkMe allows employers and recruiters to match you to jobs you didn't even know were out there. You can also search more than 30,000 jobs on our job board every day.
            </p>
            <p>
                For employers and recruiters, LinkMe puts you in the driver's seat. There's no waiting for resumes to arrive or sorting through hundreds of unsuitable resumes. It takes less than 5 minutes to search and shortlist your ideal candidates. Then call them or email them directly. See? No waiting!
            </p>
            <p>
                LinkMe for job hunters:
            </p>
            <ul>
                <li>Don't Seek. Be Sought. Be found by employers and recruiters and let jobs come to you. Less than five minutes to upload your resume and <%= Html.RouteRefLink("create your LinkMe profile", JoinRoutes.Join)%></li>
                <li>Make your job search effortless. Sit back and let the jobs come to you through LinkMe's job matching email alerts</li>
                <li>Take control and simplify your job hunting by letting LinkMe store all your resumes and job applications online</li>
                <li>Choose the jobs that really fit you from thousands of jobs listed. Why not work close to home or in a <%= Html.RouteRefLink("job that you love", JobAdsRoutes.BrowseJobAds) %>?</li>
                <li>Get advice job hunting, resume and career straight from the experts. <%= Html.RouteRefLink("Ask your question today", ResourcesRoutes.Resources) %></li>
            </ul>
            <p>
                LinkMe for the happily employed:
            </p>
            <ul>
                <li>Be the best you can be. Get that promotion, be a star performer, love your job, get that pay rise, take control of your work life balance with our online videos and advice. <%= Html.RouteRefLink("Ask your question today", ResourcesRoutes.Resources) %></li>
                <li>Keep your work options open, without commitment by making your resume anonymously searchable in the LinkMe Database. It still feels good to get calls from time to time about new and exciting opportunities</li>
                <li>Have peace of mind knowing emails, phone numbers, and names of your contacts are securely stored online</li>
                <li>Don't lose track of your resume again. LinkMe is a trusted place to store your resume for the life of your career</li>
            </ul>
            <p>
                LinkMe for employers:
            </p>
            <ul>
                <li>Save time recruiting, through instant access to a large pool of quality candidates, and using "Candidate Connect" – LinkMe's iPhone/ iPad app which puts all of LinkMe's candidates at your fingertips</li>
                <li>Target candidates who are immediately available, indigenous, willing to relocate, and many more criteria</li>
                <li>Save money by reducing the need to advertise externally</li>
                <li>Access quality passive candidates not found through traditional job boards – even those working for your competitors</li>
                <li>Take control of your recruiting process: track applicants, list jobs automatically on your website, store resumes</li>
                <li>Receive alerts when hard-to-find candidates join LinkMe or update their resume</li>
                <li>Call 1800-LINKME for more information</li>
            </ul>
            <p>
                 LinkMe for recruiters:
            </p>
            <ul>
                <li>Save time sourcing candidates through instant access to a large pool of quality candidates, and using "Candidate Connect" – LinkMe's iPhone/ iPad app which puts all of LinkMe's candidates at your fingertips</li>
                <li>Target candidates who are immediately available, indigenous, willing to relocate, and many more criteria</li>
                <li>Generate additional revenue by accessing hard to find candidates - including those working for your clients' competitors</li>
                <li>Access quality passive candidates not found through traditional job boards</li>
                <li>Receive alerts when hard-to-find candidates join LinkMe or update their resume</li>
                <li>Simplify your candidate sourcing processes through direct integration into your systems</li>
                <li>Call 1800-LINKME for more information</li>
            </ul>
            <p>
                Conducting your talent search on LinkMe:
            </p>
            <p>
                LinkMe offers two main ways for employers and recruiters to get in touch with suitable candidates:
            </p>
            <p>
                First, LinkMe has a database containing hundreds of thousands of resumes from Australian candidates (645,000+ as of March 2012). Our advanced search technology means employers and recruiters can quickly identify and shortlist candidates based on skills, experience, education and location. Unlike traditional sourcing methods, employers and recruiters can target both candidates who are actively looking for a job, and also those passive candidates who are highly sought after but may not be actively looking for a job right now.
            </p>
            <p>
                Second, LinkMe also offers a job board where employers and recruiters can tell candidates about their opportunities. LinkMe distributes these ads across our network of over 100 partner sites, so each ad gets substantial online exposure. Candidates conduct over 20,000 job searches each and every day on LinkMe's job board
            </p>
	    </div>
    </div>

</asp:Content>

