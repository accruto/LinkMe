<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="LocationConfirmation.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.LocationConfirmation" %>

<script type="text/javascript">
	LinkMeUI.JSLoadHelper.LoadLocation();   
</script>
<script type="text/javascript">
function toggleConfirmationDisplay(visible)
{
    // Always hide for now, too many problems See Case 9375.
    
//    if(visible == 'true') {
//        $('<%= divConfirmation.ClientID %>').style['display'] = 'block';
//    } else {
        $('<%= divConfirmation.ClientID %>').style['display'] = 'none';
//    }
}
</script>

<div id="divConfirmation" class="locationconfirmation_ascx" style="display: none;" runat="server">
    <div class="speecharrow"></div>
    <asp:Label ID="lblMessage" runat="server">
        <span id="spanConfirm" runat="server">
            <div class="question">Is this the location you meant?</div>
            <ul class="button_action-list action-list">
                <li><asp:HyperLink ID="hlinkYes" Visible="true" CssClass="yes" runat="server">Yes</asp:HyperLink></li>
                <li><asp:HyperLink ID="hlinkNo" Visible="true" CssClass="no" runat="server">No</asp:HyperLink></li>
            </ul>
        </span>
    </asp:Label>
</div>