<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Content"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <% Page.Title = Model.JobAd.GetPageTitle(); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MetaTags" runat="server">
<%  if (Model.JobAd.Status == JobAdStatus.Closed)
    { %>
        <%= Html.MetaTag("robots", "noindex") %>
<%  } %>    
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.JobAds) %>
<%  if (!string.IsNullOrEmpty(Model.OrganisationCssFile))
    { %>
        <%= Html.StyleSheet(new StyleSheetReference(Model.OrganisationCssFile))%>
<%  } %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom)%>
        <%= Html.JavaScript(JavaScripts.JobAd) %>
        <%= Html.JavaScript(JavaScripts.JQueryFileUpload) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
<%  Model.Status.CurrentIndex = 1;
    Model.Status.TotalCount = 1;
    var jobTypes = Model.CurrentSearch == null
        ? Model.JobAd.Description.JobTypes.GetOrderedJobTypes()
        : Model.JobAd.Description.JobTypes.GetOrderedJobTypes(Model.CurrentSearch.Criteria.JobTypes); %>

    <div class="jobviewed">
<%  if (Model.CurrentSearch != null)
    { %>
            <div class="jobindex"><b>Job <%= Model.Status.CurrentIndex %></b> of <%= Model.Status.TotalCount %></div>
<%  } %>
        <div class="viewed">
            <div class="icon"></div>
            <div class="desc">This job has been viewed <b><%= Model.DistinctViewedCount %></b> times</div>
        </div>
    </div>

<%  if (Model.JobAd.Status == JobAdStatus.Closed)
    { %>
        <div class="closedjob">
            <div class="text">
                <span>This job is no longer advertised.</span>
                <span>Please <a class="link" href="<%= new ReadOnlyApplicationUrl(false, "~/search/jobs") %>">search for a new job</a> or see <a class="link" href="<%= CurrentMember == null ? JobAdsRoutes.Similar.GenerateUrl(new {jobAdId = Model.JobAd.Id.ToString()}) : JobAdsRoutes.Suggested.GenerateUrl() %>">other LinkMe jobs like this one</a></span>
            </div>
        </div>
<%  } %>
    
    <div class="main-content <%= Model.JobAd.Status == JobAdStatus.Closed ? "closed" : "" %>">
        <div class="titlebar <%= CurrentMember == null ? "notloggedin" : "" %>">
<%  if (Model.JobAd.IsNew())
    { %>
                <div class="isnew"></div>
<%  } %>
            <a class="previous <%= (Model.CurrentSearch == null || Model.Status.CurrentIndex == 1) ? "" : "active" %>" href="<%= (Model.CurrentSearch == null || Model.Status.CurrentIndex == 1) ? "" : Model.Status.PreviousJobAd.GenerateJobAdUrl().ToString() %>" <%= (Model.CurrentSearch == null || Model.Status.CurrentIndex == 1) ? "onclick='javascript:return false;'" : "" %>></a>
            <div class="jobtype <%= jobTypes.Count > 0 ? jobTypes[0] : JobTypes.None %>"></div>
            <div class="title"><h1 title="<%= Model.JobAd.Title %>"><%= Model.JobAd.Title %></h1></div>
            <a class="next <%= (Model.CurrentSearch == null || Model.Status.CurrentIndex == Model.Status.TotalCount) ? "" : "active" %>" href="<%= (Model.CurrentSearch == null || Model.Status.CurrentIndex == Model.Status.TotalCount) ? "" : Model.Status.NextJobAd.GenerateJobAdUrl().ToString() %>" <%= (Model.CurrentSearch == null || Model.Status.CurrentIndex == Model.Status.TotalCount) ? "onclick='javascript:return false;'" : "" %>></a>
            <div class="subtypes">
