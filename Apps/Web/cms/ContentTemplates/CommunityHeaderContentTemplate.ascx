<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CommunityHeaderContentTemplate.ascx.cs" Inherits="LinkMe.Web.Cms.ContentTemplates.CommunityHeaderContentTemplate" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Register TagPrefix="cm" Namespace="LinkMe.Web.Cms.ContentDisplayViews" Assembly="LinkMe.Web" %>

<%-- MF: Cannot use AddStyleSheetReference because LinkMeUserControl does not
     support generic constructor as does UserControl (which is what this control
     inherits from). --%>
<link href="<%= StyleSheets.Communities.Url %>" rel="stylesheet" />
    
<div id="community-header" class="community">
    <cm:PropertyContentDisplayView ID="cmHeader" ChildName="Content" runat="server" />
</div>