<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="CandidateAccess.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Employers.CandidateAccess" %>
<%@ Import Namespace="LinkMe.Apps.Agents"%>
<%@ Import Namespace="LinkMe.Apps.Presentation"%>

<table>
	<tr>
		<td>
			<label>Contact details</label>
		</td>
		<td>
			<span class="bold-blue-text" style="font-size:11px;">Call to enable</span>
		</td>
	</tr>
	<tr>
		<td>
			<label>Job ad posting</label>
		</td>
		<td>
			<span class="bold-blue-text" style="font-size:11px;">Call to enable</span>
		</td>
	</tr>
</table>
<p align="center">
	To access candidate details call
	<span class="bold-blue-text" style="font-size:14px;"><%= Constants.PhoneNumbers.FreecallHtml %></span>
</p>
