<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Invitations.aspx.cs" Inherits="LinkMe.Web.Members.Friends.Invitations" MasterPageFile="~/master/TwoColumnMasterPage.master" %>
<%@ Import namespace="LinkMe.Web.UI.Registered.Networkers"%>
<%@ Register TagPrefix="uc" TagName="ReceivedNetworkInvitationList" Src="~/ui/controls/networkers/ReceivedNetworkInvitationList.ascx" %>
<%@ Register TagPrefix="uc" TagName="SentNetworkInvitationsList" Src="~/ui/controls/networkers/SentNetworkInvitationsList.ascx" %>

<asp:Content ContentPlaceHolderID="LeftContent" runat="server">

    <div class="section">
        <div class="section-title">
            <h1>People who've invited me</h1>
        </div>

        <div class="section-content">
            <uc:ReceivedNetworkInvitationList ID="ucReceivedNetworkInvitationList" runat="server" />
        </div>
    </div>    
    
</asp:Content>

<asp:Content ContentPlaceHolderID="RightContent" runat="server">

    <div class="section">
        <div class="section-title">
            <h1>Invitations I've sent</h1>
        </div>

        <div class="section-content">
            <uc:SentNetworkInvitationsList id="ucSentInvites" runat="server" />
        </div>
    </div>
        
</asp:Content>


