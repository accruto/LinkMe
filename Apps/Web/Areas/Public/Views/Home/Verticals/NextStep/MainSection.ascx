<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image"><img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/nextstep/img/linkme02.jpg") %>" alt="Next Step Australia" width="149" height="198" /></div>
    <div id="main-body-text">
        <h1>Direct Exposure to the Australian Job Market</h1>
        <p>Next Step Australia's job portal connects you with employers and recruiters across Australia.</p>
        <ul>
            <li>Simply upload your resume, set your career objectives and have employers and recruiters from all industries calling you about relevant job opportunities</li>
            <li>Listing your details is simple and takes just a couple of minutes to complete!</li>
            <li>Each week over 550 recruiters and employers list 60,000 jobs</li>
            <li>Access resources and tools to help you secure work</li>
            <li>Join the community and learn about working in Australia</li>
            <li>Retain full control of your privacy and what information employers see about you</li>
        </ul>
    </div>
</div>