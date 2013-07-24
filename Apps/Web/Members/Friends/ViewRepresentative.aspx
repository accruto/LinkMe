<%@ Page Language="C#" MasterPageFile="~/master/TwoColumnMasterPage.master" CodeBehind="ViewRepresentative.aspx.cs" Inherits="LinkMe.Web.Members.Friends.ViewRepresentative" %>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members"%>
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

<asp:Content ContentPlaceHolderID="TopContent" runat="server">

    <div class="page-title">
        <h1>My nominated representative</h1>
    </div>
    
    <div class="section">
        <div class="section-content">
            <p>
                Your nominated representative will receive all employer enquiries on your behalf.
            </p>
        </div>
    </div>

    <asp:PlaceHolder id="phRepresentative" runat="server" Visible="false">
    
        <div class="section">
            <div class="section-content">
                <uc:ContactsList id="ucRepresentativeContactsList" runat="server" />
                
                <ul class="action-list">
                    <li>
                        <a id="lnkRemoveRepresentative" href="javascript: void(0);">Remove</a>
                        <%= HtmlUtil.TextToHtml(Representative.GetFirstNameDisplayText()) %> from acting as my representative
                    </li>
                    <span style="display:none">
                        <asp:Button id="btnRemoveRepresentative" runat="server" />
                    </span>
                    <script type="text/javascript">
                        $('lnkRemoveRepresentative').observe('click', function() {
                        if (confirm('Do you really want to stop <%= HtmlUtil.TextToHtml(Representative.GetFirstNameDisplayText()) %> from acting as your representative?"')) {
                                $('<%= btnRemoveRepresentative.ClientID %>').click();
                            }
                            return false;
                        });
                    </script>
                </ul>
            </div>
        </div>
        
    </asp:PlaceHolder>
    
    <div class="section">
        <div class="section-content">
            <ul class="action-list">
                <li><a href="<%= GetUrlForPage<ViewRepresentees>() %>">View</a> the people I am representing</li>
            </ul>
        </div>
    </div>
    
    <div class="section">
        <div class="section-title">
            <h1>Find and nominate a representative</h1>
        </div>
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="LeftContent" runat="server">

    <div class="section">

        <div class="section-content">
            <p>
                <strong>Step 1:</strong>
                Identify a person you would like to have receive all employer enquiries on your behalf.
                This should be a person you know and what has agreed to be your representative.
                They could be a Case Manager or trusted Recruitment Consultant.
            </p>
            <table>
                <tr>
                    <td>
                        <label class="small-form-label"><%= FieldNames.FIELD_SEARCH_NAME %></label>
                    </td>
                    <td>
                        <asp:TextBox id="txtName" runat="server" cssclass="find-friends-input" maxlength="60" />
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
                        <asp:Textbox id="txtEmailAddress" runat="server" CssClass="find-friends-input" MaxLength="320" />
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
        <div class="section-content">

            <p>
                <strong>Step 2:</strong>
                Once you have found the person in the listing below click on the link to invite them to be your nominated representative.
            </p>
            <p>
                <strong>Step 3:</strong>
                They then have 28 days to accept your invitation.
            </p>
            <p>
                <strong>Step 4:</strong>
                If they accept, all of your employer enquiries will be directed to your representative on your behalf.
            </p>
            <p>
                <strong>Note:</strong>
                Your nominated representative can be changed or removed by you at any time.
            </p>
            
        </div>
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="BottomContent" runat="server">
    <asp:PlaceHolder id="phResults" runat="server" visible="false">
        <div class="section">
            <div class="section-content">
                <asp:PlaceHolder id="phNoMatches" runat="server" visible="false">
                    <p>No matches were found for your search criteria.</p>
                </asp:PlaceHolder>
                
                <uc:ContactsList id="ucResultList" CanSendMessage="false" CanAccessFriends="false" CanAddToFriends="false" runat="server" />
                <uc:PagingBar id="ucPagingBar" runat="server" />
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>


