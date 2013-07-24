<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Results"%>
<%@ Import Namespace="LinkMe.Query.Search.Members"%>

<%  if (Model.TotalItems == 0)
    { %>
        <span class="no-saved-searches">You currently have no saved searches.</span>
<%  }
    else
    { %>
        <div class="list-holder">
            <div class="list">
                <ul>
<%      for (var index = 0; index < Model.Searches.Count; index += 2)
        {
            var search = Model.Searches[index]; %>
                    <li><%= Html.RouteRefLink(search.GetDisplayHtml(), SearchRoutes.Saved, new {savedSearchId = search.Id}, new {@class = "js_ellipsis"}) %></li>
<%      } %>        
                </ul>
            </div>
        </div>            
        <div class="list-holder">
            <div class="right-list">
                <ul>
<%      for (var index = 1; index < Model.Searches.Count; index += 2)
        {
            var search = Model.Searches[index]; %>
                    <li><%= Html.RouteRefLink(search.GetDisplayHtml(), SearchRoutes.Saved, new {savedSearchId = search.Id}, new {@class = "js_ellipsis"}) %></li>
<%      } %>        
                </ul>
            </div>
        </div>
        
        <div class="manage-saved-searches-container">
<%      if (Model.TotalItems > Model.MoreItems)
        {
            if (Model.Searches.Count <= Model.MoreItems)
            { %>
            <div class="view-all js_view-all"><a href="javascript:void(0);">View all</a><span class="icon down-icon"></span></div>
<%          }
            else
            { %>
            <div class="view-all js_view-all"><a href="javascript:void(0);">View less</a><span class="icon up-icon"></span></div>
<%          }
        } %>            
            
            <div class="manage-saved-searches"><a href="<%= new ReadOnlyApplicationUrl("~/ui/registered/employers/SavedResumeSearches.aspx") %>">Manage saved searches</a></div>
        </div>   

<%  } %>   

