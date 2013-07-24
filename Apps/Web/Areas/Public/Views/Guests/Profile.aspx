<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.CandidateProfile)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="guestprofile">
        <div class="bg">
            <a class="joinlink" href="<%= JoinRoutes.Join.GenerateUrl() %>" title="Not a member? Join now">Not a member? Join now</a>
            <a class="loginlink" href="<%= Context.GetLoginUrl() %>" title="If you are already a member, login here">If you are already a member, login here</a>
            <a class="startlink" href="<%= JoinRoutes.Join.GenerateUrl() %>" title="Get started to create your profile in 30 seconds">Get started to create your profile in 30 seconds</a>
        </div>
        <% Html.RenderPartial("GoogleVerticalAds"); %>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $(".joinlink, .loginlink, .startlink").text("");
        });
    </script>
</asp:Content>

