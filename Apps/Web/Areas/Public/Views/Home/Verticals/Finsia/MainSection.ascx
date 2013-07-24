<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image"><img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/finsia/img/v2/gears.png") %>" alt="" /></div>
    <div id="main-body-text" style="padding-left: 30px;">
        <h1 class="thousands-of-possibilities">One site ... <br />thousands of<br />career possibilities!</h1>
        <h2 class="your-online-community">Finsia Career Network is your online community of financial services professionals.</h2>
        <p><span><strong><span style="color: #000000; font-size: medium;">Join in three easy steps to:</span></strong></span></p>
        <ul>
            <li><span style="font-size: small;"><span><span><span style="color: #000000;"><strong>Manage and extend your personal business network </strong>by connecting with friends, colleagues and associates.</span></span></span></span></li>
            <li><span style="font-size: small;"><span><span><span style="color: #000000;"><strong>Participate in our member-exclusive groups </strong>to download resources and find out about upcoming events.</span></span></span></span></li>
            <li><span style="font-size: small;"><span><span><span style="color: #000000;"><strong>Expand your job prospects</strong> by tapping into the hidden jobs market, or use our extensive range of online tools.</span></span></span></span></li>
        </ul>
        <p><strong><span style="color: #000000; font-size: medium;">Get connected with Finsia Career Network.</span></strong></p>
    </div>
</div>