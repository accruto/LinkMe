<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ContactsList.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.ContactsList" %>
<%@ Register TagPrefix="uc" TagName="ContactsListDetails" Src="~/ui/controls/networkers/ContactsListDetails.ascx" %>

<asp:Repeater id="rptContacts" runat="server">
    <ItemTemplate>
        <div id="<%= ContactItemDivId %><%# GetItemSuffix(Container.ItemIndex) %>" class="<%# GetCssClass(Container.ItemIndex) %>">
            <uc:ContactsListDetails id="ucContactsListDetails" runat="server" />
        </div>
    </ItemTemplate>
</asp:Repeater>
