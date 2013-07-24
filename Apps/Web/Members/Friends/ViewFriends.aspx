<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ViewFriends.aspx.cs" Inherits="LinkMe.Web.Members.Friends.ViewFriends" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import Namespace="LinkMe.Web.Members.Friends"%>
<%@ Import namespace="LinkMe.Common"%>
<%@ Register TagPrefix="uc" TagName="ContactsList" Src="~/ui/controls/networkers/ContactsList.ascx" %>
<%@ Register TagPrefix="uc" TagName="PagingBar" Src="~/ui/controls/common/AlphabeticalPagingBar.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    
    <div class="page-title">
        <h1>My friends and colleagues</h1>
    </div>
    
    <div class="forms_v2">
    
        <div class="section">
            <div class="section-content">
        
                <asp:PlaceHolder id="phNoFriendsText" runat="server" Visible="false">
                    <div>Inviting friends and colleagues into your network helps your career by:</div>
                    <div class="tick-list">
                        <ul>
                            <li>Gaining the visibility and power within their connections who you probably don't know;</li>
                            <li>Seeing who knows someone who knows someone at a company you want to work for who you can speak to before applying for a job;</li>
                            <li>Seeing who knows someone who knows someone who you can talk to who is already doing a job you aspire to; and</li>
                            <li>Allowing you to help the careers and aspirations of the people you know and care about.</li>
                        </ul>
                    </div>
                    <div>
                        <h3>Harnessing the power of 'who you know' is the most powerful way to get your dream job.</h3>
                        <h3>Let us help you. Invite all your contacts into your network now.</h3>
                    </div>
                    <div>
                        <p></p>            
                        <p><a href="<%= GetUrlForPage<InviteFriends>() %>">Invite a friend to join</a></p>
                        <p><a href="<%= GetUrlForPage<FindFriends>() %>">Find people</a></p>
                    </div>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder ID="phFriends" runat="server" Visible="true">
                    <h4><%= DescribeFriendsCount() %></h4>
                    <uc:PagingBar id="ucPagingBarTop" runat="server" />
                    <asp:PlaceHolder ID="displayFriends" runat="server" Visible="true">
                        <uc:ContactsList id="contactsListControl" runat="server" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="displayNoFriends" runat="server" Visible="false">
                        <div style="padding:10px;margin-top:10px;margin-bottom:10px;background-color:#EEE;">
                            You currently have no friends starting with <%= ucPagingBarTop.NameStartsWith %>.
                        </div>
                    </asp:PlaceHolder>
                    <uc:PagingBar id="ucPagingBarBottom" runat="server" />
                </asp:PlaceHolder>

            </div>
        </div>

    </div>
    
</asp:Content>
