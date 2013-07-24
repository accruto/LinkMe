<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="IndustryField.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Fields.IndustryField" %>
<%@ Register TagPrefix="uc" TagName="IndustryList" Src="~/ui/controls/common/IndustryList.ascx" %>

<div id="divField" class="industry_listbox_field listbox_field field" runat="server">
    <asp:Label ID="lblField" AssociatedControlID="ucIndustryList" runat="server" />
    <div class="listbox_control control">
        <uc:IndustryList id="ucIndustryList" runat="server" />
    </div>
</div>
