<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.Accounts.AccountModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models" %>
<%@ Import Namespace="LinkMe.Domain.Contacts" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes" %>
<%@ Import Namespace="LinkMe.Web.Html" %>

<div class="two-columns">
    <div class="column">
        <div class="login-section two-columns-shadowed-section shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Log in to your account</h2>
                </div>
                <div class="section-content">
<%  using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
    { %>
                    <%= Html.TextBoxField(Model.Login, m => m.LoginId).WithLabel("Username").WithIsRequired() %>
                    <%= Html.PasswordField(Model.Login, m => m.Password).WithLabel("Password").WithIsRequired()%>
                    <%= Html.CheckBoxField(Model.Login, m => m.RememberMe).WithLabelOnRight("Remember me") %>
                    <%= Html.RouteLinkField("Forgot password?", AccountsRoutes.NewPassword, new { userType = UserType.Employer }) %>
                    <%= Html.ButtonsField(new LoginButton())%>
<%  } %>
                </div>
            </div>
            <div class="section-foot"></div>
        </div>
    </div>
    <div class="column">
        <div class="join-section two-columns-shadowed-section shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Join with a new account</h2>
                </div>
                <div class="section-content">
                    <div class="fieldsets">
<%  using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
    { %>
                        <legend>Account details</legend>
                        <%= Html.TextBoxField(Model.Join, m => m.JoinLoginId).WithLabel("Username").WithIsRequired()%>
                        <%= Html.PasswordField(Model.Join, m => m.JoinPassword).WithLabel("Password").WithIsRequired()%>
                        <%= Html.PasswordField(Model.Join, m => m.JoinConfirmPassword).WithLabel("Confirm password").WithIsRequired()%>
                        <%= Html.TextBoxField(Model.Join, m => m.FirstName).WithLabel("First name").WithIsRequired()%>
                        <%= Html.TextBoxField(Model.Join, m => m.LastName).WithLabel("Last name").WithIsRequired()%>
                        <%= Html.TextBoxField(Model.Join, m => m.OrganisationName).WithLabel("Organisation name").WithIsRequired()%>
                        <%= Html.TextBoxField(Model.Join, m => m.Location).WithLabel("Location").WithIsRequired()%>
                        <%= Html.RadioButtonsField(Model.Join, m => m.SubRole).WithLabel("Role").WithIsRequired().WithCssPrefix("join-role-section")%>
                        <%= Html.IndustriesField(Model.Join, m => m.IndustryIds, Model.Industries).WithLabel("Industry") %>
<%  } %>

<%  using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
    { %>
                        <legend>Contact details</legend>
                        <%= Html.EmailAddressTextBoxField(Model.Join, m => m.EmailAddress).WithLabel("Email address").WithIsRequired()%>
                        <%= Html.PhoneNumberTextBoxField(Model.Join, m => m.PhoneNumber).WithLabel("Phone number").WithIsRequired()%>
<%  } %>
                                
<%  using (Html.BeginFieldSet())
    { %>
                        <% Html.PartialField("AcceptTerms", new CheckBoxValue { IsChecked = Model.AcceptTerms }).WithCssPrefix("checkbox").WithIsRequired().Render(); %>
                        <%= Html.ButtonsField(new JoinButton())%>
<%  } %>
                                
                    </div>
                </div>
            </div>
            <div class="section-foot"></div>
        </div>
    </div>
</div>
