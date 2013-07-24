<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>Indigenous Services</h1>
    </div>
    <div class="home-editable-section-content">
        <div class="ic-teaser">
            <h3><a href="<%= new ReadOnlyApplicationUrl("~/ui/unregistered/groups/ViewGroup.aspx?groupId=ccf7931c-dc66-429e-b230-acd619be6ae8") %>">Indigenous Graduates group</a></h3>
            <p>Join this group to identify as an Indigenous graduate.</p>
        </div>
        <div class="ic-teaser">
            <h3><a href="<%= new ReadOnlyApplicationUrl("~/ui/unregistered/groups/ViewGroup.aspx?groupId=092ee69e-5c3d-433b-9b45-bf7916f12b87") %>">Indigenous Professionals group</a></h3>
            <p>Join this group to identify as an Indigenous professional.</p>
        </div>
        <div class="ic-teaser"><img class="ic-illustration" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/indigenouscareers/img/illust_inbox.gif") %>" alt="" />
            <h3>Jobs straight to your inbox</h3>
            <p>Get email alerts when the jobs you want are up for grabs.</p>
        </div>
        <div class="ic-teaser"><img class="ic-illustration" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/indigenouscareers/img/illust_keep-posted.gif") %>" alt="" />
            <h3>Keep your resume online</h3>
            <p>Employers are looking at Indigenous resumes now.</p>
        </div>
        <div class="ic-teaser"><img class="ic-illustration" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/indigenouscareers/img/illust_tools.gif") %>" alt="" />
            <h3>Be well-connected</h3>
            <p>Keeping in touch with others exposes you to more employment opportunities and support.</p>
        </div>
    </div>        
</div>
