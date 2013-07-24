<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Employers"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<div class="jobads_ascx section js_jobads_collapsible">
    <div class="section-collapsible-title main-title">
		<div class="jobads-section-icon"></div>
        <div class="main-title-nav-icon">LinkMe job ads</div>
    </div>
    
    <div class="section-content section-collapsible-content">
        <div class="open-jobads_section section">
            <div class="section-collapsible-title droppable">
                Open LinkMe job ads
            </div>
            <div class="section-content">
                <div id="open-jobads">
                    <ul class="open-jobads-list">
                    </ul>
                </div>
            </div>
        </div>
        <div class="closed-jobads_section section">
            <div class="section-collapsible-title droppable">
                Closed LinkMe job ads
            </div>
            <div class="section-content">
                <div id="closed-jobads">
                    <ul class="closed-jobads-list">
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="overlay-container forms_v2">
	    <div class="jobad-overlay shadow" style="display:none;">
		    <div class="overlay">
			    <div class="overlay-title"><span class="overlay-title-text">Title</span><span class="close-icon"></span></div>
			    <div class="overlay-content">
			        <div class="overlay-text">Text goes here</div>
                    <div class="buttons-holder"></div>
                </div>
            </div>
        </div>
    </div>
</div>
