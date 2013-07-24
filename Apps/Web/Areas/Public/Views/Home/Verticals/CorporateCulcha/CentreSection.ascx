<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.CentreSection" %>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>Featured Employers</h1>
    </div>
    <div class="home-editable-section-content">
        <table class="featured-employers" border="0" cellspacing="0" cellpadding="0" align="center">
            <tbody>
                <tr>
                    <td align="center">
<%  foreach (var employer in TopFeaturedEmployers)
    { %>                
                        <a href="<%= employer.SearchUrl %>"><img style="margin: <%= employer.Margin %>;" src="<%= employer.ImageUrl %>" alt="<%= employer.Alt %>" /></a>
<%  } %>
                        <br />
<%  foreach (var employer in BottomFeaturedEmployers)
    { %>                
                        <a href="<%= employer.SearchUrl %>"><img style="margin: <%= employer.Margin %>;" src="<%= employer.ImageUrl %>" alt="<%= employer.Alt %>" /></a>
<%  } %>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>        
</div>
