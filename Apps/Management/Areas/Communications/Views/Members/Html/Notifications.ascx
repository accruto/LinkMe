<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Notifications" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td background="<%= Html.ImageUrl("sidebar_section_subtitle_bg.png") %>" style="padding: 6px; border-bottom: 1px solid #FFF; font-size: 8.5pt;" bgcolor="#F0F0F0">
            <p>
                You may have noticed in the past few months some unexpected emails from us
                containing lists of job ads on our site. These are jobs that we believe are
                matched to your profile, based on your nominated job title, keywords in your
                resume and your location.
            </p>
            <p>
                If you find the jobs are not appropriate, make sure that your
                <a href="<%= Html.TinyUrl(true, "~/members/profile") %>">profile</a>
                is up to date and accurate in terms of your salary expectations, desired job and
                suburb location. You can also control the frequency you receive these emails
                by editing your settings when logged into your profile.
            </p>
        </td>
    </tr>
</table>

