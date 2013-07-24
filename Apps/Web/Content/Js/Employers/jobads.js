(function($) {

    getJobAds = function(onSuccess) {
        employers.api.getJobAds(onSuccess);
    }

    updateJobAds = function(candidateContext) {

        // Clear all that is there.

        $("#open-jobads").find("ul.open-jobads-list").empty();
        $("#open-jobads").find("div.large-jobads").remove();
        $("#closed-jobads").find("ul.closed-jobads-list").empty();
        $("#closed-jobads").find("div.large-jobads").remove();

        // Reset the cache and display again.

        employers.api.getJobAds(true, function(jobads) { displayJobads(jobads, candidateContext); });
        if (typeof updateResults == 'function')
            updateResults(false);
    }

    shortlistCandidatesForJobAd = function(jobAdId, candidateIds) {
        employers.api.shortlistCandidatesForJobAd(
            jobAdId,
            candidateIds,
            function(count) {
				updateStatusCount(jobAdId, count.JobAd.ApplicantCounts);
                updateResults(false);
            });
    }

	function updateStatusCount(jobAdId, statusCounts) {
		(function($) {
			$("#" + jobAdId).parent().find("span.new-candidates-count").text(statusCounts.New);
			$("#" + jobAdId).parent().find("span.short-listed-candidates-count").text(statusCounts.ShortListed);
			$("#" + jobAdId).parent().find("span.rejected-candidates-count").text(statusCounts.Rejected);
			//update candidate tab count only if drop point is current job ads
			if (jobAdId == candidateContext.jobAdId) {
				$(".new-category-tab .new-candidates-count").text(statusCounts.New);
				$(".shortlisted-category-tab .shortlisted-candidates-count").text(statusCounts.ShortListed);
				$(".rejected-category-tab .rejected-candidates-count").text(statusCounts.Rejected);
			}
		})(jQuery);
	}

    initializeJobAds = function(candidateContext) {
        getJobAds(function(jobads) {
            displayJobAds(jobads, candidateContext);
        });
    }

    collapseIfJobAdsLarge = function(listName) {
        $("." + listName).find("li").slice(5).hide();
        var showAllSection = "<div class='large-jobads'><a href='javascript:void(0);' class='show-jobads'>Show all " + listName.substring(0, listName.indexOf("-")) + " LinkMe job ads</a></div>";
        $("." + listName).parent().append(showAllSection);
    }

	function calcJobAdsWidth(sectionTitle) {
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

    $.fn.openJobAdSubSections = function() {
        $(this).find(".section-collapsible-title").each(function() {
            if ($(this).hasClass("main-title")) {
                if ($(this).hasClass("section-collapsible-title-active")) {
                    $(this).addClass("section-collapsible-title-default").removeClass("section-collapsible-title-active")
                            .find("> .section-icon").addClass("section-icon-down").removeClass("section-icon-up")
                            .end().next().show().next().next().hide();
                }
            } else {
                if ($(this).hasClass("section-collapsible-title-default")) {
                    $(this).addClass("section-collapsible-title-active").removeClass("section-collapsible-title-default")
                            .find("> .section-icon").addClass("section-icon-up").removeClass("section-icon-down")
                            .end().next().show();
                }
                $(this).parent().find(".large-jobads").find("a").each(function() {
                    if ($(this).hasClass("show-jobads")) {
                        $(this).parent().prev("ul").children("li").show();
                        $(this).text("Hide jobads");
                        $(this).removeClass("show-jobads").addClass("hide-jobads");
                    }
                });
            }
        });
    }

    displayJobAds = function(jobads, candidateContext) {
        if (!(jobads == null)) {

            var openJobAds = $("#open-jobads").find("ul");
            var closedJobAds= $("#closed-jobads").find("ul");
			var openJobAdsCount = 0;
			var closedJobAdsCount = 0;

            for (var i = 0; i < jobads.length; i++) {

                if (jobads[i].Status == "Open") {
					if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
						var jobAdsItem = $("<li class='jobAdDroppable IE7'><div class='jobads-icon'></div><a href='" + manageJobAdsUrl + '/' + jobads[i].Id + "' class='nav-icon js_ellipsis' id=" + jobads[i].Id + ">" + jobads[i].Title + "</a><span class='count-holder'><div> (</div><div class='new-count-icon'></div><span class='count new-candidates-count'>" + jobads[i].ApplicantCounts.New + "</span><div class='shortlisted-count-icon'></div><span class='count short-listed-candidates-count'>" + jobads[i].ApplicantCounts.ShortListed + "</span><div class='rejected-count-icon'></div><span class='count rejected-candidates-count'>" + jobads[i].ApplicantCounts.Rejected + "</span><div>)</div></span></li>");
						jobAdsItem.find("div").addClass("IE7");
						jobAdsItem.find("span").addClass("IE7");
						openJobAds.append(jobAdsItem);
					}
					else
						openJobAds.append("<li class='jobAdDroppable'><div class='jobads-icon'></div><a href='" + manageJobAdsUrl + '/' + jobads[i].Id + "' class='nav-icon js_ellipsis' id=" + jobads[i].Id + ">" + jobads[i].Title + "</a><span class='count-holder'> (<div class='new-count-icon'></div><span class='count new-candidates-count'>" + jobads[i].ApplicantCounts.New + "</span><div class='shortlisted-count-icon'></div><span class='count short-listed-candidates-count'>" + jobads[i].ApplicantCounts.ShortListed + "</span><div class='rejected-count-icon'></div><span class='count rejected-candidates-count'>" + jobads[i].ApplicantCounts.Rejected + "</span>)</span></li>");
					openJobAdsCount++;
                }
                else {
					if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
						var jobAdsItem = $("<li class='jobAdDroppable IE7'><div class='jobads-icon'></div><a href='" + manageJobAdsUrl + '/' + jobads[i].Id + "' class='nav-icon js_ellipsis' id=" + jobads[i].Id + ">" + jobads[i].Title + "</a><span class='count-holder'><div> (</div><div class='new-count-icon'></div><span class='count new-candidates-count'>" + jobads[i].ApplicantCounts.New + "</span><div class='shortlisted-count-icon'></div><span class='count short-listed-candidates-count'>" + jobads[i].ApplicantCounts.ShortListed + "</span><div class='rejected-count-icon'></div><span class='count rejected-candidates-count'>" + jobads[i].ApplicantCounts.Rejected + "</span><div>)</div></span></li>");
						jobAdsItem.find("div").addClass("IE7");
						jobAdsItem.find("span").addClass("IE7");
						closedJobAds.append(jobAdsItem);
					}
					else
						closedJobAds.append("<li class='jobAdDroppable'><div class='jobads-icon'></div><a href='" + manageJobAdsUrl + '/' + jobads[i].Id + "' class='nav-icon js_ellipsis' id=" + jobads[i].Id + ">" + jobads[i].Title + "</a> <span class='count-holder'> (<div class='new-count-icon'></div><span class='count new-candidates-count'>" + jobads[i].ApplicantCounts.New + "</span><div class='shortlisted-count-icon'></div><span class='count short-listed-candidates-count'>" + jobads[i].ApplicantCounts.ShortListed + "</span><div class='rejected-count-icon'></div><span class='count rejected-candidates-count'>" + jobads[i].ApplicantCounts.Rejected + "</span>)</span></li>");
					closedJobAdsCount++;
                }
            }
			if (openJobAdsCount == 0 && closedJobAdsCount == 0) {
				$(".jobads_ascx").hide();
			} else if (openJobAdsCount == 0) {
				$(".open-jobads_section").hide().insertAfter(".closed-jobads_section");
			} else if (closedJobAdsCount == 0) {
				$(".closed-jobads_section").hide().insertAfter(".open-jobads_section");
			}

            $(".jobads_ascx .js_ellipsis").customEllipsis();
            $(".jobad-float_container").find(".js_ellipsis").customEllipsis();
			
            if (openJobAdsCount > 5) {
                collapseIfJobAdsLarge("open-jobads-list");
            }
            if (closedJobAdsCount > 5) {
                collapseIfJobAdsLarge("closed-jobads-list");
            }
			calcJobAdsWidth($(".open-jobads_section"));
			calcJobAdsWidth($(".closed-jobads_section"));
            if ((openJobAdsCount > 5) || (closedJobAdsCount > 5)) {
                $(".large-jobads").find("a").click(function() {
                    if ($(this).hasClass("show-jobads")) {
                        $(this).parent().prev("ul").children("li").show();
						calcJobAdsWidth($(this).parent().parent());
                        $(this).text("Hide " + ($(this).parent().prev().hasClass("open-jobads-list") ? "open" : "closed") + " LinkMe job ads");
                        $(this).removeClass("show-jobads").addClass("hide-jobads");
                    } else {
                        $(this).parent().prev("ul").children("li").slice(5).hide();
                        $(this).text("Show all " + ($(this).parent().prev().hasClass("open-jobads-list") ? "open" : "closed") + " LinkMe job ads");
                        $(this).removeClass("hide-jobads").addClass("show-jobads");
                    }
                });
            }

            if (!candidateContext.isAnonymous) {

                $(".jobAdDroppable").droppable({
                    hoverClass: 'active',
                    tolerance: 'pointer',
                    activate: function(event, ui) {
                        if ($(".jobads_ascx").length > 0) {
                            $(".jobads_ascx").openJobAdSubSections();
                        } else if ($(".jobads-resume_ascx").length > 0) {
                            $(".jobads-resume_ascx").find(".section-icon").addClass("section-icon-left").removeClass("section-icon-right");
                            $(".jobad-float_holder").show();
                            $(".jobads-resume_ascx").find(".main-title").addClass("section-collapsible-title-active").removeClass("section-collapsible-title-default");
                            $(".jobad-float_container").openJobAdSubSections();
                        }
                    },
                    drop: function(event, ui) {
                        var candidateId = "";
                        var jobAdId = "";
                        if (!($(this).hasClass("section-collapsible-title"))) {
                            jobAdId = $(this).find("a").attr("id");
                            //alert("dropped");
                            if ($("#" + ($(ui.draggable).attr("data-memberid"))).closest(".search-result").hasClass("selected_candidate_search-result")) {
                                shortlistCandidatesForJobAd(jobAdId, candidateContext.selectedCandidateIds);
                            } else {
                            candidateId = $(ui.draggable).attr("data-memberid");
                                shortlistCandidatesForJobAd(jobAdId, new Array(candidateId));
                            }
                        }
                    }
                });

                $(".js_add-open-jobad").click(function() {
                    return showAddJobAdOverlay(true, null, function() { updateJobAds(candidateContext); });
                });
                $(".js_add-closed-jobad").click(function() {
                    return showAddJobAdOverlay(false, null, function() { updateJobAds(candidateContext); });
                });
            }
            else {
                $(".js_add-open-jobad").click(function() {
                    return showLoginOverlay("addjobad");
                });
                $(".js_add-closed-jobad").click(function() {
                    return showLoginOverlay("addjobad");
                });
            }
        }
		if ($.browser.msie) {
			if ($.browser.version.indexOf("9") == 0) {
				$(".folders_ascx .section-content ul li a").addClass("IE9");
				$(".open-jobads_section").addClass("IE9");
			}
			if ($.browser.version.indexOf("8") == 0) {
				$(".folders_ascx .section-content ul li a").addClass("IE8");
				$(".open-jobads_section").addClass("IE8");
			}
			if ($.browser.version.indexOf("7") == 0) {
				$("#hlAddToJobAd .menu-submenu-button").addClass("IE7");
				$("#hlBulkAddToJobAd .menu-submenu-button").addClass("IE7");
			}
		}
		if ($.browser.mozilla) {
			$(".open-jobads_section").addClass("FireFox");
		}
    }

})(jQuery);