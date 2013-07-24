<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Web.Areas.Employers.Views.Candidates.Resumes" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.Context" %>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Employers"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Views"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<asp:Content ContentPlaceHolderID="MetaTags" runat="server">
    <%= Html.MetaTag("robots", "noindex") %>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <% Page.Title = Model.CurrentCandidate.View.GetPageTitle(); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarOnLeft) %>
        <%= Html.StyleSheet(StyleSheets.SearchResults) %>
        <%= Html.StyleSheet(StyleSheets.JQueryWidgets) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.EmployerLoggedInFrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.Folders) %>
        <%= Html.StyleSheet(StyleSheets.Error) %>
        <%= Html.StyleSheet(StyleSheets.Overlay) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
        <%= Html.StyleSheet(StyleSheets.JQuerySlider) %>
        <%= Html.StyleSheet(StyleSheets.JQueryTabs) %>
        <%= Html.StyleSheet(StyleSheets.Pagination) %>
        <%= Html.StyleSheet(StyleSheets.Notes) %>
        <%= Html.StyleSheet(StyleSheets.ViewResume) %>
        <%= Html.StyleSheet(StyleSheets.JQueryDragDrop) %>
        <%= Html.StyleSheet(StyleSheets.JQueryFileUploadUi) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">

    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftAjax) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftMvcAjax) %>
        <%= Html.JavaScript(JavaScripts.CustomCheckbox) %>
        <%= Html.JavaScript(JavaScripts.AlignWith) %>
        <%= Html.JavaScript(JavaScripts.DesktopMenu) %>
        <%= Html.JavaScript(JavaScripts.TextOverflow) %>
        <%= Html.JavaScript(JavaScripts.Download) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.SectionCollapsible) %>
        <%= Html.JavaScript(JavaScripts.Slider) %>
        <%= Html.JavaScript(JavaScripts.EApi) %>
        <%= Html.JavaScript(JavaScripts.EmployersApi) %>
        <%= Html.JavaScript(JavaScripts.Credits) %>
        <%= Html.JavaScript(JavaScripts.Overlay) %>
        <%= Html.JavaScript(JavaScripts.Actions) %>
        <%= Html.JavaScript(JavaScripts.Folders) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.FlagLists) %>
        <%= Html.JavaScript(JavaScripts.Search) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JsonManipulations) %>
        <%= Html.JavaScript(JavaScripts.Tabs) %>
        <%= Html.JavaScript(JavaScripts.ToggleCheckbox) %>
        <%= Html.JavaScript(JavaScripts.Tooltips) %>
        <%= Html.JavaScript(JavaScripts.CenterAlign) %>
        <%= Html.JavaScript(JavaScripts.HoverIntent) %>
        <%= Html.JavaScript(JavaScripts.Notes) %>
        <%= Html.JavaScript(JavaScripts.ViewResume) %>
        <%= Html.JavaScript(JavaScripts.FileUpload) %>
        <%= Html.JavaScript(JavaScripts.FileUploadUi) %>
        <%= Html.JavaScript(JavaScripts.TinyMceSrc) %>
        <%= Html.JavaScript(JavaScripts.JqueryTinyMce) %>
        <%= Html.JavaScript(JavaScripts.JqueryTinyMceInit) %>
    </mvc:RegisterJavaScripts>

    <script language="javascript" type="text/javascript">

        <% var employerContext = ViewContext.HttpContext.GetEmployerContext(); %>

        var candidateContext = {
            isAnonymous: <%= CurrentEmployer == null ? "true" : "false" %>,
            isSearch: false,
            candidateIds: ["<%= Model.CurrentCandidate.View.Id.ToString() %>"],
            hideCreditReminder: <%= employerContext.IsCreditReminderHidden ? "true" : "false" %>,
            hideBulkCreditReminder: <%= employerContext.IsBulkCreditReminderHidden ? "true" : "false" %>
        };
        
        currentRequest = null;

        function updateResults(isCreditAction) {
            (function($) {

                if (currentRequest)
                    currentRequest.abort();

                showResultsOverlay(function () { updateResults(false); });

                var requestData = "candidateId=" + $("#candidateId").val();

                currentRequest = $.get("<%= Html.RouteRefUrl(CandidatesRoutes.PartialCandidates) %>",
                    requestData,
                    function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showResultsFailedOverlay(function() { updateResults(false); });
                            return;
                        }
                        hideResultsOverlay();
                        $("#resume-container").html(data);
                        initializeResumeActions(candidateContext);
                        currentRequest = null;
                    });

                if (isCreditAction) {
                    $.get("<%= Html.RouteRefUrl(CandidatesRoutes.Credits) %>",
                        null,
                        function(data, textStatus, xmlHttpRequest) {
                            $("#credit-summary-container").html(data);
                            var candidateNameObj = $(".resume-section").find(".basic-details").find(".candidate-name");
                            var candidateName = ($(candidateNameObj).attr("title") == "") ? $(candidateNameObj).text() : candidateName = $(candidateNameObj).attr("title");
                            var candidateNavNameObj = $(".resumes-nav").find("#" + ($(".resume-section").find(".basic-details").attr("data-memberid"))).find(".basic-details").find(".candidate-name");
                            $(candidateNavNameObj).text(candidateName);
                            $(candidateNavNameObj).ellipsis();
                            $(candidateNavNameObj).attr("title", $(candidateNameObj).attr("title"));
                        });
                }

            })(jQuery);
        }
        
