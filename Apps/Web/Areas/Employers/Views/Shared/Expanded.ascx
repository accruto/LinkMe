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
       
<div class="expanded_search-results">

    <div class="candidate_search-result search-result js_selectable-item draggable result-container" data-memberid="<%=Html.Encode(view.Id)%>">
        <div class="search-result-header <% if (Model is ManageCandidatesListModel) { if (((ManageCandidatesListModel)Model).JobAd.Applications.ContainsKey(view.Id)) {%>candidate-source-applied<%} else {%>candidate-source-shortlisted<%} }%>">
        
            <div class="basic-details_holder">
                <div class="basic-details js_toggler">
                    <input class="result-selected-checkbox js_selectable-item-checkbox" type="checkbox" id="<%=view.Id%>" />
                    <span class="<%= view.Status.GetCssClassPrefixForStatus() %>_job-hunter-status job-hunter-status"><% =view.Status.GetDisplayText()%></span>
                    
                    <div class="basic-details-sublayout">
                        <div class="candidate-name">
				            <a href="<%= view.GenerateCandidateUrl() %>" class="js_ellipsis"><%= Html.Encode(view.GetCandidateTitle()) %></a>
                        </div>
                        <div class="location js_ellipsis"><%= view.Address == null ? string.Empty : Html.Encode(view.Address.Location)%></div>
                        <div class="last-updated js_ellipsis">Updated: <%= Html.Encode(lastUpdatedTime.ToString("dd MMM yyyy")) %></div>
				        <% Html.RenderPartial("ExtraBasicDetails", Model, new ViewDataDictionary { { "CandidateId", view.Id } }); %>
                    </div>
                </div>
                
                <div class="status-icons">
                
                    <ul class="client-activity">
<%      if (view.Folders > 0)
        { %>
                        <li>
                            <span class="in-folders-icon" title="Saved in <%=view.Folders%> folder<%=view.Folders == 1 ? "" : "s"%>">In folders</span>
                            <span class="count-holder">(<span class="in-folders-count"><%=view.Folders%></span>)</span>
                        </li>
<%      }
        if (view.Notes > 0)
        { %>
                        <li>
                            <a class="has-notes-icon" onclick="onClickDisplayNotes(this, '<%=view.Id%>');" href="javascript:void(0);" title="There <%=view.Notes == 1 ? "is 1 note" : "are " + view.Notes + " notes"%> for this candidate">Notes</a>
                            <span class="count-holder">(<span class="notes-count"><%=view.Notes%></span>)</span>
                        </li>
<%      }
        else
        { %>
                        <li>
                            <a class="has-notes-icon" onclick="onClickDisplayNotes(this, '<%=view.Id%>');" href="javascript:void(0);" title="There are no notes for this candidate">Notes</a>
<%          if (CurrentEmployer != null)
            { %>                    
                            <span class="count-holder">(<span class="notes-count"><%=view.Notes%></span>)</span>
<%          } %>
                        </li>
<%      }
        if (view.HasBeenViewed)
        { %>
                        <li><span class="viewed-icon" title="You have viewed this candidate's resume">Viewed</span></li>
<%      } %>
                    </ul>
                <span class="locking">
