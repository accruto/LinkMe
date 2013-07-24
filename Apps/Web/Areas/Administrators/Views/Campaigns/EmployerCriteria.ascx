<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CampaignEmployerCriteriaModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
   { %>

    <%= Html.CheckBoxesField(Model)
        .Add(m => m.Criteria.Employers)
        .Add(m => m.Criteria.Recruiters)
        .WithLabel("Include")
        .WithIsReadOnly(Model.IsReadOnly)
    %>

    <%= Html.CheckBoxesField(Model)
        .Add("Verified", m => m.Criteria.VerifiedOrganisations)
        .Add("Unverified", m => m.Criteria.UnverifiedOrganisations)
        .WithLabel("Organisations")
        .WithIsReadOnly(Model.IsReadOnly)
    %>

    <%= Html.IndustriesField(Model, m => m.Criteria.IndustryIds, Model.Reference.Industries)
        .WithLabel("Industries")
        .WithIsReadOnly(Model.IsReadOnly) %>
        
    <%= Html.TextBoxField(Model, m => m.Criteria.MinimumLogins)
        .WithLabel("Minimum logins")
        .WithIsReadOnly(Model.IsReadOnly) %>
        
    <%= Html.TextBoxField(Model, m => m.Criteria.MaximumLogins)
        .WithLabel("Maximum logins")
        .WithIsReadOnly(Model.IsReadOnly) %>

<% } %>
