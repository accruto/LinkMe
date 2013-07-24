<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<UserModel<IMember, MemberLoginModel>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Members"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Member account</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderRouteForm(MembersRoutes.Enable, new { id = Model.User.Id }, "ContactForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            var primaryPhoneNumber = Model.User.GetPrimaryPhoneNumber();
            var secondaryPhoneNumber = Model.User.GetSecondaryPhoneNumber();
            var tertiaryPhoneNumber = Model.User.GetTertiaryPhoneNumber(); %>
         
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Contact details</h1>
                </div>
            
                <%= Html.NameTextBoxField(Model.User, e => e.FullName)
                    .WithLabel("Name").WithIsReadOnly() %>
                <%= Html.EmailLinkField(Model.User.EmailAddresses[0].Address, Model.User.EmailAddresses[0].Address)
                    .WithLabel("Email address") %>
                <%= Html.PhoneNumberTextBoxField(Model.User, "PrimaryPhoneNumber", e => primaryPhoneNumber == null ? null : primaryPhoneNumber.Number)
                    .WithLabel("Primary phone number").WithIsReadOnly() %>
                <%= Html.RadioButtonsField(Model.User, "PrimaryPhoneNumberType", e => primaryPhoneNumber == null ? (PhoneNumberType?) null : primaryPhoneNumber.Type)
                    .WithId(PhoneNumberType.Mobile, "PrimaryPhoneNumberMobile")
                    .WithId(PhoneNumberType.Home, "PrimaryPhoneNumberHome")
                    .WithId(PhoneNumberType.Work, "PrimaryPhoneNumberWork")
                    .WithIsReadOnly()
                    .WithLabel("") %>
                <%= Html.PhoneNumberTextBoxField(Model.User, "SecondaryPhoneNumber", e => secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Number)
                    .WithLabel("Secondary phone number").WithIsReadOnly() %>
                <%= Html.RadioButtonsField(Model.User, "SecondaryPhoneNumberType", e => secondaryPhoneNumber == null ? (PhoneNumberType?) null : secondaryPhoneNumber.Type)
                    .WithId(PhoneNumberType.Mobile, "SecondaryPhoneNumberMobile")
                    .WithId(PhoneNumberType.Home, "SecondaryPhoneNumberHome")
                    .WithId(PhoneNumberType.Work, "SecondaryPhoneNumberWork")
                    .WithIsReadOnly()
                    .WithLabel("") %>
                <%= Html.PhoneNumberTextBoxField(Model.User, "TertiaryPhoneNumber", e => tertiaryPhoneNumber == null ? null : tertiaryPhoneNumber.Number)
                    .WithLabel("Tertiary phone number").WithIsReadOnly() %>
                <%= Html.RadioButtonsField(Model.User, "TertiaryPhoneNumberType", e => tertiaryPhoneNumber == null ? (PhoneNumberType?) null : tertiaryPhoneNumber.Type)
                    .WithId(PhoneNumberType.Mobile, "TertiaryPhoneNumberMobile")
                    .WithId(PhoneNumberType.Home, "TertiaryPhoneNumberHome")
                    .WithId(PhoneNumberType.Work, "TertiaryPhoneNumberWork")
                    .WithIsReadOnly()
                    .WithLabel("") %>
                    
            </div>
            <div class="section-foot"></div>
        </div>

        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Personal details</h1>
                </div>
                
                <%= Html.TextBoxField(Model.User, e => e.Gender)
                    .WithLabel("Gender").WithIsReadOnly() %>
                <%= Html.TextBoxField(Model.User, "Age", e => e.DateOfBirth.GetAgeDisplayText())
                    .WithIsReadOnly() %>
                <%= Html.TextBoxField(Model.User, "Country", m => m.Address == null ? "" : (m.Address.Location == null ? "" : m.Address.Location.Country.Name))
                    .WithIsReadOnly() %>
                <%= Html.TextBoxField(Model.User, "Location", m => m.Address == null ? "" : m.Address.Location == null ? "" : m.Address.Location.ToString())
                    .WithIsReadOnly() %>
                
            </div>
            <div class="section-foot"></div>
        </div>

<%      }
    } %>

<%  using (Html.RenderRouteForm(MembersRoutes.Enable, new { id = Model.User.Id }, "EnableForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Account details</h1>
                </div>
                
                <%= Html.TextBoxField(Model.UserLogin, e => e.LoginId)
                    .WithLabel("Username").WithIsReadOnly().WithLargestWidth() %>
        
                <%= Html.DateField(Model.User, m => m.CreatedTime)
                    .WithLabel("Join date").WithIsReadOnly() %>
            
                <%= Html.YesNoTextBoxField(Model.User, e => e.IsEnabled)
                    .WithLabel("Enabled") %>

                <%= Model.User.IsEnabled ? Html.ButtonsField(new DisableButton()) : Html.ButtonsField(new EnableButton()) %>
                    
<%          if (!string.IsNullOrEmpty(Model.UserLogin.LoginId))
            { %>
        
                <%= Html.YesNoTextBoxField(Model.User, e => e.IsActivated)
                    .WithLabel("Activated") %>

                <%= Model.User.IsActivated ? Html.ButtonsField(new DeactivateButton()) : Html.ButtonsField(new ActivateButton()) %>
<%          } %>

            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

<%  using (Html.RenderRouteForm(MembersRoutes.ChangePassword, new { id = Model.User.Id }, "ChangePasswordForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            if (!string.IsNullOrEmpty(Model.UserLogin.LoginId))
            { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Change password</h2>
                </div>
                
                <p>The member will need to change their password on their next login.</p>
                
                <%= Html.TextBoxField(Model, m => m.UserLogin.Password)
                    .WithLabel("New password")
                    .WithExampleText("A new temporary password.")%>
                <%= Html.CheckBoxesField(Model)
                    .Add("Send new password email", m => m.UserLogin.SendPasswordEmail)
                    .WithExampleText("If this is checked then an email will be sent to the member.")%>
                
                <%= Html.ButtonsField(new ChangeButton()) %>
<%          } %>

            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

    </div>

</asp:Content>
