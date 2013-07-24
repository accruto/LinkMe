<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EmailField.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Fields.EmailField" %>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Register TagPrefix="uc" TagName="TextBoxFieldContents" Src="TextBoxFieldContents.ascx" %>

<uc:TextBoxFieldContents id="ucContents" runat="server" />
<cc:EmailAddressValidator id="valEmailAddress" runat="server" />
