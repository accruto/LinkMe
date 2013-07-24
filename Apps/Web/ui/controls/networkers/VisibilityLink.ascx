<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VisibilityLink.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.VisibilityLink" %>
<%@ Import namespace="LinkMe.Web.UI.Registered.Networkers"%>

<div class="visibility-link">
    Who can see my details?
    <a id="lnkVisibilitySettings" href="<%= GetUrlForPage<VisibilitySettingsBasic>()%>" class="affordance-link" name="SetMyVisibility">Set my visibility</a>
</div>