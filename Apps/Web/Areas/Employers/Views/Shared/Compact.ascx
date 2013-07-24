<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Shared.CandidateListViewUserControl" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Views"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>

<%  foreach (var candidateId in Model.Results.CandidateIds)
    {
        var view = Model.Results.Views[candidateId];
        var lastUpdatedTime = Model.Results.LastUpdatedTimes[candidateId];
        var primaryPhoneNumber = view.GetPrimaryPhoneNumber();
        var secondaryPhoneNumber = view.GetSecondaryPhoneNumber(); %>

<div class="compact_search-results_ascx <% if (Model is SuggestedCandidatesListModel) { %>suggest-candidate<% } %>">
    <div class="candidate_search-result search-result js_selectable-item draggable result-container" data-memberid="<%= Html.Encode(candidateId) %>">
                
	    <div class="search-result-body columns <% if (Model is ManageCandidatesListModel) { if (((ManageCandidatesListModel)Model).JobAd.Applications.ContainsKey(view.Id)) {%>candidate-source-applied<%} else {%>candidate-source-shortlisted<%} }%>">
            <div class="header_column column">
            
                <div class="basic-details js_toggler">
                    <input class="result-selected-checkbox js_selectable-item-checkbox" type="checkbox" id="<%= view.Id %>" />
                    <span class="<%= view.Status.GetCssClassPrefixForStatus() %>_job-hunter-status job-hunter-status"><% =view.Status.GetDisplayText() %></span>
                    <div class="basic-details-sublayout">
				        <div class="candidate-name">
				            <a href="<%= view.GenerateCandidateUrl() %>" class="js_ellipsis"><%= Html.Encode(view.GetCandidateTitle()) %></a>
                        </div>
                        <div class="location js_ellipsis"><%= view.Address == null ? string.Empty : Html.Encode(view.Address.Location) %></div>
				        <div class="last-updated js_ellipsis">Updated:  <%= Html.Encode(lastUpdatedTime.ToString("dd MMM yyyy")) %></div>
				        <% Html.RenderPartial("ExtraBasicDetails", Model, new ViewDataDictionary { { "CandidateId", view.Id } }); %>
                    </div>
		        </div>
		        
		        <div class="status-icons">
                    <ul>
<%      switch (view.CanContact())
        {
            case CanContactStatus.YesWithoutCredit:
                if (view.HasBeenAccessed)
                { %>
                        <li>
		                    <span class="contact-unlocked-icon" title="Candidate unlocked"></span>
		                </li>
<%              }
                else
                { %>
                        <li>
		                    <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickUnlimitedUnlock('<%= view.Id %>');" title="Click to unlock this candidate"></a>
		                </li>
<%              }
                break;
                
            case CanContactStatus.YesWithCredit: %>
                        <li>
		                    <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickUnlock(this, '<%= view.Id %>');" title="It costs 1 credit to contact this candidate or view their resume"></a>
                        </li>
<%              break;
                
            default: %>
                        <li>
                            <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickLocked();" title="Insufficient credits"></a>
                        </li>
<%              break;
        } %>
<%      switch (view.CanContact())
        {
            case CanContactStatus.YesWithoutCredit:
                if (view.HasBeenAccessed)
                {
                    if (view.CanContactByPhone() != CanContactStatus.No)
                    { %>
                        <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-icon phone-number" title="<%=Html.Encode(primaryPhoneNumber.Number)%>"></span></li>
<%                      if (secondaryPhoneNumber != null && !string.IsNullOrEmpty(secondaryPhoneNumber.Number))
                        { %>
                        <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-icon phone-number" title="<%=Html.Encode(secondaryPhoneNumber.Number)%>"></span></li>
<%                      }
                    }
                }
                else
                {
                    if (view.CanContactByPhone() != CanContactStatus.No)
                    { %>
                        <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available"></span></li>
<%                      if (secondaryPhoneNumber != null)
                        { %>
                        <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available"></span></li>
<%                      }
                    }
                }
                break;

            case CanContactStatus.YesWithCredit:
                if (view.CanContactByPhone() != CanContactStatus.No)
                { %>
                        <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available for purchase"></span></li>
<%                  if (secondaryPhoneNumber != null)
                    { %>
                        <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available for purchase"></span></li>
<%                  }
                }
                break;

            default:
                if (view.CanContactByPhone() != CanContactStatus.No)
                { %>
                        <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Mobile phone number available - Insufficient Credits"></span></li>
<%                  if (secondaryPhoneNumber != null)
                    { %>
                        <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Mobile phone number available - Insufficient Credits"></span></li>
<%                  }
                }
                break;
        } %>

                        <li class="notes">
<%      if (view.Notes > 0)
        { %>
                            <a class="has-notes-icon" onclick="onClickDisplayNotes(this, '<%= view.Id %>');" href="javascript:void(0);" title="There <%= view.Notes == 1 ? "is 1 note" : "are " + view.Notes + " notes" %> for this candidate"></a>
<%      }
        else
        { %>
                            <a class="has-notes-icon" onclick="onClickDisplayNotes(this, '<%= view.Id %>');" href="javascript:void(0);" title="There are no notes for this candidate"></a>
<%      } %>
                        </li>
                        <li class="folder"><% if (view.Folders > 0) {%><span class="in-folders-icon" title="Saved in <%= view.Folders %> folder<%= view.Folders == 1 ? "" : "s" %>"></span><%}%></li>
		                <li class="viewed"><% if (view.HasBeenViewed) { %><span class="viewed-icon" title="You have viewed this candidate's resume"></span><%}%></li>
                    </ul>
                </div>
            </div>

            <div class="jobs_column column">

<%      var jobs = (view.Resume == null || view.Resume.Jobs == null) ? null : view.Resume.Jobs;
        if (jobs != null && jobs.Count() > 0)
        { %>
                <table class="jobs">
<%          foreach (var job in jobs.Take(2))
            { %>
                    <tr class="job">
                        <td class="description <% if (jobs.IndexOf(job) > 0) { %>latter<% } %>">
                            <span class="title">
<%              if (!string.IsNullOrEmpty(job.Title))
                { %>
                                <%= HighlightJobTitle(job.Title)%>
<%              }
                else
                { %>
                                <span class="empty">Unknown job title</span>
<%              } %>
                            </span>
                            <span class="company">
<%              if (!string.IsNullOrEmpty(job.Company))
                { %>
                                <%= HighlightEmployer(job.Company)%>
<%              }
                else
                { %>
                                &lt;Employer Hidden&gt;
<%              } %>
                            </span>
                        </td>
                        <td>
                            <span class="tenure"><%=Html.Encode(job.Dates.GetTenureDisplayText()) %></span>
                        </td>
                    </tr>
<%          } %>
                </table>
<%      }
        else
        { %>
               Recent jobs are unavailable for this candidate.
<%      } %>
            </div>
            
            <div class="desired-salary_column column">
                <%= view.DesiredSalary != null && view.DesiredSalary.LowerBound != null ? view.DesiredSalary.ToRate(SalaryRate.Year).GetDisplayText() : "None specified" %>
            </div>
            <div class="block-link_holder" onclick="<%= Model.GetBlockCandidatePermanently() ? "onClickPermanentlyBlockCandidate" : "onClickBlockCandidate" %>('<%= view.Id %>');"><a href="javascript:void(0);" class="block-icon">Block</a></div>
	        <div class="restore-link_holder" onclick="onClickRestoreCandidateFromBlockList('<%= view.Id %>');"><a href="javascript:void(0);" class="restore-icon">Restore</a></div>

            <div class="flag-holder<%= view.IsFlagged ? " flagged" : "" %> <%= Model.GetFlagHolderCssClass() %>">
                <div class="flag flag<%= view.Id %>" id="flag<%= view.Id %>" onclick="onClickFlag(this);"></div>
                <% Html.RenderPartial("FlagActions", Model, new ViewDataDictionary { { "CandidateId", view.Id }}); %>
				<% if (! (Model is ManageCandidatesListModel)) {%>
                <a class="open_expando expando js_expando" href="javascript:void(0)" data-for="search-result-extension-<%= Html.Encode(candidateId) %>" style="display: none"></a>
				<% } %>
            </div>
            
<%      if (view.AffiliateId != null && Model.Communities.ContainsKey(view.AffiliateId.Value) && Model.Verticals.ContainsKey(view.AffiliateId.Value))
        { %>
            <div class="community-partner_outer-container">
                <% Html.RenderPartial(Model.Verticals[view.AffiliateId.Value], "CommunityDetail", Model.Communities[view.AffiliateId.Value]); %>
            </div>
<%      } %>

        </div>

        <div class="search-result-extension <% if (Model is ManageCandidatesListModel) { if (((ManageCandidatesListModel)Model).JobAd.Applications.ContainsKey(view.Id)) {%>candidate-source-applied<%} else {%>candidate-source-shortlisted<%} }%>" style="display: none" id="search-result-extension-<%= Html.Encode(candidateId) %>">
            <div class="static_additional-details additional-details">
	            <div class="detail">&nbsp;
<%      if (!string.IsNullOrEmpty(view.DesiredJobTitle))
        { %>
                    <div class="detail-title">Desired jobs:</div>
				    <div class="detail-content js_ellipsis"><%= HighlightDesiredJobTitle(view.DesiredJobTitle) %></div>
<%      } %>
                </div>
            </div>
            
        
<%      if (view.Resume != null)
        { %>
            <div class="dynamic_additional-details additional-details <% if (SummarizeSections(view).Count() > 0) { %>has-content<% } %> <% if (string.IsNullOrEmpty(view.DesiredJobTitle)) { %> empty-static<% } %>">
<%          foreach (var summary in SummarizeSections(view).Take(2))
            { %>
                <div class="detail">
                    <div class="detail-title"><%= summary.Key %>:</div>
					<div class="detail-content js_skill-ellipsis"><%= summary.Value %></div>
                </div>
<%          } %>
            </div>
<%      } %>
        </div>
	
        <div class="notes-content <% if (Model is ManageCandidatesListModel) { if (((ManageCandidatesListModel)Model).JobAd.Applications.ContainsKey(view.Id)) {%>candidate-source-applied<%} else {%>candidate-source-shortlisted<%} }%>" id="notes-<%=view.Id%>" style="display:none;">
            <% Html.RenderPartial("Notes", view); %>
        </div>
    </div>

<%  var candidateName = Html.Encode(view.GetCandidateTitle());
	Guid? jobAdId = null;

    if (Model is SuggestedCandidatesListModel) jobAdId = ((SuggestedCandidatesListModel)Model).JobAd.Id;
	else if (Model is ManageCandidatesListModel) jobAdId = ((ManageCandidatesListModel)Model).JobAd.Id;
%>
            
    <div id="prompt-region<%= Html.Encode(view.Id) %>" candidateName="<%= candidateName %>" <% if (Model is SuggestedCandidatesListModel || Model is ManageCandidatesListModel) { %>jobAdId="<%= jobAdId %>"<% } %>></div>
</div>
            
<%  } %>