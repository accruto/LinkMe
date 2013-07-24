<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="InformationMessage.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.OverlayPopups.InformationMessage" %>
<div>
<p style="text-align:center;">
<%= GetMessageHtml() %>
</p>

<p style="text-align:center;"><input id="btnOK" type="button" class="ok-button" onclick="banishOverlayPopup();" /></p>
</div>

<%= GetErrorBorderScript() %>

