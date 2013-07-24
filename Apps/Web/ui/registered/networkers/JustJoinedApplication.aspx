<%@ Page Title="" Language="C#" MasterPageFile="~/master/StandardMasterPage.master" AutoEventWireup="false" CodeBehind="JustJoinedApplication.aspx.cs" Inherits="LinkMe.Web.UI.Registered.Networkers.JustJoinedApplication" %>
<%@ Register TagPrefix="uc" TagName="JoinToApplySteps" Src="~/ui/controls/networkers/JoinToApplySteps.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <uc:JoinToApplySteps ID="ucJoinToApplySteps" runat="server" />
    
</asp:Content>