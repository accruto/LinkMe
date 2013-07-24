<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="display: none">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/itcralink-side.png") %>" alt="" />
    </div>
    <div id="main-body-text">
        <p><strong>Welcome to ITCRA Link, the online portal connecting ICT recruiters and providing opportunities to enhance and promote individual professionalism.</strong></p>
        <p>Register here to:</p>
        <ul>
            <li>Engage with other IT recruitment professionals</li>
            <li>Make a start on or maintain your <em>ITCRA Certification</em></li>
            <li>Take part in Professional Development sessions</li>
            <li>Form groups</li>
            <li>Take part in forums</li>
            <li>Stay informed of ITCRA's activities</li>
            <li>Keep up to date with what is happening in your industry</li>
            <li>Access ITCRA's Professional Voice</li>
            <li>Access services that will benefit you both professionally and personally.</li>
        </ul>
        <p>Join now: It's quick and it's free.</p>
    </div>
</div>