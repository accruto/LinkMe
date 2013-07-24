<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Shared.CandidateList" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Views"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>

<div>
    <script language="javascript" type="text/javascript">

        (function($) {
            $.extend(candidateContext,
            {
                candidateIds: [<%= GetCandidateContext(Model.Results.CandidateIds) %>],
                
                canContactWithoutCredit: [<%= GetCanContact(CanContactStatus.YesWithoutCredit) %>],
                canContactWithCredit: [<%= GetCanContact(CanContactStatus.YesWithCredit) %>],
                cannotContact: [<%= GetCannotContact() %>],
                
                canContactByPhoneWithoutCredit: [<%= GetCanContactByPhone(CanContactStatus.YesWithoutCredit) %>],
                canContactByPhoneWithCredit: [<%= GetCanContactByPhone(CanContactStatus.YesWithCredit) %>],
                cannotContactByPhone: [<%= GetCannotContactByPhone() %>],
                
                canAccessResumeWithoutCredit: [<%= GetCanAccessResume(CanContactStatus.YesWithoutCredit) %>],
                canAccessResumeWithCredit: [<%= GetCanAccessResume(CanContactStatus.YesWithCredit) %>],
                cannotAccessResume: [<%= GetCannotAccessResume() %>],
                
                rejected: <% if (Model.Results.RejectedCandidateIds == null) {%>null<% } else { %>[<%= GetCandidateContext(Model.Results.RejectedCandidateIds) %>]<% } %>
            });
            
            candidateContext.selectedCandidateIds = getIntersection(candidateContext.selectedCandidateIds, candidateContext.candidateIds);
            
<%  var hash = Model.GetHash();
    if (!string.IsNullOrEmpty(hash))
    { %>
        if (candidateContext.setHash) {
            window.location.hash = "#<%=hash%>";
        }
<%  } %>
        })(jQuery);
        
        function initializeJobHunterStatusCount(){    
            (function($) {  
<%              foreach (var candidateStatusHit in Model.Results.CandidateStatusHits)
                { %>
                    $(".<%= candidateStatusHit.Key %>_pushcheck").removeClass("<%= candidateStatusHit.Key %>_disabled_pushcheck").removeClass("disabled_pushcheck");
<%              } %>
            })(jQuery);
        }
        
        function updateJobHunterStatusCount(){    
            (function($) { 
                var tempValue = "0";
                $(".candidate-status-count").text("0");   
<%              foreach (var candidateStatusHit in Model.Results.CandidateStatusHits)
                { %>      
                    tempValue = toFormattedDigits("<%= candidateStatusHit.Value %>");   
                    $(".<%= candidateStatusHit.Key %>").text(tempValue);
<%              } %>
            })(jQuery);
        }
        
        function initializeJobTypeCount(){    
            (function($) {  
<%              foreach (var jobTypeHit in Model.Results.DesiredJobTypeHits)
                { %>
                    $(".<%= jobTypeHit.Key %>_pushcheck").removeClass("<%= jobTypeHit.Key %>_disabled_pushcheck").removeClass("disabled_pushcheck");
<%              } %>
            })(jQuery);
        }
        
        function updateJobTypeCount(){    
            (function($) { 
                var tempValue = "0";
                $(".job-type-count").text("0");   
<%              foreach (var jobTypeHit in Model.Results.DesiredJobTypeHits)
                { %>  
                    tempValue = toFormattedDigits("<%= jobTypeHit.Value %>");       
                    $(".<%= jobTypeHit.Key %>").text(tempValue);                
<%              } %>
            })(jQuery);
        }       
    
        function initializeIndustriesCount(){    
            (function($) {
                $(".industries_section").find(".child-holder").removeClass("disabled-industry");
                makeInitialToggleCheckboxes($(".js_toggle_checkboxes"), false);          
                $(".js_toggle_checkboxes").toggleCheckboxes();
                makeInitialCollapsibleDisplay($(".js_collapsible_toggle_checkboxes"), 5, 10);
                $(".js_collapsible_toggle_checkboxes").makeCollapsibleDisplay(5, 10);
            })(jQuery);
        }
        
        function updateIndustriesCount(){    
            (function($) { 
                var tempValue = "0";
                $(".industries-count").text("0");   
                <% foreach (var industriesHit in Model.Results.IndustryHits) { %>   
                    tempValue = toFormattedDigits("<%= industriesHit.Value %>");  
                    $(".<%= industriesHit.Key %>").text(tempValue);
                <% } %>
            })(jQuery);
        }
        
        function updateFilterCounts(){
            updateJobHunterStatusCount();
            updateJobTypeCount();
            updateIndustriesCount();
        }
        
        function initializeFilterCounts(){
            initializeJobHunterStatusCount();
            initializeJobTypeCount();
            initializeIndustriesCount();
        }

    </script>

    <div class="search-results_ascx" id="search-results">

<%  Html.RenderPartial("Title", Model); %>

    <div id="search-recovery-container">
        <% Html.RenderPartial("Recovery", Model); %>
    </div>
    
