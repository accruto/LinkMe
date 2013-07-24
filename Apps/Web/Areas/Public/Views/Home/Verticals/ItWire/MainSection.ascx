<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itwire/img/vbanner.png") %>" alt="ITWire" />
    </div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 18px;">iTWire has teamed with LinkMe to bring you MyProfile, a new way to network in the IT&amp;T careers community.</h1>
        <p>Join MyProfile to position your career for success in 3 easy steps:</p>
        <ul>
            <li>Upload your resume and have employers and recruiters find you</li>
            <li>Anonymous or visible, you choose. Avoid unwanted attention from colleagues or your boss</li>
            <li>Build your professional network. Connect with friends, colleagues and join industry groups. Your connections could hold the key to your next job</li>
        </ul>
        <p>MyProfile is free to join and takes only 1 minute to connect.</p>
    </div>
</div>
