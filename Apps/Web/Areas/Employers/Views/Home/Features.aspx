<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.EmployerHome)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div class="yellow-ribbon"></div>
	<div class="left-side">
		<div class="top-bar"></div>
		<div class="bg">
			<div class="content"></div>
			<div class="togglebutton" for="features"></div>
			<div class="togglebutton" for="benefits"></div>
			<div class="features"></div>
			<div class="benefits"></div>
		</div>
		<div class="bottom-bar"></div>
	</div>
	<div class="right-side">
		<div class="top-bar"></div>
		<div class="bg">
			<div class="content"></div>
			<div class="togglebutton" for="features"></div>
			<div class="togglebutton" for="benefits"></div>
			<div class="features"></div>
			<div class="benefits"></div>
		</div>
		<div class="bottom-bar"></div>
	</div>
	<script language="javascript" type="text/javascript">
		$(document).ready(function() {
			$("#container").addClass("features");
			$(".togglebutton").click(function() {
				$(this).parent().find("." + $(this).attr("For")).toggle();
			});
		});
	</script>
</asp:Content>