<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FlagListListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<br /><br />
This folder does not contain any candidates.<br /><br />

The Flagged folder is used to collect all candidates you have flagged during your searches. You can flag
candidates by clicking on the flag in the top-right of any candidate record. You can clear flags by reclicking
individual candidates, or by using the “Clear flags” next to the Flagged folder or at the top of any
search.<br /><br />

You can manage all your folders from the <%= Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders) %> page.

