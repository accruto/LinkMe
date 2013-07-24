<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<div id="footer-container">
    <div class="footer-holder">
        <div class="footer-links-holder">
            <div class="left-section">
<%  if (ViewData.GetActiveUserType() == UserType.Employer)
    { %>
                <div class="logo" onclick="javascript:loadPage('<%= LinkMe.Web.Areas.Employers.Routes.HomeRoutes.Home.GenerateUrl() %>');"></div>
<%  }
    else
    { %>
                <div class="logo" onclick="javascript:loadPage('<%= HomeRoutes.Home.GenerateUrl() %>');"></div>
<%  } %>                
                <div class="copyright">&copy; Copyright LinkMe Pty Ltd</div>
            </div>
            <div class="right-section">
                <div class="main-links <% if (CurrentRegisteredUser == null) { %>not-login<% } %>">
                    <div class="bullet"></div>
                    <div class="footer-link"><%= Html.RouteRefLink("About", SupportRoutes.AboutUs) %></div>
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= Html.RouteRefUrl(FaqsRoutes.Faqs) %>">Help/FAQs</a></div>
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= Html.RouteRefUrl(SupportRoutes.ContactUs) %>">Contact Us</a></div>
<%  if (CurrentRegisteredUser == null)
    { %>                    
                    <div class="separator"></div>
                    <div class="bullet member-bullet"></div>
                    <div class="footer-link member-link"><%= Html.RouteRefLink("Candidate site", HomeRoutes.Home, new { ignorePreferred = true }) %></div>
                    <div class="bullet employer-bullet"></div>
                    <div class="footer-link employer-link"><%= Html.RouteRefLink("Employer site", LinkMe.Web.Areas.Employers.Routes.HomeRoutes.Home, new { ignorePreferred = true }) %></div>
<%  } %>                    
                </div>
                <div class="sub-links">
<%  if (Request.Browser.IsMobileDevice)
    { %>
                    <div class="bullet"></div>
                    <div class="footer-link"><%= Html.RouteRefLink("Mobile site", SupportRoutes.SwitchBrowser, new { mobile = true, returnUrl = Context.GetClientUrl().PathAndQuery })%></div>
<%  } %>                    
                    <div class="bullet"></div>
                    <div class="footer-link"><%= Html.RouteRefLink("Terms of use", SupportRoutes.Terms) %></div>
                    <div class="bullet"></div>
                    <div class="footer-link"><%= Html.RouteRefLink("Privacy", SupportRoutes.Privacy) %></div>
                </div>
            </div>
        </div>
        <div class="divider"></div>
        <div class="seo-links-holder">
            <div class="seo-links">
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs") %>">Jobs</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs") %>">Australian Jobs</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs/-/it-telecommunications-jobs") %>">IT Jobs</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs/-/accounting-jobs") %>">Accounting Jobs</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs") %>">Melbourne Jobs</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs/sydney-jobs") %>">Sydney Jobs</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/jobs/brisbane-jobs") %>">Brisbane Jobs</a></div>
            </div>
            <div class="seo-links candidates">
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates") %>">Australian Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/melbourne-candidates") %>">Melbourne Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/sydney-candidates") %>">Sydney Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/brisbane-candidates") %>">Brisbane Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/western-australia-candidates") %>">WA Candidates</a></div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        if ($.browser.msie) {
            if ($.browser.version.indexOf("7") == 0) {
                $(".main-links").addClass("IE7");
                $("body").addClass("IE7");
            }
            if ($.browser.version.indexOf("8") == 0)
                $("body").addClass("IE8");
            if ($.browser.version.indexOf("9") == 0)
                $("body").addClass("IE9");
            if ($.browser.version.indexOf("10") == 0)
                $("body").addClass("IE10");
        }
        if ($.browser.mozilla) $("body").addClass("FF");
    });
</script>
<% Html.RenderPartial("DevInfo"); %>
