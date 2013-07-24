<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Candidates.ResumeContent" %>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Candidates"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<%  var view = Model.View; %>

        <div class="tabs-container employment-container" id="Employment-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
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
                        
<%  var jobsDetails = (Model.View.Resume == null || Model.View.Resume.Jobs == null) ? null : Model.View.Resume.Jobs;
    if (jobsDetails != null && jobsDetails.Count > 0)
    { %>
                                <table class="jobs">
<%      foreach (var job in jobsDetails)
        { %>
                                        <tr class="job">
                                            <td class="description">
                                                <span class="title">
                                                    <a href="javascript:void(0);" id="anchor_job-title_<%=jobsDetails.IndexOf(job)%>">
<%          if (!string.IsNullOrEmpty(job.Title))
            { %>
                                                        <%=HighlightJobTitle(job.Title)%>
<%          }
            else
            { %>
                                                        <span class="empty">Job title not specified</span>
<%          } %>
                                                    </a>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="duration">
                                                <span class="date-range">
                                                    <span class="start-date"><%= Html.Encode(job.Dates != null && job.Dates.Start != null && job.Dates.Start.HasValue ? job.Dates.Start.Value.ToString("MMM yyyy") : "N/A")%></span>
                                                    <span class="hyphen">-</span>
                                                    <span class="end-date"><%= Html.Encode(job.IsCurrent ? "Current" : (job.Dates != null && job.Dates.End != null && job.Dates.End.HasValue ? job.Dates.End.Value.ToString("MMM yyyy") : "N/A"))%></span>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="company">
<% if (!string.IsNullOrEmpty(job.Company))
                { %>
                                                            <%=HighlightEmployer(job.Company)%>
<%              }
            else
            { %>
                                                        &lt;Employer hidden&gt;
<%          } %>
                                                </span>
                                            </td>                                        
                                        </tr>
                                        <tr>
                                            <td class="last-data">
                                                <span class="description">
<%          if (!string.IsNullOrEmpty(job.Description))
            { %>
                                                        <%=HighlightKeywords(job.Description)%>
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
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div>
        <div class="tabs-container education-container" id="Education-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
                    <div class="education-history_section resume_section">
                        <div class="resume-heading">Qualifications</div>
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

<%  var schoolsDetails = (Model.View.Resume == null || Model.View.Resume.Schools == null)
        ? null
        : Model.View.Resume.Schools;
    if (schoolsDetails != null && schoolsDetails.Count > 0)
    { %>
                                <table class="schools">
<%      foreach (var school in schoolsDetails)
        { %>
                                        <tr class="school">
                                            <td class="description">
                                                <span class="title">
                                                    <a href="javascript:void(0);" id="anchor_school-degree_<%=schoolsDetails.IndexOf(school)%>">
<%          if (!string.IsNullOrEmpty(school.Degree))
            { %>
                                                        <%=HighlightEducation(school.Degree)%>
<%          }
            else
            { %>
                                                        <span class="empty">Degree not specified</span>
<%          } %>
                                                    </a>
                                                </span>
                                             </td>
                                         </tr>
                                        <tr class="school">
                                            <td class="duration">
                                                <span class="date-range">
                                                    <span class="completion">Completion Date: </span><span class="end-date"><%= Html.Encode(school.IsCurrent ? "Current" : (school.CompletionDate != null && school.CompletionDate.End != null && school.CompletionDate.End.HasValue ? school.CompletionDate.End.Value.ToString("MMM yyyy") : "N/A"))%></span>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="last-data">
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
                        <div class="resume-heading">Courses</div>
                        <div class="resume-content">
<%  var courses = (Model.View.Resume == null || Model.View.Resume.Courses == null) ? null : Model.View.Resume.Courses;
    if (courses != null && courses.Count > 0)
    { %>
                                <table class="courses">
<%      foreach (var course in courses)
        { %>
                                        <tr>
                                            <td class="description">
                                                <span class="description">
<%          if (!string.IsNullOrEmpty(course))
            { %>
                                                        <%=HighlightEducation(course)%>
<%          }
            else
            { %>
                                                        <span class="empty">N/A</span>
<%          } %>
                                                </span>
                                             </td>
                                         </tr>                              
<%      } %>
                                </table>
<%  }
    else
    { %>
                                <table class="courses">
                                    <tr class="course">
                                        <td class="description">
                                            <span class="description"> N/A </span>
                                        </td>
                                    </tr>
                                </table>
<%  } %>
                        </div>
                        <div class="resume-heading">Professional certifications</div>
                        <div class="resume-content">
<%  var professional = (view.Resume == null || view.Resume.Professional == null) ? null : view.Resume.Professional;
    if (!string.IsNullOrEmpty(professional))
    { %>
                                    <%=HighlightEducation(professional)%>
<%  }
    else
    { %>
                                    Not specified
<%  } %>
                        </div>
                    </div>
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div>
        <div class="tabs-container skills-container" id="Skills-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
                    <div class="skills_section resume_section">
                        <div class="resume-heading">Skills</div>
                        <div class="resume-content">
<%  var skills = (view.Resume == null || view.Resume.Skills == null) ? null : view.Resume.Skills;
    if (!string.IsNullOrEmpty(skills))
    { %>
                               <%=HtmlUtil.LineBreaksToHtml(HighlightEducation(skills))%>
<%  } %>
                        </div>
                    </div>
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div>
        <div class="tabs-container professional-container" id="Professional-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
                    <div class="professional_section resume_section">
                        <div class="resume-heading">Professional</div>
                        <div class="resume-content">                      
                            <table class="professional">
                                 <tr>
                                    <td>
                                        <span class="title"> Industries: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Industries != null && view.Industries.Count > 0)
    {
        var i = 0;
        foreach (var industry in view.Industries)
        {
            if (i++ < view.Industries.Count - 1)
            { %>
                <%=industry.ToString()%>,
<%          }
            else
            { %>
                <%=industry.ToString()%>
<%          }
        }
    } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Desired job: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (!string.IsNullOrEmpty(view.DesiredJobTitle))
    { %>
                                                <span class="detail-title">Desired jobs:</span>
                                                <span class="detail-content"><%=HighlightDesiredJobTitle(view.DesiredJobTitle)%></span><br />
<%  }
    if (!string.IsNullOrEmpty(view.DesiredJobTypes.GetDesiredClauseDisplayText()))
    { %>
                                                <span class="detail-title">Desired job type:</span>
                                                <span class="detail-content"><%=Html.Encode(view.DesiredJobTypes.GetDesiredClauseDisplayText())%></span><br />
<%  } %>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="title"> Desired salary: </span>
                                    </td>
                                    <td>
                                        <span class="description">
                                            <span class="detail-content">
<%  if (view.DesiredSalary != null && view.DesiredSalary.LowerBound != null)
    {
        if (view.DesiredSalary.Rate == SalaryRate.Hour)
        { %>
                                                    <span class="detail-content"><%= view.DesiredSalary.GetDisplayText() %> (total remuneration per hour)</span><br />
                                                    <span class="detail-content">Equivalent to approximately <%= view.DesiredSalary.ToRate(SalaryRate.Year).GetDisplayText() %> per annum</span>
<%      }
        else
        { %>
                                                    <span class="detail-content"><%= view.DesiredSalary.ToRate(SalaryRate.Year).GetDisplayText() %> (total remuneration per annum)</span>
<%      }
    }
    else
    { %>
                                                    <span class="empty"></span>
<%  } %>
                                            </span>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Career objective: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.Resume.Objective))
    { %>                                            
                                                   <%=HighlightKeywords(view.Resume.Objective)%>
<%  }
    else
    { %>
                                                <span class="empty"></span>
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Affiliations: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.Resume.Affiliations))
    { %>                                            
                                                   <%=HighlightKeywords(view.Resume.Affiliations)%>
<%  }
    else
    { %>
                                                <span class="empty"></span>
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Awards: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Resume != null && view.Resume.Awards != null && view.Resume.Awards.Count > 0)
    {
        foreach (var award in view.Resume.Awards)
        { %>
                                                   <%=HighlightEducation(award)%>
<%      }
    }
    else
    { %>
                                                <span class="empty"></span>
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> References: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.Resume.Referees))
    { %>                                            
                                                   <%=Html.Encode(view.Resume.Referees)%>
<%  }
    else
    { %>
                                                <span class="empty"></span>
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.Resume.Other))
    { %>
                                 <tr>
                                    <td>
                                        <span class="title"> Other: </span>
                                    </td>
                                    <td>
                                        <span class="description">                      
                                            <%=HighlightKeywords(view.Resume.Other)%>
                                        </span>
                                     </td>
                                 </tr>
<%  } %>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div>
        <div class="tabs-container personal-container" id="Personal-details">
            <div class="resume-header-bg"></div>
            <div class="resume-bg">
                <div class="tabs-inner-container">
                    <div class="personal_section resume_section">
                        <div class="resume-heading">Personal</div>
                        <div class="resume-content">                      
                            <table class="personal">
                                <tr>
                                    <td>
                                        <span class="title"> Location: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Address != null && view.Address.Location != null)
    {
        if (view.Address.Location.IsCountry)
        { %>
                                            <%=Html.Encode(view.Address.Location.Country)%>
<%      }
        else
        { %>            
                                            <%=Html.Encode(view.Address.Location)%><br />
                                            <%=Html.Encode(view.Address.Location.Country)%>
<%      }
    }
    else
    { %>
                                                <span class="empty"></span>
<%  } %>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="title"> Contact details: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if ((view.CanContact() == CanContactStatus.YesWithoutCredit && view.HasBeenAccessed) || view.CanContact() == CanContactStatus.YesWithCredit)
    {
        if (view.PhoneNumbers != null)
        {
            foreach (var phoneNumber in view.PhoneNumbers)
            {
                if (phoneNumber.Type == PhoneNumberType.Mobile)
                { %>
                                            Mobile : <%=Html.Encode(phoneNumber.Number)%><br />
<%              }
                else if (phoneNumber.Type == PhoneNumberType.Work)
                { %>
                                            Work : <%=Html.Encode(phoneNumber.Number)%><br />
<%              }
                else if (phoneNumber.Type == PhoneNumberType.Home)
                { %>
                                            Home : <%=Html.Encode(phoneNumber.Number)%><br />
<%              }
            }
        }
    }
    else
    {
        if (view.PhoneNumbers != null && view.PhoneNumbers.Count > 0)
        { %>
                                            Available - Please unlock the candidate to view details.
<%      }
    } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Relocation: </span>
                                    </td>
                                    <td>
                                        <span class="description">
                                            Willingness : <%= Html.Encode(view.RelocationPreference == RelocationPreference.WouldConsider ? "Would consider relocating" : view.RelocationPreference.ToString()) %><br />
