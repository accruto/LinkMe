<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Candidates.Resume" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members"%>
<%@ Import Namespace="LinkMe.Web.Context" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Views"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<%  var jobAdId = new Guid();
    if (Model.CurrentCandidates is SuggestedCandidatesNavigation)
        jobAdId = ((SuggestedCandidatesNavigation) Model.CurrentCandidates).JobAdId;
    if (Model.CurrentCandidates is ManageCandidatesNavigation)
        jobAdId = ((ManageCandidatesNavigation) Model.CurrentCandidates).JobAdId;

    var shortlistedStatus = "";
    var rejectedStatus = "";
    if (Model.CurrentCandidates is ManageCandidatesNavigation)
    {
        if (Model.CurrentCandidate.ApplicantStatus.HasValue && Model.CurrentCandidate.ApplicantStatus.Value == ApplicantStatus.Shortlisted)
            shortlistedStatus = "selected";
        if (Model.CurrentCandidate.ApplicantStatus.HasValue && Model.CurrentCandidate.ApplicantStatus.Value == ApplicantStatus.Rejected)
            rejectedStatus = "selected";
    }

    var view = Model.CurrentCandidate.View;
    var lastUpdatedTime = Model.LastUpdatedTimes[Model.CurrentCandidate.View.Id]; %>

<script language="javascript" type="text/javascript">

    (function($) {
        $.extend(candidateContext,
        {
            candidateIds: ["<%= view.Id.ToString() %>"]
        });
        document.title = "<%= view.GetPageTitle() %>";
    })(jQuery);

</script>

<div class="resume_holder result-container" data-memberid="<%= view.Id %>">
    <div id="<%= view.Id %>">
        <div class="resume-header">
            <div class="basic-details js_resume-header draggable" data-memberid="<%= view.Id %>">
                <span class="menu-icon menu-icon-down"></span>
                <div class="candidate-name js_candidate-name_ellipsis"><%= Html.Encode(view.GetCandidateTitle()) %></div>
                <span class="<%= view.Status.GetCssClassPrefixForStatus() %>_job-hunter-status job-hunter-status"></span>
                <span class="job-hunter-status_text"><% =view.Status.GetDisplayText()%></span>
            </div>
            <div class="divider"></div>
			<% if (Model.CurrentCandidates is SuggestedCandidatesNavigation || Model.CurrentCandidates is ManageCandidatesNavigation) { %>
			<div class="shortcut-button-holder">
				<div class="top-holder"><div class="shortlist-link_holder <%= shortlistedStatus %>"></div></div>
				<div class="horizontal-divider"></div>
				<div class="bottom-holder"><div class="reject-link_holder <%= rejectedStatus %>"></div></div>
			</div>
            <div class="divider"></div>
			<% } %>
            <div class="additional-details_holder <% if (Model.CurrentCandidates is SuggestedCandidatesNavigation || Model.CurrentCandidates is ManageCandidatesNavigation) { %>not-search-result<% } %>">
                <div class="additional-details">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr class="last-updated">
                            <td class="detail">Resume last updated:</td>
                            <td class="date"><%= Html.Encode(lastUpdatedTime.ToString("dd MMM yyyy")) %></td>
                        </tr>
                        <tr class="last-visited">
                            <td class="detail">Last visited LinkMe:</td>
                            <td class="date"><%= (Model.CurrentCandidate.LastLoginTime != null) ? Model.CurrentCandidate.LastLoginTime.Value.ToString("dd MMM yyyy") : view.CreatedTime.ToString("dd MMM yyyy")%></td>
                        </tr>
                        <tr class="joined">
                            <td class="detail">Joined LinkMe:</td>
                            <td class="date"><%= view.CreatedTime.ToString("dd MMM yyyy") %></td>
                        </tr>
                    </table>
                </div>
                <div class="fullscreen_holder js_fullscreen">
                    <a class="fullscreen_link">Hide menu</a>
                    <div class="fullscreen_icon"></div>
                </div>
                <div class="flag-holder <%= Model.CurrentCandidate.View.IsFlagged ? "flagged" : "" %>">
                    <div class="flag flag<%=Model.CurrentCandidate.View.Id%>" id="flag<%=Model.CurrentCandidate.View.Id%>" onclick="onClickFlag(this);"><span>Flag</span></div>
                </div>
            </div>
        </div>
        <div class="resume-menu-header">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="action-menu js_action-items">
                <tr class="action-items">
                    <% var canContact = view.CanContact(); %>
                    <% var canContactByPhone = view.CanContactByPhone(); %>
                    <% var canAccessResume = view.CanAccessResume(); %>

                    <td>
                        <div class="send-message<%= GetActionSuffix(canContact) %> js_action-item action-item">Send message</div>
                    </td>
                    <td>
                        <div class="download-resume<%= GetActionSuffix(canAccessResume) %> js_action-item action-item">Download resume</div>
                    </td>
                    <% if (CurrentEmployer == null) { %>
                    <td>
                        <div class="add-to-folder-disabled js_action-item action-item">Add to <span class="dynamic-text">a new folder</span></div>
                    </td>
                    <% } else { %>
                    <td class="has-dropdown">
                        <div class="add-to-folder js_action-item action-item js_absorb-clicked-child" onclick="onClickAddToNewFolder('<%= view.Id %>');">Add to <span class="dynamic-text">a new folder</span><span class="action-icon-holder js_toggleDropdown"><span class="action-icon action-icon-down">&nbsp;&nbsp;</span></span></div>
                        <div class="add-to-folder_dropdown dropdown">
                            <div class="add-to-folder_dropdown_holder">
                                <div class="folders-dropdown-list"></div>
                            </div>
                        </div>
                    </td>
                    <% } %>
                </tr>
                <tr class="action-items">
                    <td>
                        <div class="view-phone-numbers<%= GetActionSuffix(view.HasBeenAccessed, canContact, canContactByPhone) %> js_action-item action-item">View phone numbers</div>
                    </td>
                    <td>
                        <div class="email-resume<%= GetActionSuffix(canAccessResume) %> js_action-item action-item">Email resume to me</div>
                    </td>
                    <% if (CurrentEmployer == null) { %>
                    <td>
                        <div class="add-to-jobad-disabled js_action-item action-item">Add to <span class="dynamic-text">a job ad</span></div>
                    </td>
                    <% } else { %>
                    <td class="has-dropdown">
                        <div class="add-to-jobad js_action-item action-item js_absorb-clicked-child">Add to <span class="dynamic-text">a job ad</span><span class="action-icon-holder js_toggleDropdown"><span class="action-icon action-icon-down">&nbsp;&nbsp;</span></span></div>
                        <div class="add-to-jobad_dropdown dropdown">
                            <div class="add-to-jobad_dropdown_holder">
                                <div class="jobads-dropdown-list"></div>
                            </div>
                        </div>
                    </td>
                    <% } %>
                </tr>
            </table>
        </div>
        <% Html.RenderPartial("ResumeDetails", Model.CurrentCandidate); %>
    </div>
