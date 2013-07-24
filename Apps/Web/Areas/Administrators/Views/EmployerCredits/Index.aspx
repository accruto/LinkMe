<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Administrators.Models.Employers.EmployerCreditsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Domain.Products" %>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.JQueryUiAll) %>
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Administrators) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
		        <li><%= Html.RouteRefLink(Model.Employer.FullName, EmployersRoutes.Edit, new { id = Model.Employer.Id }) %></li>
                <li><%= Html.RouteRefLink("View credit usage", EmployersRoutes.Usage, new { id = Model.Employer.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Employer credits</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>New credits</h1>
                </div>
        
                <%= Html.CreditField(Model, m => m.CreditId, Model.Credits).WithLabel("Credit") %>
                <%= Html.TextBoxField(Model, m => m.Quantity)
                    .WithExampleText("Leave blank to specify unlimited credits or enter a number") %>
                <%= Html.DateField(Model, m => m.ExpiryDate)
                    .WithLabel("Expiry date")
                    .WithButton(Images.Block.Calendar)
                    .WithFormat("dd/MM/yyyy")
                    .WithExampleText("Leave blank to specify credits that never expire or enter a date")%>
                <%= Html.ButtonsField(new AddButton()) %>
                   
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

        <div class="section">
            <div class="section-body">
                <div class="section-title">
                    <h1>Existing credits</h1>
                </div>

<%  var allocations = Model.Allocations[Model.Employer.Id];
    if (allocations.Count == 0)
    { %>
                <div id="credits-header">
                    There are no credits allocated to this employer.
                </div>
<%  }
    else
    { %>
                <table id="credits" class="list">
                    <thead>
                        <tr>
			                <th class="credit-column-header">Credit</th>
			                <th class="status-column-header">Status</th>
			                <th class="initial-quantity-column-header">Initial Quantity</th>
			                <th class="remaining-quantity-column-header">Remaining Quantity</th>
			                <th class="expirydate-column-header">Expiry date</th>
			                <th class="usage-column-header"></th>
  			                <th class="order-column-header">Order</th>
    			            <th class="deallocate-column-header"></th>
                        </tr>
                    </thead>
                    <tbody>
                            
<%      for (var index = 0; index < allocations.Count; ++index)
        {
            var allocation = allocations[index]; %>
                                   
                    <tr id="<%= allocation.Id %>" class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                        <td class="credit-column"><%= (from c in Model.Credits where c.Id == allocation.CreditId select c).Single().ShortDescription %></td>
                        <td class="status-column"><%= Html.Status(allocation) %></td>
                        <td class="initial-quantity-column"><%= Html.Quantity(allocation.InitialQuantity)%></td>
                        <td class="remaining-quantity-column"><%= Html.Quantity(allocation.RemainingQuantity)%></td>
                        <td class="expirydate-column"><%= Html.ExpiryDate(allocation.ExpiryDate) %></td>
                        <td class="usage-column"><%= Html.RouteRefLink("Usage", EmployersRoutes.AllocationUsage, new { id = Model.Employer.Id, allocationId = allocation.Id })%></td>
                        <td class="order-column">
                            <%= allocation.ReferenceId == null
                                ? Html.Encode("<none>")
                                : Html.Encode((from o in Model.Orders where o.Id == allocation.ReferenceId select o.ConfirmationCode).SingleOrDefault()) %>
                        </td>
                	    <td class="deallocate-column">
<%          if (allocation.IsActive)
            { %>
                            <input type="submit" value="" class="deallocate mini-light-x-button button" />
<%          } %>
                	    </td>
                    </tr>
<%      } %>
                </tbody>
            </table>
                        
<%  } %>
            </div>
        </div>

    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.administrators.credits.ready({
                urls: {
                    apiDeallocateUrl: '<%= EmployersRoutes.ApiDeallocate.GenerateUrl(new { id = Model.Employer.Id }) %>'
                }
            });
        });
    </script>
            
</asp:Content>
