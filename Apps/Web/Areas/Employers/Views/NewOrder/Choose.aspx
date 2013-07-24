<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<NewOrderChooseModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Products"%>
<%@ Import Namespace="LinkMe.Web.Domain.Products"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Products"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Products.Choose) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Employers.Products.Choose) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">New order</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
        <div class="section-body">
            <% Html.RenderPartial("WizardSteps", Model.Steps); %>
        </div>
    </div>
    <div class="section">
        <div class="section-body">
            <div id="order-compact-details">
                <% Html.RenderPartial("OrderCompactDetails", Model.OrderDetails); %>
            </div>
        </div>
    </div>
    <div class="hint_section section">
        <div class="section-body">
            Please call <span class="free-call-phone"><%= Constants.PhoneNumbers.FreecallHtml %></span> to discuss
            'unlimited usage' and other billing options.
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Choose credits</h1>
    </div>

<%  using (Html.RenderForm(Model.GetUrl(Context.GetClientUrl())))
    { %>
        <div class="search-resumes-section shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Search resumes</h2>
                    <div class="tagline">Immediate access to candidates you won't find anywhere else.</div>
                </div>
                <div class="section-content">
                    <div class="self-clearing">
                        <div class="product">
                            <ul class="features">
                                <li class="feature">Search for candidates and view resumes for free.</li>
                                <li class="feature">Contact candidates immediately via email or telephone.</li>
                                <li class="feature">Receive candidate alerts when "hot" candidates join.</li>
                                <li class="feature">Contact candidates on the move: <%= Html.RouteRefLink("download", HomeRoutes.CandidateConnect, null, new { @class = "learnmore" })%> "Candidate Connect" for iPhone/iPad.</li>
                            </ul>
                        </div>
                        <div id="less-features">
                            <a id="features-more-details" class="more-details" href="javascript:void(0);">More details</a>
                        </div>
                        <div id="more-features" style="display: none">
                            <a id="features-hide-details" class="less-details" href="javascript:void(0);">Hide details</a>
                            <div class="product">
                                <ul class="features">
                                    <li class="feature">Advanced search allows targetting of candidates who are immediately available, Indigenous, willing to relocate, and many more criteria.</li>
                                    <li class="feature">Contact credits must be purchased to unlock a candidate's record. Unlocking a candidate record allows you to reveal personal details, their phone number, download their resume and send an email.</li>
                                    <li class="feature">Unlimited access available – call <%= Constants.PhoneNumbers.FreecallHtml %> for more details.</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="product-cost">
                        <h3>Candidate contacts</h3>
                        <dl id="product-cost-total" class="total">
                            <dt>Total:</dt>
                            <dd><span id="product-cost-total-price"></span><span class="plus-gst"> (plus GST)</span></dd>
                        </dl>
                        <select id="ContactProductId" name="ContactProductId">
<%      for (var i = 0; i < Model.ContactProducts.Count; ++i)
        {
            var product = Model.ContactProducts[i];
            var adjustment = product.GetPrimaryCreditAdjustment(); %>
                            <option value="<%= product.Id %>" <%= Model.SelectedContactProductId == product.Id ? "selected='selected'" : "" %>><%= Html.Quantity(adjustment.Quantity) %></option>
<%      } %>
                        </select>
                        <span class="at-n-each">@ <span class="product-price" id="product-cost-price"></span> each</span>
                    </div>
                </div>
            </div>
            <div class="section-foot"></div>
            <div class="section-icon"></div>
        </div>
        <div class="form">
            <div class="right-buttons-section section">
                <div class="section-body">
                    <%= Html.ButtonsField(new PurchaseButton()) %>
                </div>
            </div>
        </div>
<%  } %>
    
    <script type="text/javascript">
        $(document).ready(function () {
            linkme.employers.products.choose.ready({
                urls: {
                    prepareCompactOrderUrl: '<%= Html.RouteRefUrl(ProductsRoutes.PrepareCompactOrder) %>'
                },
                products: [
<%  foreach (var product in Model.ContactProducts)
    {
        var adjustment = product.GetPrimaryCreditAdjustment(); %>
                    {
                        id: '<%= product.Id %>',
                        pricePerCredit: '<%= Html.PricePerCredit(product.Price, adjustment.Quantity, product.Currency) %>',
                        price: '<%= Html.Price(product.Price, product.Currency) %>'
                    },
<%  } %>                            
                ]
            });
        });
    </script>

</asp:Content>
