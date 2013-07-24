<%@ Control Language="c#" AutoEventWireup="False" EnableViewState="false" Codebehind="IndustryList.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.IndustryList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<span class="forms_v2">
    <asp:ListBox id="lbIndustry" runat="server" Rows="10" CssClass="industry_listbox listbox" />
    <cc:LinkMeRequiredFieldValidator id="reqIndustry" runat="server" Enabled="false" controlToValidate="lbIndustry" />
</span>