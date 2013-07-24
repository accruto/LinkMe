<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IndustriesJobAdsModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<%  var industries = (from i in Model.Industries where i.Name != "Other" select i).Concat(from i in Model.Industries where i.Name == "Other" select i).ToList(); %>

<div class="columns">
    <div class="column">
<%  foreach (var industry in industries.Take(industries.Count / 2))
    { %>    
        <div>
<%      if (Model.Location == null)
        { %>        
            <%= Html.JobAdsRouteLink(industry.Name + " jobs", industry, new { title = industry.Name + " jobs" })%>
<%      }
        else
        { %>
            <%= Html.JobAdsRouteLink(industry.Name + " jobs", Model.Location, industry, new { title = industry.Name + " jobs" })%>
<%      } %>            
        </div>
<%  } %>
    </div>
    <div class="column">
<%  foreach (var industry in industries.Skip(industries.Count / 2))
    { %>    
        <div>
<%      if (Model.Location == null)
        { %>        
            <%= Html.JobAdsRouteLink(industry.Name + " jobs", industry, new { title = industry.Name + " Jobs" })%>
<%      }
        else
        { %>
            <%= Html.JobAdsRouteLink(industry.Name+ " jobs", Model.Location, industry, new { title = industry.Name + " jobs" })%>
<%      } %>            
        </div>
<%  } %>
    </div>
</div>
