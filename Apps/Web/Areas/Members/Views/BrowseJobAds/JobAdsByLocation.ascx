<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LocationsJobAdsModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<div class="columns">
    <div class="column">
<%  foreach (var subdivision in Model.CountrySubdivisions.OrderBy(s => s.Name))
    { %>    
        <div>
<%      if (Model.Industry == null)
        { %>        
            <%= Html.JobAdsRouteLink(subdivision.Name + " jobs", subdivision, new { title = subdivision.Name + " jobs" })%>
<%      }
        else
        { %>
            <%= Html.JobAdsRouteLink(subdivision.Name + " jobs", subdivision, Model.Industry, new { title = subdivision.Name + " jobs" })%>
<%      } %>            
        </div>
<%  } %>
    </div>
    <div class="column">
<%  foreach (var region in Model.Regions.OrderBy(r => r.Name))
    { %>    
        <div>
<%      if (Model.Industry == null)
        { %>        
            <%= Html.JobAdsRouteLink(region.Name + " jobs", region, new { title = region.Name + " jobs" })%>
<%      }
        else
        { %>
            <%= Html.JobAdsRouteLink(region.Name + " jobs", region, Model.Industry, new { title = region.Name + " jobs" })%>
<%      } %>            
        </div>
<%  } %>
    </div>
</div>

