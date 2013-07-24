<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<script language="javascript" type="text/javascript">
    var apiPartialMatchesUrl = "<%= Html.RouteRefUrl(LocationRoutes.PartialMatches) %>" + "?countryId=1&location=";
    var apiFlagJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiFlagJobAds)) %>".unMungeUrl();
    var apiUnflagJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiUnflagJobAds)) %>".unMungeUrl();
    var apiUnflagAllJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiUnflagAllJobAds)) %>".unMungeUrl();
    var apiUnflagCurrentJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiUnflagCurrentJobAds)) %>".unMungeUrl();
    var apiBlockJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiBlockJobAds)) %>".unMungeUrl();
    var apiUnblockJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiUnblockJobAds)) %>".unMungeUrl();
    var apiUnblockAllJobAdsUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiUnblockAllJobAds)) %>".unMungeUrl();
    var apiNotesUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiNotes)) %>".unMungeUrl();
    var apiNewNoteUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiNewNote)) %>".unMungeUrl();
    var apiEditNoteUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiEditNote, new { noteId = Guid.Empty })) %>".unMungeUrl();
    var apiDeleteNoteUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiDeleteNote, new { noteId = Guid.Empty })) %>".unMungeUrl();
    var downloadUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.Download)) %>".unMungeUrl();
    var apiRenameFolderUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiRenameFolder, new { folderId = Guid.Empty })) %>".unMungeUrl();
    var apiEmptyFolderUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiEmptyFolder, new { folderId = Guid.Empty })) %>".unMungeUrl();
    var apiCreateSearchFromCurrentUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.SearchRoutes.ApiCreateSearchFromCurrent)) %>".unMungeUrl();
    
    var industries = <%= "[" + string.Join(",", (from i in Model.Industries select "{Id: '" + i.Id + "', Name: '" + i.Name + "'}").ToArray()) + "]"%>;

    var setHash = <%= Model.ListType == JobAdListType.SearchResult ? "true" : "false" %>;
    var criteria = {};
</script>