<%@ Control Language="c#" AutoEventWireup="False" Codebehind="NetworkerSearchCriteria.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.NetworkerSearchCriteria" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<table>
    <tr>
	    <td>
	        <label>First name</label>
	    </td>
	    <td>
		    <asp:TextBox id="txtFirstName" runat="server" cssclass="generic-form-input" columns="40" />
	    </td>
    </tr>
    <tr>
	    <td>
	        <label>Last name</label>
	    </td>
	    <td>
		    <asp:TextBox id="txtLastName" runat="server" cssclass="generic-form-input" columns="40" />
	    </td>
    </tr>
    <tr>
	    <td>
	        <label>Email address</label>
	    </td>
		<td>
			<asp:TextBox id="txtEmailAddress" runat="server" cssclass="generic-form-input" columns="40" />
		</td>
	</tr>
</table>
