<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Link>" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>

<% if (Model != null) { %>
    <li><%= Html.Link(Model) %>
    <img src="<%= new ReadOnlyApplicationUrl("~/ui/images/universal/logo-small.png") %>" alt="LinkMe" /></li>
<% } %>

