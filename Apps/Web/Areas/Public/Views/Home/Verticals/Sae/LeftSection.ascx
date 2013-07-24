<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>Search Automotive Jobs</h1>
</div>
<div class="home-editable-section-content">
    <ul>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&CountryId=1&Distance=&Keywords=automotive&resultIndex=0") %>">Automotive Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=automotive%2c+graduate&CountryId=1&SortOrder=1&JobTypes=31") %>">Graduate Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=automotive%2c+mechanic&CountryId=1&SortOrder=1&JobTypes=31") %>">Mechanic Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=automotive&AnyKeywords=science%2c+research&CountryId=1&SortOrder=1&JobTypes=31") %>">Science &amp; Research Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=engineering&AnyKeywords=automotive%2c+mechanical&CountryId=1&SortOrder=1&JobTypes=31") %>">Automotive &amp; Mechanical Engineering Jobs</a></li>
    </ul>
</div>
