<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="SampleJoin.aspx.cs" Inherits="LinkMe.Web.Landing.SampleJoin" MasterPageFile="~/master/SiteMasterPage.master" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <p>
        The dotted box below holds a sample external join form.
    </p>
    <p>
        It can be called with a pcode parameter, like any other page, but it will not put in a hardcoded
        pcode.
    </p>
    <p>
        You shouldn't need to change this markup, but if you do, you must:
    </p>
    <ul>
        <li>Ensure the <code>form</code> element is present;</li>
        <li>Ensure all 5 <code>input</code> elements are present;</li>
        <li>Preserve at least the <var>name</var> attribute of each <code>input</code> element;</li>
    </ul>
    
    <div id="linkme_divJoinForm">
	    <!-- the crappy form name is only for the benefit of unit testing -->
	    <form id="Form1" method="POST" action="<%= ActionUrl %>">
	        <fieldset>
		        <div class="linkme_textbox_field linkme_field">
			        <label for="linkme_txtFirstName">First name</label>
			        <div class="linkme_textbox_control linkme_control">
			            <input type="text" class="linkme_textbox"
			                               id=   "linkme_txtFirstName"
			                               name        ="txtFirstName"
			            />
			        </div>
		        </div>
    		    
		        <div class="linkme_textbox_field linkme_field">
			        <label for="linkme_txtLastName">Last name</label>
			        <div class="linkme_textbox_control linkme_control">
			            <input type="text"
			                   class="linkme_textbox"
			                   id=   "linkme_txtLastName"
			                   name=        "txtLastName"
			            />
			        </div>
		        </div>
    		    
		        <div class="linkme_textbox_field linkme_field">
			        <label for="linkme_txtUsername">Email <span class="linkme_helptext">Your username</span></label>
			        <div class="linkme_textbox_control linkme_control">
			            <input type="text"
			                   class="linkme_textbox"
			                   id=   "linkme_txtUsername"
			                   name=        "txtUsername"
			            />
			        </div>
		        </div>
    		    
		        <div class="linkme_password_textbox_field linkme_textbox_field linkme_field">
			        <label for="linkme_txtPassword">Password</label>
			        <div class="linkme_password_textbox_control linkme_textbox_control linkme_control">
			            <input type="password"
			                   class="linkme_password_textbox linkme_textbox"
			                   id=   "linkme_txtPassword"
			                   name=        "txtPassword"
			            />
			        </div>
		        </div>
    		    
		        <div class="linkme_checkbox_field linkme_field">
		            <div class="linkme_checkbox_control linkeme_control">
			            <input type="checkbox"
			                   class="linkme_checkbox"
			                   id=   "linkme_chkAcceptTermsAndConditions"
			                   name=        "chkAcceptTermsAndConditions"
			                   value="true"
			            />
			            <label for="linkme_chkAcceptTermsAndConditions">I accept the <%=TermsAndConditionsPopup %></label>
			        </div>
		        </div>
    		</fieldset>
    		
		    <div class="linkme_join_button_control linkme_button_control linkme_control">
		        <input type="submit"
		               class="linkme_join_button linkme_button"
		               id=   "linkme_btnJoin"
		               name=        "btnJoin"
		               value="Join Now!"
		        />
		    </div>
	    </form>   
    </div>
	    
</asp:Content>