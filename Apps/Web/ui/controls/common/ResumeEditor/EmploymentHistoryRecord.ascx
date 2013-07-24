<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EmploymentHistoryRecord.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.EmploymentHistoryRecord" %>
<%@ Import namespace="LinkMe.Framework.Utility"%>

<% if (AllowEditing) { %>
    <script type="text/javascript">
        if(jobHistoryControls == null) {
            var jobHistoryControls = new Array();
        }    
        
        var hash = $H({jobIDId: '<%=lblJobId.ClientID %>', jobTitleId: '<%= txtJobTitle.ClientID %>', companyNameId: '<%= txtCompanyName.ClientID %>', startYearId: '<%= selectStartYear.ClientID %>', startMonthId: '<%= selectStartMonth.ClientID %>', endYearId: '<%= selectEndYear.ClientID %>', endMonthId: '<%= selectEndMonth.ClientID %>', isCurrentId: '<%= chkCurrentJob.ClientID %>', descriptionId: '<%= txtDescription.ClientID %>'});
        if(<%= (Job == null ? "true" : "false") %>) {
            var jobHistoryTemplate = hash;
        } else {
            jobHistoryControls.push(hash);
        }
    </script>
<% } %>

<div id="divEmploymentRecord" runat="server" class="employment_record record">
    <% if (AllowEditing) { %>
        <div class="empty_record-view record-view"></div>
        <span class="resume_error"></span>
    <% } %>

    <div id="divHistoryRecordDisplay" runat="server" class="displaying_record-view record-view">
        <div class="section-content forms_v2">
            <% if (AllowEditing) { %>
                <ul class="corner_inline_action-list inline_action-list action-list">
                    <li><a href="javascript:void(0);" class="delete-action js_delete-record">Delete</a></li>
                    <li><a href="javascript:void(0);" class="edit-action js_edit">Edit</a></li>
                </ul>
                <label id="lblJobId" class="record_id" runat="server"><%= JobId %></label>
                <fieldset>
                    <div class="timeframe_field read-only_field field">
                        <label>Timeframe of Employment</label>
                        <div class="control">
                            <span id="lblStartMonth" for="<%# selectStartMonth.ClientID %>" runat="server"><%# JobStartMonth %></span>
                            <span id="lblStartYear" runat="server"><%# JobStartYear %></span>
                            -
                            <span id="lblEndMonth" runat="server"><%# JobEndMonth %></span>
                            <span id="lblEndYear" runat="server"><%# JobEndYear %></span>
                            <span for="<%= chkCurrentJob.ClientID %>">
                                <%-- This must NOT be an <asp:Checkbox> because that generates another label with for="..." specified, which confuses SectionsEditor. --%>
                                <input id="chkCurrentJobDisplay" runat="server" type="checkbox" disabled="disabled" style="display:none;" />
                                <span style="<%# !JobIsCurrent ? "display:none;" : ""%>">Current</span>
                            </span>
                        </div>
                    </div>
                    <div class="jobtitle_field textbox_field read-only_field field">
                        <label>Job Title</label>
                        <div class="textbox_control control">
                            <div id="lblJobTitle" class="textbox" runat="server"><%# HtmlUtil.TextToHtml(JobTitle) %></div>
                        </div>
                    </div>
                    <div class="companyname_field textbox_field read-only_field field">
                        <label>Company Name</label>
                        <div class="textbox_control control">
                            <div id="lblCompanyName" class="textbox" runat="server"><%# HtmlUtil.TextToHtml(JobEmployer) %></div>
                        </div>
                    </div>
                    <div class="description_field multiline_textbox_field textbox_field read-only_field field">
                        <label>Description</label>
                        <div class="multiline_textbox_control textbox_control control">
                            <div id="lblDescription" class="multiline_textbox textbox" runat="server"><%# HtmlUtil.TextToHtml(JobDescription) %></div>
                        </div>
                    </div>
                </fieldset>
            <% } else { %>
            
                <fieldset>
                    <div class="timeframe_field read-only_field field">
                        <label>Timeframe of Employment</label>
                        <div class="control">
                            <% if (JobStartYear != "") { %>
                                <span runat="server"><%# JobStartMonth %></span>
                                <span runat="server"><%# JobStartYear %></span>
                            <% } %>
                            <% if (JobIsCurrent || JobEndYear != "") { %>
                            -
                                <span runat="server"><%# JobEndMonth %></span>
                                <span runat="server"><%# JobEndYear %></span>
                                <span style="<%# !JobIsCurrent ? "display:none;" : ""%>">Current</span>
                            <% } %>
                        </div>
                    </div>
                    <% if (GetJobTitleHtml() != "") { %>
                        <div class="jobtitle_field textbox_field read-only_field field">
                            <label>Job Title</label>
                            <div class="textbox_control control">
                                <div class="textbox" runat="server"><%# GetJobTitleHtml() %></div>
                            </div>
                        </div>
                    <% } %>
                    <% if (GetCompanyNameHtml() != "") { %>
                        <div class="companyname_field textbox_field read-only_field field">
                            <label>Company Name</label>
                            <div class="textbox_control control">
                                <div class="textbox" runat="server"><%# GetCompanyNameHtml() %></div>
                            </div>
                        </div>
                   <% } %>
                    <% if (GetJobDescriptionHtml() != "") { %>
                        <div class="description_field multiline_textbox_field textbox_field read-only_field field">
                            <label>Description</label>
                            <div class="multiline_textbox_control textbox_control control">
                                <div class="multiline_textbox textbox" runat="server"><%# GetJobDescriptionHtml() %></div>
                            </div>
                        </div>
                   <% } %>
                </fieldset>
                
            <% } %>
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
                    <div class="startdate_field mmm-yyyy-current_field field">
                        <label>Start Date</label>
                        <div class="mmm-yyyy-current_control control">
                            <select id="selectStartMonth" class="js_validate-selection-month mmm_dropdown dropdown" runat="server">
                                <option value="">Month</option>
                                <option value="Jan">Jan</option>
                                <option value="Feb">Feb</option>
                                <option value="Mar">Mar</option>
                                <option value="Apr">Apr</option>
                                <option value="May">May</option>
                                <option value="Jun">Jun</option>
                                <option value="Jul">Jul</option>
                                <option value="Aug">Aug</option>
                                <option value="Sep">Sep</option>
                                <option value="Oct">Oct</option>
                                <option value="Nov">Nov</option>
                                <option value="Dec">Dec</option>
                            </select>
                            <asp:DropDownList id="selectStartYear" CssClass="js_validate-selection-year yyyy_dropdown dropdown" runat="server" />
                            <div class="error" style="clear:left;"></div>
                        </div>
                    </div>
                    <div class="enddate_field mmm-yyyy-current_field field">
                        <label>End Date</label>
                        <div class="mmm-yyyy-current_control control">
                            <select id="selectEndMonth" runat="server" class="mmm_dropdown dropdown">
                                <option value="">Month</option>
                                <option value="Jan">Jan</option>
                                <option value="Feb">Feb</option>
                                <option value="Mar">Mar</option>
                                <option value="Apr">Apr</option>
                                <option value="May">May</option>
                                <option value="Jun">Jun</option>
                                <option value="Jul">Jul</option>
                                <option value="Aug">Aug</option>
                                <option value="Sep">Sep</option>
                                <option value="Oct">Oct</option>
                                <option value="Nov">Nov</option>
                                <option value="Dec">Dec</option>
                            </select>
                            <asp:DropDownList id="selectEndYear" CssClass="yyyy_dropdown dropdown" runat="server" />
                            <asp:checkbox CssClass="checkbox" ID="chkCurrentJob" runat="server" Text="Current" />
                            <span class="error"></span>
                        </div>
                    </div>
                    <div class="jobtitle_field textbox_field field">
                        <label>Job title</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtJobTitle" class="textbox" nocontent="Job title" runat="server"/>
                        </div>
                    </div>
                    <div class="companyname_field textbox_field field">
                        <label>Company Name</label>
                        <div class="textbox_control control">
                            <input type="text" id="txtCompanyName" class="textbox" nocontent="Company name" runat="server"/>
                        </div>
                    </div>
                    <div class="description_field multiline_textbox_field textbox_field field">
                        <label>Description</label>
                        <div class="multiline_textbox_control textbox_control control">
                            <textarea id="txtDescription" class="multiline_textbox textbox" nocontent="List your responsibilities and achievements" runat="server"></textarea>
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
    <% if(AllowEditing && Job != null) {%>
        <script type="text/javascript">        
            Event.observe(window, 'load', function() {
            LinkMeUI.Editor.RegisterEditableControl(                         
                    {classNameContainingControl: 'record', 
                    classNameEditable : 'editing_record-view', 
                    classNameDisplay: 'displaying_record-view', 
                    classNameNoContents: 'empty_record-view', 
                    classNameError : 'resume_error', 
                    classNameEditLink : 'js_edit', 
                    classNameUpdateLink : 'js_update',
					classNameCancelLink : 'js_cancel',
                    classNameButtonUpdate : 'js_button-update',
                    classNameButtonCancel : 'js_button-cancel', 
                    classNameRecordsCollection : 'js_records_collection',
                    classNameRecordsNoContent : 'records_no_content',
                    classNameRecordId : 'record_id',
                    disableOtherControls :  false, 
                    control : '<%= divEmploymentRecord.ClientID  %>', 
                    userUpdateFunction : 'UpdateEmploymentHistoryRecord',
                    userDeleteFunction : 'DeleteEmploymentHistoryRecord',
                    classNameDeleteRecordLink : 'js_delete-record',
                    currentSectionControls : jobHistoryControls,
                    userPostUpdateFunction : 'DisplayErrors',
                    userPostDeleteFunction : 'DisplayErrors' });
             });
        </script>    
     <% } %>
</div>