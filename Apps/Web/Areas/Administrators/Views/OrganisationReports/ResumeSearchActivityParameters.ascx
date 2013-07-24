<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ResumeSearchActivityReport>" %>
<%@ Import Namespace="LinkMe.Apps.Agents.Reports.Employers"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<%= Html.CheckBoxesField(Model)
    .Add("Include child organisations", r => r.IncludeChildOrganisations) %>
<%= Html.CheckBoxesField(Model)
    .Add("Include disabled users", r => r.IncludeDisabledUsers) %>