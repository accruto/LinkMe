<%@ Register TagPrefix="cc1" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Control Language="c#" AutoEventWireup="False" Codebehind="TermsConditionsCheckbox.ascx.cs" Inherits="LinkMe.Web.UI.TermsConditionsCheckbox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<asp:checkbox id="chkTermsOfUse" runat="server" />
<label class="compulsory" for="<%= chkTermsOfUse.ClientID %>">I agree to the <%= GetTCPopupHtml() %></label>
<cc1:linkmecustomvalidator ClientValidationFunction="ValidateTermsConditionsChecked" id="valChkTermsUse" runat="server" />

<script language="javascript">
	function ValidateTermsConditionsChecked(oSrc, args){
		var chk = document.getElementById('<%= chkTermsOfUse.ClientID %>');
		args.IsValid = chk.checked;
	}
</script>
