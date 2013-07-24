(function($) {

    $.fn.makeSectionCollapsible = function() {
        /* Expanding the parent section */
        $(this).find(".section-collapsible-title").addClass("section-collapsible-title-active").append('<span class="section-icon section-icon-up"/>').next().addClass("section-collapsible-content");

        /* Creating collapsible sections */
        $(this).find(".section-collapsible-title").click(function() {
            $(this).find("> .section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down").end().next().toggle().toggleClass("section-collapsible-content");

            if (($(this).hasClass("main-title"))) {
                $(this).toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default");
            }

            return false;
        });

        /* Collapsing all sub sections (except the first sub section -- commented out to hide it for bookmarked urls) */
        $(this).find(".section-collapsible-content").find("div.section-collapsible-title")  //.slice(1)
            .toggleClass("section-collapsible-title-default").toggleClass("section-collapsible-title-active")
            .find("> .section-icon").toggleClass("section-icon-down").toggleClass("section-icon-up")
            .end().next().toggle();
    }

    function toggleFoldersSection(sectionTitle, showDragArea) {
        sectionTitle.toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default")
            .find("> .section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down")
            .end().next().toggle();

        if (showDragArea)
            sectionTitle.next().next().next().toggle();
		
		calcFolderWidth(sectionTitle);
		if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
			//only for css rerendenering in IE7
			$(".folders_ascx .section-icon").toggle().toggle();
			$(".add-folder-list").toggle().toggle();
			$(".jobads_ascx ul li .count-holder").toggle().toggle();
		}
    }

    function toggleFoldersSubSection(sectionTitle) {
        sectionTitle.toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default")
            .find("> .section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down")
            .end().next().toggle().toggleClass("section-collapsible-content");

			calcFolderWidth(sectionTitle);
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

    $.fn.makeFoldersSectionCollapsible = function(showDragArea) {

        /* Set up all everything as expanded */
		//exclude any section-collapsible-title under jobads_ascx
        $(this).find(".section-collapsible-title:not(.jobads_ascx .section-collapsible-title)")
            .addClass("section-collapsible-title-active")
            .append('<span class="section-icon section-icon-up"/>')
            .next().addClass("section-collapsible-content")
            .next().next().toggle();

        /* Collapse the section itself though */

        toggleFoldersSection($(this).find(".section-collapsible-title:eq(0)"), showDragArea);

        /* Expand and collapse */

        $(this).find(".personal-folders_section").find(".section-collapsible-title").click(function() {
            toggleFoldersSubSection($(this));
            return false;
        });

        $(this).find(".organisation-wide-folders_section").find(".section-collapsible-title").click(function() {
            toggleFoldersSubSection($(this));
            return false;
        });

        $(this).find(".section-collapsible-title:eq(0)").click(function() {
            toggleFoldersSection($(this), showDragArea);
            return false;
        });
    }

    function toggleJobAdsSection(sectionTitle, showDragArea) {
        sectionTitle.toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default")
            .find("> .section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down")
            .end().next().toggle();

        if (showDragArea)
            sectionTitle.next().next().next().toggle();

		calcJobAdsWidth(sectionTitle);
	}

    function toggleJobAdsSubSection(sectionTitle) {
        sectionTitle.toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default")
            .find("> .section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down")
            .end().next().toggle().toggleClass("section-collapsible-content");

		calcJobAdsWidth(sectionTitle);
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

    $.fn.makeJobAdsSectionCollapsible = function(showDragArea) {
        /* Set up all everything as expanded */
        $(this).find(".section-collapsible-title")
            .addClass("section-collapsible-title-active")
            .append('<span class="section-icon section-icon-up"/>')
            .next().addClass("section-collapsible-content")
            .next().next().toggle();

		//different main title for jobads
		$(this).find(".main-title").find("span").addClass("jobads-main-title-icon");
		
        /* Collapse the section itself though */

        toggleJobAdsSection($(this).find(".section-collapsible-title:eq(0)"), showDragArea);

        /* Expand and collapse */

        $(this).find(".open-jobads_section").find(".section-collapsible-title").click(function() {
            toggleJobAdsSubSection($(this));
            return false;
        });

        $(this).find(".closed-jobads_section").find(".section-collapsible-title").click(function() {
            toggleJobAdsSubSection($(this));
            return false;
        });

        $(this).find(".section-collapsible-title:eq(0)").click(function() {
            toggleJobAdsSection($(this), showDragArea);
            return false;
        });
    }

    function toggleBlockListsSection(sectionTitle, showDragArea) {
        sectionTitle.toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default")
            .find("> .section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down")
            .end().next().toggle();

        if (showDragArea)
            sectionTitle.next().next().next().toggle();
    }

    $.fn.makeBlockListsSectionCollapsible = function(showDragArea) {

        /* Set up all everything as expanded */

        $(this).find(".section-collapsible-title")
            .addClass("section-collapsible-title-active")
            .append('<span class="section-icon section-icon-up"/>')
            .next().addClass("section-collapsible-content")
            .next().next().toggle();

        /* Collapse the section itself though */

        toggleBlockListsSection($(this).find(".section-collapsible-title:eq(0)"), showDragArea);

        /* Expand and collapse */

        $(this).find(".section-collapsible-title:eq(0)").click(function() {
            toggleBlockListsSection($(this), showDragArea);
            return false;
        });
    }

    $.fn.makeMainSectionCollapsible = function(showSection) {

        // Collapsing the parent sections by default.

        $(this).find(".m-section-title").append('<span class="m-section-icon section-icon-down"/>').next().toggle();

        $(this).find(".m-section-title").click(function() {
            $(this).find("> .m-section-icon").toggleClass("section-icon-up").toggleClass("section-icon-down").end().next().toggle().toggleClass("section-collapsible-content");
            if ($(this).find("> .m-section-icon").hasClass("section-icon-up")) {
                if (showSection != null)
                    showSection();
            }
            return false;
        });
    }

    $.fn.makeGetMostSubSectionsCollapsible = function() {
        /* Collapsing the sections by default */
        $(this).find("li.section-title").find("div.section-content").toggle();
        /* Creating collapsible sections */
        $(this).find("li.section-title").find("span.title")
			.click(function() {
                $(this).toggleClass("down-icon").toggleClass("up-icon");
			    $(this).parent().find("div.section-content").toggle();
			    return false;
			});
    }

    $.fn.makeFolderResumeCollapsible = function() {
        /* Collapsing the parent section */
        $(this).find(".section-collapsible-title").addClass("section-collapsible-title-default").append('<span class="section-icon section-icon-right"/>');
        $(".folder-float_holder").makeSectionCollapsible();
        $(".folder-float_holder").hide();

        /* Creating collapsible sections */
        $(this).find(".section-collapsible-title").click(function() {
            $(this).find("> .section-icon").toggleClass("section-icon-left").toggleClass("section-icon-right");
            $(".folder-float_holder").toggle();
            if (($(this).hasClass("main-title"))) {
                $(this).toggleClass("section-collapsible-title-active").toggleClass("section-collapsible-title-default");
            }
            return false;
        });
    }

})(jQuery);