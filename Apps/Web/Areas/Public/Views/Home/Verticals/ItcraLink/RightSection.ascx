<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>ITCRA Certification</h1>
</div>    

<div class="home-editable-section-content">
    <p style="font-size: 85%;">The ITCRA Training Supporters listed below provide high quality, relevant programs to support the Certification Exam and other Professional Development needs.</p>
    <ul class="action-list" style="font-size: 85%;">
        <li><a href="http://itcra.com.au/itcra-certification/itcra-certification">About the ITCRA Certification Exam</a></li>
    </ul>
    <div class="self-clearing" style="padding-bottom: 15px;"><a style="float: left;" href="http://www.hcmglobal.com.au/"><img title="HCM Global" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/logo-hcm-global.gif") %>" alt="HCM Global" /></a><a style="float: right;" href="http://www.recruitmentacademy.com.au/"><img title="Recruitment Academy" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/logo-recruitment-academy.gif") %>" alt="Recruitment Academy" /> </a></div>
    <div class="self-clearing" style="padding-bottom: 15px;"><a style="float: left;" href="http://www.trtc.com.au/"><img title="The Recruitment Training Company" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/logo-trtc.gif") %>" alt="The Recruitment Training Company" /> </a><a style="float: right;" href="http://www.carmanwhite.com/mainsite/"><img title="Carman White" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/logo-carman-white.gif") %>" alt="Carman White" /> </a></div>
    <div class="self-clearing" style="padding-bottom: 15px;"><a style="float: left;" href="http://www.offeredandaccepted.com.au/"><img title="Offered and Accepted" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/logo-offered-and-accepted.gif") %>" alt="Offered and Accepted" /> </a><a style="float: right;" href="http://www.imagination-training.com.au/"><img style="position: relative; top: -6px;" title="Imagination Training" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/itcralink/img/logo-imagination-training.gif") %>" alt="Imagination Training" /> </a></div>
</div>            
