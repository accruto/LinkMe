<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<JobAdsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarOnLeft) %>
        <%= Html.StyleSheet(StyleSheets.SearchResults) %>
        <%= Html.StyleSheet(StyleSheets.JQueryWidgets) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.EmployerLoggedInFrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.JobAds) %>
        <%= Html.StyleSheet(StyleSheets.FlagLists) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Error) %>
        <%= Html.StyleSheet(StyleSheets.Overlay) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
        <%= Html.StyleSheet(StyleSheets.JQuerySlider)%>
        <%= Html.StyleSheet(StyleSheets.JQueryTabs)%>
        <%= Html.StyleSheet(StyleSheets.JQueryDragDrop)%>
        <%= Html.StyleSheet(StyleSheets.Pagination) %>
        <%= Html.StyleSheet(StyleSheets.Notes) %>
        <%= Html.StyleSheet(StyleSheets.JQueryFileUploadUi)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftAjax) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftMvcAjax) %>
        <%= Html.JavaScript(JavaScripts.CustomCheckbox) %>
        <%= Html.JavaScript(JavaScripts.AlignWith) %>
        <%= Html.JavaScript(JavaScripts.DesktopMenu) %>
        <%= Html.JavaScript(JavaScripts.TextOverflow) %>
        <%= Html.JavaScript(JavaScripts.Download) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom)%>
        <%= Html.JavaScript(JavaScripts.SectionCollapsible) %>
        <%= Html.JavaScript(JavaScripts.Slider) %>
        <%= Html.JavaScript(JavaScripts.EApi) %>
        <%= Html.JavaScript(JavaScripts.EmployersApi) %>
        <%= Html.JavaScript(JavaScripts.Credits) %>
        <%= Html.JavaScript(JavaScripts.Overlay) %>
        <%= Html.JavaScript(JavaScripts.Actions) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.FlagLists) %>
        <%= Html.JavaScript(JavaScripts.BlockLists) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.Search) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JsonManipulations) %>
        <%= Html.JavaScript(JavaScripts.Tabs) %>
        <%= Html.JavaScript(JavaScripts.ToggleCheckbox) %>
        <%= Html.JavaScript(JavaScripts.Tooltips) %>
        <%= Html.JavaScript(JavaScripts.CenterAlign) %>
        <%= Html.JavaScript(JavaScripts.HoverIntent) %>
        <%= Html.JavaScript(JavaScripts.Notes) %>
        <%= Html.JavaScript(JavaScripts.FileUpload) %>
        <%= Html.JavaScript(JavaScripts.FileUploadUi) %>
    </mvc:RegisterJavaScripts>
</asp:Content>    

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Manage JobAds</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Manage jobads</h1>
    </div>
    
    <div class="manage-jobads">
        
	    <div class="forced-first_section section">
	        <div class="section-title">
                <h2>Open jobads</h2>
            </div>
            <div class="section-content">
            
                <table class="list">
                    <tbody>
<%  for (var index = 0; index < Model.OpenJobAds.Count; ++index)
    {
        var jobAd = Model.OpenJobAds[index];
        var counts = Model.ApplicantCounts[jobAd.Id]; %>
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td>
                                <div class="display-item">
                                    <span style="float:left;"><span id="name-<%= jobAd.Id %>"><%= Html.RouteRefLink(jobAd.Title, JobAdsRoutes.ManageJobAdCandidates, new { jobAdId = jobAd.Id, status = ApplicantStatus.New })%></span> (<%=counts.ShortListed%>)</span>
                                </div>
                            </td>
                        </tr>
                <% } %>
                        
                    </tbody>
                </table>
            
                <ul class="horizontal_action-list action-list">
                    <li><a href="javascript: void(0);" class="add-action js_add-open-jobad">Post a job ad</a></li>
                </ul>
            </div>
	    </div>
	    
	    <div class="section">
	        <div class="section-title">
                <h2>Closed jobads</h2>
            </div>
            <div class="section-content">
               
                <table class="list">
                    <tbody>
                
<%  for (var index = 0; index < Model.ClosedJobAds.Count; ++index)
    {
        var jobAd = Model.ClosedJobAds[index];
        var counts = Model.ApplicantCounts[jobAd.Id]; %>
                        
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td>
                                <div class="display-item">
                                    <span style="float:left;"><span id="name-<%=jobAd.Id%>"><%= Html.RouteRefLink(jobAd.Title, JobAdsRoutes.ManageJobAdCandidates, new { jobAdId = jobAd.Id, status = ApplicantStatus.New })%></span> (<%=counts.ShortListed%>)</span>
                                </div>
                            </td>
                        </tr>
                <% } %>
                        
                    </tbody>
                </table>

            </div>
	    </div>
	    <div class="overlay-container forms_v2">
	        <div class="folder-overlay shadow" style="display:none;">
		        <div class="overlay">
			        <div class="overlay-title"><span class="overlay-title-text"> Title </span><span class="close-icon"></span></div>
			        <div class="overlay-content">
			             <div class="overlay-text">Text goes here</div>
			             <div class="buttons-holder">
	                     </div>
			        </div>
		        </div>
	        </div>
        </div>
    </div>
</asp:Content>

