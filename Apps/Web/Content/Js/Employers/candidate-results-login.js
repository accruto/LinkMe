(function($) {
    onClickLocked = function () {
        (function($) {
            showLoginOverlay("unlock");
        })(jQuery);
    }

    onClickFlag = function (element) {
        (function($) {
            showLoginOverlay("flag");
        })(jQuery);
    }

    onClickBlockCandidate = function (candidateId) {
        (function($) {
            showLoginOverlay("block");
        })(jQuery);
    }

    onClickPermanentlyBlockCandidate = function (candidateId) {
        (function($) {
            showLoginOverlay("block");
        })(jQuery);
    }
    
    onClickDisplayNotes = function (element, candidateId) {
        (function($) {
            showLoginOverlay("notes");
        })(jQuery);
    }

    onClickShortlistCandidate = function (jobAdId, candidateId) {
        (function($) {
            showLoginOverlay("shortlist");
        })(jQuery);
    }

    onClickRejectCandidate = function (jobAdId, candidateId) {
        (function($) {
            showLoginOverlay("reject");
        })(jQuery);
    }

})(jQuery);