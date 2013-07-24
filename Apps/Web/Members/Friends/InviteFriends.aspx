<%@ Page language="c#" Codebehind="InviteFriends.aspx.cs" ValidateRequest="false" Inherits="LinkMe.Web.Members.Friends.InviteFriends" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="InviteFriends" Src="~/ui/controls/networkers/InviteFriends.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript" src="<%=ApplicationPath %>/js/ImportContacts.js" ></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="page-title">
        <h1>Invite my friends</h1>
    </div>

    <div class="section">
	    <div class="section-content">
            <div>
                <asp:PlaceHolder ID="phInviteFriends" Visible="false" runat="server">
                    <uc:InviteFriends id="ucInviteFriends" runat="server" />
                    <p>
                        <asp:Button cssclass="send-invitation-button" id="btnSendInvitations" runat="server" />&nbsp;
                        <asp:Button cssclass="cancel-button" id="btnCancelSendInvitations" causesvalidation="False" runat="server" />
                    </p>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>    	
</asp:Content>


