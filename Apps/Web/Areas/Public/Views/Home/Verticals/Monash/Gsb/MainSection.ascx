<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/monash/gsb/img/balls.png") %>" alt="Monash Business and Economics" />
    </div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 34px; width: 400px;"><span style="font-size: x-large;">Get your resume in the hands of 1000's of employers</span></h1>
        <p><span style="font-size: medium;">The Career and Networking Portal connects you with thousands of employers and recruiters, as well as helping you leverage your personal, university and professional networks to achieve your career outcomes.</span></p>
        <ul>
            <li><span style="font-size: medium;">Open to all Business and Economics postgraduate students, it's free and takes less than a minute to join.</span></li>
            <li><span style="font-size: medium;">You retain full control of your privacy and what information employers see about you.</span></li>
            <li><span style="font-size: medium;">Simply upload a resume, set your career objectives and have employers and recruiters from all industries calling you about relevant job opportunities.</span></li>
        </ul>
        <p><span style="font-size: large;"><strong><br /></strong></span></p>
    </div>
</div>
