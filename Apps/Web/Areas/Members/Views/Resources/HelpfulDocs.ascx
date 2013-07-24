<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="helpfuldocs">
    <div class="titlebar">
        <div class="leftbar"></div>
        <div class="bg">Helpful documents</div>
        <div class="rightbar"></div>
    </div>
    <div class="bg">
        <a target="_blank" class="whoami" href="<%= new ReadOnlyApplicationUrl("~/resources/member/resources/Who_Am_I.pdf") %>" title="Who am I?">Download PDF</a>
        <a target="_blank" class="actionverbs" href="<%= new ReadOnlyApplicationUrl("~/resources/member/resources/Action_verbs_for_achievement_statements.pdf") %>" title="Action verbs for achievement statement">Download PDF</a>
    </div>
    <div class="rightbar"></div>
</div>
