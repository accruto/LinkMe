<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.SplashImage" %>

<div runat="server">
    <div id="splash-image">
        <div id="splash-image-1" class="pngfix">
            <a href="<%= JoinUrl %>"><img src="<%= Step1ImageUrl %>" alt="Job Search - Upload your resume for free" width="237" height="147" border="0" /></a>
        </div>
        <div id="splash-image-2" class="pngfix">
            <img src="<%= StepArrowImageUrl %>" width="67" height="147"/>
        </div>
        <div id="splash-image-3" class="pngfix">
            <a href="<%= EmployerUrl %>"><img src="<%= Step2ImageUrl %>" alt="Job Search - Employers search for you" width="239" height="147" border="0" /></a>
        </div>
    </div>
</div>

