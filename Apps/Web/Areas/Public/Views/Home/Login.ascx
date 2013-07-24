<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Login" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="System.Web.Routing"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="login_ascx">
    <div class="section">
        <div class="left-section">
            <div class="section">
                <!--div class="not-a-member">Not a member?</div-->
                <div class="bottom-row">
                    <div class="homepage-button join_button js_join-dropdown" onclick="javascript:createJoinLayer();"></div>
                    <!--div class="today">today</div-->
                    <div class="join-section" style="display:none;">
                        <div class="arrow js_join-dropdown" onclick="javascript:createJoinLayer();">&nbsp;</div>
                        <div class="join-dropdown">
                            <div class="join-top">&nbsp;</div>
                            <div class="join-bg">
                                <div class="join-content">
                                    <div class="join-heading">
                                        <div class="heading">Candidate registration</div>
                                        <div class="sub-text">
                                            <div class="not-candidate">Not a candidate?</div>
                                            <a class="employer-link" href="<%= EmployerJoinUrl %>">Employer registration</a>
                                        </div>
                                    </div>
                                    <form id="JoinForm" method="post" action="<%= Context.GetClientUrl(true) %>">
                                        <div class="join-body">
<%  if (IsJoinError(""))
    { %>
                                            <div id="join_error" class="join-error"><%= GetJoinError("") %></div>
<%  }
    else
    { %>
                                            <div id="join_error" class="join-error" style="display:none;"></div>
<%  } %>
                                            
                                            <div class="homepage-field_holder">
                                                <div class="homepage-field_label">First name</div>
                                                <div class="text-holder">
                                                    <input class="homepage-field join_text-field join-submit" type="text" name="FirstName" id="FirstName" value="<%= Model.Join != null ? Model.Join.FirstName : "" %>" tabIndex="11" />
                                                </div>
                                            </div>
<%  if (IsJoinError("FirstName"))
    { %>
                                            <div id="first-name_error" class="join-error"><%= GetJoinError("FirstName") %></div>
<%  }
    else
    { %>
                                            <div id="first-name_error" class="join-error" style="display:none;"></div>
<%  } %>
                                            
                                            <div class="homepage-field_holder">
                                                <div class="homepage-field_label">Last name</div>
                                                <div class="text-holder">
                                                    <input class="homepage-field join_text-field join-submit" type="text" name="LastName" id="LastName" value="<%= Model.Join != null ? Model.Join.LastName : "" %>" tabIndex="12" />
                                                </div>
                                            </div>
<%  if (IsJoinError("LastName"))
    { %>
                                            <div id="last-name_error" class="join-error"><%= GetJoinError("LastName") %></div>
<%  }
    else
    { %>
                                            <div id="last-name_error" class="join-error" style="display:none;"></div>
<%  } %>
                                            
                                            <div class="homepage-field_holder">
                                                <div class="homepage-field_label">Email<div class="homepage-field_sub-label">(Your username)</div></div>
                                                <div class="text-holder">
                                                    <input class="homepage-field join_text-field join-submit" type="text" name="EmailAddress" id="EmailAddress" value="<%= Model.Join != null ? Model.Join.EmailAddress : "" %>" tabIndex="13" />
                                                </div>
                                            </div>
<%  if (IsJoinError("EmailAddress"))
    { %>
                                            <div id="email_error" class="join-error"><%= GetJoinError("EmailAddress")%></div>
<%  }
    else
    { %>
                                            <div id="email_error" class="join-error" style="display:none;"></div>
<%  } %>
                                            
                                            <div class="homepage-field_holder">
                                                <div class="homepage-field_label">Password</div>
                                                <div class="text-holder">
                                                    <input class="homepage-field join_text-field join-submit" type="password" name="JoinPassword" id="JoinPassword" value="<%= Model.Join != null ? Model.Join.JoinPassword : "" %>" tabIndex="14" />
                                                </div>
                                            </div>
<%  if (IsJoinError("JoinPassword"))
    { %>
                                            <div id="password_error" class="join-error"><%= GetJoinError("JoinPassword")%></div>
<%  }
    else
    { %>
                                            <div id="password_error" class="join-error" style="display:none;"></div>
<%  } %>
                                            
                                            <div class="homepage-field_holder">
                                                <div class="homepage-field_label last-child join-submit">Confirm password</div>
                                                <div class="text-holder">
                                                    <input class="homepage-field join_text-field" type="password" name="JoinConfirmPassword" id="JoinConfirmPassword" value="<%= Model.Join != null ? Model.Join.JoinConfirmPassword : "" %>" tabIndex="15" />
                                                </div>
                                            </div>
