<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.EmployerHeaderScript" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>

<script language="javascript">
    var apiSaveSearchUrl = "<%= Html.RouteRefUrl(SearchRoutes.ApiSaveSearch) %>";

    var candidatesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.Candidates) %>";
    var resumeUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.PartialCandidates) %>";
    var downloadResumesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.Download) %>";
    var creditsUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.Credits) %>";
    var apiDownloadResumesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiDownloadResumes) %>";
    var apiSendResumesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiSendResumes) %>";
    var apiAttachUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiAttach) %>";
    var apiDetachUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiDetach) %>";
    var apiCheckCanSendMessagesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiCheckCanSendMessages) %>";
    var apiSendMessagesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiSendMessages) %>";
    var apiUnlockUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiUnlock) %>";
    var apiBlockListsUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiBlockLists) %>";
    
    var foldersUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.Folders) %>";
    var apiFoldersUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiFolders) %>";
    var apiNewFolderUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiNewFolder) %>";
    var apiNewFolderUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiNewFolder) %>";
    
    var flagListUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.FlagList) %>";
    var apiFlagCandidatesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiFlagCandidates) %>";
    var apiUnflagCandidatesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiUnflagCandidates) %>";
    var apiUnflagAllCandidatesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiUnflagAllCandidates) %>";
    var apiUnflagCurrentCandidatesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiUnflagCurrentCandidates) %>";
    var apiPhoneNumbersUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiPhoneNumbers) %>";

    var apiTemporarilyBlockCandidates = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiTemporarilyBlockCandidates) %>";
    var apiTemporarilyUnblockCandidates = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiTemporarilyUnblockCandidates) %>";
    var apiPermanentlyBlockCandidates = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiPermanentlyBlockCandidates) %>";
    var apiPermanentlyUnblockCandidates = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiPermanentlyUnblockCandidates) %>";
    var temporaryBlockListsUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.TemporaryBlockList) %>";
    var permanentBlockListsUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.PermanentBlockList) %>";
    var temporaryBlockListName = "<%= BlockListType.Temporary.GetNameDisplayText() %>";
    var permanentBlockListName = "<%= BlockListType.Permanent.GetNameDisplayText() %>";
    var reorderBlockListForSuggestedCandidate = false;
	var bHideTemporaryBlockList = false;
    
    var apiNotesUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiNotes) %>";
    var apiNewNoteUrl = "<%= Html.RouteRefUrl(CandidatesRoutes.ApiNewNote) %>";
    
    var jobAdsUrl = "<%= Html.RouteRefUrl(JobAdsRoutes.JobAds) %>";
    var manageJobAdsUrl = "<%= Html.RouteRefUrl(JobAdsRoutes.ManageCandidates) %>";
    var apiJobAdsUrl = "<%= Html.RouteRefUrl(JobAdsRoutes.ApiJobAds) %>";
    
    var apiHideCreditReminder = "<%= Html.RouteRefUrl(AccountsRoutes.HideCreditReminder) %>";
    var apiHideBulkCreditReminder = "<%= Html.RouteRefUrl(AccountsRoutes.HideBulkCreditReminder) %>";

    var helpIconUrl = "<%= Images.Help %>";
    var candidateFirstNameImgUrl = "<%= new ReadOnlyApplicationUrl("~/Content/Images/employers/send-email/candidate-first-name.png") %>";
    var candidateLastNameImgUrl = "<%= new ReadOnlyApplicationUrl("~/Content/Images/employers/send-email/candidate-last-name.png") %>";
    
    var currentUserEmailAddr = "";
    <% if (CurrentEmployer != null){ %>
        var currentUserEmailAddr = "<%= CurrentEmployer.EmailAddress.Address %>";
        var currentUserFullName = "<%= CurrentEmployer.FullName %>";
        var currentOrgName = "<%= CurrentEmployer.Organisation.Name %>";
    <% } %>
</script>

