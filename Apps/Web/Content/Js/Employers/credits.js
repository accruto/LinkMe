(function ($) {

    viewPhoneNumbers = function (candidateIds) {
        showResultsOverlay();
        employers.api.viewPhoneNumbers(
            candidateIds,
            function () {
                hideResultsOverlay();
                updateResults(true);
            },
            function () {
                showResultsFailedOverlay();
            },
            function () {
                hideResultsOverlay();
            });
    }

    downloadResumes = function (candidateIds) {
        showResultsOverlay();
        employers.api.downloadResumes(
            candidateIds,
            function () {
                hideResultsOverlay();
                updateResults(true);
            },
            function () {
                showResultsFailedOverlay();
            },
            function () {
                hideResultsOverlay();
            });
    }

    emailResumes = function (candidateIds) {
        showResultsOverlay();
        employers.api.emailResumes(
            candidateIds,
            function () {
                hideResultsOverlay();
                updateResults(true);
            },
            function () {
                showResultsFailedOverlay();
            },
            function () {
                hideResultsOverlay();
            });
    }

    unlock = function (candidateIds) {
        showResultsOverlay();
        employers.api.unlock(
            candidateIds,
            function () {
                hideResultsOverlay();
                updateResults(true);
            },
            function () {
                showResultsFailedOverlay();
            },
            function () {
                hideResultsOverlay();
            });
    }

    checkCanSendMessage = function (candidateIds, onSuccess) {
        showResultsOverlay();
        employers.api.checkCanSendMessage(
            candidateIds,
            function () {
                hideResultsOverlay();
                onSuccess();
            },
            function () {
                showResultsFailedOverlay();
            },
            function () {
                hideResultsOverlay();
            });
    }

    sendMessage = function (subject, body, from, candidateIds, sendCopy, attachmentIds) {
        showResultsOverlay();
        employers.api.sendMessage(
            subject,
            body,
            from,
            candidateIds,
            sendCopy,
            attachmentIds,
            function () {
                hideResultsOverlay();
                updateResults(true);
            },
            function () {
                showResultsFailedOverlay();
            },
            function () {
                hideResultsOverlay();
            });
    }

})(jQuery);