<%  if (CurrentEmployer != null)
    { %>
    
        function onClickUnlock(element) {
            (function($) {
                showUnlockOverlay($(element), candidateContext.candidateIds, candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
            })(jQuery);
        }

        function onClickUnlimitedUnlock() {
            (function($) {
                unlock(candidateContext.candidateIds);
            })(jQuery);
        }

        function onClickFlag(element) {
            (function($) {
                $(element).updateCandidateFlag(candidateContext);
            })(jQuery);
        }
        
        function onClickDisplayNotes(element, candidateId) {
            (function($) {
                displayNotes($(element), candidateId);
            })(jQuery);
        }
        
        function onClickAddNote(element) {
            (function($) {
                $(element).closest(".notes-content").find(".add-notes_section").show();
                $(element).closest(".notes-content").find(".add-notes_section").find("#org-wide").attr("checked", "checked");
                $(element).closest(".notes-content").find(".add-notes_button").hide();
            })(jQuery);
        }
        
        function onClickCancelAddNote(element) {
            (function($) {
                $(element).closest(".notes-content").find(".add-notes_button").show();
                $(element).closest(".notes-content").find(".add-notes_section").hide();
            })(jQuery);
        }

        function onClickSaveNote(element) {
            (function($) {
                newNote($(element).closest(".notes-content").attr("id").substring(6));
            })(jQuery);
        }
        
        function onClickAddToNewFolder(candidateId) {
            (function($) {
                showAddFolderOverlay(false, [candidateId], function() { updateFolders(candidateContext); });
            })(jQuery);
        }

<%  }
    else
    { %>

        function onClickLocked() {
            (function($) {
                showLoginOverlay("unlock");
            })(jQuery);
        }

        function onClickFlag(element) {
            (function($) {
                showLoginOverlay("flag");
            })(jQuery);
        }
        
        function onClickDisplayNotes(element, candidateId) {
            (function($) {
                showLoginOverlay("notes");
            })(jQuery);
        }
        
        function onClickAddToNewFolder(candidateId) {
            (function($) {
                showLoginOverlay("addresults");
            })(jQuery);
        }

<%  } %> 

        function onClickJumpToLink(element, elementid){
                $("#" + $(element).attr("jump-to-link")).click();
                /*$("#" + $(element).attr("jump-to-link") + "-details").find(".tabs-inner-container").scrollTop($("#anchor_" + $(element).attr('id')).offset().top);*/
        }   

    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
<%  if (Model.CurrentCandidates is ManageCandidatesNavigation )
    { %>
        <li><a href="<%= NavigationManager.GetUrlForPage<EmployerJobAds>("mode", (((ManageCandidatesNavigation)Model.CurrentCandidates).JobAdStatus.ToString())) %>"><%= ((ManageCandidatesNavigation)Model.CurrentCandidates).JobAdStatus%> job ads</a></li>
<%  }
    else if (Model.CurrentCandidates is SuggestedCandidatesNavigation)
    {%>
        <li><a href="<%= NavigationManager.GetUrlForPage<EmployerJobAds>("mode", (((SuggestedCandidatesNavigation)Model.CurrentCandidates).JobAdStatus.ToString())) %>"><%= ((SuggestedCandidatesNavigation)Model.CurrentCandidates).JobAdStatus%> job ads</a></li>
<%  }
    else if (Model.CurrentCandidates is MemberSearchNavigation) 
    {%>
        <li><%=Html.RouteRefLink("New candidate search", SearchRoutes.Search)%></li>
<%  } else { %>        
        <li><%=Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders)%></li>
<%  } %>        
        <li><%= Html.BreadcrumbLink(Model.CurrentCandidates) %></li>
        <li class="current-breadcrumb">View resumes</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <input type="hidden" value="<%= Model.CurrentCandidate.View.Id %>" id="candidateId" name="candidateId" />
    <div class="resume-left-nav">
        <div>
	        <div class="section">
		        <div class="section-content">
			        <ul class="plain_action-list action-list">
                    <% if (Model.CurrentCandidates != null)
                       { %>
				        <li><%=Html.BackToLink(Model.CurrentCandidates, new {@class = "back-action"})%></li>
                    <% } %>				        
			        </ul>
		        </div>
	        </div>
        </div>
        <div class="folders-resume_ascx section js_folders-resume_collapsible">
            <div class="section-collapsible-title main-title">
                <div class="main-title-nav-icon folders-section-icon">Folders</div>
            </div>
            <div class="section-content drag-area">
	            <div class="section">
	                <div style="border:2px dotted #BEE7FF; font-size:85%; color:#76A7C4; text-align:center; padding:2px;">Drag candidates here</div>
	            </div>
	        </div>
        </div>
        
	    <% Html.RenderPartial("CandidatesNav", Model); %>
	    
	</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <% Html.RenderPartial("Folders"); %>
    
    <div class="resume-section">
        <div id="resume-container">
            <% Html.RenderPartial("Resume", Model); %>
        </div>
        <div id="results-overlay" class="ajax-overlay" style="display: none;">
            <div class="loading">Loading...</div>
            <div class="error" style="display: none;">
                <p>Your search couldn't be processed.</p>
                <ul class="action-list">
                    <li><a id="results-overlay-retry" href="javascript:void(0);">Try again</a></li>
                    <li><a id="results-overlay-no-retry" href="javascript:void(0);">Close this box</a></li>
                </ul>
            </div>
        </div>
    </div>
    
