<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.ExposeYourself" %>

<div class="homepage-section expose-yourself">
    <div class="play">
        <span class="discover">Discover why over <span class="red">600,000</span> candidates have used LinkMe to help them secure a job</span>
        <span class="watch">Watch a video</span>
    </div>
    <div class="video">
        <div class="section-top"></div>
        <div class="section-bg">
            <div id="whatmakeslinkmediff" url="http://www.youtube.com/v/wVhnmQYnNUI?version=3&amp;hl=en_US&amp;rel=0&amp;autoplay=1&amp;enablejsapi=1&amp;playerapiid=whatmakeslinkmediff&amp;fs=1">
                You need Flash player 8+ and JavaScript enabled to view this video.
            </div>
            <div onclick="javascript:loadPage('<%= JoinUrl %>');" class="homepage-button upload-resume-button"></div>
            <a href="#candidatehelp" class="button learnmore"></a>
        </div>
    </div>
    <div class="shaded-jobs-bottom">
        <div class="shaded-box-content">
            <div class="title">Recent searches:</div>
            <div class="sub-content" id="candidate-search-ticker-items">
                <ul>
<%  foreach (var search in Model.FeaturedCandidateSearches)
    { %>                    
                    <li class="link-holder"><a href="<%= search.Url %>" class="ticker-item" title="<%= search.Title %>"><%= search.Title %></a></li>
<%  } %>
                </ul>
            </div>
        </div>      
    </div>
</div>