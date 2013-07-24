<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DelayLoadPanel.ascx.cs" Inherits="LinkMe.Web.UI.Controls.DelayLoadPanel" %>
<%@ Import Namespace="LinkMe.Apps.Asp"%>

<div id="<%= ContainerId %>">
    <img id="<%= Constants.AjaxProgressIndicatorId %><%= Suffix %>" src="<%= ApplicationPath %>/ui/img/transparent.gif" alt="In progress..." />
    <span id="<%= ContainerId %>_error" style="display: none;">
        An error occurred in loading this section
    </span>

    <script type="text/javascript">
        AjaxRequestAndRefresh('get', '<%= UrlToGet %>', null, '<%= ContainerId %>', '<%= Suffix %>', '<%= SuccessString %>');
    </script>

    <noscript>
        <i>JavaScript is required to load this part of the page</i>
    </noscript>
</div>
