<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Employers"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>

<div class="overlay-container forms_v2">

    <div id="save-search-overlay" class="custom-overlay shadow" style="display:none;">
	    <div class="overlay">
		    <div class="overlay-title">
		        <span class="overlay-title-text">Save search</span>
		        <span class="close-icon"></span>
		    </div>
		    <div class="overlay-content">
                <div class="overlay-text">
                    <div class="error-report">
                    </div>
		            <div class="textbox_field field">
		                <label>Save search as:</label>
		                <div class="textbox_control control">
		                    <input id="save-search-name" maxlength="265" class="save-search-txt" type="text" value="" size="50" />
		                </div>
		            </div>
		            <div class="overlay-type">
		                <input id="save-search-email" value="false" type="checkbox" />
		                <span class="save-search-email-txt">Email me updates to these results</span>
		            </div>
		            <div class="paragraph">
		                <span id="save-search-alert" style="display:none;">
		                    Alerts will be sent to <span class="email-address"></span>.
		                    <br />
		                    <span class="sub-text">To change where alerts are sent, you need to change the contact email address in your Account.</span>
		                </span>
		            </div>
		        </div>
		        <div class="buttons-holder">
		            <input class="cancel-button button" id="save-search-cancel" name="cancel" type="button" value="Cancel" />
		            <input class="save-new-search_button button" id="save-search" name="save-search" type="button" value="Save search" />
                </div>
		    </div>
	    </div>
    </div>

    <div id="save-alert-overlay" class="custom-overlay shadow" style="display:none;">
	    <div class="overlay">
		    <div class="overlay-title">
		        <span class="overlay-title-text">Email me updates to results</span>
		        <span class="close-icon"></span>
		    </div>
		    <div class="overlay-content">
		         <div class="overlay-text">
                    <div class="error-report">
                    </div>
                    <div class="textbox_field field">
                        <label>Save email alert as:</label>
                        <div class="textbox_control control">
                            <input id="save-alert-name" maxlength="265" class="save-alert-txt" type="text" value="" size="50" />
                        </div>
                    </div>
                    <div class="paragraph">
                        <span>
                            Alerts will be sent to <span class="email-address"></span>.
                            <br />
                            <span class="sub-text">To change where alerts are sent, you need to change the contact email address in your Account.</span>
                        </span>
                    </div>
		         </div>
		         <div class="buttons-holder">
		            <input class="cancel-button button" id="save-alert-cancel" name="cancel" type="button" value="Cancel" />
		            <input class="create-email-alert_button button" id="save-alert" name="save-alert" type="button" value="Email Updates" />
                 </div>
		    </div>
	    </div>
    </div>

    <div class="credits-overlay shadow" style="display:none;">
        <div class="overlay">
	        <div class="overlay-content">
	             <div class="overlay-text">
    	             <span id="credits-description"></span>
    	             <b>Unlocking costs 1 credit per candidate.</b><br/><br/>
    	             Unlocking allows you to view and save the candidate's full resume (including name, and current and previous employer), and see all contact details the candidate has provided.<br/><br/>
    	             Click 'OK' to proceed or 'Cancel' to return to the current page.<br/><br/>
    	             <input id="credits-reminder" class="unchecked" type="checkbox" /> Don't show me this reminder in future.
	             </div>
	             <div class="buttons-holder">
	                <input class="cancel-button button" id="credits-cancel" name="credits-cancel" type="button" value="Cancel" />
	                <input class="ok_button button" id="credits-ok" name="credits-ok" type="button" value="OK" />
                 </div>
	        </div>
        </div>
    </div>
    
	<div class="createemailalertprompt shadow" style="display:none">
	    <span class="overlay-arrow"></span>
		<div class="overlay">
	        <div class="overlay-title"><span class="overlay-title-text">Create new email alert</span> <span class="close-icon"></span></div>
			<div class="overlay-content">
				<div class="overlay-text">
					Click here once you've added keywords, location and filters to your search and are ready to create your email alert.
				</div>
			</div>
		</div>
	</div>
</div>

