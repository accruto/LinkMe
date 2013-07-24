<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="RadioButtonsField.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Fields.RadioButtonsField" %>

<div id="fldCategories" class="radiobuttons_field field" runat="server">
    <asp:Label ID="lblField" AssociatedControlID="rptRadioButtons" runat="server" />
    <div class="radiobuttons_control control" runat="server">
        <asp:Repeater ID="rptRadioButtons" runat="server">
            <ItemTemplate>
                <div class="radio_control control">
                    <asp:RadioButton id="rdoButton" CssClass="radio" GroupName="Buttons" Text="<%# GetText(Container.DataItem) %>" runat="server" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
    
