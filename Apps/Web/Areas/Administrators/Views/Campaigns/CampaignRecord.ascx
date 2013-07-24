<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CampaignRecord>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>

<div class="section-content forms_v2 record">

    <% Html.RenderPartial("CampaignRecordActions", Model); %>
    
    <fieldset>

        <%= Html.TextBoxField(Model, m => m.Name).WithIsReadOnly().WithCssPrefix("title").WithLargestWidth() %>
        <%= Html.TextBoxField(Model, m => m.Subject).WithIsReadOnly().WithCssPrefix("subtitle").WithLargestWidth() %>

    </fieldset>

</div>
