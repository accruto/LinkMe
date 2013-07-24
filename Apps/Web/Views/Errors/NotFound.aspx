<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="System.Web.Mvc.ViewPage<NotFoundModel>" %>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Errors.Models.Errors"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
	    <h1>Page not found</h1>
    </div>
    <div class="section">
        <div class="section-head"></div>
        <div class="section-body">
            <div class="section-content">
	            <p>
<%  if (Model.RequestedUrl == null)
    { %>
                    The page you are looking for has been removed or renamed.
<%  }
    else
    { %>
                    The page you are looking for,
<%      if (!HtmlUtil.ContainsScript(Model.RequestedUrl.ToString()))
        { %>
                    <a href="<%= Model.RequestedUrl %>"><%= Model.RequestedUrl %></a>,
<%      }
        else
        { %>
                    <%= Model.RequestedUrl %>
<%      } %>
                    has been removed or renamed.
<%  }
    if (Model.ReferrerUrl != null)
    {
        if (!NavigationManager.IsInternalUrl(Model.ReferrerUrl))
        { %>
        		    <br />
		            You were incorrectly referred to this page by
		            <a href="<%= Model.ReferrerUrl %>"><%= Model.ReferrerUrl %></a>.
<%      }
        else if (Model.RequestedUrl != null && Model.ReferrerUrl == null)
        { %>
        		    <br />
		            The internal link that you followed has been logged and will be investigated.
		            We apologise for the inconvenience.
<%      }
    } %>
        		    <br /><br />
		            Please click Back to go back to the previous page or return to the
		            <%= Html.RouteRefLink("home page", HomeRoutes.Home)%>.
	            </p>
	            <div>
	                <input class="back-button" onclick="history.back()" type="button">
	            </div>
	        </div>
	    </div>
    </div>

</asp:Content>

