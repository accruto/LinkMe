<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="AlphabeticalPagingBar.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.AlphabeticalPagingBar" targetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0"%>

    <div class="pagination">
	
<%--	<asp:Repeater id="repeaterPages" runat="server">
		<ItemTemplate>
			<%# GetPageLink((string)Container.DataItem) %>
		</ItemTemplate>
	</asp:Repeater>--%>
	<%= GetPageLink("A") %>
	<%= GetPageLink("B") %>
	<%= GetPageLink("C") %>
	<%= GetPageLink("D") %>
	<%= GetPageLink("E") %>
	<%= GetPageLink("F") %>
	<%= GetPageLink("G") %>
	<%= GetPageLink("H") %>
	<%= GetPageLink("I") %>
	<%= GetPageLink("J") %>
	<%= GetPageLink("K") %>
	<%= GetPageLink("L") %>
	<%= GetPageLink("M") %>
	<%= GetPageLink("N") %>
	<%= GetPageLink("O") %>
	<%= GetPageLink("P") %>
	<%= GetPageLink("Q") %>
	<%= GetPageLink("R") %>
	<%= GetPageLink("S") %>
	<%= GetPageLink("T") %>
	<%= GetPageLink("U") %>
	<%= GetPageLink("V") %>
	<%= GetPageLink("W") %>
	<%= GetPageLink("X") %>
	<%= GetPageLink("Y") %>
	<%= GetPageLink("Z") %>
	
    </div>
