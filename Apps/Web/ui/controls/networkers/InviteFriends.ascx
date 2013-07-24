<%@ Import namespace="LinkMe.Utility.Validation"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="InviteFriends.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.InviteFriends" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<br />
<asp:Panel ID="invitesSentPanel" visible="false" runat="server">
    <p>
        <div id="donationWillBeMade" class="example" visible="false" runat="server">A donation will be made when these are accepted</div>
        <h1>Invites sent:</h1>
        <div id="invitesSent" runat="server"></div>
        <br />
    </p>
</asp:Panel>

<asp:Panel ID="duplicateInvitesPanel" visible="false" runat="server">
    <p>
        <h1><%= ValidationInfoMessages.ALREADYINVITED_TEXT %></h1>
        <div id="duplicateList" runat="server"></div>
        <br />
    </p>
</asp:Panel>

<asp:Panel ID="alreadyFriendsPanel" visible="false" runat="server">
    <p>
        <h1>Already in your friends list:</h1>
        <div id="alreadyFriendsList" runat="server"></div>
        <br />
    </p>
</asp:Panel>

<div>
    <label class="small-form-label email-label">Email addresses</label>
</div>
<div>
    <asp:TextBox id="txtEmailAddresses" TextMode="multiline" Float="left" CssClass="generic-form-input email-invite-box" runat="server" />
    <cc:LinkMeRequiredFieldValidator id="reqValEmailAddresses" Float="left" Width="160px" ControlToValidate="txtEmailAddresses" runat="server" />
    <asp:Panel ID="invalidEmailsPanel" CssClass="mock-validator" Visible="false" runat="server">
        <asp:Image ID="invalidEmailsErrorImage" runat="server" />
        <%= ValidationErrorMessages.INVALID_EMAILS_IN_INVITE_FRIENDS %>
    </asp:Panel>
</div>
<div class="clearer"></div>
<div class="example">Separate emails with a comma</div>
<br />

<div>
    <label class="small-form-label">Your message</label>
</div>
<div>
    <asp:TextBox id="txtBody" textmode="multiline" cssclass="email-text-area" runat="server" />
    <cc:TextLengthValidator ID="valTextLength" ControlToValidate="txtBody" MinLength="0" runat="server" />
</div>     
     
<asp:Placeholder id="phDonation" visible="true" runat="server">
    <div>
        <p>When my friends accept, <strong>LinkMe donates <%=DonationAmount %> to:</strong></p>
        <asp:CheckBox id="chkDonationRecipient" runat="server" />
    </div>
</asp:Placeholder>
