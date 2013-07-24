<%@ Page language="c#" Codebehind="SavedResumeSearchAlerts.aspx.cs" AutoEventWireup="False" EnableViewState="false" Inherits="LinkMe.Web.UI.Registered.Employers.SavedResumeSearchAlerts" MasterPageFile="~/master/Page.master" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Query.Search.Members"%>
<%@ Import namespace="LinkMe.Web.UI.Registered.Employers"%>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Register TagPrefix="uc" TagName="EmployerLeftSideBar" Src="EmployerLeftSideBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerSubHeader" Src="EmployerSubHeader.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Candidate alerts</li>
    </ul>
	<script language="javascript" type="text/javascript">
	    bHideTemporaryBlockList = true;
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <uc:EmployerLeftSideBar runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="SubHeader" runat="server">
    <uc:EmployerSubHeader runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript" language="javascript">
		LinkMeUI.JSLoadHelper.LoadScriptaculous();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadScrollTracker();
        LinkMeUI.JSLoadHelper.LoadStringUtils();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadOverlayPopup();
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="FormContent" runat="server">

    <div class="page-title">
        <h1>Candidate alerts
        <cc:TooltipIcon id="tipCandidateAlerts" runat="server" HtmlFormat="true"
        Text="<p>Get candidates you want delivered straight to your inbox with 
        Candidate Alerts.</p>
        <ul>
        <li>Create a new email alert from scratch, or</li>
        <li>Use a saved search from your saved searches.</li></ul>" />
        </h1>
    </div>
    
    <asp:placeholder id="EmptyRepeaterMessage" runat="server" visible="false">
        <div>
            <p>
                Start a new search and create a new email alert based on your search results.
            </p>
        </div>
    </asp:placeholder>

    <ul class="plain_horizontal_action-list horizontal_action-list action-list forms_v2 createemailalert">
        <li><a class="button createnewemailalert" href="<%= SearchRoutes.Search.GenerateUrl(new { createEmailAlert = true }) %>">Create new email alert</a></li>
    </ul>
    
    <asp:PlaceHolder id="Divider" runat="server" visible="false">
        <div class="divider createemailalert"></div>
    </asp:PlaceHolder>
    <asp:Repeater id="SavedResumeSearchAlertsRepeater" runat="server" visible="false" OnItemCommand="SavedResumeSearchAlertsRepeater_ItemCommand">
        <ItemTemplate>
            <div class="repeater-container">
	            <div class="repeater-details-container">
	                <a href="<%# GetSearchUrl(Container.DataItem) %>"><%# GetSearchDisplayHtml(Container.DataItem) %></a>
	            </div>
	            <div class="repeater-actions-container">
	                <ul class="plain_action-list action-list">
		                <li>
		                    <asp:LinkButton CssClass="delete-action" id="removeSavedSearchAlert" runat="server" CommandName="Remove"
		                        CommandArgument='<%# ((MemberSearch)Container.DataItem).Id %>' Text="Remove" />
		                </li>
		            </ul>
	            </div>
	            <div class="clearer"></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

