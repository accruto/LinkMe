<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Login.aspx.cs" Inherits="LinkMe.Web.Landing.Login" MasterPageFile="~/master/SiteMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="Login" src="~/ui/controls/common/Login.ascx" %>
<%@ Register TagPrefix="nv" Namespace="LinkMe.Web.Manager.Navigation" Assembly="LinkMe.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="JavaScript" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div id="main">
        <div id="content">
            <p> <strong>Don't you hate it</strong>
                when you hear that your dream job has just been filled
                and everyone else knew that it was up for grabs, but you?
            </p>
            <p>
                LinkMe is used by thousands of recruiters and employers to find the right people for the job.
                <strong>Join now!</strong>
            </p>
        </div>
        <div class="login_control self-clearing">
            <nv:NavigationForm id="LinkMeLoginForm" runat="server">
                <uc:Login ID="ucLogin" runat="server" />
            </nv:NavigationForm>
        </div>
    </div>
    <div id="footer">
     	<div id="footer_image"></div>
    </div>
</asp:Content>

