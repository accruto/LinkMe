<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<FoldersModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

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
        <%= Html.StyleSheet(StyleSheets.FlagLists) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Error) %>
        <%= Html.StyleSheet(StyleSheets.Overlay) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
        <%= Html.StyleSheet(StyleSheets.JQuerySlider) %>
        <%= Html.StyleSheet(StyleSheets.JQueryTabs) %>
        <%= Html.StyleSheet(StyleSheets.JQueryDragDrop) %>
        <%= Html.StyleSheet(StyleSheets.Pagination) %>
        <%= Html.StyleSheet(StyleSheets.Notes) %>
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
        <%= Html.JavaScript(JavaScripts.FlagLists) %>
        <%= Html.JavaScript(JavaScripts.BlockLists) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.Search) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JsonManipulations) %>
        <%= Html.JavaScript(JavaScripts.Tabs) %>
        <%= Html.JavaScript(JavaScripts.ToggleCheckbox) %>
        <%= Html.JavaScript(JavaScripts.Tooltips) %>
        <%= Html.JavaScript(JavaScripts.CenterAlign) %>
        <%= Html.JavaScript(JavaScripts.HoverIntent) %>
        <%= Html.JavaScript(JavaScripts.Notes) %>
        <%= Html.JavaScript(JavaScripts.FileUpload) %>
        <%= Html.JavaScript(JavaScripts.FileUploadUi) %>
    </mvc:RegisterJavaScripts>
</asp:Content>    

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Manage folders</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div>
	    <div class="section">
		    <div class="section-content">
			    <ul class="plain_action-list action-list">
				    <li><%= Html.RouteRefLink("New search", SearchRoutes.Search, null, new {@class = "new-search-action"}) %></li>
			    </ul>
		    </div>
	    </div>

        <% Html.RenderPartial("BlockListsSection"); %>

	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Manage folders</h1>
    </div>
    
    <div class="manage-folders">
        
	    <div class="forced-first_section section">
	        <div class="section-title">
                <h2>Private folders</h2>
            </div>
            <div class="section-content">
            
                <table class="list">
                    <tbody>
                        <tr class="item item_odd">
                            <td><%= Html.RouteRefLink(Model.FlagList.GetNameDisplayText(), CandidatesRoutes.FlagList) %> (<%= Model.FolderData[Model.FlagList.Id].Count %>)</td>
                        </tr>
                        <tr class="item item_even">
                            <td>
<%  var shortlistData = Model.FolderData[Model.ShortlistFolder.Id]; %>
                                <div class="display-item">
                                    <span style="float:left;"><span id="name-<%= Model.ShortlistFolder.Id %>"><%= Html.RouteRefLink(Model.ShortlistFolder.GetNameDisplayText(), CandidatesRoutes.Folder, new { folderId = Model.ShortlistFolder.Id })%></span> (<%= shortlistData.Count %>)</span>
                                    <span style="float:right;" class="action-list">
<%  if (shortlistData.CanRename) { %>
                                        <a href="javascript:void(0);" class="edit-action js_rename-folder">Rename</a>
<%  } %>
                                    </span>
                                </div>
                                <div class="renamable-item" style="display:none;">
                                    <div class="cfolder_editable">
                                        <input type="text" onkeypress="if (event.keyCode == 13) { $('save-<%= Model.ShortlistFolder.Id %>').click(); return CancelEvent(event); } else if (event.keyCode == 27) { $('cancel-<%= Model.ShortlistFolder.Id %>').click(); }" maxlength="75" value="<%= Model.ShortlistFolder.GetNameDisplayText() %>" class="rename-folder" id="rename-<%=Model.ShortlistFolder.Id%>" />
                                        <input type="button" class="save-button js_rename" id="save-<%=Model.ShortlistFolder.Id%>" />
                                        <input type="button" class="cancel-button js_cancel" id="cancel-<%=Model.ShortlistFolder.Id%>" />
                                    </div>
                                </div>
                            </td>
                        </tr>
