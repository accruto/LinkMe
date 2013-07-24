<%@ Import namespace="LinkMe.Web.Manager.Navigation"%>
<%@ Import namespace="LinkMe.Web"%>
<%@ Import namespace="LinkMe.Web.Employers"%>
<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Login.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Login" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="wc" Namespace="LinkMe.Apps.Asp.UI" Assembly="LinkMe.Apps.Asp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Import namespace="LinkMe.Web.UI.Unregistered"%>

<wc:SecondaryForm ID="frmLogin" Method="post" runat="server">
<div class="login_ascx forms_v2">
	<div class="login-box">
	    <div class="login-box-content">
		    <asp:placeholder id="phLoginMsg" runat="server" visible="false">
			    <div class="login-failure">
				    <asp:Label id="lblLoginMsg" runat="server" width="100%" />
			    </div>
		    </asp:placeholder>

            <fieldset>
                <div class="username_textbox_field textbox_field field">
                    <label>Username</label>
                    <div class="username_textbox_control textbox_control control">
                        <asp:TextBox id="txtUserId" runat="server" ValidationGroup="<%= LoginFormValidationGroup %>" TabIndex="11" CssClass="username_textbox textbox" MaxLength="100" />
                    </div>
                </div>
                <div class="password_textbox_field textbox_field field">
                    <label>Password</label>
                    <div class="password_textbox_control textbox_control control">
                        <asp:TextBox id="txtPassword" runat="server" ValidationGroup="<%= LoginFormValidationGroup %>" TabIndex="12" CssClass="password_textbox textbox" MaxLength="55" textmode="Password" EnableViewState="False" />
                    </div>
                </div>
                
		        <asp:Panel ID="pnlLoginFields" runat="server" DefaultButton="btnLogin">            		
		            <div class="layout_control control">
			            <div class="sublayout_control control">
			                <div class="rememberme_checkbox_control checkbox_control control">
			                    <asp:checkbox id="chkLoginPersist" CssClass="rememberme_checkbox checkbox" runat="server" TabIndex="13" Text="Remember me" />
			                </div>
    			            <div class="forgotpassword_link_control link_control control">
	    		                <a id="lnkForgotPassword" class="forgotpassword_link link" href="<%=ForgotPasswordUrl%>">Forgot password?</a>
		    	            </div>
		    	        </div>
		                <div class="login_button_control button_control control">
			                <asp:Button id="btnLogin" CssClass="login_button button" runat="server" ValidationGroup="<%= LoginFormValidationGroup %>"  TabIndex="14" ToolTip="Log In" /><br />
		                </div>    		        
		            </div>
		        </asp:Panel>		        
            </fieldset>		    
    		
		    <asp:PlaceHolder ID="phJoin" runat="server">
		        <div class="login-join-links">
		            Join as <a href="<%= MemberJoinUrl %>" title="Member LinkMe" name="Login/MemberJoin">Member</a>
		            or <a href="<%= EmployerJoinUrl %>" title="Employer Join" name="Login/EmployerJoin">Employer</a>
		        </div>
		    </asp:PlaceHolder>
		</div>
	</div>
</div>

	<%= FocusScript %>
</wc:SecondaryForm>