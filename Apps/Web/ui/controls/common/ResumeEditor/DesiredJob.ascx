<%@ Import namespace="LinkMe.Web.UI.Controls.Networkers"%>
<%@ Import namespace="LinkMe.Utility.Validation"%>
<%@ Import namespace="LinkMe.Common"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DesiredJob.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.DesiredJob" %>
<%@ Register TagPrefix="uc1" TagName="JobTypesCheckBoxList" Src="~/ui/controls/common/JobTypesCheckBoxList.ascx" %>
<%@ Register TagPrefix="uc" TagName="WillingnessToRelocate" Src="~/ui/controls/networkers/WillingnessToRelocate.ascx" %>

<div id="divDesiredJob" class="desiredjob_ascx desired-job_resume_section resume_section section">
	<% if (AllowEditing) {%>
		<script type="text/javascript">
		    function ValidateDesiredJobDetails() {
		        var jsWTR = <%= ucWTR.JSObjectName() %>;
		        return jsWTR.Validate();		        
		    }
		    
			function GetCheckedIntValues(checkBoxListSpan) {
				var value = 0;
	
				var spans = checkBoxListSpan.getElementsByTagName('span');
				for (var i = 0; i < spans.length; i++) {
					var childSpan = spans[i];
					if (childSpan.firstChild.checked) {
						// Get the "intValue" attribute from the span - this needs to be manually added to the checkbox.
						var itemValue = childSpan.attributes.getNamedItem('intValue').value;
						value |= itemValue;
					}
				}	
				return value;
			}
	
 		    function UpdateDesiredJobDetails(callback) {
				var desiredJobTitle = $F('txtDesiredJobTitle');
				var desiredSalaryLower = $F('txtDesiredSalaryLower');
				var desiredSalaryUpper = $F('txtDesiredSalaryUpper');
				var desiredJobTypes = GetCheckedIntValues($('<%= ucDesiredJobTypes.ListBoxClientID %>'));
				var emailSuggestedJobs = $('<%= chkEmailSuggestedJobs.ClientID %>').checked;
				
				var jsWTR = <%= ucWTR.JSObjectName() %>;
				var relPref = jsWTR.GetRelocationPreference();
				var selectedLocalities = jsWTR.GetSelectedLocalities();				

				var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.SaveDesiredJobDetails(
				    desiredJobTitle, desiredSalaryLower, desiredSalaryUpper, 
					'lblDesiredSalary', desiredJobTypes, 
					'lblDesiredJobTypes', emailSuggestedJobs, 
					'lblSelectedLocalities', 'lblRelocationChoice', 
					relPref, selectedLocalities,
					callback);
			}
		</script>
    	<div id="spanNoDesiredJob" runat="server" class="empty_resume-section-view resume-section-view" style="display: none;">
		    <ul class="action-list">
		        <li><a href="javascript:void(0);" class="add-action">Add desired job</a></li> <%--// This DIV seems to be dead code --%>
		    </ul>
		</div>		
	    <div id="divDesiredJobErrors" class="resume_error error"></div>
    	<div id="spanDesiredJobEdit" class="editing_resume-section-view resume-section-view" style="display: none;">
			<div class="section-title">
			    <h2>Desired job</h2>
		        <ul class="corner_inline_action-list inline_action-list action-list">
		            <li><a href="javascript:void(0);" class="save-action js_update">Save</a></li>
		            <li><a href="javascript:void(0);" class="cancel-action js_cancel">Cancel</a></li>
		        </ul>
			</div>
			<div class="section-content forms_v2">
		        <fieldset>
		            <div class="jobtitle_field autocomplete_textbox_field textbox_field field">
				        <label>Desired job title</label>
				        <div class="autocomplete_textbox_control textbox_control control">
				            <input id="txtDesiredJobTitle" class="autocomplete_textbox textbox" maxlength="<%=DomainConstants.JobTitleMaxLength%>" size="30" type="text" />
				            <span id="indicator2" style="display: none"><img src="<%=ApplicationPath%>/ui/images/universal/loading.gif" alt="Working..." /></span>
				            <div id="autocomplete_choices_desired_job" class="auto-complete"></div>

				            <script language="javascript" type="text/javascript">
					            new Ajax.Autocompleter("txtDesiredJobTitle", "autocomplete_choices_desired_job", "<%= ApplicationPath %>/service/GetProfessionalTitles.ashx", {method: 'get', paramName: 'txtProfessionalTitle', parameters: 'limitBy=8', minChars: 2, indicator: 'indicator2'} );
				            </script>
				            <span id="txtDesiredJobTitleValidation" class="validation"></span>
                            <div class="helptext">
                                <p>This helps employers match you to jobs you want. Separate multiple titles with commas.</p>
                            </div>
				        </div>
			        </div>

			        <div class="expected_salaryrange_field salaryrange_field field">
				        <label>Expected salary ($)</label>
				        <div class="expected_salaryrange_control salaryrange_control control">
				            <input id="txtDesiredSalaryLower" class="textbox" value="<%=GetDesiredSalaryLower()%>" maxlength="10" size="10" type="text" />
				            to
				            <input id="txtDesiredSalaryUpper" class="textbox" value="<%=GetDesiredSalaryUpper()%>" maxlength="10" size="10" type="text" />
				            <span id="txtDesiredSalaryValidation" class="error"></span>
				        </div>
				        <div class="helptext"><strong>Strongly recommended.</strong> Please be realistic to provide the best job matches.</div>
			        </div>

			        <div class="horizontal_checkboxlist_field checkboxlist_field field">
				        <label>Desired job type</label>
				        <div class="horizontal_checkboxlist_control checkboxlist_control control">
				            <uc1:JobTypesCheckBoxList id="ucDesiredJobTypes" runat="server" />
				        </div>
		            </div>
        		    
                    <uc:WillingnessToRelocate id="ucWTR" runat="server"></uc:WillingnessToRelocate>
			        
		            <div class="checkbox_field field">
		                <div class="checkbox_control control">
				            <asp:CheckBox CssClass="checkbox" ID="chkEmailSuggestedJobs" runat="server" Text="Email me jobs like this" />
				        </div>
			        </div>			        
			    </fieldset>
			    
		        <ul class="button_action-list action-list">
			        <li><input type="button" value="Update" class="js_button-update update_button button" /></li>
			        <li><input type="button" value="Cancel" class="js_button-cancel cancel-button button" /></li>
		        </ul>
		        
			    <div class="compulsory-legend">required</div>
			</div>		    
		</div>
		
	<%}%>	
	
    <div id="spanDesiredJobDisplay" runat="server" class="displaying_resume-section-view resume-section-view">
		<% if (AllowEditing) { %>
			<div class="section-title">
			    <h2>Desired job</h2>
			    <ul class="corner_inline_action-list inline_action-list action-list">
			        <li><a href="javascript:void(0);" class="edit-action js_edit">Edit</a></li>
			    </ul>
			</div>
			
		<% } %>   
	    <div class="section-content forms_v2">
	        <fieldset>
	            <div class="jobtitle_field autocomplete_textbox_field textbox_field read-only_field field property-hide-if-empty" style="<%= HideIf(string.IsNullOrEmpty(GetDesiredJobTitle())) %>">
		            <label>Desired job title</label>
		            <div class="autocomplete_textbox_control textbox_control control">
			            <div id="lblDesiredJobTitle" class="autocomplete_textbox textbox" for="txtDesiredJobTitle"><%= GetDesiredJobTitle() %></div>
		            </div>
	            </div>

                <div class="read-only_field field">
	                <label>Desired job type</label>
	                <div class="control">
		                <div id="lblDesiredJobTypes"><%= GetDesiredJobTypesText() %></div>
	                </div>
	            </div>

	            <div class="read-only_field field property-hide-if-empty" style="<%= HideIf(string.IsNullOrEmpty(GetDesiredSalaryText())) %>">
		            <label>Expected salary</label>
		            <div class="control">
			            <div id="lblDesiredSalary"><%= GetDesiredSalaryText() %></div>
		            </div>
	            </div>
    	        
                <div class="read-only_field field property-hide-if-empty">
		            <label><span id="lblRelocationChoice"><%=GetRelocationPreference() %></span></label>
		            <div class="control">
			            <div id="lblSelectedLocalities"><%= GetSelectedLocalities() %></div>
		            </div>
	            </div>
	        </fieldset>
        </div>
    </div>
     
	<% if (AllowEditing) { %>

	<script language="javascript" type="text/javascript">
	    function SaveRelocationSelectionStep() {
	        var jsWTR = <%= ucWTR.JSObjectName() %>;
	        jsWTR.SaveValue();
	    }
	        
	    function RestoreRelocationSelectionStep() {
	        var jsWTR = <%= ucWTR.JSObjectName() %>;
	        jsWTR.RestoreValue();
	    }
	    
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
					    control : 'divDesiredJob',	
					    userUpdateFunction : 'UpdateDesiredJobDetails',
					    userPreEditFunction : 'SaveRelocationSelectionStep',
					    userPostCancelUpdateFunction : 'RestoreRelocationSelectionStep',
					    userValidateFunction: 'ValidateDesiredJobDetails'});
            window.DesiredJobSection = editor;

            <% if (StartEditingOnLoad) { %>
                LinkMeUI.Editor.LoaderGlobalCallback = editor.Edit.bindAsEventListener(editor);
            <% } %>
	    });
	</script>
	
	<% } %>
</div>

<link href="<%=ApplicationPath %>/ui/css/desired-job.css" rel="stylesheet" />