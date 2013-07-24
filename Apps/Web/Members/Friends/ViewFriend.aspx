<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ViewFriend.aspx.cs" Inherits="LinkMe.Web.Members.Friends.ViewFriend" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Resumes"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Applications.Facade"%>
<%@ Import namespace="LinkMe.Framework.Utility"%>
<%@ Import namespace="LinkMe.Web.Service"%>
<%@ Register TagPrefix="uc" TagName="FriendsList" Src="~/ui/controls/networkers/MiniFriendsList.ascx" %>
<%@ Register TagPrefix="wc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadScrollTracker();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadOverlayPopup();
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="left-half">
        <div class="section">
            <div class="section-content">
                <div id="profile-photo-holder">
                    <div class="picture-container">
                        <asp:Image id="imgPhoto" runat="server" />
                    </div>
                    <div class="clearer"></div>
                    
                    <asp:PlaceHolder ID="phInvitation" runat="server">
                        <p>
                            <%= HtmlUtil.TextToHtml(ViewedMember.FirstName) %> has asked to be your friend.
                        </p>
                        <ul class="action-list">
                            <li><asp:LinkButton id="lnkAcceptInvitation" runat="server" Text="Accept invitation" /></li>
                            <li><asp:LinkButton id="lnkIgnoreInvitation" runat="server" Text="Ignore invitation" /></li>
                        </ul>
                    </asp:PlaceHolder>

                    <ul class="action-list">
                        <asp:Placeholder id="phInvite" runat="server">
                            <li><a id="lnkInvite" href="javascript: void(0);">Add to friends</a></li>
                            <script type="text/javascript">
                                $('lnkInvite').observe('click', function()
                                {
                                    populateOverlayPopup('<%= GetUrlForPage<InvitationPopupContents>() %>', '<%= InvitationPopupContents.InviteeIdParameter %>=<%= ViewedMember.Id %>');
                                });
                            </script>
                        </asp:Placeholder>
                        <asp:Placeholder id="phRemoveFriend" runat="server">
                            <li><a id="lnkRemoveFriend" href="javascript: void(0);">Remove friend</a></li>
                            <% // Need a button, but can't mark it visible="false", or it won't appear on the client.
                               // So we'll just put it into an invisible span. %>
                            <span style="display:none">
                                <asp:Button id="btnRemove" runat="server" />
                            </span>
                            <script type="text/javascript">
                                $('lnkRemoveFriend').observe('click', function()
                                {
                                    if (confirm('Do you really want to remove <%= Server.HtmlEncode(ViewedMember.FirstName) %> as a friend on LinkMe?'))
                                    {
                                        $('<%= btnRemove.ClientID %>').click();
                                    }
                                    return false;
                                });
                            </script>
                        </asp:Placeholder>
                    </ul>
                    
                    <asp:PlaceHolder ID="phRepresentatives" runat="server">
                        <p>
                            <%= GetRepresentativeText() %>.
                        </p>
                        <ul class="action-list">
                            <li><a id="lnkRemoveRepresentative" href="javascript: void(0);"><%= GetRepresentativeRemoveText() %></a></li>
                            <% // Need a button, but can't mark it visible="false", or it won't appear on the client.
                               // So we'll just put it into an invisible span. %>
                            <span style="display:none">
                                <asp:Button id="btnRemoveRepresentative" runat="server" />
                            </span>
                            <script type="text/javascript">
                                $('lnkRemoveRepresentative').observe('click', function()
                                {
                                    if (confirm('<%= GetRepresentativeConfirmText() %>'))
                                    {
                                        $('<%= btnRemoveRepresentative.ClientID %>').click();
                                    }
                                    return false;
                                });
                            </script>
                        </ul>
                    </asp:PlaceHolder>
                    
                </div>
            </div>
        </div>
        
        <div class="section">
            <div class="section-content">
            
                <asp:PlaceHolder ID="mainContactDetails" runat="server">
                    <div style="float:left;">
                        <div class="text-heading"><%= HtmlUtil.TextToHtml(ViewedMember.FirstName) %></div>
                        <div> 
                            <table>
                                <tr>
                                    <td><label class="small-form-label">First name</label></td>
                                    <td><%= HtmlUtil.TextToHtml(ViewedMember.FirstName)%></td>
                                </tr>
                                <tr>
                                    <td><label class="small-form-label">Last name</label></td>
                                    <td><%= HtmlUtil.TextToHtml(ViewedMember.LastName) %></td>
                                </tr>
                                <asp:PlaceHolder id="phLocation" runat="server">
                                <tr>
                                    <td><label class="small-form-label">Location</label></td>
                                    <td><asp:Literal id="ltlLocation" runat="server" /></td>
                                </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phCountry" runat="server">
                                <tr>
                                    <td><label class="small-form-label">Country</label></td>
                                    <td><%= ViewedMember.Address.Location.CountrySubdivision.Country.Name %></td>
                                </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phGender" runat="server">
                                <tr>
                                    <td><label class="small-form-label">Gender</label></td>
                                    <td><%= ViewedMember.Gender %></td>
                                </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phAge" runat="server">
                                    <tr>
                                        <td><label class="small-form-label">Age</label></td>
                                        <td><%= GetAgeText()%></td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </div>
                    </div>
                </asp:PlaceHolder>
                <div class="clearer"></div>
            </div>
        </div>
        
        <asp:PlaceHolder id="phFriends" runat="server">
            <div class="section">
                <div class="section-title">
                    <h1><%= HtmlUtil.TextToHtml(ViewedMember.FirstName.MakeNamePossessive()) %> friends</h1>
                </div>
                <div class="section-content">
                    <uc:FriendsList id="ucMiniFriendsList" runat="server" />
                    <ul class="horizontal_action-list action-list">
                        <li><a href="<%= GetViewFriendsUrl() %>">See all</a></li>
                    </ul>
                </div>
            </div>
        </asp:PlaceHolder>
        
        <asp:PlaceHolder id="phContactDetails" runat="server">
            <div class="section-title">
                <h1>Contact details</h1>
            </div>
            <div class="section-content" style="overflow:hidden;">        
                <p />
                <table>
                    <asp:PlaceHolder id="phPhone" runat="server">
