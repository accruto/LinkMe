<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="VisibilitySettingsBasic.aspx.cs" Inherits="LinkMe.Web.UI.Registered.Networkers.VisibilitySettingsBasic" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import namespace="LinkMe.Web"%>
<%@ Register TagPrefix="uc" TagName="VisibilityBasicSettings" Src="~/ui/controls/networkers/VisibilityBasicSettings.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerPrivacy" Src="~/ui/controls/networkers/EmployerPrivacy.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkStatusSettings" Src="~/ui/controls/networkers/workStatusSettings.ascx" %>

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
        <h1>My visibility</h1>
    </div>

    <div style="float:right; text-align:right; margin-right: 20px;">
        <a href="javascript:void(0);" onclick="RestoreDefaults();">Restore Defaults</a>
    </div>

    <div class="clearer"></div>
    <p>Control what employers and other members see about you and how they communicate with you.</p>
    
    <div class="text-heading-2">Employment</div>

    <uc:EmployerPrivacy ID="ucEmployerPrivacy" runat="server" EnableShowResume="true" />
    
    <uc:WorkStatusSettings id="ucWorkStatusSettings" runat="server" />
    <br />
    
    <div class="text-heading-2">Friends and members</div> 

    <uc:VisibilityBasicSettings ID="ucVisibilityBasicSettings" runat="server" />
    
    <asp:Button id="btnSave" cssClass="save-button" runat="server" /> 
    <input type="button" id="btnCancel" class="cancel-button" onclick="buttonClicked=true; window.location.href='<%= GetRedirectUrl() %>'" />
    
    <script type="text/javascript">
        function RestoreDefaults()
        {
            RestoreEmployerPrivacyDefaults();
            RestoreVisibilityBasicSettingsDefaults();
        }
        
        function CheckForUnsavedChanges()
        {            
            if (PageHasChanged() && !buttonClicked)
                return 'You have unsaved changes.';
        }
        
        function PageHasChanged()
        {
            return VisibilityBasicSettingsHaveChanged() || EmployerPrivacyHasChanged();
        }
        
        window.onbeforeunload = CheckForUnsavedChanges;
        var buttonClicked = false;
            
        ShowBasicVisibilityRadioButtonMessage();
    </script>

</asp:Content>

