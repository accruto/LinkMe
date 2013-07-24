<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<LinkMe.Web.Areas.Shared.Models.JobAdViewModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Domain" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds" %>

<%  var jobTypes = Model.JobAd.Description.JobTypes.GetOrderedJobTypes(); %>

    <div id="jobad-view">
        <div class="titlebar <%= CurrentMember == null ? "notloggedin" : "" %>">
<%  if (Model.JobAd.IsNew())
    { %>
        <div class="isnew"></div>
<%  } %>
            <div class="jobtype <%= jobTypes.Count > 0 ? jobTypes[0] : JobTypes.None %>"></div>
            <div class="title"><h1 title="<%= Model.JobAd.Title %>"><%= Model.JobAd.Title %></h1></div>
            <div class="subtypes">
<%  foreach (var jt in new[] { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare })
    { %>
                <div class="icon <%= jt %> <%= jobTypes.Count > 0 && jobTypes[0].Equals(jt) ? "hide" : "" %> <%= jobTypes.Count > 1 && jobTypes.Skip(1).Contains(jt) ? "active" : "" %>"></div>
<%  } %>
                <span>Job-type</span>
            </div>
            <div class="viewed">
                <div class="icon <%= Model.JobAd.Applicant.HasViewed ? "active" : "" %>"></div>
                <div class="text">Viewed</div>
            </div>
            <div class="applied">
                <div class="icon <%= Model.JobAd.Applicant.HasApplied ? "active" : "" %>"></div>
                <div class="text">Applied</div>
            </div>
            <div class="notes">
                <div class="icon"></div>
                <div class="text">Notes</div>
                <div class="count">(0)</div>
            </div>
            <div class="menupan note">
                <div class="add">
                    <div class="icon"></div>
                    <div class="text">Add note</div>
                </div>
                <div class="row empty">
                    <div class="title">
                        <div class="date"></div>
                        <div class="buttons">
                            <div class="icon edit"></div>
                            <div class="text edit">Edit</div>
                            <div class="icon delete"></div>
                            <div class="text delete">Delete</div>
                        </div>
                    </div>
                    <div class="content"></div>
                </div>
                <div class="editarea">
                    <%= Html.MultilineTextBoxField("Notes", "").WithLabel("").WithAttribute("row", "5").WithAttribute("maxlength", "500") %>
                    <div class="button save"></div>
                    <div class="button cancel eighty"></div>
                </div>
            </div>
        </div>
        <div class="bg">
        
<%  if (!string.IsNullOrEmpty(Model.OrganisationCssFile))
        Html.RenderPartial("JobAdViewCustomContent", Model.JobAd);
    else
        Html.RenderPartial("JobAdViewContent", Model.JobAd); %>

        </div>
        <div class="bottom-bar"></div>
    </div>

