<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<System.Collections.Generic.IList<LinkMe.Domain.Resources.Category>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<%
    var categories = Model;
    var items = new List<SelectListItem>
                    {new SelectListItem {Selected = true, Text = "Entire resources section", Value = "-1"}};
    foreach (var c in categories)
        items.Add(new SelectListItem { Selected = false, Text = c.Name, Value = c.Id.ToString() });
%>
<div class="search">
	<div class="leftbar"></div>
    <div class="bg">
        <div class="title">Search for help and advice</div>
        <div class="keywords">
            <%= Html.TextBoxField("Keywords", "Enter keywords").WithAttribute("data-watermark", "Enter keywords").WithLabel("") %>
        </div>
        <div class="in">in</div>
        <div class="categorylist">
            <div class="selected">Entire resources section</div>
            <%= Html.DropDownList("SearchCategory", items) %>
            <div class="dropdown-items"></div>
        </div>
        <div class="button search" url="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.Search)) %>"></div>
    </div>
	<div class="rightbar"></div>
</div>