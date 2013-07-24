<%@ Import namespace="LinkMe.Web"%>
<%@ Import namespace="LinkMe.Web.UI.Unregistered"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Content.ascx.cs" Inherits="LinkMe.Web.Guests.Controls.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<div id="main-body-guest">
    <div class="text-heading"><%=Heading %></div>
    <p>
        Not a member? <a href="<%= JoinRoutes.Join.GenerateUrl() %>">Join now</a> - it's free!
        Or you can <a href="<%= HttpContext.Current.GetLoginUrl() %>">log in here</a>
    </p>
    
	<div style="margin-top:50px;">
	    <img src="<%=ImageUrl %>" border="0" />
    </div>
</div>

