<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Domain.Contacts.Member>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>

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
        p span.blue {
            color: #307EEC;
        }
        a {
            float: none;
            color: #307EEC;
            text-decoration: none;
        }
        li {
            list-style: inside;
            font-size: 1.75em;
            color: #307EEC;
            text-decoration: none;
            margin : 0 0.4285714285714286em 2em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Welcome to LinkMe</div>
    <p>You can now save jobs and searches direct from your mobile device.</p>
    <p>Please check your email <span class="blue"><%= Model.EmailAddresses[0].Address %></span> to verify your account.</p>
    <p>To complete your LinkMe profile, please upload your resume from your compuer.</p>
    <ul>
        <li><%= Html.RouteRefLink("Start a new job search", HomeRoutes.Home) %></li>
    </ul>
</asp:Content>