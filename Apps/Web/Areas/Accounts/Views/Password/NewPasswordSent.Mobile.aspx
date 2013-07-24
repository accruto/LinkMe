<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Accounts.Models.NewPasswordModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        #mainbody .title {
	        font-size : 2.25em;
	        font-weight : Bold;
	        color : #464646;
	        line-height : 1.2em;
	        margin : 0 0.3333333333333333em 0.5em;
        }
        p {
            font-size: 1.75em;
            color: #464646;
            line-height: 1.2em;
            margin : 0 0.4285714285714286em 2em;
        }
        li {
            list-style: inside;
	        font-size : 1.75em;
	        font-weight : Bold;
	        color : #464646;
	        line-height : 1.2em;
	        margin : 0 0.4285714285714286em 0.5em;        }
        #mainbody ul li a {
            float: none;
            color: #307EEC;
            text-decoration: none;
            display: inline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Request a new password</div>
    <p>Your new password has been emailed to you at <strong><%= Model.LoginId %></strong></p>
    <ul>
        <li><a class="login" href="<%= Html.RouteRefUrl(LoginsRoutes.LogIn) %>">Login</a> to your account now</li>
    </ul>
</asp:Content>