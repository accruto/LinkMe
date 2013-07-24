(function($) {
    onClickFlag = function (element) {
        (function($) {
            $(element).updateCandidateFlag(candidateContext);
        })(jQuery);
    }

    onClickUnlock = function (element, candidateId) {
        (function($) {
            showUnlockOverlay($(element), new Array(candidateId), candidateContext.hideCreditReminder, candidateContext.hideBulkCreditReminder);
        })(jQuery);
    }

    onClickUnlimitedUnlock = function (candidateId) {
        (function($) {
            unlock(new Array(candidateId));
        })(jQuery);
    }
	
	function generatePromptHtml(operation, candidateId) {
		var promptRegion = $("#prompt-region" + candidateId);
		if (promptRegion.html() == "" || promptRegion.attr("class") != "candidate_search-result_" + operation) {
			promptRegion.attr("class", "candidate_search-result_" + operation);
			var candidateName = promptRegion.attr("candidateName");
			var jobAdId = promptRegion.attr("jobAdId");
			var promptText = "";
			var undoOperation = "";
			var paramList = "";
			var undoText = "";
			switch (operation) {
				case "blocked":
					if ($(".tabs-container").hasClass("suggest-candidate")) 
						promptText = candidateName + " will no longer appear in this list of Suggested Candidates.";
					else
						promptText = candidateName + " will no longer appear in results for this search.";
					undoOperation = "onClickRestoreCandidate";
					break;
				case "shortlisted":
					promptText = candidateName + " has been added to the Shortlist for this job.";
					undoOperation = "onClickUndoShortlistCandidate";
					break;
				case "rejected":
					promptText = candidateName + " has been moved to the Rejected tab.";
					undoOperation = "onClickUndoRejectCandidate";
					break;
				case "removed":
					promptText = candidateName + " has been removed from the list of candidates for this job.";
					undoOperation = "onClickUndoRemoveCandidate";
					break;
			}
			if (operation == "blocked") paramList = "'" + candidateId + "'";
			else paramList = "'" + jobAdId + "', '" + candidateId + "'";
			if (operation == "removed") undoText = "Undo remove";
			else undoText = "Undo " + operation.substring(0, operation.length - 2);
			var strPromptHtml = "<div class='restore-holder'><a href='javascript:void(0);' class='restore-icon' onclick=\"" + undoOperation + "(" + paramList + ");\">" + undoText + "</a></div><div class='" + operation + "-text'>" + promptText + "</div>";
			if (operation == "blocked") strPromptHtml += "<div class='not-enough'> Not enough? <a href='javascript:void(0);' class='block-link' onclick=\"onClickPermanentlyBlockCandidate('" + candidateId + "');\">Block " + candidateName + " for all future searches</a></div>";
			promptRegion.html(strPromptHtml);
		}
	}

    onClickBlockCandidate = function (candidateId) {
        (function($) {
			generatePromptHtml("blocked", candidateId);
            temporarilyBlockCandidate(candidateId);
        })(jQuery);
    }

    onClickPermanentlyBlockCandidate = function (candidateId) {
        (function($) {
            permanentlyBlockCandidate(candidateId, candidateContext);
        })(jQuery);
    }
    
    onClickRestoreCandidate = function (candidateId) {
        (function($) {
            restoreTemporarilyBlockedCandidate(candidateId, "Search");
        })(jQuery);
    }

    onClickRestoreCandidateFromBlockList = function (candidateId) {
        (function($) {
            if ($(".blocklist_aspx").hasClass("blocklist-current_aspx")) {
                restoreTemporarilyBlockedCandidate(candidateId, "BlockList");
            }
            else {
                restorePermanentlyBlockedCandidate(candidateId);
            }
        })(jQuery);
    }
    
    onClickDisplayNotes = function (element, candidateId) {
        (function($) {
            displayNotes($(element), candidateId);
			contentExpandedInCompactView(element);
        })(jQuery);
    }

    onClickAddNote = function (element) {
        (function($) {
            $(element).closest(".notes-content").find(".add-notes_section").show();
            $(element).closest(".notes-content").find(".add-notes_section").find("#org-wide").attr("checked", "checked");
            $(element).closest(".notes-content").find(".add-notes_button").hide();
        })(jQuery);
    }
    
    onClickCancelAddNote = function (element) {
        (function($) {
            $(element).closest(".notes-content").find(".add-notes_button").show();
            $(element).closest(".notes-content").find(".add-notes_section").hide();
        })(jQuery);
    }

    onClickSaveNote = function (element) {
        (function($) {
            newNote($(element).closest(".notes-content").attr("id").substring(6));
        })(jQuery);
    }

    onClickCancelAddBulkNotes = function () {
        (function($) {
            $(".bulk-notes_holder").hide();
        })(jQuery);
    }
	
	function updateStatusCount(jobAdId, statusCounts) {
		(function($) {
			$("#" + jobAdId).parent().find("span.new-candidates-count").text(statusCounts.New);
			$("#" + jobAdId).parent().find("span.short-listed-candidates-count").text(statusCounts.ShortListed);
			$("#" + jobAdId).parent().find("span.rejected-candidates-count").text(statusCounts.Rejected);
			$(".new-category-tab .new-candidates-count").text(statusCounts.New);
			$(".shortlisted-category-tab .shortlisted-candidates-count").text(statusCounts.ShortListed);
			$(".rejected-category-tab .rejected-candidates-count").text(statusCounts.Rejected);
		})(jQuery);
	}

    onClickShortlistCandidate = function (jobAdId, candidateId) {
        (function($) {
			generatePromptHtml("shortlisted", candidateId);
			employers.api.shortlistCandidatesForJobAd(
                jobAdId,
                new Array(candidateId),
                function(response) {
					$("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_shortlisted").show();
                    $("#" + candidateId).closest(".candidate_search-result").hide();
					updateStatusCount(jobAdId, response.JobAd.ApplicantCounts);
                });
            })(jQuery)
    }

    onClickRemoveCandidate = function (jobAdId, candidateId) {
        (function($) {
			generatePromptHtml("removed", candidateId);
			employers.api.removeCandidatesFromJobAd(
                jobAdId,
                new Array(candidateId),
                function(response) {
					$("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_removed").show();
                    $("#" + candidateId).closest(".candidate_search-result").hide();
					updateStatusCount(jobAdId, response.JobAd.ApplicantCounts);
                });
            })(jQuery)
    }
	
	onClickRejectCandidate = function (jobAdId, candidateId) {
		(function($) {
			generatePromptHtml("rejected", candidateId);
			employers.api.rejectCandidatesForJobAd(
				jobAdId,
				new Array(candidateId),
				function(response) {
					$("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_rejected").show();
                    $("#" + candidateId).closest(".candidate_search-result").hide();
					updateStatusCount(jobAdId, response.JobAd.ApplicantCounts);
				}
			);
		})(jQuery);
	}

    onClickUndoShortlistCandidate = function (jobAdId, candidateId) {
        (function($) {
            employers.api.undoShortlistCandidatesForJobAd(
                jobAdId,
                new Array(candidateId),
				$(".category-indicator").attr("currentCategory"),
                function(response) {
					$("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_shortlisted").hide();
                    $("#" + candidateId).closest(".candidate_search-result").show();
					updateStatusCount(jobAdId, response.JobAd.ApplicantCounts);
                });
            })(jQuery)
    }

    onClickUndoRejectCandidate = function (jobAdId, candidateId) {
        (function($) {
            employers.api.undoRejectCandidatesForJobAd(
                jobAdId,
                new Array(candidateId),
				$(".category-indicator").attr("currentCategory"),
                function(response) {
					$("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_rejected").hide();
                    $("#" + candidateId).closest(".candidate_search-result").show();
					updateStatusCount(jobAdId, response.JobAd.ApplicantCounts);
                });
            })(jQuery)
    }

    onClickUndoRemoveCandidate = function (jobAdId, candidateId) {
        (function($) {
            employers.api.undoRemoveCandidatesFromJobAd(
                jobAdId,
                new Array(candidateId),
                function(response) {
                    $("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_removed").hide();
                    $("#" + candidateId).closest(".candidate_search-result").show();
					updateStatusCount(jobAdId, response.JobAd.ApplicantCounts);
                });
            })(jQuery)
    }

    onClickSaveBulkNotes = function () {
        (function($) {
            newNote($("#hlBulkAddNotes").data("candidateIds"));
        })(jQuery);
    }
	
	contentExpandedInCompactView = function (element) {
		if ($(element).closest(".search-result").find("> div:visible").length > 1)
			$(element).closest(".search-result-body").addClass("expanded");
		else $(element).closest(".search-result-body").removeClass("expanded");
	}

})(jQuery);