<%  foreach (var jt in new List<JobTypes> { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare })
    { %>
                <div class="icon <%= jt %> <%= jobTypes.Count > 0 && jobTypes[0].Equals(jt) ? "hide" : "" %> <%= jobTypes.Count > 1 && jobTypes.Skip(1).Contains(jt) ? "active" : "" %>"></div>
<%  } %>
                <span>Job-type</span>
            </div>
            <div class="viewed">
                <div class="icon <%= Model.JobAd.Applicant.HasViewed ? "active" : "" %>"></div>
                <div class="text">Viewed</div>
            </div>
            <div class="applied">
                <div class="icon <%= Model.JobAd.Applicant.HasApplied ? "active" : "" %>"></div>
                <div class="text">Applied</div>
            </div>
            <div class="notes" apinotesurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiNotes)) %>" apideletenoteurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiDeleteNote, new { noteId = Guid.Empty })) %>" apinewnoteurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiNewNote)) %>" apieditnoteurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiEditNote, new { noteId = Guid.Empty })) %>">
                <div class="icon"></div>
                <div class="text">Notes</div>
                <div class="count">(0)</div>
            </div>
            <div class="menupan note">
                <div class="add">
                    <div class="icon"></div>
                    <div class="text">Add note</div>
                </div>
                <div class="row empty">
                    <div class="title">
                        <div class="date"></div>
                        <div class="buttons">
                            <div class="icon edit"></div>
                            <div class="text edit">Edit</div>
                            <div class="icon delete"></div>
                            <div class="text delete">Delete</div>
                        </div>
                    </div>
                    <div class="content"></div>
                </div>
                <div class="editarea">
                    <%= Html.MultilineTextBoxField("Notes", "").WithLabel("").WithAttribute("row", "5").WithAttribute("maxlength", "500") %>
                    <div class="button save"></div>
                    <div class="button cancel eighty"></div>
                </div>
            </div>
        </div>
        <div class="bg">
<%  if (!string.IsNullOrEmpty(Model.OrganisationCssFile))
        Html.RenderPartial("JobAdViewCustomContent", Model.JobAd);
    else
        Html.RenderPartial("JobAdViewContent", Model.JobAd); %>

	        <div class="divider ad-and-ribbon"></div>
	        <div class="ribbons <%= Model.SuggestedJobs != null && Model.SuggestedJobs.Count > 0 ? "" : "halfsize" %> <%= Model.JobAd.Status == JobAdStatus.Closed ? "closed" : "" %>">
	            <div class="job-title">Apply for: <%= Model.JobAd.Title %></div>
<%  if (Model.SuggestedJobs != null && Model.SuggestedJobs.Count > 0)
    {
        if (CurrentMember == null)
        { %>
                <div class="right"><span>Other LinkMe jobs like this one</span></div>
<%      }
        else
        { %>
                <div class="right"><span>LinkMe jobs suggested for you</span></div>
<%      }
    } %>
	        </div>
			<div class="afterribbon">
<%  if (Model.JobAd.Status != JobAdStatus.Closed)
    { %>
				<div class="applyarea <%= Model.JobAd.Processing %>">
				    <a name="applyarea"></a>
<%      if (Model.JobAd.Processing == JobAdProcessing.AppliedForExternally)
            Html.RenderPartial("CareerOneApply", Model);
        else if (CurrentMember != null)
            Html.RenderPartial("LoggedInUserApply", Model);
        else if (Model.JobAd.Processing == JobAdProcessing.ManagedExternally)
            Html.RenderPartial("ManagedExternallyApply", Model);
        else if (Model.JobAd.Processing == JobAdProcessing.ManagedInternally)
            Html.RenderPartial("ManagedInternallyApply", Model);
        
        if (Model.JobAd.Processing != JobAdProcessing.ManagedInternally)
        { %>
                    <div class="popupsetting">* Please check your browser's popup blocker settings if you experience any problems.</div>
<%      } %>
				</div>
<%  }
    if (Model.SuggestedJobs != null && Model.SuggestedJobs.Count > 0)
    { %>
				<div class="divider"></div>
				<div class="suggestedjobs">
				    <% Html.RenderPartial("SuggestedJobs", Model.SuggestedJobs); %>
				</div>
<%  } %>
			</div>
        </div>

        <div class="bottom-bar"></div>
        
    </div>
	<div class="rightside <%= Model.JobAd.Status == JobAdStatus.Closed ? "closed" : "" %>">
