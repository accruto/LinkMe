<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>Search Accounts Jobs</h1>
</div>
<div class="home-editable-section-content">
    <ul>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Accounts+Payable&CountryId=1&SortOrder=1&JobTypes=31") %>">Accounts Payable Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Accounts+Receivable&CountryId=1&SortOrder=1&JobTypes=31") %>">Accounts Receivable Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=payroll&CountryId=1&SortOrder=1&JobTypes=31") %>">Payroll Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Finance+Manager&CountryId=1&SortOrder=1&JobTypes=31") %>">Finance Manager Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Finance+Assistant&CountryId=1&SortOrder=1&JobTypes=31") %>">Finance Assistant Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Assistant+Accountant&CountryId=1&SortOrder=1&JobTypes=31") %>">Assistant Accountant Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AnyKeywords=Bookkeeping%2c+Bookkeeper&CountryId=1&SortOrder=1&JobTypes=31") %>">Bookkeeping Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Trainee+Accountant&CountryId=1&SortOrder=1&JobTypes=31") %>">Trainee Accountant Jobs</a></li>
        <li><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Reconciliation&CountryId=1&SortOrder=1&JobTypes=31") %>">Reconciliation Jobs</a></li>
    </ul>
</div>
