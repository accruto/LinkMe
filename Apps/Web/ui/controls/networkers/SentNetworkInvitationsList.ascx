<%@ Import namespace="LinkMe.Web"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SentNetworkInvitationsList.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.SentNetworkInvitationsList" %>
<%@ Import Namespace="LinkMe.Domain.Requests"%>
<%@ Import Namespace="LinkMe.Web.Members.Friends"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Networking"%>

<h3>Friends</h3>

<asp:PlaceHolder ID="phNoSentInvitations" Visible="false" runat="server">
    <div>
        <p><%= WebTextConstants.TEXT_NO_INVITATIONS_SENT_1 %></p>
        <p><%= WebTextConstants.TEXT_NO_INVITATIONS_SENT_2 %></p>
    </div>
    <ul class="action-list">
        <li><a href="<%= GetUrlForPage<InviteFriends>() %>">Invite a friend to join</a></li>
    </ul>
</asp:PlaceHolder>

<asp:PlaceHolder ID="phSentInvitations" Visible="true" runat="server">
    <ul class="action-list">
        <li><a href="<%= GetUrlForPage<InviteFriends>() %>">Invite more friends to join</a></li>
    </ul>
    <p>
        <asp:Repeater ID="rptSentInvitations" runat="server">
            <HeaderTemplate>
            <ul id="sentInvitations">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <strong><%# GetInviteeDisplayName((Invitation)Container.DataItem) %></strong>
                    - invited on <%# GetInvitationSentDate((Invitation)Container.DataItem)%>
                </li>
            </ItemTemplate>
            <FooterTemplate>
            </ul>
            </FooterTemplate>
        </asp:Repeater>
    </p>
</asp:PlaceHolder>

<asp:PlaceHolder ID="phSentRepresentativeInvitation" Visible="false" runat="server">

    <h3>Nominated representative</h3>
    <p>
        You have invited
        <strong><%= GetInviteeDisplayName(RepresentativeInvitation) %></strong>,
        on <%= GetInvitationSentDate(RepresentativeInvitation)%>,
        to be your nominated representative.
    </p>
</asp:PlaceHolder>