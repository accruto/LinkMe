<%@ Import Namespace="LinkMe.Utility.Security"%>
<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.DevInfo" %>
<%@ Import Namespace="LinkMe.Apps.Agents.Security"%>

<script type="text/javascript">
    var disqus_developer = 1;
</script>

  <% if (ShowAuthenticationInfo && Context != null) { %>
    <!--script type="text/javascript" src="<%= FireBugUrl %>"></script-->

	<div class="debug-note">
		<p>
			<b>Remove For Production.</b>
			<b>Debug with:
			    <a href="javascript: void(0);" onclick="javascript:(function(){document.body.appendChild(document.createElement('script')).src='http://www.billyreisinger.com/jash/source/latest/Jash.js';})();">Jash</a>
			    <a href="javascript: void(0);" onclick="javascript: window.console.open();">Firebug (F12)</a>
			</b>
		</p>
		<p>
			Host: <span style="color:Red;"><%= Context.Server.MachineName %></span>.
			Database: <span style="color:Red;"><%= Database %></span>
			<strong><a href="<%= DevUrl %>">More info</a></strong>
		</p>
		<p>
			Authenticated: <span style="color:Red;"><%= Context.User.Identity.IsAuthenticated %></span>
			Role: <span style="color:Red;"><%= Context.User.UserType() %></span>
			Identity: <span style="color:Red;"><%= Identity %></span>.
			Requires SSL: <span style="color:Red;"><%= FormsAuthentication.RequireSSL %></span>.
		</p>
    </div>
  <% } %>
