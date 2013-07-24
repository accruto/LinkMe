<%@ Import namespace="LinkMe.Web.UI.Controls.Networkers"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VisibilityAdvancedSettings.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.VisibilityAdvancedSettings" %>
<%@ Register TagPrefix="uc" TagName="VisibilityCheckBoxRow" Src="~/ui/controls/networkers/VisibilityCheckBoxRow.ascx" %>

<style type="text/css">
.checkbox-matrix-container
{
	margin-top: 15px;
}
    
.checkbox-matrix 
{ 
	text-align: center; 
}

.checkbox-matrix th 
{ 
    font-weight: bold; 
}

.checkbox-matrix td, .checkbox-matrix th 
{
	width: 100px;
}

div.text-heading-2
{
	position: absolute;
}

td.table-row-name 
{ 
    text-align: right; 
    width: 240px;
}
</style>

<p class="checkbox-matrix-container">
    <div class="text-heading-2">Personal details</div>
    <table class="checkbox-matrix">
        <tr>
            <th></th>
            <th valign="bottom">Nobody</th>
            <th valign="bottom">Friends</th>
            <th valign="bottom">Friends' Friends</th>
            <th valign="bottom">Public</th>
        </tr>
        <uc:VisibilityCheckBoxRow ID="rowName" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowPhoto" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowGender" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowAge" runat="server" />
    </table>
</p>
<p class="checkbox-matrix-container">
    <div class="text-heading-2">Job hunting details</div>
    <table class="checkbox-matrix">
        <tr>
            <th></th>
            <th valign="bottom">Nobody</th>
            <th valign="bottom">Friends</th>
            <th valign="bottom">Friends' Friends</th>
            <th valign="bottom">All members</th>
        </tr>
        <uc:VisibilityCheckBoxRow ID="rowCandidateStatus" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowDesiredJob" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowResume" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowCurrentJob" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowPreviousJob" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowEducation" runat="server" />
    </table>
</p>
<p class="checkbox-matrix-container">
    <div class="text-heading-2">Contact details</div>
    <table class="checkbox-matrix">
        <tr>
            <th></th>
            <th valign="bottom">Nobody</th>
            <th valign="bottom">Friends</th>
            <th valign="bottom">Friends' Friends</th>
            <th valign="bottom">All members</th>
        </tr>
        <uc:VisibilityCheckBoxRow ID="rowPhone" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowEmail" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowSuburb" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowCountrySubdivision" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowCountry" runat="server" />
    </table>
</p>
<p class="checkbox-matrix-container">
    <div class="text-heading-2">Communication settings</div>
    <table class="checkbox-matrix">
        <tr>
            <th></th>
            <th valign="bottom">Nobody</th>
            <th valign="bottom">Friends</th>
            <th valign="bottom">Friends' Friends</th>
            <th valign="bottom">All members</th>
        </tr>
        <uc:VisibilityCheckBoxRow ID="rowSendInvites" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowSendMessages" runat="server" />
    </table>
</p>
<p class="checkbox-matrix-container">
    <div class="text-heading-2">Friends list</div>
    <table class="checkbox-matrix">
        <tr>
            <th></th>
            <th valign="bottom">Nobody</th>
            <th valign="bottom">Friends</th>
            <th valign="bottom">Friends' Friends</th>
            <th valign="bottom">All members</th>
        </tr>
        <uc:VisibilityCheckBoxRow ID="rowFriendsList" runat="server" />
    </table>
</p>
<p class="checkbox-matrix-container">
    <div class="text-heading-2">Interests &amp; affiliations</div>
    <table class="checkbox-matrix">
        <tr>
            <th></th>
            <th valign="bottom">Nobody</th>
            <th valign="bottom">Friends</th>
            <th valign="bottom">Friends' Friends</th>
            <th valign="bottom">All members</th>
        </tr>
        <uc:VisibilityCheckBoxRow ID="rowInterests" runat="server" />
        <uc:VisibilityCheckBoxRow ID="rowAffiliations" runat="server" />
    </table>
</p>

<script type="text/javascript">

var VisibilityRadioButtonStates = [
<% for (int i = 0; i < AllVisibilities.Length; i++)
  {
    var visibilities = AllVisibilities[i]; %>
    // Setting = <%= i %>
    [
        <% foreach (VisibilityCheckBoxRow row in Rows) { %>
        // <%= row.DisplayName %>
        [
            <%= ((visibilities[0] & row.ContactAccessFlag) == 0).ToString().ToLower() %>, // Nobody
            <%= ((visibilities[0] & row.ContactAccessFlag) == row.ContactAccessFlag).ToString().ToLower() %>, // FirstDegree
            <%= ((visibilities[1] & row.ContactAccessFlag) == row.ContactAccessFlag).ToString().ToLower() %>, // SecondDegree
            <%= ((visibilities[2] & row.ContactAccessFlag) == row.ContactAccessFlag).ToString().ToLower() %> // Public
        ]<%= row == Rows[Rows.Count-1] ? "" : "," %>
        <% } %>
    ]<%= visibilities == AllVisibilities[AllVisibilities.Length-1] ? "" : "," %>
<% } %>
]

function SetCheckBoxColumnEnabled(columnName, enabled)
{
    var rows = $$('.js_checkbox-row');
    var nameSeen = false;
    
    for (var i = 0; i < rows.length; i++)
    {
        if (!nameSeen)
        {
            var rowName = rows[i].select('.table-row-name')[0].innerHTML;
            
            if (rowName == 'Name')
            {
                nameSeen = true;
                continue;
            }
        }
        
        if (rows[i].select('.' + columnName)[0] != null)
        {
            rows[i].select('.' + columnName)[0].firstChild.disabled = !enabled;
            if(!enabled)
                rows[i].select('.' + columnName)[0].firstChild.checked = false;
        }
    }
}

SetCheckBoxColumnEnabled('js_public', $('<%= rowName.chkPublic.ClientID %>').checked);
SetCheckBoxColumnEnabled('js_second-degree', $('<%= rowName.chkSecondDegree.ClientID %>').checked);
</script>