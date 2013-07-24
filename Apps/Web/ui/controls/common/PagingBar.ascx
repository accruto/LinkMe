<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="PagingBar.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.PagingBar" targetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0"%>
<% if (NumberOfPages > 1) { %>
    <div class="pagination">
	<%= GetPreviousLink() %>
	
	<asp:Repeater id="repeaterPages" runat="server">
		<ItemTemplate>
			<%# GetPageLink((int)Container.DataItem) %>
		</ItemTemplate>
	</asp:Repeater>
	
	<%= GetNextLink() %>
    </div>
<% } %>
