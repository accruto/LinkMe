<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter:none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/rcsa/img/rcsa_hp.png") %>" alt="RCSA" />
    </div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 18px;">Take Control of your career with RCSA Recruiter Connections<br /></h1>
        <p>No matter where you go, by keeping your recruitment network alive with Recruiter Connections you'll keep your valuable connections in the recruitment profession, even when people move. Keep up-to-date with the latest "inside news", share your thoughts with other recruiters, and keep your name in the profession no matter what challenges come your way.</p>
        <p>It's simple to take charge of your future with Recruiter Connections:</p>
        <ul>
            <li>Create your own personal profile </li>
            <li>Upload your CV so lost contacts can find you (as well as prospective employers)<br /></li>
            <li>Link with professional recruitment colleagues and contacts</li>
            <li>Join in on discussions with blogs and news sharing</li>
        </ul>
        <p>You can start to build your own recruitment industry network right now. It's <strong>free </strong>and you're in control of your privacy and visibility to others.</p>
        <p>Get networking with Recruiter Connections!</p>
    </div>
</div>