<%  if (Model.JobAd.Status != JobAdStatus.Closed)
    { %>
		<div class="topbuttons">
			<div class="top-bar"></div>
			<div class="backbutton <% if (Model.CurrentSearch == null) { %>newsearch<% } %>" url="<%= Model.CurrentSearch == null ? Html.RouteRefUrl(SearchRoutes.Search) : Html.RouteRefUrl(SearchRoutes.Results) %>"></div>
			<div class="actions <%= CurrentMember == null ? "notloggedin" : "" %>" jobadid="<%= Model.JobAd.Id %>">
			    <div class="icon folder" title="Add this job to a folder"></div>
			    <div class="icon print" title="Print this job out"></div>
			    <div class="icon email" title="Email this job to friends"></div>
			    <div class="icon download" title="Download this job as a DOC" url="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.Download)) %>"></div>
			    <div class="icon hide" title="Hide this job from further searches" apiblockjobadsurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiBlockJobAds)) %>"></div>
			</div>
			<div class="emaillayer">
				<div class="top-bar"></div>
				<div class="bg">
					<div class="fields">
                        <%  var member = CurrentMember;
                            var fullName = member == null ? null : member.FullName;
                            var emailAddress = member == null ? null : member.GetBestEmailAddress().Address; %>
						<%= Html.TextBoxField("FromName", fullName)
                            .WithLabel("Your name")
                            .WithIsRequired().WithIsReadOnly(!fullName.IsNullOrEmpty())%>
						<%= Html.TextBoxField("FromEmailAddress", emailAddress)
                            .WithLabel("Your e-mail")
                            .WithIsRequired().WithIsReadOnly(!emailAddress.IsNullOrEmpty())%>
						<%= Html.TextBoxField("ToNames", "")
                            .WithLabel("Your friend's name(s)")
                            .WithIsRequired().WithAttribute("data-watermark", "e.g.Johnny, Mick")%>
						<%= Html.TextBoxField("ToEmailAddresses", "")
                            .WithLabel("Your friend's email address(es)")
                            .WithIsRequired().WithAttribute("data-watermark", "e.g. johnny@gmail.com, mich@hr.com.au")%>
						<div class="sendbutton" url="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiEmailJobAds)) %>"></div>
						<div class="cancelbutton"></div>
						<div class="validationerror">
							<div class="top-bar">
								<div class="leftcorner"></div>
								<div class="center"></div>
								<div class="rightcorner"></div>
							</div>
							<div class="bg">
								<div>
									<div class="alerticon"></div>
									<div class="prompt">There are some errors, please correct them below.</div>
								</div>
								<ul>
								</ul>
							</div>
							<div class="bottom-bar">
								<div class="leftcorner"></div>
								<div class="center"></div>
								<div class="rightcorner"></div>
							</div>
						</div>
						<div class="succ-info"></div>
					</div>
				</div>
				<div class="bottom-bar"></div>
			</div>
<%      if (CurrentMember != null)
        { %>
                <div class="menupan folder">
                    <div class="menuitem favourite disabled">
                        <div class="icon"></div>
                        <div class="text">Add to <span class="desc">My favourite jobs</span></div>
                    </div>
                    <div class="divider"></div>
                    <div class="divider white"></div>
<%          for (var i = 0; i < Model.Folders.PrivateFolders.Count; i++)
            { %>
                            <div class="menuitem folder" folderid="<%= Model.Folders.PrivateFolders[i].Id %>" url="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiAddJobAdsToFolder, new { folderId = Model.Folders.PrivateFolders[i].Id })) %>">
                                <div class="icon"></div>
                                <div class="text"><%= Model.Folders.PrivateFolders[i].Name%></div>
                            </div>
<%          } %>
                </div>
<%      } %>
			<div class="bg">
<%      if (Model.JobAd.Processing == JobAdProcessing.AppliedForExternally)
        {
                Html.RenderPartial("CareerOneApply", Model);
        }
        else
        { %>
                                <a class="applybutton" href="<%= Model.JobAd.GenerateJobAdUrl() %>#applyarea">Apply now</a>
<%      } %>
			</div>
			<div class="bottom-bar"></div>
		</div>
<%  }
    else
    { %>
        <div class="topbuttons">
	        <div class="actions <%= CurrentMember == null ? "notloggedin" : "" %>" jobadid="<%= Model.JobAd.Id %>"></div>
        </div>
<%  } %>

        <% Html.RenderPartial("GoogleVerticalAds"); %>
		
	</div>
    
    <div class="clearer"></div>
    
    <% Html.RenderPartial("GoogleHorizontalAds"); %>
	
	<div class="CallToActionOverlays" overlayfor="<%= Model.VisitorStatus.ShouldPrompt ? Model.VisitorStatus.Frequency.ToString() : "None" %>" viewedurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiViewed, new {jobAdId = Model.JobAd.Id})) %>">
	    <% Html.RenderPartial("CallToActionOverlays", Model); %>
	</div>
	
	<div class="learnmoredialog">
	    <div class="okbutton"></div>
	</div>
</asp:Content>