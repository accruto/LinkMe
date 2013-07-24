<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div id="ic-diagram"></div>
<h1 id="employers-look-for-indigenous">Employers are looking for Indigenous talent</h1>
<ul>
    <li>Thousands of employers search IndigenousCareers to fill roles in all areas including engineering, health, I.T., professions and trades.</li>
    <li>Don&rsquo;t want your boss to see? You can set your profile to anonymous.</li>
</ul>
<p id="career-blurb">Career is all about confidence. In times of uncertainty, it's always best to have options. Join IndigenousCareers now and proactively manage your jobs future.</p>
<div class="indigenous-affiliates">
    <a href="http://www.aboriginalemploymentstrategy.com.au/">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/indigenouscareers/img/aes.png") %>" alt="Visit AES" />
    </a>
    <a href="http://aimementoring.com/">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/indigenouscareers/img/aime.png") %>" alt="Visit AIME Mentoring" />
    </a>
</div>

