<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustodianHeader.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Navs.CustodianHeader" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Security"%>

<div id="header-links-container">
    <script type="text/javascript">
        function loadPage(url) {
            window.location = url;
        }
    </script>
    <div id="header-links">    
        <div class="left-section">
            <div id="logo" onclick="javascript:loadPage('<%= HomeUrl %>');"></div>
            <div id="nav">
	            <div class="nav-menu">
		            <div class="header-link"><a href="<%= ChangePasswordUrl %>"><span class="menu-text">Password</span></a></div>
	            </div>
            </div>
        </div>
        <div class="right-section">
            <div id="action-links">
                <div class="rhs-links">
                    <div class="user-name_holder">Logged in as <div class="user-name"><%= TextUtil.TruncateForDisplay(Context.User.FullName(), 20)%></div></div>           
                    <%-- <div class="user-name_holder">
                        <a href="<%= AboutUrl %>">Provided by</a>
                        <img src="<%= new ReadOnlyApplicationUrl("~/ui/images/universal/logo-small.png") %>" alt="LinkMe" />
                    </div> --%>             
                    <div class="logout action-link" onclick="javascript:loadPage('<%= LogOutUrl %>');"></div>
                </div>
            </div>
        </div>       
    </div>
</div>
<div id="header-links-container-shadow"></div>