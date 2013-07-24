<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<!-- AddThis Bookmark Button BEGIN -->

<a href="http://www.addthis.com/bookmark.php" onclick="addthis_url = location.href; addthis_title = document.title; return addthis_click(this);" target="_blank">
    <img src="<%= Context.Request.IsSecureConnection ? "https://secure.addthis.com" : "http://s9.addthis.com" %>/button1-bm.gif" width="125" height="16" border="0" alt="AddThis Social Bookmark Button" />
</a>

<script type="text/javascript">
    var addthis_pub = 'strongpawn';
</script>

<script type="text/javascript" src="<%= Context.Request.IsSecureConnection ? "https://secure.addthis.com" : "http://s9.addthis.com" %>/js/widget.php?v=10"></script>

<!-- AddThis Bookmark Button END -->
