<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ViewFriendsFriends.aspx.cs" Inherits="LinkMe.Web.Members.Friends.ViewFriendsFriends" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import namespace="LinkMe.Web.Service"%>
<%@ Import namespace="LinkMe.Framework.Utility"%>
<%@ Register TagPrefix="uc" TagName="ContactsList" Src="~/ui/controls/networkers/ContactsList.ascx" %>
<%@ Register TagPrefix="uc" TagName="PagingBar" Src="~/ui/controls/common/PagingBar.ascx" %>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadScrollTracker();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadOverlayPopup();
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="page-title">
        <h1><%= HtmlUtil.TextToHtml(OwnerOfFriends.FullName.MakeNamePossessive())%> friends</h1>
    </div>
    
    <div class="forms_v2">
    
        <div class="section">
            <div class="section-content">
                <div style="float:left;">
                    <asp:Image id="imgPhoto" runat="server" />
                </div>
                
                <div class="friend-list-details">
                    <ul class="horizontal_action-list action-list">
	    			    <li><a href="<%= BuildViewProfileLink() %>">Full profile</a></li>
                        <asp:PlaceHolder id="phAddToFriends" runat="server">
                            <li>
                                <a href="javascript:populateOverlayPopup('<%= GetUrlForPage<InvitationPopupContents>() %>','<%= InvitationPopupContents.InviteeIdParameter %>=<%= OwnerOfFriends.Id %>');">Add to friends</a>
                            </li>
                        </asp:PlaceHolder>
                    </ul>
                </div>

                <div class="clearer"></div>
            </div> 
            <h4><%= DescribeFriendsFriendsCount() %></h4>
            <uc:Pagingbar id="ucPagingBarTop" runat="server" />
            <uc:ContactsList id="contactsListControl" runat="server" />
            <uc:Pagingbar id="ucPagingBarBottom" runat="server" />
            
        </div>
    </div>

</asp:Content>