<%  if (IsJoinError("JoinConfirmPassword"))
    { %>
                                            <div id="confirm-password_error" class="join-error"><%= GetJoinError("JoinConfirmPassword")%></div>
<%  }
    else if (IsJoinError("ConfirmPassword"))
    { %>
                                            <div id="confirm-password_error" class="join-error"><%= GetJoinError("ConfirmPassword")%></div>
<%  }
    else
    { %>
                                            <div id="confirm-password_error" class="join-error" style="display:none;"></div>
<%  } %>
                                        </div>
<%  if (IsJoinError("AcceptTerms"))
    { %>
                                        <div id="terms-and-conditions_error" class="join-error"><%= GetJoinError("AcceptTerms")%></div>
<%  }
    else
    { %>
                                        <div id="terms-and-conditions_error" class="join-error" style="display:none;"></div>
<%  } %>
                                        <div class="join-terms">
                                            <input type="checkbox" name="AcceptTerms" id="AcceptTerms" <%= Model.Join != null && Model.AcceptTerms ? "checked=\"checked\"" : "" %> class="homepage-checkbox" tabIndex="16" />
                                            <div class="terms-and-conditions">I accept the <a href="<%= TermsUrl %>" target="_blank" class="homepage-link" tabIndex="17">terms and conditions</a></div>
                                            <div class="homepage-button join_tiny-button js_join"><input class="homepage-actual-button join-actual-button" id="join" name="join" value="" type="button" onclick="javascript:validateJoin();" tabIndex="18" /></div>
                                        </div>
                                        <input type="hidden" id="HomeJoin" name="join" value="" />
                                    </form>
                                </div>
                            </div>
                            <div class="join-bottom">&nbsp;</div>
                        </div>
                    </div>
                </div>
            </div>            
        </div>
        <div class="right-section login-section">
            <div class="section">
                <form id="LoginForm" method="post" action="<%= Context.GetClientUrl(true) %>">
                    <div class="login-holder">
                        <table>
                            <tr>
                                <td>
                                    <div class="loginid_holder">
                                        <div class="text-holder">
                                            <input type="text" id="LoginId" name="LoginId" value="<%= Model.Login != null ? Model.Login.LoginId : "" %>" class="homepage-field loginid-field loginid js_watermarked login-submit" data-watermark="Username/Email address" tabIndex="1" />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="password_holder">
                                        <div class="text-holder">
                                            <input type="password" id="Password" name="Password" class="homepage-field password-field password js_password-watermarked login-submit" data-watermark="Password" style="display:none;" tabIndex="2"/>
                                            <input type="text" id="DefaultPassword" name="DefaultPassword" class="homepage-field password-field password js_password-watermarked login-submit" data-watermark="Password" tabIndex="2"/>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="homepage-button login_button js_login"><input class="homepage-actual-button login-actual-button" type="button" id="login" onclick="javascript:validateLogin();" tabIndex="3" /></div>
                                </td>
                            </tr>
                            <tr class="bottom-row">
                                <td class="login-error-holder">
                                    <%  if (Model.Login != null && !ViewData.ModelState.IsValid) {
                                            var messages = ViewData.ModelState.GetErrorMessages();
                                            if (messages.Length > 0)
                                            { %>
                                                <div class="login-error"><%= messages[0] %></div>
                                    <%      }
                                        } %>
                                </td>
                                <td colspan="2">
                                    <div class="checkbox-holder"><input type="checkbox" name="RememberMe" id="RememberMe" value="false" class="homepage-checkbox" /><span>Remember me</span></div>
                                    <div class="link-holder"><%= Html.RouteRefLink("Forgot password?", LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.NewPassword, null, new { @class = "homepage-link", id = "ForgotPassword" })%></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <input type="hidden" id="HiddenLogin" name="login" value="" />
                </form>
            </div>
        </div>
    </div>
</div>

<%  if (Model.Join != null && !ViewData.ModelState.IsValid)
    { %>
<script type="text/javascript">
    (function($) {
        createJoinLayer();
    })(jQuery);
</script>
<%  } %>

