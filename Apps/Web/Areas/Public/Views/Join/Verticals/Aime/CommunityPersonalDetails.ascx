<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Join.Verticals.Aime.CommunityPersonalDetails" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Affiliations.Affiliates"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Domain.Users.Members.Affiliations"%>

<div class="divider-2-1"></div>

<%= Html.DropDownListField(Model.AffiliationItems, "AimeMemberStatus", i => GetStatus(i))
    .WithText(s => s.GetDisplayText())
    .WithAttribute("size", "6")
    .WithHelpArea("Indicate your reason for joining the AIME portal.")
    .WithLabel("AIME Status")%>