<%  for (var index = 0; index < Model.PrivateFolders.Count; ++index)
    {
        var folder = Model.PrivateFolders[index];
        var data = Model.FolderData[folder.Id]; %>
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td>
                                <div class="display-item">
                                    <span style="float:left;"><span id="name-<%= folder.Id %>"><%= Html.RouteRefLink(folder.GetNameDisplayText(), CandidatesRoutes.Folder, new { folderId = folder.Id })%></span> (<%=data.Count%>)</span>
                                    <span style="float:right;" class="action-list">
<%      if (data.CanRename) { %>
                                        <a href="javascript:void(0);" class="edit-action js_rename-folder">Rename</a>
<%      }
        if (data.CanDelete) { %>
                                        <a href="javascript:void(0);" id="delete-<%= folder.Id %>" class="delete-action js_delete-folder js_<%=folder.FolderType%>">Remove</a>
<%      } %>
                                    </span>
                                </div>
                                <div class="renamable-item" style="display:none;">
                                    <div class="cfolder_editable">
                                        <input type="text" onkeypress="if (event.keyCode == 13) { $('save-<%=folder.Id%>').click(); return CancelEvent(event); } else if (event.keyCode == 27) { $('cancel-<%=folder.Id%>').click(); }" maxlength="75" value="<%=folder.Name%>" class="rename-folder" id="rename-<%=folder.Id%>" />
                                        <input type="button" class="save-button js_rename" id="save-<%=folder.Id%>" />
                                        <input type="button" class="cancel-button js_cancel" id="cancel-<%=folder.Id%>" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                <% } %>
                        
                    </tbody>
                </table>
            
                <ul class="horizontal_action-list action-list">
                    <li><a href="javascript: void(0);" class="add-action js_add-private-folder">Add folder</a></li>
                </ul>
            </div>
	    </div>
	    
	    <div class="section">
	        <div class="section-title">
                <h2>Shared folders</h2>
            </div>
            <div class="section-content">
                <p>
                    Your colleagues can also see the candidates in these shared folders.
                    <img alt="" onmouseout="javascript:LinkMeUI.TooltipBehaviour.mouseOut(event, 'add_shared_img', 'add_shared_tip');" onmouseover="javascript:LinkMeUI.TooltipBehaviour.mouseOver(event, 'add_shared_img', 'add_shared_tip', 'Shared folders are a place for you to share candidate details with other users in your organisation.&lt;br /&gt;&lt;br /&gt;Your colleagues also have the ability to manage shared folders.&lt;br /&gt;&lt;br /&gt; People outside of your organisation cannot see these shared folders.');" src="<%= Images.Help %>" class="helpicon" id="add_shared_img" />
                </p>
                
                <table class="list">
                    <tbody>
                
<%  for (var index = 0; index < Model.SharedFolders.Count; ++index)
    {
        var folder = Model.SharedFolders[index];
        var data = Model.FolderData[folder.Id]; %>
                        
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td>
                                <div class="display-item">
                                    <span style="float:left;"><span id="name-<%=folder.Id%>"><%= Html.RouteRefLink(folder.Name, CandidatesRoutes.Folder, new { folderId = folder.Id })%></span> (<%=data.Count%>)</span>
                                    <span style="float:right;" class="action-list">
<%      if (data.CanRename) { %>
                                        <a href="javascript:void(0);" class="edit-action js_rename-folder">Rename</a>
<%      }
        if (data.CanDelete) { %>
                                        <a href="javascript:void(0);" id="delete-<%= folder.Id %>" class="delete-action js_delete-folder js_<%=folder.FolderType%>">Remove</a>
<%      } %>
                                    </span>
                                </div>
                                <div class="renamable-item" style="display:none;">
                                    <div class="cfolder_editable">
                                        <input type="text" onkeypress="if (event.keyCode == 13) { $('save-<%=folder.Id%>').click(); return CancelEvent(event); } else if (event.keyCode == 27) { $('cancel-<%=folder.Id%>').click(); }" maxlength="75" value="<%=folder.Name%>" class="rename-folder" id="rename-<%=folder.Id%>" />
                                        <input type="button" class="save-button js_rename" id="save-<%=folder.Id%>" />
                                        <input type="button" class="cancel-button js_cancel" id="cancel-<%=folder.Id%>" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                <% } %>
                        
                    </tbody>
                </table>

                <ul class="horizontal_action-list action-list">
                    <li><a href="javascript: void(0);" class="add-action js_add-shared-folder">Add folder</a></li>
                </ul>
            </div>
	    </div>
	    <div class="overlay-container forms_v2">
	        <div class="folder-overlay shadow" style="display:none;">
		        <div class="overlay">
			        <div class="overlay-title"><span class="overlay-title-text"> Title </span><span class="close-icon"></span></div>
			        <div class="overlay-content">
			             <div class="overlay-text">Text goes here</div>
			             <div class="buttons-holder">
	                     </div>
			        </div>
		        </div>
	        </div>
        </div>
    </div>
    
    <script type="text/javascript">
        (function($) {

            $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(true);
            initializeBlockLists();

            $(".js_add-private-folder").click(function() {
                return showAddFolderOverlay(true, null, function() { window.location.reload(); });
            });
            $(".js_add-shared-folder").click(function() {
                return showAddFolderOverlay(false, null, function() { window.location.reload(); });
            });

            $(".js_rename-folder").click(function() {
                $(this).closest("tr").find(".renamable-item").css("display", "block");
                $(this).closest("tr").find(".display-item").css("display", "none");
            });
            $(".js_delete-folder").click(function() {
                var folderId = $(this).attr("id").slice(7);
                showDeleteFolderOverlay(folderId, $(this).hasClass("js_Private"), function() {
                    window.location.reload();
                });
            });

            $(".js_cancel").click(function() {
                $(this).closest("tr").find(".display-item").css("display", "block");
                $(this).closest("tr").find(".renamable-item").css("display", "none");
            });
            $(".js_rename").click(function() {
                var renameFolderId = $(this).attr("id").slice(5);
                renameFolder(
                    renameFolderId,
                    $("#name-" + renameFolderId).find("a").html(),
                    $("#rename-" + renameFolderId).val(),
                    function(folderId, name) {
                        $("#name-" + folderId).find("a").html(name);
                    },
                    function(folderId) {
                        $("#name-" + folderId).closest("tr").find(".display-item").css("display", "block");
                        $("#name-" + folderId).closest("tr").find(".renamable-item").css("display", "none");
                    });
            });

        })(jQuery);
    </script>
</asp:Content>

