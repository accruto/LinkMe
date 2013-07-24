using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using Linkme.Domain.Users.Employers.Contacts;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Controllers.Candidates;
using LinkMe.Web.Areas.Employers.Controllers.Search;
using LinkMe.Web.Areas.Employers.Models;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class CandidatesRoutes
    {
        public const string SegmentSuffix = "candidates";

        public static RouteReference Candidates { get; private set; }
        public static RouteReference Candidate { get; private set; }

        public static RouteReference SalaryBandCandidates { get; private set; }
        public static RouteReference LocationCandidates { get; private set; }
        public static RouteReference LocationSalaryBandCandidates { get; private set; }
        public static RouteReference PagedLocationSalaryBandCandidates { get; private set; }
        public static RouteReference BrowseCandidates { get; private set; }

        public static RouteReference PartialCandidates { get; private set; }
        public static RouteReference Photo { get; private set; }
        public static RouteReference Download { get; private set; }
        public static RouteReference Credits { get; private set; }

        public static RouteReference Folders { get; private set; }
        public static RouteReference Folder { get; private set; }
        public static RouteReference PartialFolder { get; private set; }

        public static RouteReference ApiFolders { get; private set; }
        public static RouteReference ApiNewFolder { get; private set; }
        public static RouteReference ApiAddCandidatesToFolder { get; private set; }

        public static RouteReference FlagList { get; private set; }
        public static RouteReference PartialFlagList { get; private set; }

        public static RouteReference ApiFlagCandidates { get; private set; }
        public static RouteReference ApiUnflagCandidates { get; private set; }
        public static RouteReference ApiUnflagAllCandidates { get; private set; }
        public static RouteReference ApiUnflagCurrentCandidates { get; private set; }

        public static RouteReference ApiPhoneNumbers { get; private set; }
        public static RouteReference ApiCheckCanSendMessages { get; private set; }
        public static RouteReference ApiSendMessages { get; private set; }
        public static RouteReference ApiSendRejectionMessages { get; private set; }
        public static RouteReference ApiAttach { get; private set; }
        public static RouteReference ApiDetach { get; private set; }
        public static RouteReference ApiDownloadResumes { get; private set; }
        public static RouteReference ApiSendResumes { get; private set; }
        public static RouteReference ApiUnlock { get; private set; }

        public static RouteReference TemporaryBlockList { get; private set; }
        public static RouteReference PermanentBlockList { get; private set; }
        public static RouteReference TemporaryPartialBlockList { get; private set; }
        public static RouteReference PermanentPartialBlockList { get; private set; }

        public static RouteReference ApiBlockLists { get; private set; }
        public static RouteReference ApiTemporarilyBlockCandidates { get; private set; }
        public static RouteReference ApiTemporarilyUnblockCandidates { get; private set; }
        public static RouteReference ApiTemporarilyUnblockAllCandidates { get; private set; }
        public static RouteReference ApiPermanentlyBlockCandidates { get; private set; }
        public static RouteReference ApiPermanentlyUnblockCandidates { get; private set; }
        public static RouteReference ApiPermanentlyUnblockAllCandidates { get; private set; }
        public static RouteReference ApiAddCandidatesToBlockList { get; private set; }

        public static RouteReference ApiEditNote { get; private set; }
        public static RouteReference ApiDeleteNote { get; private set; }
        public static RouteReference ApiNotes { get; private set; }
        public static RouteReference ApiNewNote { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Candidates = context.MapAreaRoute<CandidatesController, Guid?, Guid[], Guid?>("employers/candidates", c => c.Resumes);
            Candidate = context.MapAreaRoute<CandidatesController, Guid>("candidates/{locationSegment}/{salarySegment}/{titleSegment}/{candidateId}", c => c.Candidate);

            SalaryBandCandidates = context.MapAreaRoute<BrowseCandidatesController, string>("candidates/-/{salarySegment}", c => c.SalaryBandCandidates);
            LocationCandidates = context.MapAreaRoute<BrowseCandidatesController, string>("candidates/{locationSegment}", c => c.LocationCandidates);
            LocationSalaryBandCandidates = context.MapAreaRoute<SearchController, string, string>("candidates/{locationSegment}/{salarySegment}", c => c.LocationSalaryBandCandidates);
            PagedLocationSalaryBandCandidates = context.MapAreaRoute<SearchController, string, string, int?>("candidates/{locationSegment}/{salarySegment}/{page}", c => c.PagedLocationSalaryBandCandidates);
            BrowseCandidates = context.MapAreaRoute<BrowseCandidatesController>("candidates", c => c.Candidates);

            PartialCandidates = context.MapAreaRoute<CandidatesController, Guid>("employers/candidates/partial", c => c.PartialResume);
            context.MapAreaRoute<CandidatesController, Guid>("employers/candidates/details", c => c.ResumeDetail);
            Photo = context.MapAreaRoute<CandidateFilesController, Guid>("employers/candidates/photo", c => c.Photo);
            Download = context.MapAreaRoute<CandidateFilesController, ResumeMimeType?, Guid[]>("employers/candidates/download", c => c.Download);
            Credits = context.MapAreaRoute<CandidatesController>("employers/candidates/credits", c => c.Credits);

            ApiPhoneNumbers = context.MapAreaRoute<CandidatesApiController, Guid[]>("employers/candidates/api/phonenumbers", c => c.PhoneNumbers);
            ApiCheckCanSendMessages = context.MapAreaRoute<CandidatesApiController, Guid[]>("employers/candidates/api/checkcansendmessages", c => c.CheckCanSendMessages);
            ApiSendMessages = context.MapAreaRoute<CandidatesApiController, ContactMemberMessage, Guid[], Guid[]>("employers/candidates/api/sendmessages", c => c.SendMessages);
            ApiSendRejectionMessages = context.MapAreaRoute<CandidatesApiController, RejectionMemberMessage, Guid, Guid[]>("employers/candidates/api/sendrejectionmessages", c => c.SendRejections);
            ApiAttach = context.MapAreaRoute<CandidatesApiController, Guid[], HttpPostedFileBase>("employers/candidates/api/attach", c => c.Attach);
            ApiDetach = context.MapAreaRoute<CandidatesApiController, Guid>("employers/candidates/api/detach", c => c.Detach);
            ApiUnlock = context.MapAreaRoute<CandidatesApiController, Guid[]>("employers/candidates/api/unlock", c => c.Unlock);
            ApiDownloadResumes = context.MapAreaRoute<CandidatesApiController, Guid[]>("employers/candidates/api/downloadresumes", c => c.DownloadResumes);
            ApiSendResumes = context.MapAreaRoute<CandidatesApiController, ResumeMimeType?, Guid[]>("employers/candidates/api/sendresumes", c => c.SendResumes);

            // Folders API.

            ApiFolders = context.MapAreaRoute<FoldersApiController>("employers/candidates/folders/api", c => c.Folders);
            ApiNewFolder = context.MapAreaRoute<FoldersApiController, string, bool, Guid[]>("employers/candidates/folders/api/new", c => c.NewFolder);
            context.MapAreaRoute<FoldersApiController, Guid>("employers/candidates/folders/api/{folderId}/delete", c => c.DeleteFolder);
            context.MapAreaRoute<FoldersApiController, Guid, string>("employers/candidates/folders/api/{folderId}/rename", c => c.RenameFolder);
            ApiAddCandidatesToFolder = context.MapAreaRoute<FoldersApiController, Guid, Guid[]>("employers/candidates/folders/api/{folderId}/addcandidates", c => c.AddCandidates);
            context.MapAreaRoute<FoldersApiController, Guid, Guid[]>("employers/candidates/folders/api/{folderId}/removecandidates", c => c.RemoveCandidates);

            // Folders.

            Folder = context.MapAreaRoute<FoldersController, Guid, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/folders/{folderId}", c => c.Folder);
            PartialFolder = context.MapAreaRoute<FoldersController, Guid, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/folders/{folderId}/partial", c => c.PartialFolder);
            Folders = context.MapAreaRoute<FoldersController>("employers/candidates/folders", c => c.Folders);

            // FlagLists.

            FlagList = context.MapAreaRoute<FlagListsController, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/flaglist", c => c.FlagList);
            PartialFlagList = context.MapAreaRoute<FlagListsController, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/flaglist/partial", c => c.PartialFlagList);

            // FlagLists API.

            ApiFlagCandidates = context.MapAreaRoute<FlagListsApiController, Guid[]>("employers/candidates/flaglists/api/addcandidates", c => c.AddCandidates);
            ApiUnflagCandidates = context.MapAreaRoute<FlagListsApiController, Guid[]>("employers/candidates/flaglists/api/removecandidates", c => c.RemoveCandidates);
            ApiUnflagAllCandidates = context.MapAreaRoute<FlagListsApiController>("employers/candidates/flaglists/api/removeallcandidates", c => c.RemoveAllCandidates);
            ApiUnflagCurrentCandidates = context.MapAreaRoute<FlagListsApiController>("employers/candidates/flaglists/api/removecurrentcandidates", c => c.RemoveCurrentCandidates);

            // BlockLists API.

            ApiBlockLists = context.MapAreaRoute<BlockListsApiController>("employers/candidates/blocklists/api", c => c.BlockLists);
            ApiAddCandidatesToBlockList = context.MapAreaRoute<BlockListsApiController, Guid, Guid[]>("employers/candidates/blocklists/api/{blockListId}/addcandidates", c => c.AddCandidates);
            context.MapAreaRoute<BlockListsApiController, Guid, Guid[]>("employers/candidates/blocklists/api/{blockListId}/removecandidates", c => c.RemoveCandidates);
            ApiTemporarilyBlockCandidates = context.MapAreaRoute<BlockListsApiController, Guid[]>("employers/candidates/blocklists/api/blocktemporarycandidates", c => c.BlockTemporaryCandidates);
            ApiTemporarilyUnblockCandidates = context.MapAreaRoute<BlockListsApiController, Guid[]>("employers/candidates/blocklists/api/unblocktemporarycandidates", c => c.UnblockTemporaryCandidates);
            ApiTemporarilyUnblockAllCandidates = context.MapAreaRoute<BlockListsApiController>("employers/candidates/blocklists/api/unblockalltemporarycandidates", c => c.UnblockAllTemporaryCandidates);
            ApiPermanentlyBlockCandidates = context.MapAreaRoute<BlockListsApiController, Guid[]>("employers/candidates/blocklists/api/blockpermanentcandidates", c => c.BlockPermanentCandidates);
            ApiPermanentlyUnblockCandidates = context.MapAreaRoute<BlockListsApiController, Guid[]>("employers/candidates/blocklists/api/unblockpermanentcandidates", c => c.UnblockPermanentCandidates);
            ApiPermanentlyUnblockAllCandidates = context.MapAreaRoute<BlockListsApiController>("employers/candidates/blocklists/api/unblockallpermanentcandidates", c => c.UnblockAllPermanentCandidates);

            // BlockLists.

            TemporaryBlockList = context.MapAreaRoute<BlockListsController, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/blocklists/temporary", c => c.TemporaryBlockList);
            PermanentBlockList = context.MapAreaRoute<BlockListsController, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/blocklists/permanent", c => c.PermanentBlockList);
            TemporaryPartialBlockList = context.MapAreaRoute<BlockListsController, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/blocklists/temporary/partial", c => c.TemporaryPartialBlockList);
            PermanentPartialBlockList = context.MapAreaRoute<BlockListsController, MemberSearchSortCriteria, CandidatesPresentationModel>("employers/candidates/blocklists/permanent/partial", c => c.PermanentPartialBlockList);

            // Notes API.

            ApiNotes = context.MapAreaRoute<NotesApiController, Guid>("employers/candidates/notes/api", c => c.Notes);
            ApiNewNote = context.MapAreaRoute<NotesApiController, Guid[], string, bool>("employers/candidates/notes/api/new", c => c.NewNote);
            ApiEditNote = context.MapAreaRoute<NotesApiController, Guid, string, bool?>("employers/candidates/notes/api/{noteId}/edit", c => c.EditNote);
            ApiDeleteNote = context.MapAreaRoute<NotesApiController, Guid>("employers/candidates/notes/api/{noteId}/delete", c => c.DeleteNote);
            context.MapAreaRoute<NotesApiController, Guid>("employers/candidates/notes/api/{noteId}", c => c.Note);
        }
    }
}
