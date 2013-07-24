(function($) {

    var cInstance = null;
    cInitCallback = function(c) {
        cInstance = c;
    };

    createJoinLayer = function() {
        if ($(".join-section").css("display") == "block") {
            if (($.browser.msie) && ($.browser.version == "7.0")) {
                $(".find-jobs").find(".shaded-box-content").show();
                /*cInstance.reload();*/
            }
            $(".join-section").fadeOut(200);
            $(".join-error").hide();
            $(".join-section").find("input[type=text]").each(function() {
                $(this).val("");
            });
            $(".join-section").find("input[type=password]").each(function() {
                $(this).val("");
            });
            var termsChk = $(".join-section").find("#AcceptTerms");
            if ($(termsChk).is(":checked")) {
                $(termsChk).removeAttr("checked");
                $(termsChk).val(false);
                $(".join-section").find("#AcceptTerms").each(function() {
                    this.refresh();
                });
            }
        } else {
            $(".join-section").show();
            if (($.browser.msie) && ($.browser.version == "7.0") && ($(".join-bg").height() > 380)) {
                $(".find-jobs").find(".shaded-box-content").hide();
            }
        }
    }

    expandHelp = function(obj) {
        if ($(obj).hasClass("more-link")) {
            $(".expanded-help-text").show();
            $(obj).removeClass("more-link").addClass("hide-link");
            $(obj).text("hide");
        } else {
            $(".expanded-help-text").hide();
            $(obj).removeClass("hide-link").addClass("more-link");
            $(obj).text("more");
        }
    }

    expandSearch = function(obj) {
        if ($(obj).hasClass("more-search-link")) {
            $(".expanded-search").show();
            $(obj).removeClass("more-search-link").addClass("hide-search-link");
            $(obj).text("HIDE OPTIONS");
        } else {
            $(".expanded-search").hide();
            $(obj).removeClass("hide-search-link").addClass("more-search-link");
            $(obj).text("MORE OPTIONS");
        }
    }

    loadPage = function(url) {
        window.location = url;
    }

    validateLogin = function() {
        var loginId = $(".login-section").find("#LoginId");
        var password = $(".login-section").find("#Password");
        var rememberMe = $(".login-section").find("#RememberMe");

        if (loginId.hasClass("text-label") || loginId.val() == loginId.attr("data-watermark") || loginId.val() == null) {
            loginId.val('');
        }
        if (password.hasClass("text-label") || password.val() == password.attr("data-watermark") || password.val() == null) {
            password.val('');
        }

        rememberMe.val(rememberMe.is(":checked") ? true : false);

        $('#LoginForm').submit();
    }

    validateJoin = function() {
        if ($("#AcceptTerms").is(":checked")) {
            $("#AcceptTerms").val(true);
        }

        $('#JoinForm').submit();
    }

    /* Salary slider */

    toFormattedDigits = function(nValue) {
        var s = nValue.toString();
        for (j = 3; j < 15; j += 4) {  // insert commas
            if ((s.length > j) && (s.length > 4)) {
                s = s.substr(0, s.length - j) + ',' + s.substr(s.length - j);
            }
        }
        return (s);
    }

    initializeSalary = function(lowerBound, upperBound, minSalary, maxSalary, stepSalary) {

        setSalarySliderLabel = function(lowerBound, upperBound) {

            var label = "";
            if (lowerBound == minSalary && upperBound == maxSalary) {
                /*label = "$" + toFormattedDigits(maxSalary) + "+";*/
                label = "Any salary";
            }
            else if (lowerBound == minSalary) {
                label = "up to $" + toFormattedDigits(upperBound);
            }
            else if (upperBound == maxSalary) {
                label = "$" + toFormattedDigits(lowerBound) + "+";
            }
            else {
                label = "$" + toFormattedDigits(lowerBound) + " - $" + toFormattedDigits(upperBound);
            }

            $("#salary-range").parent("div").parent("div").find("div.range:eq(0)").html(label);
        }

        setSalarySliderValues = function(lowerBound, upperBound) {
            if (lowerBound == null)
                lowerBound = minSalary;
            if (upperBound == null)
                upperBound = maxSalary;
            setSalarySliderLabel(lowerBound, upperBound);
            $("#salary-range").slider("option", "values", [lowerBound, upperBound]);
        }

        setDefaultSalarySliderValues = function() {
            setSalarySliderValues(null, null);
        }

        $("#salary-range").slider({
            range: true,
            min: minSalary,
            max: maxSalary,
            step: stepSalary,
            values: [minSalary, maxSalary],
            slide: function(event, ui) {
                setSalarySliderLabel(ui.values[0], ui.values[1]);
                $("#SalaryLowerBound").val(ui.values[0]);
                $("#SalaryUpperBound").val(ui.values[1]);
            }
        });

        $("#salary-range").parent("div").parent("div").find("div.minrange:eq(0)").html('$' + toFormattedDigits(minSalary));
        $("#salary-range").parent("div").parent("div").find("div.maxrange:eq(0)").html('$' + toFormattedDigits(maxSalary) + '+');

        setSalarySliderValues(lowerBound, upperBound);
    }

    searchJobs = function(searchUrl, jobTypes, allJobTypes, minSalary, maxSalary) {

        var keywords = $("#Keywords");
        var location = $("#Location");
        if (!keywords.hasClass("text-label") && keywords.val() != keywords.attr("data-watermark") && keywords.val() != null) {
            searchUrl = searchUrl + "&keywords=" + escape(keywords.val());
        }
        if (!location.hasClass("text-label") && location.val() != location.attr("data-watermark") && location.val() != null) {
            searchUrl = searchUrl + "&location=" + escape(location.val());
        }

        var searchJobTypes = 0;
        for (var index = 0; index < jobTypes.length; ++index) {
            var jobType = jobTypes[index];
            if ($("#" + jobType.name).is(":checked"))
                searchJobTypes = searchJobTypes + jobType.value;
        }

        if (searchJobTypes != 0 && searchJobTypes != allJobTypes)
            searchUrl = searchUrl + "&JobTypes=" + searchJobTypes;

        var salaryLowerBound = $("#SalaryLowerBound");
        if (salaryLowerBound.length > 0 && salaryLowerBound.val() != minSalary)
            searchUrl = searchUrl + "&SalaryLowerBound=" + salaryLowerBound.val();

        var salaryUpperBound = $("#SalaryUpperBound");
        if (salaryUpperBound.length > 0 && salaryUpperBound.val() != maxSalary)
            searchUrl = searchUrl + "&SalaryUpperBound=" + salaryUpperBound.val();

        var includeNoSalary = $("#IncludeNoSalary");
        if (includeNoSalary.length > 0 && !includeNoSalary.is(":checked"))
            searchUrl = searchUrl + "&IncludeNoSalary=false";

        loadPage(searchUrl);
    }

    $(document).ready(function() {
		$(".find-jobs .section-tab").click(function() {
			var currentTab = $(this);
			if (currentTab.hasClass("section-tab-selected")) return false;
			$(".section-tab-content").hide();
			$(".section-tab").removeClass("section-tab-selected");
			currentTab.addClass("section-tab-selected");
			$("#" + currentTab.attr("id") + "-content").show();
			if (currentTab.attr("id") == "industry-tab")
				$("#industry-tab-content .row .item span").each(function(index, element) {
					if ($(element).height() > 11) $(element).css("margin-top", "-3px");
				});
			return false;
		});

		function generatePushCheckParameters(cssClassPrefix) {
			return {
				cssClass: cssClassPrefix + "_pushcheck pushcheck",
				hoverClass: cssClassPrefix + "_pushcheck-hover pushcheck-hover",
				downClass: cssClassPrefix + "_pushcheck-down pushcheck-down",
				hoverDownClass: cssClassPrefix + "_pushcheck-down pushcheck-down",
				checkedClass: cssClassPrefix + "_pushcheck-checked pushcheck-checked",
				checkedHoverClass: cssClassPrefix + "_pushcheck-checked-hover pushcheck-checked-hover",
				checkedDownClass: cssClassPrefix + "_pushcheck-checked-down pushcheck-checked-down",
				checkedHoverDownClass: cssClassPrefix + "_pushcheck-checked-down pushcheck-checked-down",
				mutuallyExclusiveStateClasses: false,
				labelTextInside: false
			};
		}

		$(".homepage-checkbox").customCheckbox(generatePushCheckParameters("homepage-checkbox"));
		
		/* Login on hit of Enter key */
		$(".login-submit").keypress(function(e) {
			if ((e.keyCode || e.which) == 13) {
				$("#login").click();
			}
		});

		/* Join on hit of Enter key */
		$(".join-submit").keypress(function(e) {
			if ((e.keyCode || e.which) == 13) {
				$("#join").click();
			}
		});

		/* Search on hit of Enter key */
		$(".search-submit").keypress(function(e) {
			if ((e.keyCode || e.which) == 13) {
				$("#search").click();
			}
		});

		/* Initilization for Location autocomplete */
		$("#Location").autocomplete(apiPartialMatchesUrl);
		//by industry
		organiseFields($("#industry-tab-content"));
		$("#industry-tab-content #IndustriesIndustry option").each(function(index, element) {
			$("#industry-tab-content .row:eq(" + (index % 10) + ")").append("<div class='item'><div class='checkbox' value='" + $(element).attr("value") + "'></div><span>" + $(element).text() + "</span></div>");
		});
		$("#industry-tab-content .row .item .checkbox").click(function() {
			if ($(this).hasClass("disabled")) return;
			if ($(this).hasClass("checked")) {
				$("#industry-tab-content #IndustriesIndustry option[value='" + $(this).attr("value") + "']").removeAttr("selected");
				$(this).removeClass("checked");
				if ($("#industry-tab-content #IndustriesIndustry option:selected").length == 0)
					$("#industry-tab-content .nowenter").hide();
				else $("#industry-tab-content .nowenter").show();
			} else {
				$("#industry-tab-content #IndustriesIndustry option[value='" + $(this).attr("value") + "']").attr("selected", "selected");
				$(this).addClass("checked");
				if ($("#industry-tab-content #IndustriesIndustry option:selected").length == 0)
					$("#industry-tab-content .nowenter").hide();
				else $("#industry-tab-content .nowenter").show();
			}
		});
		$("#industry-tab-content .selectall").click(function() {
			$("#industry-tab-content .row .item .checkbox").each(function(index, element) {
				if (!$(element).hasClass("checked")) $(element).click();
			});
			$("#industry-tab-content .nowenter").show();
		});
		$("#industry-tab-content .clearall").click(function() {
			$("#industry-tab-content .row .item .checkbox").each(function(index, element) {
				if ($(element).hasClass("checked")) $(element).click();
			});
			$("#industry-tab-content .nowenter").hide();
		});
		$("#industry-tab-content #LocationIndustry").autocomplete(apiPartialMatchesUrl);
		$("#industry-tab-content form").submit(function() {
			$(this).find("#LocationIndustry").val($("#industry-tab-content #LocationIndustry").realValue());
		});
		//location
		organiseFields($("#location-tab-content"));
		$("map[name='mapAus'] area").hover(function() {
			$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "'], .aus-map .region[state='" + $(this).attr("areafor") + "']").addClass("hover");
			if ($(".aus-map .state[state='" + $(this).attr("areafor") + "']").length > 0)
				$(".aus-map .region[state='" + $(this).attr("areafor") + "']").addClass("state");
		}, function() {
			$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "'], .aus-map .region[state='" + $(this).attr("areafor") + "']").removeClass("hover");
			if (!$(".aus-map .region[state='" + $(this).attr("areafor") + "']").hasClass("active") && $(".aus-map .state[state='" + $(this).attr("areafor") + "']").length > 0)
				$(".aus-map .region[state='" + $(this).attr("areafor") + "']").removeClass("state");
		}).click(function() {
			if ($(".aus-map .pin").attr("pinfor") == $(this).attr("areafor")) {
				$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "'], .aus-map .region[state='" + $(this).attr("areafor") + "']").removeClass("active");
				$(".aus-map .pin").attr("pinfor", "");
				$("#location-tab-content .Industry_field label span").text("");
				$("#location-tab-content #browseByLocation").hide();
			} else {
				$(".aus-map .state, .aus-map .region").removeClass("active");
				$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "'], .aus-map .region[state='" + $(this).attr("areafor") + "']").addClass("active");
				$(".aus-map .pin").attr("pinfor", $(this).attr("areafor"));
				$("#AnyWhereInAus .checkbox").removeClass("checked");
				$("#location-tab-content .Industry_field label span").text($(this).attr("areafor"));
				$("#location-tab-content #browseByLocation").show();
			}
			if ($(".aus-map .state[state='" + $(this).attr("areafor") + "']").length > 0)
				$(".aus-map .region[state='" + $(this).attr("areafor") + "']").addClass("state");
			else $(".aus-map .region[region='" + $(this).attr("areafor") + "']").removeClass("state");
		});
		$(".aus-map .region[region='Sydney']").attr("state", "New South Wales");
		$(".aus-map .region[region='Darwin']").attr("state", "Northern Territory");
		$(".aus-map .region[region='Brisbane']").attr("state", "Queensland");
		$(".aus-map .region[region='Adelaide']").attr("state", "South Australia");
		$(".aus-map .region[region='Hobart']").attr("state", "Tasmania");
		$(".aus-map .region[region='Melbourne']").attr("state", "Victoria");
		$(".aus-map .region[region='Perth']").attr("state", "Western Australia");
		$(".aus-map .region[region='Gold Coast']").attr("state", "Queensland");
		$("#AnyWhereInAus .checkbox").click(function(event) {
			if ($(this).hasClass("disabled")) return;
			if ($(this).hasClass("checked")) {
				$("#location-tab-content .Industry_field label span").text("");
				$(".aus-map .state, .aus-map .region").removeClass("active");
				$(".aus-map .region").removeClass("state");
				$(".aus-map .pin").attr("pinfor", "");
				$(this).removeClass("checked");
				$("#location-tab-content #browseByLocation").hide();
			} else {
				$("#location-tab-content .Industry_field label span").text("Australia");
				$(".aus-map .state, .aus-map .region").addClass("active");
				$(".aus-map .region").addClass("state");
				$(".aus-map .pin").attr("pinfor", "Australia");
				$(this).addClass("checked");
				$("#location-tab-content #browseByLocation").show();
			}
			event.stopPropagation();
		});
		$("#location-tab-content .Industry_field label").append("<span></span>");
		$("#location-tab-content .Industry_field select").each(function() {
			var textBg = $("<div class='bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='leftbar'></div>").after("<div class='rightbar'></div>");
		});
		$("#location-tab-content .Industry_field select").prepend("<option value='all' selected='selected'>All</option>").attr("size", "6").addClass("dropdown").closest(".control").addClass("dropdown_control").closest(".field").addClass("dropdown_field");
		$("#location-tab-content #IndustriesLocation").parent().dropdown();
		$("#location-tab-content form").submit(function() {
			var location = $("#location-tab-content .Industry_field label span").text();
			//if (location == "Australia") location = "";
			$(this).find("#LocationLocation").val(location);
			if ($(this).find("#IndustriesLocation option:selected").attr("value") == "all") $(this).find("#IndustriesLocation option").attr("selected", "selected");
		});

		$(".inputs_for_sliders").hide();

        $('input.js_watermarked').each(function() {
            if (this.value == '') {
                this.value = $(this).attr('data-watermark');
                $(this).addClass('text-label');
            }
            $(this).focus(function() {
                if (($(this).hasClass('text-label')) || (this.value == $(this).attr('data-watermark'))) {
                    this.value = '';
                    $(this).removeClass('text-label');
                }
                $(".login-error").fadeOut(2000);
            });
            $(this).blur(function() {
                if (this.value == '') {
                    this.value = $(this).attr('data-watermark');
                    $(this).addClass('text-label');
                }
            });
            $(this).hover(function() {
                if (($(this).hasClass('text-label')) && (!(this.value == $(this).attr('data-watermark')))) {
                    $(this).removeClass('text-label');
                }
            });
        });

        $('input.js_password-watermarked').each(function() {
            if ((this.value == '') && (this.type == "text")) {
                this.value = $(this).attr('data-watermark');
                $(this).addClass('text-label');
            }
            $(this).focus(function() {
                if (this.type == "text") {
                    $(this).parent().find("#Password").val('');
                    $(this).parent().find("#Password").show();
                    $(this).parent().find("#Password").focus();
                    $(".login-error").fadeOut(2000);
                    $(this).hide();
                }
            });
            $(this).blur(function() {
                if (this.type == "password") {
                    if (this.value == '') {
                        $(this).parent().find("#DefaultPassword").val($(this).attr('data-watermark'));
                        $(this).parent().find("#DefaultPassword").addClass('text-label');
                        $(this).parent().find("#DefaultPassword").show();
                        $(this).hide();
                    }
                }
            });
        });

        $(".join-section").find("input[type=text]").each(function() {
            $(this).focus(function() {
                $("#" + $(this).attr("id") + "_error").fadeOut(500);
            });
        });
        $(".join-section").find("input[type=password]").each(function() {
            $(this).focus(function() {
                $("#" + $(this).attr("id") + "_error").fadeOut(500);
            });
        });
        $(".join-section").find("#AcceptTerms").each(function() {
            $(this).click(function() {
                $("#" + $(this).attr("id") + "_error").fadeOut(500);
            });
        });

        if ($("#jobad-ticker-items ul li").length > 0)
            $("#jobad-ticker-items").jCarouselLite({
                initCallback: cInitCallback,
                vertical: true,
                hoverPause: true,
                visible: 2,
                auto: 3000, /* Speed set to 3 seconds */
                speed: 1000
            });

        if ($("#candidate-search-ticker-items ul li").length > 0)
            $("#candidate-search-ticker-items").jCarouselLite({
                initCallback: cInitCallback,
                vertical: true,
                hoverPause: true,
                visible: 2,
                auto: 3000, /* Speed set to 3 seconds */
                speed: 1000
            });

        if (($.browser.msie) && ($.browser.version == "7.0") && ($(".join-bg").height() > 380)) {
            $(".find-jobs").find(".shaded-box-content").hide();
        }
		
		//expose yourself
		$(".expose-yourself .play").click(function() {
			$(this).hide().next().show();
		});

        $('img.featured-link').hover(function() {
            var imgSource = $(this).attr("src");
            var position = imgSource.lastIndexOf(".");
            var hoverImgSource = [imgSource.slice(0, position), "_over", imgSource.slice(position)].join('');
            $(this).attr("src", hoverImgSource);
        }, function() {
            var imgSource = $(this).attr("src").replace("_over", "");
            $(this).attr("src", imgSource);
        });

		if ($(".firstlogin").length > 0) {
			$(".firstlogin").dialog({
				modal: true,
				width: 751,
				closeOnEscape: true,
				resizable: false,
				dialogClass: "firstlogin-dialog"
			}).find(".icon.close").click(function() {
				$(this).closest(".firstlogin").dialog("close");
			});
			$(".firstlogin .button.candidate").click(function() {
				//partial call to save then close dialog
				$.ajax({
					type: "POST",
					url: $(this).closest(".firstlogin").attr("url").unMungeUrl(),
					data: JSON.stringify({ userType : "Member" }),
					async: true,
					dataType: "json",
					contentType: "application/json",
					success: function(data, textStatus, xmlHttpRequest) {
					},
					error: function(error) {
					}
				});
				$(this).prev().click();
			});
			$(".firstlogin .button.employer").click(function() {
				//partial call to save then redirect
				$.ajax({
					type: "POST",
					url: $(this).closest(".firstlogin").attr("url").unMungeUrl(),
					data: JSON.stringify({ userType : "Employer" }),
					async: true,
					dataType: "json",
					contentType: "application/json",
					success: function(data, textStatus, xmlHttpRequest) {
					},
					error: function(error) {
					}
				});
				window.location = $(this).attr("url");
			});
		}

		if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
            var json = document.createElement('script'); json.type = 'text/javascript'; json.async = true;
            json.src = LinkMeUI.Locations.JSON;
            (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(json);
		}
		
		//3 youtube videos
		var params = { allowScriptAccess : "always", allowfullscreen : "true", wmode : "transparent" };
		var atts = { id: "whatislinkme" };
		swfobject.embedSWF($("#whatislinkme").attr("url"), "whatislinkme", "290", "245", "8", null, null, params, atts);
		atts = { id : "whyuselinkme" };
		swfobject.embedSWF($("#whyuselinkme").attr("url"), "whyuselinkme", "290", "245", "8", null, null, params, atts);
		atts = { id : "whatmakeslinkmediff" };
		swfobject.embedSWF($("#whatmakeslinkmediff").attr("url"), "whatmakeslinkmediff", "415", "295", "8", null, null, params, atts);
    });
	
    onYouTubePlayerReady = function(playerId) {
        var player = $("#" + playerId)[0];
        player.addEventListener("onStateChange", "onPlayerStateChange_" + playerId);
    }
	
	onPlayerStateChange_whatislinkme = function(newState) {
		if (newState == 1) _gaq.push(['_trackEvent', 'Video', 'Playing', "What is LinkMe? - on Candidate Homepage"]);
		if (newState == 0) _gaq.push(['_trackEvent', 'Video', 'Played to the end', "What is LinkMe? - on Candidate Homepage"]);
	}
	
	onPlayerStateChange_whyuselinkme = function(newState) {
		if (newState == 1) _gaq.push(['_trackEvent', 'Video', 'Playing', "Why use LinkMe? - on Candidate Homepage"]);
		if (newState == 0) _gaq.push(['_trackEvent', 'Video', 'Played to the end', "Why use LinkMe? - on Candidate Homepage"]);
	}
	
	onPlayerStateChange_whatmakeslinkmediff = function(newState) {
		if (newState == 1) _gaq.push(['_trackEvent', 'Video', 'Playing', "What makes LinkMe different? - on Candidate Homepage"]);
		if (newState == 0) _gaq.push(['_trackEvent', 'Video', 'Played to the end', "What makes LinkMe different? - on Candidate Homepage"]);
	}
})(jQuery);