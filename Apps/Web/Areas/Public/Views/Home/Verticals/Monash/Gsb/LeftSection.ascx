<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>Search Graduate Jobs</h1>
</div>
<div class="home-editable-section-content">
    <ul>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=marketing&AnyKeywords=Graduate%2c+Internship&CountryId=1&SortOrder=1&JobTypes=31") %>">Graduate Jobs in Marketing</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=business&AnyKeywords=Graduate%2c+Internship&CountryId=1&SortOrder=1&JobTypes=31") %>">Graduate Jobs in Business</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AnyKeywords=Graduate%2c+Internship&CountryId=1&IndustryIds=995542b4-11f8-401e-b288-300fc9f6e376&SortOrder=1&JobTypes=31") %>">Graduate Jobs in HR &amp; Recruitment</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=finance&AnyKeywords=Graduate%2c+Internship&CountryId=1&SortOrder=1&JobTypes=31") %>">Graduate Jobs in Finance</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=business+law&AnyKeywords=Graduate%2c+Internship&CountryId=1&SortOrder=1&JobTypes=31") %>">Graduate Jobs in Business Law</a></li>
    </ul>
</div>
