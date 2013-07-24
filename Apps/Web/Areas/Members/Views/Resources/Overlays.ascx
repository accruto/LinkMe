<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<System.Collections.Generic.IList<LinkMe.Domain.Resources.Category>>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<%
    var categories = Model;
    var items = new List<SelectListItem>
                    {new SelectListItem {Selected = true, Text = "Select category", Value = "-1"}};
    foreach (var c in categories)
        items.Add(new SelectListItem { Selected = false, Text = c.Name, Value = c.Id.ToString() });
%>
<div class="loginprompt">
    <div class="topbar"></div>
    <div class="bg">
        <div class="prompt text"></div>
        <div class="prompt login">
            <%= Html.LoginLink("Log in", Context, new { @class = "login"}) %> to your account, or if you don't already have a login, <%= Html.RouteRefLink("create", JoinRoutes.Join) %> a new account now.
        </div>
        <div class="button cancel"></div>
    </div>
    <div class="bottombar"></div>
</div>

<div class="askexperts">
    <div class="topbar"></div>
    <div class="bg">
        <div class="title">Select a category for your question</div>
        <div class="categorylist">
            <div class="selected">Select category</div>
            <%= Html.DropDownList("AskCategory", items) %>
            <div class="dropdown-items"></div>
        </div>
        <%= Html.TextArea("QuestionText", null, new Dictionary<string, object>{ { "data-watermark", "Write your question here"}, { "maxlength", "500" } }) %>
        <div class="charsleft">Characters left: 500</div>
    </div>
    <div class="divider"></div>
    <div class="bg dark">
        <div class="checkbox checked"></div>
        <div class="permission">I give LinkMe permission to send my name and email address to RedStarResume. (Your details will not be used for any purposes other than to respond to your question).</div>
        <div class="disclaimer">Disclaimer: All responses will be sent directly from RedStarResume and LinkMe takes no responsibility for the content. Due to the high volume of questions received, RedStarResume may not be able to respond to each question individually.</div>
        <div class="errorinfo"></div>
        <div class="succinfo"></div>
        <div class="buttonarea">
            <div class="button ask" url="<%= Html.MungeUrl(Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.ResourcesRoutes.ApiAskQuestion)) %>"></div>
            <div class="button cancel"></div>
        </div>
    </div>
    <div class="bottombar"></div>
</div>