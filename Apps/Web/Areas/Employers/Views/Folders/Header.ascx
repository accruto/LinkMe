<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<FolderListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<div id="folder-header-container">
    <div id="folder-header" class="folder-header">
        <div class="results_page-title page-title">
            <div>
                <h1>
                    <span id="folder-header-text" style="float:left;"></span>
                    <span style="float:right;" class="action-list">
                    <% if (Model.FolderData.CanRename) { %>
                        <a href="javascript:void(0);" class="edit-action js_rename-folder">Rename</a>
                    <% } %>
                    <% if (Model.FolderData.CanDelete) { %>
                        <a href="javascript:void(0);" id="del_<%=Model.Folder.Id%>" class="delete-action js_delete-folder js_<%=Model.Folder.FolderType%>">Remove</a>
                    <% } %>
                    </span>
                </h1>
            </div>
        </div>    
    </div>
    
    <div id="rename-folder-header" class="rename-folder-header" style="display:none;">
        <div class="results_page-title page-title">
            <div>
                <h1>
                    <div class="cfolder_editable" style="">
                        <input type="text" onkeypress="if (event.keyCode == 13) { $('save-<%=Model.Folder.Id%>').click(); return CancelEvent(event); } else if (event.keyCode == 27) { $('cancel-<%=Model.Folder.Id%>').click(); }" maxlength="75" value="<%= Model.Folder.GetNameDisplayText() %>" class="rename-folder" id="rename-<%=Model.Folder.Id%>">
                        <input type="button" class="save-button js_rename" id="save-<%=Model.Folder.Id%>">
                        <input type="button" class="cancel-button js_cancel" id="cancel-<%=Model.Folder.Id%>">
                        <div style="display: none;" class="error-message cfolder_error"></div>
                    </div>
                </h1>
            </div>
            <script type="text/javascript">
                (function($) {
                    $(".js_cancel").click(function() {
                        $("#folder-header").css("display", "block");
                        $("#rename-folder-header").css("display", "none");
                    });
                    $(".js_rename").click(function() {
                        renameFolder(
                            "<%=Model.Folder.Id%>",
                            "<%= Model.Folder.GetNameDisplayText() %>",
                            $("#rename-<%=Model.Folder.Id%>").val(),
                            function(folderId, name) {
                                updateFolders(candidateContext);
                            },
                            function(folderId) {
                                $("#folder-header").css("display", "block");
                                $("#rename-folder-header").css("display", "none");
                            });
                    });
                })(jQuery);
            </script>
        </div>
    </div>
    
</div>
    

