<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Unsubscribe" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<div class="footer">
    You have received this email because you have registered at
    <a href="<%= Html.HomeUrl() %>">LinkMe.com.au</a>.
    <br />
    You can <a href="<%= Html.TinyUrl(true, "~/members/settings") %>">edit your settings</a>
    to modify the frequency of emails or
    <a href="<%= Html.TinyUrl(false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category) %>">unsubscribe</a>.
</div>