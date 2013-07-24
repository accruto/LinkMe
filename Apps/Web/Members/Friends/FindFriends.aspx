<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" ValidateRequest="false" CodeBehind="FindFriends.aspx.cs" Inherits="LinkMe.Web.Members.Friends.FindFriends" MasterPageFile="~/master/TwoColumnMasterPage.master" %>
<%@ Import Namespace="LinkMe.Web.Members.Friends"%>
<%@ Import namespace="LinkMe.Web.Configuration"%>
<%@ Register TagPrefix="uc" TagName="ContactsList" Src="~/ui/controls/networkers/ContactsList.ascx" %>
<%@ Register TagPrefix="uc" TagName="PagingBar" Src="~/ui/controls/common/PagingBar.ascx" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadScriptaculous();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadScrollTracker();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadOverlayPopup();
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftContent" runat="server">
    <div class="section">
        <div class="section-title">
            <h1>Find people on LinkMe</h1>
        </div>

        <div class="section-content">
            <p></p>
            <table>
                <tr>
                    <td>
                        <label class="small-form-label"><%= FieldNames.FIELD_SEARCH_NAME %></label>
                    </td>
                    <td>
                        <asp:TextBox id="txtQuery" runat="server" cssclass="find-friends-input" maxlength="60" />
                    </td>
                    <td valign="top">
                    </td>
                </tr>
                <tr>
                    <td><span style="padding-left:14px">or</span></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label class="small-form-label"><%= FieldNames.FIELD_SEARCH_EMAIL %></label>
                    </td>
                    <td>
                        <asp:Textbox id="txtEmail" runat="server" CssClass="find-friends-input" MaxLength="320" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <cc:SubmitAsGetButton id="btnSearch" runat="server" cssclass="search-button" />
                    </td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="RightContent" runat="server">
    <div class="section">
        <div class="section-title">
            <h1>Invite people into my network</h1>
        </div>
        
        <div class="section-content">
            <ul class="action-list">
                <li><a href="<%= GetUrlForPage<InviteFriends>() %>">Invite a friend to join</a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="BottomContent" runat="server">
    <asp:PlaceHolder id="phResults" runat="server" visible="false">
        <div class="section">
            <div class="section-title">
                <h1>Your search results</h1>
            </div>
            
            <div class="section-content">
                <asp:PlaceHolder id="phNoMatches" runat="server" visible="false">
                    No matches were found for your search criteria.
                </asp:PlaceHolder>
                
                <uc:ContactsList id="ucResultList" runat="server" />
                <uc:PagingBar id="ucPagingBar" runat="server" />
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>


