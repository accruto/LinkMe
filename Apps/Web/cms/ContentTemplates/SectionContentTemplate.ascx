<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SectionContentTemplate.ascx.cs" Inherits="LinkMe.Web.Cms.ContentTemplates.SectionContentTemplate" %>
<%@ Register TagPrefix="cm" Namespace="LinkMe.Web.Cms.ContentDisplayViews" Assembly="LinkMe.Web" %>

<asp:PlaceHolder ID="phSection" runat="server">
    <div class="section-title">
        <h1><%= Item.SectionTitle %></h1>
    </div>

    <div class="section-content">
        <cm:PropertyContentDisplayView id="ucContent" ChildName="SectionContent" runat="server" />
    </div>
</asp:PlaceHolder>