<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div>
    <div id="mascot-image" style="filter: none;">
        <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/golfjobs/img/bunker.jpg") %>" alt="Golfjobs" />
    </div>
    <div id="main-body-text">
        <h1>The PGA has teamed with LinkMe.com.au to bring you Golfjobs.com.au, a career network for golf industry professionals and enthusiasts interested in working in the golf industry...wherever you are.</h1>
        <p>Join Golfjobs.com.au to position your career for success in 3 easy steps:</p>
        <ul>
            <li>Upload your resume and have employers and recruiters find you</li>
            <li>Set your visibility to avoid any unwanted attention from colleagues or your boss</li>
            <li>Build your golf network. Connect with friends, associates and groups. You never know who you'll meet.</li>
        </ul>
        <p>Golfjobs.com.au is free to join and takes only 1 minute to connect.</p>
        <p>Even if you aren't currently actively looking for work, it's worthwhile joining Golfjobs.com.au. Just set your work status to "Not looking but happy to talk" - your dream job could be just around the corner.</p>
    </div>
</div>
