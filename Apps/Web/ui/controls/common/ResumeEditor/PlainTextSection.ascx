<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PlainTextSection.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.PlainTextSection" %>
<%@ Import namespace="LinkMe.Utility.Utilities"%>
<%@ Import Namespace="LinkMe.Framework.Utility" %>

<% if (AllowEditing || HaveContent) { %>
<div id="div<%= SectionName %>" class="plaintext_resume_section resume_section section">
	<% if (AllowEditing) { %>
		<script type="text/javascript">
			function Update<%= SectionName %>(callback) {
				var fieldValue = $F('txt<%= SectionName %>').stripTags();
				var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.Save<%= SectionName %>(fieldValue, callback);
			}
		</script>

		<span class="resume_error"></span>

		<div id="span<%= SectionName %>NoContent" class="empty_resume-section-view resume-section-view" style="display: <%= !HaveContent ? "inherit" : "none" %>;">
			<div class="section-title">
			    <h2><%= SectionDisplayName %></h2>
			    <ul class="corner_inline_action-list inline_action-list action-list">
			        <li><a href="javascript:void(0);" class="edit-action js_edit">Edit</a></li>
			    </ul>
			</div>
			<div class="section-content">
			    <%= NoContentText %>
			</div>
		</div>

		<div id="span<%= SectionName %>Editable" class="editing_resume-section-view resume-section-view forms_v2" style="display: none;">
			<div class="section-title">
			    <h2><%= SectionDisplayName %></h2>
			    <ul class="corner_inline_action-list inline_action-list action-list">
			        <li><a href="javascript:void(0);" class="save-action js_update">Save</a></li>
			        <li><a href="javascript:void(0);" class="cancel-action js_cancel">Cancel</a></li>
			    </ul>
			</div>
			<div class="section-content">
		        <fieldset>
	                <div class="multiline_textbox_field textbox_field field">
                        <div class="multiline_textbox_control textbox_control control">
				            <textarea id="txt<%= SectionName %>" class="multiline_textbox textbox" rows="5" style="width:100%"><% = Content %></textarea>
				        </div>
				    </div>
			    </fieldset>
			    <ul class="button_action-list action-list">
				    <li><input type="button" value="Update" class="save-button button js_button-update" /></li>
				    <li><input type="button" value="Cancel" class="cancel-button button js_button-cancel" /></li>
			    </ul>
			</div>
		</div>
	<% } %>
	<div id="span<%= SectionName %>Display" class="displaying_resume-section-view resume-section-view forms_v2" style="display: <%= HaveContent ? "inherit" : "none" %>;">
		<div class="section-title">
		    <h2><%= SectionDisplayName %></h2>
		    <% if (AllowEditing) { %>
			    <ul class="corner_inline_action-list inline_action-list action-list">
			        <li><a href="javascript:void(0);" class="edit-action js_edit">Edit</a></li>
			    </ul>
			<% } %>
		</div>
		<div class="section-content">
		    <fieldset>
	            <div class="multiline_textbox_field textbox_field read-only_field field">
                    <div class="multiline_textbox_control textbox_control control">
                        <div id="lbl<%= SectionName %>" class="multiline_textbox textbox" for="txt<%= SectionName %>"><%= HtmlUtil.LineBreaksToHtml(GetContentForDisplay()) %></div>
                    </div>
                </div>
            </fieldset>
        </div>
	</div>
	<% if (AllowEditing) { %>
		<script type="text/javascript">
            Event.observe(window, 'load', function() {            
			var editor = LinkMeUI.Editor.RegisterEditableControl(
					{classNameContainingControl: 'resume_section',
					classNameEditable : 'editing_resume-section-view',
					classNameDisplay: 'displaying_resume-section-view',
					classNameNoContents: 'empty_resume-section-view',
					classNameError : 'resume_error',
					classNameEditLink : 'js_edit',
					classNameUpdateLink : 'js_update',
					classNameCancelLink : 'js_cancel',
					classNameButtonUpdate : 'js_button-update',
					classNameButtonCancel : 'js_button-cancel',
					disableOtherControls :  false,
					control : 'div<%= SectionName %>',
					userUpdateFunction : 'Update<%= SectionName %>',
					userPostUpdateFunction : 'DisplayErrors',
					userPostDeleteFunction : 'DisplayErrors' });

                <% if (StartEditingOnLoad) { %>
                    LinkMeUI.Editor.LoaderGlobalCallback = editor.Edit.bindAsEventListener(editor);
                <% } %>
            });         
		</script>
	<% } %>
</div>
<% } %>