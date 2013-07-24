<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Join.Verticals.Finsia.CommunityPersonalDetails" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<div class="divider-2-1"></div>

<%= Html.TextBoxField(Model, "FinsiaMemberId", m => GetMemberId(m.AffiliationItems))
    .WithHelpArea("Enter your FINSIA membership ID if you are currently a member of FINSIA.")
    .WithLabel("FINSIA Membership ID")%>
