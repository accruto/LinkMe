<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="UnsubscribeFromEmail.aspx.cs" Inherits="LinkMe.Web.UI.Unregistered.UnsubscribeFromEmail" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="section-title">
        <h1><asp:Literal id="litTitle" runat="server" /></h1>
    </div>
    
	<p>
	    <asp:Literal id="litMessage" runat="server" />
	</p>
    <div class="action">
        <cc:LinkMeOneClickButton id="btnUnsubscribe" runat="server" CssClass="button" Text="Unsubscribe" OnClick="btnUnsubscribe_Click" />
        <cc:LinkMeOneClickButton id="btnHome" runat="server" CssClass="button" Text="Cancel" OnClick="btnHome_Click" />
    </div>

</asp:Content>