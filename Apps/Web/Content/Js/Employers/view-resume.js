(function($) {

    initializeResumeFolders = function(candidateContext) {
        getFolders(function(folders) {
            displayFolders(folders, candidateContext);
        });
    }

    $.fn.candidateNavigation = function(totalCandidateCount) {
        var selectedCandidateIndex = parseInt($(this).find(".resumes-nav").find(".nav-candidate-selected").attr("index-id"));
        var selectedCandidateIndexText = selectedCandidateIndex + 1;
        $(this).find(".nav-results-count_holder").find(".selected-candidate-index").text(selectedCandidateIndexText);
        if (totalCandidateCount > 1) {
            $(this).find(".nav-arrow_holder").find(".arrow").click(function() {
                selectedCandidateIndex = parseInt($(".resumes-nav").find(".nav-candidate-selected").attr("index-id"));
                if ($(this).hasClass("left-arrow")) {
                    if (selectedCandidateIndex == 0) {
                        return false;
                    } else {
                        var newSelectedCandidateIndex = selectedCandidateIndex - 1;
                    }
                } else {
                    if (selectedCandidateIndex == (totalCandidateCount - 1)) {
                        return false;
                    } else {
                        var newSelectedCandidateIndex = selectedCandidateIndex + 1;
                    }
                }
                var newSelectedCandidateIndexText = newSelectedCandidateIndex + 1;
                $(".resumes-nav").find(".nav-candidate-selected").removeClass("nav-candidate-selected");
                $(".resumes-nav").find(".nav-candidate:eq(" + newSelectedCandidateIndex + ")").addClass("nav-candidate-selected");
                $(".nav-results-count_holder").find(".selected-candidate-index").text(newSelectedCandidateIndexText);
                var candidateId = $(".resumes-nav").find(".nav-candidate-selected").attr("id");
                $("#candidateId").val(candidateId);
                updateResults(false);
            });

            $(this).find(".resumes-nav").find(".nav-candidate").click(function() {
                $(".resumes-nav").find(".nav-candidate-selected").removeClass("nav-candidate-selected");
                $(this).addClass("nav-candidate-selected");
                var selectedCandidateIndexText = parseInt($(this).attr("index-id")) + 1;
                $(".nav-results-count_holder").find(".selected-candidate-index").text(selectedCandidateIndexText);
                var candidateId = $(".resumes-nav").find(".nav-candidate-selected").attr("id");
                $("#candidateId").val(candidateId);
                updateResults(false);
            });

        }
    }

    $.fn.activateResumeHeader = function() {
        $(this).mouseover(function() {
            $(this).addClass("basic-details-over");
        });
        $(this).mousedown(function() {
            $(this).addClass("basic-details-down");
        });
        $(this).mouseup(function() {
            $(this).removeClass("basic-details-down");
        });
        $(this).mouseout(function() {
            $(this).removeClass("basic-details-over");
            $(this).removeClass("basic-details-down");
        });
    }

    $.fn.toggleActionMenu = function() {
        /* Collapsing the menu section */
        $(".resume-menu-header").hide();

        /* Creating collapsible sections */
        $(this).click(function() {
            $(this).find("> .menu-icon").toggleClass("menu-icon-down").toggleClass("menu-icon-up");
            $(".resume-menu-header").toggle();
            $(this).toggleClass("basic-details-expanded");
            return false;
        });
    }

    $.fn.toggleDropdown = function(candidateContext) {
        $(this).click(function() {
            /*$(this).closest(".has-dropdown").find(".dropdown").toggle();*/
            if ($(this).closest(".has-dropdown").find(".dropdown").css("display") == "block") {
                $(this).closest(".has-dropdown").find(".dropdown").css("display", "none");
            } else {
                $(this).closest(".has-dropdown").find(".dropdown").css("display", "block");
            }
            $(this).find(".action-icon").toggleClass("action-icon-down").toggleClass("action-icon-up");
            return false;
        });
    }

    $.fn.displayResumeContents = function() {
        $(this).click(function() {
            var selectedTabContentId = $(this).attr("id") + "-details";
            // var currentTabContentId = "";
            // $(this).closest(".resume-details").find(".tabs-container").each(function() {
                // if (!($(this).hasClass("tabs-hide"))) {
                    // currentTabContentId = $(this).attr("id");
                // }
            // });
            if (selectedTabContentId == "FullResume-details") {
                $("#" + selectedTabContentId).find(".tabs-inner-container").html($("#resume-photo").clone());
                $("#" + selectedTabContentId).find(".tabs-inner-container").append($("#self-summary").clone());
                $("#" + selectedTabContentId).find(".tabs-inner-container").append($("#Employment-details").find(".tabs-inner-container").html());
                $("#" + selectedTabContentId).find(".tabs-inner-container").append($("#Education-details").find(".tabs-inner-container").html());
                $("#" + selectedTabContentId).find(".tabs-inner-container").append($("#Skills-details").find(".tabs-inner-container").html());
                $("#" + selectedTabContentId).find(".tabs-inner-container").append($("#Professional-details").find(".tabs-inner-container").html());
                $("#" + selectedTabContentId).find(".tabs-inner-container").append($("#Personal-details").find(".tabs-inner-container").html());
				$("#Professional-details").find(".tabs-inner-container .professional_section .professional tr:eq(0)").before($("#Personal-details").find(".tabs-inner-container .personal_section .personal tr:lt(3)").clone());
            }
            $(".resume-details .tabs-container").addClass("tabs-hide");
            $("#" + selectedTabContentId).removeClass("tabs-hide");
        });
    }

    var isFullScreen = false;
    
    $.fn.toggleFullscreenView = function() {
        if (isFullScreen) {
            $(this).addClass("fullscreen");
            $(this).find(".fullscreen_link").text("Show menu");
            $("#body-container").css("margin-top", "0");
            $("#header").hide();
            $("#subheader").hide();
        }
        $(this).click(function() {
            $(this).toggleClass("fullscreen");
            if ($(this).hasClass("fullscreen")) {
                $(this).find(".fullscreen_link").text("Show menu");
                $("#body-container").css("margin-top", "0");
                isFullScreen = true;
            } else {
                $(this).find(".fullscreen_link").text("Hide menu");
                $("#body-container").css("margin-top", "25px");
                isFullScreen = false;
            }
            $("#header").toggle();
            $("#subheader").toggle();
            return false;
        });
    }

    shortenLongFolderLists = function(folderListType) {
        if (folderListType == "Private") {
            $(".folders-dropdown-list").prepend("<div class='large-private-folder-list'></div>");
            ($(".folders-dropdown-list").find("div.Shortlist")).each(function() {
                $(this).appendTo($(this).closest(".folders-dropdown-list").find("div.large-private-folder-list"));
            });
            ($(".folders-dropdown-list").find("div.Private")).each(function() {
                $(this).appendTo($(this).closest(".folders-dropdown-list").find("div.large-private-folder-list"));
            });
        } else {
            $(".folders-dropdown-list").find(".divider:eq(0)").after("<div class='large-shared-folder-list'></div>");
            ($(".folders-dropdown-list").find("div.Shared")).each(function() {
                $(this).appendTo($(this).closest(".folders-dropdown-list").find("div.large-shared-folder-list"));
            });
        }
    }

})(jQuery);