<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BlockListListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Context" %>

<span id="results-header-text" style="display:none;">
    <span class="blocklist-header-title">Block list&nbsp;</span>
    <span class="blocklist-header-title-separator">for&nbsp;</span>

<% if (Model.BlockList.BlockListType == BlockListType.Permanent)
   { %>
        <%= Html.Encode(Model.BlockList.GetNameDisplayText())%>
<% }
   else
 {
     if (Model.CurrentSearch == null)
     {%>
            <%=Html.Encode(Model.BlockList.GetNameDisplayText())%>
    <%
     }
     else
     {
         if (Model.CurrentSearch is SuggestedCandidatesNavigation)
         {%> 
            <%= ((SuggestedCandidatesNavigation)Model.CurrentSearch).JobAdTitle%>
           <%
         }
         else
         {%>
            <%=Model.CurrentSearch.Criteria.GetDisplayHtml()%>
            <span class="synonyms-filter-holder">
        <%
             if (Model.Criteria.IncludeSynonyms)
             {%>
                <span class="synonyms-filter-text">with synonyms</span>
        <%
             }
             else
             {%>
                <span class="synonyms-filter-text">without synonyms</span>
        <%
             }%>
    </span>
    <%
         }
     }
 }%>
</span>


