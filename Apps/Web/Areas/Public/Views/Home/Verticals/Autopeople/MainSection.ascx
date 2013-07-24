<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/autopeople/img/643466_speedometer.jpg") %>" alt="Autopeople" />
    </div>
    <div id="main-body-text">
        <h1 style="font-weight: bolder; font-size: 18px;">Autopeople has teamed with LinkMe.com.au to bring a career network for the Automotive Industry</h1>
        <p><strong>Join the Autopeople Career Network to position your career for success in 3 easy steps:</strong></p>
        <ul>
            <li>Upload your resume and have employers and recruiters find you</li>
            <li>Anonymous or visible, you choose. Avoid unwanted attention from colleagues or your boss</li>
            <li>Build your automotive network. Connect with friends, associates and join industry groups. You never know who you'll meet.</li>
        </ul>
        <p>The Autopeople Career Network is free to join and takes only 1 minute to connect.</p>
        <p>Thinking about a change? Be found for opportunities that might surprise you.</p>
    </div>
</div>
