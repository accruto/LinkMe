<%@ Page language="c#" Codebehind="PreviousApplications.aspx.cs" AutoEventWireup="False" EnableViewState="false" Inherits="LinkMe.Web.UI.Registered.Networkers.PreviousApplications" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Register TagPrefix="uc" TagName="PagingBar" Src="../../controls/common/PagingBar.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

	<div class="page-title">
	    <h1>Job applications</h1>
	</div>
	
	<div class="section">
	
	    <div class="section-content">

	        <asp:PlaceHolder id="phNoApplications" visible="false" runat="server">
		        <div>
		            <p>You currently have no job applications.</p>
		        </div>
	        </asp:PlaceHolder>

            <asp:PlaceHolder id="phApplications" visible="false" runat="server">
	            <TABLE id="job-ad-table" cellspacing="0" cellpadding="1">
		            <tr class="section-title">
			            <th>Job details</th>
			            <th>Applied on</th>
			            <th>Status</th>
			            <th>Actions</th>
		            </tr>

		            <asp:repeater id="rptApplications" runat="server" OnItemCreated="rptApplications_ItemCreated" OnItemCommand="rptApplications_ItemCommand">
			            <ItemTemplate>
				            <tr>
					            <td>
							            <a href="<%# GetJobAdUrl(Container.DataItem) %>">
								            <asp:Literal ID="jobAdTitleLiteral" Runat="server"></asp:Literal>
							            </a>
							            &nbsp;<%# GetJobLocality(Container.DataItem) %><br />
							            <%# GetContactDetailsText(Container.DataItem) %><br />
							            <asp:Literal ID="postedDateLiteral" runat="server" />
					            </td>
					            <td>
						            <div><%# ((Application)Container.DataItem).CreatedTime.ToShortDateString() %></div>
					            </td>
					            <td>
						            <%# GetStatusText((Application)Container.DataItem) %>
					            </td>
					            <td>
					                <ul class="plain_action-list action-list">
					                    <li><asp:linkbutton id="lbDeleteApplication" cssclass="delete-action" CommandName="deleteCmd"
					                        runat="server" CommandArgument="<%# ((Application)Container.DataItem).Id %>"
						                    Visible="<%# EnableDelete((Application)Container.DataItem) %>">Delete</asp:linkbutton>
						                </li>
						            </ul>
					            </td>
				            </tr>
			            </ItemTemplate>
		            </asp:repeater>
	            </TABLE>
        		
	            <uc:PagingBar id="ucPagingBar" runat="server" />
	        </asp:PlaceHolder>

        </div>
    </div>

</asp:Content>
