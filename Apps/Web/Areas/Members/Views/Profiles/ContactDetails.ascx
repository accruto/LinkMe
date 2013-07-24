<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ContactDetailsModel>" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<script type="text/javascript">
    var contactDetailsMemberModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Member) %>;
	var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialPostalMatches) %>" + "?countryId=1&location=";
</script>

<div class="content">
	<div class="edit"></div>
	<div class="cancel"></div>
	<div class="view-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="field">
					<div class="photo">
						<div class="photo-bg"></div>
						<img class="photo-fg" />
					</div>
					<div class="name"></div>
					<div class="divider gray"></div>
					<div class="divider light"></div>
					<div class="location">
						<span class="title">Location</span>
						<span class="desc country"></span>
						<span class="desc suburb"></span>
					</div>
					<div class="primary-phone">
						<span class="title"></span>
						<span class="desc"></span>
					</div>
					<div class="secondary-phone">
						<span class="title"></span>
						<span class="desc"></span>
					</div>
					<div class="visa">
						<span class="title">Visa details</span>
						<span class="desc"></span>
					</div>
					<div class="divider gray"></div>
					<div class="divider light"></div>
					<div class="privacy">
						<span class="title red">The information below is not shown to employers</span>
						<div class="primary-email">
							<span class="title">Primary email</span>
							<span class="desc"></span>
						</div>
						<div class="secondary-email">
							<span class="title">Secondary email</span>
							<span class="desc"></span>
						</div>
						<div class="dob">
							<span class="title">Date of birth</span>
							<span class="desc"></span>
						</div>
						<div class="gender">
							<span class="title">Gender</span>
							<span class="desc"></span>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="footer-bar"></div>
	</div>
	<div class="edit-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="field">
					<label for="ResumePhoto">Upload your photo</label>
					<div class="photo_control control">
						<div class="photo-bg small">
							<img class="photo-fg small" url="<%= Html.RouteRefUrl(ProfilesRoutes.Photo) %>"></img>
						</div>						
						<div class="textbox-left"></div>
						<div class="textbox-bg">
							<input class="textbox" id="Photo" name="Photo" type="text" readonly="readonly" value="Choose file ..." />
						</div>
						<div class="textbox-right"></div>
						<div class="delete" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiRemovePhoto) %>"></div>
						<div class="upload"></div>
						<div>
							<form id="fileupload" method="post" enctype="multipart/form-data" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiUploadPhoto) %>">
								<label class="fileinput-button browse-button">
									<span></span>
									<input type="file" name="file" multiple />
								</label>
							</form>
						</div>
						<div class="help-area" helpfor="Photo">
						    <div class="triangle"></div>
						    <div class="help-text">
						        <span>Put your best face forward.</span>
						    </div>
						</div>
					</div>
				</div>
			    <div class="divider"></div>
			    <div class="divider light"></div>
<%  if (Model.CanEditContactDetails)
    { %>
			    <%= Html.TextBoxField(Model.Member, m => m.FirstName).WithLabel("First name").WithIsRequired() %>
			    <%= Html.TextBoxField(Model.Member, m => m.LastName).WithLabel("Last name").WithIsRequired() %>
<%  }
    else
    { %>
			    <%= Html.TextBoxField(Model.Member, m => m.FirstName).WithLabel("First name").WithIsReadOnly() %>
			    <%= Html.TextBoxField(Model.Member, m => m.LastName).WithLabel("Last name").WithIsReadOnly() %>
<%  } %>
			    <div class="divider"></div>
			    <div class="divider light"></div>
			    <%= Html.CountryField(Model.Member, m => m.CountryId, Model.Reference.Countries)
				    .WithLabel("Where do you live?")
				    .WithIsRequired()
				    .WithAttribute("size", "6")
				    .WithHelpArea("Select your country of residence.") %>
			    <%= Html.TextBoxField(Model.Member, m => m.Location)
				    .WithLabel("Suburb / State / Postcode")
				    .WithIsRequired()
				    .WithHelpArea("Start typing your city, state, or postcode and we'll complete the details for you.") %>
			</div>
		</div>
		<div class="footer-bar"></div>
		<div class="email-section">
			<div class="header-bar pink"></div>
			<div class="bg pink">
				<div class="fg">
