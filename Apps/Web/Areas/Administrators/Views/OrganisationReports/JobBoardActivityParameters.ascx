<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JobBoardActivityReport>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Reports.Employers"%>

<%= Html.CheckBoxesField(Model)
    .Add("Include child organisations", r => r.IncludeChildOrganisations) %>
