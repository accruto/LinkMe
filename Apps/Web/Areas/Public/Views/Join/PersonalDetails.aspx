<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<PersonalDetailsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Join"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.Join)%>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JoinFlow) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JQueryValidation) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="joinflow-header">Get Started: Create your profile</div>
	<div class="joinflow-steps"><% Html.RenderPartial("Steps", Model); %></div>

    <%= Html.ErrorSummary("There are some errors, please correct them below.")%>

<%  using (Html.RenderForm(Model.GetUrl(Context.GetClientUrl(true)), "PersonalDetailsForm"))
    { %>
	<div id="joinflow-content">
        <div class="step-content">
        
<%      if (!Model.IsUploadingResume)
        { %>
	        <div class="divider-2-1"></div>
	            
            <%= Html.TextBoxField(Model.Member, m => m.FirstName)
                .WithLabel("First name")
                .WithIsRequired() %>
                    
            <%= Html.TextBoxField(Model.Member, m => m.LastName)
                .WithLabel("Last name")
                .WithIsRequired() %>
                    
            <%= Html.CountryField(Model.Member, m => m.CountryId, Model.Reference.Countries)
                .WithLabel("Where do you live?")
                .WithIsRequired()
                .WithAttribute("size", "6")
                .WithHelpArea("Select your country of residence.") %>
                
            <%= Html.TextBoxField(Model.Member, m => m.Location)
                .WithLabel("Suburb / State / Postcode")
                .WithIsRequired()
                .WithHelpArea("Start typing your city, state, or postcode and we'll complete the details for you.") %>
                    
<%          if (Model.HasMember)
            { %>
            <%= Html.TextBoxField(Model.Member, m => m.EmailAddress)
                .WithLabel("Primary email")
                .WithIsRequired()
                .WithIsReadOnly() %>
<%          }
            else
            { %>
            <%= Html.TextBoxField(Model.Member, m => m.EmailAddress)
                .WithLabel("Primary email")
                .WithIsRequired() %>
<%          } %>
                
            <%= Html.TextBoxField(Model.Member, m => m.PhoneNumber)
                .WithLabel("Primary phone")
                .WithIsRequired()
                .WithHelpArea("Shown to employers on your Profile. Many employers prefer to talk directly to candidates rather than send an email.") %>

            <%= Html.RadioButtonsField(Model.Member, m => m.PhoneNumberType)
                .WithLabel(" ") %>

<%      }
        else
        { %>
            <div class="alert">
	            <span class="alert-text">Please review the information below and update if necessary.</span>
	            <span class="alert-text">The rest of your resume has been successfully extracted and has been added to your LinkMe Profile.</span>
            </div>
            <div class="divider-2-1"></div>
            <div class="contact">
	            <div class="edit"></div>
	            <div class="cancel"></div>
	            <div class="content">
		            <div class="view-mode">
			            <div class="left-table">
				            <table>
					            <tr class="name">
						            <td class="label">Your name</td>
						            <td class="text"><%= Model.Member.FirstName.CombineLastName(Model.Member.LastName)%></td>
					            </tr>
					            <tr class="location">
						            <td class="label"><div>Location</div><div class="alert-icon"></div></td>
						            <td class="text">
						                <span><%= (from c in Model.Reference.Countries where c.Id == Model.Member.CountryId select c).Single().Name %></span><br />
						                <span><%= Model.Member.Location %></span>
						            </td>
					            </tr>
					            <tr class="primary-email">
						            <td class="label">Primary email</td>
						            <td class="text">
						                <span><%= Model.Member.EmailAddress %></span>
							            <div class="username-callout"></div>
						            </td>
					            </tr>
					            <tr class="primary-phone">
						            <td class="label">Primary phone</td>
						            <td class="text">
							            <span><%= Model.Member.PhoneNumber %></span>
							            <span>(<%= Model.Member.PhoneNumberType %>)</span>
						            </td>
					            </tr>
				            </table>
			            </div>
			            <div class="auto-extraction">
				            <div class="icon"></div>
				            <span class="title">Automatic resume extraction</span>
<%          if (!Model.HasInitialMember)
            { %>
				                <span class="desc">Your location and contact details have</span>
				                <span class="desc">been pre-filled from your resume</span>
<%          }
            else
            { %>
				                <span class="desc">This information has been pre-filled</span>
				                <span class="desc">from your resume</span>
<%          } %>
			            </div>
		            </div>
		            <div class="edit-mode">
		                <%= Html.TextBoxField(Model.Member, m => m.FirstName)
		                    .WithLabel("First name")
		                    .WithIsRequired()
		                    .WithAttribute("reset-value", Model.Member.FirstName) %>
		                    
		                <%= Html.TextBoxField(Model.Member, m => m.LastName)
		                    .WithLabel("Last name")
		                    .WithIsRequired()
		                    .WithAttribute("reset-value", Model.Member.LastName) %>
		                    
		                <%= Html.CountryField(Model.Member, m => m.CountryId, Model.Reference.Countries)
		                    .WithLabel("Where do you live?")
		                    .WithIsRequired()
		                    .WithAttribute("size", "6")
		                    .WithAttribute("reset-value", Model.Member.CountryId.ToString())
		                    .WithHelpArea("Select your country of residence.") %>
		                    
		                <%= Html.TextBoxField(Model.Member, m => m.Location)
		                    .WithLabel("Suburb / State / Postcode")
		                    .WithIsRequired()
		                    .WithAttribute("reset-value", Model.Member.Location)
		                    .WithHelpArea("Start typing your city, state, or postcode and we'll complete the details for you.") %>

<%          if (Model.HasMember)
            { %>
				        <%= Html.TextBoxField(Model.Member, m => m.EmailAddress)
							.WithLabel("Primary email")
							.WithIsRequired()
							.WithIsReadOnly()
							.WithAttribute("reset-value", Model.Member.EmailAddress) %>
<%          }
            else
            { %>
                        <%= Html.TextBoxField(Model.Member, m => m.EmailAddress)
                            .WithLabel("Primary email")
                            .WithIsRequired()
                            .WithAttribute("reset-value", Model.Member.EmailAddress) %>
<%          } %>
		                <%= Html.TextBoxField(Model.Member, m => m.PhoneNumber)
		                    .WithLabel("Primary phone")
		                    .WithIsRequired()
		                    .WithAttribute("reset-value", Model.Member.PhoneNumber)
		                    .WithHelpArea("Shown to employers on your Profile. Many employers prefer to talk directly to candidates rather than send an email.") %>

		                <%= Html.RadioButtonsField(Model.Member, m => m.PhoneNumberType)
		                    .WithLabel(" ") %>
		                    
		                <input type="hidden" id="PrimaryPhoneType" value="<%= Model.Member.PhoneNumberType %>" />
		                
			            <div class="save-button"></div>
			            <div class="cancel-button"></div>
		            </div>
	            </div>
	            <div class="footer"></div>
            </div>
<%      } %>

<%      if (!Model.HasMember)
        { %>	        
	        <div class="divider-2-1"></div>
	        <div class="password-section">
	            <%= Html.PasswordField(Model.Passwords, m => m.Password)
                    .WithLabel("Account password")
                    .WithIsRequired()
                    .WithHelpArea("<span>Your password must be between 6 and 50 characters in length.</span><span>We recommend using a combination of upper and lower-case letters and numbers to increase security</span>") %>

	            <%= Html.PasswordField(Model.Passwords, m => m.ConfirmPassword)
                    .WithLabel("Confirm password")
                    .WithIsRequired()
                    .WithHelpArea("This must match your account password") %>
	        </div>
<%      } %>

            <% Html.RenderPartial("CommunityPersonalDetails", Model); %>

	        <div class="divider-2-1"></div>
	        
	        <div class="availability-details">
                <%= Html.RadioButtonsField(Model.Member, m => m.Status)
                    .WithLabel("Your availability")
                    .WithIsRequired()
                    .WithCssPrefix("availability")
                    .WithHelpArea("AvailableNow", "<span>Keep your work status up to date so employers know if you're looking for work or not.</span><span>We'll contact you occasionally to confirm your availability.</span>") %>
                    
	            <div class="availability-icon immediately-available" value="AvailableNow"></div>
	            <div class="availability-icon actively-looking" value="ActivelyLooking"></div>
	            <div class="availability-icon not-looking-but-happy-to-talk" value="OpenToOffers"></div>
	            <div class="availability-icon not-looking" value="NotLooking"></div>
	            <div class="availability-desc-outer">
					<div class="availability-desc"></div>
				</div>
	        </div>
	        <div class="divider-2-1"></div>
	        <div class="salary-section">
		        <table>
			        <tr class="salary">
				        <td class="label">Expected minimum salary
					        <div class="mandatory"></div>
					        <span class="desc more-space">Be realistic. Employers prefer candidates who have reasonable salary expectations</span>
				        </td>
				        <td class="edit-field">
					        <div class="total-remuneration">
					            <%= Html.RadioButtonsField(Model.Member, m => m.SalaryRate)
					                .With(Model.Reference.SalaryRates)
					                .WithId(SalaryRate.Year, "SalaryRateYear")
                                    .WithId(SalaryRate.Hour, "SalaryRateHour")
                                    .WithLabel("Total remuneration:")
                                    .WithLabel(SalaryRate.Year, "per annum")
                                    .WithLabel(SalaryRate.Hour, "per hour") %>
					        </div>
					        <div class="help-icon" helpfor="DesiredSalary"></div>
					        <div class="salary-range">
						        <div class="left-range">$0</div>
						        <div class="right-range">$250,000+</div>
					        </div>
					        <div class="salary-slider-bg"></div>
					        <div class="salary-slider"></div>
					        <div class="salary-slider-ruler"></div>
					        <div class="salary-desc"><center>Select a minimum salary</center></div>
					        <%= Html.TextBoxField(Model.Member, "SalaryLowerBound", m => m.SalaryLowerBound == null ? null : ((int) m.SalaryLowerBound.Value).ToString()) %>
					        <%= Html.CheckBoxField(Model.Member, "NotSalaryVisibility", m => !m.Visibility.IsFlagSet(ProfessionalVisibility.Salary)).WithLabelOnRight("Don't show to employers").WithCssPrefix("not-show-salary") %>
					        <div class="help-icon" helpfor="not-show-salary"></div>
				        </td>
			        </tr>
		        </table>
		        <div class="help-area" helpfor="DesiredSalary">
		            <div class="triangle"></div>
		            <div class="help-text">
		                <span>Include Super and the value of all bonuses and benefits.</span>
		                <span>Candidates who specify their salary expectations are more likely to be contacted by employers. There will always be an opportunity to negotiate this later.</span>
		            </div>
		        </div>
		        <div class="help-area" helpfor="not-show-salary">
		            <div class="triangle"></div>
		            <div class="help-text">
		                <span>You can choose not to disclose your salary expectations to employers; however, we recommend you keep this visible as employers often exclude candidates who don't indicate their salary expectations</span>
		            </div>
		        </div>
	        </div>
	        <div class="divider-2-1"></div>
	        <div class="other-details">
		        <table>
			        <tr class="privacy">
				        <td class="label">
					        <span class="title">Privacy settings</span>
					        <span class="red">We respect your privacy.</span>
					        <span>Read LinkMe's <%= Html.RouteRefLink("privacy statement", SupportRoutes.Privacy, null, new { @class = "privacy-link", target = "_blank" }) %>.</span>
					        <span class="red more-space">LinkMe will never reveal your email address to employers.</span>
				        </td>
				        <td class="edit-field">
							<div>
								<%= Html.CheckBoxField(Model.Member.Visibility, "ResumeVisibility", v => v.IsFlagSet(ProfessionalVisibility.Resume))
									.WithLabelOnRight("Allow employers to find my resume")
									.WithHelpArea("<span>Hiding your resume means employers won't be able to find your Profile and you won't be contacted about job opportunities.</span><span>We recommend displaying all your details to increase your chances of being contacted</span>") %>

								<div class="allow-to-see-my">
									<div class="down-arrow"></div>
									<span>Allow employers to see my:</span>
									<%= Html.CheckBoxField(Model.Member.Visibility, "NameVisibility", v => v.IsFlagSet(ProfessionalVisibility.Name)).WithLabelOnRight("Name")%>
									<%= Html.CheckBoxField(Model.Member.Visibility, "PhoneNumbersVisibility", v => v.IsFlagSet(ProfessionalVisibility.PhoneNumbers)).WithLabelOnRight("Phone numbers")%>
									<%= Html.CheckBoxField(Model.Member.Visibility, "RecentEmployersVisibility", v => v.IsFlagSet(ProfessionalVisibility.RecentEmployers)).WithLabelOnRight("Current & previous employers")%>
								</div>
							</div>
				        </td>
			        </tr>
<%  if (!Model.HasMember)
    { %>	        
			        <tr class="t-and-c">
				        <td class="label">Terms and conditions
					        <div class="mandatory"></div>
				        </td>
				        <td class="edit-field">
				            <%= Html.CheckBoxField(Model, "AcceptTerms", m => m.Passwords.AcceptTerms).WithLabelOnRight("I accept the terms and conditions") %>
				        </td>
			        </tr>
<%  } %>			        
		        </table>
				<div class="button-holder">
					<div class="<%if (!Model.IsUploadingResume) { %>next-button<% } else { %>confirm-button<% } %>"></div>
					<div class="button-spliter"></div>
					<a class="back-link" href="<%= Html.RouteRefUrl(JoinRoutes.Join) %>">Back to <%if (!Model.IsUploadingResume) { %>create my profile manually<% } else { %>upload your resume<% } %></a>
					<div class="savefirst">Please Save or Cancel your current changes before moving to the next step.</div>
				</div>
	        </div>
        </div>

        <script language="javascript" type="text/javascript">
	        var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialPostalMatches) %>" + "?countryId=1&location=";
	        var termsUrl = "<%= Html.RouteRefUrl(SupportRoutes.Terms) %>"

	        var urls = {
	            Join: '<%= Html.RouteRefUrl(JoinRoutes.Join) %>',
	            PersonalDetails: '<%= Html.RouteRefUrl(JoinRoutes.PersonalDetails) %>',
	            JobDetails: '<%= Html.RouteRefUrl(JoinRoutes.JobDetails) %>',
	            Activate: '<%= Html.RouteRefUrl(JoinRoutes.Activate) %>'
	        };
	        
	        $(document).ready(function() {
		        initScriptForStep("PersonalDetails", urls);
	        });
        </script>

	</div>

<%  } %>
    
</asp:Content>

