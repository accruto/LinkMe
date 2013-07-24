<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CampaignSummaryModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Communications.Settings"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<%= Html.TextBoxField(Model, m => m.Campaign.Name).WithIsReadOnly(Model.IsReadOnly).WithLargestWidth() %>

<% if (Model.Campaign.CreatedTime != DateTime.MinValue)
   { %>
    <%= Html.TextBoxField(Model, m => m.Campaign.Status).WithIsReadOnly(true) %>
    <%= Html.DateTimeField(Model, m => m.Campaign.CreatedTime).WithLabel("Created").WithFormat("d").WithIsReadOnly(true)%>
    <%= Html.TextBoxField(Model, m => m.CreatedBy.FullName).WithLabel("Created by").WithIsReadOnly(true) %>
<% } %>

<%= Html.DropDownListField(Model, m => m.Campaign.Category)
    .WithLabel("Campaign category")
    .WithIsReadOnly(Model.IsReadOnly) %>
<%= Html.DropDownListField(Model, m => m.Campaign.CommunicationDefinitionId, new Guid?[] { null }.Concat(from c in Model.CommunicationDefinitions select (Guid?)c.Id))
    .WithText(i => i == null ? null : (from c in Model.CommunicationDefinitions where c.Id == i select c.Name).Single())
    .WithLabel("Communication definition")
    .WithIsReadOnly(Model.IsReadOnly) %>
<%= Html.DropDownListField(Model, m => m.Campaign.CommunicationCategoryId, new Guid?[] { null }.Concat(from c in Model.CommunicationCategories select (Guid?)c.Id))
    .WithText(i => i == null ? null : (from c in Model.CommunicationCategories where c.Id == i select c.GetDisplayText()).Single())
    .WithLabel("Communication category")
    .WithIsReadOnly(Model.IsReadOnly) %>
<%= Html.MultilineTextBoxField(Model, m => m.Campaign.Query).WithSize(15, 175).WithIsReadOnly(Model.IsReadOnly).WithLargestWidth() %>

