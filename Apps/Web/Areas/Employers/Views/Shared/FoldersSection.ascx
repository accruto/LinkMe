<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<div class="folders_ascx section js_folders_collapsible">
    <div class="section-collapsible-title main-title">
        <div class="main-title-nav-icon folders-section-icon"><div>Folders</div></div>
    </div>
    
    <div class="section-content section-collapsible-content">

        <!-- Links section -->
            
        <div class="section">
            <div class="section-content">
                <ul class="plain_action-list action-list">
                    <li><div class="manage-folders-icon"></div><%= Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders, null, new { @class = "nav-icon"})%></li>
                </ul>
            </div>
        </div>
        
        <!-- Personal folders section -->
        
        <div class="personal-folders_section section">
            <div class="section-collapsible-title droppable">
                Personal folders
            </div>
            <div class="section-content">
                <div id="personal-folders-flagged">
                    <ul class="flagged-folder-list">
                    </ul>
                </div>
                <div id="personal-folders">
                    <ul class="personal-folders-list">
                    </ul>
                </div>
                <ul class="add-folder-list">
                    <li><div></div><a href="javascript:void(0);" class="add-personal-folders js_add-private-folder">Add folder</a></li>
                </ul>
            </div>
        </div>
        
        <!-- Organisation-wide Folders -->
        
        <div class="organisation-wide-folders_section section">
            <div class="section-collapsible-title droppable">
                Organisation-wide folders
            </div>
            <div class="section-content">
                <div id="org-wide-folders">
                    <ul class="org-wide-folders-list">
                    </ul>
                </div>
                <ul class="add-folder-list">
                    <li><div></div><a href="javascript:void(0);" class="add-org-wide-folders js_add-shared-folder">Add folder</a></li>
                </ul>
            </div>
        </div>
        
<%  if (CurrentEmployer != null)
        Html.RenderPartial("JobAdsSection"); %>
        
    </div>
    
	<div class="section-content drag-area">
	    <div class="section">
	        <div style="border:2px dotted #BEE7FF; font-size:85%; color:#76A7C4; text-align:center; padding:2px;">Drag candidates here</div>
	    </div>
	</div>
</div>
<div class="overlay-container forms_v2">
	<div class="folder-overlay shadow" style="display:none;">
		<div class="overlay">
			<div class="overlay-title"><span class="overlay-title-text">Title</span><span class="close-icon"></span></div>
			<div class="overlay-content">
				<div class="overlay-text">Text goes here</div>
				<div class="buttons-holder"></div>
			</div>
		</div>
	</div>
</div>