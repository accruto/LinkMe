<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ReceivedNetworkInvitationList.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.ReceivedNetworkInvitationList" %>
<%@ Import Namespace="LinkMe.Domain.Roles.Representatives"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Networking"%>

<h3>Friends</h3>
<p>
    <asp:Literal ID="litNoInvitationsMessage" Visible="false" runat="server"></asp:Literal>
</p>
<asp:Repeater ID="rptInvitations" OnItemCommand="rptInvitations_ItemCommand" runat="server">
    <ItemTemplate>
        <%-- Photo --%>
        <div class="mini-profile-container">
            <div class="profile-thumb">
                <a href="<%# GetInviterProfileUrl((NetworkingInvitation)Container.DataItem) %>">
                    <img width="<%= LinkMe.Domain.Contacts.Constants.ThumbnailMaxSize.Width %>" height="<%= LinkMe.Domain.Contacts.Constants.ThumbnailMaxSize.Height %>" 
                        src="<%# GetInviterImageUrl((NetworkingInvitation)Container.DataItem) %>" alt="Profile photo" />
                </a>
            </div>
            <div class="profile-link">
                <a href="<%# GetInviterProfileUrl((NetworkingInvitation)Container.DataItem) %>">
                    <%# GetInviterFirstName((NetworkingInvitation)Container.DataItem) %>
                </a>
            </div>
        </div>

        <p><%# GetInviterFullName((NetworkingInvitation)Container.DataItem) %> has asked to be your friend.</p>
        <p>
            <a href="<%# GetInviterProfileUrl((NetworkingInvitation)Container.DataItem) %>">
            View <%# GetInviterFullName((NetworkingInvitation)Container.DataItem).MakeNamePossessive() %> profile</a>
        </p>
        <p>
            <asp:Button ID="btnAccept" CommandName="AcceptInvitation"
             CommandArgument="<%# ((NetworkingInvitation)Container.DataItem).Id.ToString() %>" CssClass="accept-button" runat="server" />
            <asp:Button ID="btnIgnore" CommandArgument="<%# ((NetworkingInvitation)Container.DataItem).Id.ToString() %>"
                CommandName="IgnoreInvitation" CssClass="ignore-button" runat="server" />
        </p>
    </ItemTemplate>
    
    <SeparatorTemplate>
        <hr />
    </SeparatorTemplate>
</asp:Repeater>

<asp:PlaceHolder ID="phRepresentative" Visible="false" runat="server">
    <h3>Representative</h3>
    <asp:Repeater ID="rptRepresentativeInvitations" OnItemCommand="rptRepresentativeInvitations_ItemCommand" runat="server">
        <ItemTemplate>
            <%-- Photo --%>
            <div class="mini-profile-container">
                <div class="profile-thumb">
                    <a href="<%# GetInviterProfileUrl((RepresentativeInvitation)Container.DataItem) %>">
                        <img width="<%= LinkMe.Domain.Contacts.Constants.ThumbnailMaxSize.Width %>" height="<%= LinkMe.Domain.Contacts.Constants.ThumbnailMaxSize.Height %>" 
                            src="<%# GetInviterImageUrl((RepresentativeInvitation)Container.DataItem) %>" alt="Profile photo" />
                    </a>
                </div>
                <div class="profile-link">
                    <a href="<%# GetInviterProfileUrl((RepresentativeInvitation)Container.DataItem) %>">
                        <%# GetInviterFirstName((RepresentativeInvitation)Container.DataItem)%>
                    </a>
                </div>
            </div>

            <p><%# GetInviterFullName((RepresentativeInvitation)Container.DataItem)%> has asked you to be their representative.</p>
            <p>
                <a href="<%# GetInviterProfileUrl((RepresentativeInvitation)Container.DataItem) %>">
                View <%# GetInviterFullName((RepresentativeInvitation)Container.DataItem).MakeNamePossessive() %> profile</a>
            </p>
            <p>
                <asp:Button ID="btnRepresentativeAccept" CommandName="AcceptInvitation"
                 CommandArgument="<%# ((RepresentativeInvitation)Container.DataItem).Id.ToString() %>" CssClass="accept-button" runat="server" />
                <asp:Button ID="btnRepresentativeIgnore" CommandArgument="<%# ((RepresentativeInvitation)Container.DataItem).Id.ToString() %>"
                    CommandName="IgnoreInvitation" CssClass="ignore-button" runat="server" />
            </p>
        </ItemTemplate>
        
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>
</asp:PlaceHolder>
