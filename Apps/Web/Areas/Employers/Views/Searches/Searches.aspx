<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="System.Web.Mvc.ViewPage<IList<MemberSearch>>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Query.Search.Members"%>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="section forced-first_section">
		<div class="section-title">
		    <div class="ribbon">
			    <div class="ribbon_inner">
				    <ul class="plain_horizontal_action-list horizontal_action-list action-list">
					    <li><a class="add-action" href="#" title="Create a new saved search">New</a></li>
				    </ul>
			    </div>
		    </div>
            <h1>Saved searches</h1>
        </div>
        <div class="list_section-content section-content">
        
<% if (Model.Count == 0)
   { %>
            <div>
                You currently have no saved searches.
            </div>
<% }
   else
   { %>
            <div>
                <table class="saved-searches list">
    <% foreach (var search in Model)
       { %>
                    <tr>
                        <td><%= search.GetDisplayHtml() %></td>
                    </tr>   
    <% } %>
                </table>
            </div>
<% } %>
        </div>
    </div>

</asp:Content>

