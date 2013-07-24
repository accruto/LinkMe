<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="VisibilitySettingsAdvanced.aspx.cs" Inherits="LinkMe.Web.UI.Registered.Networkers.VisibilitySettingsAdvanced" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="VisibilityBasicSettings" Src="~/ui/controls/networkers/VisibilityBasicSettings.ascx" %>
<%@ Register TagPrefix="uc" TagName="VisibilityAdvancedSettings" Src="~/ui/controls/networkers/VisibilityAdvancedSettings.ascx" %>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadScrollTracker();
    </script>
    <script type="text/javascript">
        LinkMeUI.JSLoadHelper.LoadOverlayPopup();
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    
    <div class="section-title">
        <h1>Friends &amp; members - advanced settings</h1>
    </div>
    
    <p>This page allows you to control exactly which of your details your friends and other members can see, and how they can 
    communicate with you. You may use either the basic settings or the individual advanced settings below.</p>
    
    <p>The settings on this page do not affect what employers can see.<br />
    Return to the <a href="<%= VisibilitySettingsUrl %>">Basic Settings</a> page if you wish to review what employers can see.</p>
    
    <div style="float:right; text-align:right; margin-right: 20px;">
        <a href="javascript:void(0);" onclick="RestoreDefaults();">Restore Defaults</a>
    </div>
    
    <div>
        <asp:Button id="btnSaveTop" cssClass="save-button" runat="server" />
        <input type="button" id="btnCancelTop" class="cancel-button" />
    </div>
    
    <div class="clearer"></div>

    <uc:VisibilityBasicSettings ID="ucVisibilityBasicSettings" ShowAdvancedSettingsLink="false" runat="server" />
    
    <hr />
    
    <uc:VisibilityAdvancedSettings ID="ucVisibilityAdvancedSettings" runat="server" />
    
    <div>
        <asp:Button id="btnSaveBottom" cssClass="save-button" runat="server" />
        <input type="button" id="btnCancelBottom" class="cancel-button" />
    </div>

    <script type="text/javascript" src="../../../js/VisibilitySettings.js"></script>
    
    <script type="text/javascript">
        function RestoreDefaults()
        {
            RestoreVisibilityBasicSettingsDefaults();
            RestoreVisibilityAdvancedSettingsDefaults();
        }
        
        function CancelButtonClicked()
        {
            window.location.href = '<%= VisibilitySettingsUrl %>';
        }
        
        $('btnCancelTop').observe('click', CancelButtonClicked);
        $('btnCancelBottom').observe('click', CancelButtonClicked);

        ShowBasicVisibilityRadioButtonMessage();
    </script>

</asp:Content>

