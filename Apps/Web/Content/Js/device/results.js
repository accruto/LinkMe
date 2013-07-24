(function($) {
	var scrollArea;
	$(document).ready(function() {
		$("#mainbody").addClass("fullwidth");
        updateCriteria($(".results .hits .total").text(), $(".results .criteriahtml").text());
		//empty results
		if ($(".results .hits .total").text() == "0") {
			$("#mainbody .results .emptylist").show();
			$("#mainbody .buttons .showmorejobs").hide();
		} else {
			$("#mainbody .results .emptylist").hide();
			$("#mainbody .buttons .showmorejobs").show();
		}
		$("#mainbody .results .emptylist ul li.changefilters").click(function() {
			$("#filterbar .button.filter").click();
		});
		$("#mainbody .results .emptylist ul li.resetfilters").click(function() {
			$("#filter header .button.reset").click();
		});
		initJobAdItemEvents();
		$(".showmorejobs").click(function() {
			if ($(this).hasClass("active")) return;
			$(this).addClass("active");
			var currentPage = parseInt($(this).attr("data-currentpage"));
            criteria["Page"] = currentPage + 1;
			criteria["AppendMode"] = true;
			partialCall();
		});
		initIScroll();
		$(".button.sort").click(function() {
			var button = $(this);
			var data = $.parseJSON(button.attr("data-sortorder"));
			SpinningWheel.addSlot(data, "", button.attr("data-current"));
			SpinningWheel.setDoneAction(function() {
				var sort = SpinningWheel.getSelectedValues().keys[0];
				criteria["SortOrder"] = sort;
				criteria["SortOrderDirection"] = "SortOrderIsDescending";
				criteria["Page"] = 1;
				criteria["Items"] = 10;
				partialCall();
				button.attr("data-current", sort);
			});
			SpinningWheel.open();
		});
		//filter
		$("#filter").insertAfter($("#footer")).hide();
		$("#filterbar .button.filter").click(function() {
			$("#fixedbottombar, #header, #mainbody, #footer, #filter #AllIndustriesList").hide();
			$("#filter, #filter .tabs, #filter .tabcontent").show();
			$("#filter .tabs .tab.Distance").click();
			$("#scrollarea").css({ "bottom" : "0px" });
			scrollArea.refresh();
		});
		$("#filter .button.reset").click(function() {
			criteria = {};
			for (var p in resetCriteria)
				if (resetCriteria.hasOwnProperty(p))
				criteria[p] = resetCriteria[p];
			criteria["Page"] = 1;
			criteria["Items"] = 10;
			partialCall();
			$("#header, #mainbody, #footer, #fixedbottombar").show();
			$("#filter").hide();			
			$("#scrollarea").css({ "bottom" : $("#fixedbottombar").height() });
			scrollArea.refresh();
			resetFilters();
		});
		$("#filter .button.back").click(function() {
			if ($(this).hasClass("industries")) {
				$("#filter .tabs, #filter .tabcontent").show();
				$("#filter #AllIndustriesList").hide();
				$(this).removeClass("industries");
				return;
			} else if ($(this).hasClass("apply")) {
				//location
				if ($("#Location").realValue() == "") {
					delete criteria["Location"];
					delete criteria["Distance"];
				} else {
					criteria["Location"] = $("#Location").realValue();
					criteria["Distance"] = $("#Distance").val();
				}
				//salary
				criteria["SalaryRate"] = $("#SalaryRateYear").is(":checked") ? "Year" : "Hour";
				criteria["SalaryLowerBound"] = $("#SalaryLowerBound").val();
				criteria["SalaryUpperBound"] = $("#SalaryUpperBound").val();
				criteria["IncludeNoSalary"] = $("#IncludeNoSalary").is(":checked") ? "True" : "False";
				//jobtype
				var filterJobTypes = new Array();
				$("#filter .tabcontent .content.JobType .wrapper").each(function() {
					if ($(this).find(".jobtype").hasClass("checked"))
						filterJobTypes.push($(this).attr("class").replace("wrapper ", ""));
				});
				criteria["JobTypes"] = filterJobTypes.join();
				//recency
				if ($("#Recency").val() == $(".recencyrange").attr("data-defaultrecency")) delete criteria["Recency"];
				else criteria["Recency"] = $("#Recency").val();
				//industry
				var filterIndustries = new Array();
				if ($("#filter #AllIndustriesList .allindustries .checkbox").hasClass("checked")) {
					$("#filter #AllIndustriesList .industry").each(function() {
						filterIndustries.push($(this).attr("id"));
					});
				} else {
					$("#filter #AllIndustriesList .industry").each(function() {
						if ($(this).find(".checkbox").hasClass("checked"))
							filterIndustries.push($(this).attr("id"));
					});
				}
				if (filterIndustries.length == 0) delete criteria["IndustryIds"];
				else criteria["IndustryIds"] = filterIndustries;
				criteria["Page"] = 1;
				criteria["Items"] = 10;
				partialCall();
				$(this).removeClass("apply");
			}
			$("#header, #mainbody, #footer, #fixedbottombar").show();
			$("#filter").hide();
			$("#scrollarea").css({ "bottom" : $("#fixedbottombar").height() });
			scrollArea.refresh();
		});
		$("#filter .tabs .tab").click(function() {
			if ($(this).hasClass("active")) return;
			$(this).siblings(".active").removeClass("active").end().addClass("active");
			var index = $(this).parent().children().index(this);
			$("#filter .tabcontent").find(".content.active").removeClass("active").end().find(".content:eq(" + index + ")").addClass("active");
		});
		organiseFields($("#filter"));
		//Distance filter
		$("#Location").change(function() {
			$("#filter .tabcontent .Distance .field.desc .location").text($(this).realValue());
			if ($(this).realValue() != "") {
				$("#filter .tabcontent .Distance .field.desc, #filter .tabcontent .Distance .field.distanceslider_field, #filter .tabcontent .Distance .field.distancerange_field").show();
			} else {
				$("#filter .tabcontent .Distance .field.desc, #filter .tabcontent .Distance .field.distanceslider_field, #filter .tabcontent .Distance .field.distancerange_field").hide();
			}
		});
		$("#Location").change();
		var dr = $(".distancerange");
		initializeDistance(parseInt($("#Distance").val()), parseInt(dr.attr("data-mindistance")), parseInt(dr.attr("data-maxdistance")), parseInt(dr.attr("data-stepdistance")), eval(dr.attr("data-distances")));
		//Salary filter
		var range = $.parseJSON($(".salaryrange").attr("data-range"));
		var rate = $("#SalaryRateYear").is(":checked") ? "SalaryRateYear" : "SalaryRateHour";
		var lowerBound = $("#SalaryLowerBound").val();
		var upperBound = $("#SalaryUpperBound").val();
		var minSalary = parseInt(range[rate].MinSalary);
		var maxSalary = parseInt(range[rate].MaxSalary);
		var stepSalary = parseInt(range[rate].StepSalary);
		initializeSalary(lowerBound, upperBound, minSalary, maxSalary, stepSalary);
		//JobType filter
		$("#filter .tabcontent .content.JobType .jobtype").click(function() {
            if ($(this).parent().parent().find(".jobtype.checked").length == 1 && $(this).hasClass("checked")) return;
            $(this).toggleClass("checked");
		});
		//Recency filter
		var rr = $(".recencyrange");
		initializeRecency(parseInt($("#Recency").val()), parseInt(rr.attr("data-minrecency")), parseInt(rr.attr("data-maxrecency")), parseInt(rr.attr("data-steprecency")), eval(rr.attr("data-recencies")));
		//Industry filter
		$("#filter .tabcontent .content.Industry .button.choose").click(function() {
			$("#filter .tabs, #filter .tabcontent").hide();
			$("#filter #AllIndustriesList").show();
			$("#filter .button.back").addClass("industries");
		});
		$("#filter #AllIndustriesList .allindustries").click(function() {
			var checkbox = $(this).find(".checkbox");
			if (checkbox.hasClass("checked")) {
				checkbox.removeClass("checked");
			} else {
				checkbox.addClass("checked");
				$("#filter #AllIndustriesList .industry .checkbox").removeClass("checked");
			}
		});
		$("#filter #AllIndustriesList .industry").click(function() {
			$(this).find(".checkbox").toggleClass("checked");
			$("#filter #AllIndustriesList .allindustries .checkbox").removeClass("checked");
		});
		//toggle apply filter button
		$("#Location").change(function() {
			toggleApplyFilterButton();
		});
		$(".distanceslider").slider({
			stop : function() {
				toggleApplyFilterButton();
			}
		});
		$("#filter .tabcontent .content.Salary .salaryrate_field .radiobutton").click(function() {
			toggleApplyFilterButton();
			$(".salaryslider").slider({
				stop : function(event, ui) {
					$("#SalaryLowerBound").val(ui.values[0]);
					$("#SalaryUpperBound").val(ui.values[1]);
					toggleApplyFilterButton();
				}
			});
		});
		$(".salaryslider").slider({
			stop : function(event, ui) {
                $("#SalaryLowerBound").val(ui.values[0]);
                $("#SalaryUpperBound").val(ui.values[1]);
				toggleApplyFilterButton();
			}
		});
		$("#filter .tabcontent .content.Salary .checkbox_field .checkbox").click(function() {
			toggleApplyFilterButton();
		});
		$("#filter .tabcontent .content.JobType .jobtype").click(function() {
			toggleApplyFilterButton();
		});
		$(".recencyslider").slider({
			stop : function() {
				toggleApplyFilterButton();
			}
		});
		$("#filter #AllIndustriesList .allindustries").click(function() {
			toggleApplyFilterButton();
		});
		$("#filter #AllIndustriesList .industry").click(function() {
			toggleApplyFilterButton();
		});
		if (showSavedNotification) {
			$(".notification.saved").show().delay(10000).queue(function() {
				$(this).hide();
			});		
		}
		if (showAddedNotification) {
			$(".notification.added").show().delay(10000).queue(function() {
				$(this).hide();
			});		
		}
	});
	
	resetFilters = function() {
		//location
		$("#Location").val(resetCriteria["Location"]).removeAttr("resolvedlocation");
		$("#filter .tabcontent .content.Distance .mylocation").removeClass("checked");
		$("#filter .tabcontent .content.Distance .distanceslider").slider("value", resetCriteria["Distance"] ? resetCriteria["Distance"] : $(".distancerange").attr("data-defaultdistance"));
		//salary
		$("#filter .tabcontent .content.Salary .salaryrate_field .radiobutton[value='SalaryRate" + (resetCriteria["SalaryRate"] ? resetCriteria["SalaryRate"] : "Year") + "']").click();
		$(".salaryslider").slider("values", 0, resetCriteria["SalaryLowerBound"] ? parseInt(resetCriteria["SalaryLowerBound"]) : $(".salaryslider").slider("option", "min"));
		$(".salaryslider").slider("values", 1, resetCriteria["SalaryUpperBound"] ? parseInt(resetCriteria["SalaryUpperBound"]) : $(".salaryslider").slider("option", "max"));
		$("#filter .tabcontent .content.Salary .checkbox.IncludeNoSalary").removeClass("checked");
		$("#IncludeNoSalary").removeAttr("checked");
		//jobtype
		$("#filter .tabcontent .content.JobType .wrapper .jobtype").addClass("checked");
		//recency
		$(".recencyslider").slider("value", parseInt($(".recencyrange").attr("data-defaultrecency")));
		//industry
		$("#filter #AllIndustriesList .allindustries .checkbox, #filter #AllIndustriesList .industry .checkbox").removeClass("checked");
	}
	
	toggleApplyFilterButton = function() {
		var filterApplied = false;
		//location
		if (!filterApplied) {
			if ($("#Location").realValue() != criteria["Location"]) filterApplied = true;
			if ($("#Location").realValue() != "" && $("#Distance").val() != (criteria["Distance"] ? criteria["Distance"] : ($(".distancerange").attr("data-defaultdistance")))) filterApplied = true;
		}
		//salary
		if (!filterApplied) {
			if (($("#SalaryRateYear").is(":checked") ? "Year" : "Hour") != (criteria["SalaryRate"] ? criteria["SalaryRate"] : "Year")) filterApplied = true;
			if ($("#SalaryLowerBound").val() != (criteria["SalaryLowerBound"] ? criteria["SalaryLowerBound"] : "")) filterApplied = true;
			if ($("#SalaryUpperBound").val() != (criteria["SalaryUpperBound"] ? criteria["SalaryUpperBound"] : "")) filterApplied = true;
			if (($("#IncludeNoSalary").is(":checked") ? "True" : "False") != (criteria["IncludeNoSalary"] ? criteria["IncludeNoSalary"] : "True")) filterApplied = true;
		}
		//jobtype
		if (!filterApplied) {
            var filterJobTypes = new Array();
            $("#filter .tabcontent .content.JobType .wrapper").each(function() {
                if ($(this).find(".jobtype").hasClass("checked"))
                    filterJobTypes.push($(this).attr("class").replace("wrapper ", ""));
            });
			var s = filterJobTypes.length == 5 ? "" : filterJobTypes.join().replace(",", ", ");
			if (s != (criteria["JobTypes"] ? criteria["JobTypes"] : "")) filterApplied = true;
		}
		//recency
		if (!filterApplied)
			if ($("#Recency").val() != (criteria["Recency"] ? criteria["Recency"] : $(".recencyrange").attr("data-defaultrecency"))) filterApplied = true;
		//industry
		if (!filterApplied) {
			var filterIndustries = new Array();
			if ($("#filter #AllIndustriesList .allindustries .checkbox").hasClass("checked")) {
				$("#filter #AllIndustriesList .industry").each(function() {
					filterIndustries.push($(this).attr("id"));
				});
			} else {
				$("#filter #AllIndustriesList .industry").each(function() {
					if ($(this).find(".checkbox").hasClass("checked"))
						filterIndustries.push($(this).attr("id"));
				});
			}
			var s = filterIndustries.join().replace(",", ", ");
			var c = $.isArray(criteria["IndustryIds"]) ? criteria["IndustryIds"].join() : "";
			if (s != c) filterApplied = true;
		}
		if (filterApplied) $("#filter .button.back").addClass("apply");
		else $("#filter .button.back").removeClass("apply");
	}
	
	initIScroll = function() {
		var fb = $("#filterbar");
		var filterBarHeight = fb.height();
		var buttonsBar = $("#mainbody .buttons");
		//add #scrollarea and #scroller, set scrollarea.bottom = #filterbar.height
		var sa = $("<div id='scrollarea'><div id='scroller'></div></div>");		
		sa.css({ "bottom" : filterBarHeight }).prependTo($("body"));
		//move #header, #mainbody, #footer into #scrollarea
		sa.find("#scroller").append($("#header, #mainbody, #footer"));
		//move #filterbar into #fixedbottombar under #scrollarea
		var fbb = $("<div id='fixedbottombar'></div>");
		fbb.insertAfter(sa).append(fb);
		//new iScroll
		var footerHeight = $("#footer").height();
		scrollArea = new iScroll('scrollarea', {
			onBeforeScrollStart : function(e) {
				if (e.target.tagName == "A" || e.target.tagName == "INPUT") return;
				e.preventDefault();
			},
			onAnimating : function() {
				if (scrollArea.options.filterShown) return;
				if (scrollArea.options.filterBarFixed) {
					if (scrollArea.y < scrollArea.wrapperH - scrollArea.scrollerH + footerHeight) {
						scrollArea.options.filterBarFixed = false;
						fb.insertAfter(buttonsBar);
						sa.css({ "bottom" : "0px" });
						scrollArea.refresh();
					}
				} else {
					if (scrollArea.y > scrollArea.wrapperH - scrollArea.scrollerH + footerHeight) {// + filterBarHeight) {
						scrollArea.options.filterBarFixed = true;
						fb.appendTo(fbb);
						sa.css({ "bottom" : filterBarHeight });
						scrollArea.refresh();
					}
				}
			},
			onScrollMove: function() {
				if (scrollArea.options.filterShown) return;
				if (scrollArea.options.filterBarFixed) {
					if (scrollArea.y < scrollArea.wrapperH - scrollArea.scrollerH + footerHeight) {
						scrollArea.options.filterBarFixed = false;
						fb.insertAfter(buttonsBar);
						sa.css({ "bottom" : "0px" });
						scrollArea.refresh();
					}
				} else {
					if (scrollArea.y > scrollArea.wrapperH - scrollArea.scrollerH + footerHeight) {// + filterBarHeight) {
						scrollArea.options.filterBarFixed = true;
						fb.appendTo(fbb);
						sa.css({ "bottom" : filterBarHeight });
						scrollArea.refresh();
					}
				}
			},
			momentum : true,
			filterBarFixed : true,
			filterShown : false//,
			//zoom : true
		});
		// console.log("scrollArea.options.filterBarFixed:" + scrollArea.options.filterBarFixed);
		// console.log("scrollArea.y:" + scrollArea.y);
		// console.log("scrollArea.wrapperH:" + scrollArea.wrapperH);
		// console.log("scrollArea.scrollerH:" + scrollArea.scrollerH);
		// console.log("footerHeight:" + footerHeight);
	}
	
    //request a new search with current filter, get results, refresh page content
    partialCall = function() {
        var requestData = {};
        for (var p in criteria)
            if (criteria.hasOwnProperty(p) && p != "AppendMode")
            requestData[p] = criteria[p];
        if (requestData["JobTypes"]) {
            var value = 0;
            var requestTypes = requestData["JobTypes"].replace(/ /gi, "").split(",");
            $.each(requestTypes, function(index, element) {
                value += jobTypes[element];
            });
            requestData["JobTypes"] = value;
        }
        //always send Australia to backend
        if (!requestData["CountryId"] || requestData["CountryId"] == "")
            requestData["CountryId"] = $(".leftside .area.search #CountryId").val();
		//logTime("Make API call");
		//request
		currentRequest = $.get($(".results").attr("data-apiurl").unMungeUrl(),
			requestData,
			function(data, textStatus, xmlHttpRequest) {
				if (data == "") {
					//showAPIErrorOverlay();
				} else if (data.Success) {
					updateAPIResults(data);
				} else {
					//showAPIErrorOverlay();
				}
				currentRequest = null;
			}
		);
    }

    updateAPIResults = function(data) {
        updateJsonData(data);
        //logTime("Finish updating json data");
        //update criteria & display
        updateCriteria(data.TotalJobAds, data.CriteriaHtml);
        //logTime("Finish update criteria text");
        //update counts
        updateIndustryHits(data.IndustryHits);
        updateJobTypeHits(data.JobTypeHits);
        //logTime("Finish update job ad item data");
		$(".showmorejobs").removeClass("active").attr("data-currentpage", criteria["Page"]);
		initJobAdItemEvents();
		//empty results
		if (data.TotalJobAds == 0) {
			$("#mainbody .results .emptylist").show().addClass("api");
			$("#mainbody .buttons .showmorejobs").hide();
		} else {
			$("#mainbody .results .emptylist").hide();
			$("#mainbody .buttons .showmorejobs").show();
		}
		//iScroll
		if (scrollArea) scrollArea.refresh();
    }

    updateCriteria = function(totalCount, criteria) {
        $(".results .criteriahtml").html("<b>" + totalCount + "</b> <span class='fulltext'>job" + (totalCount > 1 ? "s" : "") + " match" + (totalCount > 1 ? "" : "es") + " criteria: " + criteria + "</span>");
		shortenCriteria();
    }

    updateJsonData = function(data) {
        //current rows count
        var rows = $(".results .row");
        $.each(data.JobAds, function(index, jobAd) {
            var currentRow, currentIndex;
			currentIndex = index;
			if (criteria["AppendMode"]) currentIndex = index + (criteria["Page"] - 1) * 10;
            if (currentIndex > rows.length - 1) {
                currentRow = $(rows[0]).clone(true).appendTo($(".results"));
            } else {
                currentRow = $(rows[currentIndex]);
            }
            currentRow.removeClass("empty");
            //Featured
            if (jobAd.Featured) currentRow.addClass("featured");
            else currentRow.removeClass("featured");
            //ID
            currentRow.attr("id", jobAd.JobAdId);
            //IsNew
            if (jobAd.IsNew)
                currentRow.find(".icon.new").addClass("active");
            else currentRow.find(".icon.new").removeClass("active");
            //JobTypes
            currentRow.find("icon.jobtype").attr("jobtypes", jobAd.JobTypes);
            //Title
            var title = currentRow.find(".title");
            title.text(jobAd.Title);
            //Company
            var company = currentRow.find(".company");
            var contactDetails = typeof jobAd.ContactDetails == "undefined" ? "" : jobAd.ContactDetails
            company.text(contactDetails);
            //Location
            currentRow.find(".location").text(jobAd.Location);
            //Salary
            currentRow.find(".salary").text(jobAd.Salary);
            //Date
            currentRow.find(".date").text("Posted " + jobAd.CreatedTime);
			updateJobAdItemData(currentRow);
        });
        var currentLength = data.JobAds.length;
		if (criteria["AppendMode"]) currentLength = currentLength + (criteria["Page"] - 1) * 10;
        for (var i = currentLength; i < rows.length; i++)
            $(rows[i]).addClass("empty");
    }

    updateIndustryHits = function(hits) {
		$("#filter #AllIndustriesList .industry .count").text("(0)");
		for (var i in hits)
			if (hits.hasOwnProperty(i))
			$("#filter #AllIndustriesList #" + i + " .count").text("(" + hits[i] + ")");
    }

    updateJobTypeHits = function(hits) {
        // for (var t in hits)
            // if (hits.hasOwnProperty(t))
            // $(".leftside .area.filter .section.jobtypes ." + t + " .count").text("(" + hits[t] + ")");
    }

	shortenCriteria = function() {
		var criteriahtml = $(".results .criteriahtml");
		var hidePart = $("<span class='hidepart'></span>");
		var moreOrLessHolder = $("<span class='moreorlessholder'><span class='ellipsis'>... </span><span class='moreorlesstext'>show more</span></span>");
		var lineHeight = parseFloat(criteriahtml.css("line-height"));
		if (criteriahtml.height() > lineHeight * 2) {
			hidePart.hide().appendTo(criteriahtml);
			moreOrLessHolder.appendTo(criteriahtml);
			var fulltext  = criteriahtml.find(".fulltext");
			while (criteriahtml.height() > lineHeight * 2) {
				var text = fulltext.text();
				var ch = text.charAt(text.length - 1);
				hidePart.text(ch + hidePart.text());
				fulltext.text(text.substring(0, text.length - 1));
			}
			hidePart.hide();
			criteriahtml.find(".moreorlessholder").click(function() {
				if ($(this).prev(":hidden").length > 0) {
					$(this).find(".ellipsis").text(" ");
					$(this).find(".moreorlesstext").text("show less");
					$(this).prev().toggle();
				} else {
					$(this).find(".ellipsis").text("... ");
					$(this).find(".moreorlesstext").text("show more");
					$(this).prev().toggle();
				}
			});
		}
	}

    initializeDistance = function(defaultDistance, minDistance, maxDistance, stepDistance, distances) {
        var defaultIndex;
        for (var i = 0; i < distances.length; i++)
            if (distances[i] == defaultDistance) {
            defaultIndex = i;
            break;
        }
        $(".distanceslider").slider({
            range: "min",
            min: minDistance,
            max: maxDistance,
            step: stepDistance,
            value: defaultIndex,
            slide: function(event, ui) {
                $(".field.desc .red").text(distances[ui.value] + (ui.value == (distances.length - 1) ? "+" : "") + " km");
                $("#Distance").val(distances[ui.value]);
            },
            stop: function(event, ui) {
            },
            change: function(event, ui) {
                $(".field.desc .red").text(distances[ui.value] + (ui.value == (distances.length - 1) ? "+" : "") + " km");
                $("#Distance").val(distances[ui.value]);
            }
        }).attr("defaultindex", defaultIndex);

        $(".field.desc .red").text(distances[defaultIndex] + " km");
        $("#Distance").val(defaultDistance);
    }
	
    initializeRecency = function(defaultRecency, minRecency, maxRecency, stepRecency, recencies) {
        var defaultIndex;
        for (var i = 0; i < recencies.length; i++)
            if (recencies[i].days == defaultRecency) {
            defaultIndex = i;
            break;
        }
        $(".recencyslider").slider({
            range: "min",
            min: minRecency,
            max: maxRecency,
            step: stepRecency,
            value: defaultIndex,
            slide: function(event, ui) {
                $(".recencydesc").text(recencies[ui.value].label);
                $("#Recency").val(recencies[ui.value].days);
            },
            stop: function(event, ui) {
            },
            change: function(event, ui) {
                $(".recencydesc").text(recencies[ui.value].label);
                $("#Recency").val(recencies[ui.value].days);
            }
        }).attr("defaultindex", defaultIndex);

        $(".recencydesc").text(recencies[defaultIndex].label);
        $("#Recency").val(defaultRecency);
    }
})(jQuery);