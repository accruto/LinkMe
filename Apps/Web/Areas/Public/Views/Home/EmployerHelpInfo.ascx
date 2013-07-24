<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="homepage-section employers-and-recruiters">
    <div class="homepage-title">Employers and Recruiters</div>
    <div class="homepage-content">
        <div class="column left">
            <div class="videotitle">What is LinkMe?</div>
            <div class="desc">LinkMe let you choose <span class="red">who you want</span> in your business</div>
            <div class="video">
                <div id="whatislinkme" url="http://www.youtube.com/v/7H8RrYPDpJo?version=3&amp;hl=en_US&amp;rel=0&amp;enablejsapi=1&amp;playerapiid=whatislinkme&amp;fs=1">
                    You need Flash player 8+ and JavaScript enabled to view this video.
                </div>
            </div>
        </div>
        <div class="divider"></div>
        <div class="column right">
            <div class="videotitle">Why use LinkMe?</div>
            <div class="desc">LinkMe is a large collection of resumes giving employers and recruiters, like you, <span class="red">immediate access</span> to candidates.</div>
            <div class="video">
                <div id="whyuselinkme" url="http://www.youtube.com/v/JdFx3Qx93h8?version=3&amp;hl=en_US&amp;rel=0&amp;enablejsapi=1&amp;playerapiid=whyuselinkme&amp;fs=1">
                    You need Flash player 8+ and JavaScript enabled to view this video.
                </div>
            </div>
        </div>
        <div class="employer-button-holder">
            <div class="employer-site-section-text">Go to our</div>
            <div class="homepage-button employer-site-button" onclick="javascript:loadPage('<%= HomeRoutes.Home.GenerateUrl(new { ignorePreferred = true }) %>');"></div>
        </div>
    </div>
</div>