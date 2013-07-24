<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<LinkMe.Web.Areas.Administrators.Models.Organisations.OrganisationModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Domain.Users.Administrators"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Recruiters"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<%  if (Model.Organisation.IsVerified)
    { %>
        <%= Html.TextBoxField(Model.Organisation as VerifiedOrganisation, o => o.ParentFullName)
            .WithLabel("Parent organisation")
            .WithLargestWidth()
            .WithExampleText("Enter an existing organisation name to set it as the parent of this organisation") %>
<%  } %>

<%= Html.TextBoxField(Model.Organisation, o => o.Name)
    .WithIsRequired(Model.Organisation.IsVerified)
    .WithIsReadOnly(!Model.Organisation.IsVerified)
    .WithLargerWidth() %>
    
<%= Html.TextBoxField(Model.Organisation, "Location", o => o.Address == null || o.Address.Location == null ? null : o.Address.Location)
    .WithLargerWidth() %>    

<%  if (Model.Organisation.IsVerified)
    { %>
        <%= Html.AdministratorField(Model.Organisation as VerifiedOrganisation, o => o.AccountManagerId, Model.AccountManagers)
            .WithLabel("Account manager")%>
        <%= Html.TextBoxField("VerifiedBy", "Verified by " + Html.AccountManagerFullName(Model.VerifiedByAccountManager))
            .WithLabel("Status")
            .WithIsReadOnly()
            .WithLargerWidth() %>
<%  }
    else
    { %>
        <%= Html.TextBoxField("Status", "Not verified")
            .WithLabel("Status")
            .WithIsReadOnly()
            .WithLargerWidth() %>
<%  } %>

<%  if (Model.Organisation.IsVerified)
    {
        var verifiedOrganisation = (VerifiedOrganisation)Model.Organisation; %>
       
        <div class="section-title">
            <h2>Primary contact</h2>
        </div>
        <p>Reports, if enabled, will be emailed to this person.</p>
        
        <%= Html.NameTextBoxField(verifiedOrganisation.ContactDetails, c => c.FirstName)
            .WithLabel("First name")
            .WithLargerWidth() %>
        <%= Html.NameTextBoxField(verifiedOrganisation.ContactDetails, c => c.LastName)
            .WithLabel("Last name")
            .WithLargerWidth() %>
        <%= Html.EmailAddressTextBoxField(verifiedOrganisation.ContactDetails, "EmailAddress", c => c.GetEmailAddressesDisplayText())
            .WithLabel("Email address").WithExampleText("You can enter multiple email addresses separated by semicolons") %>

<%  } %>

<div class="section-title">
    <h2>Community</h2>
</div>
<p>Should this company be restricted to a single community's members?</p>

<%= Html.CommunityField(Model.Organisation, "CommunityId", o => o.AffiliateId, Model.Communities)
    .WithLabel("Community") %>

