<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VisibilityCheckBoxRow.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.VisibilityCheckBoxRow" %>

<tr class="js_checkbox-row">
    <td class="table-row-name"><%= DisplayName %></td>
    <td><asp:CheckBox ID="chkHidden" CssClass="js_hidden" runat="server" visible="true" /></td>
    <td>
        <asp:CheckBox ID="chkFirstDegree" CssClass="js_first-degree" runat="server" visible="true" />
        <asp:Image ID="AlwaysFirstDegreeIndicator" runat="server" Visible="false" AlternateText="(tick)" ToolTip="This setting is fixed." />
    </td>
    <td><asp:CheckBox ID="chkSecondDegree" CssClass="js_second-degree" runat="server" visible="true" /></td>
    <td><asp:CheckBox ID="chkPublic" CssClass="js_public" runat="server" visible="true" /></td>
</tr>
