<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CommunityFooterContentTemplate.ascx.cs" Inherits="LinkMe.Web.Cms.ContentTemplates.CommunityFooterContentTemplate" %>
<%@ Register TagPrefix="cm" Namespace="LinkMe.Web.Cms.ContentDisplayViews" Assembly="LinkMe.Web" %>

<div id="community-brought">
    Brought to you by <img alt="LinkMe" src="~/ui/images/universal/logo-small.png" runat="server" />
</div>
<div id="community-footer" class="community">
    <cm:PropertyContentDisplayView ID="cmFooter" ChildName="Content" runat="server" />
</div>

