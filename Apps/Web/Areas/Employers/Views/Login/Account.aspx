<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<AccountModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Accounts"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
        <%= Html.StyleSheet(StyleSheets.LoginOrJoin) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    
    <% Html.RenderPartial("LinkedInAuth"); %>

    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.TextResizeDetector) %>
        <%= Html.JavaScript(JavaScripts.HeightGroups) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Log in, join or purchase to continue</h1>
    </div>
    <p>
        To view a resume <span class="free-caps">FREE</span> (excluding
        name, recent employers and contact details), please log in or join.
    </p>
    <p>
        To unlock a candidate's complete resume, you'll need to <%= Html.RouteRefLink("purchase", ProductsRoutes.NewOrder) %> credits or an unlimited plan.<br />&nbsp;</p>

        <div class="main_columns columns forms_v2">
            <div class="column">
                <div class="login_section px335_shadowed_section shadowed_section section">
                    <div class="section-head"></div>
                    <div class="section-body" heightgroup="0">
                        <div class="section-title">
                            <h2>Log in to your account</h2>
                        </div>
                        <div class="section-content">
<%  using (Html.RenderForm(Context.GetClientUrl(true), "LoginForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
                            <%=Html.TextBoxField(Model.Login, m => m.LoginId).WithLabel("Username")%>
                            <%=Html.PasswordField(Model.Login, m => m.Password).WithLabel("Password")%>
                            <%=Html.CheckBoxField(Model.Login, m => m.RememberMe).WithLabelOnRight("Remember me")%>
                            <%=Html.RouteLinkField("Forgot password?", LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.NewPassword, new { userType = UserType.Employer}).WithId("ForgotPassword") %>
                            <%=Html.ButtonsField().Add(new SubmitButton("login", "Log in >", "login_button button"))%>
<%      }
    } %>
                            <div class="linkedin">
                                <script type="in/Login"></script>
                            </div>

                            <div class="question">
                                Don't have an account?<br />
                                <a id="hlJoin" href="javascript:void(0);">Join with a new account</a>
                            </div>
                        </div>
                    </div>
                    <div class="section-foot"></div>
                </div>
                
                <div class="join_section px335_shadowed_section shadowed_section section" style="display: none">
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
                                <%= Html.TextBoxField(Model.Join, m => m.JoinLoginId).WithLabel("Username").WithIsRequired()%>
                                <%= Html.PasswordField(Model.Join, m => m.JoinPassword).WithLabel("Password").WithIsRequired()%>
                                <%= Html.PasswordField(Model.Join, m => m.JoinConfirmPassword).WithLabel("Confirm password").WithIsRequired()%>
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
                                <%= Html.EmailAddressTextBoxField(Model.Join, m => m.EmailAddress).WithLabel("Email address")%>
                                <%= Html.PhoneNumberTextBoxField(Model.Join, m => m.PhoneNumber).WithLabel("Phone number")%>
<%      }

        using (Html.BeginFieldSet())
        { %>
                                <% Html.PartialField("AcceptTerms", new CheckBoxValue { IsChecked = Model.AcceptTerms }).WithCssPrefix("checkbox").Render(); %>
                                <%= Html.ButtonsField().Add(new SubmitButton("join", "Join >", "join_button button"))%>
<%      } %>
                            </div>
<%  } %>                            
                            <div class="question">
                                Already have an account?<br />
                                <a id="hlLogin" href="javascript:void(0);">Log in with your existing account</a>
                            </div>
                        </div>
                    </div>
                    <div class="section-foot"></div>
                </div>
            </div>
            
            <div class="column">
                <div class="purchase_section px335_shadowed_section shadowed_section section">
                    <div class="section-head"></div>
                    <div class="section-body" heightgroup="0">
                        <div class="section-title">
                            <h2>Contact candidates</h2>
                        </div>
                        <div class="section-content">
                            <h3>Purchase credits</h3>
                            <ul>
                                <li>Access candidate contact details now</li>
                                <li>We offer a variety of flexible packages to suit your needs</li>
                                <li>Pay online with a credit card using our secure online payment gateway</li>
                            </ul>
                            <div class="forms_v2">
<%  using (Html.RenderForm(Context.GetClientUrl()))
    {
        using (Html.BeginFieldSet())
        { %>
                                <%= Html.ButtonsField().Add(new SubmitButton("purchase", "Purchase", "purchase_large_button large_button button"))%>
<%      }
    } %>
                            </div>
                            <p class="last"><%= Html.RouteRefLink("Find out more", HomeRoutes.Features) %> about our sourcing tools, or contact our sales team on
                            <span class="free-call-phone"><%= LinkMe.Apps.Agents.Constants.PhoneNumbers.FreecallHtml %></span>.</p>
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

            $("#hlJoin").click(function() { $(".login_section").hide(); $(".join_section").show(); });
            $("#hlLogin").click(function() { $(".login_section").show(); $(".join_section").hide(); });

            <% if (Model.Login != null)
   { %>
            $(".login_section").show(); $(".join_section").hide();
            <% }
   else
   { %>
            $(".login_section").hide(); $(".join_section").show();
            <% } %>

            var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialMatches) %>" + "?countryId=1&location=";
            $("#Location").autocomplete(apiPartialMatchesUrl);

        });

    </script>
    
</asp:Content>
