<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberHeader.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Navs.MemberHeader" %>
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
<%  if (LoggedInMember != null)
    { %>
            <div id="logo" onclick="javascript:loadPage('<%= HomeUrl %>');"></div>
<%  }
    else
    { %>
            <div id="logo" onclick="javascript:loadPage('<%= AnonymousHomeUrl %>');"></div>
<%  } %>
            <div id="nav">
	            <div class="nav-menu">	  
<%  if (LoggedInMember != null)
    { %>
		            <div class="has-submenu_nav-toplevel-item header-link">
			            <a href="<%= CandidateProfileUrl %>"><span class="menu-text"><span class="arrow">Profile</span></span></a>
			            <span class="nav-menu-panel">
				            <span class="nav-menu-panel-inner">
					            <ul class="nav-column nav-items">
						            <li class="nav-item"><a href="<%= CandidateProfileUrl %>" class="nav-icon">Resume</a></li>
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

    if (LoggedInMember != null)
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

    if (LoggedInMember != null)
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
        </div>
        <div class="right-section">
            <div id="action-links">
                <div class="rhs-links">
<%  if (LoggedInMember != null)
    { %>
                    <div class="user-name_holder">Logged in as <div class="user-name"><%= TextUtil.TruncateForDisplay(Context.User.FullName(), 20)%></div></div>                        
                    <div class="logout action-link" onclick="javascript:loadPage('<%= LogOutUrl %>');"></div>
                    <div class="settings action-link" onclick="javascript:loadPage('<%= AccountUrl %>');"></div>
<%  }
    else
    { %>
                    <div class="employer switch-link" onclick="javascript:loadPage('<%= EmployerHomeUrl %>');"></div>
                    <div class="login-join switch-link" onclick="javascript:loadPage('<%= LogInUrl %>');"></div>
<%  } %>
                </div>
            </div>
        </div>       
    </div>
</div>
<div id="header-links-container-shadow"></div>