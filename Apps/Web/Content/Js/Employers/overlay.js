/*
 * Dependencies:
 * - search.css
 *
 */

(function ($) {

    showResultsOverlay = function (onRetry) {
        var overlay = $("#results-overlay");
        $(".loading", overlay).show();
        $(".error", overlay).hide();
        overlay.show();

        overlay.unbind('ajaxError');
        overlay.ajaxError(function () { showResultsFailedOverlay(onRetry); });
    }

    hideResultsOverlay = function () {
        $("#results-overlay").hide();
    }

    showResultsFailedOverlay = function (onRetry) {
        var overlay = $("#results-overlay");
        var retry = $("#results-overlay-retry", overlay);
        var noRetry = $("#results-overlay-no-retry", overlay);
        retry.unbind('click');
        if (onRetry) {
            retry.parent().show();
            retry.click(function () {
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                onRetry();
            });
        }
        else {
            retry.parent().hide();
        }
        noRetry.unbind('click');
        noRetry.click(function () {
            hideResultsOverlay();
        });
        $(".loading", overlay).hide();
        $(".error", overlay).show();
        $(".error").css("display", ""); // Reset display:block; to display:(whatever is in the CSS);
    }

    performCreditsAction = function (action, candidateIds) {
        if (action == "viewPhoneNumbers")
            viewPhoneNumbers(candidateIds);
        else if (action == "downloadResumes")
            downloadResumes(candidateIds);
        else if (action == "emailResumes")
            emailResumes(candidateIds);
    }

    function showOverlay(overlay) {
        var backgroundOverlay = $("#background-overlay");
        if (backgroundOverlay.length == 0) {
            backgroundOverlay = $(document.createElement("div"));
            backgroundOverlay.attr("id", "background-overlay");
            backgroundOverlay.addClass("overlay-bg");
            backgroundOverlay.css("height", ($("body").get(0).offsetHeight) + "px");    // Hack! There ought to be a CSS way to handle this.
            $("body").find("#container").prepend(backgroundOverlay);
        }
        else {
            $("#background-overlay").css("height", ($("body").get(0).offsetHeight) + "px");
            $("#background-overlay").show();
        }
        if (overlay != null) {
            overlay.show();
        }
    }

    function hideOverlay(overlay) {
        if (overlay != null) {
            overlay.hide();
        }
        $("#background-overlay").hide();
    }

    var _searchInitialized = false;

    initializeSearchOverlay = function () {
        if (_searchInitialized)
            return;

        // Hide the overlay on click of close button.

        $(".key_loc-overlay").find(".overlay-title").find("span.close-icon").click(function () {

            hideOverlay($(".key_loc-overlay"));

            // Cancelled so reset the values.

            $("#Distance").val($("#Distance").attr('data-reset-value'));
            $("#CountryId").val($("#CountryId").attr('data-reset-value'));

            $("#IncludeRelocating").attr('data-reset-value') == "true"
                ? $("#IncludeRelocating").attr("checked", "checked")
                : $("#IncludeRelocating").removeAttr("checked");

            $("#IncludeInternational").attr('data-reset-value') == "true"
                ? $("#IncludeInternational").attr("checked", "checked")
                : $("#IncludeInternational").removeAttr("checked");

            return false;
        });

        _searchInitialized = true;
    }

    $.fn.makeSearchOverlay = function () {

        initializeSearchOverlay();

        // Invoke the overlay.

        $(this).click(function () {

            if ($(this).hasClass("loc-suggest-candidate")) {
                $("#Distance").attr('data-reset-value', $("#Distance").val());
                $("#CountryId").attr('data-reset-value', $("#CountryId").val());
                $("#IncludeRelocating").attr('data-reset-value', $("#IncludeRelocating").is(":checked"));
                $("#IncludeInternational").attr('data-reset-value', $("#IncludeInternational").is(":checked"));

                var topOff = $(".distance_section").offset().top - 23;
                var leftOff = $(".distance_section").offset().left + 236;
                var klOverlay = $(".key_loc-overlay");
                var closeButton = klOverlay.find(".close-icon").clone(true);
                klOverlay.find(".overlay-title").html("Change location ").append(closeButton);
                klOverlay.find(".column:eq(0)").hide();
                klOverlay.find(".overlay").css("width", "350px");
                klOverlay.show().offset({ top: topOff, left: leftOff });
                showOverlay();
                return false;
            } else {
                if ($(this).hasClass("js_dist-key-loc")) {
                    $('html, body').animate({ scrollTop: 0 }, 'slow');
                }

                // Save current state.

                $("#Distance").attr('data-reset-value', $("#Distance").val());
                $("#CountryId").attr('data-reset-value', $("#CountryId").val());
                $("#IncludeRelocating").attr('data-reset-value', $("#IncludeRelocating").is(":checked"));
                $("#IncludeInternational").attr('data-reset-value', $("#IncludeInternational").is(":checked"));

                var topOff = $("#key_loc").offset().top - 30;
                var leftOff = $("#key_loc").offset().left + 229;
                $(".key_loc-overlay").show();
                $(".key_loc-overlay").offset({ top: topOff, left: leftOff });
                showOverlay();
                if ($("#CandidateName").val() != "") showAdvancedSection("keywords");

                //$("#Keywords").focus();
                return false;
            }
        });
    }

    showAddFolderOverlay = function (isPrivate, candidateIds, onSuccess) {

        var folderOverlay = $(".folder-overlay");
        showOverlay(folderOverlay);

        folderOverlay.css("top", "50%");
        folderOverlay.css("left", "50%");
        folderOverlay.css("margin-top", "-64px");
        folderOverlay.css("margin-left", "-170px");
        folderOverlay.css("position", "fixed");

        folderOverlay.find(".overlay-title").css("display", "block");
        folderOverlay.find(".overlay-title-text").text("Add folder");

        folderOverlay.find(".overlay-text").html("<div class='textbox_field field'><label>Name</label><div class='textbox_control control'><input name='Name' maxlength='265' id='Name' class='' type='text' value='' size='50'></div></div>");
        folderOverlay.find(".overlay-text").append("<div class='folderTypeRadio'><span><input name='FolderType' id='personal' value='false' type='radio'/> Personal</span><span><input name='FolderType' id='org-wide' value='true' type='radio'/> Organisation-wide</span></div>");

        folderOverlay.find(".buttons-holder").html("<input class='cancel-button button' id='cancel' name='cancel' type='button' value='Cancel' /><input class='create-folder_button button' id='create-folder' name='create-folder' type='button' value='Create Folder'/>");

        folderOverlay.find('input[name=FolderType]:checked').removeAttr('checked');
        if (isPrivate) {
            folderOverlay.find('input[name=FolderType]').val(["false"]).attr('checked');
        } else {
            folderOverlay.find('input[name=FolderType]').val(["true"]).attr('checked');
        }

        var name = folderOverlay.find('input[name=Name]');
        name.focus();

        // Close when cancelled.

        folderOverlay.find("#cancel").click(function () {
            $("#personal-folders").find(".ajax-loader").remove();
            $("#org-wide-folders").find(".ajax-loader").remove();
            hideOverlay($(".folder-overlay"));
            return false;
        });

        folderOverlay.find(".overlay-title").find("span.close-icon").click(function () {
            $("#personal-folders").find(".ajax-loader").remove();
            $("#org-wide-folders").find(".ajax-loader").remove();
            hideOverlay($(".folder-overlay"));
            return false;
        });

        // Create.

        folderOverlay.find("#create-folder").click(function () {
            if ($(".folder-overlay").find('input[type=text]').val() == "") {
                alert("Please enter a folder name");
                return false;
            }
            else {
                $("#personal-folders").prepend("<span class='ajax-loader'></span>");
                $("#org-wide-folders").prepend("<span class='ajax-loader'></span>");
                var name = $(".folder-overlay").find('input[type=text]').val();
                var isShared = $(".folder-overlay").find('input[name=FolderType]:checked').val();
                employers.api.addFolder(
                    name,
                    isShared,
                    function (folderId) {
                        if (candidateIds instanceof Array) {
                            addCandidatesToFolder(folderId, candidateIds);
                        }
                        onSuccess();
                    },
                    null,
                    function () {
                        $("#personal-folders").find(".ajax-loader").remove();
                        $("#org-wide-folders").find(".ajax-loader").remove();
                        hideOverlay($(".folder-overlay"));
                    });
            }
        });
    }

    showDeleteFolderOverlay = function (folderId, isPrivate, onSuccess) {

        var folderOverlay = $(".folder-overlay");
        showOverlay(folderOverlay);

        folderOverlay.css("top", "50%");
        folderOverlay.css("left", "50%");
        folderOverlay.css("margin-top", "-64px");
        folderOverlay.css("margin-left", "-170px");
        folderOverlay.css("position", "fixed");


        folderOverlay.find(".overlay-title").css("display", "none");
        folderOverlay.find(".overlay-text").html("<div class='overlay-sub-title'> Remove Folder </div>");

        if (isPrivate) {
            folderOverlay.find(".overlay-text").append("<div><span><b>Warning : </b>Are you sure you want to remove this folder?</span></div>");
        } else {
            folderOverlay.find(".overlay-text").append("<div><span><b>Warning : </b>Other staff in your organisation may be using the folder. Are you sure you want to remove this folder?</span></div>");
        }

        folderOverlay.find(".buttons-holder").html("<input class='cancel-button button' id='cancel' name='cancel' type='button' value='Cancel' /><input class='ok_button button' id='delete-folder' name='delete-folder' type='button' value='OK'/>");

        // Cancel.

        folderOverlay.find(".overlay-title").find("span.close-icon").click(function () {
            hideOverlay($(".folder-overlay"));
            return false;
        });

        folderOverlay.find("#cancel").click(function () {
            hideOverlay($(".folder-overlay"));
            return false;
        });

        // OK.

        folderOverlay.find("#delete-folder").click(function () {
            employers.api.deleteFolder(
                folderId,
                onSuccess,
                null,
                function () {
                    hideOverlay($(".folder-overlay"));
                });
        });
    }

    var _saveSearchInitialized = false;
    var _saveAlertInitialized = false;
    var _savedSearchName = "";

    initializeSaveSearchOverlay = function (saveSearchOverlay) {

        if (_saveSearchInitialized)
            return;

        // On change of checkbox */

        saveSearchOverlay.find("#save-search-email").change(function () {
            $("#save-search-overlay").find("#save-search-alert").toggle();
        });

        // Save

        saveSearchOverlay.find("#save-search").click(function () {
            var name = $("#save-search-overlay").find("#save-search-name");

            // Overwriting existing saved search so ask to confirm.

            if (name.val() == _savedSearchName) {
                if (!confirm("This will replace your existing '" + name.val() + "' saved search. Do you want to continue?")) {
                    name.select();
                    name.focus();
                    return;
                }
            }

            employers.api.saveSearch(
	            name.val(),
	            $("#save-search-overlay").find("#save-search-email:checked").length > 0 ? true : false,
	            function () {
	                _savedSearchName = name.val();
	            },
	            function (errors) {
	                var errorReport = "";
	                for (var index = 0; index < errors.length; ++index) {
	                    errorReport += "<span class='error-msg'>" + errors[index] + "</span><br />";
	                }
	                errorReport += "<br />";
	                $("#save-search-overlay").find(".error-report").html(errorReport);
	                name.select();
	                name.focus();
	                return false;
	            },
                function () {
                    hideOverlay($("#save-search-overlay"));
                });
        });

        // Close button.

        saveSearchOverlay.find(".overlay-title").find("span.close-icon").click(function () {
            hideOverlay($("#save-search-overlay"));
            return false;
        });

        // Cancel.

        saveSearchOverlay.find("#save-search-cancel").click(function () {
            hideOverlay($("#save-search-overlay"));
            return false;
        });

        _saveSearchInitialized = true;
    }

    initializeSaveAlertOverlay = function (saveAlertOverlay) {

        if (_saveAlertInitialized)
            return;

        // Save.

        saveAlertOverlay.find("#save-alert").click(function () {
            var name = $("#save-alert-overlay").find("#save-alert-name");

            // Overwriting existing saved search so ask to confirm.

            if (name.val() == _savedSearchName) {
                if (!confirm("This will replace your existing '" + name.val() + "' saved search. Do you want to continue?")) {
                    name.select();
                    name.focus();
                    return;
                }
            }

            employers.api.saveSearch(
	            name.val(),
	            true,
	            function () {
	                _savedSearchName = name.val();
	            },
	            function (errors) {
	                var errorReport = "";
	                for (var index = 0; index < errors.length; ++index) {
	                    errorReport += "<span class='error-msg'>" + errors[index] + "</span><br />";
	                }
	                errorReport += "<br />";
	                $("#save-alert-overlay").find(".error-report").html(errorReport);
	                name.select();
	                name.focus();
	                return false;
	            },
                function () {
                    hideOverlay($("#save-alert-overlay"));
                });
        });

        // Close button.

        saveAlertOverlay.find(".overlay-title").find("span.close-icon").click(function () {
            hideOverlay($("#save-alert-overlay"));
            return false;
        });

        // Cancel.

        saveAlertOverlay.find("#save-alert-cancel").click(function () {
            hideOverlay($("#save-alert-overlay"));
            return false;
        });

        _saveAlertInitialized = true;
    }

    showSaveSearchOverlay = function (emailAddress, savedSearchName, suggestedName) {

        var saveSearchOverlay = $("#save-search-overlay");
        initializeSaveSearchOverlay(saveSearchOverlay);
        showOverlay(saveSearchOverlay);

        saveSearchOverlay.css("top", "50%");
        saveSearchOverlay.css("left", "50%");
        saveSearchOverlay.css("margin-top", "-64px");
        saveSearchOverlay.css("margin-left", "-170px");
        saveSearchOverlay.css("position", "fixed");

        saveSearchOverlay.find(".error-report").html("");
        saveSearchOverlay.find(".email-address").text(emailAddress);

        // Prepare the name.

        var name = saveSearchOverlay.find("#save-search-name");
        if (savedSearchName != null)
            _savedSearchName = savedSearchName;
        if (_savedSearchName == "")
            name.val(suggestedName);
        else
            name.val(_savedSearchName);
        name.select();
        name.focus();
    }

    showSaveAlertOverlay = function (emailAddress, savedSearchName, suggestedName) {

        var saveAlertOverlay = $("#save-alert-overlay");
        initializeSaveAlertOverlay(saveAlertOverlay);
        showOverlay(saveAlertOverlay);

        saveAlertOverlay.css("top", "50%");
        saveAlertOverlay.css("left", "50%");
        saveAlertOverlay.css("margin-top", "-64px");
        saveAlertOverlay.css("margin-left", "-170px");
        saveAlertOverlay.css("position", "fixed");

        saveAlertOverlay.find(".error-report").html("");
        saveAlertOverlay.find(".email-address").text(emailAddress);

        // Prepare the name.

        var name = saveAlertOverlay.find("#save-alert-name");
        if (savedSearchName != null)
            _savedSearchName = savedSearchName;
        if (_savedSearchName == "")
            name.val(suggestedName);
        else
            name.val(_savedSearchName);
        name.select();
        name.focus();
    }

    var _hideCreditsState = {
        initialized: false,
        initialHideCreditReminder: false,
        initialHideBulkCreditReminder: false,
        hideCreditReminder: false,
        hideBulkCreditReminder: false
    }

    initializeHideCredits = function (hideCreditReminder, hideBulkCreditReminder) {

        if (_hideCreditsState.initialized)
            return;

        _hideCreditsState.initialHideCreditReminder = hideCreditReminder;
        _hideCreditsState.initialHideBulkCreditReminder = hideBulkCreditReminder;
        _hideCreditsState.hideCreditReminder = hideCreditReminder;
        _hideCreditsState.hideBulkCreditReminder = hideBulkCreditReminder;

        _hideCreditsState.initialized = true;
    }

    hideCreditReminders = function (candidateIds) {
        if (candidateIds.length <= 1) {
            if (_hideCreditsState.hideCreditReminder && _hideCreditsState.hideCreditReminder != _hideCreditsState.initialHideCreditReminder) {
                employers.api.hideCreditReminder();
            }
        }
        else {
            if (_hideCreditsState.hideBulkCreditReminder && _hideCreditsState.hideBulkCreditReminder != _hideCreditsState.initialHideBulkCreditReminder) {
                employers.api.hideBulkCreditReminder();
            }
        }
    };

    var _creditsState = {
        initialized: false,
        action: null,
        canWithCreditCandidateIds: null,
        canWithoutCreditCandidateIds: null
    };

    initializeCreditsOverlay = function (creditsOverlay, hideCreditReminder, hideBulkCreditReminder) {

        if (_creditsState.initialized)
            return;

        initializeHideCredits(hideCreditReminder, hideBulkCreditReminder);

        // Reminder checkbox

        $("#credits-reminder").click(function () {
            if (_creditsState.canWithCreditCandidateIds.length <= 1)
                _hideCreditsState.hideCreditReminder = $("#credits-reminder").is(":checked");
            else
                _hideCreditsState.hideBulkCreditReminder = $("#credits-reminder").is(":checked");
        });

        // OK

        creditsOverlay.find("#credits-ok").click(function () {
            hideOverlay($(".credits-overlay"));
            performCreditsAction(_creditsState.action, _creditsState.canWithCreditCandidateIds.concat(_creditsState.canWithoutCreditCandidateIds));
            hideCreditReminders(_creditsState.canWithCreditCandidateIds);
        });

        // Cancel

        creditsOverlay.find("#credits-cancel").click(function () {
            hideOverlay($(".credits-overlay"));
            _hideCreditsState.hideCreditReminder = _hideCreditsState.initialHideCreditReminder;
            _hideCreditsState.hideBulkCreditReminder = _hideCreditsState.initialHideBulkCreditReminder;
            return false;
        });

        _creditsState.initialized = true;
    }

    showCreditsOverlay = function (action, canWithCreditCandidateIds, canWithoutCreditCandidateIds, hideCreditReminder, hideBulkCreditReminder) {

        var creditsOverlay = $(".credits-overlay");
        initializeCreditsOverlay(creditsOverlay, hideCreditReminder, hideBulkCreditReminder);

        // Prevent the overlay as needed.

        if ((canWithCreditCandidateIds.length <= 1 && _hideCreditsState.hideCreditReminder) || (canWithCreditCandidateIds.length > 1 && _hideCreditsState.hideBulkCreditReminder)) {
            performCreditsAction(action, canWithCreditCandidateIds.concat(canWithoutCreditCandidateIds));
            return false;
        }

        _creditsState.action = action;
        _creditsState.canWithCreditCandidateIds = canWithCreditCandidateIds;
        _creditsState.canWithoutCreditCandidateIds = canWithoutCreditCandidateIds;

        showOverlay(creditsOverlay);

        creditsOverlay.css("top", "50%");
        creditsOverlay.css("left", "50%");
        creditsOverlay.css("margin-top", "-100px");
        creditsOverlay.css("margin-left", "-215px");
        creditsOverlay.css("position", "fixed");

        var description = "";
        if (action == "viewPhoneNumbers") {
            description = "Revealing candidate phone numbers";
        }
        else if (action == "downloadResumes") {
            description = "Downloading candidate resumes";
        }
        else if (action == "emailResumes") {
            description = "Emailing candidate resumes";
        }

        var unlockDescription = "";

        if (canWithoutCreditCandidateIds.length == 0 && canWithCreditCandidateIds.length == 1) {
            unlockDescription = " unlocks all of a candidate's personal and contact details.";
        }
        else {
            if (canWithoutCreditCandidateIds.length == 0)
                unlockDescription = " unlocks all of the " + canWithCreditCandidateIds.length + " candidate's personal and contact details.";
            else
                unlockDescription = " unlocks " + canWithCreditCandidateIds.length + " of the " + (canWithCreditCandidateIds.length + canWithoutCreditCandidateIds.length) + " candidate's personal and contact details.";
        }

        creditsOverlay.find("#credits-description").html(description + unlockDescription);
        creditsOverlay.find("#credits-reminder").removeAttr("checked");
    }

    var _unlockState = {
        initialized: false,
        candidateIds: null
    };

    initializeUnlockOverlay = function (unlockOverlay, hideCreditReminder, hideBulkCreditReminder) {

        if (_unlockState.initialized)
            return;

        initializeHideCredits(hideCreditReminder, hideBulkCreditReminder);

        // Reminder checkbox

        $("#unlock-reminder").click(function () {
            if (_unlockState.candidateIds.length <= 1)
                _hideCreditsState.hideCreditReminder = $("#unlock-reminder").is(":checked");
            else
                _hideCreditsState.hideBulkCreditReminder = $("#unlock-reminder").is(":checked");
        });

        // OK

        unlockOverlay.find("#unlock-ok").click(function () {
            hideOverlay($(".unlock-overlay"));
            unlock(_unlockState.candidateIds);
            hideCreditReminders(_unlockState.candidateIds);
        });

        // Cancel

        unlockOverlay.find("#unlock-cancel").click(function () {
            hideOverlay($(".unlock-overlay"));
            _hideCreditsState.hideCreditReminder = _hideCreditsState.initialHideCreditReminder;
            _hideCreditsState.hideBulkCreditReminder = _hideCreditsState.initialHideBulkCreditReminder;
            return false;
        });

        _unlockState.initialized = true;
    }

    showUnlockOverlay = function (positioningElement, candidateIds, hideCreditReminder, hideBulkCreditReminder) {

        var unlockOverlay = $(".unlock-overlay");
        initializeUnlockOverlay(unlockOverlay, hideCreditReminder, hideBulkCreditReminder);

        // Prevent the overlay in case the reminder box is checked.

        if ((candidateIds.length <= 1 && _hideCreditsState.hideCreditReminder) || (candidateIds.length > 1 && _hideCreditsState.hideBulkCreditReminder)) {
            unlock(candidateIds);
            return false;
        }

        _unlockState.candidateIds = candidateIds;

        var linkWidth = positioningElement.width();
        var linkCenterOffset = Math.round(linkWidth / 2);
        var containerOffset = positioningElement.offset().left - 202; //offset;
        var arrowOffset = 16;
        var newOffsetLeft = linkCenterOffset + containerOffset - arrowOffset;
        var topOff = positioningElement.closest(".status-icons").find(".contact-locked-icon").offset().top + 32;

        unlockOverlay.find("#unlock-reminder").removeAttr("checked");

        unlockOverlay.show();
        unlockOverlay.offset({ top: topOff, left: newOffsetLeft });
        showOverlay();
    }

    showLoginOverlay = function (action) {

        // Prevent the overlay in case the reminder box is checked.

        var loginOverlay = $(".login-overlay");
        showOverlay(loginOverlay);

        loginOverlay.css("top", "50%");
        loginOverlay.css("left", "50%");
        loginOverlay.css("margin-top", "-100px");
        loginOverlay.css("margin-left", "-215px");
        loginOverlay.css("position", "fixed");

        var description = "";
        if (action == "unlock") {
            description = "You need to be logged in and have purchased access to unlock a candidate's details";
        }
        else if (action == "flag") {
            description = "You need to be logged in to flag a candidate";
        }
        else if (action == "block") {
            description = "You need to be logged in to block a candidate";
        }
        else if (action == "notes") {
            description = "You need to be logged in to create and view a candidate's notes";
        }
        else if (action == "addresults") {
            description = "You need to be logged in to add candidates to a folder";
        }
        else if (action == "addfolder") {
            description = "You need to be logged in to add a folder";
        }
        else if (action == "savesearch") {
            description = "You need to be logged in save your search";
        }
        else if (action == "savealert") {
            description = "You need to be logged in to create an email alert";
        }

        loginOverlay.find(".overlay-text span").html(description);

        // Cancel

        loginOverlay.find("#login-cancel").click(function () {
            hideOverlay($(".login-overlay"));
        });

        return false;
    }

    displayBrowseOption = function () {
        $("#file_uploader").show();
    }

    showSendMessageOverlay = function (canWithCreditCandidateIds, canWithoutCreditCandidateIds, actionType) {

        var sendMessageOverlay = $(".send-message-overlay");
        showOverlay(sendMessageOverlay);

        sendMessageOverlay.css("top", "50%");
        sendMessageOverlay.css("left", "50%");
        sendMessageOverlay.css("margin-top", "-250px");
        sendMessageOverlay.css("margin-left", "-330px");
        sendMessageOverlay.css("position", "fixed");

        if (canWithoutCreditCandidateIds.length == 0 && canWithCreditCandidateIds.length == 1) {
            sendMessageOverlay.find(".send-message-credits-alert").show();
            sendMessageOverlay.find("#send-message-credits-desc").html("This unlocks all of a candidate's personal and contact details.");
        }
        else if (canWithCreditCandidateIds.length > 0) {
            sendMessageOverlay.find(".send-message-credits-alert").show();
            if (canWithoutCreditCandidateIds.length == 0)
                sendMessageOverlay.find("#send-message-credits-desc").html("This unlocks all of the " + canWithCreditCandidateIds.length + " candidate's personal and contact details.");
            else
                sendMessageOverlay.find("#send-message-credits-desc").html("This unlocks " + canWithCreditCandidateIds.length + " of the " + (canWithCreditCandidateIds.length + canWithoutCreditCandidateIds.length) + " candidate's personal and contact details.");
        }
        else {
            sendMessageOverlay.find(".send-message-credits-alert").hide();
        }

        sendMessageOverlay.find(".buttons-holder").html("<input class='cancel-button button' id='cancel' name='cancel' type='button' value='Cancel' /><input class='send_button button' id='send-message' name='send-message' type='button' value='Send'/>");
        $("#send-message").removeClass("in-progress");

        // Updating To Field
        var candidateIds = new Array();
        var toCandidateList = "";
        var overlayTitle = "";
        if (actionType == "individual") {
            if ($(".resume-section").length == 0) {
                if (canWithCreditCandidateIds.length > 0) {
                    candidateIds.push(canWithCreditCandidateIds[0]);
                }
                if (canWithoutCreditCandidateIds.length > 0) {
                    candidateIds.push(canWithoutCreditCandidateIds[0]);
                }
            } else {
                candidateIds.push($(".resume-header").parent().attr("id"));
            }
            overlayTitle = "Email Candidate";
        }
        else {
            for (var i = 0; i < canWithCreditCandidateIds.length; i++) {
                candidateIds.push(canWithCreditCandidateIds[i]);
            }
            for (var i = 0; i < canWithoutCreditCandidateIds.length; i++) {
                candidateIds.push(canWithoutCreditCandidateIds[i]);
            }
            overlayTitle = "Email Candidates (" + candidateIds.length + ")";
        }
        // Updating overlay title
        sendMessageOverlay.find(".overlay-title-text").text(overlayTitle);
        // Updating "To" List
        var candidateName = "";
        for (var i = 0; i < candidateIds.length; i++) {
            if ($(".resume-section").length == 0) {
                var candidateNameObj = $("#" + candidateIds[i]).closest(".basic-details").find(".candidate-name").find("a");
            } else {
                var candidateNameObj = $("#" + candidateIds[i]).find(".basic-details").find(".candidate-name");
            }
            candidateName = ($(candidateNameObj).attr("title") == "") ? $(candidateNameObj).text() : candidateName = $(candidateNameObj).attr("title");
            if (i > 0) {
                toCandidateList = toCandidateList + ", ";
            }
            toCandidateList = toCandidateList + candidateName;
        }
        sendMessageOverlay.find("#sendMessageTo").html(toCandidateList);
        if (toCandidateList.length > 65) {
            sendMessageOverlay.find(".toggle-link-holder").show();
            sendMessageOverlay.find(".toggle-link-holder").click(function () {
                if ($(this).find(".icon").hasClass("down-icon")) {
                    sendMessageOverlay.find("#sendMessageTo").css("height", "100%");
                    $(this).find(".toggle-link").text("Less");
                } else {
                    sendMessageOverlay.find("#sendMessageTo").css("height", "15px");
                    $(this).find(".toggle-link").text("More");
                }
                $(this).find(".icon").toggleClass("down-icon").toggleClass("up-icon");
            });
        }
        // Updating "From" List
        sendMessageOverlay.find("#sendMessageFrom").val(currentUserEmailAddr);
        // Updating default subject
        sendMessageOverlay.find("#sendMessageSubject").val("Job opportunity");
        // Updating default Email body
        emailBody = "Dear ";

        if (actionType == "individual") {
            var candidateName = "";
            if ($(".resume-section").length == 0) {
                var candidateNameObj = $("#" + candidateIds[0]).closest(".basic-details").find(".candidate-name").find("a");
            } else {
                var candidateNameObj = $("#" + candidateIds[0]).find(".basic-details").find(".candidate-name");
            }
            if ($(candidateNameObj).attr("title") == "") {
                candidateName = $(candidateNameObj).text();
            } else {
                candidateName = $(candidateNameObj).attr("title");
            }
            if (candidateName == "<Name hidden>") {
                emailBody = emailBody + '<img src="' + candidateFirstNameImgUrl + '" class="first-name" />';
            } else {
                emailBody = emailBody + candidateName;
            }
        } else {
            emailBody = emailBody + '<img src="' + candidateFirstNameImgUrl + '" class="first-name" />';
        }
        emailBody = emailBody + "<br /><br /><br /><br />Regards,<br /><br />" + currentUserFullName;
        tinyMCE.execCommand('mceSetContent', false, emailBody);

        // Send

        sendMessageOverlay.find("input[name=send-message]").click(function () {
            if ($(this).hasClass("in-progress")) {
                alert("Attachment upload is in progress. Please wait.");
                return false;
            }
            var subject = $(".send-message-overlay").find("#sendMessageSubject").val();
            var body = $('#sendMessageBody').tinymce().getContent();
            var from = $(".send-message-overlay").find("#sendMessageFrom").val();
            var sendCopy = $(".send-message-overlay").find("#sendCopy").is(":checked");
            var attachmentIds = new Array();
            $("#files").find(".file").each(function () {
                attachmentIds.push($(this).attr("id"));
            });
            sendMessage(subject, body, from, candidateIds, sendCopy, attachmentIds);
            $(".send-message-overlay").find("#sendCopy").attr("checked", "checked"); // Setting the Send Copy checkbox
            $("#files").empty(); // Removing any attachments
            hideOverlay($(".send-message-overlay"));
        });

        // Cancel

        sendMessageOverlay.find(".overlay-title").find("span.close-icon").click(function () {
            $(".send-message-overlay").find("#sendCopy").attr("checked", "checked"); // Setting the Send Copy checkbox
            $("#files").empty(); // Removing any attachments
            hideOverlay($(".send-message-overlay"));
            return false;
        });

        sendMessageOverlay.find("#cancel").click(function () {
            $(".send-message-overlay").find("#sendCopy").attr("checked", "checked"); // Setting the Send Copy checkbox
            $("#files").empty(); // Removing any attachments
            hideOverlay($(".send-message-overlay"));
            return false;
        });

        return false;
    }

    showSendRejectionMessageOverlay = function (canWithoutCreditCandidateIds, actionType) {

        var sendMessageOverlay = $(".send-message-overlay");
        showOverlay(sendMessageOverlay);

        sendMessageOverlay.css("top", "50%");
        sendMessageOverlay.css("left", "50%");
        sendMessageOverlay.css("margin-top", "-250px");
        sendMessageOverlay.css("margin-left", "-330px");
        sendMessageOverlay.css("position", "fixed");

        sendMessageOverlay.find(".send-message-credits-alert").hide();

        sendMessageOverlay.find(".buttons-holder").html("<input class='cancel-button button' id='cancel' name='cancel' type='button' value='Cancel' /><input class='send_button button' id='send-message' name='send-message' type='button' value='Send'/>");
        $("#send-message").removeClass("in-progress");

        // Updating To Field
        var candidateIds = new Array();
        var toCandidateList = "";
        var overlayTitle = "";
        if (actionType == "individual") {
            if (canWithoutCreditCandidateIds.length > 0) {
                candidateIds.push(canWithoutCreditCandidateIds);
            }
            overlayTitle = "Send rejection e-mail";
        }
        else {
            for (var i = 0; i < canWithoutCreditCandidateIds.length; i++) {
                candidateIds.push(canWithoutCreditCandidateIds[i]);
            }
            overlayTitle = "Send rejection e-mail (" + candidateIds.length + ")";
        }
        // Updating overlay title
        sendMessageOverlay.find(".overlay-title-text").text(overlayTitle);
        // Updating "To" List
        var candidateName = "";
        for (var i = 0; i < candidateIds.length; i++) {
            var candidateNameObj = $("#" + candidateIds[i]).closest(".basic-details").find(".candidate-name").find("a");
            candidateName = ($(candidateNameObj).attr("title") == "") ? $(candidateNameObj).text() : candidateName = $(candidateNameObj).attr("title");
            if (i > 0) {
                toCandidateList = toCandidateList + ", ";
            }
            toCandidateList = toCandidateList + candidateName;
        }
        sendMessageOverlay.find("#sendMessageTo").html(toCandidateList);
        if (toCandidateList.length > 65) {
            sendMessageOverlay.find(".toggle-link-holder").show();
            sendMessageOverlay.find(".toggle-link-holder").click(function () {
                if ($(this).find(".icon").hasClass("down-icon")) {
                    sendMessageOverlay.find("#sendMessageTo").css("height", "100%");
                    $(this).find(".toggle-link").text("Less");
                } else {
                    sendMessageOverlay.find("#sendMessageTo").css("height", "15px");
                    $(this).find(".toggle-link").text("More");
                }
                $(this).find(".icon").toggleClass("down-icon").toggleClass("up-icon");
            });
        }
        // Updating "From" List
        sendMessageOverlay.find("#sendMessageFrom").val(currentUserEmailAddr);
        // Updating default subject
        jobTitle = $("#" + candidateContext.jobAdId).text();
        sendMessageOverlay.find("#sendMessageSubject").val(jobTitle);
        // Updating default Email body
        emailBody = "Dear ";

        if (actionType == "individual") {
            var candidateName = "";
            var candidateNameObj = $("#" + candidateIds[0]).closest(".basic-details").find(".candidate-name").find("a");
            if ($(candidateNameObj).attr("title") == "") {
                candidateName = $(candidateNameObj).text();
            } else {
                candidateName = $(candidateNameObj).attr("title");
            }
            if (candidateName == "<Name hidden>") {
                emailBody = emailBody + '<img src="' + candidateFirstNameImgUrl + '" class="first-name" />';
            } else {
                emailBody = emailBody + candidateName;
            }
        } else {
            emailBody = emailBody + '<img src="' + candidateFirstNameImgUrl + '" class="first-name" />';
        }
        emailBody = emailBody + "<br /><br />Thank you for taking the time to apply for the role of " + jobTitle + " at " + currentOrgName + ".<br /><br />";
        emailBody = emailBody + "Unfortunately, your skills and experience were not close enough to this position's requirements to allow us to progress with your application.<br /><br />";
        emailBody = emailBody + "However, I still have access to your resume and may contact you in the future if a suitable opportunity arises.<br /><br />Yours sincerely,<br /><br />" + currentUserFullName;
        tinyMCE.execCommand('mceSetContent', false, emailBody);

        // Send

        sendMessageOverlay.find("input[name=send-message]").click(function () {
            if ($(this).hasClass("in-progress")) {
                alert("Attachment upload is in progress. Please wait.");
                return false;
            }
            var subject = $(".send-message-overlay").find("#sendMessageSubject").val();
            var body = $('#sendMessageBody').tinymce().getContent();
            var from = $(".send-message-overlay").find("#sendMessageFrom").val();
            var sendCopy = $(".send-message-overlay").find("#sendCopy").is(":checked");
            var attachmentIds = new Array();
            $("#files").find(".file").each(function () {
                attachmentIds.push($(this).attr("id"));
            });
            sendMessage(subject, body, from, candidateIds, sendCopy, attachmentIds);
            $(".send-message-overlay").find("#sendCopy").attr("checked", "checked"); // Setting the Send Copy checkbox
            $("#files").empty(); // Removing any attachments
            hideOverlay($(".send-message-overlay"));
        });

        // Cancel

        sendMessageOverlay.find(".overlay-title").find("span.close-icon").click(function () {
            $(".send-message-overlay").find("#sendCopy").attr("checked", "checked"); // Setting the Send Copy checkbox
            $("#files").empty(); // Removing any attachments
            hideOverlay($(".send-message-overlay"));
            return false;
        });

        sendMessageOverlay.find("#cancel").click(function () {
            $(".send-message-overlay").find("#sendCopy").attr("checked", "checked"); // Setting the Send Copy checkbox
            $("#files").empty(); // Removing any attachments
            hideOverlay($(".send-message-overlay"));
            return false;
        });

        return false;
    }

    showRejectionMessageWarningOverlay = function (canWithoutCreditCandidateIds, actionType) {
        var rmwOverlay = $(".rejectionMessageWarningOverlay");
        showOverlay(rmwOverlay);

        if (canWithoutCreditCandidateIds.length > 1) rmwOverlay.find(".overlay-title-text").text("Send rejection e-mail (" + canWithoutCreditCandidateIds.length + ")");

        rmwOverlay.css("top", "50%");
        rmwOverlay.css("left", "50%");
        rmwOverlay.css("margin-top", "-100px");
        rmwOverlay.css("margin-left", "-215px");
        rmwOverlay.css("position", "fixed");

        rmwOverlay.find(".close-icon").click(function () {
            hideOverlay(rmwOverlay);
            return false;
        });

        rmwOverlay.find("input[name='rmw-ok']").click(function () {
            hideOverlay(rmwOverlay);
            candidateIDs = new Array();
            $.each(canWithoutCreditCandidateIds, function () {
                if ($(".candidate_search-result[data-memberid='" + this.toString() + "']").find(".search-result-header").hasClass("candidate-source-applied"))
                    candidateIDs.push(this.toString());
                else
                    $("#" + this.toString()).removeAttr("checked").trigger("change");
            });
            showSendRejectionMessageOverlay(candidateIDs, actionType);
            return false;
        });
        rmwOverlay.find("input[name='rmw-cancel']").click(function () {
            hideOverlay(rmwOverlay);
        });
        return false;
    }

    $.fn.makeSearchTipsOverlay = function () {

        var p = {
            generatedOverlayId: "only-tips-overlay",
            generatedOverlayClass: "overlay-bg",
            positionedElementClass: "tips-overlay"
        };

        // Invoke the overlay.

        $(this).click(function () {

            var topOff = 0;
            var leftOff = 0;
            /*$("." + p.positionedElementClass).show();*/

            var overlay = $("#" + p.generatedOverlayId);
            if (overlay.length == 0) {
                $("." + p.positionedElementClass).offset({ top: topOff, left: leftOff });
                overlay = $(document.createElement("div"));
                overlay.attr("id", p.generatedOverlayId);
                overlay.addClass(p.generatedOverlayClass);
                overlay.css("height", ($("body").get(0).offsetHeight) + "px");    // Hack! There ought to be a CSS way to handle this.
                if ($("body").find("#container").find("#background-overlay").css("display") == "block") {
                    $("body").find("#container").find("#background-overlay").after(overlay);
                } else {
                    $("body").find("#container").prepend(overlay);
                }

            } else {
                overlay.show();
            }
            /*$("." + p.positionedElementClass).center();*/

            var tipsOverlay = $(".tips-overlay");
            tipsOverlay.show();
            tipsOverlay.css("top", "50%");
            tipsOverlay.css("left", "50%");
            tipsOverlay.css("margin-top", "-110px");
            tipsOverlay.css("margin-left", "-200px");
            tipsOverlay.css("position", "fixed");

            return false;
        });

        // Hide the overlay on click of close button.

        $("." + p.positionedElementClass).find(".overlay-title").find("span.close-icon").click(function () {
            $("#" + p.generatedOverlayId).hide();
            $("." + p.positionedElementClass).hide();
            return false;
        });
    }

    /* Save Search results to folder Overlay */

    displayOverlayFolders = function (folders) {
        if (!(folders == null)) {
            $(".folder-overlay").find(".folder-existing").find(".folder-dropdown").append("<option class='folder-list-icon folder-icon private-folder-divider' value='' id='' disabled>Personal Folders</option>").append("<option class='folder-list-icon folder-icon shared-folder-divider' value='' id='' disabled>Organisation-wide Folders</option>");
            for (var i = 0; i < folders.length; i++) {
                var name = folders[i].Name;
                if (folders[i].Type != "Flagged") {
                    if (folders[i].Type == "Shortlist" && name == null) name = "My shortlist";
                    if (folders[i].Type == "Mobile") name = "My mobile favourites";
                    if (folders[i].Type == "Shortlist") {
                        $("<option class='folder-list-icon folder-icon' value='" + folders[i].Id + "' id='" + folders[i].Id + "' selected>" + name + "</option>").insertBefore(".folder-dropdown option.shared-folder-divider");
                    } else if (folders[i].Type == "Private" || folders[i].Type == "Mobile") {
                        $("<option class='folder-list-icon folder-icon' value='" + folders[i].Id + "' id='" + folders[i].Id + "'>" + name + "</option>").insertBefore(".folder-dropdown option.shared-folder-divider");
                    } else {
                        $(".folder-overlay").find(".folder-existing").find(".folder-dropdown").append("<option class='folder-list-icon folder-icon' value='" + folders[i].Id + "' id='" + folders[i].Id + "'>" + name + "</option>");
                    }
                }
            }
        }
    }

    showSaveSearchResultsOverlay = function (candidateIds, candidateContext) {

        var folderOverlay = $(".folder-overlay");
        showOverlay(folderOverlay);

        folderOverlay.css("top", "50%");
        folderOverlay.css("left", "50%");
        folderOverlay.css("margin-top", "-160px");
        folderOverlay.css("margin-left", "-225px");
        folderOverlay.css("position", "fixed");

        $(".folder-overlay").find(".overlay-title").css("display", "block");
        $(".folder-overlay").find(".overlay-title-text").text("Add results to folder");

        $(".folder-overlay").find(".overlay-text").html("<div class='folder-existing folder-overlay-section'></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-existing").append("<div class='folder-existing-disabled-overlay disabled-overlay'></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-existing").append("<div class='folderRadio'><span><input name='FolderExistence' id='FolderExists' value='true' type='radio'/> Existing Folder</span></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-existing").append("<div class='select_field field'><label>Choose Folder:</label><div class='select_control control'><select class='folder-dropdown'></select></div>");

        $(".folder-overlay").find(".overlay-text").append("<div class='section-separator'><span class='horizontal-separator'></span><span class='or-holder'> OR </span><span class='horizontal-separator'></span></div>");

        $(".folder-overlay").find(".overlay-text").append("<div class='folder-new folder-overlay-section'></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-new").append("<div class='folder-new-disabled-overlay disabled-overlay'></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-new").append("<div class='folderRadio'><span><input name='FolderExistence' id='FolderNew' value='flase' type='radio'/> New Folder</span></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-new").append("<div class='textbox_field field'><label>New Folder Name:</label><div class='textbox_control control'><input name='Name' maxlength='265' id='Name' class='' type='text' value='' size='50'></div></div>");
        $(".folder-overlay").find(".overlay-text").find(".folder-new").append("<div class='folderTypeRadio'><span><input name='FolderType' id='personal' value='false' type='radio'/> Personal</span><span><input name='FolderType' id='org-wide' value='true' type='radio'/> Organisation-wide</span></div>");

        $(".folder-overlay").find(".buttons-holder").html("<input class='cancel-button button' id='cancel' name='cancel' type='button' value='Cancel' /><input class='ok_button button' id='ok' name='ok' type='button' value='OK'/>");

        getFolders(displayOverlayFolders);

        $(".folder-overlay").find("input[name=FolderType]:checked").removeAttr("checked");
        $(".folder-overlay").find("input[name=FolderType][value='true']").attr("checked", "checked");

        $(".folder-overlay").find("input[name=FolderExistence]:checked").removeAttr("checked");
        $(".folder-overlay").find("input[name=FolderExistence][value='true']").attr("checked", "checked");
        $(".folder-overlay").find("input[name=FolderExistence]:checked").closest(".folder-overlay-section").find(".disabled-overlay").hide();

        /* Toggle between existing and new folders */
        $(".folder-overlay").find('input[name=FolderExistence]').change(function () {
            $(".folder-overlay").find(".disabled-overlay").show();
            $(this).closest(".folder-overlay-section").find(".disabled-overlay").hide();
        });

        // Close when cancelled.

        $(".folder-overlay").find("#cancel").click(function () {
            $("#personal-folders").find(".ajax-loader").remove();
            $("#org-wide-folders").find(".ajax-loader").remove();
            hideOverlay($(".folder-overlay"));
            return false;
        });

        $(".folder-overlay").find(".overlay-title").find("span.close-icon").click(function () {
            $("#personal-folders").find(".ajax-loader").remove();
            $("#org-wide-folders").find(".ajax-loader").remove();
            hideOverlay($(".folder-overlay"));
            return false;
        });

        // Create.

        $(".folder-overlay").find("#ok").click(function () {
            if ($(".folder-overlay").find('input[name=FolderExistence]:checked').attr("id") == "FolderNew") {
                if ($(".folder-overlay").find('input[type=text]').val() == "") {
                    alert("Please enter a folder name");
                    return false;
                } else {
                    $("#personal-folders").prepend("<span class='ajax-loader'></span>");
                    $("#org-wide-folders").prepend("<span class='ajax-loader'></span>");
                    var name = $(".folder-overlay").find('input[type=text]').val();
                    var isShared = $(".folder-overlay").find('input[name=FolderType]:checked').val();
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: apiNewFolderUrl + "?name=" + name + "&isShared=" + isShared,
                        success: function (data) {
                            if (!(data == null)) {
                                if (data.Success) {
                                    if (candidateIds instanceof Array) {
                                        addCandidatesToFolder(data.Folder.Id, candidateIds);
                                    }
                                    updateFolders(candidateContext);
                                    updateResults(false);
                                } else {
                                    alert(data.Errors[0].Message);
                                }
                            }
                        },
                        complete: function () {
                            $("#personal-folders").find(".ajax-loader").remove();
                            $("#org-wide-folders").find(".ajax-loader").remove();
                            hideOverlay($(".folder-overlay"));
                            return false;
                        }
                    });
                }
            } else {
                var folderId = $(".folder-overlay").find(".folder-existing").find(".folder-dropdown").val();
                if (candidateIds instanceof Array) {
                    addCandidatesToFolder(folderId, candidateIds);
                }
                hideOverlay($(".folder-overlay"));
                return false;
            }
        });
    }

    /* for IE7 */
    $(document).ready(function () {
        if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
            $(".close-icon").addClass("IE7");
        }
    });
    showSBNOverlay = function (action) {

        var sbnOverlay = $("." + action + "-overlay");
        showOverlay(sbnOverlay);

        sbnOverlay.css("top", "50%");
        sbnOverlay.css("left", "50%");
        sbnOverlay.css("margin-top", "-100px");
        sbnOverlay.css("margin-left", "-215px");
        sbnOverlay.css("position", "fixed");

        // OK

        sbnOverlay.find("input[name='sbn-ok']").click(function () {
            hideOverlay(sbnOverlay);
        });

        return false;
    }

    showCreateEmailAlertPromptOverlay = function () {
        var overlay = $(".shadow.createemailalertprompt");
        var offset = $(".new-candidate-alert-action").offset();
        var topOff = offset.top - 30;
        var leftOff = offset.left + 249;
        overlay.show();
        overlay.offset({ top: topOff, left: leftOff });
        showOverlay();
        overlay.find(".close-icon").click(function () {
            hideOverlay(overlay);
        });
    }

})(jQuery);