<%  Html.RenderPartial("Overlays"); %>

   <script type="text/javascript">
       (function($) {

           /* Populating Folders */

           initializeResumeFolders(candidateContext);
           initializeResumeActions(candidateContext);
           //$(".js_action-items").initializeActionMenu();

           $(".js_folders-resume_collapsible").makeFolderResumeCollapsible();

           $(".resume-left-nav .js_ellipsis").ellipsis();

           /* Candidate navigation */
           $(".nav_holder").candidateNavigation();
           
<%  if (CurrentEmployer != null)
    { %>           
           // Flag.
           
           $(".js_resume-nav-flag").click(function() {
               $(this).updateCandidateFlag(candidateContext);
           });
<%  }
    else
    { %>
        // Flag.

        $(".js_resume-nav-flag").click(function() {
            showLoginOverlay("flag");
        });
<%  } %>

           /* Initialization for Send Message Actions */
           $('#file_upload').fileUploadAction();
           initEmailRTE();
           $("#insert-pf").click(function() {
               var nameImgUrl = ($(this).closest(".personalization-holder").find(".personalization-field-list").val() == "first") ? candidateFirstNameImgUrl : candidateLastNameImgUrl;
               var nameClass = ($(this).closest(".personalization-holder").find(".personalization-field-list").val() == "first") ? "first-name" : "last-name";
               var nameImgTag = '<img src="' + nameImgUrl + '" class="' + nameClass + '" />';
               tinyMCE.execCommand('mceInsertContent', false, nameImgTag);
               return false;
           });

       })(jQuery);
    </script> 

</asp:Content>

