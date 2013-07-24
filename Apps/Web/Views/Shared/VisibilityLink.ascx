<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Networkers"%>

<div class="visibility-link">
    Who can see my details?
    <a href="<%= NavigationManager.GetUrlForPage<VisibilitySettingsBasic>()%>" class="affordance-link">Set my visibility</a>
</div>
