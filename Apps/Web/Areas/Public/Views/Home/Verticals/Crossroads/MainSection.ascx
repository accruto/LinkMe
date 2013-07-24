<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/crossroads/img/xroadimg_1.jpg") %>" alt="" />
    </div>
    <div id="main-body-text">
        <p style="margin-top:0;"><strong><span style="font-size: medium;">Welcome to the Crossroads Career Network!</span></strong></p>
        <p style="margin-top:0;"><strong>Crossroads Human Resources has teamed with LinkMe to bring you a career network for those recently in career transition.</strong></p>
        <p>Linkme provides Crossroads Outplacement Candidates with a unique opportunity to increase your exposure to employers looking for your skills and consequently increase your chances of finding the right job for you.</p>
        <p>So now that you have polished your marketing tools and are ready to present to market.</p>
        <p>All you need to do is:</p>
        <ol>
            <li>Create a login</li>
            <li>Upload your resume</li>
            <li>Choose whether you wish your details to be anonymous or visible</li>
            <li>Build your career network. Connect with friends, associates and join industry groups. Your connections could hold the key to finding your next job<br /><br />Should you require any assistance, please do not hesitate to contact Crossroads on 9862 5900. </li>
        </ol>
    </div>
</div>