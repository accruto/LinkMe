(function($) {
	$(document).ready(function() {
		organiseFields();
		if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
			$("label[for='RememberMe']").addClass("label-RememberMe");
		}
		if ($(".login-error ul li").length > 0) $(".login-error").show();
		else $(".login-error").hide();
		$(".loginbutton").click(function() {
			if ($("#LoginId").val() == $("#LoginId").attr("data-watermark")) $("#LoginId").val("");
			$(this).closest("form").submit();
		});
		$("#LoginId, #Password").focus(function() {
			$(".login-error").hide();
		});
		$(".search_btn_holder").appendTo($(".main_columns"));
		$("#CandidateName").closest(".field").hide();
		$(".heavy:not(.small)").each(function() {
			$(this).after("<b>" + $(this).text() + "</b>").remove();
		});
		$(".help").each(function() {
			$(this).appendTo($(this).parent());
		});
		$("#AnyKeywords3").parent().prev().hide().next().hide().next().hide();
		$("#AnyKeywords3").closest(".control").contents().filter(function() {
			return this.nodeType == 3;
		}).remove();
		$("#AnyKeywords2").parent().prev().before("<b class='or-text'>or</b>");
		$("#JobTitle").closest(".control").find("div[class^='textbox']").insertBefore($(".radiobutton[value='RecentJobs']").parent());
        $(".js_options-holder a.resetOptions").click(function() {
            var contents = $(this).closest(".js_options-holder").find(".options_contents").first();
            if ($(this).parents(".js_options-holder").hasClass("js_keywords_advanced")) {
				$(".radiobutton", contents).removeClass("checked");
				$(".radiobutton:eq(0)", contents).addClass("checked");
            } else {
				$("#Distance").closest(".dropdown_control").find(".dropdown-item:contains('50 km')").click();
				$("#CountryId").closest(".dropdown_control").find(".dropdown-item:contains('Australia')").click();
				$(".include-relocating-holder .checkbox").removeClass("checked");
				$(".include-international-holder .checkbox").addClass("disabled").removeClass("checked");
            }
        });
		$("#Distance").attr("size", "9").parent().dropdown();
		$("#CountryId").attr("size", "13").parent().dropdown();
		$(".include-relocating-holder .checkbox").click(function() {
			if ($(this).hasClass("checked")) {
				$(".include-international-holder .checkbox").removeClass("disabled");
				$("#IncludeInternational").removeAttr("disabled");
			} else {
				$(".include-international-holder .checkbox").addClass("disabled");
				$("#IncludeInternational").attr("disabled", "disabled");
			}
		});
		$("#Location").addClass("text-label");
		$(".joinbutton, .learnmore, .section.jobad .explore, .section.iphone .appstore").text("");
		
		//2 youtube videos
		var params = { allowScriptAccess : "always", allowfullscreen : "true", wmode : "transparent" };
		var atts = { id: "whatislinkme" };
		swfobject.embedSWF($("#whatislinkme").attr("url"), "whatislinkme", "320", "275", "8", null, null, params, atts);
		atts = { id : "whyuselinkme" };
		swfobject.embedSWF($("#whyuselinkme").attr("url"), "whyuselinkme", "320", "275", "8", null, null, params, atts);
	});

    onYouTubePlayerReady = function(playerId) {
        var player = $("#" + playerId)[0];
        player.addEventListener("onStateChange", "onPlayerStateChange_" + playerId);
    }
	
	onPlayerStateChange_whatislinkme = function(newState) {
		if (newState == 1) _gaq.push(['_trackEvent', 'Video', 'Playing', "What is LinkMe? - on Employer Homepage"]);
		if (newState == 0) _gaq.push(['_trackEvent', 'Video', 'Played to the end', "What is LinkMe? - on Employer Homepage"]);
	}
	
	onPlayerStateChange_whyuselinkme = function(newState) {
		if (newState == 1) _gaq.push(['_trackEvent', 'Video', 'Playing', "Why use LinkMe? - on Employer Homepage"]);
		if (newState == 0) _gaq.push(['_trackEvent', 'Video', 'Played to the end', "Why use LinkMe? - on Employer Homepage"]);
	}

	organiseFields = function() {
		$(".control .textbox:not(.username_textbox, .password_textbox), .control input[type='text']:not(#LoginId)").each(function() {
			var textBg = $("<div class='textbox-bg'></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			//control.text("").append(textBg);
			control.append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
		});
		$(".control .password_textbox").each(function() {
			var id = $(this).attr("id");
			var fakePwd = "<input type='text' class='textbox' id='Fake" + id + "' data-watermark='" + $(this).attr("data-watermark") + "'>";
			$(this).hide().parent().append(fakePwd);
			$("#Fake" + id).show().focus(function() {
				if ($(this).val() == $(this).attr("data-watermark")) {
					$("#" + id).show().focus();
					$(this).hide();
				}
			});
		});
		$(".control .textbox:not(.password_textbox), .control input[type='text']:not(#LoginId)").each(function() {
			if ($(this).val() == "" && $(this).attr("data-watermark") != "") $(this).val($(this).attr("data-watermark")).addClass("mask");
			else $(this).removeClass("mask");
		}).focus(function() {
			if ($(this).attr("data-watermark") == undefined) $(this).removeClass("mask");
			else if ($(this).val() == $(this).attr("data-watermark")) $(this).val("").removeClass("mask");
		}).blur(function() {
			if ($(this).val() == "") $(this).val($(this).attr("data-watermark")).addClass("mask");
		});
		$(".control .textbox.password_textbox").blur(function() {
			if ($(this).val() == "") {
				$(this).hide();
				$("#Fake" + $(this).attr("id")).show().val($("#Fake" + $(this).attr("id")).attr("data-watermark"));
			}
		});
		$(".control select").each(function() {
			var textBg = $("<div class='textbox-bg'><input type='text' /></div>");
			var control = $(this).parent();
			$(this).appendTo(textBg);
			control.text("").append(textBg);
			textBg.before("<div class='textbox-left'></div>").after("<div class='textbox-right'></div>");
		});
		$(".checkbox_control, .include-relocating-holder, .include-international-holder").each(function() {
			var checkbox = $("<div class='checkbox'></div>");
			var id = $("input[type='checkbox']", this).hide().attr("id");
			checkbox.addClass(id).prependTo(this);
			if ($("input[type='checkbox']", this).is(":checked")) checkbox.addClass("checked");
			if ($("input[type='checkbox']", this).attr("disabled") == "disabled") checkbox.addClass("disabled");
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
		$(".js_jobtitle_advanced .field").each(function() {
			var radioButtonGroup = $(this);
			$(".control span", this).each(function() {
				var radiobutton = $("<div class='radiobutton'></div>");
				var id = $("input[type='radio']", this).hide().attr("id");
				radiobutton.attr("value", id).prependTo(this);
				if ($("input[type='radio']", this).is(":checked"))
					radiobutton.addClass("checked");
				radiobutton.click(function(event) {
					$(".radiobutton", radioButtonGroup).removeClass("checked");
					$(this).addClass("checked");
					$("input.radio", radioButtonGroup).removeAttr("checked");
					$("#" + id, radioButtonGroup).attr("checked", "checked");
					event.stopPropagation();
				});
			});
		});
	}
})(jQuery);

(function ($) {
	$.fn.dropdown = function() {
		return this.each(function() {
			var dd = $(this);

			dd.addClass("dropdown");
			
			//append vertical line and an arrow to simulate down arrow
			dd.after("<div class='dropdown-arrow'></div>").after("<div class='dropdown-vline'></div>");
			
			//readonly textbox, hide select box
			$("input[type='text']", dd).attr("readonly", "readonly");
			$("select", dd).hide();
			createScrollPan(dd.parent(), "dropdown");
			var scrollpan = $(".dropdown-scrollpan", dd.parent());
			scrollpan.hide();
			
			dd.click(function (event) {
				if (scrollpan.css("display") == "none") {
					$(".dropdown-scrollpan").hide();
					var parentWidth = dd.parent().width();
					if ($(".right-edge", dd.parent()).length > 0) parentWidth += 3;
					scrollpan.width(parentWidth);
					if ($.browser.msie && $.browser.version.indexOf("7") == 0)
						scrollpan.css("margin-left", "-" + parentWidth + "px");
					if (dd.find("option").length > dd.find("select").attr("size"))
						scrollpan.find(".dropdown-items").width(parentWidth - 12 - 1); //12 = scrollbarWidth, see below
					else
						scrollpan.find(".dropdown-items").width(parentWidth);
					scrollpan.show().css("z-index", "1000");
				} else {
					scrollpan.hide().css("z-index", "-1");
				}
				event.stopPropagation();
			});
			$(".dropdown-vline, .dropdown-arrow", dd.parent()).click(function() {
				dd.click();
			});
		});
	};
	
	$.fn.listbox = function() {
		return this.each(function() {
			var lb = $(this);
			lb.addClass("listbox");
			
			$("select", lb).hide();
			createScrollPan(lb, "listbox");
		});
	};
	
	//type = "dropdown" or "listbox"
	createScrollPan = function(parent, type) {
		//items
		var items = $("<div class='" + type + "-items'></div>");
		var scrollpan = $("<div class='" + type + "-scrollpan'></div>");
		scrollpan.append(items);
		scrollpan.width(parent.width() + "px");
		parent.append(scrollpan);
		$.each($("select option", parent), function() {
			var item = $("<div class='" + type + "-item'>" + $(this).text() + "</div>");
			item.click(function (e) {
				if (type == "dropdown") {
					scrollpan.hide();
					var index = $("." + type + "-item", $(this).parent()).index(this);
					$("select option", parent).removeAttr("selected");
					$("select option:nth-child(" + (index + 1) + ")", parent).attr("selected", "selected");
					$("input[type='text']", parent).val($(this).text());
				}
				if (type == "listbox") {
					if (e.ctrlKey) {
						if ($(this).hasClass("selected")) {
							//turn on
							$(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").removeAttr("selected");
							$(this).removeClass("selected");
						} else {
							//turn off
							if ($("." + type + "-item.selected", parent).length == $("select", parent).attr("maxcount")) return;
							else {
								$(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").attr("selected", "selected");
								$(this).addClass("selected");
							}
						}
					} else {
						$("." + type + "-item", parent).removeClass("selected");
						$(this).closest(".listbox").find("select option").removeAttr("selected");
						$(this).addClass("selected");
						$(this).closest(".listbox").find("select option:contains('" + $(this).text() + "')").attr("selected", "selected");
					}
				}
				e.stopPropagation();
			});
			items.append(item);
			if ($(this).attr("selected")) item.click();
		});
		
		//scroll pan and scroll bar
		var size = $("select", parent).attr("size");
		var length = $("." + type + "-item", items).length;
		var itemHeight = 26;
		scrollpan.height(size * itemHeight + 1);
		if (length > size) {
			var topArrow = $("<div class='" + type + "-scrollbar-toparrow-border'></div><div class='" + type + "-scrollbar-toparrow'></div>");
			var bottomArrow = $("<div class='" + type + "-scrollbar-bottomarrow-border'></div><div class='" + type + "-scrollbar-bottomarrow'></div>");
			var scrollbarWrap = $("<div class='" + type + "-scrollbar-wrap'></div>");
			var scrollbar = $("<div class='" + type + "-scrollbar'></div>");
			var diff = (length - size) * itemHeight;
			var portion = diff / items.height();
			var handleHeight = 48;
			var minValue = 0;
			var maxValue = 100;
			var scrollbarWidth = 12;
			scrollbarWrap.append(scrollbar);
			scrollpan.append(topArrow).append(scrollbarWrap).append(bottomArrow);
			scrollbar.slider({
				orientation: "vertical",
				min: minValue,
				max: maxValue,
				value: maxValue,
				slide: function(event, ui) {
					var topVal = -((maxValue - ui.value) * diff / 100);
					items.css("top", topVal);
				},
				change: function(event, ui) {
					var topVal = -((maxValue - ui.value) * diff / 100);
					items.css("top", topVal);
				}
			});
			scrollbarWrap.height(size * itemHeight - 23);
			scrollbar.height(size * itemHeight - handleHeight - 23);
			items.width(parent.width() - scrollbarWidth - 1);
			scrollbarWrap.mousedown(function(e) {
				if ($(e.srcElement).hasClass("ui-slider-handle")) return;
				scrollbar.slider("value", 100 - (e.offsetY / scrollbarWrap.height() * 100));
			});
			$("." + type + "-scrollbar-toparrow, ." + type + "-scrollbar-toparrow-border", scrollpan).click(function() {
				scrollbar.slider("value", scrollbar.slider("value") + 10);
			});
			$("." + type + "-scrollbar-bottomarrow, ." + type + "-scrollbar-bottomarrow-border", scrollpan).click(function() {
				scrollbar.slider("value", scrollbar.slider("value") - 10);
			});
			if (type == "listbox") parent.height(scrollpan.height());
		}
	};
})(jQuery);