<%  if (view.RelocationPreference != RelocationPreference.No)
    {
        if (view.RelocationLocations != null && view.RelocationLocations.Count > 0)
        {
            var locations = (from l in view.RelocationLocations select Html.Encode(l.IsCountry ? l.Country.ToString() : l.ToString())).OrderBy(l => l).ToArray(); %>
                                            To : <%= string.Join(", ", locations) %>
<%      }
    } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Interests: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.Resume.Interests))
    { %>
                                               <%=HighlightKeywords(view.Resume.Interests)%>
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Citizenship: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.Resume.Citizenship))
    { %>
                                               <%=Html.Encode(view.Resume.Citizenship)%>
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        <span class="title"> Visa status: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%  if (view.VisaStatus != null)
    { %>
                                               <%=Html.Encode(view.VisaStatus.GetDisplayText())%>
<%  } else { %>
                                               N/A
<%  } %>
                                        </span>
                                     </td>
                                 </tr>
<%  if (view.Resume != null && !string.IsNullOrEmpty(view.EthnicStatus.ToString()) && !(view.EthnicStatus.ToString().Equals("0")))
    { %>
                                 <tr>
                                    <td>
                                        <span class="title"> Indigenous status indicated: </span>
                                    </td>
                                    <td>
                                        <span class="description">
<%
        var flag = false;
        foreach (var ethnicStatus in EthnicStatusDisplay.Values)
        {
            if (view.EthnicStatus.IsFlagSet(ethnicStatus))
            {
                flag = true;%>
                                                <%=Html.Encode(ethnicStatus.GetDisplayText())%><br />
<%          }
        }
        if (!flag) { %>N/A<% } %>
                                        </span>
                                     </td>
                                 </tr>
<%  } %>
                             </table>
                         </div>
                     </div>
                </div>
            </div>
            <div class="resume-footer-bg"></div>
        </div> 
