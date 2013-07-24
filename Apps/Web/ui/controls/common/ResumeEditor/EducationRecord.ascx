<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EducationRecord.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.EducationRecord" %>
<%@ Import namespace="LinkMe.Utility.Utilities"%>

<% if (AllowEditing) { %>
    <script type="text/javascript">
        if(educationControls == null) {
            var educationControls = new Array();
        }            
        
        var edHash = $H({educationIDId: '<%=lblEducationId.ClientID %>', InstitutionId: '<%= txtInstitution.ClientID %>', CityId: '<%= txtCity.ClientID %>', QualificationId: '<%= txtQualification.ClientID %>', MajorId: '<%= txtMajor.ClientID %>', CompletedId: '<%= txtCompleted.ClientID %>', DescriptionId: '<%= txtDescription.ClientID %>'});        
        <% if (School == null) { %>
            educationTemplate = edHash;
        <% } else { %>
            educationControls.push(edHash);
        <% } %>
    </script>
<% } %>

<div id="divEducationRecord" runat="server" class="education_record record">
    <% if (AllowEditing) { %>
        <div class="empty_record-view record-view"></div>
        <span class="resume_error"></span>
    <% } %>

    <div id="divEducationRecordDisplay" runat="server" class="displaying_record-view record-view">
        <div class="section-content forms_v2">
            <% if (AllowEditing) { %>
                <ul class="corner_inline_action-list inline_action-list action-list">
                    <li><a href="javascript:void(0);" class="delete-action js_delete-record">Delete</a></li>
                    <li><a href="javascript:void(0);" class="edit-action js_edit">Edit</a></li>
                </ul>
                <label id="lblEducationId" class="record_id" runat="server"><%= Id %></label>
            <% } %>
            <fieldset>
                <div class="textbox_field read-only_field completeddate_field field">
                    <label>Completion:</label>
                    <div class="textbox_control control">
                        <div id="lblCompleted" class="textbox" for="<%= txtCompleted.ClientID %>"><%= Completed %></div>
                    </div>
                </div>
                
                <div class="textbox_field read-only_field qualification_field field">
                    <label>Qualification</label>
                    <div class="textbox_control control">
                        <div id="lblQualification" class="textbox" for="<%= txtQualification.ClientID %>"><%= Qualification %></div>
                    </div>
                 </div>

                <div class="textbox_field read-only_field major_field field property-hide-if-empty"
					 style="<%= HideIf(string.IsNullOrEmpty(Major)) %>">
                    <label>Major</label>
                    <div class="textbox_control control">
                        <div class="textbox">Major: <span id="lblMajor" for="<%= txtMajor.ClientID %>"><%= Major %></span></div>
                    </div>
                </div>
                    
                <div class="textbox_field read-only_field institution_field field">
                    <label>Institution</label>
                    <div class="textbox_control control">
                        <div id="lblInstitution" class="textbox" for="<%= txtInstitution.ClientID %>"><%= Institution %></div>
                    </div>
                </div>

                <div class="textbox_field read-only_field city_field field property-hide-if-empty"
					 style="<%= HideIf(string.IsNullOrEmpty(City)) %>">
                    <label>City</label>
                    <div class="textbox_control control">
                        <div class="textbox">(<span id="lblCity" for="<%= txtCity.ClientID %>"><%= City %></span>)</div>
                    </div>
                </div>
                    

                <div class="multiline_textbox_field textbox_field read-only_field description_field field property-hide-if-empty"
					 style="<%= HideIf(string.IsNullOrEmpty(Description)) %>">
                    <label>Description</label>
                    <div class="multiline_textbox_control textbox_control control">
                        <div id="lblDescription" class="multiline_textbox textbox" for="<%= txtDescription.ClientID %>"><%= Description %></div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    
    <% if (AllowEditing) { %>
        <div id="divHistoryRecordEdit" class="editing_record-view record-view" style="display: none;">
            <div class="section-content forms_v2">   
                <ul class="corner_inline_action-list inline_action-list action-list">
                    <li><a href="javascript:void(0);" class="save-action js_update">Save</a></li>
                    <li><a href="javascript:void(0);" class="cancel-action js_cancel">Cancel</a></li>
                </ul>
                    
                <fieldset>
                    <div class="textbox_field completeddate_field field">
                        <label>Completion:</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtCompleted" class="textbox js_required-education-date" runat="server" />
                        </div>
                        <div class="example_helptext helptext">
                            For example, Jan 2008
                        </div>
                    </div>
                    
                    <div class="textbox_field qualification_field field">
                        <label>Qualification</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtQualification" class="textbox" nocontent="Qualification" runat="server" />
                        </div>
                    </div>
                    
                    <div class="textbox_field major_field field">
                        <label>Major</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtMajor" class="textbox" nocontent="Major" runat="server" />
                        </div>
                    </div>
                    
                    <div class="textbox_field institution_field field">
                        <label>Institution</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtInstitution" class="textbox" nocontent="Institution" runat="server" />
                        </div>
                    </div>
                    <div class="textbox_field city_field field">
                        <label>City</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtCity" class="textbox" nocontent="City" runat="server" />
                        </div>
                    </div>
                    
                    <div class="multiline_textbox_field textbox_field description_field field">
                        <label>Description</label>
                        <div class="multiline_textbox_control textbox_control control">
                            <textarea id="txtDescription" rows="5" cols="90" class="multiline_textbox textbox" nocontent="Description" runat="server"></textarea>
                        </div>
                    </div>
                </fieldset>
                
                <ul class="button_action-list action-list">
                    <li><input type="button" value="Update" class="js_button-update save-button button" /></li>
                    <li><input type="button" value="Cancel" class="js_button-cancel cancel-button button" /></li>
                </ul>
            </div>
        </div>
    <% } %>
    <% if (AllowEditing && School != null) { %>
        <script type="text/javascript">
            Event.observe(window, 'load', function() {            
            LinkMeUI.Editor.RegisterEditableControl(                         
                    {classNameContainingControl: 'record', 
                    classNameEditable : 'editing_record-view', 
                    classNameDisplay: 'displaying_record-view', 
                    classNameNoContents: 'empty_record-view', 
                    classNameError : 'resume_error', 
                    classNameEditLink : 'js_edit', 
                    classNameCancelLink : 'js_cancel', 
                    classNameUpdateLink : 'js_update', 
                    classNameButtonUpdate : 'js_button-update',
                    classNameButtonCancel : 'js_button-cancel', 
                    classNameRecordsCollection : 'js_records_collection',
                    classNameRecordsNoContent : 'records_no_content',
                    classNameRecordId : 'record_id',
                    disableOtherControls :  false, 
                    control : '<%= divEducationRecord.ClientID  %>', 
                    userUpdateFunction : 'UpdateEducationRecord',
                    userDeleteFunction : 'DeleteEducationRecord',
                    classNameDeleteRecordLink : 'js_delete-record',
                    currentSectionControls : educationControls,
                    userPostUpdateFunction : 'DisplayErrors',
                    userPostDeleteFunction : 'DisplayErrors' });
            });        
        </script>

    <% } %>
    
</div>
