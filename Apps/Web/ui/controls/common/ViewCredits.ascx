<%@ Control Language="c#" AutoEventWireup="False" Codebehind="ViewCredits.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ViewCredits" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Domain.Credits"%>

<asp:Literal id="litCaption" runat="server" />
<br/>
<asp:Repeater id="rptCredits" runat="server">
	<HeaderTemplate><ul></HeaderTemplate>
	<ItemTemplate>
		<li><%# GetItemText((Tuple<Credit, Allocation>)Container.DataItem) %></li>
	</ItemTemplate>
	<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>

<ul class="action-list">
    <li><a href="<%= ProductsRoutes.Credits.GenerateUrl() %>">View all credit details</a></li>
    <li><a href="<%= ProductsRoutes.NewOrder.GenerateUrl() %>">Purchase credits</a></li>
</ul>