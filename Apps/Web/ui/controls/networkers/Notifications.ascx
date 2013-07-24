<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="Notifications.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.Notifications" %>

<asp:Repeater id="rptNotifications" runat="server" OnItemCreated="rptNotifications_ItemCreated">
    <ItemTemplate>
        <div id="item-container">
            <p id="notifications-text"><asp:Literal id="ltlNotificationsText" Runat="server"></asp:Literal></p>
        </div>
    </ItemTemplate>
</asp:Repeater>

<asp:PlaceHolder ID="phNoNotifications" runat="server" Visible="false">
    <div>
        You don't have any notifications.
    </div>
</asp:PlaceHolder>