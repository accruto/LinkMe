<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.CentreSection" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1><font color="black">Featured Employers</font></h1>
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

<div class="tile second_centre-tile centre-tile featured-partners-tile">
    <div class="home-editable-section-title">
        <h1>Featured Partners</h1>
    </div>
    <div class="home-editable-section-content">
        <table class="featured-employers" border="0" cellspacing="0" cellpadding="0" align="center">
            <tbody>
                <tr>
                    <td align="center">
<%  foreach (var partner in TopFeaturedPartners)
    { %>                
                        <a href="<%= partner.SearchUrl %>"><img style="margin: <%= partner.Margin %>;" src="<%= partner.ImageUrl %>" alt="<%= partner.Alt %>" /></a>
<%  } %>
                        <br />
<%  foreach (var partner in BottomFeaturedPartners)
    { %>                
                        <a href="<%= partner.SearchUrl %>"><img style="margin: <%= partner.Margin %>;" src="<%= partner.ImageUrl %>" alt="<%= partner.Alt %>" /></a>
<%  } %>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
        
</div>