<div class="overlay-container">			                
    <div class="unlock-overlay shadow" style="display:none;">
        <span class="overlay-arrow"> &nbsp; </span>
        <div class="overlay">
            <div class="overlay-content">
                <div class="overlay-text">
                    <b>Unlocking costs 1 credit per candidate.</b><br/><br/>
                    Unlocking allows you to view and save the candidate's full resume (including name, and current and previous employer), and see all contact details the candidate has provided.<br/><br/>
                    Click 'OK' to proceed or 'Cancel' to return to the current page.<br/><br/>
                    <input id="unlock-reminder" class="unchecked" type="checkbox"/> Don't show me this reminder in future.							
                </div>
                <div class="buttons-holder">
                    <input class="cancel-button button" id="unlock-cancel" name="unlock-cancel" type="button" value="Cancel" />
                    <input class="ok_button button" id="unlock-ok" name="unlock-ok" type="button" value="Ok"/>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="overlay-container forms_v2">
    <div class="login-overlay shadow" style="display:none;">
        <div class="overlay">
	        <div class="overlay-content">
	             <div class="overlay-text">
	                <span>description</span>.
	                <br />
	                <br />
	                <%= Html.EmployerLoginLink("Log in", Context, new {id = "login-overlay-login"}) %>
	                to your account, or if you don't already have a login,
	                <%= Html.RouteRefLink("create", AccountsRoutes.Join, new {returnUrl = Context.GetClientUrl().ToString()}, new {id = "login-overlay-join"}) %>
	                a new account now.
	                <br />
	             </div>
	             <div class="buttons-holder">
	                <input class='cancel-button button' id='login-cancel' name='login-cancel' type='button' value='Cancel' />
                 </div>
	        </div>
        </div>
    </div>
</div>

<div class="overlay-container forms_v2">
    <div class="send-message-overlay shadow" style="display:none;">
        <div class="overlay">
	        <div class="overlay-title"><span class="overlay-title-text">Email candidate</span> <span class="close-icon"></span></div>
	        <div class="overlay-content">
	            <div class="overlay-text">
	                <div class="send-message-credits-alert" style="display: none;">
	                    <div>
	                        <span id="send-message-credits-desc"></span> Unlocking costs <b>1 credit</b> per candidate.
	                    </div>
	                    <div>
	                        Unlocking allows you to view and save the candidate's full resume (including name, and current and previous employer),
	                        and see all contact details the candidate has provided.
	                    </div>
	                    <div>
	                        Click 'Send' to proceed or 'Cancel' to return to the current page.
	                    </div>
	                </div>
	            </div>
	            <div class="email-container">
	                <div class="personalization-holder">
	                    <div class="heading">Personalisation fields</div>
	                    <div class="content">
	                        <span>Want to personalise the emails you send to candidates ? Just insert a field from the list below where you want the candidate's first/last name to appear and we 'll send out your emails with each candidate's details filled in.</span>
	                        <span>Note that candidates will not see the names of any of the other candidates you have sent this email to.</span>
	                    </div>
	                    <div class="personalization-field-list-holder">
	                        <select class="personalization-field-list">
	                            <option value="first">Candidate first name</option>
	                            <option value="last">Candidate last name</option>
	                        </select>
	                    </div>
	                    <div class="button-holder">
	                        <input class='insert_button button' id='insert-pf' name='insert-pf' type='button' value='Insert'/>
	                    </div>
	                </div>
	                <div class="email-holder">
	                    <div class="email-metadata-holder">
	                        <table class="email-metadata">
	                            <tr>
	                                <td>To:</td>
	                                <td class="toggleToField"><div class="toggle-link-holder"><a href="javascript:void(0);" class="toggle-link">More</a><span class="icon down-icon"></span></div><div id="sendMessageTo"></div></td>
	                            </tr>
	                            <tr>
	                                <td>&nbsp;</td>
	                                <td><input name="sendCopy" id="sendCopy" type="checkbox" checked="checked" /> Send me a copy</td>
	                            </tr>
	                            <tr>
	                                <td>From:</td>
	                                <td><input name="From" id="sendMessageFrom" type="text" /></td>
	                            </tr>
	                            <tr>
	                                <td>Subject:</td>
	                                <td><input name="Subject" id="sendMessageSubject" type="text" /></td>
	                            </tr>
	                            <tr>
	                                <td>&nbsp;</td>
	                                <td>
                                        <div class="attach_holder"><a href="javascript:void(0);" id="attach-link" onclick="displayBrowseOption();">Attach a file</a></div>
                                    </td>
	                            </tr>
	                            <tr>
	                                <td>&nbsp;</td>
	                                <td>
	                                    <div id="file_uploader" style="display:none;">
	                                        <form id="file_upload" method="POST" enctype="multipart/form-data">
                                                <input type="file" name="file" multiple>
                                                <!--button>Upload</button-->
                                                <!--div>Upload files</div-->
                                            </form>
                                        </div>
                                        <div id="files"></div>
                                    </td>
	                            </tr>
	                        </table>		                
	                    </div>            
	                    <textarea name="Body" id="sendMessageBody" cols="65" rows="15" class="tinymce"></textarea>
	                </div>
	            </div>	        
                <div class="buttons-holder"></div>
            </div>
        </div>
    </div>
