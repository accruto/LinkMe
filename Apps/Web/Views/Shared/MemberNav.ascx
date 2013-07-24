<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.MemberNav" %>

<div id="nav">
    <div class="nav-menu">
<%  if (CurrentMember != null)
    { %>
        <div class="has-submenu_nav-toplevel-item header-link">
	        <a href="<%= ProfileUrl %>"><span class="menu-text"><span class="arrow">Profile</span></span></a>
	        <span class="nav-menu-panel">
		        <span class="nav-menu-panel-inner">
			        <ul class="nav-column nav-items">
				        <li class="nav-item"><a href="<%= ProfileUrl %>" class="nav-icon">Resume</a></li>
				        <li class="nav-item"><a href="<%= DiaryUrl %>" class="nav-icon">CPD Diary</a></li>
			        </ul>
		        </span>
	        </span>
        </div>
<%  }
    else
    { %>
        <div class="header-link">
	        <a href="<%= AnonymousProfileUrl %>"><span class="menu-text">Profile</span></a>
        </div>
<%  }

    if (CurrentMember != null)
    { %>
        <div class="has-submenu_nav-toplevel-item header-link">
	        <a href="<%= JobsUrl %>"><span class="menu-text"><span class="arrow">Jobs</span></span></a>
	        <span class="nav-menu-panel">
		        <span class="nav-menu-panel-inner">
			        <ul class="nav-column nav-items">
				        <li class="nav-item"><a href="<%= JobsUrl %>" class="nav-icon">Jobs</a></li>
				        <li class="nav-item"><a href="<%= ApplicationsUrl %>" class="nav-icon">Applications</a></li>
				        <li class="nav-item"><a href="<%= PreviousSearchesUrl %>" class="nav-icon">Recent and favourite searches</a></li>
				        <li class="nav-item"><a href="<%= SuggestedJobsUrl %>" class="nav-icon">Suggested jobs</a></li>
				        <li class="nav-item"><a href="<%= BrowseJobsUrl %>" class="nav-icon">Browse all jobs</a></li>
			        </ul>
		        </span>
	        </span>
        </div>
<%  }
    else
    { %>
        <div class="has-submenu_nav-toplevel-item header-link">
	        <a href="<%= JobsUrl %>"><span class="menu-text">Jobs</span></a>
        </div>
<%  }
    
    if (CurrentMember != null)
    { %>
        <div class="has-submenu_nav-toplevel-item header-link">
	        <a href="<%= FriendsUrl %>"><span class="menu-text"><span class="arrow">Friends</span></span></a>
	        <span class="nav-menu-panel">
		        <span class="nav-menu-panel-inner">
			        <ul class="nav-column nav-items">
				        <li class="nav-item"><a href="<%= FriendsUrl %>" class="nav-icon">Friends list</a></li>
				        <li class="nav-item"><a href="<%= FindFriendsUrl %>" class="nav-icon">Find friends</a></li>
				        <li class="nav-item"><a href="<%= InviteFriendsUrl %>" class="nav-icon">Invite friends</a></li>
				        <li class="nav-item"><a href="<%= RepresentativeUrl %>" class="nav-icon">Representative</a></li>
				        <li class="nav-item"><a href="<%= InvitationsUrl %>" class="nav-icon">Invitations</a></li>
			        </ul>
		        </span>
	        </span>
        </div>
<%  }
    else
    { %>
        <div class="header-link">
	        <a href="<%= AnonymousFriendsUrl %>"><span class="menu-text">Friends</span></a>
        </div>
<%  } %>
    
        <div class="has-submenu_nav-toplevel-item header-link resources">
            <a href="<%= ResourcesUrl %>">Resources</a>
        </div>
    
    </div>
</div>