<%      switch (view.CanContact())
        {
            case CanContactStatus.YesWithoutCredit:
                if (view.HasBeenAccessed)
                { %>
                    <ul class="unlocking-and-phones">
                        <li><span class="contact-unlocked-icon"></span></li>
<%                  if (view.CanContactByPhone() != CanContactStatus.No)
                    { %>
                        <li><span class="phone-<%= primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline" %>-icon phone-number" title="Phone number"><%= Html.Encode(primaryPhoneNumber.Number) %></span></li>
<%                      if (secondaryPhoneNumber != null && !string.IsNullOrEmpty(secondaryPhoneNumber.Number))
                        { %>
                            <li><span class="phone-<%= secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline" %>-icon phone-number" title="Phone number"><%= Html.Encode(secondaryPhoneNumber.Number) %></span></li>
<%                      }
                    }
                    else
                    { %>
                        <li><span class="emptynumber">Phone numbers not supplied</span></li>
<%                  } %>
                    </ul>
<%              }
                else
                { %>
                    <ul class="unlocking-and-phones">
                        <li>
                            <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickUnlimitedUnlock('<%=view.Id%>');" title="Click to unlock this candidate"></a>
                        </li>
<%                  if (view.CanContactByPhone() != CanContactStatus.No)
                    { %>
                        <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available">Available</span></li>
<%                      if (secondaryPhoneNumber != null)
                        { %>
                            <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available">Available</span></li>
<%                      }
                    }
                    else
                    { %>
                        <li><span class="emptynumber">Phone numbers not supplied</span></li>
<%                  } %>
                    </ul>
<%              }
                break;
                
            case CanContactStatus.YesWithCredit: %>
                    <ul class="unlocking-and-phones">
                        <li><a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickUnlock(this, '<%=view.Id%>');" title="It costs 1 credit to contact this candidate or view their resume"></a></li>
<%              if (view.CanContactByPhone() != CanContactStatus.No)
                { %>
                        <li><span class="phone-<%= primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline" %>-dimmed-icon" title="Candidate's contact details are available for purchase">Available</span></li>
<%                  if (secondaryPhoneNumber != null)
                    { %>
                        <li><span class="phone-<%= secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline" %>-dimmed-icon" title="Candidate's contact details are available for purchase">Available</span></li>
<%                  }
                }
                else
                { %>
                        <li><span class="emptynumber">Phone numbers not supplied</span></li>
<%              } %>
                    </ul>
<%              break;
                
            default: %>
                    <ul class="unlocking-and-phones">
                        <li>
                            <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickLocked();" title="Insufficient credits"></a>
                        </li>
<%              if (view.CanContactByPhone() != CanContactStatus.No)
                { %>
                        <li><span class="phone-<%= primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline" %>-dimmed-icon" title="Phone number available - Insufficient credits">Available</span></li>
<%                  if (secondaryPhoneNumber != null)
                    { %>
                        <li><span class="phone-<%= secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline" %>-dimmed-icon" title="Phone number available - Insufficient credits">Available</span></li>
<%                  }
                }
                else
                { %>
                        <li><span class="emptynumber">Phone numbers not supplied</span></li>
<%              } %>
                    </ul>
<%              break;
        } %>
                </span>
                </div>
            </div>
            
            <div class="block-link_holder" onclick="<%= Model.GetBlockCandidatePermanently() ? "onClickPermanentlyBlockCandidate" : "onClickBlockCandidate" %>('<%= view.Id %>');"><a href="javascript:void(0);" class="block-icon">Block</a></div>
            <div class="restore-link_holder" onclick="onClickRestoreCandidateFromBlockList('<%=view.Id%>');"><a href="javascript:void(0);" class="restore-icon">Restore</a></div>
            
            <div class="flag-holder<%=view.IsFlagged ? " flagged" : ""%> <%= Model.GetFlagHolderCssClass() %>">
                <div class="flag flag<%=view.Id%>" id="flag<%=view.Id%>" onclick="onClickFlag(this);"></div>
                <% Html.RenderPartial("FlagActions", Model, new ViewDataDictionary { { "CandidateId", view.Id } }); %>
            </div>
        </div>
        
        <div class="search-result-body <% if (Model is ManageCandidatesListModel) { if (((ManageCandidatesListModel)Model).JobAd.Applications.ContainsKey(view.Id)) {%>candidate-source-applied<%} else {%>candidate-source-shortlisted<%} }%>">
            <div class="notes-content" id="notes-<%=view.Id%>" style="display:none;">
                <% Html.RenderPartial("Notes", view);%>
            </div>
            
            <div class="static_additional-details additional-details">
<%      if (view.AffiliateId != null && Model.Communities.ContainsKey(view.AffiliateId.Value) && Model.Verticals.ContainsKey(view.AffiliateId.Value))
        { %>
                <div class="detail">
                    <div class="detail-title">Community:</div>
                    <div class="detail-content">
                        <div class="community-partner_outer-container">
                            <% Html.RenderPartial(Model.Verticals[view.AffiliateId.Value], "CommunityDetail", Model.Communities[view.AffiliateId.Value]); %>
                        </div>                     
                    </div>
                </div>
<%      } %>
            
                <div class="detail">
                    <div class="detail-title">Most recent 3 jobs:</div>
                    <div class="detail-content">
                    
