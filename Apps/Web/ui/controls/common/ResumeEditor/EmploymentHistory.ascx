<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EmploymentHistory.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.EmploymentHistory" %>
<%@ Register TagPrefix="uc1" TagName="EmploymentHistoryRecord" Src="EmploymentHistoryRecord.ascx" %>

<% if (AllowEditing) { %>

    <script type="text/javascript">       
        function DeleteEmploymentHistoryRecord(jobID, deleteCallBack) {
            var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.DeleteJobHistory(jobID, deleteCallBack);        
        }

        function UpdateEmploymentHistoryRecord(saveSectionCallback, obj) {
            var controls = obj.ControlsMap;
            
            var jobID = $(controls.get('jobIDId')).innerHTML;
            var jobTitle = $F(controls.get('jobTitleId'));
            var compName = $F(controls.get('companyNameId'));
            var startYear = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('startYearId')));
            var startMonth = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('startMonthId')));
            var endYear = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('endYearId')));
            var endMonth = LinkMeUI.LocateHelper.GetSelectedValue($(controls.get('endMonthId')));
            var descr = $F(controls.get('descriptionId'));
            var isCurrent = $(controls.get('isCurrentId')).checked;
            
            var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.SaveJobHistory(jobID, jobTitle, compName, startYear, startMonth, endYear, endMonth, isCurrent, descr, saveSectionCallback);
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
    <div id="divEmploymentHistory" class="employment-history_resume_section resume_section section">
        <div id="divHistoryDisplay">
            <% if (AllowEditing) { %>
                <div class="section-title">
                    <h2>Employment history</h2>
                    <ul class="corner_inline_action-list inline_action-list action-list">
                        <li><a href="javascript:void(0);" class="add-action js_add-section">Add job</a></li>
                    </ul>
                </div>
                <div class="other_resume_control">
                    <div class="empty_resume-section-view resume-section-view" style="display: <%= (HaveContent ? "none" : "inherit") %>;" class="js_add-section">
                        <div class="section-content">
                            <ul>
                                <li>List all of the roles that you have held - provide a full description for those in the last 15 years. </li>
                                <li>List your responsibilities and duties - focus on your key achievements and how your company benefited. </li>
                                <li>Highlight experience that is relevant to the job you are looking for. </li>
                                <li>Be concise and avoid jargon and abbreviations.</li>
                            </ul>
                        </div>
                     </div>
                </div>

                <div class="record_template">
                    <uc1:EmploymentHistoryRecord id="templateHistRecord" runat="server" AllowEditing="true" />
                </div>
            <% } else { %>
                <div class="section-title">
                    <h2>Employment history</h2>
                </div>
            <% } %>

            <div class="js_records_collection">
                <asp:Repeater ID="repRecords" runat="server">
                    <ItemTemplate>
                        <uc1:EmploymentHistoryRecord id="historyRecord" runat="server" Job=<%# Container.DataItem %>
                            AllowEditing=<%# AllowEditing %> Highlighter=<%# Highlighter %>
                            HideRecentEmployers=<%# HideRecentEmployers %> PreviousJob=<%# PreviousJob %> />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <% if (AllowEditing) { %>
            <ul class="horizontal_action-list action-list">
                <li><a href="javascript:void(0);" class="add-action js_add-section">Add job</a></li>
            </ul>
        <% } %>
    </div>

<% } %>

<% if (AllowEditing) { %>
    <script type="text/javascript">
        var sectEmplHandler = LinkMeUI.Editor.RegisterAddControlCall({
                            control: 'divEmploymentHistory', 
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
                            userUpdateFunction : 'UpdateEmploymentHistoryRecord', 
                            templateMap : jobHistoryTemplate, 
                            currentSectionControls : jobHistoryControls, 
                            userDeleteFunction : 'DeleteEmploymentHistoryRecord',
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