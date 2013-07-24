<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<div class="login_ascx forms_v2">
    <div class="login-box">
        <div class="login-box-content">
            <form id="LoginForm" method="post" action="<%= HomeRoutes.Home.GenerateUrl() %>">
            
<%  if (Model.Login != null && !ViewData.ModelState.IsValid)
    {
        var messages = ViewData.ModelState.GetErrorMessages();
        if (messages.Length > 0)
        { %>
                <div class="login-failure login-error"><%= messages[0] %></div>
<%      }
    } %>

                <fieldset>
                    <div class="username_textbox_field textbox_field field">
                        <label>Username</label>
                        <div class="username_textbox_control textbox_control control">
                            <input type="text" id="LoginId" name="LoginId" value="<%= Model.Login != null ? Model.Login.LoginId : "" %>" class="username_textbox textbox login-submit" />
                        </div>
                    </div>
                    <div class="password_textbox_field textbox_field field">
                        <label>Password</label>
                        <div class="password_textbox_control textbox_control control">
                            <input type="password" id="Password" name="Password" class="password_textbox textbox login-submit" />
                        </div>
                    </div>
                    
                    <div class="layout_control control">
                        <div class="sublayout_control control">
                            <div class="rememberme_checkbox_control checkbox_control control">
                                <input type="checkbox" id="RememberMe" name="RememberMe" class="rememberme_checkbox checkbox" />
                                <label for="RememberMe">Remember me</label>
                            </div>
                            <div class="forgotpassword_link_control link_control control">
                                <%= Html.RouteRefLink("Forgot password?", LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.NewPassword, null, new { @class = "forgotpassword_link link" }) %>
                            </div>
                        </div>
                        <div class="login_button_control button_control control">
                            <input type="button" id="login" onclick="javascript:validateLogin();" class="login_button button" />
                            <br />
                        </div>    		        
                    </div>
                    <input type="hidden" id="HiddenLogin" name="login" value="" />
                </fieldset>		    
            </form>
        </div>
    </div>
</div>


