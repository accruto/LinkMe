<%@ Page Language="C#" CodeBehind="EmployerJobAdsSuggestions.aspx.cs" Inherits="LinkMe.Web.UI.Registered.Employers.EmployerJobAdsSuggestions" AutoEventWireup="false" MasterPageFile="~/master/Page.master" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Register TagPrefix="uc" TagName="EmployerJobAdsControl" Src="EmployerJobAdsControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerLeftSideBar" Src="EmployerLeftSideBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerSubHeader" Src="EmployerSubHeader.ascx" %>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <uc:EmployerLeftSideBar runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="SubHeader" runat="server">
    <uc:EmployerSubHeader runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="FormContent" runat="server">

    <div class="page-title">
        <h1>Suggested candidates for open job ads</h1>
    </div>
    
    <p>LinkMe suggests well matched candidates you may want to contact when you
    post job ads to the LinkMe job board.</p>
    <br />
    <uc:EmployerJobAdsControl id="employerJobAdsControl" runat="server"></uc:EmployerJobAdsControl>
        
</asp:Content>
