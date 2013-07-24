<%@ Import namespace="LinkMe.Web.Service"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MiniFindFriends.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.MiniFindFriends" %>
<%@ Import Namespace="LinkMe.Web.Members.Friends"%>
<%@ Import namespace="LinkMe.Web.Service"%>

<script type="text/javascript" language="javascript">
	LinkMeUI.JSLoadHelper.LoadScriptaculous();
    LinkMeUI.JSLoadHelper.LoadDisappearingLabel();
    LinkMeUI.JSLoadHelper.LoadScrollTracker();
    LinkMeUI.JSLoadHelper.LoadOverlayPopup();
</script>

<form method="get" action="<%= GetUrlForPage<FindFriends>() %>">
    <input type="text" name=<%= FindFriends.GenericQueryParameter %> id="txtMiniFriendsQuery" class="global-search-input" MaxLength="60" />
    <input type="submit" id="btnMiniFriendsSearch" class="search-button" value="" style="margin-bottom:0px;" />
    
    <span id="spanIndicator" style="display: none"><img src="<%=ApplicationPath%>/ui/images/universal/loading.gif" alt="Working..." /></span>
    <div id="divMiniFindFriendsNames" class="auto-complete"></div>
    <script language="javascript" type="text/javascript">
        new Ajax.Autocompleter("txtMiniFriendsQuery", "divMiniFindFriendsNames", "<%= ApplicationPath %>/service/GetSuggestedContacts.ashx", { paramName: "<%= GetSuggestedContacts.NameParam %>", minChars: 2, indicator: 'spanIndicator' });
    </script>
</form>
