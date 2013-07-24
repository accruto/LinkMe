<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="WizardProgressHeader.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.WizardProgressHeader" %>

<div id="ProgressHeader">
    <span class="title"><%= Title %></span>
    <asp:Repeater ID="rptStepList" runat="server">
        <HeaderTemplate>
        <ol id="StepList">
        </HeaderTemplate>
            <ItemTemplate>
            <li id="item_<%# Container.ItemIndex %>"<%# GetSelectedClass(Container.DataItem as string) %>><%# Container.DataItem as string %></li>
            </ItemTemplate>
        <FooterTemplate>
        </ol>
        </FooterTemplate>
    </asp:Repeater>
</div>

