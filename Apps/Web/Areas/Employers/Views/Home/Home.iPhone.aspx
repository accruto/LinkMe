<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Blank.Master" Inherits="System.Web.Mvc.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Home"%>

<asp:Content ContentPlaceHolderID="MetaTags" runat="server">
    <%= Html.MetaTag("viewport", "width = device-width, user-scalable = no") %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css" >
        .bg {
            background-image : url("<%= Images.IosHomeBackground %>");
            height : 872px;
            width : 640px;
        }
        .continuelink {
            color : White;
            cursor : pointer;
            display : block;
            font : bold 36px Arial;
            margin-left : 87px;
            padding-top : 17px;
        }
        .appstore {
            cursor : pointer;
            display : block;
            height : 102px;
            margin : 681px 0px 0px 174px;
            width : 290px;
        }
    </style>
</asp:Content>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%  var clientUrl = Context.GetClientUrl().AsNonReadOnly();
    clientUrl.QueryString["ignorePreferred"] = true.ToString().ToLower(); %>

    <div class="bg">
        <%= Html.RouteRefLink("Continue to LinkMe.com.au", SupportRoutes.SwitchBrowser, new { mobile = false, returnUrl = clientUrl }, new { @class = "continuelink", id = "Continue" })%>
        <a class="appstore" href="<%= Model.AppStoreUrl %>" title="The LinkMe official iPhone app available on the Apple App Store"></a>
    </div>
</asp:Content>