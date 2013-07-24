<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Home"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.EmployerHome)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    
    <% Html.RenderPartial("LinkedInAuth"); %>

    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.EmployerHome) %>
        <%= Html.JavaScript(JavaScripts.SwfObject) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <div class="login">
        <div class="left">
			<a class="joinbutton" href="<%= Html.RouteRefUrl(AccountsRoutes.Join) %>" title="Don't have an account? Join as an employer today">Don't have an account? Join as an employer today</a>
		</div>
        <div class="right">
<%  using (Html.RenderForm(Context.GetClientUrl(true), "LoginForm"))
    { %>
			    <%= Html.TextBoxField(Model.Login, l => l.LoginId).WithLabel("").WithCssPrefix("username").WithAttribute("data-watermark", "Username/Email address")%>
			    <%= Html.PasswordField(Model.Login, l => l.Password).WithLabel("").WithCssPrefix("password").WithAttribute("data-watermark", "Password")%>
			    <div class="loginbutton"></div>
			    <div class="login-error">
			        <ul>
<%      if (Model.Login != null && !ViewData.ModelState.IsValid)
        {
            foreach (var message in ViewData.ModelState.GetErrorMessages())
            { %>
                        <li><%= message%></li>
<%          }
        } %>
			        </ul>
			    </div>
                <div class="linkedin">
                    <script type="in/Login"></script>
                </div>
			    <%= Html.CheckBoxField(Model.Login, l => l.RememberMe).WithLabelOnRight("Remember me")%>
			    <div class="forgotpwd"><%= Html.RouteRefLink("Forgot password?", LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.NewPassword, new { userType = UserType.Employer }, new { @class = "forgotpassword_link link", id = "ForgotPassword" })%></div>
<%  } %>
		</div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="videosection">
        <div class="top-bar"></div>
        <div class="bg">
            <div class="videotitle">What is LinkMe?</div>
            <div class="video">
                <div id="whatislinkme" url="https://www.youtube.com/v/7H8RrYPDpJo?version=3&amp;hl=en_US&amp;rel=0&amp;enablejsapi=1&amp;playerapiid=whatislinkme&amp;fs=1">
                    You need Flash player 8+ and JavaScript enabled to view this video.
                </div>
            </div>
            <div class="videotitle">Why use LinkMe?</div>
            <div class="video">
                <div id="whyuselinkme" url="https://www.youtube.com/v/JdFx3Qx93h8?version=3&amp;hl=en_US&amp;rel=0&amp;enablejsapi=1&amp;playerapiid=whyuselinkme&amp;fs=1">
                    You need Flash player 8+ and JavaScript enabled to view this video.
                </div>
            </div>
        </div>
        <div class="bottom-bar"></div>
    </div>
    
    <div class="right-section">
        <div class="section search">
            <div class="top-bar"></div>
            <div class="bg">
                <div class="searchtext">
                    <span>Search for candidates in our database</span>
                </div>
                <a class="learnmore" target="_blank" href="<%= Html.RouteRefUrl(HomeRoutes.Features) %>" title="Learn more on how to search resumes">Learn more on how to search resumes</a>
                <% using (Html.RenderForm(Html.RouteRefUrl(SearchRoutes.Search)))
                   {
                       Html.RenderPartial("KeywordLocationSearch", Model.Reference.KeywordLocationSearch);
                       Html.RenderPartial("SearchTipsOverlay", null);
                   }
                %>
            </div>
            <div class="bottom-bar"></div>
        </div>
        <div class="section jobad">
            <div class="top-bar"></div>
            <div class="bg">
				<div class="fg"></div>
				<a class="explore" href="<%= Html.RouteRefUrl(JobAdsRoutes.JobAd) %>" title="Start posting your own job ads">Start posting your own job ads</a>
				<a class="learnmore" target="_blank" href="<%= Html.RouteRefUrl(HomeRoutes.Features) %>" title="Learn more on how to post job ads">Learn more on how to post job ads</a>
				<div class="title">Post job ads to our network of job boards</div>
            </div>
            <div class="bottom-bar"></div>
        </div>
        <div class="section iphone">
            <div class="top-bar"></div>
            <div class="bg">
			    <div class="fg"></div>
			    <a class="appstore" target="_blank" href="<%= Model.AppStoreUrl %>" title="The LinkMe official iPhone app available on the Apple App Store">The LinkMe official iPhone app available on the Apple App Store</a>
			    <%= Html.RouteRefLink("Learn more", HomeRoutes.CandidateConnect, null, new { @class = "learnmore" })%>
			    <div class="title">Candidate Connect</div>
            </div>
            <div class="bottom-bar"></div>
			<div class="comments"></div>
        </div>
    </div>
    
</asp:Content>

