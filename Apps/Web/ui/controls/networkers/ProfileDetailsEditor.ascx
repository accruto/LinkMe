<%@ Import namespace="LinkMe.Web.Service"%>
<%@ Import namespace="LinkMe.Web.Manager.Navigation"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProfileDetailsEditor.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.ProfileDetailsEditor" %>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Applications.Facade"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Validation"%>
<%@ Import namespace="LinkMe.Utility.Validation"%>
<%@ Import namespace="LinkMe.Common"%>
<%@ Import namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Register TagPrefix="cc" Namespace="LinkMe.Apps.Asp.Context" Assembly="LinkMe.Apps.Asp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Register TagPrefix="uc" TagName="Location" Src="~/ui/controls/common/Location.ascx" %>
<%@ Register TagPrefix="uc" TagName="LocationConfirmation" Src="~/ui/controls/common/LocationConfirmation.ascx" %>
<%@ Register TagPrefix="uc" TagName="LocationCountry" Src="~/ui/controls/common/LocationCountry.ascx" %>
<%-- 
<style type="text/css">
.info_area {
    color: #628C46;
    margin-left: 50px;
    font-weight: bold;
    background: url(../../img/tick-completed.png) no-repeat;
    padding-left: 15px;    
}
</style>--%>
<div id="divProfileDetails" class="js_aboutme_control">
    <div id="displayProfileDetails" class="js_aboutme_display"> 
        <div style="float:left;">
            <div class="text-heading" id="nameText"><%= GetFullNameText() %></div>
            <ul class="action-list">
                <li><a href="javascript:void(0);" class="js_edit-link">Edit</a></li><%-- <span class="info_area" style="display: none;"></span>--%>
            </ul>
            <p>
                <table>
                    <tr>
                        <td><label class="small-form-label">Country</label></td>
                        <td><span id="countryText"><%= Member.Address.Location.CountrySubdivision.Country.Name %></span></td>
                    </tr>
                    <tr>
                        <td><label class="small-form-label">Location</label></td>
                        <td><span id="locationText"><%= HtmlUtil.TextToHtml(Member.Address.Location.ToString()) %></span></td>
                    </tr>
                    <tr>
                        <td><label class="small-form-label">Gender</label></td>
                        <td><span id="genderText" for="<%=rdoMale.ClientID %>"><%= Member.Gender %></span></td>
                    </tr>
                    <tr>
                        <td><label class="small-form-label">Age</label></td>
                        <td><span id="ageText"><%= GetAgeText() %></span></td>
                    </tr>
                    <tr>
                        <td><label class="small-form-label">Indigenous background</label></td>
                        <td><span id="ethnicStatus"><%= GetEthnicStatusText() %></span></td>
                    </tr>
                    
                </table>
                
            </p>
            
            <cc:ContextualContent id="ccContextualProfileDetails" runat="server">
                <cc:ContextualPlaceHolder ID="ContextualPlaceHolder1" Vertical="Aime" runat="server" Visible="false">
                    <% var status = GetAimeStatusText();
                       if (!string.IsNullOrEmpty(status))
                       { %>
                            <p><%= status %></p>
                        <% } %>
                </cc:ContextualPlaceHolder>
            </cc:ContextualContent>

        </div>
        <div class="clearer"></div>
    </div>
    <div id="editProfileDetails" style="display:none;" class="js_aboutme_editable">
        <div class="error-message js_aboutme_error" style="display:none;"></div>
        <table width="100%">
