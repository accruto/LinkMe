<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BlockListListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Context" %>

<br /><br />There are no blocked candidates for <%
                                                    if (Model.CurrentSearch is SuggestedCandidatesNavigation)
                                                    { %>
                                                      the current Suggested Candidate list
                                                    <%} 
                                                    else
                                                    {%>
                                                       <%= Html.Encode(Model.BlockList.GetNameDisplayText()) %>
                                                    <%}%>.<br /><br />

