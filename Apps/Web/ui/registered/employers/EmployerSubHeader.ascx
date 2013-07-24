<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EmployerSubHeader.ascx.cs" Inherits="LinkMe.Web.UI.Registered.Employers.EmployerSubHeader" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Products"%>

<%  if (LoggedInEmployer != null && Allocations.Count > 0)
    { %>
    <table class="credit-summary">       
<%      foreach (var allocation in Allocations)
        { %>
        <tr>
            <th class="credit-item"><a href="<%= ProductsRoutes.Credits.GenerateUrl() %>"><%= allocation.Item1.Description %></a>:</th>
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