<%  if (CanEditContactDetails)
    { %>        
            <tr>
                <td align="right" style="width:40%;"><label class="compulsory-small-form-label">First name</label></td>
                <td>
                    <input id="txtFirstName" class="about-me-profile-input" value="<%=Member.FirstName%>" maxlength="30" type="text" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right"><label class="compulsory-small-form-label">Surname</label></td>
                <td>
                    <input id="txtSurname" class="about-me-profile-input" value="<%=Member.LastName%>" maxlength="30" type="text" />
                </td>
            </tr>
<%  }
    else
    { %>            
            <tr>
                <td align="right" style="width:40%;"><label class="small-form-label">First name</label></td>
                <td>
                    <input id="txtFirstName" class="about-me-profile-input" value="<%=Member.FirstName%>" maxlength="30" type="text" readonly="readonly" disabled="disabled" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right"><label class="small-form-label">Surname</label></td>
                <td>
                    <input id="txtSurname" class="about-me-profile-input" value="<%=Member.LastName%>" maxlength="30" type="text" readonly="readonly" disabled="disabled" />
                </td>
            </tr>
<%  } %>
            <tr>
                <td valign="middle" align="right"><label class="compulsory-small-form-label">Country</label></td>
                <td>
                    <uc:LocationCountry id="ucLocationCountry" cssclass="about-me-profile-input" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right"><label class="compulsory-small-form-label">Suburb / State / Postcode</label></td>
                <td>
                    <uc:Location id="ucLocation" cssclass="about-me-profile-input" runat="server" Method="AddressLocation" />
                    <uc:LocationConfirmation ID="ucLocationConfirmation" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right">
                    <label class="small-form-label">Gender</label>
                </td>
                <td id="radio-list-gender-container">
                    <input id="rdoMale" value="Male" name="gender" runat="server" type="radio" />Male<br />
                    <input id="rdoFemale" value="Female" name="gender" runat="server" type="radio" />Female
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right"><label class="small-form-label">D.O.B</label></td>
                <td>
                    <select id="ddlDay" class="about-me-day-input" runat="server">
                        <option value="">Day</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        <option value="13">13</option>
                        <option value="14">14</option>
                        <option value="15">15</option>
                        <option value="16">16</option>
                        <option value="17">17</option>
                        <option value="18">18</option>
                        <option value="19">19</option>
                        <option value="20">20</option>
                        <option value="21">21</option>
                        <option value="22">22</option>
                        <option value="23">23</option>
                        <option value="24">24</option>
                        <option value="25">25</option>
                        <option value="26">26</option>
                        <option value="27">27</option>
                        <option value="28">28</option>
                        <option value="29">29</option>
                        <option value="30">30</option>
                        <option value="31">31</option>
                    </select>
                    <select id="ddlMonth" class="about-me-month-input" runat="server">
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
                    <input id="txtYear" class="about-me-year-input" maxlength="4" type="text" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <div class="example">LinkMe will never disclose your gender, age or birth date to employers</div>
                </td>
            </tr>
            <tr>
	            <td valign="middle" align="right"><label class="small-form-label">Indigenous background</label></td>
	            <td>
	                <div class="job-type-checkboxes">
		                <asp:CheckBoxList id="chkListEthnicStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="flow" />
	                </div>
	            </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="example">LinkMe works with many employers wishing to employ Indigenous people</div>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input class="update-button" type="button" />
                    <input class="cancel-button" type="button" />
                </td>
            </tr>
        </table>
        <br /><br />
    </div>
    <script type="text/javascript">
        function ValidateProfileDetails() {
            var errorMsg = '';
            var nameRegex = new RegExp(/<%= LinkMe.Domain.RegularExpressions.CompleteFirstNamePattern %>/);

            if(!nameRegex.test($F('txtFirstName'))) 
                errorMsg = 'Invalid first name.<br/>';
            
            if(!nameRegex.test($F('txtSurname'))) 
                errorMsg += 'Invalid last name.<br/>';

            if($F('<%= txtYear.ClientID %>') != null && $F('<%= txtYear.ClientID %>').length != 0) {
                if($F('<%= ddlMonth.ClientID %>') == 'Month') {
                    errorMsg += 'Please select a month.<br/>';
                }
                if($F('<%= ddlDay.ClientID %>') == 'Day') {
                    errorMsg += 'Please select a day.';
                }
            }
            if(errorMsg.length != 0) {
                ctrlProfileDetails.ShowError(errorMsg);
            }

            return errorMsg.length == 0;                
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

        var ProfileDetails_months = { Jan: 0, Feb: 1, Mar: 2, Apr: 3, May: 4, Jun: 5, Jul: 6, Aug: 7, Sep: 8, Oct: 9, Nov: 10, Dec: 11 };
        
        function UpdateProfileDetailsText() {
            $("nameText").update($("txtFirstName").value + " " + $("txtSurname").value);

            var genderText = '<%=Gender.Unspecified.ToString() %>';
            $$('input[type="radio"][name="' + $('<%=rdoMale.ClientID%>').name + '"]').each(function(elem){
                if(elem.checked) {
                    genderText = elem.value;
                }
            });
            $("genderText").update(genderText);
            
            var country = $('<%=ucLocationCountry.DropDownClientID%>');
            var countryText = country.options[country.selectedIndex].text;
            $("countryText").update(countryText);
            
            var locationText = $('<%=ucLocation.TextBoxClientID%>').value;
            $("locationText").update(locationText);

            ShowPhotoUpload();
        }
        
        function HidePhotoUpload() {
            $('divUploadPhoto').hide();
        }
        
        function ShowPhotoUpload() {
            $('divUploadPhoto').show();
        }

        function UpdateProfileDetails(callback) {
            
            var gender = '<%=Gender.Unspecified.ToString() %>';
            $$('input[type="radio"][name="' + $('<%=rdoMale.ClientID%>').name + '"]').each(function(elem){
                if(elem.checked) {
                    gender = elem.value;
                }
            });

            var country = $('<%=ucLocationCountry.DropDownClientID%>');
            var ethnicStatus = GetCheckedIntValues($('<%= chkListEthnicStatus.ClientID %>'));
            
            return LinkMe.Web.UI.Controls.Networkers.AjaxAboutMeEditor.SaveProfileDetails(
                $('txtFirstName').value,
                $('txtSurname').value,
                gender,
                $('<%= ddlDay.ClientID %>').selectedIndex,
                $('<%= ddlMonth.ClientID %>').selectedIndex,
                $('<%= txtYear.ClientID %>').value,
                country.options[country.selectedIndex].text,
                $('<%= ucLocation.TextBoxClientID %>').value,
                ethnicStatus,
                'ageText', 'isDoing', 'ethnicStatus',
                callback);
        }
        
        function BackupNames()
        {
            ProfileDetails_FirstName = $F('txtFirstName');
            ProfileDetails_LastName = $F('txtSurname');
        }
        
        function RestoreNames()
        {
            $('txtFirstName').value = ProfileDetails_FirstName;
            $('txtSurname').value = ProfileDetails_LastName;
        }
        
        function ProfileDetailsPreEdit()
        {
            HidePhotoUpload();
            BackupNames();
        }
        
        function ProfileDetailsPostCancel()
        {
            ShowPhotoUpload();  
            RestoreNames();
        }
        
        var ProfileDetails_FirstName;
        var ProfileDetails_LastName;
        
        Event.observe(window, 'load', function() {        
            window.ctrlProfileDetails = LinkMeUI.Editor.RegisterEditableControl(                         
            {classNameContainingControl: 'js_aboutme_control', 
            classNameEditable : 'js_aboutme_editable', 
            classNameDisplay: 'js_aboutme_display', 
            classNameError : 'js_aboutme_error', 
            classNameInfo : 'info_area',
            classNameEditLink : 'js_edit-link', 
            classNameButtonUpdate : 'update-button',
            classNameButtonCancel : 'cancel-button', 
            disableOtherControls :  true, 
            control : 'divProfileDetails', 
            userUpdateFunction : 'UpdateProfileDetails',
            userPostUpdateFunction : 'UpdateProfileDetailsText',
            userPreEditFunction : 'ProfileDetailsPreEdit',
            userPostCancelUpdateFunction : 'ProfileDetailsPostCancel',
            userValidateFunction : 'ValidateProfileDetails' });
        });
    </script>
</div>
