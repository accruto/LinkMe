<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.EmployerNav" %>

<div id="nav">
	<div class="nav-menu">
		<div class="has-submenu_nav-toplevel-item header-link">
			<a href="<%= SearchCandidatesUrl %>"><span class="menu-text"><span class="arrow" >Candidate search</span></span></a>
			<span class="nav-menu-panel">
				<span class="nav-menu-panel-inner">
					<ul class="nav-column nav-items">
						<li class="nav-heading">Discover</li>
						<li class="nav-item"><div class="employer-new-search-icon"></div><a href="<%= SearchCandidatesUrl %>" class="nav-icon">New search</a></li>
<%  if (CurrentEmployer != null)
    { %>
						<li class="nav-item"><a href="<%= SavedResumeSearchesUrl %>" class="nav-icon saved-searches-icon">Saved searches</a></li>
						<li class="nav-item"><a href="<%= SuggestedCandidatesUrl %>" class="nav-icon suggested-candidates-icon">Suggested candidates</a></li>
						<li class="nav-item"><a href="<%= SavedResumeSearchAlertsUrl %>" class="nav-icon candidate-alerts-icon">Candidate alerts</a></li>
<%  }
    else
    { %>
						<li class="nav-item"><a href="<%= AnonymousSavedResumeSearchesUrl %>" class="nav-icon saved-searches-icon">Saved searches</a></li>
						<li class="nav-item"><a href="<%= AnonymousSuggestedCandidatesUrl %>" class="nav-icon suggested-candidates-icon">Suggested candidates</a></li>
						<li class="nav-item"><a href="<%= AnonymousSavedResumeSearchAlertsUrl %>" class="nav-icon candidate-alerts-icon">Candidate alerts</a></li>
<%  } %>
						<li class="nav-heading">Manage</li>
<%  if (CurrentEmployer != null)
    { %>
						<li class="nav-item"><div class="flagged-candidates-icon"></div><a href="<%= FlaggedCandidatesUrl %>" class="nav-icon">Flagged candidates</a></li>
						<li class="nav-item"><div class="manage-folders-icon"></div><a href="<%= ManageFoldersUrl %>" class="nav-icon">Manage folders</a></li>
						<li class="nav-item"><div class="blocklists-icon"></div><a href="<%= BlockListsUrl %>" class="nav-icon">Block lists</a></li>
<%  }
    else
    { %>
						<li class="nav-item"><div class="flagged-candidates-icon"></div><a href="<%= AnonymousFlaggedCandidatesUrl %>" class="nav-icon">Flagged candidates</a></li>
						<li class="nav-item"><div class="manage-folders-icon"></div><a href="<%= AnonymousManageFoldersUrl %>" class="nav-icon">Manage folders</a></li>
						<li class="nav-item"><div class="blocklists-icon"></div><a href="<%= AnonymousBlocklistsUrl %>" class="nav-icon">Block lists</a></li>
<%  } %>
					</ul>
				</span>
			</span>
		</div>
		<div class="has-submenu_nav-toplevel-item header-link">
			<a href="<%= NewJobAdUrl %>">
			<span class="menu-text"><span class="arrow">Job ads</span></span></a>
			<span class="nav-menu-panel">
				<span class="nav-menu-panel-inner">
					<ul class="nav-column nav-items">
<%  if (CurrentEmployer != null)
    { %>
						<li class="nav-item"><a href="<%= OpenJobAdsUrl %>" class="nav-icon open-jobad-icon">Open ads</a></li>
			            <li class="nav-item"><a href="<%= NewJobAdUrl %>" class="nav-icon post-jobad-icon">Post new ad</a></li>
			            <li class="nav-item"><a href="<%= DraftJobAdsUrl %>" class="nav-icon draft-jobad-icon">Draft ads</a></li>
			            <li class="nav-item"><a href="<%= ClosedJobAdsUrl %>" class="nav-icon closed-jobad-icon">Closed ads</a></li>
<%  }
    else
    { %>
						<li class="nav-item"><a href="<%= AnonymousOpenJobAdsUrl %>" class="nav-icon open-jobad-icon">Open ads</a></li>
			            <li class="nav-item"><a href="<%= NewJobAdUrl %>" class="nav-icon post-jobad-icon">Post new ad</a></li>
			            <li class="nav-item"><a href="<%= AnonymousDraftJobAdsUrl %>" class="nav-icon draft-jobad-icon">Draft ads</a></li>
			            <li class="nav-item"><a href="<%= AnonymousClosedJobAdsUrl %>" class="nav-icon closed-jobad-icon">Closed ads</a></li>
<%  } %>
					</ul>
				</span>
			</span>
		</div>
<%  if (CurrentEmployer != null)
    { %> 
		<div class="header-link"><a href="<%= AccountUrl %>"><span class="menu-text">Account</span></a></div>
<%  }
    else
    { %>
		<div class="header-link"><a href="<%= AnonymousAccountUrl %>"><span class="menu-text">Account</span></a></div>
<%  } %>
		<div class="header-link"><a href="<%= ResourcesUrl %>"><span class="menu-text">Resources</span></a></div>
	</div>
</div>