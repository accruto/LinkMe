<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Intro" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<div>
    <p>
        ... Now?
    </p>
    <p>
        If you are available to start work immediately,
        log into your <a href="<%= Html.TinyUrl(true, "~/members/profile") %>">profile</a>
        and change your work status to 'Immediately available'.
        You will be sent reminders daily to maintain your status, so you can
        keep your profile fresh and in the minds of thousands of recruiters and
        employers!
    </p>
</div>
