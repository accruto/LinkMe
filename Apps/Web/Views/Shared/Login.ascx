<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Apps.Asp.Security.Login>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Web.Guests"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>

<%  using (Html.RenderForm(Context.GetClientUrl(true), "LoginForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
            <%= Html.TextBoxField(Model, m => m.LoginId).WithLabel("Username") %>
            <%= Html.PasswordField(Model, m => m.Password).WithLabel("Password") %>
            <%= Html.CheckBoxField(Model, m => m.RememberMe).WithLabelOnRight("Remember me") %>
            <%= Html.RouteLinkField("Forgot password?", AccountsRoutes.NewPassword).WithId("ForgotPassword") %>
            <%= Html.ButtonsField().Add(new SubmitButton("login", "Log in >", "login_button button")) %>
<%      }
    } %>

