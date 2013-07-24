<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.AdministratorNav" %>

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