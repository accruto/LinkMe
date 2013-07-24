<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<EmployerCreditsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Products"%>
<%@ Import Namespace="LinkMe.Web.Domain.Products"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Credits</li>
    </ul>
</asp:Content>
                
<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <% Html.RenderPartial("LeftSideBar"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Credit allocations</h1>
    </div>
    
    <div class="forms_v2">
    
        <div class="section">
            <div class="section-content">
    
            <% if (Model.Employer.Organisation.IsVerified)
               { %>

                <p>Call sales for any enquiry on <%= Constants.PhoneNumbers.FreecallHtml %></p>

            <% } %>
        
                <ul class="actions">
                    <li><%= Html.RouteRefLink("Purchase more credits", ProductsRoutes.NewOrder, null)%></li>
                    <li><%= Html.RouteRefLink("View all orders", ProductsRoutes.Orders, null)%></li>
                </ul>

            </div>
        </div>
        
        <div class="section">
            <div class="section-content">

                <% var allocations = Model.Allocations[Model.Employer.Id];
                   if (allocations.Count > 0) { %>

                <table id="personal-credits" class="list">
                    <thead>
			            <tr>
			                <th>Credit</th>
			                <th>Quantity</th>
			                <th>Expiry date</th>
			                <th>Order</th>
			            </tr>
                    </thead>
                    <tbody>
                    
                        <% for (var index = 0; index < allocations.Count; ++index) { %>
                            <% var allocation = allocations[index]; %>
                        
                            <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                                <td><%= (from c in Model.Credits where c.Id == allocation.CreditId select c).Single().ShortDescription %></td>
                                <td><%= Html.Quantity(allocation.RemainingQuantity) %></td>
                                <td><%= Html.ExpiryDate(allocation.ExpiryDate) %></td>
                                <td><%= allocation.ReferenceId == null
                                        ? MvcHtmlString.Create("<none>")
                                        : Html.RouteRefLink((from o in Model.Orders where o.Id == allocation.ReferenceId select o.ConfirmationCode).SingleOrDefault(), ProductsRoutes.Order, new { id = allocation.ReferenceId.Value })%></td>
                            </tr>
                        <% } %>
                        
                    </tbody>
                </table>

                <% } else { %>

                    <p>You currently have no personal credits.</p>                
                
                <% } %>

            </div>        
        </div>
        
        <% if (Model.Employer.Organisation.IsVerified)
           { %>
        <div class="section">
            <div class="section-title">
                <h1>Organisation credit allocations</h1>
            </div>
        
            <div class="section-content">

                <% if ((from o in Model.OrganisationHierarchy select Model.Allocations[o.Id].Count).Sum() <= 0)
                   { %>
                        <p>
                            There are currently no credits assigned to your organisation.
                        </p>
                <% }
                   else
                   { %>
                
                        <p>
                            These credits are currently assigned to your organisation and are available to use.
                        </p>
                        <table id="organisation-credits" class="list">
                            <thead>
			                    <tr>
			                        <th>Organisation</th>
			                        <th>Credit</th>
			                        <th>Quantity</th>
			                        <th>Expiry date</th>
			                    </tr>
                            </thead>
                            <tbody>
                            
                                <% var line = 0;
                                   foreach (var organisation in Model.OrganisationHierarchy)
                                   {
                                       var organisationAllocations = Model.Allocations[organisation.Id];
                                       for (var index = 0; index < organisationAllocations.Count; ++index)
                                       { %>
                                        <% var allocation = organisationAllocations[index]; %>
                                
                                        <tr class="item item_<%= (line++ % 2) == 0 ? "odd" : "even" %>">
                                            <td><%= organisation.Name%></td>
                                            <td><%= (from c in Model.Credits where c.Id == allocation.CreditId select c).Single().ShortDescription %></td>
                                            <td><%= Html.Quantity(allocation.RemainingQuantity)%></td>
                                            <td><%= Html.ExpiryDate(allocation.ExpiryDate)%></td>
                                        </tr>
                                    <% }
                                   } %>
                                
                            </tbody>
                        </table>

                <% } %>
                
            </div>        
        </div>
        <% } %>                
        
    </div>

    <script type="text/javascript" language="javascript">
        (function($){

            $(".js_folders_collapsible").makeFoldersSectionCollapsible(false);
            $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(false);

            initializeFolders(candidateContext);
            initializeBlockLists();

        })(jQuery);
    </script>

</asp:Content>
