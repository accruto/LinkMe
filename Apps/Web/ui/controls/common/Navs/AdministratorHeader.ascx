<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdministratorHeader.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Navs.AdministratorHeader" %>
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
		            <div class="has-submenu_nav-toplevel-item header-link">
			            <a href="<%= SearchMembersUrl %>"><span class="menu-text"><span class="arrow" >Search</span></span></a>
			            <span class="nav-menu-panel">
				            <span class="nav-menu-panel-inner">
					            <ul class="nav-column nav-items">
						            <li class="nav-item"><a href="<%= SearchMembersUrl %>" class="nav-icon new-search-icon">Search members</a></li>
						            <li class="nav-item"><a href="<%= SearchEmployersUrl %>" class="nav-icon new-search-icon">Search employers</a></li>
						            <li class="nav-item"><a href="<%= SearchOrganisationsUrl %>" class="nav-icon new-search-icon">Search organisations</a></li>
						            <li class="nav-item"><a href="<%= SearchEnginesUrl %>" class="nav-icon new-search-icon">Search engines</a></li>
					            </ul>
				            </span>
			            </span>
		            </div>
		            <div class="header-link"><a href="<%= CampaignsUrl %>"><span class="menu-text">Campaigns</span></a></div>
	            </div>
            </div>
        </div>
        <div class="right-section">
            <div id="action-links">
                <div class="rhs-links">
                    <div class="user-name_holder">Logged in as <div class="user-name"><%= TextUtil.TruncateForDisplay(Context.User.FullName(), 20)%></div></div>                        
                    <div class="logout action-link" onclick="javascript:loadPage('<%= LogOutUrl %>');"></div>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="header-links-container-shadow"></div>


