<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ReferenceModel>" %>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<div class="homepage-section jobs-by-employers">
    <div class="section-top"></div>
    <div class="section-bg">
        <div class="homepage-content-holder qow">
    <% if (Model.FeaturedAnsweredQuestion != null) { %>
            <div class="homepage-title">Question of the week</div>
            <div class="q"></div>
            <div class="title"><%= Model.FeaturedAnsweredQuestion.Title%></div>
            <div class="a"></div>
            <%
                var text = HtmlUtil.StripHtmlTags(Model.FeaturedAnsweredQuestion.Text);
                var shortText = text;
                var length = text.Length;
                if (length > 150) shortText = text.Substring(0, 150) + "...";
            %>
            <div class="desc" title="<%= text %>">
                <%= shortText %>
                <a href="<%= Model.FeaturedAnsweredQuestion.GenerateUrl(Model.Categories) %>" class="readmore">read more</a>
            </div>
            
            <div class="help">Need help with your career?</div>
            <a class="button askyourownquestion" href="<%= Html.RouteRefUrl(ResourcesRoutes.Resources) %>"></a>
     <%} %>
       </div>
        <div class="divider"></div>
        <div class="homepage-content-holder featured">
            <div class="homepage-title">View jobs by Employer/Advertiser</div>
            <div class="homepage-content">
                <div class="title">Work for your dream company</div>
<%  foreach (var employer in Model.FeaturedEmployers)
    { %>                
                <div class="homepage-centered-image" employer="<%= employer.Name %>">
                    <img class="featured-link" onclick="javascript:loadPage('<%= employer.SearchUrl %>');" src="<%= employer.LogoUrl %>" />
                </div>
<%  } %>                
            </div>
        </div>
    </div>
    <div class="section-bottom"></div>
</div>
