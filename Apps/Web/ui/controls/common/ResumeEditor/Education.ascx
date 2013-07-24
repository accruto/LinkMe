<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Education.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.Education" %>
<%@ Register TagPrefix="uc1" TagName="EducationRecord" Src="EducationRecord.ascx" %>

<% if (AllowEditing) { %>
    
    <script type="text/javascript">       
    
        function DeleteEducationRecord(educationID, deleteCallBack) {
            var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.DeleteEducation(educationID, deleteCallBack);        
        }
        
        function UpdateEducationRecord(saveSectionCallback, obj) {           
            controls = obj.ControlsMap;
            
            var educationID = $(controls.get('educationIDId')).innerHTML;
            var Institution = $F(controls.get('InstitutionId'));
            var City = $F(controls.get('CityId'));
            var Qualification = $F(controls.get('QualificationId'));
            var Major = $F(controls.get('MajorId'));
            var Completed = $F(controls.get('CompletedId'));
            var Description = $F(controls.get('DescriptionId'));
            
            var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.SaveEducation(educationID, Institution, City, Qualification, Major, Completed, Description, saveSectionCallback);
        }
    </script>
    
<% } %>

<% if (AllowEditing || HaveContent) { %>
    <div id="divEducationHistory" class="education_resume_section resume_section section">
        <% if (AllowEditing) { %>
            <div class="section-title">
                <h2>Education / Qualification</h2>
                <ul class="corner_inline_action-list inline_action-list action-list">
                    <li><a href="javascript:void(0);" class="add-action js_add-section">Add education</a></li>
                </ul>
            </div>
        <% } else { %>
            <div class="section-title">
                <h2>Education / Qualification</h2>
            </div>
        <% } %>
        <div class="js_records_collection">
             <div id="divEducationHistoryDisplay">
                <% if (AllowEditing) { %>
                    <div class="other_resume_control">
                        <div class="js_add-section empty_resume-section-view" style="display: <%= (HaveContent ? "none" : "inherit") %>;">
                            <div class="section-content">
                                List your degrees and qualifications.
                            </div>
                         </div>
                     </div>

                    <div class="record_template">
                        <uc1:EducationRecord id="templateEducationRecord" runat="server" AllowEditing="true" />
                    </div>
                <% } %>

                <asp:repeater ID="repEducationRecords" runat="server">
                    <ItemTemplate>
                        <uc1:EducationRecord id="educationRecord" runat="server" School=<%# Container.DataItem %>
                            AllowEditing=<%# AllowEditing %> Highlighter=<%# Highlighter %> />
                    </ItemTemplate>
                </asp:repeater>
            </div>
        </div> 
        <% if (AllowEditing) { %>
            <ul class="horizontal_action-list action-list">
                <li><a href="javascript:void(0);" class="add-action js_add-section">Add education</a></li>
            </ul>
        <% } %>
    </div>
    
<% } %>

<% if (AllowEditing) { %>
    
    <script type="text/javascript">
        var sectHandler = LinkMeUI.Editor.RegisterAddControlCall({
                            control: 'divEducationHistory', 
                            classNameAddSection : 'js_add-section', 
                            classNameRecordsCollection : 'js_records_collection', 
                            classNameRecordTemplate : 'record_template', 
                            classNameRecordsNoContent : 'empty_resume-section-view',
                            classNameRecordId : 'record_id',
                            classNameDeleteRecordLink : 'js_delete-record',
                            classNameContainingControl : 'record', 
                            classNameEditable : 'editing_record-view',
                            classNameDisplay : 'displaying_record-view',
                            classNameNoContents : 'empty_record-view',
                            classNameError : 'resume_error', 
                            classNameEditLink : 'js_edit', 
                            classNameCancelLink : 'js_cancel', 
                            classNameUpdateLink : 'js_update', 
                            classNameButtonUpdate : 'js_button-update',
                            classNameButtonCancel : 'js_button-cancel', 
                            disableOtherControls : false,
                            userUpdateFunction : 'UpdateEducationRecord', 
                            templateMap : educationTemplate, 
                            currentSectionControls : educationControls, 
                            userDeleteFunction : 'DeleteEducationRecord',
                            userPostUpdateFunction : 'DisplayErrors',
                            userPostAddFunction : 'DisplayErrors',
                            addControlsToEnd : false });
         
        /* $('ahrefAddSection').observe('click', sectHandler.Handler.bindAsEventListener(sectHandler)); */

        <% if (StartEditingOnLoad) { %>
            LinkMeUI.Editor.LoaderGlobalCallback = sectHandler.Handler.bindAsEventListener(sectHandler);
        <% } %>

        Validation.add('js_required-education-date', 
                        'Please enter the date in format MMM YYYY (for example, Jan 2008)', 
                        function(val, elem) {
                            if(!val || val.length == 0) 
                                return true;
                            var r = new RegExp("^(Jan|Feb|Mar|Apr|May|Jul|Jun|Aug|Sep|Oct|Nov|Dec)\\s((<%= GetYearRegexPart() %>))$","gi");
                            return r.test(val);
                        });

    </script>
    <div style="clear:both;"></div>
<% } %>
