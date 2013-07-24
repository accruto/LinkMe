<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CandidateCareReport>" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Reports.Employers"%>

<%= Html.CheckBoxesField(Model)
    .Add("Include child organisations", r => r.IncludeChildOrganisations) %>
<%= Html.TextBoxField(Model, r => r.PromoCode)
    .WithLabel("Promo code")
    .WithExampleText("The promo code assigned for this organisation's member referrals.") %>