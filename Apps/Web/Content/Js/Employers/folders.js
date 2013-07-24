(function($) {

    getFolders = function(onSuccess) {
        employers.api.getFolders(false, onSuccess);
    }

    updateFolders = function(candidateContext) {

        // Clear all that is there.

        $("#personal-folders-flagged").find("ul.flagged-folder-list").empty();
        $("#personal-folders").find("ul.personal-folders-list").empty();
        $("#personal-folders").find("div.large-folders").remove();
        $("#org-wide-folders").find("ul.org-wide-folders-list").empty();
        $("#org-wide-folders").find("div.large-folders").remove();

        // Reset the cache and display again.

        employers.api.getFolders(true, function(folders) { displayFolders(folders, candidateContext); });
        if (typeof updateResults == 'function')
            updateResults(false);
    }

    initializeFolders = function(candidateContext) {
        getFolders(function(folders) {
            displayFolders(folders, candidateContext);
        });
    }

    collapseIfLarge = function(listName) {
        $("." + listName).find("li").slice(5).hide();
        var showAllSection = "<div class='large-folders'><a href='javascript:void(0);' class='show-folders'>Show all folders</a></div>";
        $("." + listName).parent().append(showAllSection);
    }

	function calcFolderWidth(sectionTitle) {
		sectionTitle.parent().find(".js_ellipsis").each(function() {
			if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
				var width = $(this).parent().width() - $(this).next().width() - 5;
				if ($(this).width() > width)
					$(this).width(width + "px").css("text-overflow", "ellipsis").attr("title", $(this).html());
			} else {
				$(this).width("500px");
				var width = $(this).parent().width() - $(this).next().width() - 5;
				$(this).width("");
				if ($(this).width() > width)
					$(this).width(width + "px").css("text-overflow", "ellipsis").attr("title", $(this).html());
			}
		});
	}

    $.fn.openFolderSubSections = function() {
		//exclude any section-collapsible-title under jobads_ascx
        $(this).find(".section-collapsible-title:not(.jobads_ascx .section-collapsible-title)").each(function() {
            if ($(this).hasClass("main-title")) {
                if ($(this).hasClass("section-collapsible-title-default")) {
                    $(this).addClass("section-collapsible-title-active").removeClass("section-collapsible-title-default")
                            .find("> .section-icon").addClass("section-icon-up").removeClass("section-icon-down")
                            .end().next().show().next().next().hide();
                }
            } else {
                if ($(this).hasClass("section-collapsible-title-default")) {
                    $(this).addClass("section-collapsible-title-active").removeClass("section-collapsible-title-default")
                            .find("> .section-icon").addClass("section-icon-up").removeClass("section-icon-down")
                            .end().next().show();
                }
                $(this).parent().find(".large-folders").find("a").each(function() {
                    if ($(this).hasClass("show-folders")) {
                        $(this).parent().prev("ul").children("li").show();
                        $(this).text("Hide folders");
                        $(this).removeClass("show-folders").addClass("hide-folders");
                    }
                });
            }
        });
    }

    displayFolders = function(folders, candidateContext) {
        if (!(folders == null)) {

            var privateFolders = $("#personal-folders").find("ul");
            var sharedFolders = $("#org-wide-folders").find("ul");

            for (var i = 0; i < folders.length; i++) {

                if (folders[i].Type == "Flagged") {
                    $("#personal-folders-flagged").find("ul").append("<li class='droppable'><div></div><a href='" + flagListUrl + "' class='nav-icon js_ellipsis' id=" + folders[i].Id + ">Flagged candidates</a> <span class='count-holder'>(<span class='count'>" + folders[i].Count + "</span>)</span> <a class='empty-link'>Empty</a></li>");
                } else if ((folders[i].Type == "Shortlist") || (folders[i].Type == "Private") || (folders[i].Type == "Mobile")) {
                    var name = folders[i].Name;
                    if ((folders[i].Type == "Shortlist") && (name == null)) {
                        name = "My shortlist";
                    }
                    if ((folders[i].Type == "Mobile") && (name == null)) {
                        name = "My mobile favourites";
                    }
                    privateFolders.append("<li class='droppable'><div></div><a href='" + foldersUrl + '/' + folders[i].Id + "' class='nav-icon js_ellipsis' id=" + folders[i].Id + ">" + name + "</a> <span class='count-holder'>(<span class='count'>" + folders[i].Count + "</span>)</span></li>");
                }
                else {
                    sharedFolders.append("<li class='droppable'><div></div><a href='" + foldersUrl + '/' + folders[i].Id + "' class='nav-icon js_ellipsis' id=" + folders[i].Id + ">" + folders[i].Name + "</a> <span class='count-holder'>(<span class='count'>" + folders[i].Count + "</span>)</span></li>");
                }
            }

            $(".folders_ascx .js_ellipsis").customEllipsis();
            $(".folder-float_container").find(".js_ellipsis").customEllipsis();

            if ($("ul.personal-folders-list > li").length > 5) {
                collapseIfLarge("personal-folders-list");
            }
            if ($("ul.org-wide-folders-list > li").length > 5) {
                collapseIfLarge("org-wide-folders-list");
            }
			calcFolderWidth($(".personal-folders_section"));
			calcFolderWidth($(".organisation-wide-folders_section"));
            if (($("ul.personal-folders-list > li").length > 5) || ($("ul.org-wide-folders-list > li").length > 5)) {
                $(".large-folders").find("a").click(function() {
                    if ($(this).hasClass("show-folders")) {
                        $(this).parent().prev("ul").children("li").show();
						calcFolderWidth($(this).parent().parent());
                        $(this).text("Hide folders");
                        $(this).removeClass("show-folders").addClass("hide-folders");
                    } else {
                        $(this).parent().prev("ul").children("li").slice(5).hide();
                        $(this).text("Show all folders");
                        $(this).removeClass("hide-folders").addClass("show-folders");
                    }
                });
            }

            if (!candidateContext.isAnonymous) {
                /* Empty link for Flagged Folder */

                $("#personal-folders-flagged").find(".empty-link").click(function() {
                    removeAllCandidatesFromFlagList();
                });

                $(".droppable").droppable({
                    hoverClass: 'active',
                    tolerance: 'pointer',
                    activate: function(event, ui) {
                        if ($(".folders_ascx").length > 0) {
                            $(".folders_ascx").openFolderSubSections();
                        } else if ($(".folders-resume_ascx").length > 0) {
                            $(".folders-resume_ascx").find(".section-icon").addClass("section-icon-left").removeClass("section-icon-right");
                            $(".folder-float_holder").show();
                            $(".folders-resume_ascx").find(".main-title").addClass("section-collapsible-title-active").removeClass("section-collapsible-title-default");
                            $(".folder-float_container").openFolderSubSections();
                        }
                    },
                    drop: function(event, ui) {
                        var candidateId = "";
                        var folderId = "";
                        if (!($(this).hasClass("section-collapsible-title"))) {
                            folderId = $(this).find("a").attr("id");
                            //alert("dropped");
                            if ($("#" + ($(ui.draggable).attr("data-memberid"))).closest(".search-result").hasClass("selected_candidate_search-result")) {
                                if ($(this).parent().hasClass("flagged-folder-list")) {
                                    updateCandidatesFlag(true, candidateContext.selectedCandidateIds, candidateContext);
                                } else {
                                    addCandidatesToFolder(folderId, candidateContext.selectedCandidateIds);
                                }
                            } else {
                                candidateId = $(ui.draggable).attr("data-memberid");
                                if ($(this).parent().hasClass("flagged-folder-list")) {
                                    updateCandidatesFlag(true, new Array(candidateId), candidateContext);
                                } else {
                                    addCandidatesToFolder(folderId, new Array(candidateId));
                                }
                            }
                        }
                    }
                });

                $(".js_add-private-folder").click(function() {
                    return showAddFolderOverlay(true, null, function() { updateFolders(candidateContext); });
                });
                $(".js_add-shared-folder").click(function() {
                    return showAddFolderOverlay(false, null, function() { updateFolders(candidateContext); });
                });
            }
            else {
                $(".js_add-private-folder").click(function() {
                    return showLoginOverlay("addfolder");
                });
                $(".js_add-shared-folder").click(function() {
                    return showLoginOverlay("addfolder");
                });
            }
        }
    }

    renameFolder = function(folderId, currentName, newName, onSuccess, onComplete) {
        if (currentName == newName) {
            onSuccess(folderId, newName);
            onComplete(folderId);
        }
        else {
            employers.api.renameFolder(
                folderId,
                newName,
                function() { onSuccess(folderId, newName); },
                null,
                function() { onComplete(folderId); });
        }
    }

    addCandidatesToFolder = function(folderId, candidateIds) {
        employers.api.addCandidatesToFolder(folderId, candidateIds, function(count) {
            $("#" + folderId).parent().find(".count").text(count);
            updateResults(false);
        });
    }

    removeCandidatesFromFolder = function(folderId, candidateIds) {
        showResultsOverlay();
        employers.api.removeCandidatesFromFolder(
            folderId,
            candidateIds,
            function(count) {
                $("#action-overlay").hide();
                $("#" + folderId).parent().find(".count").text(count);
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            },
            null,
            function() {
                $("#action-overlay").hide();
            });
    }

})(jQuery);