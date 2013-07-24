<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>Scouts Reunited</h1>
</div>    

<div class="home-editable-section-content">
    <div style="text-align: center;">
        <a href="http://scoutsreunited.com.au/">
            <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/scouts/img/ScoutsReunited.png") %>" border="0" alt="Scouts Reunited" />
        </a>
        <br />
        <a href="http://scoutsreunited.com.au/"></a>
    </div>
    <div>
        <br />
    </div>
    <div style="text-align: center;">
        Find your old Scouting mates with <a href="http://scoutsreunited.com.au/">Scouts Reunited</a>,
        a free online community for all current and former <strong>Australian</strong> Scouts!
    </div>
</div>            
