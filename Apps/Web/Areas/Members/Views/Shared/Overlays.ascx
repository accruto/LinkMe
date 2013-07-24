<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<div class="overlays">
    <div class="overlay loading">
        <div class="icon"></div>
    </div>
    <div class="overlay apierror">
        <div class="text">
            <span>Your search couldn't be processed.</span>
        </div>
        <div class="link">
            <span class="tryagain">Try again</span> or <span class="close">Close</span>
        </div>
    </div>
    <div class="overlay tips">
        <div class="titlebar">
            <div class="title">Search tips</div>
            <div class="icon close"></div>
        </div>
        <div class="text">
            <ul>
                <li><b>Synonyms: </b>We've identified similar job titles and included them in your search. eg CEO also looks for Chief Executive Officer, C.E.O.</li>
                <li><b>Boolean expressions</b>
                    <ul>
                        <li><b>AND: </b>Searches for jobs containing all words you have entered</li>
                        <li><b>OR: </b>Searches for job containing one of the words your have entered eg Mechanical OR Autocad returns resume with either of these words.</li>
                        <li><b>DOUBLE QUOTES: </b>Using "" will match your exact phrase. eg "Mechanical Engineer" returns resumes with Mechanical Engineer as a phrase.</li>
                    </ul>
                </li>
                <li><b>Location: </b>Will search for jobs within a 50km radius of your specified location unless you specify otherwise using advanced search</li>
                <li><b>Advanced search: </b>Allows you to refine your search criteria, including salary information, specific industries or job types.</li>
            </ul>
        </div>
    </div>
    <% if (CurrentMember == null) { %>
        <div class="overlay login">
            <div class="text">
                <span>You need to be logged in to access this feature.</span>
                <span><%= Html.LoginLink("Log in", SearchRoutes.Results) %> to your account, or if you're not a LinkMe member, <%= Html.RouteRefLink("join now", JoinRoutes.Join, new { returnUrl = Html.RouteRefUrl(SearchRoutes.Results) })%>.</span>
                <div class="button cancel gray"></div>
            </div>
        </div>
    <% } %>
    <div class="overlay renamefolder">
        <div class="titlebar">
            <div class="title">Rename</div>
            <div class="icon close"></div>
        </div>
        <%= Html.TextBoxField("NewFolderName", "").WithLabel("New name") %>
        <div class="mandatory"></div>
        <div class="prompt">
            <div class="icon"></div>
            <div class="text">Folder name already exists. Please</div>
        </div>
        <div class="buttons">
            <div class="button renamefolder"></div>
            <div class="button cancel gray"></div>
        </div>
    </div>
    <div class="overlay emptyfolder">
        <div class="titlebar">
            <div class="title">Empty</div>
            <div class="icon close"></div>
        </div>
        <div class="text">
            <span>Are you sure you want to empty '<span class="name"></span>' folder?</span>
            <span>All <span class="count"></span> job<span class="plural"></span> will be removed.</span>
        </div>
        <div class="buttons">
            <div class="button emptyfolder"></div>
            <div class="button cancel gray"></div>
        </div>
    </div>
    <% if (Model.ListType == JobAdListType.SearchResult) { %>
        <div class="overlay savesearch">
            <div class="titlebar">
                <div class="title">Save as a favourite search</div>
                <div class="icon close"></div>
            </div>
            <%= Html.TextBoxField("SearchName", "").WithLabel("Save search as:")%>
            <div class="icon"></div>
            <%= Html.CheckBoxField("Email", true).WithLabelOnRight("Email me updates to these results")%>
            <div class="prompt">
                <div class="icon"></div>
                <div class="text"></div>
            </div>
            <div class="buttons">
                <div class="button savesearch"></div>
                <div class="button cancel gray"></div>
            </div>
        </div>
        <div class="overlay emailalert">
            <div class="titlebar">
                <div class="title">Email me similar jobs</div>
                <div class="icon close"></div>
            </div>
            <%= Html.TextBoxField("SearchName", "").WithLabel("Save email alert as:")%>
            <div class="icon"></div>
            <div class="text">
            <%  var member = CurrentMember;
                var emailAddress = member == null ? null : member.GetBestEmailAddress().Address; %>
                <span>Alerts will be sent to <%= emailAddress%>.</span>
                <span class="gray">To change where alerts are sent, you need to change the contact email address in your Account.</span>
            </div>
            <div class="prompt">
                <div class="icon"></div>
                <div class="text"></div>
            </div>
            <div class="buttons">
                <div class="button createemailalert"></div>
                <div class="button cancel gray"></div>
            </div>
        </div>
    <% } %>
</div>