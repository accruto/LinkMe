<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Site.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Register TagPrefix="cv" Namespace="LinkMe.Web.Cms.ContentDisplayViews" Assembly="LinkMe.Web" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>        
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
        <%= Html.StyleSheet(StyleSheets.FrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Jobs) %>
        <%= Html.StyleSheet(StyleSheets.BrowseJobAds) %>
        <%= Html.StyleSheet(StyleSheets.Login) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.JQueryCarouselLite) %>
        <%= Html.JavaScript(JavaScripts.Homepage) %>
        <%= Html.JavaScript(JavaScripts.CustomCheckbox) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">

    <% Html.RenderPartial("BannerHeader"); %>

    <div id="container">
    
        <div id="header">
            <% Html.RenderPartial("PageHeader"); %>
        </div>

        <div id="home-form-container">
        
            <div id="body-container">
            
                <div class="login_section px235_glassy_section glassy_section section">
                    <div class="section-head"></div>
                    <div class="section-body">
                        <div class="section-content login-section">
                            <% Html.RenderPartial("Login"); %>
                        </div>
                    </div>
                    <div class="section-foot"></div>
                </div>
                
                <div id="upper-sections-container">
                    <div id="main-message-container">
                        <% Html.RenderPartial("SplashImage"); %>
                        <div id="main-editable-content-container">
                            <% Html.RenderPartial("MainSection"); %>
	                        <br class="clearer" />
                        </div>
                    </div>
                    
                    <div id="form-container">
                        <div class="front-page-join_section section">
                            <% Html.RenderPartial("JoinHeader"); %>

                            <div class="section-content">
                                <% Html.RenderPartial("Join"); %>
                            </div>

                        </div>
                        <div class="front-page-join_section-bottom"></div>

                        <div id="privacy"> 
                            <p><strong>Privacy: </strong> You can be visible or anonymous. We never spam.</p>
                        </div>
                    </div>
                </div>

                <div id="lower-sections-container">
                    <div class="clearer"></div>

                    <div class="tiles">
                        <div class="tile left-tile">
                            <% Html.RenderPartial("LeftSection", Model.Reference); %>
                        </div>
                        
                        <div class="centre-tiles">
                            <% Html.RenderPartial("CentreSection"); %>
                        </div>

	                    <div class="tile right-tile">
	                        <% Html.RenderPartial("RightSection"); %>
                        </div>
                    </div>
                
                    <div class="section" id="divFindJobs" style="margin-right: 10px;">
                        <div class="section-title">
                            <h1>Find jobs</h1>
                        </div>
                        
                        <div class="section-content">
                            <% Html.RenderPartial("Jobs", Model.Reference); %>
                        </div>
                    </div>
                    
                    <div class="section" id="divBrowseJobs">
                        <div class="section-title">
                            <h1>Browse jobs by industry</h1>
                        </div>
                        
                        <div class="section-content">
                            <div class="browse-job-ads_ascx">
                                <div class="columns">
                                    <div class="column">
<%  for (var index = 0; index < Model.Reference.Industries.Count / 2; ++index)
    {
        var industry = Model.Reference.Industries[index]; %>
	                                        <div class="industry">
	                                            <a href="<%= industry.GetBrowseJobsUrl() %>"><%= industry.Name %></a>
	                                        </div>
<%  } %>                                        
                                    </div>
                                    <div class="column">
<%  for (var index = Model.Reference.Industries.Count / 2; index < Model.Reference.Industries.Count; ++index)
    {
        var industry = Model.Reference.Industries[index]; %>
	                                        <div class="industry">
	                                            <a href="<%= industry.GetBrowseJobsUrl() %>"><%= industry.Name %></a>
	                                        </div>
<%  } %>                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="clearer"></div>
                </div>
            </div>
        </div>

        <div id="footer">
            <% Html.RenderPartial("PageFooter"); %>
        </div>

    </div>

    <% Html.RenderPartial("BannerFooter"); %>

    <script type="text/javascript">
        (function($) {
            /* Login on hit of Enter key */

            $(".login-submit").keypress(function(e) {
                if ((e.keyCode || e.which) == 13) {
                    $("#login").click();
                }
            });

            /* Join on hit of Enter key */

            $(".join-submit").keypress(function(e) {
                if ((e.keyCode || e.which) == 13) {
                    $("#join").click();
                }
            });

            /* Search on hit of Enter key */

            $(".search-submit").keypress(function(e) {
                if ((e.keyCode || e.which) == 13) {
                    $("#search").click();
                }
            });
        })(jQuery);
    </script>
    
</asp:Content>

