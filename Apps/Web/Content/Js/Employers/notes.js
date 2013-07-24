(function($) {

    newNote = function(candidateIds) {
        if (candidateIds instanceof Array) {
            if ($(".bulk-notes_holder").find(".add-notes_section").find(".notes-textarea").val() == "") {
                alert("Please enter some notes!");
            }
            else {
                employers.api.newNote(
                    candidateIds,
                    $(".bulk-notes_holder").find(".add-notes_section").find("#org-wide").is(":checked"),
                    $(".bulk-notes_holder").find(".add-notes_section").find(".notes-textarea").val(),
                    function(data) {
                        updateResults(false);
                    },
                    null,
                    function() {
                        $(".bulk-notes_holder").find(".add-notes_section").find(".notes-textarea").val("");
                        $(".bulk-notes_holder").hide();
                    });
            }
        }
        else {
            var candidateId = candidateIds;
            if ($("#notes-" + candidateId).find(".add-notes_section").find(".notes-textarea").val() == "") {
                alert("Please enter some notes!");
            }
            else {
                var notes = $("#notes-" + candidateId);
                employers.api.newNote(
                    new Array(candidateId),
                    notes.find(".add-notes_section").find("#org-wide").is(":checked"),
                    notes.find(".add-notes_section").find(".notes-textarea").val(),
                    function() {
                        $("#notes-" + candidateId).closest(".result-container").find(".notes-detail_holder").remove();
                        $("#notes-" + candidateId).closest(".result-container").find(".link-holder").remove();
                        displayAllNotes(candidateId);
                    },
                    null,
                    function() {
                        var notes = $("#notes-" + candidateId);
                        notes.find(".add-notes_section").find(".notes-textarea").val("");
                        notes.closest(".notes-content").find(".add-notes_button").show();
                        notes.closest(".notes-content").find(".add-notes_section").hide();
                    });
            }
        }
    }

    editNote = function(noteId) {
        if ($("#edit-" + noteId).find(".notes-textarea").val() == "") {
            alert("Please enter some notes!");
        }
        else {
            var edit = $("#edit-" + noteId);
            employers.api.editNote(
                noteId,
                edit.find("#org-wide").is(":checked"),
                edit.find(".notes-textarea").val(),
                function(note) {
                    $("#" + noteId).find(".note-data").text(note.Text);
                    $("#" + noteId).find(".note-type").text(note.IsShared ? "Organisation-wide" : "Personal");
                },
                null,
                function() {
                    $("#" + noteId).show();
                    $("#edit-" + noteId).remove();
                });
        }
    }

    deleteNote = function(noteId) {
        employers.api.deleteNote(
            noteId,
            function() {
                var container = $("#" + noteId).closest(".result-container");
                var candidateId = container.attr("data-memberid");
                container.find(".notes-detail_holder").remove();
                container.find(".link-holder").remove();
                displayAllNotes(candidateId);
            });
    }

    displayAllNotes = function(candidateId) {
        employers.api.getNotes(
            candidateId,
            function(notes) {
                $(".result-container").find("#" + candidateId).closest(".result-container").find(".notes-content").append("<div class='notes-detail_holder'></div>");
                for (var i = 0; i < notes.length; i++) {
                    var note = notes[i];
                    var noteType = note.IsShared ? "Organisation-wide" : "Personal";
                    var creator = (note.CreatedBy == null) ? "You" : note.CreatedBy;
                    var updatedTime = new Date(parseInt(note.UpdatedTime.substr(6)));
                    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                    $(".result-container").find("#" + candidateId).closest(".result-container").find(".notes-detail_holder").append("<div class='notes-detail' id=" + note.Id + "><span class='note-date'>" + updatedTime.getDate() + " " + months[updatedTime.getMonth()] + " " + updatedTime.getFullYear() + "</span><span class='note-owner'>" + creator + "</span><span class='notes-action_holder'><span class='note-type'>" + noteType + "</span></span><div class='note-data'>" + note.Text + "</div></div>");
                    if (note.CanUpdate) {
                        $("#" + note.Id).find(".notes-action_holder").append("<a class='edit-action' href='javascript: void(0);'>Edit</a>");
                    }
                    if (note.CanDelete) {
                        $("#" + note.Id).find(".notes-action_holder").append("<a class='delete-action' href='javascript: void(0);'>Delete</a>");
                    }
                    $("#" + note.Id).find(".notes-action_holder").hide();
                    $("#" + note.Id).mouseover(function() {
                        $(this).find(".notes-action_holder").show();
                    });
                    $("#" + note.Id).mouseout(function() {
                        $(this).find(".notes-action_holder").hide();
                    });
                }

                /* Updating Notes count */
                $(".result-container").find("#" + candidateId).closest(".result-container").find(".has-notes-icon").parent().find(".notes-count").text(notes.length);
                var notesCountTitleText = "There are " + notes.length + " note";
                if (!(notes.length == 1)) {
                    notesCountTitleText = notesCountTitleText + "s";
                }
                notesCountTitleText = notesCountTitleText + " for this candidate";
                $(".result-container").find("#" + candidateId).closest(".result-container").find(".has-notes-icon").attr("title", notesCountTitleText);

                $(".result-container").find("#" + candidateId).closest(".result-container").find(".notes-detail_holder").find(".edit-action").click(function() {
                    var noteId = $(this).closest(".notes-detail").attr("id");

                    $("#" + noteId).after("<div class='edit-notes_section' id='edit-" + noteId + "'><div class='notes-textarea_holder'><textarea class='notes-textarea'></textarea></div><div class='buttons_holder'><input class='save-button button' name='save-note' type='button' value='Save'/><input class='cancel-button button' name='cancel' type='button' value='Cancel' /></div><div class='note-type_holder'><span><input name='" + noteId + "_noteType' id='personal' value='false' type='radio'/> Personal</span><span><input name='" + noteId + "_noteType' id='org-wide' value='true' type='radio'/> Organisation-wide <img src='" + helpIconUrl + "' class='help js_" + noteId + "_tooltip' data-tooltip='A note marked as Organisation-wide will be visible to any of your colleagues who work for the same organisation as you. They will note be able to edit or delete your notes.' /></span></div></div>");
                    $(".js_" + noteId + "_tooltip").addTooltip();
                    $("#edit-" + noteId).find(".notes-textarea").val($("#" + noteId).find(".note-data").text());
                    if ($("#" + noteId).find(".note-type").text() == "Personal") {
                        $("#edit-" + noteId).find(".note-type_holder").find("#personal").attr("checked", "checked");
                    } else {
                        $("#edit-" + noteId).find(".note-type_holder").find("#org-wide").attr("checked", "checked");
                    }
                    $("#edit-" + noteId).find(".cancel-button").click(function() {
                        $("#" + noteId).show();
                        $("#edit-" + noteId).remove();
                    });
                    $("#edit-" + noteId).find(".save-button").click(function () {
                        editNote(noteId);
                    });

                    $("#" + noteId).hide();
                });
                $(".result-container").find("#" + candidateId).closest(".result-container").find(".notes-detail_holder").find(".delete-action").click(function() {
                    var noteId = $(this).closest(".notes-detail").attr("id");
                    deleteNote(noteId);
                });

                /* Show more link */
                if (notes.length > 3) {
                    $("#notes-" + candidateId).makeNotesCollapsibleDisplay(3, 5);
                }
            },
            null,
            function() {
                if ($("#notes-" + candidateId).closest(".compact_search-results_ascx").length == 0) {
                    $("#notes-" + candidateId).closest(".result-container").find(".has-notes-icon").text("Hide notes");
                }
                $("#notes-" + candidateId).closest(".result-container").find(".has-notes-icon").addClass("notes-displayed");
                return false;
            });
    }

    displayNotes = function(element, candidateId) {
        if (element.hasClass("notes-displayed")) {
            if (element.closest(".compact_search-results_ascx").length == 0) {
                element.text("Notes");
            }
            element.toggleClass("notes-displayed");
            element.closest(".result-container").find(".notes-detail_holder").remove();
            element.closest(".result-container").find(".link-holder").remove();
            $("#notes-" + candidateId).hide();
            return false;
        }
        else {
            $("#notes-" + candidateId).show();
            displayAllNotes(candidateId);
        }
    }

    $.fn.makeNotesCollapsibleDisplay = function(startIndex, stepIncrement) {
        $(this).find(".link-holder").remove();
        noOfNotes = $(this).find(".notes-detail").length;

        /* Displaying only <startIndex> number of notes on page load */
        if (noOfNotes > startIndex) {
            $(this).find(".notes-detail").slice(startIndex).hide();
            $(this).append('<div class="link-holder"><a href="javascript:void(0)" class="left-link"><span class="left-link"><small>Show more</small></span><span class="arrow-icon icon-down"/></a><a href="javascript:void(0)" class="right-link"></a></div>');
        }

        /* On click of left link */
        $(this).find("a.left-link").click(function() {
            var hiddenStartIndex = $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail:hidden:first").index();
            var newHiddenStartIndex = 0;
            var newHiddenCount = 0;
            $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail").show();
            /* In case of Show more */
            if ($(this).closest(".notes-content").find("a.left-link span.arrow-icon").hasClass("icon-down")) {
                if (!(hiddenStartIndex == -1)) {
                    /*if ((hiddenStartIndex > startIndex) && (hiddenStartIndex < stepIncrement)) {
                    hiddenStartIndex = 0;
                    }*/
                    $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail").slice(hiddenStartIndex + stepIncrement).hide();
                }

                $(this).closest(".notes-content").find("a.left-link span.left-link small").text("Show less");
                $(this).closest(".notes-content").find("a.left-link span.arrow-icon").removeClass("icon-down").addClass("icon-up");

                newHiddenStartIndex = $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail:hidden:first").index();
                if (!(newHiddenStartIndex == -1)) {
                    newHiddenCount = noOfNotes - newHiddenStartIndex;

                    if (!(newHiddenCount == 0)) {
                        var displayCount = 0;
                        if (newHiddenCount >= stepIncrement) {
                            displayCount = stepIncrement;
                        } else if (newHiddenCount >= startIndex) {
                            displayCount = newHiddenCount - startIndex;
                        } else {
                            displayCount = newHiddenCount;
                        }
                        $(this).closest(".notes-content div.link-holder").find("a.right-link").empty();
                        $(this).closest(".notes-content div.link-holder").find("a.right-link").append('<span class="left-link"><small>Show ' + displayCount + ' more</small></span><span class="arrow-icon icon-down"/>');
                    }

                } else {
                    $(this).closest(".toggle-checkboxes div.link-holder").find("a.right-link").empty();
                }
            } else { /* In case of Show less */
                $(this).closest(".notes-content a.left-link").find("span.left-link small").text("Show more");
                $(this).closest(".notes-content a.left-link").find("span.arrow-icon").removeClass("icon-up").addClass("icon-down");
                $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail").slice(startIndex).hide();
                $(this).closest(".notes-content div.link-holder").find("a.right-link").empty();
                //return true;
            }
        });

        /* On click of right link */
        $(this).find("a.right-link").click(function() {
            var hiddenStartIndex = $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail:hidden:first").index();
            var newHiddenStartIndex = 0;
            var newHiddenCount = 0;
            $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail").show();

            if ($(this).closest(".notes-content").find("a.right-link span.arrow-icon").hasClass("icon-down")) {
                if (!(hiddenStartIndex == -1)) {
                    if ((hiddenStartIndex >= startIndex) && (hiddenStartIndex < stepIncrement)) {
                        hiddenStartIndex = 0;
                    }
                    $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail").slice(hiddenStartIndex + stepIncrement).hide();
                }

                $(this).closest(".notes-content").find("a.left-link span.left-link small").text("Show less");
                $(this).closest(".notes-content").find("a.left-link span.arrow-icon").removeClass("icon-down").addClass("icon-up");

                newHiddenStartIndex = $(this).closest(".notes-content").find(".notes-detail_holder .notes-detail:hidden:first").index();
                if (!(newHiddenStartIndex == -1)) {
                    newHiddenCount = noOfNotes - newHiddenStartIndex;

                    if (!(newHiddenCount == 0)) {
                        var displayCount = 0;
                        if (newHiddenCount >= stepIncrement) {
                            displayCount = stepIncrement;
                        } else {
                            displayCount = newHiddenCount;
                        }
                        $(this).closest(".notes-content div.link-holder").find("a.right-link").empty();
                        $(this).closest(".notes-content div.link-holder").find("a.right-link").append('<span class="left-link"><small>Show ' + displayCount + ' more</small></span><span class="arrow-icon icon-down"/>');
                    }

                } else {
                    $(this).closest(".notes-content div.link-holder").find("a.right-link").empty();
                }
            }
        });
    }

})(jQuery);