<%@ Register TagPrefix="cc1" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Message.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Message" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<asp:PlaceHolder id="phTo" runat="server" visible="false">
    <tr>
	    <td align="right">
		    <ASP:LABEL id="lblTo" runat="server"/>
	    </td>
	    <td>
		    <asp:TextBox id="txtTo" runat="server" textmode="MultiLine" rows="1" cssclass="wide-form-input" />
	    </td>
	</tr>
	<tr>
	    <td>
	    </td> 
	    <td>
		    (use comma to separate multiple emails)
		    <CC1:LINKMEREQUIREDFIELDVALIDATOR id="reqValTo" runat="server" controltovalidate="txtTo" />
		    <CC1:EmailAddressValidator id="validatorTo" runat="server" controltovalidate="txtTo" EmailAddressValidationMode="MultipleEmails" />
	    </td>
	</tr>
</asp:PlaceHolder>

<asp:PlaceHolder id="phFrom" runat="server" visible="false">
    <tr>
	    <td align="right">
	        <asp:label ID="lblFrom" runat="server">Your email address</asp:label>
	    </td>
	    <td>
		    <asp:TextBox id="txtFrom" runat="server" cssclass="wide-form-input" />
		    <CC1:LINKMEREQUIREDFIELDVALIDATOR id="reqValFrom" runat="server" controltovalidate="txtFrom"  />
		    <CC1:EmailAddressValidator id="validatorFrom" runat="server" controltovalidate="txtFrom"  />
	    </td>
	</tr>
</asp:PlaceHolder>
<asp:PlaceHolder id="phSubject" runat="server" visible="true">
    <tr>
	    <td align="right">
	        <label class="compulsory-small-form-label">Subject</label>
	    </td>
	    <td>
		    <asp:TextBox id="txtSubject" runat="server" cssclass="wide-form-input" />
		    <CC1:LINKMEREQUIREDFIELDVALIDATOR id="reqValSubject" runat="server" controltovalidate="txtSubject" />
	    </td>
	</tr>
</asp:PlaceHolder>

    <tr>
	    <td align="right" valign="top" style="padding-top:6px">
	        <asp:label ID="lblMessage" cssclass="compulsory-small-form-label" runat="server">Message</asp:label>
	    </td>
	    <td>
		    <asp:TextBox id="txtBody" runat="server" textmode="MultiLine" rows="10" cssclass="email-text-area" />
		    <CC1:LINKMEREQUIREDFIELDVALIDATOR id="reqValBody" runat="server" controltovalidate="txtBody" />
	    </td>
	</tr>
		
