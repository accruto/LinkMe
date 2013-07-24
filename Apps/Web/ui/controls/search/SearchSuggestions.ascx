<%@ Control Language="C#" CodeBehind="SearchSuggestions.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Search.SearchSuggestions" %>
<%@ Register TagPrefix="uc" TagName="SearchSuggestionsIndustry" Src="~/ui/controls/search/SearchSuggestionsIndustry.ascx" %>

<script language="javascript">
	function NarrowSearch(params) {
		document.location = document.location + "&Industries=" + params.Industries;
	}
</script>

<div class="search-suggestions">
	<uc:SearchSuggestionsIndustry ID="ucIndustry" runat="server" />
    <asp:PlaceHolder ID="content" runat="server" />
</div>
