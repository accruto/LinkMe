<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search" %>

<script language="javascript" type="text/javascript">
<%  if (Model.ListType == JobAdListType.SearchResult)
    {
        var hash = ((JobAdSearchListModel)Model).Criteria.GetHash();
        if (!string.IsNullOrEmpty(hash))
        { %>
            if (setHash) {
                window.location.hash = "#<%=hash%>";
            }
<%      }
    } %>
</script>    

<%  if (Model.ListType == JobAdListType.SearchResult || Model.ListType == JobAdListType.BrowseResult)
    { %>
    <div class="criteriahtml"><%= ((JobAdSearchListModel)Model).Criteria == null ? null : ((JobAdSearchListModel)Model).Criteria.GetCriteriaHtml() %></div>
    <div class="querystringforga"><%= new AdSenseQueryGenerator(new JobAdSearchCriteriaAdSenseConverter()).GenerateAdSenseQuery(((JobAdSearchListModel)Model).Criteria) %></div>
<%  } %>
<div class="hits">
<%  if (Model.Results.IndustryHits != null)
    {
        foreach (var i in Model.Results.IndustryHits)
        { %>
            <div class="industryhits" id="<%= i.Key %>"><%= i.Value%></div>
<%      }
    }
    if (Model.Results.JobTypeHits != null)
    {
        foreach (var j in Model.Results.JobTypeHits)
        { %>
            <div class="jobtypehits" id="<%= j.Key %>"><%= j.Value %></div>
<%      }
    } %>
    <div class="total"><%= Model.Results.TotalJobAds %></div>
</div>
<%  foreach (var jobAdId in Model.Results.JobAdIds)
    { %>
        <% Html.RenderPartial("JobAdListView", Model.Results.JobAds[jobAdId]); %>
<%  } %>