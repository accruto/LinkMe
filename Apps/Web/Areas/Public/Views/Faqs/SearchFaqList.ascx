<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Faqs.FaqsViewUserControl" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<div class="search-faqlist">
    <div class="title">Results</div>
<%  if (Model.Results.Faqs.Count == 0)
    { %>
        <div class="noresult">
            <span class="prompt">Your search "<span class="bold"><%= Model.Criteria.Keywords %></span>" did not match any articles.</span>
            <span class="suggestions">
                <div class="title">Suggestions:</div>
                <ul>
                    <li>Please make sure all words are spelled correctly.</li>
                    <li>Try different keywords.</li>
                </ul>
            </span>
            <p class="noluck">
                <span>Still no luck?<a class="link contactus" href="javascript:void(0)">Email</a>LinkMe customer support</span>
            </p>
        </div>
<%  }
    else
    {
        foreach(var faqId in Model.Results.FaqIds)
        {
            var faq = Model.Results.Faqs[faqId]; %>
            <a class="faqitem" href="<%= faq.GenerateUrl(Model.Categories) %>" faqid="<%= faq.Id %>" keywords="<%= Model.Criteria.Keywords %>">
                <span class="title" title="<%= faq.Title %>"><%= HighlightTitle(faq.Title) %></span>
                <span class="content"><%= SummarizeContent(faq.Text) %></span>
            </a>            
<%      }
    } %>
    <div class="bottombar"></div>
</div>