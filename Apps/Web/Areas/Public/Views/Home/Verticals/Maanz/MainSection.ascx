<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/maanz/img/maanz_hp.png") %>" alt="MAANZ" />
    </div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 18px;">The Marketing Association of Australia and New Zealand has teamed  with LinkMe.com.au to bring a career network for marketing professionals.</h1>
        <p>Join the MAANZ jobs network to position your career for success in 3 easy steps:</p>
        <ul>
            <li>Upload your resume and have employers and recruiters find you</li>
            <li>Anonymous or visible, you choose. Avoid unwanted attention from colleagues or your boss</li>
            <li>Build your professional network. Connect with friends, associates and join industry groups. You never know who you'll meet.</li>
        </ul>
        <p>It's <strong>free</strong> to join and takes only 1 minute to connect.</p>
        <p>Thinking about a change? Be found for opportunities that might surprise you.</p>
    </div>
</div>
