<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="EmployerJobAdsControl.ascx.cs" Inherits="LinkMe.Web.UI.Registered.Employers.EmployerJobAdsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Register TagPrefix="pb" TagName="Pagingbar" Src="~/ui/controls/common/PagingBar.ascx" %>

<asp:PlaceHolder id="phNoJobAds" runat="server" visible="false">
    <div>
        You currently have no <%= GetCurrentMode().ToString().ToLower() %> job ads.
    </div>
</asp:PlaceHolder>

<asp:PlaceHolder id="phJobAds" runat="server" visible="false">
    <asp:repeater id="AdsRepeater" runat="server" OnItemCommand="AdsRepeater_ItemCommand">
        <itemtemplate>
            <div class="repeater-container">
                <div class="title-line">
			        <a class="title" href="<%# GetViewJobAdUrl((JobAd)Container.DataItem) %>">
			            <%# GetJobAdHeader((JobAd)Container.DataItem) %>
			        </a>
		        </div>
                <div class="repeater-details-container">
				    <table>
				        <tr>
				            <td>
				                <label class="small-form-label">Reference</label>
				            </td>
				            <td>
				                <%# GetExternalReference((JobAd)Container.DataItem) %>
				            </td>
				        </tr>
				        <tr>
				            <td>
				                <label class="small-form-label">Created</label>
				            </td>
				            <td>
				                <%# ((JobAd)Container.DataItem).CreatedTime.ToString(LinkMe.Common.Constants.DATE_FORMAT)%>
				            </td>
				        </tr>
    <% if (!ViewOnly)
       { %>
				        <tr>
				            <td>
				                <label class="small-form-label">Contact email</label>
				            </td>
				            <td>
				                <%# GetContactEmail((JobAd)Container.DataItem) %>
				            </td>
				        </tr>
				        <tr>
				            <td>
				                <label>Applicants</label>
				            </td>
				            <td>
					            <strong><%# GetShortlistedNumber((JobAd)Container.DataItem) %></strong>
					            <%# GetCandidatesLink(ApplicantStatus.Shortlisted, (JobAd)Container.DataItem) %>
					             - 
					            <strong><%# GetNewCandidatesNumber((JobAd)Container.DataItem) %></strong>
					            <%# GetCandidatesLink(ApplicantStatus.New, (JobAd)Container.DataItem) %>
					             - 
					            <strong><%# GetRejectedCandidatesNumber((JobAd)Container.DataItem) %></strong>
					            <%# GetCandidatesLink(ApplicantStatus.Rejected, (JobAd)Container.DataItem) %>
					        </td>
				        </tr>
    <% } %>
				        <tr>
				            <td>
				                <label class="small-form-label">Days left</label>
				            </td>
				            <td>
				                <%# GetDaysUntilExpiry(((JobAd)Container.DataItem)) %>
				            </td>
				        </tr>
			        </table>
                </div>
                <div class="repeater-actions-container">
                    <ul class="plain_action-list action-list">
                        <%# GetSuggestionsActionItem((JobAd)Container.DataItem) %>
    <% if (!ViewOnly)
       { %>
                        <%# GetApplicantsActionItem((JobAd)Container.DataItem) %>
                        <li><a class="edit-action" href="<%# GetEditJobAdUrl((JobAd)Container.DataItem)%>">Edit</a></li>
                        <li><asp:LinkButton CssClass="<%# GetExecutionCssClass() %>" runat="server" CommandName="<%# GetCommandName() %>" OnClientClick="<%# GetClientClickScript() %>" CommandArgument="<%# ((JobAd)Container.DataItem).Id %>" id="lbExecuteAd" text="<%# GetExecutionString((JobAd)Container.DataItem) %>" /></li>
    <% } %>
                    </ul>
                </div>
                <div class="clearer"></div>
            </div>
        </itemtemplate>
    </asp:repeater>
    <pb:PagingBar id="ucPagingBar" runat="server" />
</asp:PlaceHolder>