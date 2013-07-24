<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="VisibilityBasicSettings.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.VisibilityBasicSettings" %>
<%@ Import namespace="LinkMe.Web.UI.Registered.Networkers"%>

<style type="text/css">
.visibility-explanation-text
{
    display: none;
    padding: 5px;
    padding-left: 20px;
}
</style>

<div style="margin-top: 15px;">

<% if (ShowAdvancedSettingsLink)
   { %>
        <a href="<%= GetUrlForPage<VisibilitySettingsAdvanced>() %>">Advanced Settings</a>
<% } %>

</div>

<div id="visibility-settings-explanatory-text"></div>

<p>Select how visible you want to be to other members</p>

<div style="padding-left: 20px;">
    <div id="custom-visibility-text" class="visibility-explanation-text">
    <%= TEXT_CUSTOM_VISIBILITY_EXPLANATION %>
    </div>
    <ul class="input-list">
        <li>
            <asp:RadioButton ID="rdoHighlyVisible" runat="server" GroupName="BasicVisibility" Text="Highly visible" Checked="false" />
            <div id="rdoHighlyVisible_div" class="visibility-explanation-text">
                <%= TEXT_HIGHLY_VISIBLE_EXPLANATION %>
            </div>
        </li>
        <li>
            <asp:RadioButton ID="rdoModeratelyVisible" runat="server" GroupName="BasicVisibility" Text="Moderately visible (recommended)" Checked="false" />
            <div id="rdoModeratelyVisible_div" class="visibility-explanation-text">
                <%= TEXT_MODERATELY_VISIBLE_EXPLANATION %>
            </div>
        </li>
        <li>
            <asp:RadioButton ID="rdoLessVisible" runat="server" GroupName="BasicVisibility" Text="Less visible" Checked="false" />
            <div id="rdoLessVisible_div" class="visibility-explanation-text">
                <%= TEXT_LESS_VISIBLE_EXPLANATION %>
            </div>
        </li>
        <li>
            <asp:RadioButton ID="rdoInvisible" runat="server" GroupName="BasicVisibility" Text="Invisible" Checked="false" />
            <div id="rdoInvisible_div" class="visibility-explanation-text">
                <%= TEXT_INVISIBLE_EXPLANATION %>
            </div>
        </li>
    </ul>
</div>

<script type="text/javascript">
    var rdoHiVis = $('<%= rdoHighlyVisible.ClientID %>');
    var rdoMidVis = $('<%= rdoModeratelyVisible.ClientID %>');
    var rdoLoVis = $('<%= rdoLessVisible.ClientID %>');
    var rdoInVis = $('<%= rdoInvisible.ClientID %>');
    
    var UserHasAdvancedVisibilitySettings = !(rdoHiVis.checked || rdoMidVis.checked || rdoLoVis.checked || rdoInVis.checked);
    var button_to_change;
    
    var originalVisibilityBasicSettings = [
        <%= rdoHighlyVisible.Checked.ToString().ToLower() %>,
        <%= rdoModeratelyVisible.Checked.ToString().ToLower() %>,
        <%= rdoLessVisible.Checked.ToString().ToLower() %>,
        <%= rdoInvisible.Checked.ToString().ToLower() %>
    ];
    
    function VisibilityBasicSettingsHaveChanged()
    {
        now = CurrentVisibilityBasicSettings();
        
        for (var i = 0; i < originalVisibilityBasicSettings.length; i++)
        {
            if (now[i] != originalVisibilityBasicSettings[i])
            {
                return true;
            }
        }
        
        return false;
    }
    
    function CurrentVisibilityBasicSettings()
    {
        return [
            $('<%= rdoHighlyVisible.ClientID %>').checked,
            $('<%= rdoModeratelyVisible.ClientID %>').checked,
            $('<%= rdoLessVisible.ClientID %>').checked,
            $('<%= rdoInvisible.ClientID %>').checked
        ];
    }
    
    function ShowBasicVisibilityRadioButtonMessage()
    {
        if (rdoHiVis.checked)
        {
            DisplayVisibilityTextForButton(rdoHiVis);
        }
        else  if (rdoMidVis.checked)
        {
            DisplayVisibilityTextForButton(rdoMidVis);
        }
        else  if (rdoLoVis.checked)
        {
            DisplayVisibilityTextForButton(rdoLoVis);
        }
        else  if (rdoInVis.checked)
        {
            DisplayVisibilityTextForButton(rdoInVis);
        }
        else 
        {
            DisplayElement('custom-visibility-text', 'block');
        }
    }
</script>