</div>
<script type="text/javascript">
	(function($) {

		var resumeHeader = $(".js_resume-header");

	   /* Activating Header */
	   resumeHeader.activateResumeHeader();
	   /* Displaying action menu */
	   resumeHeader.toggleActionMenu();
	   /* Fullscreen */
	   $(".js_fullscreen").toggleFullscreenView();
	   /*$(".js_candidate-name_ellipsis").customEllipsis(13);*/
	   $(".js_candidate-name_ellipsis").ellipsis();

	})(jQuery);
   
	<% if (Model.CurrentCandidates is SuggestedCandidatesNavigation || Model.CurrentCandidates is ManageCandidatesNavigation) { %>
	(function($) {
		toggleSortlistedAndRejectedStatus = function(element) {
			if ($(element).hasClass("selected")) return;
			if ($(element).hasClass("shortlist-link_holder")) {
				employers.api.shortlistCandidatesForJobAd(
					"<%= jobAdId %>",
					new Array("<%= view.Id.ToString() %>"),
					function(response) {
						$(element).addClass("selected");
						$(".reject-link_holder").removeClass("selected");
					}
				);
			}
			if ($(element).hasClass("reject-link_holder")) {
				employers.api.rejectCandidatesForJobAd(
					"<%= jobAdId %>",
					new Array("<%= view.Id.ToString() %>"),
					function(response) {
						$(element).addClass("selected");
						$(".shortlist-link_holder").removeClass("selected");
					}
				);
			}
		};
	})(jQuery);
   
	$(document).ready(function() {
		$(".shortlist-link_holder").click(function() {
			toggleSortlistedAndRejectedStatus(this);
		});
		$(".reject-link_holder").click(function() {
			toggleSortlistedAndRejectedStatus(this);
		});
	});
	<% } %>
</script> 