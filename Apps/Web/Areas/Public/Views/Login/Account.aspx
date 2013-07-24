<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LoginModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Logins"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Guests"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
        <%= Html.StyleSheet(StyleSheets.LoginOrJoin) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.TextResizeDetector) %>
        <%= Html.JavaScript(JavaScripts.HeightGroups) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Log in or join to continue</h1>
    </div>

    <div class="main_columns columns forms_v2">
        <div class="column">
            <div class="login_section px335_shadowed_section shadowed_section section">
                <div class="section-head"></div>
                <div class="section-body" heightgroup="0">
                    <div class="section-title">
                        <h2>Log in to your account</h2>
                    </div>
                    <div class="section-content">
                        <% Html.RenderPartial("Login", Model.Login); %>
                    </div>
                </div>
                <div class="section-foot"></div>
            </div>
        </div>
            
        <div class="column">
            <div class="join_section px335_shadowed_section shadowed_section section">
                <div class="section-head"></div>
                <div class="section-body" heightgroup="0">
                    <div class="section-title">
                        <h2>Join with a new account</h2>
                    </div>
                    <div class="section-content">
<%  using (Html.RenderForm(Context.GetClientUrl(true), "JoinForm"))
    { %>
                        <div class="fieldsets">

<%      using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
                            <%= Html.TextBoxField(Model.Join, m => m.FirstName).WithLabel("First name")%>
                            <%= Html.TextBoxField(Model.Join, m => m.LastName).WithLabel("Last name")%>
                            <%= Html.EmailAddressTextBoxField(Model.Join, m => m.EmailAddress).WithLabel("Email address").WithExampleText("Your username")%>
                            <%= Html.PasswordField(Model.Join, m => m.JoinPassword).WithLabel("Password")%>
                            <%= Html.PasswordField(Model.Join, m => m.JoinConfirmPassword).WithLabel("Confirm password")%>
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
        });

    </script>
    
</asp:Content>
