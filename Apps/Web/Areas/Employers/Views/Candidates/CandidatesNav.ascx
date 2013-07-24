<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ViewCandidatesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<div class="nav_holder">
    <div class="nav-arrow_holder">
        <div class="arrow left-arrow"></div>
        <div class="nav-results-count_holder">
            <span class="nav-results-count_text"><span class="selected-candidate-index">1</span> of <span class="total-candidate-count"><%= Model.CandidateIds.Count %></span></span>
        </div>
        <div class="arrow right-arrow"></div>
    </div>
    <div class="resumes-nav_holder">
        <div class="resumes-nav">
<%  foreach (var candidateId in Model.CandidateIds)
    {
        var view = Model.Views[candidateId];
        var lastUpdatedTime = Model.LastUpdatedTimes[candidateId]; %>
            <div id="<%= view.Id %>" index-id="<%= Model.CandidateIds.IndexOf(candidateId) %>" class="nav-candidate <% if(view.Id == Model.CurrentCandidate.View.Id){%> nav-candidate-selected <%}%>">
                <span class="arrow"></span>
                <span class="list-icon"></span>
                <span class="<%= view.Status.GetCssClassPrefixForStatus() %>_job-hunter-status job-hunter-status"></span>
                <div class="basic-details">
                    <div class="candidate-name js_ellipsis"><%= Html.Encode(view.GetCandidateTitle()) %></div>
                    <div class="location js_ellipsis"><%= view.Address == null ? string.Empty : Html.Encode(view.Address.Location)%></div>
                    <div class="last-updated js_ellipsis">Updated: <%= Html.Encode(lastUpdatedTime.ToString("dd MMM yyyy")) %></div>
                </div>
                <% if (view.IsFlagged){ %>
                <div class="flag-holder flagged">
                <% }else{ %>
                <div class="flag-holder">
                <% } %>
                    <div class="flag js_resume-nav-flag flag<%= view.Id %>" id="flag<%= view.Id %>"></div>
                </div>
            </div>       
                 
<%  } %>
        </div>
    </div>
</div>
<script type="text/javascript">
   (function($) {
       /* Candidate navigation */
       $(".nav_holder").candidateNavigation("<%= Model.CandidateIds.Count %>");
       $(".js_ellipsis").ellipsis();
   })(jQuery);
</script> 