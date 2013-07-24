<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<LinkMe.Web.Areas.Employers.Models.LinkedIn.LinkedInAccountModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets ID="RegisterStyleSheets1" runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
        <%= Html.StyleSheet(StyleSheets.LoginOrJoin) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts ID="RegisterJavaScripts1" runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.TextResizeDetector) %>
        <%= Html.JavaScript(JavaScripts.HeightGroups) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Log in or join with your LinkedIn profile</h1>
    </div>
    <p>
        To log in with your LinkedIn profile from now on we need some more information.
    </p>
    <p>
        If you already have a LinkMe account just log in now with your LinkMe username and password and we will
        associate your LinkedIn profile with that account.
    </p>
    <p>
        If you don't already have a LinkMe account please fill in your account and contact details.
        <br/>
        <br/>
     </p>

        <div class="main_columns columns forms_v2">
            <div class="column">
                <div class="login_section px335_shadowed_section shadowed_section section">
                    <div class="section-head"></div>
                    <div class="section-body" heightgroup="0">
                        <div class="section-title">
                            <h2>Log in to your existing account</h2>
                        </div>
                        <div class="section-content">
<%  using (Html.RenderForm(Context.GetClientUrl(true), "LoginForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
                            <%=Html.TextBoxField(Model.Login, m => m.LoginId).WithLabel("Username").WithIsRequired()%>
                            <%=Html.PasswordField(Model.Login, m => m.Password).WithLabel("Password").WithIsRequired()%>
                            <%=Html.RouteLinkField("Forgot password?", LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.NewPassword, new { userType = UserType.Employer}).WithId("ForgotPassword") %>
                            <%=Html.ButtonsField().Add(new SubmitButton("login", "Log in >", "login_button button"))%>
<%      }
    } %>
                        </div>
                    </div>
                    <div class="section-foot"></div>
                </div>
                
            </div>
            
            <div class="column">
                <div class="join_section px335_shadowed_section shadowed_section section">
                    <div class="section-head"></div>
                    <div class="section-body">
                        <div class="section-title">
                            <h2>Join with a new account</h2>
                        </div>
                        <div class="section-content">
<%  using (Html.RenderForm(Context.GetClientUrl(true), "JoinForm"))
    { %>
                            <div class="fieldsets">
<%      using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
                                <legend>Account details</legend>
                                <%= Html.TextBoxField(Model.Join, m => m.FirstName).WithLabel("First name").WithIsRequired()%>
                                <%= Html.TextBoxField(Model.Join, m => m.LastName).WithLabel("Last name").WithIsRequired()%>
                                <%= Html.TextBoxField(Model.Join, m => m.OrganisationName).WithLabel("Organisation name").WithIsRequired()%>
                                <%= Html.TextBoxField(Model.Join, m => m.Location).WithLabel("Location").WithIsRequired()%>
                                <%= Html.RadioButtonsField(Model.Join, m => m.SubRole).WithLabel("Role").WithIsRequired().WithCssPrefix("join-role-section")%>
                                <%= Html.IndustriesField(Model.Join, m => m.IndustryIds, Model.Industries).WithLabel("Industry").WithCssPrefix("join-industry-section")%>
<%      }

        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
                                <legend>Contact details</legend>
                                <%= Html.EmailAddressTextBoxField(Model.Join, m => m.EmailAddress).WithLabel("Email address").WithIsRequired()%>
                                <%= Html.PhoneNumberTextBoxField(Model.Join, m => m.PhoneNumber).WithLabel("Phone number").WithIsRequired()%>
<%      }

        using (Html.BeginFieldSet())
        { %>
                                <% Html.PartialField("AcceptTerms", new CheckBoxValue { IsChecked = Model.AcceptTerms }).WithCssPrefix("checkbox").Render(); %>
                                <%= Html.ButtonsField().Add(new SubmitButton("join", "Join >", "join_button button"))%>
<%      } %>
                            </div>
<%  } %>                            
                        </div>
                    </div>
                    <div class="section-foot"></div>
                </div>
            </div>
        </div>
    </p>
    
    <script language="javascript">
        jQuery(function($) {
            function initTextResizeDetector() {
                TextResizeDetector.addEventListener(updateHeightGroups, null);
            }
        
            updateHeightGroups();

            $(document).ready(function() {
                TextResizeDetector.TARGET_ELEMENT_ID = 'container';
                TextResizeDetector.USER_INIT_FUNC = initTextResizeDetector;
                TextResizeDetector.iIntervalDelay = 10;
            });

            var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialMatches) %>" + "?countryId=1&location=";
            $("#Location").autocomplete(apiPartialMatchesUrl);
        });

    </script>
    
</asp:Content>
