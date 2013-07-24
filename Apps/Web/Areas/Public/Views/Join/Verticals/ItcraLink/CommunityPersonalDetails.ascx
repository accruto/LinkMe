<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Join.Verticals.ItcraLink.CommunityPersonalDetails" %>
<%@ Import Namespace="LinkMe.Domain.Users.Members.Affiliations.Affiliates"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<div class="divider-2-1"></div>

<%= Html.RadioButtonsField(Model, "ItcraLinkMemberStatus", m => GetStatus(m.AffiliationItems))
    .WithLabel(ItcraLinkMemberStatus.Certified, "ITCRA Certified – You have previously completed and passed the ITCRA Certification Program in 2008 to now.")
    .WithId(ItcraLinkMemberStatus.Certified, "ItcraLinkCertified")
    .WithLabel(ItcraLinkMemberStatus.ProfessionalMember, "ITCRA Professional Member – You have joined ITCRA as a Professional Member in 2010 or later.")
    .WithId(ItcraLinkMemberStatus.ProfessionalMember, "ItcraLinkProfessionalMember")
    .WithLabel("ITCRA Status")
    .WithVertical() %>

    <div class="field">
        <div class=control">
            <p>* Please note those individuals who completed Certification earlier than 2008 will now need to re-sit the ITCRA Certification exam, more information can be found on the ITCRA website.</p>
        </div>
    </div>

<%= Html.TextBoxField(Model, "ItcraLinkMemberId", m => GetMemberId(m.AffiliationItems))
    .WithLabel("ITCRA Membership ID") %>