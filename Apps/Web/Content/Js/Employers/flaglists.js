(function($) {

    updateCandidatesFlag = function(flag, candidateIds, candidateContext) {
        if (flag) {
            employers.api.addCandidatesToFlagList(
                candidateIds,
                function(count) {
                    $("#personal-folders-flagged").find(".count").text(count);
                    if (candidateContext.flagListId != null) {
                        updateResults(false);
                    }
                    else {
                        for (var i = 0; i < candidateIds.length; i++) {
                            $(".flag" + candidateIds[i]).parent().addClass("flagged");
                        }
                    }
                });
        }
        else {
            employers.api.removeCandidatesFromFlagList(
                candidateIds,
                function(count) {
                    $("#personal-folders-flagged").find(".count").text(count);
                    if (candidateContext.flagListId != null) {
                        updateResults(false);
                    }
                    else {
                        for (var i = 0; i < candidateIds.length; i++) {
                            $(".flag" + candidateIds[i]).parent().removeClass("flagged");
                        }
                    }
                });
        }
    }

    $.fn.updateCandidateFlag = function(candidateContext) {
        var flag = !$(this).parent().hasClass("flagged");
        updateCandidatesFlag(flag, new Array($(this).attr("id").slice(4)), candidateContext);
    }

    removeAllCandidatesFromFlagList = function() {
        $("#personal-folders-flagged").find(".empty-link").append("<span class='ajax-loader'></span>");
        employers.api.removeAllCandidatesFromFlagList(
            function(count) {
                $("#personal-folders-flagged").find(".count").text(count);
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            },
            null,
            function() {
                $("#personal-folders-flagged").find(".empty-link").find(".ajax-loader").remove();
            });
    }

    removeCandidatesFromFlagList = function(candidateIds) {
        employers.api.removeCandidatesFromFlagList(
            candidateIds,
            function(count) {
                $("#personal-folders-flagged").find(".count").text(count);
                updateResults(false);
            });
    }

    removeCurrentCandidatesFromFlagList = function() {
        employers.api.removeCurrentCandidatesFromFlagList(
            function(count) {
                $("#personal-folders-flagged").find(".count").text(count);
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            });
    }

})(jQuery);