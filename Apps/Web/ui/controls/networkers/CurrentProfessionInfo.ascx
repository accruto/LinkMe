<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CurrentProfessionInfo.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.CurrentProfessionInfo" %>
<%@ Register TagPrefix="uc" TagName="IndustryList" Src="../../controls/common/IndustryList.ascx" %>

<asp:PlaceHolder id="phCurrentJobAndEmployer" runat="server">
    <div class="textbox_field field">
        <label>Current job title</label>
        <div class="textbox_control control">
            <asp:TextBox id="txtCurrentJob" runat="server" cssclass="textbox" />
        </div>
    </div>
    <div class="textbox_field field">
        <label>Current employer</label>
        <div class="textbox_control control">
            <asp:TextBox id="txtCurrentEmployer" runat="server" cssclass="textbox" />
        </div>
    </div>
</asp:PlaceHolder>
<div class="industry_listbox_field listbox_field field">
    <label>Industries you work in</label>
    <div class="industry_listbox_control listbox_control control">
        <uc:IndustryList id="ucCurrentIndustry" runat="server" Required="false" SelectionMode="Multiple" />
    </div>
    <div class="helptext">Hold down the <strong>control</strong> key to select more than one.</div>
</div>
