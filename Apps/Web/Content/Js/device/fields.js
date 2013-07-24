(function($) {
	var currentRequest;
	
	organiseFields = function(context) {
		if (!context) context = $("body");
		//text box
		$(".control .textbox:not(.multiline_textbox)", context).each(function() {
			//hide label
			var control = $(this).parent();
			var label = control.parent().find("> label");
			var watermark = $("<div class='watermark'><span class='title'>" + label.text() + "</span><span class='text'>" + ($(this).attr("data-watermark") == undefined ? "" : $(this).attr("data-watermark")) + "</span></div>");
			label.hide();
			var tipIcon = $("<div class='icon tip'></div>");
			$(this).before(watermark).after(tipIcon);
			if ($(this).val() == "") $(this).addClass("mask");
			else $(this).removeClass("mask");
		}).focus(function() {
			if ($(this).attr("readonly") == "readonly") return;
			$(this).removeClass("mask");
		}).blur(function() {
			if ($(this).val() == "") $(this).addClass("mask");
			else $(this).removeClass("mask");
		});
		$(".location_textbox_field input[type='text']", context).keyup(function() {
			if (currentRequest) currentRequest.abort();
			var location = $(this);
			var value = location.val();
			var regEx = new RegExp(value, "gi");
			currentRequest = $.ajax({
				type: "POST",
				url: apiPartialMatchesUrl + value,
				async: false,
				dataType: "json",
				success: function(data, textStatus, xmlHttpRequest) {
					if ($("#LocationHelper").length == 0) {
						$("body").append("<div id='LocationHelper'><div class='pointer'><div class='upper'></div><div class='lower'></div></div><div class='items'></div></div>");
						var control = location.closest(".control");
						var pos = control.position();
						var height = control.height();
						var width = control.width();
						var marginLeft = parseFloat(control.css("margin-left"));
						var pointer = $("#LocationHelper .pointer");
						var pointerTop = parseFloat(pointer.css("top"));
						var pointerWidth = pointer.width();
						pointer.css({
							"left" : (width - pointerWidth) / 2
						});
						$("#LocationHelper").css({
							"position" : "absolute",
							"left" : pos.left + marginLeft,
							"top" : pos.top + height - pointerTop,
							"width" : width,
							"z-index" : 100
						}).bind("clickoutside", function(event) {
							if ($(event.target).closest(".location_textbox_field").length > 0) return;
							$(this).hide();
							event.stopPropagation();
						});
					}
					$("#LocationHelper").show().find(".items").html("");
					$.each(data, function(index, element) {
						var start = element.search(regEx);
						var item = element.substr(0, start - 1) + "<b>" + element.substr(start, value.length) + "</b>" + element.substr(start + value.length);
						$("#LocationHelper .items").append("<div class='item'>" + item + "</div>");
					});
					$("#LocationHelper .item").click(function() {
						location.val($(this).text()).change();
						$(this).addClass("active").delay(100).queue(function() {
							$("#LocationHelper").hide();
						});
					});
					currentRequest = null;
				},
				error: function(error) {
				}
			});
		});
		$("#Location").each(function() {
			$.fn.extend({
				realValue : function() {
					return $(this).val() == "My current location" ? $(this).attr("resolvedlocation") : $(this).val();
				}
			});
		});
		$(".checkbox_control", context).each(function() {
			var checkbox = $("<div class='checkbox'></div>");
			var id = $("input[type='checkbox']", this).hide().attr("id");
			checkbox.addClass(id).prependTo(this);
			if ($("input[type='checkbox']", this).is(":checked")) checkbox.addClass("checked");
			checkbox.click(function(event) {
				if ($(this).hasClass("disabled")) return;
				if ($(this).hasClass("checked")) {
					$("input[type='checkbox']", $(this).parent()).removeAttr("checked");
					$(this).removeClass("checked");
				} else {
					$("input[type='checkbox']", $(this).parent()).attr("checked", "checked");
					$(this).addClass("checked");
				}
				event.stopPropagation();
			});
		});
		$(".mylocation", context).click(function() {
			var button = $(this);
			if (button.hasClass("denied")) return;
			if (button.hasClass("checked")) {
				$("#Location").val("").addClass("mask");
				button.removeClass("checked");
				$("#Location").change();
				return;
			}
			if (navigator.geolocation)
				navigator.geolocation.getCurrentPosition(function(position) {
					button.attr("longitude", position.coords.longitude).attr("latitude", position.coords.latitude).addClass("checked");
					var resolvedLocation = "";
					currentRequest = $.ajax({
						type: "POST",
						url: apiLocationClosestUrl + "longitude=" + position.coords.longitude + "&latitude=" + position.coords.latitude,
						async: false,
						dataType: "json",
						success: function(data, textStatus, xmlHttpRequest) {
							resolvedLocation = data.ResolvedLocation;
							currentRequest = null;
						},
						error: function(error) {
						}
					});
					//need call resolve here
					$("#Location").attr("resolvedlocation", resolvedLocation);
					$("#Location").val("My current location").removeClass("mask");
					$("#Location").change();
				}, function(error) {
					myCurrentLocationDenied(button);
				});
			else myCurrentLocationDenied(button);
		});
		$(".radiobuttons_control", context).each(function() {
			var radioButtonGroup = $(this);
			$(".radio_control", this).each(function() {
				var radiobutton = $("<div class='radiobutton'></div>");
				var id = $("input[type='radio']", this).hide().attr("id");
				radiobutton.attr("value", id).prependTo(this);
				if ($("input[type='radio']", this).is(":checked"))
					radiobutton.addClass("checked");
				if ($("input[type='radio']", this).attr("disabled") == "disabled")
					radiobutton.addClass("disabled");
				radiobutton.click(function(event) {
					if ($(this).hasClass("checked")) return;
					if ($(this).hasClass("disabled")) return;
					$(".radiobutton", radioButtonGroup).removeClass("checked");
					$(this).addClass("checked");
					$("input.radio", radioButtonGroup).removeAttr("checked");
					$("#" + id, radioButtonGroup).attr("checked", "checked");
					if ($(this).closest(".field").hasClass("salaryrate_field")) {
						var range = $.parseJSON($(".salaryrange").attr("data-range"));
						var rate = $(this).attr("value");
						var minSalary = parseInt(range[rate].MinSalary);
						var maxSalary = parseInt(range[rate].MaxSalary);
						var stepSalary = parseInt(range[rate].StepSalary);
						initializeSalary(minSalary, maxSalary, minSalary, maxSalary, stepSalary);
						$(this).closest(".content.Salary").find(".title .salaryrate").text(rate == "SalaryRateYear" ? "(per annum)" : "(per hour)");
					}
					event.stopPropagation();
				});
			});
		});
 	}
	
	myCurrentLocationDenied = function(button) {
		if ($("#Location").val() == "My current location") {
			$("#Location").val("").addClass("mask");
			$("#Location").change();
		}
		button.addClass("denied");
	}
	
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
			if ($(".content.Salary").length == 0)
				if ($(".radiobutton[value='SalaryRateHour']").hasClass("checked") && label != "Any salary") label += " per hour";

            $(".salarydesc").html(label);
        }

        $(".salaryslider").slider({
            range: true,
            min: minSalary,
            max: maxSalary,
            step: stepSalary,
            values: [lowerBound, upperBound],
            slide: function(event, ui) {
                setSalarySliderLabel(ui.values[0], ui.values[1]);
            },
            stop: function(event, ui) {
                $("#SalaryLowerBound").val(ui.values[0]);
                $("#SalaryUpperBound").val(ui.values[1]);
            },
            change: function(event, ui) {
                setSalarySliderLabel(ui.values[0], ui.values[1]);
            }
        });

        setSalarySliderLabel(lowerBound, upperBound);
        if (lowerBound == minSalary) $("#SalaryLowerBound").val("");
        else $("#SalaryLowerBound").val(lowerBound);
        if (upperBound == maxSalary) $("#SalaryUpperBound").val("");
        else $("#SalaryUpperBound").val(upperBound);
		$(".salaryrange").find(".left").text("$" + toFormattedDigits(minSalary)).end().find(".right").text("$" + toFormattedDigits(maxSalary) + "+");
    }
	
	
	showErrInfo = function(context, errObj) {
		context.find(".errorinfo").show().find("ul").html("");
		$(".field .control", context).addClass("success").removeClass("error");
        $.each(errObj, function() {
			$("#" + this["Key"], context).closest(".control").addClass("error").removeClass("success");
			if (this["Key"] == "Tos")
				$("#ToName, #ToEmailAddress", context).closest(".control").addClass("error").removeClass("success");
			if (this["Key"] == "Tos") {
				$(".errorinfo ul", context).append("<li>Your friend's name is required.</li>");
				$(".errorinfo ul", context).append("<li>Your friend's email address is required.</li>");
			} else $(".errorinfo ul", context).append("<li>" + this["Message"] + "</li>");
        });
	}	
})(jQuery);