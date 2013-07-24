<%@ Import namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Service" %>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="InviteRepresentative.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.OverlayPopups.InviteRepresentative" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Unity"%>
<%@ Import Namespace="LinkMe.Utility.Configuration"%>

<div class="picture-container">
    <asp:Image id="imgPhoto" runat="server" />
</div>

<div style="padding-left:120px">
    <div class="text-heading"><%= string.Format(HeadingFormat, GetInviteeFirstName()) %></div>

    <p>
        A message will be sent to <%= GetInviteeFirstName() %> who can choose to accept your request.
        <%= GetInviteeFirstName() %> will then receive all enquiries via email
        and phone from your employers on your behalf.
        You can remove or change your representative at any time.
    </p>

    <p id="pCustomMessageBlurb">
        If you wish you can 
        <a href="javascript: void(0);" onclick="$('pCustomMessageBlurb').setStyle({display: 'none'});$('divCustomMessage').setStyle({display: 'block'});">add a 
        personal message</a> to this invitation.
    </p>
    <div id="divCustomMessage" style="display:none;">
        Your message 
        (<a href="javascript: void(0);" onclick="$('pCustomMessageBlurb').setStyle({display: 'block'});$('divCustomMessage').setStyle({display: 'none'});$('txtCustomMessage').value='';">remove</a>):<br />
        <textarea id="txtCustomMessage" cols="60" rows="6"></textarea>
    </div>

    <%-- LR: Not in phase 1 of networking
    <p><a href="javascript:PreviewInvitation();">View a preview of the invitation here</a></p>
    --%>

    <p>Note that <%= GetInviteeFirstName() %> will have <%= Container.Current.Resolve<int>("linkme.domain.roles.networking.tempFriendDays") %> days to respond to your invitation.
    During that time, <%= GetInviteeFirstName() %> will be able to see your profile unless the invitation is denied.</p>

    <hr />

    <div id="overlay-popup-buttons">
        <input type="button" value="" onclick="var msg=$F('txtCustomMessage');banishOverlayPopup();populateOverlayPopup('<%= GetUrlForPage<RepresentativePopupContents>() %>', '<%= RepresentativePopupContents.InviteeIdParameter %>=<%= Invitee.Id %>&<%= RepresentativePopupContents.SendInvitationParameter %>=true&<%= RepresentativePopupContents.MessageParameter %>=' + encodeURIComponent(msg));" 
        class="send-invitation-button" />
        <input type="button" value="" onclick="banishOverlayPopup();" class="cancel-button" />
    </div>
</div>

<script type="text/javascript">
<%= GetSendInvitationJavascript() %>

function ShowCustomMessage()
{
    $('pCustomMessageBlurb').setStyle({display: 'none'});
    $('divCustomMessage').setStyle({display: 'block'});
}

function HideCustomMessage()
{
    $('pCustomMessageBlurb').setStyle({display: 'block'});
    $('divCustomMessage').setStyle({display: 'none'});
    $('txtCustomMessage').value = '';
}
</script>
