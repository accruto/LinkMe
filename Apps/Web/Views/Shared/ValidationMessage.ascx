<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<string>" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<% if (Model != null && ViewData.ModelState[Model] != null && ViewData.ModelState[Model].Errors.Count > 0) { %>
<span style="color: Red;"><img align="absbottom" src="<%= new ReadOnlyApplicationUrl("~/ui/images/universal/error.png") %>"/></span>
<% } %>