<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/corporateculcha/img/corpculcha_150x300.jpg") %>" alt="Corporate Culcha" />
    </div>
    <div id="main-body-text" style="width: 380px;">
        <h1>Join the Corporate Culcha Career Network. Create an account, upload your resume and you're ready!</h1>
        <p>Corporate Culcha is an Indigenous owned and operated company, established to support Indigenous organisations,   business and Australian industry, in building and developing sustainable Indigenous workforces.</p>
        <p>We work collaboratively with our clients to develop culturally competent strategies to engage,   recruit and retain Indigenous talent. Our extensive suite of products support the enhancement   of organisational cultures to be more inclusive of and accessible to Indigenous people.   Packages are tailored to individual business needs.</p>
        <p>Our clientele are diverse and include some of Australia&rsquo;s largest corporate entities,   Government departments and Non-Government Organisations.</p>
        <p>Join now for FREE! and get started.</p>
    </div>
</div>