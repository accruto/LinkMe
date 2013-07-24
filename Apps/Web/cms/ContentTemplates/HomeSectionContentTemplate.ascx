<%@ Control Language="C#" CodeBehind="HomeSectionContentTemplate.ascx.cs" Inherits="LinkMe.Web.Cms.ContentTemplates.HomeSectionContentTemplate" %>
<%@ Register TagPrefix="cm" Namespace="LinkMe.Web.Cms.ContentDisplayViews" Assembly="LinkMe.Web" %>

<asp:PlaceHolder ID="phSection" runat="server">
    <div class="home-editable-section-title">
        <h1><%= Item.SectionTitle %></h1>
    </div>

    <div class="home-editable-section-content">
        <cm:PropertyContentDisplayView id="ucContent" ChildName="SectionContent" runat="server" />
    </div>
</asp:PlaceHolder>