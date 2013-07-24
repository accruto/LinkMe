<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Employers.Unsubscribe" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications" %>

<div class="footer">
    You have received this email because you have registered at
    <a target="_blank" href="<%= Html.HomeUrl() %>" style="color:white;">LinkMe.com.au</a>.
    <br />
    You can <a target="_blank" href="<%= Html.TinyLoginUrl("~/employers/login", "~/employers/settings") %>" style="color:white;">edit your settings</a>
    to modify the frequency of emails or
    <a target="_blank" href="<%= Html.TinyUrl(false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category) %>" style="color:white;">unsubscribe</a>.
</div>