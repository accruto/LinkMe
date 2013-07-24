(function($) {

    initializeBlockLists = function() {
        employers.api.getBlockLists(false, displayBlockLists);
    }

    updateBlockLists = function() {

        // Clear all that is there.

        $("#blocklists").find("ul").empty();

        // Reset the cache and display again.

        employers.api.getBlockLists(true, displayBlockLists);
    }

    displayBlockLists = function(blockLists) {
        if (!(blockLists == null)) {
            for (var i = 0; i < blockLists.length; i++) {
                if (blockLists[i].Type == "Temporary") {
                    $("#blocklists").find("ul").append("<li><div class='blocklist-action'></div><a class='temporary-blocklist-action' href='" + temporaryBlockListsUrl + "' id=" + blockLists[i].Id + ">" + temporaryBlockListName + "</a> <span class='count-holder'>(<span class='count'>" + blockLists[i].Count + "</span>)</span></li>");
                }
                else if (blockLists[i].Type == "Permanent") {
                    $("#blocklists").find("ul").append("<li><div class='blocklist-action'></div><a class='permanent-blocklist-action' href='" + permanentBlockListsUrl + "' id=" + blockLists[i].Id + ">" + permanentBlockListName + "</a> <span class='count-holder'>(<span class='count'>" + blockLists[i].Count + "</span>)</span></li>");
                }
            }
			if (bHideTemporaryBlockList) $(".temporary-blocklist-action").parent().before($(".permanent-blocklist-action").parent()).hide();
        }
    }

    removeCandidatesFromBlockList = function(blockListId, candidateIds) {
        employers.api.removeCandidatesFromBlockList(
            blockListId,
            candidateIds,
            function(count) {
                $(".blocklists_ascx").find("#" + blockListId).parent().find(".count").text(count);
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            });
    }

    temporarilyBlockCandidate = function(candidateId) {
        employers.api.temporarilyBlockCandidates(
            new Array(candidateId),
            function(count) {
                $(".blocklists_ascx").find(".temporary-blocklist-action").parent().find(".count").text(count);
                $("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_blocked").show();
                $("#" + candidateId).closest(".candidate_search-result").hide();
            });
    }

    temporarilyBlockCandidates = function(candidateIds) {
        employers.api.temporarilyBlockCandidates(
            candidateIds,
            function(count) {
                $(".blocklists_ascx").find(".temporary-blocklist-action").parent().find(".count").text(count);
                for (var i = 0; i < candidateIds.length; i++) {
                    $("#" + candidateIds[i]).closest(".candidate_search-result").parent().find(".candidate_search-result_blocked").show();
                    $("#" + candidateIds[i]).closest(".candidate_search-result").hide();
                }
            });
    }

    permanentlyBlockCandidate = function(candidateId, candidateContext) {
        employers.api.permanentlyBlockCandidates(
            new Array(candidateId),
            function(count) {
                updateFolders(candidateContext);
                updateBlockLists();
            });
    }

    permanentlyBlockCandidates = function(candidateIds, candidateContext) {
        employers.api.permanentlyBlockCandidates(
            candidateIds,
            function(count) {
                updateFolders(candidateContext);
                updateBlockLists();
            });
    }

    restoreTemporarilyBlockedCandidate = function(candidateId, view) {
        var candidateName = $("#" + candidateId).closest(".candidate_search-result").find(".candidate-name").text();

        employers.api.restoreTemporarilyBlockedCandidates(
            new Array(candidateId),
            function(count) {
                $(".blocklists_ascx").find(".temporary-blocklist-action").parent().find(".count").text(count);
                if (view == "Search") {
                    $("#" + candidateId).closest(".candidate_search-result").parent().find(".candidate_search-result_blocked").hide();
                    $("#" + candidateId).closest(".candidate_search-result").show();
                }
                else {
                    $(".candidate-restored").find(".candidate-name").text(candidateName);
                    $(".candidate-restored").show();
                    $(".candidate-restored").delay(15000).fadeOut(5000);
                    updateResults(false);
                }
            });
    }

    restorePermanentlyBlockedCandidate = function(candidateId) {
        employers.api.restorePermanentlyBlockedCandidates(
            new Array(candidateId),
            function(count) {
                $(".blocklists_ascx").find(".permanent-blocklist-action").parent().find(".count").text(count);
                updateResults(false);
            });
    }

    $.fn.initializeBlockListResultsCount = function(candidateContext) {
        $(this).find(".restore-blocklist").find("a").click(function() {
            var candidateIds = candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds : candidateContext.selectedCandidateIds;
            if ($(".blocklist_aspx").hasClass("blocklist-current_aspx")) {
                employers.api.restoreTemporarilyBlockedCandidates(
                    candidateIds,
                    function(count) {
                        $(".blocklists_ascx").find(".temporary-blocklist-action").parent().find(".count").text(count);
                        updateResults(false);
                    });
            }
            else {
                employers.api.restorePermanentlyBlockedCandidates(
                    candidateIds,
                    function(count) {
                        $(".blocklists_ascx").find(".permanent-blocklist-action").parent().find(".count").text(count);
                        updateResults(false);
                    });
            }
        });
    }

    $.fn.updateBlockListResultsCount = function(candidateContext) {
        var count = candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds.length : candidateContext.selectedCandidateIds.length;
        if (count == 0) {
            $(this).hide();
        }
        else {
            $(this).show();
            $(this).find(".results-count_save-search_header").find(".results-count").html($(".pagination-results-holder").html());
            $(this).find(".results-count_save-search_header").find(".restore-blocklist").find(".total-count").text(count);
        }
        return true;
    }
	
	moveTemporaryBlockListItemDown = function() {
		$(".temporary-blocklist-action").parent().after($(".temporary-blocklist-action").parent());
	}

})(jQuery);