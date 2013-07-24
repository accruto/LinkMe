<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="WorkStatusSettings.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.WorkStatusSettings" %>

<p>
    <strong>Work status</strong> tells employers whether you're currently looking for a job.
</p>
<div style="padding-left: 20px;">
    <p>
        <ul class="input-list">
            Set my work status to:
            <li><asp:RadioButton ID="rdoAvailable" runat="server" GroupName="WorkStatus" /></li>
            <li><asp:RadioButton ID="rdoActive" runat="server" GroupName="WorkStatus" /></li>
            <li><asp:RadioButton ID="rdoOpen" runat="server" GroupName="WorkStatus" /></li>
            <li><asp:RadioButton ID="rdoNotLooking" runat="server" GroupName="WorkStatus" /></li>
        </ul>
    </p>
</div>

<script type="text/javascript">
function RestoreWorkStatusDefault()
{
    $('<%= rdoAvailable.ClientID %>').checked = false;
    $('<%= rdoActive.ClientID %>').checked = true;
    $('<%= rdoOpen.ClientID %>').checked = false;
    $('<%= rdoNotLooking.ClientID %>').checked = false;
}

function WorkStatusSettingsHaveChanged()
{
    var now = CurrentWorkStatusSettings();
    
    for (var i = 0; i < now.length; i++)
    {
        if (now[i] != originalWorkStatusSettings[i])
            return true;
    }
    
    return false;
}

var originalWorkStatusSettings = [
    <%= rdoAvailable.Checked.ToString().ToLower() %>,
    <%= rdoActive.Checked.ToString().ToLower() %>,
    <%= rdoOpen.Checked.ToString().ToLower() %>,
    <%= rdoNotLooking.Checked.ToString().ToLower() %>,
];

function CurrentWorkStatusSettings()
{
    return [
        $('<%= rdoAvailable.ClientID %>').checked,
        $('<%= rdoActive.ClientID %>').checked,
        $('<%= rdoOpen.ClientID %>').checked,
        $('<%= rdoNotLooking.ClientID %>').checked,
    ];
}
</script>