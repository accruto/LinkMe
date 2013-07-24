<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<div id="candidate-desktop-menu" class="desktop-menu search-results-action-menu">
    <div class="menu-panel" style="display: none;">
        <div class="menu-panel-inner">
        
            <!-- View resume -->

            <div id="hlViewResume" class="menu-item candidate-menu-item view-resume">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    View resume
                </div>
            </div>
            
            <!-- View phone number -->
            
            <div id="hlViewPhoneNumber" class="menu-item candidate-menu-item view-phone-number js_contact-by-phone-action js_credit-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    View phone number
                </div>
            </div>
            
            <!-- Send message -->

            <div id="hlSendMessage" class="menu-item candidate-menu-item send-message js_contact-action js_credit-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Send message
                </div>
            </div>
            
            <!-- Send rejection email -->

            <div id="hlSendRejection" class="menu-item candidate-menu-item send-rejection-message js_contact-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Send rejection email
                </div>
            </div>
            
            <div class="menu-item divider">
                <div class="desktop-menu-item-content">
                </div>
            </div>
            
            <!-- Add to folder -->
            
            <div id="hlAddToFolder" class="menu-item candidate-menu-item js_absorb-clicked-child add-to-folder">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
	                Add to <small>a folder</small>
                </div>
                <div class="menu-submenu-button">
                </div>
                <div class="desktop-menu-submenu">
                    <div class="menu-panel" style="display: none;">
                        <div class="menu-panel-inner">
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Remove from folder -->
            
            <div id="hlRemoveFromFolder" class="menu-item candidate-menu-item remove-from-folder">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
	                Remove from this folder
                </div>
            </div>
	
	        <!-- Unflag -->
	        
	        <div id="hlRemoveFromFlagList" class="menu-item candidate-menu-item remove-from-flaglist">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
	                Remove (unflag) from this folder
                </div>
            </div>
	
	        <!-- Add to job ad -->
	        
	        <div id="hlAddToJobAd" class="menu-item candidate-menu-item js_absorb-clicked-child add-to-jobad">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
	                Add to a job ad
    	        </div>
                <div class="menu-submenu-button">
                </div>
                <div class="desktop-menu-submenu">
                    <div class="menu-panel" style="display: none;">
                        <div class="menu-panel-inner">
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Save resume -->

            <div id="hlSaveResume" class="menu-item candidate-menu-item js_absorb-clicked-child js_access-resume-action js_credit-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Save resume somehow
                </div>
                <div class="menu-submenu-button">
                </div>
                <div class="desktop-menu-submenu">
                    <div class="menu-panel" style="display: none;">
                        <div class="menu-panel-inner">
                            <div id="hlDownloadResume" class="menu-item candidate-menu-item js_default-clicked-child save-as-doc js_access-resume-action js_credit-action" data-item-shorttext="Download as DOC">
								<div class="icon"></div>
                                <div class="desktop-menu-item-content">Download resume as DOC</div>
                			</div>
                			<div id="hlEmailResume" class="menu-item candidate-menu-item save-to-email js_access-resume-action js_credit-action" data-item-shorttext="Email resume to me">
								<div class="icon"></div>
                                <div class="desktop-menu-item-content">Email resume to me</div>
                			</div>
                		</div>
                    </div>
                </div>
            </div>
            
            <div class="menu-item divider">
                <div class="desktop-menu-item-content">
                </div>
            </div>
            
            <!-- Add note -->
            
	        <div id="hlAddNote" class="menu-item candidate-menu-item add-note">
				<div class="icon"></div>
	            <div class="desktop-menu-item-content">
                    Add note
                </div>
            </div>
            
            <div class="menu-item divider">
                <div class="desktop-menu-item-content">
                </div>
            </div>
            
            <!-- Block -->
			
            <div id="hlBlockFromSearch" class="menu-item candidate-menu-item js_absorb-clicked-child block-from-search">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Block from <small>current search</small>
                </div>
                <div class="menu-submenu-button">
                </div>
                <div class="desktop-menu-submenu">
                    <div class="menu-panel" style="display: none;">
                        <div class="menu-panel-inner">
                            <div id="hlChildBlockFromCurrentSearch" class="menu-item candidate-menu-item js_default-clicked-child child-block-from-current" data-item-shorttext="current search">
								<div class="icon"></div>
                                Block from current search
                			</div>
                			<div id="hlChildBlockFromAllSearches" class="menu-item candidate-menu-item child-block-from-all" data-item-shorttext="all searches block-from-search">
								<div class="icon"></div>
                                Block from all searches
                			</div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div id="hlBlockFromAllSearches" class="menu-item candidate-menu-item block-from-all manage-candidate">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Block from all searches
                </div>
            </div>

	        <!-- Restore -->
	        
	        <div id="hlRestoreFromBlockList" class="menu-item candidate-menu-item remove-from-blocklist">
				<div class="icon"></div>
	            <div class="desktop-menu-item-content">
                    Restore candidate
                </div>
            </div>
            
        </div>
    </div>
</div>

