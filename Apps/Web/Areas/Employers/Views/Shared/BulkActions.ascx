<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<div id="bulk-desktop-menu" class="desktop-menu search-results-action-menu">
    <div class="menu-panel" style="display: none;">
        <div class="menu-panel-inner">
        
            <!-- View resume -->
        
            <div id="hlBulkViewResume" class="menu-item bulk-menu-item view-resume">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    View&nbsp;<span class="js_count"></span>resume<span class="js_suffix"></span>
                </div>
            </div>
            
            <!-- View phone number -->
            
            <div id="hlBulkViewPhoneNumber" class="menu-item bulk-menu-item view-phone-number js_contact-by-phone-action js_credit-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    View&nbsp;<span class="js_count"></span>phone number<span class="js_suffix"></span>
                </div>
            </div>
            
            <!-- Send message -->

            <div id="hlBulkSendMessage" class="menu-item bulk-menu-item send-message js_contact-action js_credit-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Send&nbsp;<span class="js_count"></span>message<span class="js_suffix"></span>
                </div>
            </div>
			
            <!-- Send rejection email -->

            <div id="hlBulkSendRejection" class="menu-item bulk-menu-item send-rejection-message js_contact-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Send&nbsp;<span class="js_count"></span>rejection email<span class="js_suffix"></span>
                </div>
            </div>
            
            <div class="menu-item bulk-menu-item divider">
                <div class="desktop-menu-item-content">
                </div>
            </div>
            
            <!-- Add to folder -->
            
            <div id="hlBulkAddToFolder" class="menu-item bulk-menu-item js_absorb-clicked-child add-to-folder">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Add&nbsp;<span class="js_count"></span>to <small>a folder</small>
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
            
            <div id="hlBulkRemoveFromFolder" class="menu-item bulk-menu-item remove-from-folder">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Remove&nbsp;<span class="js_count"></span>from this folder
                </div>
            </div>
	
	        <!-- Unflag -->
	        
	        <div id="hlBulkRemoveFromFlagList" class="menu-item bulk-menu-item remove-from-flaglist">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
    	            Remove (unflag)&nbsp;<span class="js_count"></span>from this folder
                </div>
            </div>
	
	        <!-- Add to job ad -->
	        
	        <div id="hlBulkAddToJobAd" class="menu-item bulk-menu-item js_absorb-clicked-child add-to-jobad">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
    	            Add&nbsp;<span class="js_count"></span>to a job ad
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

            <div id="hlBulkSaveResume" class="menu-item bulk-menu-item js_absorb-clicked-child js_access-resume-action js_credit-action">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Save&nbsp;<span class="js_count"></span>resume<span class="js_suffix"></span> somehow
                </div>
                <div class="menu-submenu-button">
                </div>
                <div class="desktop-menu-submenu">
                    <div class="menu-panel" style="display: none;">
                        <div class="menu-panel-inner">
                            <div id="hlBulkDownloadResume" class="menu-item bulk-menu-item js_default-clicked-child save-as-doc js_access-resume-action js_credit-action" data-item-shorttext="Download as ZIP">
								<div class="icon"></div>
                                Download&nbsp;<span class="js_count"></span>resume<span class="js_suffix"></span> as ZIP
                			</div>
                			<div id="hlBulkEmailResume" class="menu-item bulk-menu-item save-to-email js_access-resume-action js_credit-action" data-item-shorttext="Email to me">
								<div class="icon"></div>
                                Email&nbsp;<span class="js_count"></span>resume<span class="js_suffix"></span> to me
                			</div>
                		</div>
                    </div>
                </div>
            </div>
            
            <div class="menu-item bulk-menu-item divider">
                <div class="desktop-menu-item-content">
                </div>
            </div>
            
            <!-- Add note -->
            
	        <div id="hlBulkAddNotes" class="menu-item bulk-menu-item add-note">
				<div class="icon"></div>
	            <div class="desktop-menu-item-content">
	                Bulk add&nbsp;<span class="js_count"></span>note<span class="js_suffix"></span>
                </div>
            </div>
            
            <div class="menu-item bulk-menu-item divider">
                <div class="desktop-menu-item-content">
                </div>
            </div>
            
            <!-- Block -->
            
            <div id="hlBulkBlockFromSearch" class="menu-item bulk-menu-item js_absorb-clicked-child block-from-search">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Block&nbsp;<span class="js_count"></span>from <small>current search</small>
                </div>
                <div class="menu-submenu-button">
                </div>
                <div class="desktop-menu-submenu">
                    <div class="menu-panel" style="display: none;">
                        <div class="menu-panel-inner">
                            <div id="hlBulkChildBlockFromCurrentSearch" class="menu-item bulk-menu-item js_default-clicked-child child-block-from-current" data-item-shorttext="current search">
								<div class="icon"></div>
                                Block&nbsp;<span class="js_count"></span>from current search
                			</div>
                			<div id="hlBulkChildBlockFromAllSearches" class="menu-item bulk-menu-item child-block-from-all" data-item-shorttext="all searches block-from-search">
								<div class="icon"></div>
                                Block&nbsp;<span class="js_count"></span>from all searches
                			</div>
                        </div>
                    </div>
                </div>
            </div>
	
            <div id="hlBulkBlockFromAllSearches" class="menu-item bulk-menu-item block-from-all manage-candidate" data-item-shorttext="all searches block-from-search">
				<div class="icon"></div>
                <div class="desktop-menu-item-content">
                    Block&nbsp;<span class="js_count"></span>from <small>all searches</small>
                </div>
            </div>
            
	        <!-- Restore -->
	        
	        <div id="hlBulkRestoreFromBlockList" class="menu-item bulk-menu-item remove-from-blocklist">
				<div class="icon"></div>
	            <div class="desktop-menu-item-content">
                    Restore&nbsp;<span class="js_count"></span>candidate<span class="js_suffix"></span>
                </div>
            </div>
            
        </div>
    </div>
</div>

