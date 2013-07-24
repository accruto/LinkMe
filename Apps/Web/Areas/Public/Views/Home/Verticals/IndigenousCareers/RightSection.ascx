<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>Career Resources</h1>
</div>    

<div class="home-editable-section-content">
    <div class="ic-teaser">
        <h3><a href="http://www.auroraproject.com.au/charlie_perkins.htm">Guide to Indigenous Scholarships</a></h3>
        <p>The <em>Indigenous students' guide to postgraduate scholarships in Australia and overseas</em> includes over 120 postgraduate scholarships across all disciplines.</p>
    </div>
    <div class="ic-teaser">
        <a href="http://www.isagroup.com.au/"><img class="ic-illustration" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/indigenouscareers/img/illust_isa.gif") %>" alt="" /></a>
        <h3 class="ic-thinner"><a href="http://www.isagroup.com.au/">Indigenous Success Australia</a></h3>
        <p>The ISA Group plans to give Indigenous Australians real choice and opportunity to actively and productively engage in the economic and social life of this country.</p>
    </div>
</div>            