<%      var jobs = (view.Resume == null || view.Resume.Jobs == null) ? null : view.Resume.Jobs;
        if (jobs != null && jobs.Count() > 0)
        { %>
                        <table class="jobs">
<%          foreach (var job in jobs.Take(3))
            { %>
                            <tr class="job">
                                <td class="description">
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
                                    <span class="hyphen"> - </span>
                                    <span class="company">
<% if (!String.IsNullOrEmpty(job.Company))
                    { %>
                                        <%=HighlightEmployer(job.Company)%>
<%                  }
                else
                { %>
                                        &lt;Employer Hidden&gt;
<%              } %>
                                    </span>
                                </td>
                                <td>
                                    <span class="tenure">
<%              if (job.Dates.IsValidTenureForDisplay())
                { %>
                                        <%=Html.Encode(job.Dates.GetTenureDisplayText())%>
<%              }
                else
                { %>
                                        <span class="empty">Unknown</span>
<%              } %>
                                    </span>
                                </td>
                                <td>
                                    <span class="date-range">
                                        <span class="start-date"><% =Html.Encode(job.Dates != null && job.Dates.Start.HasValue ? job.Dates.Start.Value.ToString("MMM yyyy") : "N/A")%></span>
                                        <span class="hyphen">-</span>
                                        <span class="end-date"><% =Html.Encode(job.Dates != null && job.Dates.End.HasValue ? job.Dates.End.Value.ToString("MMM yyyy") : "Current")%></span>
                                    </span>
                                </td>
                            </tr>                                        
<%          }
            if (jobs.Count() < 3)
            {
                for (uint i = 1; i <= (3 - jobs.Count()); i++)
                { %>
                            <tr class="empty_job job">
                                <td class="description">
                                    N/A
                                </td>
                                <td>
                                    <span class="tenure">N/A</span>
                                </td>
                                <td>
                                    <span class="date-range">
                                        <span class="start-date">N/A</span>
                                        <span class="hyphen">-</span>
                                        <span class="end-date">N/A</span>
                                    </span>
                                </td>
                            </tr>
<%              }
            } %>
                        </table>
<%      }
        else
        { %>
                        <table class="jobs">
                            <tr class="job">
                                <td class="description">
                                    <span class="title"> N/A </span>
                                </td>
                            </tr>
                        </table>
<%      } %>
                    </div>
                </div>
                        
                <div class="detail">
                    <div class="detail-title">Desired salary:</div>
                    <div class="detail-content">
                        <div class="desired-salary">
<%      if (view.DesiredSalary != null && view.DesiredSalary.LowerBound != null)
        { %>
                            <span><%= view.DesiredSalary.ToRate(SalaryRate.Year).GetDisplayText() %></span>
<%      }
        else
        { %>
                            <span class="empty">None specified</span>
<%      } %>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="dynamic_additional-details additional-details">
                <div class="detail">
<%      if (!string.IsNullOrEmpty(view.DesiredJobTitle))
        { %>	                
                    <div class="detail-title">Desired jobs:</div>
				    <div class="detail-content js_ellipsis"><%= HighlightDesiredJobTitle(view.DesiredJobTitle) %></div>
<%      } %>	                    
                </div>
            </div>
            
<%      if (view.Resume != null)
        { %>
            <div class="dynamic_additional-details additional-details">
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
    </div>

<%      var candidateName = Html.Encode(view.GetCandidateTitle());
	    Guid? jobAdId = null;

        if (Model is SuggestedCandidatesListModel)
            jobAdId = ((SuggestedCandidatesListModel)Model).JobAd.Id;
        else if (Model is ManageCandidatesListModel)
            jobAdId = ((ManageCandidatesListModel)Model).JobAd.Id; %>
            
    <div id="prompt-region<%= Html.Encode(view.Id) %>" candidateName="<%= candidateName %>" <% if (Model is SuggestedCandidatesListModel || Model is ManageCandidatesListModel) { %>jobAdId="<%= jobAdId %>"<% } %>></div>
</div>
            
<%  } %>
