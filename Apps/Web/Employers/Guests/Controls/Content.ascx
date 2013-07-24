<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="Content.ascx.cs" Inherits="LinkMe.Web.Employers.Guests.Controls.Content" %>
<%@ Import Namespace="LinkMe.Apps.Agents"%>
<%@ Import Namespace="LinkMe.Apps.Presentation"%>
<%@ Register TagPrefix="uc" TagName="Login" src="~/ui/controls/common/Login.ascx" %>

<div id="main-body-guest">

    <div class="text-heading"><%=Heading %></div>
    
    <p>
        To get access to LinkMe call <span class="free-call-phone"><%= Constants.PhoneNumbers.FreecallHtml %></span>
    </p>

	<div id="body-login" style="margin-top: 20px;">
	    <uc:Login id="ucLogin" runat="server" ShowJoinLinks="false" />
	</div>
    
    <div style="margin-top:30px;">
        <img src="<%=ImageUrl %>" />
    </div>
    
</div>
