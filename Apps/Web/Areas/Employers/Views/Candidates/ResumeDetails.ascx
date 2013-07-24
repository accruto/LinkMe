<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Candidates.ResumeDetails" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>

<%  var primaryPhoneNumber = Model.View.GetPrimaryPhoneNumber();
    var secondaryPhoneNumber = Model.View.GetSecondaryPhoneNumber(); %>

<div class="resume-details_holder">
    <div class="horizontal-divider"></div>
    <div class="resume-status-header">
        <div class="status-icons">
            <ul>
<%  switch (Model.View.CanContact())
    {
        case CanContactStatus.YesWithoutCredit:
            if (Model.View.HasBeenAccessed)
            { %>
                <li>
                    <span class="contact-unlocked-icon" title="Contact unlocked"></span>
                </li>
<%          }
            else
            { %>
                <li>
                    <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickUnlimitedUnlock();" title="Click to unlock this candidate"></a>
                </li>
<%          }
            break;
            
        case CanContactStatus.YesWithCredit: %>
                <li>
                    <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickUnlock(this);" title="It costs 1 credit to contact this candidate or view their resume"></a>
                </li>
<%          break;
            
        default: %>
                <li>
                    <a href="javascript:void(0)" class="contact-locked-icon" onclick="onClickLocked();" title="Insufficient credits"></a>
                </li>
<%          break;
    } %>

<%  switch (Model.View.CanContact())
    {
        case CanContactStatus.YesWithoutCredit:
            if (Model.View.HasBeenAccessed)
            {
                if (Model.View.CanContactByPhone() != CanContactStatus.No)
                { %>
                <li>
                    <span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-icon phone-number" title="<%=Html.Encode(primaryPhoneNumber.Number)%>"><%=Html.Encode(primaryPhoneNumber.Number)%></span>
                </li>
<%                  if (secondaryPhoneNumber != null)
                    { %>
                        <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="<%=Html.Encode(secondaryPhoneNumber.Number)%>"><%=Html.Encode(secondaryPhoneNumber.Number)%></span></li>
                <%  }
                }
            }
            else
            {
                if (Model.View.CanContactByPhone() != CanContactStatus.No)
                { %>
                        <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available">Available</span></li>
<%                  if (secondaryPhoneNumber != null)
                    { %>
                        <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available">Available</span></li>
<%                  }
                }
            }
            break;

        case CanContactStatus.YesWithCredit:
            if (Model.View.CanContactByPhone() != CanContactStatus.No)
            { %>
                    <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available for purchase">Available</span></li>
<%              if (secondaryPhoneNumber != null)
                { %>
                    <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Candidate's contact details are available for purchase">Available</span></li>
<%              }
            }
            break;

        default:
            if (Model.View.CanContactByPhone() != CanContactStatus.No)
            { %>
                    <li><span class="phone-<%=primaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Phone number available - Insufficient credits">Available</span></li>
<%              if (secondaryPhoneNumber != null)
                { %>
                    <li><span class="phone-<%=secondaryPhoneNumber.Type == PhoneNumberType.Mobile ? "mobile" : "landline"%>-dimmed-icon" title="Phone number available - Insufficient credits">Available</span></li>
<%              }
            }
            break;
    } %>
                <li class="folder">
<%  if (Model.View.Folders > 0)
    { %>
                    <span class="in-folders-icon" title="Saved in <%=Model.View.Folders%> folder<%=Model.View.Folders == 1 ? "" : "s"%>">In folders</span>
<%      if (CurrentEmployer != null)
        { %>                    
                    <span class="count-holder">(<span class="in-folders-count"><%=Model.View.Folders%></span>)</span>
<%      }
    } %>
                </li>
                <li class="notes">
<%  if (Model.View.Notes > 0)
    { %>
                    <a class="has-notes-icon" onclick="onClickDisplayNotes(this, '<%= Model.View.Id %>');" href="javascript:void(0);" title="There <%=Model.View.Notes == 1 ? "is 1 note" : "are " + Model.View.Notes + " notes"%> for this candidate">Notes</a>
                    <span class="count-holder">(<span class="notes-count"><%=Model.View.Notes%></span>)</span>
<%  }
    else
    { %>
                    <a class="has-notes-icon" onclick="onClickDisplayNotes(this, '<%= Model.View.Id %>');" href="javascript:void(0);" title="There are no notes for this candidate">Notes</a>
<%      if (CurrentEmployer != null)
        { %>                    
                    <span class="count-holder">(<span class="notes-count">0</span>)</span>
<%      }
    } %>
                </li>
                <li class="viewed">
