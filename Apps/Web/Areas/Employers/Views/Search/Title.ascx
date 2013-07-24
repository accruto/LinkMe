<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<span id="results-header-text" style="display:none;">
    <%= Model.Criteria.GetDisplayHtml()%>
    <% if (Model.Criteria.KeywordsExpression != null || Model.Criteria.AllKeywords != null || Model.Criteria.AnyKeywords != null || Model.Criteria.ExactPhrase != null || Model.Criteria.JobTitleExpression != null || Model.Criteria.JobTitle != null || Model.Criteria.WithoutKeywords != null)
       { %>
    <span class="synonyms-filter-holder">
        <% if (Model.Criteria.IncludeSynonyms)
           { %>
                <span class="synonyms-filter-text">with synonyms</span><span class='mini-light-x_button synonyms js_synonyms-filter'></span> 
        <% }
           else
           { %>
                <span class="synonyms-filter-text">without synonyms</span><span class='mini-light-x_button js_synonyms-filter'></span>
        <% } %>
    </span>
    <% } %>
</span>


