<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.CentreSection" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>Recruitment Resources</h1>
    </div>
    <div class="home-editable-section-content">
        <div style="text-align: center;">
            <img style="border: none;" usemap="#rcsamap" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/rcsa/img/logos.png") %>" alt="" />
            <map id="rcsamap" name="rcsamap">
                <area shape="rect" coords="0,0,110,56" href="http://www.recruitmentsuper.com.au" alt="Recruitment Super" target="_blank" />
                <area shape="rect" coords="111,0,237,56" href="http://www.postjobsonce.com.au" alt="postjobsonce" target="_blank" />
                <area shape="rect" coords="0,57,237,83" href="http://www.rcsa.com.au/newshub/index.html" alt="NewsHub" target="_blank" />
            </map>
        </div>
    </div>        
</div>
