<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Web.Areas.Members.Views.Profiles.Profile" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Views"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.CandidateProfile)%>
		<%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <% Html.RenderPartial("CandidateProfile.js", Model); %>
    
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
		<%= Html.JavaScript(JavaScripts.JQueryCustom) %>		
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.CandidateProfile) %>
        <%= Html.JavaScript(JavaScripts.JQueryFileUpload) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <div class="profile-header">See how employers see me</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Candidate site</li>
        <li><%= Html.RouteRefLink("See how employers see me", ProfilesRoutes.Profile) %></li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="completion">
		<div class="bg">
			<div class="left-edge"></div>
			<div class="middle"></div>
			<div class="right-edge"></div>
		</div>
		<div class="fg">
			<span>Profile completion</span>
			<div class="progressbar">
				<div class="bg"></div>
				<div class="fg"></div>
				<div class="mask"></div>
			</div>
			<span class="progressbar-text"></span>
		</div>
    </div>
	<div class="resumeage">
		<div class="top-bar"></div>
		<div class="bg">
			<span class="title"></span>
			<div class="icon"></div>
			<div class="setcurrent"><a href url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiSetCurrent) %>">Make my resume current</a></div>			
			<div class="help-setcurrent"></div>
		</div>
		<div class="setcurrent-hint">
			<div class="top-bar"></div>
			<div class="bg">
				<div class="fg">
					<p>Updating your resume is the best way to improve your chances of being found by employers. Update your resume now to ensure that your recent skills and experiences are included in a keyword search.</p>
					<p>If your resume is up to date then mark it as current, as this will also improve your appearance in results. <a href class="hide-setcurrent-hint">Hide</a></p>
				</div>
			</div>
		</div>
	</div>
	<div class="profile-incomplete">
		<span class="title">Your profile is missing important information. Please complete the following sections.</span>
		<div class="list">
			<ul>
			</ul>
		</div>
	</div>
	<div class="user-action">
		<div class="top-bar"></div>
		<div class="bg">
			<div class="icon view" title="preview resume"></div>
			<div class="icon replace" title="upload new resume"></div>
			<div class="icon email" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiSendResume) %>" title="email resume"></div>
			<div class="icon print" title="print resume"></div>
			<div class="icon doc" url="<%= Html.RouteRefUrl(ProfilesRoutes.Download) %>" title="download resume"></div>
		</div>
	</div>
    <div class="six-section" profileUrl="<%= Html.RouteRefUrl(ProfilesRoutes.Profile) %>" loginUrl="<%= Context.GetLoginUrl(ProfilesRoutes.Profile) %>">