<%  if (Model.View.HasBeenViewed)
    { %>
                    <span class="viewed-icon" title="You have viewed this candidate's resume">Viewed</span>
<%  } %>
                </li>
            </ul>                                   
        </div>
    </div>
    <div class="resume-notes_holder">
        <div class="notes-content" id="notes-<%=Model.View.Id%>" style="display:none;">
            <% Html.RenderPartial("Notes", Model.View); %>
        </div>
    </div>
    <div class="resume-details">
        <ul class="tabs-nav js_resume-tabs">
	        <li id="Summary" class="js_default-selected-tab">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Summary">Summary</span></a>
	        </li>
	        <li id="Employment">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Employment">Employment</span></a>
	        </li>
	        <li id="Education">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Education">Education</span></a>
	        </li>
	        <li id="Skills">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Skills">Skills</span></a>
	        </li>
	        <li id="Professional">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Professional">Professional</span></a>
	        </li>
	        <li id="Personal">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Personal">Personal</span></a>
	        </li>
<%  if (Model.CurrentApplication != null)
    { %>
	        <li id="CoverLetter">
	            <a href="javascript:void(0);"><span data-tab-shorttext="Cover letter">Cover letter</span></a>
	        </li>
<%  } %>
	        <li id="FullResume" class="right-tab">
		        <a href="javascript:void(0);"><span data-tab-shorttext="Full resume">Full resume</span></a>
	        </li>
        </ul>   
        <div class="tabs-container tabs-hide full-resume-container" id="FullResume-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
                    &nbsp;
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div> 
        <div class="tabs-container summary-container" id="Summary-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">   
                    <div class="resume-photo_holder" id="resume-photo">
                        <img src="<%= Images.PhotoBg %>" height="125px" width="100px" class="resume-photo_frame" />
<%  if (Model.View.PhotoId != null)
    { %>
                        <%= Html.Image(CandidatesRoutes.Photo, new {candidateId = Model.View.Id.ToString()}, new {@class = "resume-photo"}) %>
<%  }
    else
    { %>
                        <%= Html.Image(Images.PhotoDefault, new {@class = "resume-photo"}) %>
<%  } %>                
                    </div>
                    <div class="employment-history_section resume_section">
                        <div class="resume-heading">Employment history</div>
                        <div class="resume-content">
                        
                            <table class="profession">
                                <tr>
                                    <td>
                                        <span class="title recent-profession"> Most recent profession: </span>
                                    </td>
                                    <td>
                                        <span class="description">
                                            <%= (Model.View.RecentProfession == null ? "N/A" : Model.View.RecentProfession.GetDisplayText())%>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="title recent-profession"> Most recent role seniority: </span>
                                    </td>
                                    <td>
                                        <span class="description">
                                            <%= (Model.View.RecentSeniority == null ? "N/A" : Model.View.RecentSeniority.GetDisplayText())%>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                            
<%  var jobs = (Model.View.Resume == null || Model.View.Resume.Jobs == null) ? null : Model.View.Resume.Jobs;
    if (jobs != null && jobs.Count > 0)
    { %>
                                <table class="jobs">
<%      foreach (var job in jobs)
        { %>
                                        <tr class="job">
                                            <td class="duration">
                                                <span class="date-range">
                                                    <span class="start-date"><%= Html.Encode(job.Dates != null && job.Dates.Start != null && job.Dates.Start.HasValue ? job.Dates.Start.Value.ToString("MMM yyyy") : "N/A")%></span>
                                                    <span class="hyphen">-</span>
                                                    <span class="end-date"><%= Html.Encode(job.IsCurrent ? "Current" : (job.Dates != null && job.Dates.End != null && job.Dates.End.HasValue ? job.Dates.End.Value.ToString("MMM yyyy") : "N/A"))%></span>
                                                </span>
                                            </td>
                                            <td class="description">
                                                <span class="title">
<%          if (!string.IsNullOrEmpty(job.Title))
            { %>
                                                        <a href="javascript:void(0);" onclick="onClickJumpToLink(this);" jump-to-link="Employment" id="job-title_<%=jobs.IndexOf(job)%>"><%=HighlightJobTitle(job.Title)%></a>
<%          }
            else
            { %>
                                                        <span class="empty">Job title not specified</span>
<%          } %>
                                                </span>
                                                <br />
                                                <span class="company">
<%          if (Model.View.CanAccess(ProfessionalVisibility.RecentEmployers))
            {
                if (!string.IsNullOrEmpty(job.Company))
                { %>
                                                            <%= HighlightEmployer(job.Company) %>
<%              }
                else
                { %>
                                                            <span class="empty">Employer not specified</span>
<%              }
            }
            else
            { %>
                                                        &lt;Employer Hidden&gt;
<%          } %>
                                                </span>
                                            </td>
                                            
                                        </tr>                                        
<%      } %>
                                </table>
<%  }
    else
    { %>
                                <table class="jobs">
                                    <tr class="job">
                                        <td class="description">
                                            <span class="title"> N/A </span>
                                        </td>
                                    </tr>
                                </table>
<%  } %>
                        </div>
                    </div>
                    <div class="education-history_section resume_section">
                        <div class="resume-heading">Education history</div>
                        <div class="resume-content">

                            <table class="education-level">
                                <tr>
                                    <td>
                                        <span class="title highest-education-level"> Highest education level: </span>
                                    </td>
                                    <td>
                                        <span class="description"><%= Model.View.HighestEducationLevel != null ? Model.View.HighestEducationLevel.GetDisplayText() : "N/A" %></span>
                                    </td>
                                </tr>
                            </table>
                        
