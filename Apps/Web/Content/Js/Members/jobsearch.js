(function($) {
	$(document).ready(function() {
		//tabs
		$(".tabs .tab").click(function() {
			if ($(this).hasClass("active")) return false;
			var index = $(".tabs .tab").index($(this));
			$(".tabs .tab.active, .tabcontent .tab.active, .tabs .tab:eq(" + index + "), .tabcontent .tab:eq(" + index + ")").toggleClass("active");
			if ($(this).hasClass("Industry"))
				$(".tabcontent .Industry .row .item span").each(function(index, element) {
					if ($(element).height() == 11) $(element).css("margin-top", "10px");
				});
			return false;
		});
		$(".tabs .tab.Search").click();
		if ($.browser.msie && $.browser.version.indexOf("8") >= 0) {
			$("#Keywords, #Location").trigger("focus").trigger("blur");
		}
		//fields
		organiseFields();
		//Search for jobs
		//quick search
		$(".tabcontent .Search .quick form").submit(function() {
			$(this).find("#Keywords").val($(".tabcontent .Search .quick #Keywords").realValue());
			$(this).find("#Location").val($(".tabcontent .Search .quick #Location").realValue());
		});
		$(".tabcontent .Search .quick .explaination .moreorlessholder").click(function() {
			if ($(this).prev(":hidden").length > 0) {
				$(this).find(".ellipsis").text("");
				$(this).find(".moreorlesstext").text("Read less");
				$(this).find(".arrow").text("▲");
				$(this).removeClass("less").addClass("more").prev().toggle();
			} else {
				$(this).find(".ellipsis").text("...");
				$(this).find(".moreorlesstext").text("Read more");
				$(this).find(".arrow").text("▼");
				$(this).removeClass("more").addClass("less").prev().toggle();
			}
		});
		$(".tabcontent .Search .toggle").click(function() {
			$(this).closest(".tab.Search").children().toggleClass("active");
		});
		$(".tabcontent .Search .quick #Location").autocomplete(apiPartialMatchesUrl);
		$(".tabcontent .Search .quick .explaination .arrow").text("▼");
		//advanced search
		$(".tabcontent .Search .advanced #LocationAdvanced").autocomplete(apiPartialMatchesUrl);
		$("#Distance").parent().dropdown();
		$(".tabcontent .Search .advanced #CountryId").parent().dropdown().parent().find(".dropdown-item").click(function() {
			var url = $(this).text() == "" ? "" : apiPartialMatchesUrl.substring(0, apiPartialMatchesUrl.indexOf("countryId=") + 10) + $(".tabcontent .Search .advanced #CountryId option:contains('" + $(this).text() + "')").val() + "&location=";
			$(".tabcontent .Search .advanced #LocationAdvanced").setOptions({
				url : url
			});
		});
		$(".tabcontent .Search .advanced #LocationAdvanced").change(function() {
			$(".tabcontent .Search .advanced .radiusof").text($(this).realValue());
			if ($(".tabcontent .Search .advanced .radius").height() > 15)
				$(".tabcontent .Search .advanced .radius").css({
					"margin-top" : (34 - $(".tabcontent .Search .advanced .radius").height()) / 2 + "px"
				});
			else $(".tabcontent .Search .advanced .radius").css({
					"margin-top" : "9px"
				});

		});
		$(".tabcontent .Search .advanced .area.industry select").attr("size", "6").attr("maxcount", $(".tabcontent .Search .advanced .area.industry select option").length).parent().listbox();
		$(".tabcontent .Search .advanced form").submit(function() {
			$(this).find("#KeywordsAdvanced").val($(".tabcontent .Search .advanced #KeywordsAdvanced").realValue());
			$(this).find("#AdTitle").val($(".tabcontent .Search .advanced #AdTitle").realValue());
			$(this).find("#Advertiser").val($(".tabcontent .Search .advanced #Advertiser").realValue());
			$(this).find("#LocationAdvanced").val($(".tabcontent .Search .advanced #LocationAdvanced").realValue());
		});
		initializeSalary(parseInt($(".salary-slider").attr("minsalary")), parseInt($(".salary-slider").attr("maxsalary")), parseInt($(".salary-slider").attr("minsalary")), parseInt($(".salary-slider").attr("maxsalary")), parseInt($(".salary-slider").attr("stepsalary")));
		if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
			$(".tabcontent .Search .advanced .area.jobtype .field label[for='Temp']").css({ "width" : "36px" });
			$(".tabcontent .Search .advanced .area.jobtype .field label[for='JobShare']").css({ "width" : "57px" });
		}
		//Browse by industry
		$(".tabcontent .Industry #IndustriesIndustry option").each(function(index, element) {
			$(".tabcontent .Industry .row:eq(" + (index % 6) + ")").append("<div class='item'><div class='checkbox' value='" + $(element).attr("value") + "'></div><span>" + $(element).text() + "</span></div>");
		});
		$(".tabcontent .Industry .row .item .checkbox").click(function() {
			if ($(this).hasClass("disabled")) return;
			if ($(this).hasClass("checked")) {
				$(".tabcontent .Industry #IndustriesIndustry option[value='" + $(this).attr("value") + "']").removeAttr("selected");
				$(this).removeClass("checked");
				if ($(".tabcontent .Industry #IndustriesIndustry option:selected").length == 0)
					$(".tabcontent .Industry .nowenter").hide();
				else $(".tabcontent .Industry .nowenter").show();
			} else {
				$(".tabcontent .Industry #IndustriesIndustry option[value='" + $(this).attr("value") + "']").attr("selected", "selected");
				$(this).addClass("checked");
				if ($(".tabcontent .Industry #IndustriesIndustry option:selected").length == 0)
					$(".tabcontent .Industry .nowenter").hide();
				else $(".tabcontent .Industry .nowenter").show();
			}
		});
		$(".tabcontent .Industry .selectall").click(function() {
			$(".tabcontent .Industry .row .item .checkbox").each(function(index, element) {
				if (!$(element).hasClass("checked")) $(element).click();
			});
			$(".tabcontent .Industry .nowenter").show();
		});
		$(".tabcontent .Industry .clearall").click(function() {
			$(".tabcontent .Industry .row .item .checkbox").each(function(index, element) {
				if ($(element).hasClass("checked")) $(element).click();
			});
			$(".tabcontent .Industry .nowenter").hide();
		});
		$(".tabcontent .Industry #LocationIndustry").autocomplete(apiPartialMatchesUrl);
		$(".tabcontent .Industry form").submit(function() {
			$(this).find("#LocationIndustry").val($(".tabcontent .Industry #LocationIndustry").realValue());
		});
		//Browse by location
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
				$(".tabcontent .Location .Industry_field label span").text("");
				$(".tabcontent .Location #browseByLocation").hide();
			} else {
				$(".aus-map .state, .aus-map .region").removeClass("active");
				$(".aus-map .state[state='" + $(this).attr("areafor") + "'], .aus-map .region[region='" + $(this).attr("areafor") + "'], .aus-map .region[state='" + $(this).attr("areafor") + "']").addClass("active");
				$(".aus-map .pin").attr("pinfor", $(this).attr("areafor"));
				$("#AnyWhereInAus .checkbox").removeClass("checked");
				$(".tabcontent .Location .Industry_field label span").text($(this).attr("areafor"));
				$(".tabcontent .Location #browseByLocation").show();
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
				$(".tabcontent .Location .Industry_field label span").text("");
				$(".aus-map .state, .aus-map .region").removeClass("active");
				$(".aus-map .region").removeClass("state");
				$(".aus-map .pin").attr("pinfor", "");
				$(this).removeClass("checked");
				$(".tabcontent .Location #browseByLocation").hide();
			} else {
				$(".tabcontent .Location .Industry_field label span").text("Australia");
				$(".aus-map .state, .aus-map .region").addClass("active");
				$(".aus-map .region").addClass("state");
				$(".aus-map .pin").attr("pinfor", "Australia");
				$(this).addClass("checked");
				$(".tabcontent .Location #browseByLocation").show();
			}
			event.stopPropagation();
		});
		$(".tabcontent .Location .Industry_field label").append("<span></span>");
		$(".tabcontent .Location .Industry_field select").each(function() {
			var textBg = $("<div class='bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='leftbar'></div>").after("<div class='rightbar'></div>");
		});
		$(".tabcontent .Location .Industry_field select").prepend("<option value='all' selected='selected'>All</option>").attr("size", "6").addClass("dropdown").closest(".control").addClass("dropdown_control").closest(".field").addClass("dropdown_field");
		$(".tabcontent .Location #IndustriesLocation").parent().dropdown();
		$(".tabcontent .Location form").submit(function() {
			var location = $(".tabcontent .Location .Industry_field label span").text();
			//if (location == "Australia") location = "";
			$(this).find("#LocationLocation").val(location);
			if ($(this).find("#IndustriesLocation option:selected").attr("value") == "all") $(this).find("#IndustriesLocation option").attr("selected", "selected");
		});
		//suggested jobs
		$(".suggestedjobs .titlebar").click(function() {
			$(this).parent().toggleClass("collapsed expanded");
			if ($(this).parent().hasClass("collapsed")) $(this).find(".title").html("Suggested jobs&nbsp;&nbsp;&nbsp;&nbsp;▼");
			else $(this).find(".title").html("Suggested jobs&nbsp;&nbsp;&nbsp;&nbsp;▲");
		});
		$(".suggestedjobs .titlebar .title").html("Suggested jobs&nbsp;&nbsp;&nbsp;&nbsp;▼");
		$(".suggestedjobs .content .row .button").click(function() {
			$(this).toggleClass("expand collapse").next().toggleClass("expanded collapsed");
		});
		$(".suggestedjobs .content .details .description").ellipsis( { lines: 4 } );
	});

	toFormattedDigits = function(nValue) {
		var s = nValue.toString();
		for (j = 3; j < 15; j += 4) {  // insert commas
			if ((s.length > j) && (s.length >= 4)) {
				s = s.substr(0, s.length - j) + ',' + s.substr(s.length - j);
			}
		}
		return (s);
	}

	initializeSalary = function(lowerBound, upperBound, minSalary, maxSalary, stepSalary) {

		setSalarySliderLabel = function(lowerBound, upperBound) {
			var label = "";

			if (lowerBound == minSalary && upperBound == maxSalary) label = "Any salary";
			else if (lowerBound == minSalary) label = "up to $" + toFormattedDigits(upperBound);
			else if (upperBound == maxSalary) label = "$" + toFormattedDigits(lowerBound) + "+";
			else label = "$" + toFormattedDigits(lowerBound) + " - " + "$" + toFormattedDigits(upperBound);

			$(".salary-desc center").html(label);
			//set background position
			var percentage = lowerBound / maxSalary;
			var right = upperBound / maxSalary;
			var bgPosX = 0;
			$(".salary-slider .ui-slider-range").css({
				"background-position" : (bgPosX - 276 * percentage) + "px -14px",
				"left" : (percentage * 100) + "%",
				"width" : ((right - percentage) * 100) + "%"
			});
			if (lowerBound == minSalary) $("#SalaryLowerBound").val("");
			else $("#SalaryLowerBound").val(lowerBound);
			if (upperBound == maxSalary) $("#SalaryUpperBound").val("");
			else $("#SalaryUpperBound").val(upperBound);
		}

		$(".salary-slider").slider({
			range: true,
			min: minSalary,
			max: maxSalary,
			step: stepSalary,
			values: [minSalary, maxSalary],
			slide: function(event, ui) {
				setSalarySliderLabel(ui.values[0], ui.values[1]);
			}
		});

		$(".salary-range .left-range").html('$' + toFormattedDigits(minSalary));
		$(".salary-range .right-range").html('$' + toFormattedDigits(maxSalary) + '+');
		setSalarySliderLabel(lowerBound, upperBound);
	}
})(jQuery);