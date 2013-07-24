<%@ Control Language="c#" AutoEventWireup="False" Codebehind="LocationSuggestion.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.LocationSuggestion" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<asp:HiddenField id="hidSuggestionIds" runat="server" />
<asp:repeater id="rptLocations" runat="server">
	<HeaderTemplate>
		<table>
			<tr>
				<td>Did you mean?</td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		    <tr>
			    <td>
			        <% if (string.IsNullOrEmpty(LinkParameter)) { %>
				        <asp:LinkButton id="btnLocation" runat="server" />
				    <% } else { %>
				        <asp:HyperLink id="lnkLocation" runat="server" />
				    <% } %>
			    </td>
		    </tr>
	</ItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:repeater>
