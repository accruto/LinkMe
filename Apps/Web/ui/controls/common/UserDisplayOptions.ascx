<%@ Register TagPrefix="cc1" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Control Language="c#" AutoEventWireup="False" Codebehind="UserDisplayOptions.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.UserDisplayOptions" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script type="text/javascript">
    function changeEnabledState(enabled, listToEnable) {
	    listToEnable.disabled = ! enabled;
    }    
</script>

<TABLE CELLSPACING="0" CELLPADDING="0" WIDTH="100%" BORDER="0">
	<tr>
		<td>&nbsp;</td>
	</tr>
	<TR VALIGN="top" height="25">
		<TD nowrap colspan="4">Number of Results Displayed:&nbsp;&nbsp;
			<asp:DropDownList ID="ddlNumberOfDisplayResults" runat="server">
				<asp:ListItem Value="10">10</asp:ListItem>
				<asp:ListItem Value="20">20</asp:ListItem>
				<asp:ListItem Value="30">30</asp:ListItem>
				<asp:ListItem Value="40">40</asp:ListItem>
				<asp:ListItem Value="50">50</asp:ListItem>
				<asp:ListItem Value="100">100</asp:ListItem>
				<asp:ListItem Value="200" Selected="True">200</asp:ListItem>
			</asp:DropDownList>
		</TD>
	</TR>
</TABLE>