<%  foreach (var phoneNumber in ViewedMember.PhoneNumbers)
    { %>                    
                        <tr>
<%      if (phoneNumber.Type == PhoneNumberType.Home)
        { %>                        
                            <td width="33%"><label class="small-form-label">Home phone</label></td>
<%      }
        else if (phoneNumber.Type == PhoneNumberType.Work)
        { %>                            
                            <td width="33%"><label class="small-form-label">Work phone</label></td>
<%      }
        else
        { %>
                            <td width="33%"><label class="small-form-label">Mobile phone</label></td>
<%      } %>
                            <td><%= HtmlUtil.TextToHtml(phoneNumber.Number)%></td>
                        </tr>
<%  } %>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder id="phEmailAddress" runat="server">
                        <tr>
                            <td><label class="small-form-label">Email address</label></td>
                            <td><%= FilthyHackToDisplayEmailAddress() %></td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </div>                
        </asp:PlaceHolder>

    </div>
	<div class="right-half">
	
        <asp:PlaceHolder id="phWorkStatusSection" runat="server">
            <div class="section">
                <div class="section-title">
                    <h1>Work Status</h1>
                </div>
                
                <div class="section-content">
                    <table>
                        <asp:PlaceHolder id="phWorkStatus" runat="server">
                            <tr>
                                <td class="valign-hack"><label class="small-form-label">Status</label></td>
                                <td><%= NetworkerFacade.GetCandidateStatusText(ViewedCandidate.Status)%></td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder id="phDesiredJob" runat="server">
                            <tr>
                                <td class="valign-hack"><label class="small-form-label">Desired job</label></td>
                                <td><%= HtmlUtil.TextToHtml(ViewedCandidate.DesiredJobTitle)%></td>
                            </tr>
                        </asp:PlaceHolder>
                    </table>
                </div>
            </div>
        </asp:PlaceHolder>
        
	    <div class="section">
	        <div class="section-title">
		        <h1>Career information</h1>
	        </div>
	        <div class="section-content">

		        <ul class="action-list">
			        <li><a class="affordance-link" id="ahrefViewResume" runat="server" target="_blank">View resume</a></li>
		        </ul>

                <asp:PlaceHolder id="phOccupation" runat="server">
                    <div class="section">
                        <div class="section-title"><h2>Occupation</h2></div>
                        <div class="section-content">
                            <table>
                                <asp:PlaceHolder id="phCurrentTitles" runat="server">
                                    <tr>
                                        <td class="valign-hack"><label class="small-form-label">
                                            Current job
                                        </label></td>
                                        <td><%= ViewedResume == null ? null : ViewedResume.GetCurrentJobsDisplayHtml()%></td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </div>
                    </div>
                </asp:PlaceHolder>
            
                <asp:PlaceHolder id="phEducation" runat="server">
                    <div class="section">
			            <div class="section-title"><h2>Education</h2></div>
                        <div class="section-content">
                            <table>
                                <asp:PlaceHolder id="phInstitution" runat="server">
                                    <tr>
                                        <td class="valign-hack"><label class="small-form-label">Institution</label></td>
                                        <td><%= HtmlUtil.TextToHtml(Institution)%></td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phDegree" runat="server">
                                    <tr>
                                        <td class="valign-hack"><label class="small-form-label">Degree</label></td>
                                        <td><%= HtmlUtil.TextToHtml(Degree)%></td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phDegreeCompletedDate" runat="server">
                                    <tr>
                                        <td class="valign-hack" width="45%"><label class="small-form-label">Completion date</label></td>
                                        <td><%= CompletedDate %></td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </div>
                    </div>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder id="phInterestsAffiliations" runat="server">
                    <div class="section">
			            <div class="section-title"><h2>Interests & affiliations</h2></div>
                        <div class="section-content">
                            <table>
                                <asp:PlaceHolder id="phInterests" runat="server">
                                    <tr>
                                        <td class="valign-hack"><label class="small-form-label">Interests</label></td>
                                        <td><wc:ExpandingLabel id="wcInterests" runat="server"></wc:ExpandingLabel></td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phAffiliations" runat="server">
                                    <tr>
                                        <td class="valign-hack"><label class="small-form-label">Affiliations</label></td>
                                        <td><%= HtmlUtil.TextToHtml(ViewedResume.Affiliations)%></td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </div>
                    </div>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder id="phWorkHistory" runat="server">
                    <div class="section">
			            <div class="section-title"><h2>Work history</h2></div>
                        <div class="section-content">
                            <table>
                                <asp:PlaceHolder id="phPreviousEmployer" runat="server">
                                    <tr>
                                        <td class="valign-hack" width="45%"><label class="small-form-label">Previous employer</label></td>
                                        <td><%= HtmlUtil.TextToHtml(PreviousEmployer) %></td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder id="phPreviousJob" runat="server">
                                    <tr>
                                        <td class="valign-hack"><label class="small-form-label">Previous job</label></td>
                                        <td><%= HtmlUtil.TextToHtml(PreviousTitle)%></td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </div>
                    </div>
                </asp:PlaceHolder>

            </div>            
        </div>
        
    </div>
</asp:Content>



