<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <link href="<%= new ReadOnlyApplicationUrl("~/themes/communities/ahri/css/ahri_front-page.css") %>" rel="stylesheet" />
    <div id="mascot-image" style="left: 15px;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/ahri/img/ahrimain(1).gif") %>" alt="" />
    </div>
    <div id="main-body-text" style="padding-left: 10px;">
        <div>
            <span style="padding-left: 15px; font-family: Arial; color: #000033; font-size: medium;">
                <strong>Connect to your HR community</strong>
            </span>
        </div>
        <div id="main-body-text">
            <p class="MsoNormal" style="margin: 7.5pt 0cm;"><span style="font-family: Arial; font-size: 9.5pt;"><span style="color: #000033;">AHRI has created a new networking community. </span></span><span style="font-family: Arial; font-size: 9.5pt;"><span style="color: #000033;">Join and connect with employers and recruiters from all across Australia &ndash; even if you&rsquo;re not currently looking for work. </span></span></p>
            <p class="MsoNormal" style="margin: 7.5pt 0cm;"><span style="font-family: Arial; font-size: 9.5pt;"><span style="color: #000033;">This professional networking portal also helps you to build career relationships with other HR community members.</span></span></p>
            <p class="MsoNormal" style="margin: 7.5pt 0cm;"><span style="font-family: Arial; font-size: 9.5pt;"><span style="color: #000033;">Create your career profile with HRcareers Network today! </span></span><span style="font-family: Arial; font-size: 9.5pt;"><span style="color: #000033;">It only takes a minute to join and it&rsquo;s free.</span></span></p>
        </div>
    </div>
</div>