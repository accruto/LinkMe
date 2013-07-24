<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="JobTypesCheckBoxList.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.JobTypesCheckBoxList" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<span class="forms_v2">
    <asp:checkboxlist id="cblJobTypes" runat="server" CellPadding="4" RepeatLayout="Flow" RepeatDirection="Horizontal" TextAlign="Right" />
    <CC:LINKMECHECKBOXLISTVALIDATOR id="reqJobTypes" runat="server" controlToValidate="cblJobTypes" enabled="false" />
</span>