<%  if (Model.CanEditContactDetails)
    { %>
					<%= Html.TextBoxField(Model.Member, m => m.EmailAddress)
					    .WithLabel("Primary email")
                        .WithIsRequired()
                        .WithHelpArea("The email address you used to register your account. If you change this email address, it will become your new LinkMe username")%>
					<%= Html.TextBoxField(Model.Member, m => m.SecondaryEmailAddress)
						.WithLabel("Secondary email")
						.WithHelpArea("Used in case your primary email address isn't working.") %>
<%  }
    else
    { %>
					<%= Html.TextBoxField(Model.Member, m => m.EmailAddress)
					    .WithLabel("Primary email")
                        .WithIsReadOnly()
					    .WithHelpArea("The email address you used to register your account.") %>
<%  } %>
				</div>
			</div>
			<div class="footer-bar pink"></div>
		</div>
		<div class="third-section">
			<div class="header-bar blue"></div>
			<div class="bg blue">
				<div class="fg">
					<%= Html.TextBoxField(Model.Member, m => m.PhoneNumber).WithLabel("Primary Phone").WithIsRequired().WithHelpArea("Shown to employers on your Profile. Many employers prefer to talk directly to candidates rather than send an email.")%>
					<%= Html.RadioButtonsField(Model.Member, m => m.PhoneNumberType)
					    .WithLabel(" ") %>
					<%= Html.TextBoxField(Model.Member, m => m.SecondaryPhoneNumber).WithLabel("Secondary Phone").WithHelpArea("Shown to employers on your Profile. We recommend you have two contact phone numbers to ensure employers can get in touch easily.")%>
					<%= Html.RadioButtonsField(Model.Member, m => m.SecondaryPhoneNumberType)
					    .WithLabel(" ")
					    .WithId(PhoneNumberType.Mobile, "SecondaryMobile")
                        .WithId(PhoneNumberType.Home, "SecondaryHome")
					    .WithId(PhoneNumberType.Work, "SecondaryWork") %>
					<div class="divider"></div>
					<div class="divider light"></div>
					<%= Html.TextBoxField(Model.Member, m => m.Citizenship).WithLabel("Citizenship").WithHelpArea("Citizenship", "List your citizenship details")%>
					<%= Html.RadioButtonsField(Model.Member, m => m.VisaStatus)
						.WithLabel(VisaStatus.Citizen, VisaStatus.Citizen.GetDisplayText())
						.WithLabel(VisaStatus.UnrestrictedWorkVisa, VisaStatus.UnrestrictedWorkVisa.GetDisplayText())
						.WithLabel(VisaStatus.RestrictedWorkVisa, VisaStatus.RestrictedWorkVisa.GetDisplayText())
						.WithLabel(VisaStatus.NoWorkVisa, VisaStatus.NoWorkVisa.GetDisplayText())
						.WithLabel(VisaStatus.NotApplicable, VisaStatus.NotApplicable.GetDisplayText())
						.WithLabel("Visa")
						.WithCssPrefix("Visa")
						.WithIsRequired()
						.WithHelpArea("NotApplicable", "Visa restrictions include being sponsored by a particular employer or being limited to a certain number of hours per week.") %>
					<%= Html.CheckBoxesField(Model.Member, m => m.EthnicStatus)
						.Without(EthnicStatus.None)
						.WithLabel(EthnicStatus.Aboriginal, EthnicStatus.Aboriginal.GetDisplayText())
						.WithLabel(EthnicStatus.TorresIslander, EthnicStatus.TorresIslander.GetDisplayText())
						.WithLabel("Show employers that you are...")
						.WithHelpArea("TorresIslander", "If these apply to you, we recommend you select the appropriate options as employers can specifically search for Indigenous candidates.") %>
				</div>
			</div>
			<div class="footer-bar blue"></div>
		</div>
		<div class="privacy-section">
			<div class="header-bar pink"></div>
			<div class="bg pink">
				<div class="fg">
					<div class="field">
						<span class="title red">The information below is for LinkMe purposes only. No information will be disclosed to employers.</span>
					</div>
                    <%= Html.ControlsField()
                        .WithLabel("Date of birth")
                        .Add(Html.DropDownListField(Model.Member, "DateOfBirthMonth", m => m.DateOfBirth == null ? (int?)null : m.DateOfBirth.Value.Month, Model.Reference.Months)
                            .WithText(m => m == null ? "" : m.Value.GetMonthDisplayText())
                            .WithCssPrefix("month").WithAttribute("size", "5"))
                        .Add(Html.DropDownListField(Model.Member, "DateOfBirthYear", m => m.DateOfBirth == null ? (int?)null : m.DateOfBirth.Value.Year, Model.Reference.Years)
                            .WithCssPrefix("year")
                            .WithLabel("")
                            .WithAttribute("size", "5"))
                        .WithIsRequired()
                        .WithHelpArea("DateOfBirthYear", "For our records only - never shown to employers.") %>
                    <%= Html.RadioButtonsField(Model.Member, m => m.Gender)
                        .WithLabel("Gender")
                        .WithIsRequired()
                        .WithHelpArea("Female", "For our records only - never shown to employers.") %>
					<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiContactDetails) %>"></div>
					<div class="cancel"></div>
				</div>
			</div>
			<div class="footer-bar pink"></div>
		</div>
	</div>
</div>
<div class="prompt-layer upload">
	<div>
		<span class="prompt-text">Do you want to replace your current photo?</span>
		<div class="yes"></div>
		<div class="cancel"></div>
	</div>
</div>
<div class="prompt-layer delete">
	<div>
		<span class="prompt-text">Are you sure you want to delete your current LinkMe photo?</span>
		<span class="prompt-text red bold">This action is not undoable!</span>
		<div class="delete"></div>
		<div class="cancel"></div>
	</div>
</div>