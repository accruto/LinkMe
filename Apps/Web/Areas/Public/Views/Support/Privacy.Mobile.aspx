﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        #mainbody {
            padding: 0em 0.75em;
        }
        #mainbody .title {
	        font-size : 2.25em;
	        font-weight : Bold;
	        color : #464646;
	        line-height : 1.2em;
	        margin : 0.5em 0;
        }
        #mainbody a {
            float: none;
            display: inline;
            color: #307EEC;
            text-decoration: none;
        }
        
        .terms-links li a {
            font-size: 1.5em;
            font-weight: bold;
            line-height: 1.5em;
        }
        ol {
	        counter-reset: item;
        }
        li {
	        display: block;
        }
        .privacy li:before {
	        content: counters(item, ".") " ";
	        counter-increment: item;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Privacy statement</div>
    <ul class="terms-links">
        <li><%= Html.RouteRefLink("General terms and conditions", SupportRoutes.Terms)%></li>
        <li><%= Html.RouteRefLink("Member terms and conditions", SupportRoutes.MemberTerms)%></li>
        <li><%= Html.RouteRefLink("Employer terms and conditions", SupportRoutes.EmployerTerms)%></li>
    </ul>

	<% Html.RenderPartial("PrivacyContent", true); %>
</asp:Content>