<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SalaryBandsCandidatesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<div class="columns">
    <div class="column">
<%  foreach (var band in Model.SalaryBands.Take(Model.SalaryBands.Count / 2))
    { %>    
        <div>
<%      if (Model.Location == null)
        { %>        
            <%= Html.CandidatesRouteLink(band.GetSalaryBandDisplayText(), band, new { title = band })%>
<%      }
        else
        { %>
            <%= Html.CandidatesRouteLink(band.GetSalaryBandDisplayText(), Model.Location, band, null, new { title = band })%>
<%      } %>            
        </div>
<%  } %>
    </div>
    <div class="column">
<%  foreach (var band in Model.SalaryBands.Skip(Model.SalaryBands.Count / 2))
    { %>    
        <div>
<%      if (Model.Location == null)
        { %>        
            <%= Html.CandidatesRouteLink(band.GetSalaryBandDisplayText(), band, new { title = band })%>
<%      }
        else
        { %>
            <%= Html.CandidatesRouteLink(band.GetSalaryBandDisplayText(), Model.Location, band, null, new { title = band })%>
<%      } %>            
        </div>
<%  } %>
    </div>
</div>