</div>

<div class="overlay-container forms_v2">
    <div class="sbn-not-logged-in-overlay shadow" style="display:none">
        <div class="overlay">
	        <div class="overlay-content">
	             <div class="overlay-text">
                    This feature is only available to LinkMe clients with unlimited access.
                    <br />
                    Please <%= Html.EmployerLoginLink("Log in", Context, new { id = "sbn-not-logged-in-overlay-login" })%> to your account or call LinkMe on 1800 546563 for more information.
	             </div>
	             <div class="buttons-holder">
	                <input class="ok_button button" id="Button1" name="sbn-ok" type="button" value="OK">
                 </div>
	        </div>
        </div>
    </div>
    <div class="sbn-limit-credit-overlay shadow" style="display:none">
        <div class="overlay">
	        <div class="overlay-content">
	             <div class="overlay-text">
                    This feature is only available to LinkMe clients with unlimited access.
                    <br />
                    Please call LinkMe on 1800 546563 for more information.
	             </div>
	             <div class="buttons-holder">
	                <input class="ok_button button" id="Button2" name="sbn-ok" type="button" value="OK">
                 </div>
	        </div>
        </div>
    </div>
</div>

<div class="overlay-container forms_v2">
	<div class="rejectionMessageWarningOverlay shadow" style="display:none">
		<div class="overlay">
	        <div class="overlay-title"><span class="overlay-title-text">Send rejection e-mail</span> <span class="close-icon"></span></div>
			<div class="overlay-content">
				<div class="overlay-text">
					Some of the candidates you have selected did not apply directly for this role. You won't be able to send these candidates a rejection email. These candidates have been automatically removed from your selection.
					<br />
					<br />
					Click 'Ok' to proceed or 'Cancel' to adjust your selection manually.
				</div>
				<div class="buttons-holder">
                    <input class="cancel-button button" id="rmw-cancel" name="rmw-cancel" type="button" value="Cancel" />
                    <input class="ok_button button" id="rmw-ok" name="rmw-ok" type="button" value="Ok"/>
				</div>
			</div>
		</div>
	</div>
</div>

<div class="needhelpfindingcandidates">
    <div class="top"><div class="close"></div></div>
    <% if (CurrentEmployer == null) { %>
        <div class="middle">
            <%= Html.RouteRefLink("See how LinkMe works", LinkMe.Web.Areas.Employers.Routes.HomeRoutes.Home, new { ignorePreferred = "True" }, new { @class = "link video" })%>
            <%= Html.RouteRefLink("Find out more: Features and benefits", LinkMe.Web.Areas.Employers.Routes.HomeRoutes.Features, null, new { @class = "link features" })%>
        </div>    
    <% } %>
    <div class="bottom">
        <%= Html.RouteRefLink("See our plans and pricing", ProductsRoutes.NewOrder, null, new { @class = "link purchase" })%>
        <%= Html.RouteRefLink("Contact LinkMe", SupportRoutes.ContactUs, null, new { @class = "link contact" })%>
    </div>
</div>