<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.CreditSummary" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Products" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<%  if (CurrentEmployer != null && Allocations.Count > 0)
    { %>
    <table class="credit-summary">       
<%      foreach (var allocation in Allocations)
        { %>
        <tr>
            <th class="credit-item"><%= Html.RouteRefLink(allocation.Item1.Description, ProductsRoutes.Credits)%>:</th>
            <td class="credits"><%= allocation.Item2.RemainingQuantity == null ? "" : allocation.Item2.RemainingQuantity.Value.ToString() %> credits</td>
            <td class="expiry">
<%          if (allocation.Item2.RemainingQuantity != 0)
            { %>
                Expires <%= allocation.Item2.ExpiryDate.GetExpiryDateDisplayText() %>
<%          } %>
            </td>
        </tr>
<%      } %>
    </table>
<%  } %>