<%  if (Model.Results.CandidateIds != null && Model.Results.CandidateIds.Count > 0)
    { %>
       
        <div id="bulk-action">
            <div class="bulk-select_holder">
                <div class="js_select-all-checkbox bulk-select-checkbox_holder">
                    <input type="checkbox" />
                </div>
                <div class="bulk-select-text_outer-holder">
                    <div class="js_bulk-toggler">
                        <div class="bulk-select-text_holder">
                            <span class="bulk-select_text">Act on&nbsp;<span class="selected_count"></span>selected result<span class="selected_suffix">s</span></span>
                        </div>
                    </div>
                </div>
<%      if (Model.Presentation.DetailLevel == DetailLevel.Compact)
        { %>
                    <span class="column-heading flag">Flag</span>
                    <span class="column-heading desired-salary">Desired salary</span>
                    <span class="column-heading tenure">Tenure</span>
                    <span class="column-heading most-recent-jobs">Most recent jobs</span>
<%      } %>
            </div>
        </div>
        
        <div class="add-notes_holder bulk-notes_holder"  style="display:none;">
            <div class="add-notes_section">
                <div class="notes-textarea_holder">
                    <textarea class="notes-textarea" maxlength="500"></textarea>
                </div>
                <div class="buttons_holder">
                    <input class="save-button button" onclick="onClickSaveBulkNotes();" name="save-note" type="button" value="Save"/><input class="cancel-button button" onclick="onClickCancelAddBulkNotes();" name="cancel" type="button" value="Cancel" />
                </div>
                <div class="note-type_holder">
                    <span><input name="bulk_noteType" id="personal" value="false" type="radio"/> Personal</span><span><input name="bulk_noteType" id="org-wide" value="true" type="radio"/> Organisation-wide <img src="<%= Images.Help %>" class="help js_tooltip" data-tooltip="A note marked as Organisation-wide will be visible to any of your colleagues who work for the same organisation as you. They will note be able to edit or delete your notes." /></span>
                </div>                              
            </div>
        </div>
            
<%      Html.RenderPartial(Model.Presentation.DetailLevel.ToString("G"), Model);
        Html.RenderPartial("Pagination", Model);
        Html.RenderPartial("BulkActions");
        Html.RenderPartial("Actions"); %>
   
        <script language="javascript" type="text/javascript">
            (function($) {
            
                var searchResults = $("#search-results");

                // Expando
                searchResults.find(".js_expando").each(function() {
                    var expando = $(this);
                    expando.css("display", "");  // Show expando when JavaScript is on, otherwise don't.
                    expando.click(function() {
                        $("#" + $(this).attr("data-for")).toggle();
                        expando.toggleClass("open_expando");
                        expando.toggleClass("close_expando");
                        $("#search-results .js_skill-ellipsis").skillEllipsis();
						contentExpandedInCompactView(this);
                    });
                });

                searchResults.find(".additional-details .js_ellipsis").snippetEllipsis();
                searchResults.find(".js_ellipsis").ellipsis();

                $(".js_tooltip").addTooltip();

<%      if (CurrentEmployer != null)
        { %>
                // Drag
                
                $(".draggable").draggable({
                    revert: 'invalid',
                    helper: 'clone',
                    cursorAt: { top: 8, left: 8 },
                    drag: function(event, ui) {
                        if($(this).hasClass("selected_candidate_search-result")){
                            if(candidateContext.selectedCandidateIds.length == 1){
                                $(".ui-draggable-dragging").text($(this).find(".candidate-name").find("a").text());
                            } else {
                                $(".ui-draggable-dragging").text("Multiple candidates");
                            }
                        } else {
                            $(".ui-draggable-dragging").text($(this).find(".candidate-name").find("a").text());
                        }
                    }
                });

<%      } %>
			$(document).ready(function() {
				//append special class for IE
				if ($.browser.msie) {
					if ($.browser.version.indexOf("9") == 0) {
						$(".flag-holder").addClass("IE9");
						$(".search-result-body").addClass("IE9");
						$(".basic-details-sublayout").addClass("IE9");
					}
					if ($.browser.version.indexOf("8") == 0) $(".flag-holder").addClass("IE8");
					if ($.browser.version.indexOf("7") == 0) {
						$(".search-result").addClass("IE7");
						$(".basic-details_holder").addClass("IE7");
						$(".search-result-header").addClass("IE7");
						$(".search-result-body").addClass("IE7");
						$("#hlAddToJobAd .menu-submenu-button").addClass("IE7");
						$("#hlBulkAddToJobAd .menu-submenu-button").addClass("IE7");
					}
					$(".icon").addClass("IE");
					$(".block-icon").addClass("IE");
				}
				if ($.browser.mozilla) $(".icon").addClass("FireFox");
				$("ul.unlocking-and-phones").each(function() {
				    $(this).find("li:eq(2)").addClass("withdivider");
				});
			});

            })(jQuery);
        </script>
        
<%  }
    else
    {
        Html.RenderPartial("NoResults", Model);
    } %>

    </div>
</div>
