<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="ViewCreditsExpiry.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.ViewCreditsExpiry" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>
<%@ Import Namespace="LinkMe.Apps.Presentation"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Domain.Credits"%>

<asp:PlaceHolder id="phViewCredits" runat="server" visible="false">
    <small>
        <table id="view-credits">
            <tr>
                <td><label>Credits</label></td>
                <td align="center"><label>Expiry</label></td>
                <td align="center"><label>Quantity</label></td>
            </tr>
	        <asp:Repeater id="rptCredits" runat="server">
		        <ItemTemplate>
			        <tr>
				        <td>
				            <span title="<%# ((Tuple<Credit, Allocation>)Container.DataItem).Item1.Description %>">
					            <%# ((Tuple<Credit, Allocation>)Container.DataItem).Item1.ShortDescription %>
				            </span>
				        </td>
				        <td style="white-space: nowrap;" align="center"><%# GetExpiryText(((Tuple<Credit, Allocation>)Container.DataItem).Item2) %></td>
				        <td style="white-space: nowrap;" align="center"><%# GetQuantityText(((Tuple<Credit, Allocation>)Container.DataItem).Item2) %></td>
			        </tr>
		        </ItemTemplate>
	        </asp:Repeater>
        </table>
    </small>
    
    <asp:PlaceHolder ID="phViewCreditsLink" runat="server">
        <p style="text-align:center">
            <a href="<%= ProductsRoutes.Credits.GenerateUrl() %>">View all credit details</a>
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phVerified" runat="server">
        <p style="text-align:center">
            Call sales for any enquiry on
            <span class="bold-blue-text" style="font-size: 14px;"><%= Constants.PhoneNumbers.FreecallHtml %></span>
        </p>
    </asp:PlaceHolder>

</asp:PlaceHolder>
