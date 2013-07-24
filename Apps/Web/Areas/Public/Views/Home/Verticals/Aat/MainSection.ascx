<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/aat/img/aat_hp.jpg") %>" alt="AAT" />
    </div>
    <div id="main-body-text">
        <h1>AAT Australia</h1>
        <h2>Career &amp; Networking Portal</h2>
        <h3>The Association of Accounting Technicians Australia has partnered with leading career &amp; networking site, LinkMe.com.au, to provide AAT Australia members with the AAT Australia Career and Networking Portal.</h3>
        <ul>
            <li>Upload your resume and have employers and recruiters find you.</li>
            <li>Anonymous or visible, you choose. Avoid unwanted attention from colleagues or your boss.</li>
            <li>Build your professional network. Connect with friends, associates and join industry groups. You never know who you'll meet.</li>
        </ul>
        <p>The Career &amp; Networking Portal is free to join and only takes a minute to connect.</p>
        <p>Thinking about a change? Be found for opportunities that might surprise you.</p>
    </div>
</div>