<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SendJobToFriend.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.OverlayPopups.SendJobToFriend" %>
<div>
    <div class="text-heading">Send job ad to a friend</div>
    <p>Forward job ad <%= JobAd.Title %> to a friend</p>
    
    <div id="divSendJobError" class="error-message" style="display:none;"></div>
    
    <table>
        <asp:PlaceHolder id="phShowSender" runat="server">
            <tr>
                <td align="right"><label class="compulsory">Your name</label></td>
                <td>
                    <input id="txtSenderName" type="text" value="<%= UsersName %>" maxlength="30" class="generic-form-input" tabindex="100" />
                </td>
            </tr>
            <tr>
                <td align="right"><label class="compulsory">Your email address</label></td>
                <td>
                    <input id="txtSenderEmail" type="text" value="<%= UsersEmail %>" maxlength="320" class="generic-form-input" tabindex="101" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phHidenSender" Visible="false" runat="server">
            <tr>
                <td>
                    <input type="hidden" id="txtSenderName" value="<%= UsersName %>" />
                    <input type="hidden" id="txtSenderEmail" value="<%= UsersEmail %>" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td align="right"><label class="compulsory">Your friend's name</label></td>
            <td>
                <input id="txtRecipientName" type="text" maxlength="30" class="generic-form-input" tabindex="102" />
            </td>
        </tr>
        <tr>
            <td align="right"><label class="compulsory">Your friend's email address</label></td>
            <td>
                <input id="txtRecipientEmail" type="text" maxlength="320" class="generic-form-input" tabindex="103" />
            </td>
        </tr>
        <tr>
            <td align="right"><label>Message</label></td>
            <td>
                <textarea id="txtMessage" class="small-text-area" tabindex="104"></textarea>
            </td>
        </tr>
    </table>
    <div id="overlay-popup-buttons">
        <input id="btnSend" type="button" class="send-button" onclick="btnSendEmail_Click();" tabindex="105" />
        <input id="btnCancel" type="button" class="cancel-button" onclick="banishOverlayPopup();" tabindex="106" />
    </div>
</div>