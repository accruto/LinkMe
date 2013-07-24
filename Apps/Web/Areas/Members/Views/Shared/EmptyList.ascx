<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="emptylist">
<%  if (Model.ListType == JobAdListType.SearchResult)
    { %>
        <span>Unfortunately, there aren't any jobs matching your criteria. Please change keywords or location, or reapply other filters.</span>
<%  }
    if (Model.ListType == JobAdListType.BrowseResult)
    { %>
        <span>Unfortunately, there aren't any jobs in this category. Please try a different category for jobs.</span>
<%  }
    if (Model.ListType == JobAdListType.SuggestedJobs)
    { %>
        <span>Unfortunately, there aren't any suggested jobs for you at the moment.</span>
<%  }
    if (Model.ListType == JobAdListType.SimilarJobs)
    { %>
        <span>Unfortunately, there aren't any similar jobs at the moment.</span>
<%  }
    if (Model.ListType == JobAdListType.FlagList)
    { %>
        <span>This folder does not contain any jobs.</span>
        <span>The Flagged folder is used to collect all jobs you have flagged during your searches. You can flag jobs by clicking on the flag in the top-right of any job listing. You can clear flags by deselecting individual jobs, or by using the "Clear flags" next to the Flagged folder or at the top of any search.</span>
<%  }
    if (Model.ListType == JobAdListType.Folder)
    { %>
        <span>This folder does not contain any jobs.</span>
        <span>You can add jobs to a folder from your search results by using the Folder action at the bottom of each job.</span>
<%  }
    if (Model.ListType == JobAdListType.BlockList)
    { %>
        <span>This folder does not contain any jobs.</span>
<%  } %>
</div>
<%  if (Model.Results.JobAds == null || Model.Results.JobAds.Count == 0)
    { %>
    <div class="row empty">
        <div class="topbar"></div>
        <div class="bg">
            <div class="column tick">
                <div class="icon new"></div>
                <div class="checkbox"></div>
            </div>
            <div class="column jobtype">
                <div class="icon types"></div>
                <div class="subtypes">
                    <div class="subtype FullTime"></div>
                    <div class="subtype PartTime"></div>
                    <div class="subtype Contract"></div>
                    <div class="subtype Temp"></div>
                    <div class="subtype JobShare"></div>
                </div>
            </div>
            <div class="column title">
                <a href="" class="title" title=""></a>
                <div class="company"></div>
                <div class="location" title=""></div>
                <div class="action">
                    <div class="icon folder"></div>
                    <div class="icon note"><span class="count">(0)</span></div>
                    <div class="icon email"></div>
                    <div class="icon download"></div>
                    <div class="icon hide"></div>
                    <div class="icon restore"></div>
                </div>
            </div>
            <div class="column info">
                <div class="icon viewed"></div>
                <div class="icon applied"></div>
            </div>
            <div class="divider"></div>
            <div class="salary"></div>
            <div class="divider"></div>
            <div class="date"></div>
            <div class="column flag">
                <div class="flag"></div>
                <div class="button expand"></div>
            </div>
        </div>
        <div class="details collapsed">
            <div class="divider"></div>
            <div class="bg">
                <div class="summary">
                    <div class="title">Summary:</div>
                    <ul class="bulletpoints"></ul>
                </div>
                <div class="industry">
                    <div class="title">Industry:</div>
                    <div class="desc"></div>
                </div>
                <div class="description"></div>
            </div>
        </div>
        <div class="bottombar"></div>
    </div>
<%  } %>