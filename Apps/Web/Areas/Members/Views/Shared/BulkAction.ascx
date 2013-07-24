<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="bulktick">
    <div class="checkbox"></div>
</div>
<div class="bulkaction">
    <div class="dropdown">
        <div class="icon"></div>
        <div class="text">Act on <span class="count"></span> selected job<span class="plural">s</span></div>
        <div class="vline"></div>
        <div class="arrow"></div>
    </div>
</div>

<div class="menupan bulk">
    <div class="menuitem parent folder" child=".menupan.folder">
        <div class="icon"></div>
        <div class="text">Add <span class="count"></span> to <span class="desc">My favourite jobs</span></div>
        <div class="arrow"></div>
    </div>
    <div class="divider"></div>
    <div class="divider white"></div>
    <div class="menuitem parent email" child=".menupan.email">
        <div class="icon"></div>
        <div class="text">Email <span class="count"></span> job<span class="plural">s</span></div>
        <div class="arrow"></div>
    </div>
    <div class="menuitem download" child="">
        <div class="icon"></div>
        <div class="text">Download <span class="count"></span> job<span class="plural">s</span> as <span class="type">ZIP</span></div>
    </div>
    <div class="divider"></div>
    <div class="divider white"></div>
    <div class="menuitem parent note" child=".menupan.note">
        <div class="icon"></div>
        <div class="text">Bulk add <span class="count"></span> note<span class="plural">s</span></div>
        <div class="arrow"></div>
    </div>
    <div class="divider"></div>
    <div class="divider white"></div>
    <% if (Model.ListType == JobAdListType.BlockList) { %>
        <div class="menuitem restore" child="">
            <div class="icon"></div>
            <div class="text">Restore <span class="count"></span> job<span class="plural">s</span></div>
        </div>
    <% } else { %>
        <div class="menuitem hide" child="">
            <div class="icon"></div>
            <div class="text">Hide <span class="count"></span> job<span class="plural">s</span> from future searches</div>
        </div>
    <% } %>
</div>

<div class="menupan folder">
    <div class="menuitem favourite disabled">
        <div class="icon"></div>
        <div class="text">Add to <span class="desc">My favourite jobs</span></div>
    </div>
    <div class="divider"></div>
    <div class="divider white"></div>
    <% if (CurrentMember != null) { %>
        <%for (var i = 0; i < Model.Folders.PrivateFolders.Count; i++)
          { %>
            <div class="menuitem folder" folderid="<%= Model.Folders.PrivateFolders[i].Id %>" url="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiAddJobAdsToFolder, new { folderId = Model.Folders.PrivateFolders[i].Id })) %>">
                <div class="icon"></div>
                <div class="text"><%= Model.Folders.PrivateFolders[i].Name%></div>
            </div>
        <% } %>
    <% } %>
    <% if (Model.ListType == JobAdListType.Folder) { %>
        <div class="divider"></div>
        <div class="divider white"></div>
        <div class="menuitem removefromfolder" url="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiRemoveJobAdsFromFolder, new { folderId = Guid.Empty } )) %>">
            <div class="icon"></div>
            <div class="text">Remove from this folder</div>
        </div>
    <% } %>
</div>
<div class="menupan email">
    <div class="title">Email <span class="count"></span> job<span class="plural">s</span></div>
    <div class="divider"></div>
    <div class="divider white"></div>
    <div class="desc"><span>Email </span><span class="jobtitle"></span><span class="thisjob"></span><span> to yourself, another person, friend or colleague.</span></div>
    <div class="desc">All fields are required.</div>
    <div class="desc">Use commas to e-mail this job to more than one person.</div>
    <div class="divider"></div>
    <div class="divider white"></div>
	<div class="validationerror">
		<div class="bg">
			<div class="prompt">There are some errors, please correct them below.</div>
			<ul></ul>
		</div>
        <div class="divider"></div>
        <div class="divider white"></div>
	</div>
    <%  var member = CurrentMember;
        var fullName = member == null ? null : member.FullName;
        var emailAddress = member == null ? null : member.GetBestEmailAddress().Address; %>
	<%= Html.TextBoxField("FromName", fullName)
        .WithLabel("Your name")
        .WithIsRequired().WithIsReadOnly(!string.IsNullOrEmpty(fullName)).WithAttribute("data-watermark", "John Doe") %>
	<%= Html.TextBoxField("FromEmailAddress", emailAddress)
        .WithLabel("Your e-mail")
        .WithIsRequired().WithIsReadOnly(!string.IsNullOrEmpty(emailAddress)).WithAttribute("data-watermark", "johndoe_cv@yahoo.com.au") %>
	<%= Html.TextBoxField("ToNames", "")
        .WithLabel("Your friend's name(s)")
        .WithIsRequired().WithAttribute("data-watermark", "e.g. Johnny, Mick")%>
	<%= Html.TextBoxField("ToEmailAddresses", "")
        .WithLabel("Your friend's email address(es)")
        .WithIsRequired().WithAttribute("data-watermark", "e.g. johnny@gmail.com, mick@hr.com.au")%>
	<div class="button send" url="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiEmailJobAds)) %>"></div>
	<div class="button cancel eighty"></div>
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