<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CampaignMemberCriteriaModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Domain"%>

<% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
   { %>
    <legend>Job filters</legend>

    <%= Html.TextBoxField(Model, m => m.Criteria.JobTitle)
        .WithLabel("Job title")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>
    
    <%= Html.TextBoxField(Model, m => m.Criteria.CompanyKeywords)
        .WithLabel("Worked for")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>
    
    <%= Html.TextBoxField(Model, m => m.Criteria.DesiredJobTitle)
        .WithLabel("Desired job titles")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>

<% } %>

<% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
   { %>
    <legend>Keywords</legend>

    <%= Html.TextBoxField(Model, m => m.Criteria.AnyKeywords)
        .WithLabel("With at least one of the words")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>
    
    <%= Html.TextBoxField(Model, m => m.Criteria.AllKeywords)
        .WithLabel("With all the words")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>
    
    <%= Html.TextBoxField(Model, m => m.Criteria.ExactPhrase)
        .WithLabel("With the exact phrase")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>
    
    <%= Html.TextBoxField(Model, m => m.Criteria.WithoutKeywords)
        .WithLabel("Without the words")
        .WithIsReadOnly(Model.IsReadOnly)
        .WithLargerWidth() %>

<% } %>

<% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
   { %>
    <legend>Location</legend>

    <%= Html.CountryField(Model, "CountryId", m => m.Criteria.Location == null ? (int?)null : m.Criteria.Location.Country.Id, Model.Reference.Countries)
        .WithLabel("Country")
        .WithIsReadOnly(Model.IsReadOnly) %>
    
    <%= Html.ControlsField()
        .WithLabel("Within")
        .Add(Html.TextBoxField(Model, m => m.Criteria.Distance).WithLabel("km of"))
        .WithIsReadOnly(Model.IsReadOnly) %>
    
    <%= Html.TextBoxField(Model, m => m.Criteria.Location)
        .WithIsReadOnly(Model.IsReadOnly) %>

    <%= Html.ControlsField()
        .WithLabel("Relocation")
        .Add(Html.CheckBoxField(Model, m => m.Criteria.IncludeRelocating).WithLabel("Include members willing to relocate")) %>

<% } %>

<% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
   { %>
    <legend>Industry</legend>

    <%= Html.IndustriesField(Model, m => m.Criteria.IndustryIds, Model.Reference.Industries)
        .WithLabel("Industries")
        .WithIsReadOnly(Model.IsReadOnly) %>

<% } %>

<% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
   { %>
    <legend>Filters</legend>

    <%= Html.ControlsField()
        .WithLabel("Desired salary")
        .Add(Html.TextBoxField(Model, "SalaryLowerBound", m => m.Criteria.Salary == null ? null : m.Criteria.Salary.LowerBound).WithLabel("to"))
        .Add(Html.TextBoxField(Model, "SalaryUpperBound", m => m.Criteria.Salary == null ? null : m.Criteria.Salary.UpperBound).WithLabel("per annum"))
        .WithIsReadOnly(Model.IsReadOnly) %>

    <%= Html.CheckBoxesField(Model, m => m.Criteria.CandidateStatusFlags)
        .WithLabel("Job hunter status")
        .Without(CandidateStatusFlags.All)
        .WithLabel(CandidateStatusFlags.AvailableNow, "Immediately available")
        .WithLabel(CandidateStatusFlags.ActivelyLooking, "Actively looking")
        .WithLabel(CandidateStatusFlags.OpenToOffers, "Not looking but happy to talk")
        .WithLabel(CandidateStatusFlags.NotLooking, "Not looking")
        .WithLabel(CandidateStatusFlags.Unspecified, "Unspecified")
        .WithIsReadOnly(Model.IsReadOnly) %>
    
    <%= Html.CheckBoxesField(Model, m => m.Criteria.EthnicStatus)
		.Without(EthnicStatus.None)
        .WithLabel("Indigenous background")
        .WithLabel(EthnicStatus.Aboriginal, "Australian Aboriginal")
        .WithLabel(EthnicStatus.TorresIslander, "Torres Strait Islander")
        .WithIsReadOnly(Model.IsReadOnly) %>
    
    <%= Html.CommunityField(Model, m => m.Criteria.CommunityId, Model.Reference.Communities)
        .WithLabel("Community")
        .WithIsReadOnly(Model.IsReadOnly) %>

<% } %>
