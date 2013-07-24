<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Account.aspx.cs" Inherits="LinkMe.Web.Employers.Guests.Account" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="Content" Src="~/employers/guests/controls/Content.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <uc:Content ID="ucContent" Heading="To view and edit your account you need to be logged in" runat="server" />

</asp:Content>