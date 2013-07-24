<%@ Import namespace="LinkMe.Framework.Utility"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PhoneEmailEditor.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.PhoneEmailEditor" %>

<div id="divProfileContactDetails" class="js_aboutme_control section">
    <div id="editContactDetails" style="display: none;" class="js_aboutme_editable">
        <div class="section-title">
            <h1>Contact Details</h1>
            <ul class="corner_inline_action-list inline_action-list action-list">
                <li><a class="js_update-link save-action" href="javascript:void(0);">Save</a></li>
                <li><a class="js_cancel-link cancel-action" href="javascript:void(0);">Cancel</a></li>
            </ul>
        </div>
        
        <div id="errorInContactDetails" class="error-message js_aboutme_error" style="display:none;"></div>
        
        <table>
            <tr>
                <td valign="middle" align="right">
                    <label class="small-form-label">Home phone</label>
                </td>
                <td>
                    <input id="txtHomePhone" class="about-me-input" value="<%= HomePhoneNumber %>" maxlength="20" type="text" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right">
                    <label class="small-form-label">Work phone</label>
                </td>
                <td>
                    <input id="txtWorkPhone" class="about-me-input" value="<%= WorkPhoneNumber %>" maxlength="20" type="text" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right">
                    <label class="small-form-label">Mobile phone</label>
                </td>
                <td>
                    <input id="txtMobilePhone" class="about-me-input" value="<%= MobilePhoneNumber %>" maxlength="20" type="text" />
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right">
                    <label class="compulsory-small-form-label">Email address</label>
                </td>
                <td>
                    <input id="txtEmail" class="about-me-input" value="<%= EmailAddress %>" maxlength="100" type="text" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input class="save-button" type="button" />
                    <input class="cancel-button" type="button" />
                </td>
            </tr>
        </table>

    </div>

    <div id="displayContactDetails" class="js_aboutme_display">
        <div class="section-title">
            <h1>Contact Details</h1>
            <ul class="corner_inline_action-list inline_action-list action-list">
                <li><a class="js_edit-link edit-action" href="javascript:void(0);">Edit</a></li>
            </ul>
        </div>
        
        <p>
            <table>
                <tr>
                    <td class="valign-hack">
                        <label class="small-form-label">Home phone</label>
                    </td>
                    <td>
                        <span id="homePhoneText"><%= HtmlUtil.TextToHtml(HomePhoneNumber) %></span>
                    </td>
                </tr>
                <tr>
                    <td class="valign-hack">
                        <label class="small-form-label">Work phone</label>
                    </td>
                    <td>
                        <span id="workPhoneText"><%= HtmlUtil.TextToHtml(WorkPhoneNumber) %></span>
                    </td>
                </tr>
                <tr>
                    <td class="valign-hack">
                        <label class="small-form-label">Mobile phone</label>
                    </td>
                    <td>
                        <span id="mobilePhoneText"><%= HtmlUtil.TextToHtml(MobilePhoneNumber) %></span>
                    </td>
                </tr>
                <tr>
                    <td class="valign-hack">
                        <label class="small-form-label">Email address</label>
                    </td>
                    <td>
                        <span id="emailAddressText"><%= HtmlUtil.TextToHtml(EmailAddress) %></span>
                    </td>
                </tr>
            </table>
        </p>
    </div>
</div>
<script type="text/javascript">
    function UpdateContactDetails(callback) {
        var homePhoneControl = $('txtHomePhone');
        var homePhone = homePhoneControl.value;
        var workPhoneControl = $('txtWorkPhone');
        var workPhone = workPhoneControl.value;
        var mobilePhoneControl = $('txtMobilePhone');
        var mobilePhone = mobilePhoneControl.value;
        var emailControl = $('txtEmail');
        var emailAddress = emailControl.value;
        return LinkMe.Web.UI.Controls.Networkers.AjaxAboutMeEditor.SaveContactDetails(homePhone, workPhone, mobilePhone, emailAddress, callback);
    }
    
    function UpdatePhoneEmailDetailsText() {
    
        $("homePhoneText").update($('txtHomePhone').value);
        $("workPhoneText").update($('txtWorkPhone').value);
        $("mobilePhoneText").update($('txtMobilePhone').value);
        $("emailAddressText").update($('txtEmail').value);
    }

    Event.observe(window, 'load', function() {        
        LinkMeUI.Editor.RegisterEditableControl(                         
            {classNameContainingControl: 'js_aboutme_control', 
            classNameEditable : 'js_aboutme_editable', 
            classNameDisplay: 'js_aboutme_display', 
            classNameError : 'js_aboutme_error',
            classNameEditLink: 'js_edit-link',
            classNameCancelLink: 'js_cancel-link',
            classNameUpdateLink: 'js_update-link',
            classNameButtonUpdate : 'save-button',
            classNameButtonCancel : 'cancel-button', 
            disableOtherControls :  true, 
            control : 'divProfileContactDetails', 
            userUpdateFunction : 'UpdateContactDetails',
            userPostUpdateFunction : 'UpdatePhoneEmailDetailsText' });
    });
</script>