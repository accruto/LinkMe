<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DiaryEntries.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.DiaryEntries" %>
<%@ Register TagPrefix="uc" TagName="DiaryEntryRecord" Src="DiaryEntryRecord.ascx" %>

<% if (AllowEditing) { %>

    <script type="text/javascript">       
        function DeleteDiaryEntry(entryID, deleteCallBack) {
            var res = LinkMe.Web.UI.Controls.Networkers.AjaxDiaryEditor.DeleteDiaryEntry(entryID, deleteCallBack);        
        }

        function UpdateDiaryEntry(saveSectionCallback, obj) {
            var controls = obj.ControlsMap;
            
            var entryID = $(controls.get('entryId')).innerHTML;
            var title = $F(controls.get('titleId'));
            var startYear = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('startYearId')));
            var startMonth = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('startMonthId')));
            var endYear = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('endYearId')));
            var endMonth = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('endMonthId')));
            var description = $F(controls.get('descriptionId'));
            var totalHours = $F(controls.get('totalHoursId'));
            var isCurrent = $(controls.get('isCurrentId')).checked;

            var res = LinkMe.Web.UI.Controls.Networkers.AjaxDiaryEditor.SaveDiaryEntry(entryID, title, startYear, startMonth, endYear, endMonth, isCurrent, totalHours, description, saveSectionCallback);
        }
        
        function HandleCurrentClick(chkBox) {
            var selects = $(chkBox.parentNode.parentNode).getElementsByTagName('select');
            for(var i in selects) {
                selects[i].selectedIndex = 0;
                selects[i].enabled = !chkBox.checked;
                selects[i].disabled = chkBox.checked;
            }
        }
    </script>

<% } %>

<% if (AllowEditing || HaveContent) { %>
    <div id="divDiaryEntries" class="employment-history_resume_section resume_section section">
        <div id="divEntriesDisplay">
            <% if (AllowEditing) { %>
                <div class="section-title">
                    <h2>Diary entries</h2>
                    <ul class="corner_inline_action-list inline_action-list action-list">
                        <li><a href="javascript:void(0);" class="add-action js_add-section">Add entry</a></li>
                    </ul>
                </div>
                <div class="other_resume_control">
                    <div class="empty_resume-section-view resume-section-view" style="display: <%= (HaveContent ? "none" : "inherit") %>;" class="js_add-section">
                        <div class="section-content">
                            <ul>
                                <li>Use your CPD Diary to record and retrieve details and hours spent in professional development. </li>
                                <li>
                                    This information does not form part of your resume and may be used to demonstrate compliance
                                    with minimum requirements for professional accreditation and development.
                                </li>
                            </ul>
                        </div>
                     </div>
                </div>

                <div class="record_template">
                    <uc:DiaryEntryRecord id="templateDiaryEntry" runat="server" AllowEditing="true" />
                </div>
            <% } else { %>
                <div class="section-title">
                    <h2>Diary entries</h2>
                </div>
            <% } %>

            <div class="js_records_collection">
                <asp:Repeater ID="rptEntries" runat="server">
                    <ItemTemplate>
                        <uc:DiaryEntryRecord id="diaryEntry" runat="server" Entry=<%# Container.DataItem %> AllowEditing=<%# AllowEditing %> />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <% if (AllowEditing) { %>
            <ul class="horizontal_action-list action-list">
                <li><a href="javascript:void(0);" class="add-action js_add-section">Add entry</a></li>
            </ul>
        <% } %>
    </div>

<% } %>

<% if (AllowEditing) { %>
    <script type="text/javascript">
        var sectEmplHandler = LinkMeUI.Editor.RegisterAddControlCall({
                            control: 'divDiaryEntries', 
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
                            classNameUpdateLink : 'js_update',
                            classNameCancelLink : 'js_cancel',
                            classNameButtonUpdate : 'js_button-update',
                            classNameButtonCancel : 'js_button-cancel', 
                            disableOtherControls : false,
                            userUpdateFunction : 'UpdateDiaryEntry', 
                            templateMap : diaryEntryTemplate, 
                            currentSectionControls : diaryEntryControls, 
                            userDeleteFunction : 'DeleteDiaryEntry',
                            userPostUpdateFunction : 'DisplayErrors',
                            userPostAddFunction : 'DisplayErrors',
                            addControlsToEnd : false });
         
        /*$('ahrefAddEmployment').observe('click', sectEmplHandler.Handler.bindAsEventListener(sectEmplHandler));*/

        <% if (StartEditingOnLoad) { %>
            LinkMeUI.Editor.LoaderGlobalCallback = sectEmplHandler.Handler.bindAsEventListener(sectEmplHandler);
        <% } %>
         
         Validation.add('js_validate-selection-year', 'Select year', {
             isNot : 'Year'
         });
        
        Validation.add('js_validate-selection-month', 'Select month', {
             isNot : 'Month'
         });     
    </script>    
<% } %>