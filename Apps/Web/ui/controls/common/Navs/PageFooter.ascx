<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="PageFooter.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Navs.PageFooter" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Register TagPrefix="vw" TagName="DevInfo" Src="~/Views/Shared/DevInfo.ascx" %>

<%  if (ActiveVerticalHeader != "autopeople" && ActiveVerticalHeader != "otd")
    { %>
    
<div id="footer-container">
    <div class="footer-holder">
        <div class="footer-links-holder">
            <div class="left-section">
<%  if (ActiveUserType == UserType.Employer)
    { %>
                <div class="logo" onclick="javascript:loadPage('<%= EmployerHomeUrl %>');"></div>
<%  }
    else
    { %>
                <div class="logo" onclick="javascript:loadPage('<%= HomeUrl %>');"></div>
<%  } %>                
                <div class="copyright">&copy; Copyright LinkMe Pty Ltd</div>
            </div>
            <div class="right-section">
                <div class="main-links">
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= AboutUrl %>">About</a></div>
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= FaqsRoutes.Faqs.GenerateUrl() %>">Help/FAQs</a></div>
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= SupportRoutes.ContactUs.GenerateUrl() %>">Contact Us</a></div>
<%  if (LoggedInUserId == null)
    { %>                    
                    <div class="separator"></div>
                    <div class="bullet member-bullet"></div>
                    <div class="footer-link member-link"><a href="<%= MemberSiteHomeUrl %>">Candidate Site</a></div>
                    <div class="bullet employer-bullet"></div>
                    <div class="footer-link employer-link"><a href="<%= EmployerSiteHomeUrl %>">Employer Site</a></div>
<%  } %>                    
                </div>
                <div class="sub-links">
<%  if (Request.Browser.IsMobileDevice)
    { %>
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= SupportRoutes.SwitchBrowser.GenerateUrl(new { mobile = true, returnUrl = Context.GetClientUrl().PathAndQuery }) %>">Mobile site</a></div>
<%  } %>                    
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= TermsUrl %>">Terms of use</a></div>
                    <div class="bullet"></div>
                    <div class="footer-link"><a href="<%= PrivacyUrl %>">Privacy</a></div>
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
            <div class="seo-links">
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates") %>">Australian Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/melbourne-candidates") %>">Melbourne Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/sydney-candidates") %>">Sydney Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/brisbane-candidates") %>">Brisbane Candidates</a></div>
                <div class="footer-link"><a href="<%= new ReadOnlyApplicationUrl("~/candidates/western-australia-candidates") %>">WA Candidates</a></div>
            </div>
        </div>
    </div>
</div>
<% } %>
<vw:DevInfo runat="server" />
