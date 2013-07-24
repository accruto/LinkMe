<%@ Control Language="C#" CodeBehind="SidebarSectionContentTemplate.ascx.cs" Inherits="LinkMe.Web.Cms.ContentTemplates.SidebarSectionContentTemplate" %>
<%@ Register TagPrefix="cm" Namespace="LinkMe.Web.Cms.ContentDisplayViews" Assembly="LinkMe.Web" %>

<asp:PlaceHolder ID="phSection" runat="server">
    <cm:PropertyContentDisplayView id="ucContent" ChildName="SectionContent" runat="server" />
</asp:PlaceHolder>