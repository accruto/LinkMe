<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="MiniFriendsList.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.MiniFriendsList" %>

<asp:Repeater id="rptMiniFriends" runat="server">
    <ItemTemplate>
        <div class="mini-profile-container">
            <div class="profile-thumb">
                <a href="<%# BuildViewProfileLink(((Guid)Container.DataItem)) %>">
                    <img width="<%= LinkMe.Domain.Contacts.Constants.ThumbnailMaxSize.Width %>" height="<%= LinkMe.Domain.Contacts.Constants.ThumbnailMaxSize.Height %>" src="<%# BuildImageUrl(((Guid)Container.DataItem)) %>" alt="Profile photo" />
                </a>
            </div>
            <div class="profile-link">
                <a href="<%# BuildViewProfileLink(((Guid)Container.DataItem)) %>">
                    <%# GetFirstName(((Guid)Container.DataItem)) %>
                </a>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        <div class="clearer"></div>
    </FooterTemplate>
</asp:Repeater>