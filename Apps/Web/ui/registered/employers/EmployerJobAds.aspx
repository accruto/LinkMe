<%@ Page language="c#" Codebehind="EmployerJobAds.aspx.cs" AutoEventWireup="False" EnableViewState="false" Inherits="LinkMe.Web.UI.Registered.Employers.EmployerJobAds" MasterPageFile="~/master/Page.master" %>
<%@ Register TagPrefix="uc" TagName="EmployerJobAdsControl" Src="EmployerJobAdsControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerLeftSideBar" Src="EmployerLeftSideBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerSubHeader" Src="EmployerSubHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerWriteJobAd" Src="EmployerWriteJobAd.ascx" %>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <uc:EmployerLeftSideBar runat="server" />
    <uc:EmployerWriteJobAd runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="SubHeader" runat="server">
    <uc:EmployerSubHeader runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="FormContent" runat="server">

    <div class="page-title">
        <h1><%= employerJobAdsControl.GetCurrentMode() %> job ads</h1>
    </div>
    
    <uc:EmployerJobAdsControl id="employerJobAdsControl" runat="server"></uc:EmployerJobAdsControl>
        
</asp:Content>

