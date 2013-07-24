<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <link href="<%= new ReadOnlyApplicationUrl("~/themes/communities/aime/css/aime_front-page.css") %>" rel="stylesheet" />
    <div id="mascot-image" style="float:left">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/aime/img/AimeFrontPage.jpg") %>" alt="Aime" />
    </div>
    <div id="main-body-text" style="padding-left: 30px; width: 350px;">
        <h1 style="font-weight: bolder; font-size: 20px;">Welcome to AIME Mentoring Careers.<br /></h1>
        <p class="MsoNormal"><span style="font-family: Arial; font-size: 9pt;">At AIME we have created the training ground for Australia's future leaders -  from our Mentees, to our Mentors, to our staff. The sky is the limit &ndash; a young  person at AIME can rise to leadership positions at ages previously unheard of  across the employment sector.<span>&nbsp; </span>Our CEO was 22 when he started in  that role.<br /></span></p>
        <p class="MsoNormal"><span style="font-family: Arial; font-size: 9pt;">At AIME we  believe that <strong>to be Indigenous means to be successful</strong> .</span></p>
        <p class="MsoNormal"><span style="font-family: Arial; font-size: 9pt;">We are  tackling Indigenous inequality head on and our staff and Mentors are stepping up  to say, &ldquo;This stops with us&rdquo;. </span></p>
        <p class="MsoNormal"><span style="font-family: Arial; font-size: 9pt;">You can be  at the heart of this journey. <span>&nbsp;</span>Are you up for the challenge?<span>&nbsp; </span><br /></span></p>
        <p class="MsoNormal"><span style="font-family: Arial; font-size: 9pt;">History  beckons . . . Will you walk with us?</span></p>
        <p class="MsoNormal"><span style="font-family: Arial; font-size: 9pt;">The AIME  Team</span></p>
    </div>
</div>