<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ContactsListDetails.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.ContactsListDetails" %>

<asp:HyperLink ID="lnkPhoto" runat="server">
    <div class="picture-container">
        <asp:Image id="imgPhoto" runat="server" />
    </div>
</asp:HyperLink>
<div>
    <div class="friend-list-details">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="white-space: nowrap; vertical-align: top;">
                    <span class="text-heading"><asp:HyperLink ID="lnkFullName" runat="server" /></span>
                </td>
                <td class="is-doing" style="padding-top: 0.35em; padding-left: 0.5ex;">
                    <asp:PlaceHolder ID="phThisIsYou" runat="server" visible="false"> (This is you)</asp:PlaceHolder>
                    <asp:PlaceHolder id="phRepresentative" runat="server" Visible="false"> (<asp:Label ID="lblRepresentative" runat="server" />)</asp:PlaceHolder>
                    <asp:PlaceHolder ID="phExtraDescription" runat="server" />
                </td>
            </tr>
        </table>

        <table>
            <asp:PlaceHolder id="phCurrentJobs" runat="server">
                <tr>
                    <td width="100px" class="valign-hack">
                        <asp:Label ID="lblCurrentJobsCaption" CssClass="small-form-label" runat="server" />
                    </td>
                    <td>
                        <asp:Literal ID="ltlCurrentJobs" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder id="phCandidateStatus" runat="server">
                <tr>
                    <td width="100px">
                        <label class="small-form-label">Work status</label>
                    </td>
                    <td>
                        <asp:Literal ID="ltlCandidateStatus" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder id="phLocation" runat="server">
                <tr>
                    <td width="100px">
                        <label class="small-form-label">Location</label>
                    </td>
                    <td>
                        <asp:Literal ID="ltlLocation" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </div>
    <div class="friend-list-actions">
        <asp:HyperLink ID="lnkFullProfile" CssClass="affordance-link" runat="server">Full profile</asp:HyperLink>
        <br />
        <asp:PlaceHolder id="phViewFriends" runat="server">
            <asp:HyperLink ID="lnkViewFriends" CssClass="affordance-link" runat="server">View friends</asp:HyperLink>
            <br />
        </asp:PlaceHolder>
        <asp:PlaceHolder id="phAddToFriends" runat="server">
            <asp:HyperLink id="lnkAddToFriends" CssClass="affordance-link" runat="server">Add to Friends</asp:HyperLink>
            <br />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phExtraActions" runat="server" />
    </div>
    <div class="clearer"></div>
</div>
<div class="clearer"></div>
