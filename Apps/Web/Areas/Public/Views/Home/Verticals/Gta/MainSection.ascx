<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="left: 5px;"><img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/gta/img/homepage.jpg") %>" alt="" /></div>
    <div id="main-body-text">
        <h1>Looking to start an apprenticeship or traineeship? Or recently completed your training?</h1>
        <p>Join GTA's Apprentice Trainee Careers Network and connect with 1000s of employers and recruiters across Australia.</p>
        <p>Upload your resume, employers come to you.</p>
        <p>It only takes 1 minute to join and it's free.</p>
        <p>To control who sees your details set your job profile to anonymous.</p>
    </div>
</div>