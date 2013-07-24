<%@ Import namespace="LinkMe.Web.Service"%>
<%@ Control Language="C#" CodeBehind="Location.ascx.cs" AutoEventWireup="false" EnableViewState="false" Inherits="LinkMe.Web.UI.Controls.Common.Location" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<script type="text/javascript">
	LinkMeUI.JSLoadHelper.LoadScriptaculous();
</script>	
<script type="text/javascript">
	LinkMeUI.JSLoadHelper.LoadLocation();   
</script>

<span class="forms_v2">
    <div class="location_autocomplete_textbox_control autocomplete_textbox_control textbox_control control">
        <asp:TextBox id="txtLocation" CssClass="location_autocomplete_textbox autocomplete_textbox textbox" runat="server"></asp:TextBox>
        <% if (!string.IsNullOrEmpty(ExampleText)) { %>
	        <div class="description">e.g. <%= ExampleText %></div>
        <% } %>
    </div>
</span>

<cc:TextLengthValidator runat="server" ControlToValidate="txtLocation" id="valTxtLocation" Display="Dynamic" />
<div id="divLocation" class="auto-complete"></div>
<script language="javascript" type="text/javascript">
    var <%=ClientID %>Autocompleter = new Ajax.Autocompleter("<%=txtLocation.ClientID %>", 
        "divLocation", "<%=ApplicationPath %>/service/GetSuggestedLocations.ashx", 
        {paramName: "<%=LocationParameter %>", minChars: 1, frequency: 0.001, 
        parameters: '<%=CountryParameter %>=<%=CountryId %>&'+
        '<%=MaximumSuggestionsParameter %>=<%=MaximumSuggestions %>&<%=MethodParameter %>=<%=Method %>', 
        afterUpdateElement: afterUpdateLocation});
    
    function afterUpdateLocation(text, li)
    {
        <%=GetResolveLocationScript() %>
    }
    
    <%= SetCountryScript %> <%-- Bug 5122: set the country while loadin the page --%>
</script>
