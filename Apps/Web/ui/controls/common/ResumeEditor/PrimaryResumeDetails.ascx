<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PrimaryResumeDetails.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ResumeEditor.PrimaryResumeDetails" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Validation"%>
<%@ Import namespace="LinkMe.Utility.Validation"%>
<%@ Import namespace="LinkMe.Web.Service"%>
<%@ Import namespace="LinkMe.Common"%>
<%@ Register TagPrefix="uc1" TagName="IndustryList" Src="~/ui/controls/common/IndustryList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Location" Src="~/ui/controls/common/Location.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationCountry" Src="~/ui/controls/common/LocationCountry.ascx" %>

<div id="divContactDetails" class="primary-resume-details_resume_section resume_section section">
	<% if (CanEdit) { %>
		<script type="text/javascript">
		    function UpdateDetailsCancel() {
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
	
			function GetPossessiveSuffix(name) {
				if (name.length == 0)
					return '';
	
				var c = name.charAt(name.length - 1);
				return (c == 's' || c == 'S' ? '\'' : '\'s');
			}

			function UpdateContactDetails(callback) {
				var name = $F('txtFirstName');
				var surname = $F('txtLastName');
				var mobilePhone = $F('txtMobilePhone');
				var workPhone = $F('txtWorkPhone');
				var homePhone = $F('txtHomePhone');
				var industryIDs = $F('<%= ucIndustryList.ListBoxClientID %>');

				var desiredJobTitle = $F('txtDesiredJobTitle');
				var desiredSalaryLower = $F('txtDesiredSalaryLower');
				var desiredSalaryUpper = $F('txtDesiredSalaryUpper');
				
                var countryID = $('<%= ucLocationCountry.DropDownClientID %>').value;
				var location = $F('<%= ucLocation.TextBoxClientID %>');

				var res = LinkMe.Web.UI.Controls.Networkers.AjaxResumeEditor.SavePrimaryDetails(
					name, surname, mobilePhone, workPhone, homePhone, industryIDs, countryID, location, callback);
			}

			function ValidateContactDetails() {
				$('spnPhoneNumberValidation').update('');
				var passed = true;
				if(!ValidatePhoneNumber('txtMobilePhone', 'spnPhoneNumberValidation', 'mobile')) {
					passed = false;	
				}
				if(!ValidatePhoneNumber('txtWorkPhone', 'spnPhoneNumberValidation', 'work')) {
					passed = false;
				}
				if(!ValidatePhoneNumber('txtHomePhone', 'spnPhoneNumberValidation', 'home')) {
					passed = false;
				}
				if(!ValidateName()) {
					passed = false;
				}
				return passed;
			}
	
			function ValidateName() {
			    var nameRegex = new RegExp(/<%= LinkMe.Domain.RegularExpressions.CompleteFirstNamePattern %>/);
	
				var passed = true;
				if(!nameRegex.test($F('txtFirstName'))) {
					UpdateValidationMessage('Please correct first name', 'divContactErrors')
					passed = false;
				}
	
				if(!nameRegex.test($F('txtLastName'))) {
					UpdateValidationMessage('Please correct last name', 'divContactErrors')
					passed = false;
				}
	
				return passed;
			}
	
			function UpdateValidationMessage(message, elem) {
				$(elem).show();
				$(elem).update($(elem).innerHTML + message + '<br/>');
				$(elem).scrollTo();
			}

			function ValidatePhoneNumber(phoneTextbox, validationSpan, phoneDescription) {	

				var text = $F(phoneTextbox);
				if (text.length == 0)
					return true;
	
				var phoneRegex = PHONE_REGEX;
	
				if (!phoneRegex.test(text)) {
					$(validationSpan).update($(validationSpan).innerHTML +	
					 'The ' + phoneDescription + ' phone number is not in a valid format.<br/>');
					return false;
				}

				return true;
			}

			function UpdatePrimaryDetailsUI() {
				//$('lblLastSuffix').update(GetPossessiveSuffix($F('txtLastName')));
				DisplayErrors();
			}
		</script>

		<div id="spanNoContactDetails" runat="server" class="empty_resume-section-view resume-section-view" style="display: none;" />					
		<div id="divContactErrors" class="resume_error error"></div>

		<div id="spanContactDetailsEditable" class="editing_resume-section-view resume-section-view forms_v2" style="display: none;">
		    <div class="section-content">
			    <ul class="corner_inline_action-list inline_action-list action-list">
				    <li><a href="javascript:void(0);" class="save-action js_update">Save</a></li>
				    <li><a href="javascript:void(0);" class="cancel-action js_cancel">Cancel</a></li>
			    </ul>
			    <fieldset>
<%  if (CanEditContactDetails)
    { %>			    
			        <div class="textbox_field compulsory_field field">
				        <label>First name</label>
				        <div class="textbox_control control">
				            <input name="txtFirstName" class="textbox" value="<%=GetFirstName()%>" maxlength="30" id="txtFirstName" size="20" type="text" />
				        </div>
			        </div>

			        <div class="textbox_field compulsory_field field">
				        <label>Last name</label>
				        <div class="textbox_control control">
				            <input name="txtLastName" class="textbox" value="<%=GetLastName()%>" maxlength="30" id="txtLastName" size="20" type="text" />
				        </div>
			        </div>
<%  }
    else
    { %>
			        <div class="textbox_field field">
				        <label>First name</label>
				        <div class="textbox_control control">
				            <input name="txtFirstName" class="textbox" value="<%=GetFirstName()%>" id="txtFirstName" type="text" readonly="readonly" disabled="disabled" />
				        </div>
			        </div>

			        <div class="textbox_field field">
				        <label>Last name</label>
				        <div class="textbox_control control">
				            <input name="txtLastName" class="textbox" value="<%=GetLastName()%>" id="txtLastName" type="text" readonly="readonly" disabled="disabled" />
				        </div>
			        </div>
<%  } %>
			        <div class="listbox_field field">
				        <label>Industries</label>
				        <div class="listbox_control control">
				            <uc1:IndustryList id="ucIndustryList" runat="server" Rows="5" />
				        </div>
			        </div>
        			
		            <div class="country_dropdown_field compulsory_field dropdown_field field">
				        <label>Country</label>
				        <div class="country_dropdown_control dropdown_control control">
				            <uc1:LocationCountry id="ucLocationCountry" runat="server" />
				        </div>
		            </div>
        	
		            <div class="location_autocomplete_textbox_field compulsory_field autocomplete_textbox_field textbox_field field">	
				        <label>Location</label>
				        <div class="location_autocomplete_textbox_control autocomplete_textbox_control textbox_control control">
				            <uc1:Location id="ucLocation" runat="server" Method="AddressLocation" />
				        </div>
                    </div>
        		    
			        <div class="phone_textbox_field textbox_field field">
				        <label>Mobile phone</label>
				        <div class="phone_textbox_control textbox_control control">
				            <input name="txtMobilePhone" class="phone_textbox textbox" value="<%= GetMobilePhone() %>" maxlength="<%= DomainConstants.PhoneNumberMaxLength %>" id="txtMobilePhone" size="12" type="text" />
				        </div>
			        </div>
    	
			        <div class="phone_textbox_field textbox_field field">
				        <label>Work phone</label>
				        <div class="phone_textbox_control textbox_control control">
    			            <input name="txtWorkPhone" class="phone_textbox textbox" value="<%= GetWorkPhone() %>" maxlength="<%= DomainConstants.PhoneNumberMaxLength %>" id="txtWorkPhone" size="12" type="text" />
				        </div>
				    </div>
    				
			        <div class="phone_textbox_field textbox_field field">
				        <label>Home phone</label>
				        <div class="phone_textbox_control textbox_control control">
				            <input name="txtHomePhone" class="phone_textbox textbox" value="<%= GetHomePhone() %>" maxlength="<%= DomainConstants.PhoneNumberMaxLength %>" id="txtHomePhone" size="12" type="text" />
				        </div>
			        </div>
			        <div class="validation" id="spnPhoneNumberValidation"></div>
			    </fieldset>

			    <ul class="button_action-list action-list">
				    <li><input type="button" value="Update" class="js_button-update update_button button" /></li>
				    <li><input type="button" value="Cancel" class="js_button-cancel cancel-button button" /></li>
			    </ul>
			    <div class="compulsory-legend">required</div>
			</div>
		</div>
	<% } %>

	<div id="spanContactDetailsDisplay" runat="server" class="displaying_resume-section-view resume-section-view forms_v2">
	    <div class="section-content">
	        <% if (CanEdit) { %>

		        <ul class="corner_inline_action-list inline_action-list action-list">
			        <li><a href="javascript:void(0);" class="edit-action js_edit">Edit</a></li>
		        </ul>
			    
		        <%-- The name is only shown as part of this control when editing is allowed, otherwise it's up
		        to the parent page to show it - a little ugly, but necessary to support editing the name using AJAX.
		        --%>
		        <div class="name-title">    				
			        <span id="lblFirst" for="txtFirstName"><%= GetFirstName() %></span>
			        <span id="lblLastName" for="txtLastName"><%= GetLastName() %></span>
		        </div>
		        <br class="clearer" />
	        <% } %>

	        <fieldset>
		        <div class="listbox_field read-only_field field property-hide-if-empty" style="<%= HideIf(HaveNoIndustries()) %>">
			        <label>Industries</label>
			        <div class="listbox_control control">
				        <span id="lblIndustry" for="<%= ucIndustryList.ListBoxClientID %>"><%= GetIndustryDisplayNames() %></span>
			        </div>
		        </div>
    		    
                <div class="read-only_field field">	
                    <label>My current location</label>
                    <div class="control">
			            <label id="lblCountry" for="<%= ucLocationCountry.DropDownClientID %>"><%= GetCountry() %></label><br />
			            <label id="lblLocation" for="<%= ucLocation.TextBoxClientID %>"><%= GetLocation() %></label><br />
		            </div>
                </div>

		        <% if (DisplayPhone) { %>
		            <div class="read-only_field field">
		                <label>Contact details</label>
		                <div class="control">	
				            <div class="property-hide-if-empty"
					            style="<%= HideIf(string.IsNullOrEmpty(GetMobilePhone())) %>">
					            m: <span id="lblMobilePhone" for="txtMobilePhone"><%= GetMobilePhone() %></span>
				            </div>
				            <div class="property-hide-if-empty"
					            style="<%= HideIf(string.IsNullOrEmpty(GetWorkPhone())) %>">
					            w: <span id="lblWorkPhone" for="txtWorkPhone"><%= GetWorkPhone() %></span>
				            </div>
				            <div class="property-hide-if-empty"
					            style="<%= HideIf(string.IsNullOrEmpty(GetHomePhone())) %>">
					            h: <span id="lblHomePhone" for="txtHomePhone"><%= GetHomePhone() %></span>
				            </div>
       		            </div>
       		        </div>
		        <% } %>
            </fieldset>
        </div>
	</div>
	<% if (CanEdit) { %>

	<script language="javascript" type="text/javascript">
	    Event.observe(window, 'load', function() {
		    var editor = LinkMeUI.Editor.RegisterEditableControl(
				       {classNameContainingControl: 'resume_section',	
					    classNameEditable : 'editing_resume-section-view',	
					    classNameDisplay: 'displaying_resume-section-view',	
					    classNameNoContents: 'empty_resume-section-view',	
					    classNameError : 'resume_error',	
					    classNameEditLink : 'js_edit',
					    classNameCancelLink : 'js_cancel',
					    classNameUpdateLink : 'js_update',
					    classNameButtonUpdate : 'js_button-update',
					    classNameButtonCancel : 'js_button-cancel',	
					    disableOtherControls :  false,	
					    control : 'divContactDetails',	
					    userPreEditFunction : '',
					    userUpdateFunction : 'UpdateContactDetails',
					    userPostUpdateFunction : 'UpdatePrimaryDetailsUI',
					    userPostCancelUpdateFunction : 'UpdateDetailsCancel',
					    userPostDeleteFunction : 'DisplayErrors',
					    userValidateFunction: 'ValidateContactDetails' });

            <% if (StartEditingOnLoad) { %>
                LinkMeUI.Editor.LoaderGlobalCallback = editor.Edit.bindAsEventListener(editor);
            <% } %>
	    });
	</script>
	
	<% } %>
</div>
