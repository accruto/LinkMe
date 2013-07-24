<%@ Control Language="C#" CodeBehind="SearchSuggestionsIndustry.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Search.SearchSuggestionsIndustry" %>

<% if (Industries != null && Industries.Count > 0) { %>
	<div class="search-suggestions-industry">
		<asp:Repeater ID="repeater" runat="server">
			<HeaderTemplate>
				Filter by Industry:
				<ul class="inline_action-list action-list">
			</HeaderTemplate>
			<ItemTemplate>
				    <li class="-suggestion"><%# GetItemHtml(Container.DataItem) %></li>
			</ItemTemplate>
			<FooterTemplate>
			    </ul>
			</FooterTemplate>
		</asp:Repeater>
	</div>
<% } %>
