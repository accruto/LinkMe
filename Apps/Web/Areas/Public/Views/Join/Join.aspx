<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<JoinModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Join"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.Join)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.JQueryFileUpload) %>
        <%= Html.JavaScript(JavaScripts.JoinFlow) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="joinflow-header">Get Started: Create your profile</div>
	<div class="joinflow-steps"><% Html.RenderPartial("Steps", Model); %></div>
	
    <div class="step-content upload">
	    <div class="upload-resume">
		    <div class="radiobutton checked"></div>
		    <div class="upload-resume-txt">
			    <span class="upload-resume-title">I'll upload my resume to create my profile</span>
			    <span class="upload-resume-desc">(Formats allowed: DOC, DOCX, RTF, PDF, TXT, HTML)</span>
		    </div>
		    <div class="filebox-bg"><input type="textbox" id="ResumePath" class="mask" value="Choose file ..." readonly /></div>
		    <div>
		        <form id="fileupload" method="post" enctype="multipart/form-data">
			        <label class="fileinput-button browse-button">
				        <span></span>
				        <input type="file" name="file" multiple />
			        </label>
		        </form>
		    </div>
			<div class="nofileselected">Please select a file from your computer</div>
		    <div class="upload-button"></div>
		    <span class="upload-tips">Upload Tips</span>
	    </div>
	    <div class="divider left"></div>
	    <span class="or">or</span>
	    <div class="divider right"></div>
	    <div class="manually-create">
	        <div class="topbar"></div>
	        <div class="bg">
		        <div class="radiobutton"></div>
		        <div class="manually-create-txt">
			        <span class="manually-create-title">I'll create my profile manually</span>
			        <span class="manually-create-desc">Don't have a resume to hand? Don't worry - you can upload it later or create one now.</span>
			        <span class="manually-create-desc">Read <a class="helpful-tips" href="<%= Html.RouteRefUrl(ResourcesRoutes.Resources) %>"  target="_blank">helpful tips</a> on writing a great resume.</span>
		        </div>
		        <div class="createbutton"></div>
	        </div>
	        <div class="bottombar"></div>
	    </div>
	    <div class="vertical-divider"></div>
	    <div class="right-side-tip">
		    <div class="tips-title">3 reasons to upload</div>
		    <div class="tip">
			    <div class="tip-icon quickly"></div>
			    <span class="tip-title">Upload quickly</span>
			    <span class="tip-desc">Upload your resume to create your profile in less than 30 seconds.</span>
		    </div>
		    <div class="tip">
			    <div class="tip-icon be-viewed"></div>
			    <span class="tip-title">Be viewed by employers</span>
			    <span class="tip-desc">You'll appear in employer searches immediately. <span class="find-out-more">Find out more</span> about how your profile can help you find a job.</span>
		    </div>
		    <div class="tip">
			    <div class="tip-icon reuse"></div>
			    <span class="tip-title">Reuse</span>
			    <span class="tip-desc">You can also reuse this resume to apply for jobs.</span>
		    </div>
	    </div>
		<div class="upload-layer">
			<div class="upload-background"></div>
			<div class="uploading-and-parsing">
				<div class="prompt-text">
					<span>Please wait while we upload your resume ...</span>
				</div>
				<div class="progress-bar-holder">
					<div class="filename">johndoe_cv.doc</div>
					<div class="check-icon"></div>
					<div class="progress-bar-bg">
						<div class="progress-bar"></div>
						<div class="progress-bar-right"></div>
					</div>
					<span class="percent"></span>
				</div>
				<div class="button-holder">
					<div class="ok-button"></div>
					<div class="next-step">Take me to the next step</div>
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
		<div class="find-out-more-layer">
			<p><span>Upload your resume and let LinkMe do the work for you - you'll be found by the thousands of employers and recruiters searching for people to fill their job opportunities.</span></p>
			<p><span>Think about the key words and terms that employers might be using to look for someone with your education, work experience and skills, and ensure that these words appear in your resume. The more complete your resume is, the more likely you are to be found and contacted by employers.</span></p>
			<p><span>We recommend reviewing review your profile regularly to ensure that all your resume information has been entered correctly and is up to date.</span></p>
		</div>
		<div class="upload-tips-layer">
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
		<%  using (Html.RenderForm(Model.GetUrl(Context.GetClientUrl(true)), "JoinForm")) { %>
			<input type="hidden" id="FileReferenceId" name="FileReferenceId" value="" />
			<input type="hidden" id="ParsedResumeId" name="ParsedResumeId" value="" />
		<%  } %>                        
    </div>

    <script language="javascript" type="text/javascript">

        var urls = {
            Join: '<%= Html.RouteRefUrl(JoinRoutes.Join) %>',
            PersonalDetails: '<%= Html.RouteRefUrl(JoinRoutes.PersonalDetails) %>',
            JobDetails: '<%= Html.RouteRefUrl(JoinRoutes.JobDetails) %>',
            Activate: '<%= Html.RouteRefUrl(JoinRoutes.Activate) %>'
        };
	
	    $(document).ready(function() {
		    initScriptForStep("Join", urls);
	    });
    </script>
	
</asp:Content>

