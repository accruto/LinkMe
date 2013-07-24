<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LocationsCandidatesModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<div class="columns">
    <div class="column">
<%  foreach (var subdivision in Model.CountrySubdivisions.OrderBy(s => s.Name))
    { %>    
        <div>
<%      if (Model.SalaryBand == null)
        { %>        
            <%= Html.CandidatesRouteLink(subdivision.Name, subdivision, new { title = subdivision.Name + " Candidates" })%>
<%      }
        else
        { %>
            <%= Html.CandidatesRouteLink(subdivision.Name, subdivision, Model.SalaryBand, null, new { title = subdivision.Name + " Candidates" })%>
<%      } %>            
        </div>
<%  } %>
    </div>
    <div class="column">
<%  foreach (var region in Model.Regions.OrderBy(r => r.Name))
    { %>    
        <div>
<%      if (Model.SalaryBand == null)
        { %>        
            <%= Html.CandidatesRouteLink(region.Name, region, new { title = region.Name + " Candidates" })%>
<%      }
        else
        { %>
            <%= Html.CandidatesRouteLink(region.Name, region, Model.SalaryBand, null, new { title = region.Name + " Candidates" })%>
<%      } %>            
        </div>
<%  } %>
    </div>
</div>

