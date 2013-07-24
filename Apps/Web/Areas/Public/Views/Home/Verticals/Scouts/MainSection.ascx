<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;"><img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/scouts/img/vbanner.jpg") %>" alt="Scouts Australia" /></div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 20px;">The Scouts Australia Career Network<br /></h1>
        <p style="text-align: justify;">Is it your first time applying for a full time job or are you looking for a change? The Scouts Australia Career Network will help you find your path!</p>
        <ul>
            <li>Make yourself visible to thousands of employers and recruiters who are looking for employees with the character and skills that Scouts Australia members have to offer. </li>
            <li>Browse jobs and choose the ones that are right for you.</li>
            <li>Store your applications and resume online.</li>
            <li>Build your professional network by connecting with friends, associates and business groups, and by participating in Scouts-based community groups. </li>
            <li>Take a comprehensive personality test that allows you to determine your strengths and weaknesses and your most suitable work environment.</li>
        </ul>
        The Scouts Australia Career Network is free to join and takes only one minute to connect. Realise your full potential... launch your new career today!<br />
    </div>
</div>