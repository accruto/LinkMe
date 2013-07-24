<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="BrowseJobAdsControl.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.BrowseJobAdsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<div class="browse-job-ads_ascx">
    <% if (TwoColumnLayout) { %>
        <div style="float: left; width:50%;">
            <asp:Repeater id="rptCategories1" Runat="server">
	            <ItemTemplate>
		            <div>
		                <a href="<%# GetUrl(Container.DataItem) %>" title="<%# GetTitle(Container.DataItem) %>" ><%# GetDisplayName(Container.DataItem)%></a>
		            </div>
	            </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="float:left; width: 45%;">
            <asp:Repeater id="rptCategories2" Runat="server">
	            <ItemTemplate>
		            <div>
		                <a href="<%# GetUrl(Container.DataItem) %>" title="<%# GetTitle(Container.DataItem) %>" ><%# GetDisplayName(Container.DataItem)%></a>
		            </div>
	            </ItemTemplate>
            </asp:Repeater>
        </div>
    <% } else { %>
        <div class="columns">
            <div class="column">
                <asp:Repeater id="rptCategories3" Runat="server">
	                <ItemTemplate>
		                <div class="industry">
		                    <a href="<%# GetUrl(Container.DataItem) %>" title="<%# GetTitle(Container.DataItem) %>" ><%# GetDisplayName(Container.DataItem)%></a>
		                </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="column">
                <asp:Repeater id="rptCategories4" Runat="server">
	                <ItemTemplate>
		                <div class="industry">
		                    <a href="<%# GetUrl(Container.DataItem) %>" title="<%# GetTitle(Container.DataItem) %>" ><%# GetDisplayName(Container.DataItem)%></a>
		                </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    <% } %>
</div>