<%  var schools = (Model.View.Resume == null || Model.View.Resume.Schools == null) ? null : Model.View.Resume.Schools;
    if (schools != null && schools.Count > 0)
    { %>
                                <table class="schools">
<%      foreach (var school in schools)
        { %>
                                        <tr class="school">
                                            <td class="duration">
                                                <span class="date-range">
                                                    <span class="completion">Completion Date: </span><span class="end-date"><%= Html.Encode(school.IsCurrent ? "Current" : (school.CompletionDate != null && school.CompletionDate.End != null && school.CompletionDate.End.HasValue ? school.CompletionDate.End.Value.ToString("MMM yyyy") : "N/A"))%></span>
                                                </span>
                                            </td>
                                            <td class="description">
                                                <span class="title">
<%          if (!string.IsNullOrEmpty(school.Degree))
            { %>
                                                        <a href="javascript:void(0);" onclick="onClickJumpToLink(this);" jump-to-link="Education" id="school-degree_<%=schools.IndexOf(school)%>"><%=HighlightEducation(school.Degree)%></a>
<%          }
            else
            { %>
                                                        <span class="empty">Degree not specified</span>
<%          } %>
                                                </span>
                                                <br />
                                                <span class="institution">
<%          if (!string.IsNullOrEmpty(school.Institution))
            { %>
                                                        <%=HighlightEducation(school.Institution)%>
<%          }
            else
            { %>
                                                        <span class="empty">Institution not specified</span>
<%          } %>
                                                </span>
                                            </td>
                                            
                                        </tr>                                        
<%      } %>
                                </table>
<%  }
    else
    { %>
                                <table class="jobs">
                                    <tr class="job">
                                        <td class="description">
                                            <span class="title"> N/A </span>
                                        </td>
                                    </tr>
                                </table>
<%  } %>
                        </div>
                    </div>
                    <% Html.RenderPartial("ResumeSummary", Model); %>
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div>
             
        <% Html.RenderPartial("ResumeContent", Model); %>
<%  if (Model.CurrentApplication != null)
    { %>
        <div class="tabs-container tabs-hide cover-letter-container" id="CoverLetter-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
<%      var coverletter = Model.CurrentApplication.CoverLetterText;
        if (!string.IsNullOrEmpty(coverletter))
        { %>
                               <%=HtmlUtil.LineBreaksToHtml(coverletter)%>
<%      } %>
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div> 
<%  } %>
    </div>
</div>

<script type="text/javascript">
    (function($) {
        /* Initialization for tabs */

        var resumeTabs = $(".js_resume-tabs");
        resumeTabs.makeTabs({
            selectedFlagClass: "js_selected-tab",
            hyperlinkReplacementTag: "span",
            hyperlinkOrReplacementClassesToAdd: "tab-content-outer",
            tabContentSelector: "span",
            tabContentClassesToAdd: "tab-content-inner"
        });

        resumeTabs.find("li").displayResumeContents();

        $(".resume-details .tabs-container:not(.full-resume-container)").addClass("tabs-hide");
        $("#FullResume").click();
        
        $(".js_tooltip").addTooltip();

        
        
<%  if (CurrentEmployer != null)
    { %>
        // Drag.
        
        $(".draggable").draggable({
            revert: 'invalid',
            helper: 'clone',
            cursorAt: { top: 8, left: 8 },
            drag: function(event, ui) {
                $(".ui-draggable-dragging").removeClass("basic-details").removeClass("basic-details-over").removeClass("basic-details-down");
                $(".ui-draggable-dragging").text($(this).find(".candidate-name").text());
            }
        });
        
<%  } %>

    })(jQuery);
</script> 