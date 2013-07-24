<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.MainSection" %>

<div>
    <div id="mascot-image" class="pngfix" style="left: 0px;">
        <img style="margin-right: 25px;" src="<%= MascotImageUrl %>" alt="Jobs - Online Job Search for Jobs, Careers and Employment as LinkMe" />
    </div>
    <div id="main-body-text" style="padding-left: 2px;">
        <h1 style="font-size: 22px;">
            <img src="<%= UploadImageUrl %>" alt="Upload your resume, jobs come to you." width="313" height="73" />
        </h1>
        <ul style="margin-top: 13px; margin-left: 0; padding-left: 17px;">
            <li>1000s of employers search LinkMe to fill everything from engineering jobs to health jobs to IT jobs.</li>
            <li>Don't want your boss to see? Set your jobs profile to anonymous.</li>
        </ul>
        <p style="font-size: 85%; color: #214263;">
            <strong>Career is all about confidence; in times of uncertainty it's always best to have options. Join LinkMe now to proactively manage your jobs future.</strong>
        </p>
    </div>
</div>
