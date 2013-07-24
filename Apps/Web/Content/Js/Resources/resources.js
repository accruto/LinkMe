(function($) {
    var currentRequest = null;
    var slideTimeout;

    $(document).ready(function() {
        //search
        $("#Keywords").focus(function() {
            if ($(this).val() == $(this).attr("data-watermark")) $(this).val("");
            $(this).removeClass("masked");
        }).blur(function() {
            if ($(this).val() == "") {
                $(this).val($(this).attr("data-watermark")).addClass("masked");
            } else $(this).removeClass("masked");
        }).keypress(function(event) {
			if (event.which == 13) $(".search .button.search").click();
		});
        if ($("#Keywords").val() == $("#Keywords").attr("data-watermark")) $("#Keywords").addClass("masked");
        $(".search .categorylist select option").each(function() {
            $(".search .categorylist .dropdown-items").append("<div class='dropdown-item'>" + $(this).text() + "</div>");
        });
        $(".search .categorylist").click(function() {
            $(".search .categorylist .dropdown-items").toggle();
        });
        $(".search .categorylist .dropdown-items .dropdown-item").click(function(event) {
            $(this).parent().hide();
            var index = $(".dropdown-item", $(this).parent()).index(this);
            $("#SearchCategory option:selected").removeAttr("selected");
            $("#SearchCategory option:nth-child(" + (index + 1) + ")").attr("selected", "selected");
            $(".search .categorylist .selected").text($(this).text());
            event.stopPropagation();
        });
        $(".search .button.search").click(function() {
            if ($("#top-section").length > 0) {
                var keywords = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                window.location = $(this).attr("url").unMungeUrl() + "?keywords=" + keywords + "&categoryId=" + categoryId;
            } else partialCall("Search", "");
        });
        //ask experts
        $("#QuestionText").focus(function() {
            if ($(this).val() == $(this).attr("data-watermark")) $(this).val("");
            $(this).removeClass("masked");
        }).blur(function() {
            if ($(this).val() == "") {
                $(this).val($(this).attr("data-watermark")).addClass("masked");
            } else $(this).removeClass("masked");
        }).keyup(function() {
			var limitLabel = $(".charsleft");
			var maxlength = 500;
			var enforceLength = true;
			
			//Replace \r\n with \n then replace \n with \r\n
			//Can't replace \n with \r\n directly because \r\n will be come \r\r\n

			//We do this because different browsers and servers handle new lines differently.
			//Internet Explorer and Opera say a new line is \r\n
			//Firefox and Safari say a new line is just a \n
			//ASP.NET seems to convert any plain \n characters to \r\n, which leads to counting issues
			var value = $(this).val().replace(/\u000d\u000a/g,'\u000a').replace(/\u000a/g,'\u000d\u000a');
			var currentLength = value.length;
			var remaining = 0;
			
			if(maxlength == null || limitLabel == null) return false;
			remaining = maxlength - currentLength;
			
			if(remaining >= 0)
			{
				limitLabel.text("Characters left: " + remaining);
			}
			else
			{
				value = value.substring(0, maxlength);
				$(this).val(value);
				limitLabel.text("Characters left: 0");
			}
        });
        if ($("#QuestionText").val() == $("#QuestionText").attr("data-watermark")) $("#QuestionText").addClass("masked");
        $(".askexperts .categorylist select option").each(function() {
            $(".askexperts .categorylist .dropdown-items").append("<div class='dropdown-item'>" + $(this).text() + "</div>");
        });
        $(".askexperts .categorylist").click(function() {
            $(".askexperts .categorylist .dropdown-items").toggle();
        });
        $(".askexperts .categorylist .dropdown-items .dropdown-item").click(function(event) {
            $(this).parent().hide();
            var index = $(".dropdown-item", $(this).parent()).index(this);
            $("#AskCategory option:selected").removeAttr("selected");
            $("#AskCategory option:nth-child(" + (index + 1) + ")").attr("selected", "selected");
            $(".askexperts .categorylist .selected").text($(this).text());
            event.stopPropagation();
        });
        $(".askexperts .checkbox").click(function() {
            $(this).toggleClass("checked");
            $(".askexperts .button.ask").toggleClass("disabled");
        });
        $(".askexperts .ask").click(function() {
            if ($(this).hasClass("disabled")) return;
            if ($("#AskCategory").val() == "-1") showErrorMsg("ask-missingcategory");
            else if ($("#QuestionText").val() == $("#QuestionText").attr("data-watermark")) showErrorMsg("ask-emptytext");
            else {
                var requestData = {};
                requestData["categoryId"] = $("#AskCategory").val();
                requestData["questionText"] = $("#QuestionText").val();
                currentRequest = $.post($(this).attr("url").unMungeUrl(), requestData, function(data, textStatus, xmlHttpRequest) {
                    if (data == "") {
                    } else if ($.parseJSON(data).Success) {
                        showSuccMsg("ask");
                        setTimeout("$('.askexperts .button.cancel').click();", 5000);
                    } else {
                    }
                    currentRequest = null;
                }, "html").error(function(error) {
                });
            }
        });
        //slides show
        if ($(".slidesshow").length > 0) {
            $(".slidesshow .balls .number").click(function() {
                if ($(this).hasClass("active")) return;

                var currentIndex = $(".slidesshow .balls .number").index($(this));
                showSlideAtNumberX(currentIndex);
                clearTimeout(slideTimeout);
                slideTimeout = setTimeout("slidesShow()", 60000);
            });
            showSlideAtNumberX(0);
            slideTimeout = setTimeout("slidesShow()", 10000);
            $(".slidesshow .bg .text .content").ellipsis({
                lines: 4
            });
        }
        $(".featuredquestion .content").ellipsis({ lines: 2 });
        //userstatus
        if ($(".userstatus .bg").hasClass("notloggedin")) {
            $(".userstatus .bg .loginbutton").text("");
        } else if ($(".userstatus .bg").hasClass("loggedin")) {
            //show profile completion bar
            var percentage = $(".userstatus .profilecompletion").attr("percent");
            var width = Math.round($(".userstatus .completionbar .bg").width() * percentage / 100);
            $(".userstatus .completionbar .fg").width(width);
            $(".userstatus .profilecompletion .percenttext").text(percentage + "%");
        }
        //nav menu
        $("#side-nav a").click(function() {
            if ($("#top-section").length == 0) {
                $(this).parent().parent().click();
                return false;
            }
        });
        $("#side-nav .itemtype").click(function() {
            //redirect for homepage or ajax call for content page
            if ($("#top-section").length > 0)
                window.location = $(this).find("> .bg > a").attr("href");
            else {
                if ($(this).find("> .bg > .icon.arrow").hasClass("expanded") && $(this).find(".category.current, .subcategory.current").length == 0) return;
                if (!$(this).find("> .bg > .icon.arrow").hasClass("expanded")) {
                    //expand or collapse self			
                    $(this).find("> .bg > .icon.arrow").toggleClass("expanded collapsed");
                    $(this).closest(".itemtype").find(".categories").toggleClass("expanded collapsed");
                    //collapse all others
                    $(this).closest(".itemtype").siblings().find("> .bg > .icon.arrow.expanded, .categories.expanded").toggleClass("expanded collapsed");
                }
                partialCall("ItemType", $(this).attr("type"), $(this).find("> .bg > a").attr("href"));
            }
        });
        $("#side-nav .category").click(function(event) {
            event.stopPropagation();
            //redirect for homepage or ajax call for content page
            if ($("#top-section").length > 0)
                window.location = $(this).find("> .bg > a").attr("href");
            else {
                if (!$(this).hasClass("current")) {
                    var url = $(this).find("> .bg  > a").attr("href");
                    var categoryId = url.substring(url.lastIndexOf("=") + 1);
                    //toggle current
                    $(this).closest(".itemtype").find(".category.current, subcategory.current").toggleClass("current");
                    $(this).toggleClass("current");
                    partialCall("Category", categoryId, $(this).find("> .bg > a").attr("href"));
                } else {
                    //expand or collapse self			
                    $(this).find("> .bg > .icon.arrow").toggleClass("expanded collapsed");
                    $(this).closest(".category").find(".subcategories").toggleClass("expanded collapsed");
                    //collapse all others
                    $(this).closest(".category").siblings().find("> .bg > .icon.arrow.expanded, .subcategories.expanded").toggleClass("expanded collapsed");
                }
            }
        });
        $("#side-nav .subcategory").click(function(event) {
            event.stopPropagation();
            //redirect for homepage or ajax call for content page
            if ($("#top-section").length > 0)
                window.location.href = $(this).find("> .bg > a").attr("href");
            else {
                if (!$(this).hasClass("current")) {
                    var url = $(this).find("> .bg  > a").attr("href");
                    var subcategoryId = url.substring(url.lastIndexOf("=") + 1);
                    //toggle current
                    $(this).closest(".itemtype").find(".category.current, subcategory.current").toggleClass("current");
                    $(this).toggleClass("current");
                    partialCall("Subcategory", subcategoryId, $(this).find("> .bg  > a").attr("href"));
                }
            }
        });
        $(".maincontent .tabs .tab").click(function() {
            if ($(this).hasClass("current")) return;
            partialCall("Tab", $(this).attr("itemtype"));
        });
        //update results
        updateResults();
        //vote
        $(".vote .radiobutton").click(function() {
            if ($(this).hasClass("checked")) return;
            $(this).closest("ul").find(".radiobutton").removeClass("checked");
            $(this).closest("ul").find(".VoteControlRadioButton").removeAttr("checked");
            $(this).addClass("checked");
            $(this).next().attr("checked", "checked");
        });
        //right side panel
		$(".middlepart .newarticles .bg.article1 .title").ellipsis({ lines : 2});
        $(".middlepart .newarticles .bg .content").ellipsis({ lines : 6 });
        $(".rightcontent .button.ask, .featuredquestion .titlebar .icon").click(function() {
            if ($("#action-links .user-name_holder").length == 0) {
                showLoginPrompt("Ask");
            } else {
                $(".askexperts").dialog({
                    modal: true,
                    width: 451,
                    closeOnEscape: false,
                    resizable: false,
                    dialogClass: "askexperts-dialog"
                }).find(".button.cancel").click(function() {
                    $(this).closest(".askexperts").dialog("close");
                });
                $(".askexperts .succinfo, .askexperts .errorinfo").hide();
            }
        });
        $(".rightcontent .mostpopular .title").ellipsis({ lines: 2 });
        $(".rightcontent .toprated .content, .rightcontent .mostpopular .content").ellipsis({
            lines: 8
        });
        $(".rightcontent .toprated a, .rightcontent .mostpopular a").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
		$(".rightpart .RSR .bg, .rightcontent .RSR > .bg").click(function() {
			window.open("http://www.redstarresume.com/?affiliateCode=LINKME");
		});
    });

	$(window).load(function() {
		if (window.location.hash) window.location.href = window.location.hash.substring(1);
	});
	
    showErrorMsg = function(type, message) {
        if (type == "ask-missingcategory") message = "You should select a category";
        if (type == "ask-emptytext") message = "You shoud input your question";
        $(".askexperts .errorinfo").text(message).show();
        $(".askexperts .succinfo").hide();
    }

    showSuccMsg = function(type, message) {
        if (type == "ask") message = "Thank you for submitting your question to our experts";
        $(".askexperts .succinfo").text(message).show();
        $(".askexperts .errorinfo").hide();
    }

    showLoginPrompt = function(type) {
        var msg, url;
        url = $(".loginprompt .prompt.login a.login").attr("href");
        if (url.indexOf("?") > 0)
            url = url.substring(0, url.indexOf("?"));
        if (type == "Rating") {
            msg = "You need to be logged in to rate articles.";
            url += "?returnUrl=" + encodeURIComponent($(".article").attr("url"));
        }
        if (type == "Ask") {
            msg = "You need to be logged in to ask your question.";
            url += "?returnUrl=" + encodeURIComponent($(".breadcrumbs").length > 0 ? $(".breadcrumbs li:last a").attr("href") : $(".featuredquestion .titlebar .icon").attr("url").unMungeUrl());
        }
        $(".loginprompt .prompt.text").text(msg);
        $(".loginprompt .prompt.login a.login").attr("href", url);

        $(".loginprompt").dialog({
            modal: true,
            width: 354,
            closeOnEscape: false,
            resizable: false,
            dialogClass: "loginprompt-dialog"
        }).find(".button.cancel").click(function() {
            $(this).closest(".loginprompt").dialog("close");
        });
    }

    partialCall = function(type, param, hash) {
        if (currentRequest)
            currentRequest.abort();

        var requestData = {}, url;

        switch (type) {
            case "Current":
                //go back to previous search/level
                url = param;
                break;
            case "ItemType":
                //My recently viewed
                if (param == "RecentlyViewed") {
                    url = $(".itemtype[type='Article']").attr("recentlyviewedpartialurl").unMungeUrl();
                } else {
                    requestData["ItemType"] = param;
                    url = $(".itemtype[type='" + param + "']").attr("partialurl").unMungeUrl();
                }
                break;
            case "Category":
                requestData["CategoryId"] = param;
                url = $(".categories.expanded").parent().attr("partialurl").unMungeUrl();
                break;
            case "Subcategory":
                requestData["SubcategoryId"] = param;
                if ($(".breadcrumbs .keywords").length > 0) {
                    requestData["Keywords"] = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                    var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                    requestData["CategoryId"] = categoryId;
                }
                url = $(".categories.expanded").parent().attr("partialurl").unMungeUrl();
                break;
            case "Tab":
                requestData["ItemType"] = param;
				if ($("#subheader .breadcrumbs .category").length > 0)
					requestData["CategoryId"] = $("#subheader .breadcrumbs .category").attr("categoryid");
				if ($("#subheader .breadcrumbs .subcategory").length > 0)
					requestData["SubcategoryId"] = $("#subheader .breadcrumbs .subcategory").attr("subcategoryid");
                if ($(".breadcrumbs .keywords").length > 0) {
                    requestData["Keywords"] = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                    var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                    requestData["CategoryId"] = categoryId;
                }
				var fullUrl = "";
                if ($(".breadcrumbs .recentlyviewed").length > 0) {
					url = $(".itemtype[type='" + param + "']").attr("recentlyviewedpartialurl").unMungeUrl();
					fullUrl = $("#side-nav .itemtype[type='RecentlyViewed'] a." + param).attr("href");
				} else {
					url = $(".itemtype[type='" + param + "']").attr("partialurl").unMungeUrl();
					fullUrl = $(".itemtype[type='" + param + "'] > .bg > a").attr("href");
				}
				hash = fullUrl + "?" + $.param(requestData);
                break;
            case "Sort":
                requestData["ItemType"] = $("#subheader .breadcrumbs .itemtype").attr("itemtype");
				if ($("#subheader .breadcrumbs .category").length > 0)
					requestData["CategoryId"] = $("#subheader .breadcrumbs .category").attr("categoryid");
				if ($("#subheader .breadcrumbs .subcategory").length > 0)
					requestData["SubcategoryId"] = $("#subheader .breadcrumbs .subcategory").attr("subcategoryid");
                if ($(".breadcrumbs .keywords").length > 0) {
                    requestData["Keywords"] = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                    var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                    requestData["CategoryId"] = categoryId;
                }
                url = $(".itemtype[type='" + $("#subheader .breadcrumbs .itemtype").attr("itemtype") + "']").attr("partialurl").unMungeUrl();
				hash = $(".itemtype[type='" + $("#subheader .breadcrumbs .itemtype").attr("itemtype") + "'] .bg a").attr("href") + "?" + $.param(requestData);
                break;
            case "Pagination":
                requestData["ItemType"] = $("#subheader .breadcrumbs .itemtype").attr("itemtype");
                requestData["CategoryId"] = $("#subheader .breadcrumbs .category").attr("categoryid");
                requestData["SubcategoryId"] = $("#subheader .breadcrumbs .subcategory").attr("subcategoryid");
                requestData["Page"] = param;
                if ($(".breadcrumbs .keywords").length > 0) {
                    requestData["Keywords"] = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                    var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                    requestData["CategoryId"] = categoryId;
                }
                url = $(".itemtype[type='" + $("#subheader .breadcrumbs .itemtype").attr("itemtype") + "']").attr("partialurl").unMungeUrl();
                break;
            case "ResourceItem":
                if ($(".breadcrumbs .keywords").length > 0) {
                    requestData["Keywords"] = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                    var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                    requestData["CategoryId"] = categoryId;
                }
                url = param;
                break;
            case "RecentlyViewed":
                url = $(".itemtype[type='" + param + "']").attr("recentlyviewedpartialurl").unMungeUrl();
                break;
            case "Search":
                requestData["ItemType"] = $("#subheader .breadcrumbs .itemtype").attr("itemtype");
                requestData["Keywords"] = $("#Keywords").val() == $("#Keywords").attr("data-watermark") ? "" : $("#Keywords").val();
                var categoryId = $("#SearchCategory option:selected").val() == "-1" ? "" : $("#SearchCategory option:selected").val();
                requestData["CategoryId"] = categoryId;
                url = $(".itemtype[type='" + $("#subheader .breadcrumbs .itemtype").attr("itemtype") + "']").attr("partialurl").unMungeUrl();
				hash = $("#subheader .search .button.search").attr("url").unMungeUrl() + "?" + $.param(requestData);
                break;
        }

        currentRequest = $.post(url, requestData, function(data, textStatus, xmlHttpRequest) {
			if (data == "") {
			} else if (data.Success) {
			} else {
				$(".maincontent .tabcontent .bg").html(data);
				updateResults();
			}
			currentRequest = null;
		}, "html").error(function(error) {
		});
		//set hash
		if (hash) window.location.hash = "#" + hash;
    }

    //update results, called after partial calls or when page init
    updateResults = function() {
        //update breadcrumbs and tab numbers etc.
        updateBreadcrumbs();
        updateTabs();
        updateTabNumbers();
        updateNavMenu();
        //update search criteria
        updateSearchCriteria();
        //title and content ellipsis for list & pagination
        updateEllipsis();
        updatePagination();
        //update recent viewed and related items under tabs
        updateRelatedItems();
        updateRecentViewed();
        //init data & events
        initData();
        initEvents();
    }

    updateBreadcrumbs = function() {
        $("#subheader .breadcrumbs").remove();
        $(".tabcontent .bg .breadcrumbs").appendTo($("#subheader"));
    }

    updateTabs = function() {
        $(".maincontent .tabs .tab").removeClass("current");
        $(".maincontent .tabs .tab." + $("#subheader .breadcrumbs .itemtype").attr("itemtype")).addClass("current");
    }

    updateTabNumbers = function() {
        $(".tabs .tab.Article .count").html($(".tabcontent .tabnumbers .Article").html());
        $(".tabs .tab.Video .count").html($(".tabcontent .tabnumbers .Video").html());
        $(".tabs .tab.QnA .count").html($(".tabcontent .tabnumbers .QnA").html());
    }

    updateNavMenu = function() {
        //item type
        var itemType = $("#subheader .breadcrumbs .itemtype").attr("itemtype");
        if (!itemType) itemType = "Article";
        $("#side-nav .itemtype > .bg .icon.arrow, #side-nav .itemtype .categories").addClass("collapsed").removeClass("expanded");
        $("#side-nav .itemtype[type='" + itemType + "'] > .bg .icon.arrow, #side-nav .itemtype[type='" + itemType + "'] .categories").toggleClass("expanded collapsed");
        $("#side-nav .itemtype[type='" + itemType + "'] .current").toggleClass("current");
        //category
        $("#side-nav .itemtype[type='" + itemType + "'] .category > .bg .icon.arrow, #side-nav .itemtype[type='" + itemType + "'] .category .subcategories").addClass("collapsed").removeClass("expanded");
        if ($("#subheader .breadcrumbs .category").length > 0) {
            var categoryId = $("#subheader .breadcrumbs .category").attr("categoryid");
            if ((!$("#side-nav .itemtype[type='" + itemType + "'] .category[categoryid='" + categoryId + "'] .subcategories").hasClass("expanded")) && (!$("#side-nav .itemtype[type='" + itemType + "'] .category[categoryid='" + categoryId + "'] .subcategories").hasClass("current"))) {
                $("#side-nav .itemtype[type='" + itemType + "'] .category > .bg .icon.arrow, #side-nav .itemtype[type='" + itemType + "'] .category .subcategories").addClass("collapsed").removeClass("expanded");
                $("#side-nav .itemtype[type='" + itemType + "'] .category[categoryid='" + categoryId + "'] > .bg .icon.arrow, #side-nav .itemtype[type='" + itemType + "'] .category[categoryid='" + categoryId + "'] .subcategories").toggleClass("expanded collapsed");
                if ($("#subheader .breadcrumbs .subcategory").length == 0) $("#side-nav .itemtype[type='" + itemType + "'] .category[categoryid='" + categoryId + "']").addClass("current");
            }
        } else {
            $("#side-nav .itemtype[type='" + itemType + "'] .category > .bg .icon.arrow, #side-nav .itemtype[type='" + itemType + "'] .category .subcategories").addClass("collapsed").removeClass("expanded");
        }
        //subcategory
        if ($("#subheader .breadcrumbs .subcategory").length > 0) {
            var subcategoryId = $("#subheader .breadcrumbs .subcategory").attr("subcategoryid");
            $("#side-nav .itemtype[type='" + itemType + "'] .subcategory[subcategoryid='" + subcategoryId + "']").addClass("current");
        }
        //Recently Viewed List
        if ($("#subheader .breadcrumbs .recentlyviewed").length > 0) {
            $("#side-nav .itemtype[type='" + itemType + "'] > .bg .icon.arrow, #side-nav .itemtype[type='" + itemType + "'] .categories").toggleClass("expanded collapsed");
        }
    }

    updateSearchCriteria = function() {
        $("#SearchCategory option:selected").removeAttr("selected");
        if ($(".breadcrumbs .keywords").length > 0) {
            $("#Keywords").val($(".breadcrumbs .keywords .red").text()).removeClass("masked");
            var categoryId = $(".breadcrumbs .category").length > 0 ? $(".breadcrumbs .category").attr("categoryid") : "-1";
            $("#SearchCategory option[value='" + categoryId + "']").attr("selected", "selected");
        } else {
            $("#Keywords").val($("#Keywords").attr("data-watermark")).addClass("masked");
            $("#SearchCategory option[value='-1']").attr("selected", "selected");
        }
        $(".search .categorylist .selected").text($("#SearchCategory option:selected").text());
    }

    updateEllipsis = function() {
		if ($(".breadcrumbs .keywords").length > 0) return;
        $(".articleitem .title").ellipsis({
            lines: 2
        });
        $(".articleitem .content").ellipsis({
            lines: 3
        });
		$(".answeredquestionitem .content").ellipsis({ lines : 3 });
    }

    updatePagination = function() {
        var margin = ($(".pagination-container").width() - $(".pagination-holder").width()) / 2;
        $(".pagination-holder").css("margin-left", margin + "px");
    }

    updateRelatedItems = function() {
        $(".relatedcontent").html("").append($(".relatedlist"));
    }

    updateRecentViewed = function() {
        $(".recentcontent").html("").append($(".recentlist"));
    }

    initData = function() {
        //sort order
        $(".sort .sorttext").text(getSortText());
		//empty p in IE7
		if ($.browser.msie && $.browser.version.indexOf("7") >= 0)
			$(".article .articlearea .content p:empty").addClass("empty");
        //video details
        if ($(".videolist .videoitem").length > 0)
            $(".videolist .videoitem").each(function() {
                var id = $(this).attr("externalvideoid");
				var url = $(this).attr("videodetailurl").unMungeUrl() + "?externalVideoId=" + id;
				$.getJSON(url, function(data) {
						var video = $.parseJSON(data).data;
						$(".videolist .videoitem[externalvideoid='" + id + "'] .preview").attr("src", video.thumbnail.hqDefault);
						$(".videolist .videoitem[externalvideoid='" + id + "'] .transcript").ellipsis({ lines: 3 });
						$(".videolist .videoitem[externalvideoid='" + id + "'] .duration").text("Duration - " + Math.floor(video.duration / 60) + ":" + (video.duration % 60 < 10 ? "0" : "") + (video.duration % 60) + " min");
					}
				);
            });
        if ($(".video").length > 0) {
			var id = $(".video").attr("externalvideoid");
			var url = $(".video").attr("videodetailurl").unMungeUrl() + "?externalVideoId=" + id;
        }
        //answered question disqus
        disqus_identifier = $(".answeredquestion").attr("answeredquestionid");
        disqus_url = $(".answeredquestion").attr("url");
        disqus_shortname = "linkme";
        if ($("#disqus_thread").length > 0) {
            var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
            dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
            if ($("head script[src*='.disqus.com'], head link[href*='.disqus.com']").length > 0)
                $("head script[src*='.disqus.com'], head link[href*='.disqus.com']").remove();
            (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
        }
    }

    initEvents = function() {
        //sort order
        $("#SortOrder").change(function() {
            partialCall("Sort");
        });
        $(".sortarea .sort .ascending, .sortarea .sort .descending").click(function() {
            $(".sortarea .sort .ascending, .sortarea .sort .descending").toggleClass("active");
            partialCall("Sort");
        });
        //pagination
        $(".pagination-container a.page").click(function() {
            partialCall("Pagination", $(this).attr("page"), $(this).attr("href"));
			return false;
        });
        //all links
        $(".articleitem .readfull a, .articleitem .leftside .toparea .title").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".article .back a").click(function() {
            partialCall("Current", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".videolist .videoitem a.title, .videolist .videoitem .button.watch a").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".video .back a").click(function() {
            partialCall("Current", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".answeredquestionitem .readfull a, .answeredquestionitem .leftside .title").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".answeredquestion .back a").click(function() {
            partialCall("Current", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".recentlist .item .title").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".recentlist .viewmore a").click(function() {
            partialCall("RecentlyViewed", $(this).attr("type"), $(this).attr("href"));
            return false;
        });
        $(".relatedlist .item .title").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        //breadcrumbs
        $(".breadcrumbs .itemtype").click(function() {
            partialCall("ItemType", $(this).attr("itemtype"), $(this).find("a").attr("href"));
            return false;
        });
        $(".breadcrumbs .category").click(function() {
            partialCall("Category", $(this).attr("categoryid"), $(this).find("a").attr("href"));
            return false;
        });
        $(".breadcrumbs .subcategory").click(function() {
            partialCall("Subcategory", $(this).attr("subcategoryid"), $(this).find("a").attr("href"));
            return false;
        });
        $(".breadcrumbs .item").click(function() {
            partialCall("ResourceItem", $(this).attr("partialurl").unMungeUrl(), $(this).attr("href"));
            return false;
        });
        $(".breadcrumbs .recentlyviewed").click(function() {
            partialCall("RecentlyViewed", $(this).attr("type"), $(this).attr("href"));
            return false;
        });
        //article rating
        $(".article .ratingarea .rating .star").hover(function() {
            //not logged in or not rated, return
            if ($(".userstatus .bg").hasClass("notloggedin") || $(this).parent().attr("userrating") != "0") return;
            var index = $(this).parent().find(".star").index($(this));
            $(this).parent().find(".star:lt(" + (index + 1) + ")").removeClass("empty");
        }, function() {
            //not logged in or not rated, return
            if ($(".userstatus .bg").hasClass("notloggedin") || $(this).parent().attr("userrating") != "0") return;
            var index = $(this).parent().find(".star").index($(this));
            $(this).parent().find(".star:lt(" + (index + 1) + ")").addClass("empty");
        }).click(function() {
            if ($(".userstatus .bg").hasClass("notloggedin")) {
                showLoginPrompt("Rating");
            } else {
                var index = $(this).parent().find(".star").index($(this));
                $(this).parent().attr("userrating", index + 1);
                $(this).parent().find(".star:lt(" + (index + 1) + ")").removeClass("empty");
                $(this).parent().find(".star:gt(" + index + ")").addClass("empty");
                $.ajax({
                    type: "POST",
                    url: $(this).parent().attr("rateurl").unMungeUrl(),
                    data: JSON.stringify({
                        rating: index + 1
                    }),
                    dataType: "json",
                    contentType: "application/json",
                    success: function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                        } else if (data.Success) {
                            $(".article .ratingarea .rating .ratingsaved").show().delay(3000).fadeOut("slow");
                        } else {
                        }
                    },
                    error: function(error) {
                    }
                });
            }
        });
        //youtube player
        if ($(".video").length > 0) {
            var params = { allowScriptAccess : "always", allowfullscreen : "true", wmode : "transparent" };
            var atts = { id: "lmytplayer" };
            swfobject.embedSWF($("#ytapiplayer").attr("url"), "ytapiplayer", "510", "376", "8", null, null, params, atts);
        }
        //social
        if (typeof gapi != "undefined" && typeof gapi.plusone != "undefined") gapi.plusone.go();
        if (typeof FB != "undefined" && typeof FB.XFBML != "undefined") FB.XFBML.parse();
        if (typeof twttr != "undefined" && typeof twttr.widgets != "undefined") twttr.widgets.load();
        //view
        if ($(".tabcontent .article").length > 0)
            $.ajax({
                type: "POST",
                url: $(".tabcontent .article").attr("viewurl").unMungeUrl(),
                data: "",
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                },
                error: function(error) {
                }
            });
        if ($(".tabcontent .answeredquestion").length > 0)
            $.ajax({
                type: "POST",
                url: $(".tabcontent .answeredquestion").attr("viewurl").unMungeUrl(),
                data: "",
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                },
                error: function(error) {
                }
            });
    }

    onYouTubePlayerReady = function(playerId) {
        ytplayer = document.getElementById("lmytplayer");
        ytplayer.addEventListener("onStateChange", "onytplayerStateChange");
    }

    onytplayerStateChange = function(newState) {
        if (newState == 1 && $("#lmytplayer").attr("viewed") != "viewed")
            $.ajax({
                type: "POST",
                url: $(".tabcontent .video").attr("viewurl").unMungeUrl(),
                data: "",
                dataType: "json",
                contentType: "application/json",
                success: function(data, textStatus, xmlHttpRequest) {
                    $("#lmytplayer").attr("viewed", "viewed");
                },
                error: function(error) {
                }
            });
    }

    getSortText = function() {
        var sortText = "";
        if ($("#SortOrder option:selected").attr("value") == "CreatedTime") {
            if ($(".sort .descending").hasClass("active")) sortText = "Most recent to oldest";
            else sortText = "Oldest to most recent";
        } else if ($("#SortOrder option:selected").attr("value") == "Popularity" || $("#SortOrder option:selected").attr("value") == "Relevance") {
            if ($(".sort .descending").hasClass("active")) sortText = "Most to least";
            else sortText = "Least to most";
        }
        return sortText;
    }

    showSlideAtNumberX = function(index) {
        $(".slidesshow .balls .number.active, .slidesshow .balls .number:eq(" + index + "), .slidesshow .bg.active, .slidesshow .bg:eq(" + index + ")").toggleClass("active");
        if ($(".slidesshow .bg:eq(" + index + ") .text .nametitle").height() < 66)
            $(".slidesshow .bg:eq(" + index + ") .text .content").css({
                "margin-top": "33px"
            });
    }

    slidesShow = function(type) {
        var currentIndex = $(".slidesshow .balls .number").index($(".slidesshow .balls .number.active"));
        if (currentIndex == $(".slidesshow .balls .number").length - 1) currentIndex = -1;
        showSlideAtNumberX(currentIndex + 1);
        slideTimeout = setTimeout("slidesShow()", 10000);
    }
})(jQuery);