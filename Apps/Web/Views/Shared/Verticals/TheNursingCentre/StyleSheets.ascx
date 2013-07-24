<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Views"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<%= Html.StyleSheet(StyleSheets.Communities) %>
<link href="<%= new ReadOnlyApplicationUrl("~/themes/communities/TheNursingCentre/css/thenursingcentre_front-page.css") %>" rel="stylesheet" />
