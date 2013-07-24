<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<System.Collections.Generic.IList<Category>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>

<div id="side-nav">
	<div class="itemtype" type="Article" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialArticles)) %>" recentlyviewedpartialurl="<%= Html.RouteRefUrl((ResourcesRoutes.PartialRecentArticles)) %>">
	    <div class="leftbar"></div>
	    <div class="bg">
	        <div class="icon articles"></div><%= Html.RouteRefLink("Articles", ResourcesRoutes.Articles) %><div class="icon arrow"></div>
	    </div>
	    <div class="rightbar"></div>
	    <div class="categories">
	    <%  if (Model != null) {
                foreach (var category in Model) {
        %>
	        <div class="category" categoryid="<%= category.Id %>">
	            <div class="leftbar"></div>
	            <div class="bg">
	                <a href="<%= category.GenerateUrl(ResourceType.Article) %>"><%= category.Name %></a>
	                <div class="icon arrow"></div>
	            </div>
	            <div class="rightbar"></div>
	            <div class="subcategories">
		        <% foreach (var subcategory in category.Subcategories) { %>
		            <div class="subcategory" subcategoryid="<%= subcategory.Id %>">
	                    <div class="leftbar"></div>
	                    <div class="bg">
	                        <a href="<%= subcategory.GenerateUrl(ResourceType.Article, Model) %>"><%= subcategory.Name %></a>
	                    </div>
	                    <div class="rightbar"></div>
		            </div>
                <% } %>  
	            </div>
	        </div>
        <% }
        } %>
	    </div>
    </div>
	<div class="itemtype" type="Video" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialVideos)) %>" recentlyviewedpartialurl="<%= Html.MungeUrl(Html.RouteRefUrl((ResourcesRoutes.PartialRecentVideos))) %>">
	    <div class="leftbar"></div>
	    <div class="bg">
	        <div class="icon videos"></div><%= Html.RouteRefLink("Videos", ResourcesRoutes.Videos) %><div class="icon arrow"></div>
	    </div>
	    <div class="rightbar"></div>
	    <div class="categories">
	    <%  if (Model != null) {
                foreach (var category in Model) {
        %>
	        <div class="category" categoryid="<%= category.Id %>">
	            <div class="leftbar"></div>
	            <div class="bg">
	                <a href="<%= category.GenerateUrl(ResourceType.Video) %>"><%= category.Name %></a>
	                <div class="icon arrow"></div>
	            </div>
	            <div class="rightbar"></div>
	            <div class="subcategories">
		        <% foreach (var subcategory in category.Subcategories) { %>
		            <div class="subcategory" subcategoryid="<%= subcategory.Id %>">
	                    <div class="leftbar"></div>
	                    <div class="bg">
	                        <a href="<%= subcategory.GenerateUrl(ResourceType.Video, Model) %>"><%= subcategory.Name %></a>
	                    </div>
	                    <div class="rightbar"></div>
		            </div>
                <% } %>  
	            </div>
	        </div>
        <% }
        } %>
	    </div>
    </div>
	<div class="itemtype" type="QnA" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialQnAs)) %>" recentlyviewedpartialurl="<%= Html.MungeUrl(Html.RouteRefUrl((ResourcesRoutes.PartialRecentQnAs))) %>">
	    <div class="leftbar"></div>
	    <div class="bg">
	        <div class="icon qna"></div><%= Html.RouteRefLink("Q & A", ResourcesRoutes.QnAs) %><div class="icon arrow"></div>
	    </div>
	    <div class="rightbar"></div>
	    <div class="categories">
	    <%  if (Model != null) {
                foreach (var category in Model) {
        %>
	        <div class="category" categoryid="<%= category.Id %>">
	            <div class="leftbar"></div>
	            <div class="bg">
	                <a href="<%= category.GenerateUrl(ResourceType.QnA) %>"><%= category.Name %></a>
	                <div class="icon arrow"></div>
	            </div>
	            <div class="rightbar"></div>
	            <div class="subcategories">
		        <% foreach (var subcategory in category.Subcategories) { %>
		            <div class="subcategory" subcategoryid="<%= subcategory.Id %>">
	                    <div class="leftbar"></div>
	                    <div class="bg">
	                        <a href="<%= subcategory.GenerateUrl(ResourceType.QnA, Model) %>"><%= subcategory.Name %></a>
	                    </div>
	                    <div class="rightbar"></div>
		            </div>
                <% } %>  
	            </div>
	        </div>
        <% }
        } %>
	    </div>
    </div>
    <div class="itemtype" type="RecentlyViewed">
	    <div class="leftbar"></div>
	    <div class="bg">
            <div class="icon recentviewed"></div><% = Html.RouteRefLink("My recently viewed", ResourcesRoutes.RecentArticles, null, new { @class = "Article" })%><% = Html.RouteRefLink("My recently viewed", ResourcesRoutes.RecentVideos, null, new { @class = "Video" }) %><% = Html.RouteRefLink("My recently viewed", ResourcesRoutes.RecentQnAs, null, new { @class = "QnA" })%>
	    </div>
	    <div class="rightbar"></div>
    </div>
</div>