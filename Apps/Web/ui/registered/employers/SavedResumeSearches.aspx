<%@ Page language="c#" Codebehind="SavedResumeSearches.aspx.cs" AutoEventWireup="False" Inherits="LinkMe.Web.UI.Registered.Employers.SavedResumeSearches" MasterPageFile="~/master/Page.master" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Register TagPrefix="uc" TagName="SavedResumeSearchesControl" Src="SavedResumeSearchesControl.ascx" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Register TagPrefix="uc" TagName="EmployerLeftSideBar" Src="EmployerLeftSideBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerSubHeader" Src="EmployerSubHeader.ascx" %>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <uc:EmployerLeftSideBar runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="SubHeader" runat="server">
    <uc:EmployerSubHeader runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="FormContent" runat="server">
    <div class="section forced-first_section">
		<div class="page-title">
            <h1>Saved searches
            <cc:ToolTipIcon id="ToolTipIcon1" HtmlFormat="true" runat="server"
            text="When you save a resume search, it goes to your saved searches. The saved search also appears on your homepage when you login."/>
            </h1>
        </div>
		<div class="page-title">
		    <div class="ribbon">
			    <div class="ribbon_inner">
				    <ul class="plain_horizontal_action-list horizontal_action-list action-list">
					    <li><a class="add-action" href="<%= NewSavedSearchUrl %>" title="Create a new saved search using Advanced Search">New</a></li>
				    </ul>
			    </div>
		    </div>
        </div>

        <uc:SavedResumeSearchesControl id="savedResumeSearchesControl" runat="server" />
        
    </div>
</asp:Content>