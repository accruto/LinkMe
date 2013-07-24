<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TextBoxFieldContents.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Fields.TextBoxFieldContents" %>
<%@ Register TagPrefix="wc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<div id="divField" runat="server">
    <asp:Label ID="lblField" AssociatedControlID="txtField" runat="server" />
    <div id="divControl" class="<%=ControlCssClass %>" runat="server">
        <asp:TextBox id="txtField" CssClass="<%=TextBoxCssClass %>" runat="server" /><wc:TooltipIcon id="tooltipIcon" runat="server" />
        <wc:TextLengthValidator id="valFieldLength" Enabled="false" ControlToValidate="txtField" Display="Dynamic" runat="server" />
        <wc:LinkMeRequiredFieldValidator id="valFieldRequired" Enabled="false" ControlToValidate="txtField" Display="Dynamic" runat="server" />
        <wc:LinkMeRegularExpressionValidator id="valFieldRegex" Enabled="false" ControlToValidate="txtField" Display="Dynamic" runat="server" />
    </div>
    <asp:PlaceHolder ID="phExample" Visible="false" runat="server">
        <div class="example_helptext helptext"><asp:Literal ID="litExample" runat="server" /></div>
    </asp:PlaceHolder>
</div>