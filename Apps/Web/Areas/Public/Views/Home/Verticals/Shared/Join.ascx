<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.Join" %>

<div class="minijoin_ascx self-clearing forms_v2">
    <form id="JoinForm" method="post" action="<%= HomeUrl %>">
        <fieldset>
        
<%  if (IsJoinError(""))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("")%></div>
<%  } %>
        
            <div id="first-name" class="textbox_field field">
                <label for="FirstName">First name</label>
                <div class="textbox_control control">
                    <input type="text" id="FirstName" name="FirstName" value="<%= Model.Join != null ? Model.Join.FirstName : "" %>" class="textbox join-submit" />
                </div>
            </div>
            
<%  if (IsJoinError("FirstName"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("FirstName") %></div>
<%  } %>
            
            <div id="last-name" class="textbox_field field">
                <label for="LastName">Last name</label>
                <div class="textbox_control control">
                    <input type="text" id="LastName" name="LastName" value="<%= Model.Join != null ? Model.Join.LastName : "" %>" class="textbox join-submit" />
                </div>
            </div>
            
<%  if (IsJoinError("LastName"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("LastName") %></div>
<%  } %>
            
            <div id="user-name" class="email_textbox_field textbox_field field">
                <label for="EmailAddress">Email</label>
                <div class="email_textbox_control textbox_control control">
                    <input type="text" id="EmailAddress" name="EmailAddress" value="<%= Model.Join != null ? Model.Join.EmailAddress : "" %>" class="email_textbox textbox join-submit" />
                </div>
                <div class="helptext">
                    Your username
                </div>
            </div>
            
<%  if (IsJoinError("EmailAddress"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("EmailAddress")%></div>
<%  } %>
            
            <div class="password_textbox_field textbox_field field">
                <label for="JoinPassword">Password</label>
                <div class="password_textbox_control textbox_control control">
                    <input type="password" id="JoinPassword" name="JoinPassword" value="<%= Model.Join != null ? Model.Join.JoinPassword : "" %>" class="password_textbox textbox join-submit" />
                </div>
            </div>
            
<%  if (IsJoinError("JoinPassword"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("JoinPassword")%></div>
<%  } %>
            
            <div class="password_textbox_field textbox_field field">
                <label for="ConfirmJoinPassword">Confirm password</label>
                <div class="password_textbox_control textbox_control control">
                    <input type="password" id="JoinConfirmPassword" name="JoinConfirmPassword" value="<%= Model.Join != null ? Model.Join.JoinConfirmPassword : "" %>" class="password_textbox textbox join-submit" />
                </div>
            </div>
            
<%  if (IsJoinError("JoinConfirmPassword"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("JoinConfirmPassword")%></div>
<%  }
    else if (IsJoinError("ConfirmPassword"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("ConfirmPassword")%></div>
<%  } %>

            <div id="accept-terms-and-conditions" class="checkbox_field field">
                <div class="checkbox_control control">
                    <span class="checkbox">
                        <input type="checkbox" id="AcceptTerms" name="AcceptTerms" <%= Model.Join != null && Model.AcceptTerms ? "checked=\"checked\"" : "" %> value="<%= Model.Join != null && Model.AcceptTerms ? "true" : "false" %>" />
                        <label for="AcceptTerms">I accept the <a href="<%= TermsUrl %>" class="homepage-link" target="_blank">terms and conditions</a></label>
                    </span>
                </div>
            </div>
            
<%  if (IsJoinError("AcceptTerms"))
    { %>
            <div class="join-failure join-error"><%= GetJoinError("AcceptTerms")%></div>
<%  } %>
            
            <div id="join-button-area">
                <input type="button" id="join" onclick="javascript:validateJoin();" class="login-sidebar-button" />
            </div>
            <input type="hidden" name="join" value="" />
        </fieldset>
    </form>
</div>
