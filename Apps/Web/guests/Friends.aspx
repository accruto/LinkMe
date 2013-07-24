<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Friends.aspx.cs" Inherits="LinkMe.Web.Guests.Friends" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="Content" Src="~/guests/controls/Content.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <uc:Content ID="ucContent" Heading="To interact with your friends & colleagues you must be logged in" runat="server" />

</asp:Content>
