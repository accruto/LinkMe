<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/aimpe/img/aimpe_hp2.png") %>" alt="" />
    </div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 18px;">The Australian Institute Of Marine And Power Engineers has teamed with LinkMe.com.au to bring a career network for workers in the maritime industry.</h1>
        <p>Join the AIMPE jobs community to position your career for success in 3 easy steps:</p>
        <ul>
            <li>Upload your resume and have employers and recruiters find you</li>
            <li>Anonymous or visible, you choose. Avoid unwanted attention from colleagues or your boss</li>
            <li>Stay in touch with your maritime family. Connect with friends, associates and join industry groups. Your connections could hold the key to your next job</li>
        </ul>
        <p>It's <strong>free</strong> to join and takes only 1 minute to connect.</p>
    </div>
</div>
