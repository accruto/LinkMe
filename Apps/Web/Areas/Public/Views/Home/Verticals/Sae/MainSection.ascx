<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/sae/img/imagehomepage.jpg") %>" alt="Society of Automotive Engineers" width="149" height="300" />
    </div>
    <div id="main-body-text">
        <h1>Welcome to SAE-A CareerDrive.com.au</h1>
        <p><strong>Why join SAE-A CareerDrive.com.au?</strong></p>
        <p>CareerDrive.com.au is a program developed by SAE-A with the support and assistance of both the State Government of Victoria and the Commonwealth Government of Australia with the aim of creating a highly qualified pool of skilled, job ready candidates for the Australian automotive industry.</p>
        <p>The aims of the program are to:</p>
        <ul>
            <li>Offer an online networking/self promotion tool for all SAE-A members</li>
            <li>Make available professionally delivered career transition coaching workshops and where required one on one follow up coaching sessions</li>
            <li>Deliver a series of free targeted technical skills enhancement training courses</li>
            <li>Improve the career opportunities for all participants through available SAE-A networking mechanisms</li>
            <li>Assist SAE-A members, current Engineers and future Graduates to establish rewarding careers within the Australian automotive industry</li>
        </ul>
        <p>In summary - If you need assistance in advancing your career within the automotive industry now is the time for you to take control by joining SAE-A CareerDrive.com.au.</p>
    </div>
</div>