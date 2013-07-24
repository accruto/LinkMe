(function($) {

    employers = {
        api: {

            cachedFolders: undefined,
            cachingFolders: false,
            foldersSuccesses: [],

            cachedBlockLists: undefined,
            cachingBlockLists: false,
            blockListSuccesses: [],

            cachedJobAds: undefined,
            cachingJobAds: false,
            jobAdsSuccesses: [],

            getCandidateIdsQueryString: function(candidateIds) {
                return api.getArrayQueryString("candidateId", candidateIds);
            },

            // Folders.

            onGetFoldersSuccess: function(response) {
                employers.api.cachedFolders = response.Folders;
                employers.api.cachingFolders = false;

                // Call each registered folder function.

                for (var i = 0; i < employers.api.foldersSuccesses.length; i++)
                    employers.api.foldersSuccesses[i](employers.api.cachedFolders);

                employers.api.foldersSuccesses.length = 0;
            },

            getFolders: function(resetCache, onSuccess) {
                if (resetCache)
                    employers.api.cachedFolders = undefined;

                if (employers.api.cachedFolders == undefined) {

                    // Store for when folders are returned.

                    employers.api.foldersSuccesses.push(onSuccess);

                    // Issue a request if one has not already out there.

                    if (!employers.api.cachingFolders) {
                        employers.api.cachingFolders = true;
                        api.call(apiFoldersUrl, null, employers.api.onGetFoldersSuccess);
                    }
                }
                else {
                    onSuccess(employers.api.cachedFolders);
                }
            },

            renameFolder: function(folderId, name, onSuccess, onError, onComplete) {
                api.call(
                    apiFoldersUrl + "/" + folderId + "/rename?name=" + name,
                    null,
                    onSuccess,
                    onError,
                    onComplete);
            },

            deleteFolder: function(folderId, onSuccess, onError, onComplete) {
                api.call(
                    apiFoldersUrl + "/" + folderId + "/delete",
                    null,
                    onSuccess,
                    onError,
                    onComplete);
            },

            addFolder: function(name, isShared, onSuccess, onError, onComplete) {
                api.call(
                    apiNewFolderUrl + "?name=" + name + "&isShared=" + isShared,
                    null,
                    function(response) { onSuccess(response.Folder.Id); },
                    onError,
                    onComplete);
            },

            addCandidatesToFolder: function(folderId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiFoldersUrl + "/" + folderId + "/addcandidates",
                    { candidateIds: candidateIds },
                    function(response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            removeCandidatesFromFolder: function(folderId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiFoldersUrl + "/" + folderId + "/removecandidates",
                    { candidateIds: candidateIds },
                    function(response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            // BlockLists.

            onGetBlockListsSuccess: function(response) {
                employers.api.cachedBlockLists = response.BlockLists;
                employers.api.cachingBlockLists = false;

                // Call each registered blockList function.

                for (var i = 0; i < employers.api.blockListSuccesses.length; i++)
                    employers.api.blockListSuccesses[i](employers.api.cachedBlockLists);

                employers.api.blockListSuccesses.length = 0;
            },

            getBlockLists: function(resetCache, onSuccess) {
                if (resetCache)
                    employers.api.cachedBlockLists = undefined;

                if (employers.api.cachedBlockLists == undefined) {

                    // Store for when blocklists are returned.

                    employers.api.blockListSuccesses.push(onSuccess);

                    // Issue a request if one has not already out there.

                    if (!employers.api.cachingBlockLists) {
                        employers.api.cachingBlockLists = true;
                        api.call(apiBlockListsUrl, null, employers.api.onGetBlockListsSuccess);
                    }
                }
                else {
                    onSuccess(employers.api.cachedBlockLists);
                }
            },

            removeCandidatesFromBlockList: function(blockListId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiBlockListsUrl + "/" + blockListId + "/removecandidates",
                    { candidateIds: candidateIds },
                    function (response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            temporarilyBlockCandidates: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiTemporarilyBlockCandidates,
                    { candidateIds: candidateIds },
                    function(response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            permanentlyBlockCandidates: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiPermanentlyBlockCandidates,
                    { candidateIds: candidateIds },
                    function (response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            restoreTemporarilyBlockedCandidates: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiTemporarilyUnblockCandidates,
                    { candidateIds: candidateIds },
                    function (response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            restorePermanentlyBlockedCandidates: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiPermanentlyUnblockCandidates,
                    { candidateIds: candidateIds },
                    function (response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            // FlagLists.

            addCandidatesToFlagList: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiFlagCandidatesUrl,
                    { candidateIds: candidateIds },
                    function (response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            removeCandidatesFromFlagList: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiUnflagCandidatesUrl,
                    { candidateIds: candidateIds },
                    function (response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            removeAllCandidatesFromFlagList: function(onSuccess, onError, onComplete) {
                api.call(
                    apiUnflagAllCandidatesUrl,
                    null,
                    function(response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            removeCurrentCandidatesFromFlagList: function(onSuccess, onError, onComplete) {
                api.call(
                    apiUnflagCurrentCandidatesUrl,
                    null,
                    function(response) { onSuccess(response.Count); },
                    onError,
                    onComplete);
            },

            // JobAds.

            onGetJobAdsSuccess: function(response) {
                employers.api.cachedJobAds = response.JobAds;
                employers.api.cachingJobAds = false;

                // Call each registered jobad function.

                for (var i = 0; i < employers.api.jobAdsSuccesses.length; i++)
                    employers.api.jobAdsSuccesses[i](employers.api.cachedJobAds);

                employers.api.jobAdsSuccesses.length = 0;
            },

            getJobAds: function(onSuccess) {
                if (employers.api.cachedJobAds == undefined) {

                    // Store for when jobAds are returned.

                    employers.api.jobAdsSuccesses.push(onSuccess);

                    // Issue a request if one has not already out there.

                    if (!employers.api.cachingJobAds) {
                        employers.api.cachingJobAds = true;
                        api.call(apiJobAdsUrl, null, employers.api.onGetJobAdsSuccess);
                    }
                }
                else {
                    onSuccess(employers.api.cachedJobAds);
                }
            },

            shortlistCandidatesForJobAd: function(jobAdId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiJobAdsUrl + "/" + jobAdId + "/shortlistcandidates",
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            rejectCandidatesForJobAd: function(jobAdId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiJobAdsUrl + "/" + jobAdId + "/rejectcandidates",
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            removeCandidatesFromJobAd: function(jobAdId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiJobAdsUrl + "/" + jobAdId + "/removecandidates",
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            undoShortlistCandidatesForJobAd: function(jobAdId, candidateIds, previousStatus, onSuccess, onError, onComplete) {
                if (!previousStatus)
			        previousStatus = "";
			    api.call(
                    apiJobAdsUrl + "/" + jobAdId + "/undoshortlistcandidates?previousStatus=" + previousStatus,
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            undoRejectCandidatesForJobAd: function(jobAdId, candidateIds, previousStatus, onSuccess, onError, onComplete) {
                api.call(
                    apiJobAdsUrl + "/" + jobAdId + "/undorejectcandidates?previousStatus=" + previousStatus,
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            undoRemoveCandidatesFromJobAd: function(jobAdId, candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiJobAdsUrl + "/" + jobAdId + "/undoremovecandidates",
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            // Candidates.

            viewPhoneNumbers: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiPhoneNumbersUrl,
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            downloadResumes: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiDownloadResumesUrl,
                    { candidateIds: candidateIds },
                    function () {
                        download({ url: downloadResumesUrl + "?" + employers.api.getCandidateIdsQueryString(candidateIds) });
                        onSuccess();
                    },
                    onError,
                    onComplete);
            },

            emailResumes: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiSendResumesUrl,
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            unlock: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiUnlockUrl,
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            checkCanSendMessage: function(candidateIds, onSuccess, onError, onComplete) {
                api.call(
                    apiCheckCanSendMessagesUrl,
                    { candidateIds: candidateIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            sendMessage: function(subject, body, from, candidateIds, sendCopy, attachmentIds, onSuccess, onError, onComplete) {
                api.call(
                    apiSendMessagesUrl,
                    { subject: subject, body: body, from: from, sendCopy: sendCopy, candidateIds: candidateIds, attachmentIds: attachmentIds },
                    onSuccess,
                    onError,
                    onComplete);
            },

            sendRejectionMessage: function(subject, body, from, candidateIds, sendCopy, jobAdId, onSuccess, onError, onComplete) {
                api.call(
                    apiSendRejectionMessagesUrl,
                    { subject: subject, body: body, from: from, sendCopy: sendCopy, candidateIds: candidateIds, jobAdId: jobAdId },
                    onSuccess,
                    onError,
                    onComplete);
            },

            // Saved searches.

            saveSearch: function(name, isAlert, onSuccess, onError, onComplete) {
                api.call(
                    apiSaveSearchUrl + "?name=" + encodeURIComponent(name) + "&isAlert=" + isAlert, //add encodeURIComponent here for name like C#, added by Gary on 31/10/2011
                    null,
                    onSuccess,
                    onError,
                    onComplete);
            },

            // Notes.

            getNotes: function(candidateId, onSuccess, onError, onComplete) {
                api.call(
                    apiNotesUrl + "?candidateId=" + candidateId,
                    null,
                    function(response) { onSuccess(response.Notes); },
                    onError,
                    onComplete);
            },

            newNote: function(candidateIds, isShared, text, onSuccess, onError, onComplete) {
                api.call(
                    apiNewNoteUrl,
                    { text: text, candidateIds: candidateIds, isShared: isShared },
                    onSuccess,
                    onError,
                    onComplete);
            },

            deleteNote: function(noteId, onSuccess, onError, onComplete) {
                api.call(
                    apiNotesUrl + "/" + noteId + "/delete",
                    null,
                    onSuccess,
                    onError,
                    onComplete);
            },

            editNote: function(noteId, isShared, text, onSuccess, onError, onComplete) {

                // Put the text in the post data.

                api.call(
                    apiNotesUrl + "/" + noteId + "/edit?isShared=" + isShared,
                    { text: text },
                    function(response) { onSuccess(response.Note); },
                    onError,
                    onComplete);
            },

            hideCreditReminder: function() {

                // Ignore all responses.

                api.call(
                    apiHideCreditReminder,
                    null,
                    function() { },
                    function() { },
                    function() { });
            },

            hideBulkCreditReminder: function() {

                // Ignore all responses.

                api.call(
                    apiHideBulkCreditReminder,
                    null,
                    function() { },
                    function() { },
                    function() { });
            }
        }
    }

})(jQuery);