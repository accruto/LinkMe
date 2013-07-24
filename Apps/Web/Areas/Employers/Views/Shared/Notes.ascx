<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<EmployerMemberView>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Views"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>

<div class="add-notes_holder">
    <div class="add-notes_button" onclick="onClickAddNote(this);">
        <a class="add-action" href="javascript: void(0);">Add note</a>
    </div>
    <div class="add-notes_section" style="display:none;">
        <div class="notes-textarea_holder">
            <textarea class="notes-textarea" maxlength="500"></textarea>
        </div>
        <div class="buttons_holder">
            <input class="save-button button" onclick="onClickSaveNote(this);" name="save-note" type="button" value="Save"/><input class="cancel-button button" onclick="onClickCancelAddNote(this);" name="cancel" type="button" value="Cancel" />
        </div>
        <div class="note-type_holder">
            <span><input name="<%= Model.Id %>_noteType" id="personal" value="false" type="radio"/> Personal</span><span><input name="<%= Model.Id %>_noteType" id="org-wide" value="true" type="radio"/> Organisation-wide <img src="<%= Images.Help %>" class="help js_tooltip" data-tooltip="A note marked as Organisation-wide will be visible to any of your colleagues who work for the same organisation as you. They will note be able to edit or delete your notes." /></span>
        </div>                              
    </div>
</div>