<%  for (var index = 0; index < 6; index++)
    { %>
            <div class="section" url="<%= GetTabs(index).TabUrl%>">
                <div class="divider-vline left"></div>
				<div class="divider-vline left light"></div>
                <div class="divider-hline gray"></div>
                <div class="divider-hline light"></div>
                <div class="bg"></div>
				<div class="divider-vline right light"></div>
                <div class="divider-vline right"></div>
                <div class="fg">
					<div class="icon-holder">
						<div class="section-icon"></div>
					</div>
                    <span class="section-title"><%= GetTabs(index).TabName %></span>
                    <span class="section-desc"><%= GetTabs(index).TabDesc %></span>
					<div class="incomplete-icon"></div>
                </div>
            </div>
<%  } %>
		<div class="current">
			<div class="divider-vline"></div>
			<div class="divider-vline light"></div>
			<div class="divider-hline"></div>
			<div class="divider-hline light"></div>
			<div class="bg"></div>
			<div class="divider-hline bottom light"></div>
			<div class="divider-hline bottom"></div>
			<div class="right-arrow"></div>
		</div>
    </div>
	<div class="set-visibility">
		<div class="top-bar"></div>
		<div class="bg">
			<div class="title">Show my profile</div>
			<div class="text-on <%= Model.Visibility.ShowResume ? "active" : "" %>">ON</div>
			<div class="switch-icon <%= Model.Visibility.ShowResume ? "on" : "off" %>"></div>
			<div class="text-off <%= Model.Visibility.ShowResume ? "" : "active" %>">OFF</div>
		</div>
		<div class="bottom-bar"></div>
		<div class="setup-icon"></div>
	</div>
    <div class="visibility">
		<div class="top-bar"></div>
		<div class="bg">
			<div class="fg">
				<span class="title">My visibility</span>
				<span class="desc">Control what employers and other members see about you and how they communicate with you.</span>
				<span class="desc red">LinkMe will never reveal your personal address or email address to employers unless you actually apply for a job they have advertised. See our <%= Html.RouteRefLink("privacy statement", SupportRoutes.Privacy, null, new { target = "_blank" }) %> for more details.</span>
				<span class="title">Allow employers to see my:</span>
				<%= Html.CheckBoxField(Model.Visibility, m => m.ShowName).WithLabelOnRight("Name")%>
				<%= Html.CheckBoxField(Model.Visibility, m => m.ShowPhoneNumbers).WithLabelOnRight("Phone numbers")%>
				<%= Html.CheckBoxField(Model.Visibility, m => m.ShowProfilePhoto).WithLabelOnRight("Photo")%>
				<%= Html.CheckBoxField(Model.Visibility, m => m.ShowRecentEmployers).WithLabelOnRight("Current & previous employers")%>
				<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiVisibility) %>"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="bottom-bar"></div>
	</div>
	<div class="upload-layer">
		<div class="upload-prompt">
			<span>When you upload your resume, LinkMe converts it to a format that employers can search. Once it has been uploaded, please ensure that all information has been converted properly.</span>
			<span class="red">This action will replace your current LinkMe resume and cannot be undone! </span>
			<div class="textbox-left"></div>
			<div class="textbox-bg"><input type="text" id="ResumePath" value="Choose file ..." readonly/></div>
			<div class="textbox-right"></div>
		    <div class="browse-holder">
		        <form id="resumeupload" method="post" enctype="multipart/form-data" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiUploadResume) %>" parseUrl="<%= Html.RouteRefUrl(ProfilesRoutes.ApiParseResume) %>">
			        <label class="fileinput-button browse-button">
				        <span></span>
				        <input type="file" name="file" multiple />
			        </label>
		        </form>
		    </div>
			<div class="format">
				<span><b>Accepts: </b>DOC, DOCX, RTF, PDF, TXT, HTML</span>
				<span><b>Max size: </b>2 MB</span>
			</div>
		    <span class="resume-tips">Resume tips</span>
			<div class="button-holder">
				<div class="upload"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="divider"></div>
		<div class="uploading-and-parsing">
			<div class="prompt-text">
				<span>Please wait while we upload your resume ...</span>
			</div>
			<div class="progress-bar-holder">
				<div class="filename">Upload progress</div>
				<div class="check-icon"></div>
				<div class="uploadprogressbar">
					<div class="bg"></div>
					<div class="fg"></div>
					<div class="mask"></div>
				</div>
				<span class="percent"></span>
			</div>
			<div class="button-holder">
				<div class="ok-button"></div>
			</div>
		</div>
		<div class="auto-extraction">
			<img class="icon" />
			<span>Automatic extraction...</span>
		</div>
		<div class="errorMsg">
			<span>Error</span>
		</div>
	</div>
	<div class="upload-tips-layer">
		<div class="section-title">
			<h1>Resume tips</h1>
		</div>
		
		<div class="section-content">
			<p>These tips come directly from recruitment industry sources to help improve your resume and make 
			employers and recruiters want to read it.</p>

			<ul>
				<li><strong>Sequence:</strong> Ensure your resume is methodical and in sequence.  If the reader finds it too hard to 
				follow (eg unexplainable date gaps, or otherwise incoherent), they will stop reading.
				</li>
				<li><strong>Targeted:</strong> Your resume should be targeted to the role you are applying for. This may result in 
				you having several different versions of your resume specific to each job you apply for.
				</li>
				<li><strong>Abbreviations:</strong> Avoid them as not everyone knows what they mean.
				</li>
				<li><strong>Buzzwords:</strong> Are okay only if they are relevant to the role and industry you are applying for.
				</li>
				<li><strong>Cohesive:</strong> Get someone else to read your resume (family, friends etc.).  It needs to flow, 
				make sense and should be concise.
				</li>
				<li><strong>Page limit:</strong> Only include necessary information and try to limit your resume to 4 pages.
				More information can be provided in an interview.</li>
				<li><strong>Join:</strong> Thousands of employers and recruiters use LinkMe everyday to find resumes 
				like yours to fill the roles they are recruiting for.</li>
				<li><strong>Stand out:</strong> Use job titles that encapsulate the industry and position you have had 
				(e.g. Marketing Manager, Marketing Assistant), and keep your LinkMe resume up to date.  When you update it, 
				we tag it with the word NEW or UPDATED so it stands out for employers when they search.</li>
			</ul>
			
			<p>For more help on your resume and career, see our <a href="<%= Html.RouteRefUrl(ResourcesRoutes.Resources) %>" target="_blank">Resources</a> page.</p>
		</div>
	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("ValidationSummary"); %>
	<form id="candidateProfile">
		<fieldset>
			<div class="lastupdate"></div>
			<div class="succ-info">
				<div class="top-bar"></div>
				<div class="bg">
					<div class="fg">
						<div class="icon"></div>
						<div class="info">
							<span class="text"></span>
							<span class="text red"></span>
						</div>
						<div class="close-icon"></div>
					</div>
				</div>
				<div class="bottom-bar"></div>
			</div>
			<div class="err-info">
				<div class="top-bar"></div>
				<div class="bg">
					<div class="fg">
						<div class="icon"></div>
						<div class="info">
							<span class="text"></span>
						</div>
						<div class="close-icon"></div>
					</div>
				</div>
				<div class="bottom-bar"></div>
			</div>
			<div id="results"></div>
			<div id="preview">
				<div class="icon-holder">
					<div class="icon print"></div>
					<div class="icon back"></div>
				</div>
				<div class="loading"><img />Loading...</div>
				<% for (var i = 0; i < 6; i++) { %>
					<div class="resume-part" partType="<%= GetTabs(i).TabUrl%>">
					</div>
				<% } %>
			</div>
		</fieldset>
	</form>
	
    <script type="text/javascript">
<%  var succInfoType = SuccInfoType;
    if (succInfoType == null)
    { %>
        var succInfoType = null;
<%  }
    else
    { %>
        var succInfoType = "<%= succInfoType %>";
<%  } %>
    </script>
	
</asp:Content>

